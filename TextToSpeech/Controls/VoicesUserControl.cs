using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using JocysCom.ClassLibrary.Controls;
using JocysCom.TextToSpeech.Monitor.Audio;
using JocysCom.TextToSpeech.Monitor.Voices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
	public partial class VoicesUserControl : UserControl
	{
		public VoicesUserControl()
		{
			InitializeComponent();
			if (ControlsHelper.IsDesignMode(this))
				return;
			StatusPanel.Visible = false;
			VoicesDataGridView.AutoGenerateColumns = false;
			// Enable double buffering to make redraw faster.
			typeof(DataGridView).InvokeMember("DoubleBuffered",
			BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
			null, VoicesDataGridView, new object[] { true });
		}

		public DataGridView VoicesGridView
		{
			get { return VoicesDataGridView; }
		}

		public void EnableGrid(bool enabled)
		{
			VoicesDataGridView.Enabled = enabled;
			VoicesDataGridView.DefaultCellStyle.SelectionBackColor = enabled
				? SystemColors.Highlight
				: SystemColors.ControlDark;
		}


		public List<InstalledVoiceEx> GetSelectedItems()
		{
			var items = VoicesGridView.SelectedRows.Cast<DataGridViewRow>()
				.Select(x => (InstalledVoiceEx)x.DataBoundItem)
				.ToList();
			return items;
		}

		public void SelectItem(InstalledVoiceEx item)
		{
			// Select rows first.
			foreach (DataGridViewRow row in VoicesDataGridView.Rows)
			{
				if (row.DataBoundItem.Equals(item) && !row.Selected)
					row.Selected = true;
			}
			// Unselect rows.
			foreach (DataGridViewRow row in VoicesDataGridView.Rows)
			{
				if (!row.DataBoundItem.Equals(item) && row.Selected)
					row.Selected = false;
			}
		}

		#region Appearance

		/// <summary>
		/// Gets or sets the object used to marshal event-handler calls that are issued when
		/// an interval has elapsed.
		/// </summary>
		[Category("Appearance"), Browsable(true), DefaultValue(true)]
		public bool MenuButtonsVisible { get { return VoicesToolStrip.Visible; } set { VoicesToolStrip.Visible = value; } }

		/// <summary>
		/// Gets or sets the object used to marshal event-handler calls that are issued when
		/// an interval has elapsed.
		/// </summary>
		[Category("Appearance"), Browsable(true), DefaultValue(true)]
		public bool GenderRatesVisible
		{
			get
			{
				return
					MaleColumn.Visible &
					FemaleColumn.Visible &
					NeutralColumn.Visible;
			}
			set
			{
				MaleColumn.Visible = value;
				FemaleColumn.Visible = value;
				NeutralColumn.Visible = value;
			}
		}

		#endregion

		#region VoicesDataGridView

		private void VoicesDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			//if (e.RowIndex == -1) return;
			//var grid = (DataGridView)sender;
			//var voice = (InstalledVoiceEx)grid.Rows[e.RowIndex].DataBoundItem;
			//var column = VoicesDataGridView.Columns[e.ColumnIndex];
			e.Cancel = true;
		}

		private void VoicesDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex == -1) return;
			var grid = (DataGridView)sender;
			var voice = (InstalledVoiceEx)grid.Rows[e.RowIndex].DataBoundItem;
			var column = VoicesDataGridView.Columns[e.ColumnIndex];
			if (e.ColumnIndex == grid.Columns[AgeColumn.Name].Index)
			{
				if (voice.Age.ToString() == "NotSet") e.Value = "...";
			}
			// If main window then...
			if (MenuButtonsVisible)
			{
				e.CellStyle.ForeColor = voice.Enabled
					? VoicesDataGridView.DefaultCellStyle.ForeColor
					: SystemColors.ControlDark;
				e.CellStyle.SelectionBackColor = voice.Enabled
				 ? VoicesDataGridView.DefaultCellStyle.SelectionBackColor
				 : SystemColors.ControlDark;
			}
		}

		private void VoicesDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;
			var grid = (DataGridView)sender;
			//var column = VoicesDataGridView.Columns[e.ColumnIndex];
			if (e.ColumnIndex == grid.Columns[EnabledColumn.Name].Index)
			{
				var voice = (InstalledVoiceEx)grid.Rows[e.RowIndex].DataBoundItem;
				voice.Enabled = !voice.Enabled;
				VoicesDataGridView.Invalidate();
			}
			if (e.ColumnIndex == grid.Columns[FemaleColumn.Name].Index) VoicesDataGridView.BeginEdit(true);
			if (e.ColumnIndex == grid.Columns[MaleColumn.Name].Index) VoicesDataGridView.BeginEdit(true);
			if (e.ColumnIndex == grid.Columns[NeutralColumn.Name].Index) VoicesDataGridView.BeginEdit(true);
		}


		#endregion


		void AddVoices(string name, BindingList<InstalledVoiceEx> from)
		{
			var form = new AddVoicesForm();
			form.Text = string.Format("{0} {1}: {2}", Application.CompanyName, Application.ProductName, name);
			form.StartPosition = FormStartPosition.CenterParent;
			form.VoicesGridView.DataSource = from;
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
			{
				var voices = from.Where(x => x.Enabled).ToList();
				Global.ImportVoices(Global.InstalledVoices, voices);
			}
			form.VoicesGridView.DataSource = null;
		}


		private void AddLocalVoicesButton_Click(object sender, System.EventArgs e)
		{
			AddVoices("Local Voices", Global.LocalVoices);
		}

		private void AddAmazonNeuralVoicesButton_Click(object sender, System.EventArgs e)
		{
			AddVoices("Amazon Neural Voices", Global.AmazonNeuralVoices);
		}

		private void AddAmazonStandardVoicesButton_Click(object sender, System.EventArgs e)
		{
			AddVoices("Amazon Standard Voices", Global.AmazonStandardVoices);
		}

		// Used from AddVoicesForm only.
		public void RefreshVoices(bool force = false)
		{
			var list = (BindingList<InstalledVoiceEx>)VoicesGridView.DataSource;
			//// Uncehck selection by default.
			//foreach (var item in list)
			//	if (item.Enabled)
			//		item.Enabled = false;
			if (list == Global.LocalVoices)
				RefreshLocalVoices(list);
			if (list == Global.AmazonNeuralVoices && (list.Count == 0 || force))
				RefreshAmazonVoices(Engine.Neural, list);
			if (list == Global.AmazonStandardVoices && (list.Count == 0 || force))
				RefreshAmazonVoices(Engine.Standard, list);
		}

		void RefreshLocalVoices(BindingList<InstalledVoiceEx> list)
		{
			// Start refreshing voices.
			ControlsHelper.BeginInvoke(() =>
			{
				list.Clear();
				var voices = Voices.VoiceHelper.GetLocalVoices();
				voices = RemoveVoices(voices, Global.InstalledVoices);
				// Uncehck selection by default.
				//foreach (var item in list)
				//	if (item.Enabled)
				//		item.Enabled = false;
				foreach (var voice in voices)
					list.Add(voice);
			});
		}

		Thread _Thread;
		bool _CancelGetVoices;

		void RefreshAmazonVoices(Engine engine, BindingList<InstalledVoiceEx> list)
		{
			_CancelGetVoices = false;
			StatusLabel.Text = "Scan all supported voices. Please Wait...\r\n";
			StatusPanel.Visible = true;
			list.Clear();
			var voices = new List<InstalledVoiceEx>();
			var ts = new System.Threading.ThreadStart(delegate ()
			{
				voices = GetAmazonVoices(null, engine, null);
				// Uncehck selection by default.
				//foreach (var item in list)
				//	if (item.Enabled)
				//		item.Enabled = false;
				ControlsHelper.Invoke(() =>
				{
					foreach (var voice in voices)
						list.Add(voice);
					StatusPanel.Visible = voices.Count == 0;
				});
			});
			_Thread = new Thread(ts);
			_Thread.Start();
		}

		public List<InstalledVoiceEx> GetAmazonVoices(RegionEndpoint region = null, Engine engine = null, CultureInfo culture = null)
		{
			var list = new List<InstalledVoiceEx>();
			// Get regions to process.
			var regions = region == null
				? RegionEndpoint.EnumerableAllRegions.OrderBy(x => x.ToString()).ToList()
				: new List<RegionEndpoint>() { region };
			for (int r = 0; r < regions.Count; r++)
			{
				var reg = regions[r];
				AmazonPolly client = null;
				try
				{
					client = new AmazonPolly(
						SettingsManager.Options.AmazonAccessKey,
						SettingsManager.Options.AmazonSecretKey,
						reg.SystemName
					);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
					continue;
				}
				var request = new DescribeVoicesRequest();
				if (engine != null)
					request.Engine = engine;
				if (culture != null)
					request.LanguageCode = culture.Name;
				AddStatus("{0}/{1} - Region={2}, Engine={3}, Culture={4}",
					r + 1, regions.Count, reg.DisplayName, request.Engine, request.LanguageCode);
				// Create stop watch to measure speed with the servers.
				var sw = new Stopwatch();
				var voices = client.GetVoices(request, 5000);
				var elapsed = sw.Elapsed;
				AddStatus(", Voices={0}", voices.Count);
				if (client.LastException != null)
					AddStatus("\r\n       Exception: {0}\r\n", client.LastException.Message);
				else if (voices.Count == 0)
					AddStatus("\r\n");
				if (voices.Count == 0)
					continue;
				var vex = 0;
				for (int v = 0; v < voices.Count; v++)
				{
					var voice = voices[v];
					var cultureNames = new List<string>();
					cultureNames.Add(voice.LanguageCode);
					cultureNames.AddRange(voice.AdditionalLanguageCodes);
					// Add extra cultures.
					foreach (var cultureName in cultureNames)
					{
						// Add engines.
						var c = new CultureInfo(cultureName);
						foreach (var engineName in voice.SupportedEngines)
						{
							var vx = new InstalledVoiceEx(voice);
							vx.SetCulture(c);
							vx.SourceRequestSpeed = elapsed;
							var keys = System.Web.HttpUtility.ParseQueryString("");
							keys.Add("source", vx.Source.ToString());
							keys.Add("region", reg.SystemName);
							keys.Add("culture", cultureName);
							keys.Add("engine", engineName);
							vx.SourceKeys = keys.ToString();
							// Add voice.
							list.Add(vx);
							vex++;
							if (_CancelGetVoices)
								return null;
						}
					}
				}
				AddStatus(", VoicesEx={0}\r\n", vex);
			}
			AddStatus("Done\r\n");
			list = RemoveDuplicateVoices(list);
			list = RemoveVoices(list, Global.InstalledVoices);
			return list;
		}

		void AddStatus(string format, params object[] args)
		{
			ControlsHelper.Invoke(() =>
			{
				StatusLabel.Text += string.Format(format, args);
				StatusPanel.VerticalScroll.Value = StatusPanel.VerticalScroll.Maximum;
			});
		}

		List<InstalledVoiceEx> RemoveVoices(List<InstalledVoiceEx> list, IList<InstalledVoiceEx> excludeList)
		{
			var newList = new List<InstalledVoiceEx>();
			for (int i = 0; i < list.Count; i++)
			{
				var voice = list[i];
				// Try to find same voice in new List.
				var contains = excludeList.Any(x => x.IsSameVoice(voice));
				// If item was not found then add voice to the list.
				if (!contains)
					newList.Add(voice);
			}
			// Reorder list.
			return newList.OrderBy(x => x.CultureName).ThenBy(x => x.Name).ToList();
		}

		List<InstalledVoiceEx> RemoveDuplicateVoices(List<InstalledVoiceEx> list)
		{
			var newList = new List<InstalledVoiceEx>();
			// Make sure that fastest to retrieve from the Internet voices on the top.
			list = list.OrderBy(x => x.SourceRequestSpeed).ToList();
			for (int i = 0; i < list.Count; i++)
			{
				var voice = list[i];
				// Try to find same voice in new List.
				var newItem = newList.FirstOrDefault(x => x.IsSameVoice(voice));
				// If item was not found then add voice to the list.
				if (newItem == null)
					newList.Add(voice);
			}
			// Reorder list.
			return newList.OrderBy(x => x.CultureName).ThenBy(x => x.Name).ToList();
		}

		private void AmazonPolly_Exception(object sender, ClassLibrary.EventArgs<System.Exception> e)
		{
			throw new System.NotImplementedException();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_CancelGetVoices = true;
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void RemoveButton_Click(object sender, EventArgs e)
		{
			var items = GetSelectedItems();
			var message = string.Format("Are you sure you want to remove {0} item{1}?",
					items.Count, items.Count == 1 ? "" : "s");
			var form = new MessageBoxForm();
			form.StartPosition = FormStartPosition.CenterParent;
			var result = form.ShowForm(message, "Remove", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
			if (result == DialogResult.OK)
			{
				VoicesDataGridView.ClearSelection();
				var list = (BindingList<InstalledVoiceEx>)VoicesGridView.DataSource;
				foreach (var item in items)
				{

					list.Remove(item);
				}
			}
		}
	}
}