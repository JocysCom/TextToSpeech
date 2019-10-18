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
using System.Threading.Tasks;
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
			if (IsAmazonConfigurationValid())
				AddVoices("Amazon Neural Voices", Global.AmazonNeuralVoices);
		}

		private void AddAmazonStandardVoicesButton_Click(object sender, System.EventArgs e)
		{
			if (IsAmazonConfigurationValid())
				AddVoices("Amazon Standard Voices", Global.AmazonStandardVoices);
		}

		bool IsAmazonConfigurationValid()
		{
			var isValid =
				!string.IsNullOrEmpty(SettingsManager.Options.AmazonAccessKey) &&
				!string.IsNullOrEmpty(SettingsManager.Options.AmazonSecretKey);
			if (!isValid)
			{
				var message = "";
				message += "Amazon \"Access key\" and \"Secret Key\" is not configured.\r\n";
				message += "Would you like to go to [Options] and configure [Amazon Polly] settings?";
				var form = new MessageBoxForm();
				form.StartPosition = FormStartPosition.CenterParent;
				var result = form.ShowForm(message, "Amazon Polly Options", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (result == DialogResult.Yes)
				{
					ControlsHelper.BeginInvoke(() =>
					{
						Program.TopForm.OptionsPanel.OptionsTabControl.SelectedTab = Program.TopForm.OptionsPanel.AmazonPollyTabPage;
						Program.TopForm.MessagesTabControl.SelectedTab = Program.TopForm.OptionsTabPage;
					});
				}
				form.Dispose();
			}
			return isValid;
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
			form.Dispose();
		}


	}
}