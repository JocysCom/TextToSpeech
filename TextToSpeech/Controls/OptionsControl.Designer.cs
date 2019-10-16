namespace JocysCom.TextToSpeech.Monitor.Controls
{
	partial class OptionsControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsControl));
			this.OptionsTabControl = new System.Windows.Forms.TabControl();
			this.GeneralTabPage = new System.Windows.Forms.TabPage();
			this.CacheTabPage = new System.Windows.Forms.TabPage();
			this.CachePanel = new JocysCom.TextToSpeech.Monitor.Controls.CacheOptionsUserControl();
			this.GoogleTTSTabPage = new System.Windows.Forms.TabPage();
			this.GoogleCloudPanel = new JocysCom.TextToSpeech.Monitor.Google.GoogleTTSUserControl();
			this.AmazonPollyTabPage = new System.Windows.Forms.TabPage();
			this.AmazonPollyPanel = new JocysCom.TextToSpeech.Monitor.Controls.Options.AmazonPollyUserControl();
			this.MicrosoftCortanaTabPage = new System.Windows.Forms.TabPage();
			this.MicrosoftCortanaPanel = new JocysCom.TextToSpeech.Monitor.Controls.MicrosoftCortanaOptionsUserControl();
			this.MonitorDispalyTabPage = new System.Windows.Forms.TabPage();
			this.monitorDisplayUserControl1 = new JocysCom.TextToSpeech.Monitor.Controls.MonitorDisplayUserControl();
			this.MonitorNetworkTabPage = new System.Windows.Forms.TabPage();
			this.CapturingPanel = new JocysCom.TextToSpeech.Monitor.Controls.MonitorNetworkUserControl();
			this.MonitorServerTabPage = new System.Windows.Forms.TabPage();
			this.monitorUdpPortUserControl1 = new JocysCom.TextToSpeech.Monitor.Controls.MonitorServerUserControl();
			this.MonitorClipBoardTabPage = new System.Windows.Forms.TabPage();
			this.monitorClipboardUserControl1 = new JocysCom.TextToSpeech.Monitor.Controls.MonitorClipboardUserControl();
			this.TabsImageList = new System.Windows.Forms.ImageList(this.components);
			this.GeneralOptionsPanel = new JocysCom.TextToSpeech.Monitor.Controls.Options.GeneralOptionsUserControl();
			this.OptionsTabControl.SuspendLayout();
			this.GeneralTabPage.SuspendLayout();
			this.CacheTabPage.SuspendLayout();
			this.GoogleTTSTabPage.SuspendLayout();
			this.AmazonPollyTabPage.SuspendLayout();
			this.MicrosoftCortanaTabPage.SuspendLayout();
			this.MonitorDispalyTabPage.SuspendLayout();
			this.MonitorNetworkTabPage.SuspendLayout();
			this.MonitorServerTabPage.SuspendLayout();
			this.MonitorClipBoardTabPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// OptionsTabControl
			// 
			this.OptionsTabControl.Controls.Add(this.GeneralTabPage);
			this.OptionsTabControl.Controls.Add(this.CacheTabPage);
			this.OptionsTabControl.Controls.Add(this.GoogleTTSTabPage);
			this.OptionsTabControl.Controls.Add(this.AmazonPollyTabPage);
			this.OptionsTabControl.Controls.Add(this.MicrosoftCortanaTabPage);
			this.OptionsTabControl.Controls.Add(this.MonitorDispalyTabPage);
			this.OptionsTabControl.Controls.Add(this.MonitorNetworkTabPage);
			this.OptionsTabControl.Controls.Add(this.MonitorServerTabPage);
			this.OptionsTabControl.Controls.Add(this.MonitorClipBoardTabPage);
			this.OptionsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OptionsTabControl.ImageList = this.TabsImageList;
			this.OptionsTabControl.Location = new System.Drawing.Point(0, 0);
			this.OptionsTabControl.Name = "OptionsTabControl";
			this.OptionsTabControl.SelectedIndex = 0;
			this.OptionsTabControl.Size = new System.Drawing.Size(914, 367);
			this.OptionsTabControl.TabIndex = 11;
			// 
			// GeneralTabPage
			// 
			this.GeneralTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.GeneralTabPage.Controls.Add(this.GeneralOptionsPanel);
			this.GeneralTabPage.ImageKey = "Options_General";
			this.GeneralTabPage.Location = new System.Drawing.Point(4, 23);
			this.GeneralTabPage.Name = "GeneralTabPage";
			this.GeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.GeneralTabPage.Size = new System.Drawing.Size(906, 340);
			this.GeneralTabPage.TabIndex = 0;
			this.GeneralTabPage.Text = "General";
			// 
			// CacheTabPage
			// 
			this.CacheTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.CacheTabPage.Controls.Add(this.CachePanel);
			this.CacheTabPage.ImageKey = "Options_Cache";
			this.CacheTabPage.Location = new System.Drawing.Point(4, 23);
			this.CacheTabPage.Name = "CacheTabPage";
			this.CacheTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.CacheTabPage.Size = new System.Drawing.Size(906, 340);
			this.CacheTabPage.TabIndex = 1;
			this.CacheTabPage.Text = "Cache";
			// 
			// CachePanel
			// 
			this.CachePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CachePanel.Location = new System.Drawing.Point(3, 3);
			this.CachePanel.Name = "CachePanel";
			this.CachePanel.Size = new System.Drawing.Size(900, 334);
			this.CachePanel.TabIndex = 0;
			// 
			// GoogleTTSTabPage
			// 
			this.GoogleTTSTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.GoogleTTSTabPage.Controls.Add(this.GoogleCloudPanel);
			this.GoogleTTSTabPage.ImageKey = "Options_Google_TTS";
			this.GoogleTTSTabPage.Location = new System.Drawing.Point(4, 23);
			this.GoogleTTSTabPage.Name = "GoogleTTSTabPage";
			this.GoogleTTSTabPage.Size = new System.Drawing.Size(906, 340);
			this.GoogleTTSTabPage.TabIndex = 2;
			this.GoogleTTSTabPage.Text = "Google TTS";
			// 
			// GoogleCloudPanel
			// 
			this.GoogleCloudPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GoogleCloudPanel.Location = new System.Drawing.Point(0, 0);
			this.GoogleCloudPanel.Name = "GoogleCloudPanel";
			this.GoogleCloudPanel.Size = new System.Drawing.Size(906, 340);
			this.GoogleCloudPanel.TabIndex = 0;
			// 
			// AmazonPollyTabPage
			// 
			this.AmazonPollyTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.AmazonPollyTabPage.Controls.Add(this.AmazonPollyPanel);
			this.AmazonPollyTabPage.ImageKey = "Options_Amazon_Polly";
			this.AmazonPollyTabPage.Location = new System.Drawing.Point(4, 23);
			this.AmazonPollyTabPage.Name = "AmazonPollyTabPage";
			this.AmazonPollyTabPage.Size = new System.Drawing.Size(906, 340);
			this.AmazonPollyTabPage.TabIndex = 8;
			this.AmazonPollyTabPage.Text = "Amazon Polly";
			// 
			// AmazonPollyPanel
			// 
			this.AmazonPollyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AmazonPollyPanel.Location = new System.Drawing.Point(0, 0);
			this.AmazonPollyPanel.Name = "AmazonPollyPanel";
			this.AmazonPollyPanel.Size = new System.Drawing.Size(906, 340);
			this.AmazonPollyPanel.TabIndex = 0;
			// 
			// MicrosoftCortanaTabPage
			// 
			this.MicrosoftCortanaTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.MicrosoftCortanaTabPage.Controls.Add(this.MicrosoftCortanaPanel);
			this.MicrosoftCortanaTabPage.ImageKey = "Options_Microsoft_Cortana";
			this.MicrosoftCortanaTabPage.Location = new System.Drawing.Point(4, 23);
			this.MicrosoftCortanaTabPage.Name = "MicrosoftCortanaTabPage";
			this.MicrosoftCortanaTabPage.Size = new System.Drawing.Size(906, 340);
			this.MicrosoftCortanaTabPage.TabIndex = 3;
			this.MicrosoftCortanaTabPage.Text = "Microsoft Cortana";
			// 
			// MicrosoftCortanaPanel
			// 
			this.MicrosoftCortanaPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MicrosoftCortanaPanel.Location = new System.Drawing.Point(0, 0);
			this.MicrosoftCortanaPanel.Name = "MicrosoftCortanaPanel";
			this.MicrosoftCortanaPanel.Size = new System.Drawing.Size(906, 340);
			this.MicrosoftCortanaPanel.TabIndex = 0;
			// 
			// MonitorDispalyTabPage
			// 
			this.MonitorDispalyTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.MonitorDispalyTabPage.Controls.Add(this.monitorDisplayUserControl1);
			this.MonitorDispalyTabPage.ImageKey = "Options_Monitor_Display";
			this.MonitorDispalyTabPage.Location = new System.Drawing.Point(4, 23);
			this.MonitorDispalyTabPage.Name = "MonitorDispalyTabPage";
			this.MonitorDispalyTabPage.Size = new System.Drawing.Size(906, 340);
			this.MonitorDispalyTabPage.TabIndex = 6;
			this.MonitorDispalyTabPage.Text = "Monitor: Display";
			// 
			// monitorDisplayUserControl1
			// 
			this.monitorDisplayUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.monitorDisplayUserControl1.Location = new System.Drawing.Point(0, 0);
			this.monitorDisplayUserControl1.Name = "monitorDisplayUserControl1";
			this.monitorDisplayUserControl1.Size = new System.Drawing.Size(906, 340);
			this.monitorDisplayUserControl1.TabIndex = 0;
			// 
			// MonitorNetworkTabPage
			// 
			this.MonitorNetworkTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.MonitorNetworkTabPage.Controls.Add(this.CapturingPanel);
			this.MonitorNetworkTabPage.ImageKey = "Options_Monitor_Network";
			this.MonitorNetworkTabPage.Location = new System.Drawing.Point(4, 23);
			this.MonitorNetworkTabPage.Name = "MonitorNetworkTabPage";
			this.MonitorNetworkTabPage.Size = new System.Drawing.Size(906, 340);
			this.MonitorNetworkTabPage.TabIndex = 4;
			this.MonitorNetworkTabPage.Text = "Monitor: Network";
			// 
			// CapturingPanel
			// 
			this.CapturingPanel._CapturingType = JocysCom.TextToSpeech.Monitor.Capturing.CapturingType.SocPcap;
			this.CapturingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CapturingPanel.Location = new System.Drawing.Point(0, 0);
			this.CapturingPanel.Name = "CapturingPanel";
			this.CapturingPanel.Size = new System.Drawing.Size(906, 340);
			this.CapturingPanel.TabIndex = 0;
			// 
			// MonitorServerTabPage
			// 
			this.MonitorServerTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.MonitorServerTabPage.Controls.Add(this.monitorUdpPortUserControl1);
			this.MonitorServerTabPage.ImageKey = "Options_Monitor_Server";
			this.MonitorServerTabPage.Location = new System.Drawing.Point(4, 23);
			this.MonitorServerTabPage.Name = "MonitorServerTabPage";
			this.MonitorServerTabPage.Size = new System.Drawing.Size(906, 340);
			this.MonitorServerTabPage.TabIndex = 5;
			this.MonitorServerTabPage.Text = "Monitor: Server";
			// 
			// monitorUdpPortUserControl1
			// 
			this.monitorUdpPortUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.monitorUdpPortUserControl1.Location = new System.Drawing.Point(0, 0);
			this.monitorUdpPortUserControl1.Name = "monitorUdpPortUserControl1";
			this.monitorUdpPortUserControl1.Size = new System.Drawing.Size(906, 340);
			this.monitorUdpPortUserControl1.TabIndex = 0;
			// 
			// MonitorClipBoardTabPage
			// 
			this.MonitorClipBoardTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.MonitorClipBoardTabPage.Controls.Add(this.monitorClipboardUserControl1);
			this.MonitorClipBoardTabPage.ImageKey = "Options_Monitor_Clipboard";
			this.MonitorClipBoardTabPage.Location = new System.Drawing.Point(4, 23);
			this.MonitorClipBoardTabPage.Name = "MonitorClipBoardTabPage";
			this.MonitorClipBoardTabPage.Size = new System.Drawing.Size(906, 340);
			this.MonitorClipBoardTabPage.TabIndex = 7;
			this.MonitorClipBoardTabPage.Text = "Monitor: Clipboard";
			// 
			// monitorClipboardUserControl1
			// 
			this.monitorClipboardUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.monitorClipboardUserControl1.Location = new System.Drawing.Point(0, 0);
			this.monitorClipboardUserControl1.Name = "monitorClipboardUserControl1";
			this.monitorClipboardUserControl1.Size = new System.Drawing.Size(906, 340);
			this.monitorClipboardUserControl1.TabIndex = 0;
			// 
			// TabsImageList
			// 
			this.TabsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TabsImageList.ImageStream")));
			this.TabsImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.TabsImageList.Images.SetKeyName(0, "Options_Cache");
			this.TabsImageList.Images.SetKeyName(1, "Options_General");
			this.TabsImageList.Images.SetKeyName(2, "Options_Google_TTS");
			this.TabsImageList.Images.SetKeyName(3, "Options_Amazon_Polly");
			this.TabsImageList.Images.SetKeyName(4, "Options_Microsoft_Cortana");
			this.TabsImageList.Images.SetKeyName(5, "Options_Monitor_Display");
			this.TabsImageList.Images.SetKeyName(6, "Options_Monitor_Network");
			this.TabsImageList.Images.SetKeyName(7, "Options_Monitor_Server");
			this.TabsImageList.Images.SetKeyName(8, "Options_Monitor_Clipboard");
			// 
			// GeneralOptionsPanel
			// 
			this.GeneralOptionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GeneralOptionsPanel.Location = new System.Drawing.Point(3, 3);
			this.GeneralOptionsPanel.Name = "GeneralOptionsPanel";
			this.GeneralOptionsPanel.Size = new System.Drawing.Size(900, 334);
			this.GeneralOptionsPanel.TabIndex = 0;
			// 
			// OptionsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.OptionsTabControl);
			this.Name = "OptionsControl";
			this.Size = new System.Drawing.Size(914, 367);
			this.OptionsTabControl.ResumeLayout(false);
			this.GeneralTabPage.ResumeLayout(false);
			this.CacheTabPage.ResumeLayout(false);
			this.GoogleTTSTabPage.ResumeLayout(false);
			this.AmazonPollyTabPage.ResumeLayout(false);
			this.MicrosoftCortanaTabPage.ResumeLayout(false);
			this.MonitorDispalyTabPage.ResumeLayout(false);
			this.MonitorNetworkTabPage.ResumeLayout(false);
			this.MonitorServerTabPage.ResumeLayout(false);
			this.MonitorClipBoardTabPage.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TabPage GeneralTabPage;
		private System.Windows.Forms.TabPage CacheTabPage;
		private System.Windows.Forms.TabPage GoogleTTSTabPage;
		private System.Windows.Forms.TabPage MicrosoftCortanaTabPage;
		private System.Windows.Forms.TabPage MonitorNetworkTabPage;
		private MicrosoftCortanaOptionsUserControl MicrosoftCortanaPanel;
		private Google.GoogleTTSUserControl GoogleCloudPanel;
		private CacheOptionsUserControl CachePanel;
		private MonitorNetworkUserControl CapturingPanel;
		private System.Windows.Forms.TabPage MonitorServerTabPage;
		private MonitorServerUserControl monitorUdpPortUserControl1;
		private System.Windows.Forms.TabPage MonitorDispalyTabPage;
		private MonitorDisplayUserControl monitorDisplayUserControl1;
		private System.Windows.Forms.TabPage MonitorClipBoardTabPage;
		private MonitorClipboardUserControl monitorClipboardUserControl1;
		private System.Windows.Forms.ImageList TabsImageList;
		private Options.AmazonPollyUserControl AmazonPollyPanel;
		public System.Windows.Forms.TabControl OptionsTabControl;
		public System.Windows.Forms.TabPage AmazonPollyTabPage;
		private Options.GeneralOptionsUserControl GeneralOptionsPanel;
	}
}
