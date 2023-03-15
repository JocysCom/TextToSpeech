using Amazon;
using Amazon.Polly;
using JocysCom.ClassLibrary.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using JocysCom.TextToSpeech.Monitor.Audio;
using System.ComponentModel;
using JocysCom.TextToSpeech.Monitor.Voices;
using Amazon.Polly.Model;
using System.Diagnostics;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
	public partial class AddVoicesForm : Form
	{
		public AddVoicesForm()
		{
			InitializeComponent();
			if (ControlsHelper.IsDesignMode(this))
				return;
		}

		private void List_ListChanged(object sender, ListChangedEventArgs e)
		{
			var list = (BindingList<InstalledVoiceEx>)sender;
			OkButton.Enabled = list.Any(x => x.Enabled);
		}

		public DataGridView VoicesGridView
		{
			get { return VoicesPanel.VoicesGridView; }
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void CloseButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void RefreshButton_Click(object sender, EventArgs e)
		{
			RefreshVoices(true);
		}

		private void AddVoicesForm_Load(object sender, EventArgs e)
		{
			RefreshVoices();
			var list = (BindingList<InstalledVoiceEx>)VoicesGridView.DataSource;
			list.ListChanged += List_ListChanged;
			OkButton.Enabled = list.Any(x => x.Enabled);
		}

		#region Parallel tasks.

		public class RegionTaskSettings
		{
			public string Name { get; set; }
			public RegionEndpoint Region { get; set; }
			public List<InstalledVoiceEx> Voices { get; set; }
			public Engine Engine { get; set; }
			public CultureInfo Culture { get; set; }
		}

		int ParallelCount;
		int ParallelTotal;
		object ParallelReportLock = new object();
		string ParalelLineFormat;

		public void ParallelAction(List<RegionTaskSettings> settingsList, Func<RegionTaskSettings, string> action, string group, int parallelTasks = 16)
		{
			ParallelCount = 0;
			ParallelTotal = settingsList.Count;
			var maxName = settingsList.Max(x => x.Name.Length);
			ParalelLineFormat = "{0} {1,5:0.0}% - {2," + ParallelTotal.ToString().Length + "}. {3,-" + maxName + "} - {4}\r\n";
			Parallel.ForEach(settingsList,
			new ParallelOptions { MaxDegreeOfParallelism = parallelTasks },
			   x => ParallelItemAction(x, action, group)
			);
		}

		public void ParallelItemAction(RegionTaskSettings settings, Func<RegionTaskSettings, string> action, string group)
		{
			string result;
			try
			{
				result = string.Format("{0}", action(settings));
			}
			catch (Exception ex)
			{
				result = string.Format("Exception: {0}", ex.Message);
			}
			// Report.
			lock (ParallelReportLock)
			{
				System.Threading.Interlocked.Increment(ref ParallelCount);
				var percent = (decimal)ParallelCount / (decimal)ParallelTotal * 100m;
				AddStatus(ParalelLineFormat, group, percent, ParallelCount, settings.Name, result);
			}
		}

		#endregion

		void RefreshAmazonVoices(Engine engine, BindingList<InstalledVoiceEx> list)
		{
			_CancelGetVoices = false;
			StatusLabel.Text = "";
			LogTabPage.Text = "Log: Scanning voices. Please wait...";
			RefreshButton.Enabled = false;
			MainTabControl.SelectedTab = LogTabPage;
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
					if (voices.Count == 0)
						StatusLabel.Text += "No new voices found.";
					if (voices.Count > 0)
						MainTabControl.SelectedTab = VoicesTabPage;
					LogTabPage.Text = "Log";
					RefreshButton.Enabled = true;
				});
			});
			_Thread = new Thread(ts);
			_Thread.Start();
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

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				var list = (BindingList<InstalledVoiceEx>)VoicesGridView.DataSource;
				list.ListChanged -= List_ListChanged;
				_CancelGetVoices = true;
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}


		Thread _Thread;
		bool _CancelGetVoices;

		public List<InstalledVoiceEx> GetAmazonVoices(RegionEndpoint region = null, Engine engine = null, CultureInfo culture = null)
		{
			// Get regions to process.
			var regions = region == null
				? RegionEndpoint.EnumerableAllRegions.OrderBy(x => x.ToString()).ToList()
				: new List<RegionEndpoint>() { region };
			var settingsList = regions.Select(x => new RegionTaskSettings()
			{
				Region = x,
				Engine = engine,
				Culture = culture,
				Name = string.Format(
					"{0}{1}", x.DisplayName,
					culture == null ? "" : string.Format(", Culture={0}", culture.Name)),
			}).ToList();
			var group = engine == null ? "All" : engine.Value;
			ParallelAction(settingsList, GetVoices, group, 16);
			AddStatus("{0} Done\r\n", group);
			var list = new List<InstalledVoiceEx>();
			foreach (var item in settingsList)
				list.AddRange(item.Voices);
			// Filter list by engine.
			if (engine != null)
				list = list.Where(x => x.AmazonEngine == engine).ToList();
			list = RemoveDuplicateVoices(list);
			list = RemoveVoices(list, Global.InstalledVoices);
			return list;
		}

		string GetVoices(RegionTaskSettings settings)
		{
			var region = settings.Region;
			var engine = settings.Engine;
			var culture = settings.Culture;
			settings.Voices = new List<InstalledVoiceEx>();
			//var engine = reg.e		
			Voices.AmazonVoiceClient client;
			try
			{
				client = new Voices.AmazonVoiceClient(
					SettingsManager.Options.AmazonAccessKey,
					SettingsManager.Options.AmazonSecretKey,
					region.SystemName
				);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return ex.Message;
			}
			// Create stop watch to measure speed with the servers.
			var sw = new Stopwatch();
			sw.Start();
			var voices = client.GetVoices(culture?.Name, engine == Engine.Neural, 5000);
			var elapsed = sw.Elapsed;
			if (client.LastException != null)
				return string.Format("Exception: {0}", client.LastException.Message);
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
						var vx = client.Convert(voice);
						vx.SetCulture(c);
						vx.SourceRequestSpeed = elapsed;
						// Store custom data whi
						var keys = System.Web.HttpUtility.ParseQueryString("");
						keys.Add(InstalledVoiceEx._KeySource, vx.Source.ToString());
						keys.Add(InstalledVoiceEx._KeyRegion, client.Client.Config.RegionEndpoint.SystemName);
						keys.Add(InstalledVoiceEx._KeyCulture, cultureName);
						keys.Add(InstalledVoiceEx._KeyEngine, engineName);
						keys.Add(InstalledVoiceEx._KeyVoiceId, voice.Id);
						vx.SourceKeys = keys.ToString();
						// Override Description.
						vx.Description = string.Format("{0}, {1}, {2}, {3}",
							vx.Source, client.Client.Config.RegionEndpoint.DisplayName, cultureName, engineName);
						// Add voice.
						settings.Voices.Add(vx);
						vex++;
						if (_CancelGetVoices)
							return "Cancelled";
					}
				}
			}
			return string.Format("Time (ms): {0,4}, Voices: {1,2}", elapsed.Milliseconds, voices.Count);
		}

		void AddStatus(string format, params object[] args)
		{
			ControlsHelper.Invoke(() =>
			{
				StatusLabel.Text += string.Format(format, args);
				BodyPanel.VerticalScroll.Value = BodyPanel.VerticalScroll.Maximum;
			});
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
				var client = new WindowsVoiceClient();
				var voices = client.GetVoices()
					.Select(x=> client.Convert(x))
					.ToList();
				voices = RemoveVoices(voices, Global.InstalledVoices);
				// Uncehck selection by default.
				//foreach (var item in list)
				//	if (item.Enabled)
				//		item.Enabled = false;
				foreach (var voice in voices)
					list.Add(voice);
			});
		}

	}
}
