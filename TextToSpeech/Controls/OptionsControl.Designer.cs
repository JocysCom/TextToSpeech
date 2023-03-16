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
            this.GeneralOptionsPanel = new JocysCom.TextToSpeech.Monitor.Controls.Options.GeneralOptionsUserControl();
            this.CacheTabPage = new System.Windows.Forms.TabPage();
            this.CachePanel = new JocysCom.TextToSpeech.Monitor.Controls.CacheOptionsUserControl();
            this.WindowsVoicesTabPage = new System.Windows.Forms.TabPage();
            this.MicrosoftCortanaPanel = new JocysCom.TextToSpeech.Monitor.Controls.MicrosoftCortanaOptionsUserControl();
            this.GoogleVoicesTabPage = new System.Windows.Forms.TabPage();
            this.GoogleCloudPanel = new JocysCom.TextToSpeech.Monitor.Google.GoogleTTSUserControl();
            this.AmazonVoicesTabPage = new System.Windows.Forms.TabPage();
            this.AmazonVoicesPanel = new JocysCom.TextToSpeech.Monitor.Controls.Options.AmazonVoicesUserControl();
            this.ElevanLabsVoicesTabPage = new System.Windows.Forms.TabPage();
            this.ElevenLabsVoicesPanel = new JocysCom.TextToSpeech.Monitor.Controls.Options.ElevenLabsVoicesUserControl();
            this.MonitorDispalyTabPage = new System.Windows.Forms.TabPage();
            this.monitorDisplayUserControl1 = new JocysCom.TextToSpeech.Monitor.Controls.MonitorDisplayUserControl();
            this.MonitorNetworkTabPage = new System.Windows.Forms.TabPage();
            this.CapturingPanel = new JocysCom.TextToSpeech.Monitor.Controls.MonitorNetworkUserControl();
            this.MonitorServerTabPage = new System.Windows.Forms.TabPage();
            this.monitorUdpPortUserControl1 = new JocysCom.TextToSpeech.Monitor.Controls.MonitorServerUserControl();
            this.MonitorClipBoardTabPage = new System.Windows.Forms.TabPage();
            this.monitorClipboardUserControl1 = new JocysCom.TextToSpeech.Monitor.Controls.MonitorClipboardUserControl();
            this.TabsImageList = new System.Windows.Forms.ImageList(this.components);
            this.OptionsTabControl.SuspendLayout();
            this.GeneralTabPage.SuspendLayout();
            this.CacheTabPage.SuspendLayout();
            this.WindowsVoicesTabPage.SuspendLayout();
            this.GoogleVoicesTabPage.SuspendLayout();
            this.AmazonVoicesTabPage.SuspendLayout();
            this.ElevanLabsVoicesTabPage.SuspendLayout();
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
            this.OptionsTabControl.Controls.Add(this.WindowsVoicesTabPage);
            this.OptionsTabControl.Controls.Add(this.GoogleVoicesTabPage);
            this.OptionsTabControl.Controls.Add(this.AmazonVoicesTabPage);
            this.OptionsTabControl.Controls.Add(this.ElevanLabsVoicesTabPage);
            this.OptionsTabControl.Controls.Add(this.MonitorDispalyTabPage);
            this.OptionsTabControl.Controls.Add(this.MonitorNetworkTabPage);
            this.OptionsTabControl.Controls.Add(this.MonitorServerTabPage);
            this.OptionsTabControl.Controls.Add(this.MonitorClipBoardTabPage);
            this.OptionsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OptionsTabControl.ImageList = this.TabsImageList;
            this.OptionsTabControl.Location = new System.Drawing.Point(0, 0);
            this.OptionsTabControl.Margin = new System.Windows.Forms.Padding(6);
            this.OptionsTabControl.Name = "OptionsTabControl";
            this.OptionsTabControl.SelectedIndex = 0;
            this.OptionsTabControl.Size = new System.Drawing.Size(1828, 706);
            this.OptionsTabControl.TabIndex = 11;
            // 
            // GeneralTabPage
            // 
            this.GeneralTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.GeneralTabPage.Controls.Add(this.GeneralOptionsPanel);
            this.GeneralTabPage.ImageKey = "Options_General";
            this.GeneralTabPage.Location = new System.Drawing.Point(8, 39);
            this.GeneralTabPage.Margin = new System.Windows.Forms.Padding(6);
            this.GeneralTabPage.Name = "GeneralTabPage";
            this.GeneralTabPage.Padding = new System.Windows.Forms.Padding(6);
            this.GeneralTabPage.Size = new System.Drawing.Size(1812, 659);
            this.GeneralTabPage.TabIndex = 0;
            this.GeneralTabPage.Text = "General";
            // 
            // GeneralOptionsPanel
            // 
            this.GeneralOptionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GeneralOptionsPanel.Location = new System.Drawing.Point(6, 6);
            this.GeneralOptionsPanel.Margin = new System.Windows.Forms.Padding(12);
            this.GeneralOptionsPanel.Name = "GeneralOptionsPanel";
            this.GeneralOptionsPanel.Size = new System.Drawing.Size(1800, 647);
            this.GeneralOptionsPanel.TabIndex = 0;
            // 
            // CacheTabPage
            // 
            this.CacheTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.CacheTabPage.Controls.Add(this.CachePanel);
            this.CacheTabPage.ImageKey = "Options_Cache";
            this.CacheTabPage.Location = new System.Drawing.Point(8, 39);
            this.CacheTabPage.Margin = new System.Windows.Forms.Padding(6);
            this.CacheTabPage.Name = "CacheTabPage";
            this.CacheTabPage.Padding = new System.Windows.Forms.Padding(6);
            this.CacheTabPage.Size = new System.Drawing.Size(1812, 659);
            this.CacheTabPage.TabIndex = 1;
            this.CacheTabPage.Text = "Cache";
            // 
            // CachePanel
            // 
            this.CachePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CachePanel.Location = new System.Drawing.Point(6, 6);
            this.CachePanel.Margin = new System.Windows.Forms.Padding(12);
            this.CachePanel.Name = "CachePanel";
            this.CachePanel.Size = new System.Drawing.Size(1800, 647);
            this.CachePanel.TabIndex = 0;
            // 
            // WindowsVoicesTabPage
            // 
            this.WindowsVoicesTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.WindowsVoicesTabPage.Controls.Add(this.MicrosoftCortanaPanel);
            this.WindowsVoicesTabPage.ImageKey = "Options_Microsoft_Cortana";
            this.WindowsVoicesTabPage.Location = new System.Drawing.Point(8, 39);
            this.WindowsVoicesTabPage.Margin = new System.Windows.Forms.Padding(6);
            this.WindowsVoicesTabPage.Name = "WindowsVoicesTabPage";
            this.WindowsVoicesTabPage.Size = new System.Drawing.Size(1812, 659);
            this.WindowsVoicesTabPage.TabIndex = 3;
            this.WindowsVoicesTabPage.Text = "Windows Voices";
            // 
            // MicrosoftCortanaPanel
            // 
            this.MicrosoftCortanaPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MicrosoftCortanaPanel.Location = new System.Drawing.Point(0, 0);
            this.MicrosoftCortanaPanel.Margin = new System.Windows.Forms.Padding(12);
            this.MicrosoftCortanaPanel.Name = "MicrosoftCortanaPanel";
            this.MicrosoftCortanaPanel.Size = new System.Drawing.Size(1812, 659);
            this.MicrosoftCortanaPanel.TabIndex = 0;
            // 
            // GoogleVoicesTabPage
            // 
            this.GoogleVoicesTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.GoogleVoicesTabPage.Controls.Add(this.GoogleCloudPanel);
            this.GoogleVoicesTabPage.ImageKey = "Options_Google_TTS";
            this.GoogleVoicesTabPage.Location = new System.Drawing.Point(8, 39);
            this.GoogleVoicesTabPage.Margin = new System.Windows.Forms.Padding(6);
            this.GoogleVoicesTabPage.Name = "GoogleVoicesTabPage";
            this.GoogleVoicesTabPage.Size = new System.Drawing.Size(1812, 659);
            this.GoogleVoicesTabPage.TabIndex = 2;
            this.GoogleVoicesTabPage.Text = "Google Voices";
            // 
            // GoogleCloudPanel
            // 
            this.GoogleCloudPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GoogleCloudPanel.Location = new System.Drawing.Point(0, 0);
            this.GoogleCloudPanel.Margin = new System.Windows.Forms.Padding(12);
            this.GoogleCloudPanel.Name = "GoogleCloudPanel";
            this.GoogleCloudPanel.Size = new System.Drawing.Size(1812, 659);
            this.GoogleCloudPanel.TabIndex = 0;
            // 
            // AmazonVoicesTabPage
            // 
            this.AmazonVoicesTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.AmazonVoicesTabPage.Controls.Add(this.AmazonVoicesPanel);
            this.AmazonVoicesTabPage.ImageKey = "Options_Amazon_Polly";
            this.AmazonVoicesTabPage.Location = new System.Drawing.Point(8, 39);
            this.AmazonVoicesTabPage.Margin = new System.Windows.Forms.Padding(6);
            this.AmazonVoicesTabPage.Name = "AmazonVoicesTabPage";
            this.AmazonVoicesTabPage.Size = new System.Drawing.Size(1812, 659);
            this.AmazonVoicesTabPage.TabIndex = 8;
            this.AmazonVoicesTabPage.Text = "Amazon Voices";
            // 
            // AmazonVoicesPanel
            // 
            this.AmazonVoicesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AmazonVoicesPanel.Location = new System.Drawing.Point(0, 0);
            this.AmazonVoicesPanel.Margin = new System.Windows.Forms.Padding(12);
            this.AmazonVoicesPanel.Name = "AmazonVoicesPanel";
            this.AmazonVoicesPanel.Size = new System.Drawing.Size(1812, 659);
            this.AmazonVoicesPanel.TabIndex = 0;
            // 
            // ElevanLabsVoicesTabPage
            // 
            this.ElevanLabsVoicesTabPage.Controls.Add(this.ElevenLabsVoicesPanel);
            this.ElevanLabsVoicesTabPage.ImageKey = "Voice.ico";
            this.ElevanLabsVoicesTabPage.Location = new System.Drawing.Point(8, 39);
            this.ElevanLabsVoicesTabPage.Name = "ElevanLabsVoicesTabPage";
            this.ElevanLabsVoicesTabPage.Size = new System.Drawing.Size(1812, 659);
            this.ElevanLabsVoicesTabPage.TabIndex = 9;
            this.ElevanLabsVoicesTabPage.Text = "ElevanLabs Voices";
            this.ElevanLabsVoicesTabPage.UseVisualStyleBackColor = true;
            // 
            // ElevenLabsVoicesPanel
            // 
            this.ElevenLabsVoicesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ElevenLabsVoicesPanel.Location = new System.Drawing.Point(0, 0);
            this.ElevenLabsVoicesPanel.Margin = new System.Windows.Forms.Padding(6);
            this.ElevenLabsVoicesPanel.Name = "ElevenLabsVoicesPanel";
            this.ElevenLabsVoicesPanel.Size = new System.Drawing.Size(1812, 659);
            this.ElevenLabsVoicesPanel.TabIndex = 0;
            // 
            // MonitorDispalyTabPage
            // 
            this.MonitorDispalyTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.MonitorDispalyTabPage.Controls.Add(this.monitorDisplayUserControl1);
            this.MonitorDispalyTabPage.ImageKey = "Options_Monitor_Display";
            this.MonitorDispalyTabPage.Location = new System.Drawing.Point(8, 39);
            this.MonitorDispalyTabPage.Margin = new System.Windows.Forms.Padding(6);
            this.MonitorDispalyTabPage.Name = "MonitorDispalyTabPage";
            this.MonitorDispalyTabPage.Size = new System.Drawing.Size(1812, 659);
            this.MonitorDispalyTabPage.TabIndex = 6;
            this.MonitorDispalyTabPage.Text = "Monitor: Display";
            // 
            // monitorDisplayUserControl1
            // 
            this.monitorDisplayUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monitorDisplayUserControl1.Location = new System.Drawing.Point(0, 0);
            this.monitorDisplayUserControl1.Margin = new System.Windows.Forms.Padding(12);
            this.monitorDisplayUserControl1.Name = "monitorDisplayUserControl1";
            this.monitorDisplayUserControl1.Size = new System.Drawing.Size(1812, 659);
            this.monitorDisplayUserControl1.TabIndex = 0;
            // 
            // MonitorNetworkTabPage
            // 
            this.MonitorNetworkTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.MonitorNetworkTabPage.Controls.Add(this.CapturingPanel);
            this.MonitorNetworkTabPage.ImageKey = "Options_Monitor_Network";
            this.MonitorNetworkTabPage.Location = new System.Drawing.Point(8, 39);
            this.MonitorNetworkTabPage.Margin = new System.Windows.Forms.Padding(6);
            this.MonitorNetworkTabPage.Name = "MonitorNetworkTabPage";
            this.MonitorNetworkTabPage.Size = new System.Drawing.Size(1812, 659);
            this.MonitorNetworkTabPage.TabIndex = 4;
            this.MonitorNetworkTabPage.Text = "Monitor: Network";
            // 
            // CapturingPanel
            // 
            this.CapturingPanel._CapturingType = JocysCom.TextToSpeech.Monitor.Capturing.CapturingType.SocPcap;
            this.CapturingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CapturingPanel.Location = new System.Drawing.Point(0, 0);
            this.CapturingPanel.Margin = new System.Windows.Forms.Padding(12);
            this.CapturingPanel.Name = "CapturingPanel";
            this.CapturingPanel.Size = new System.Drawing.Size(1812, 659);
            this.CapturingPanel.TabIndex = 0;
            // 
            // MonitorServerTabPage
            // 
            this.MonitorServerTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.MonitorServerTabPage.Controls.Add(this.monitorUdpPortUserControl1);
            this.MonitorServerTabPage.ImageKey = "Options_Monitor_Server";
            this.MonitorServerTabPage.Location = new System.Drawing.Point(8, 39);
            this.MonitorServerTabPage.Margin = new System.Windows.Forms.Padding(6);
            this.MonitorServerTabPage.Name = "MonitorServerTabPage";
            this.MonitorServerTabPage.Size = new System.Drawing.Size(1812, 659);
            this.MonitorServerTabPage.TabIndex = 5;
            this.MonitorServerTabPage.Text = "Monitor: Server";
            // 
            // monitorUdpPortUserControl1
            // 
            this.monitorUdpPortUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monitorUdpPortUserControl1.Location = new System.Drawing.Point(0, 0);
            this.monitorUdpPortUserControl1.Margin = new System.Windows.Forms.Padding(12);
            this.monitorUdpPortUserControl1.Name = "monitorUdpPortUserControl1";
            this.monitorUdpPortUserControl1.Size = new System.Drawing.Size(1812, 659);
            this.monitorUdpPortUserControl1.TabIndex = 0;
            // 
            // MonitorClipBoardTabPage
            // 
            this.MonitorClipBoardTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.MonitorClipBoardTabPage.Controls.Add(this.monitorClipboardUserControl1);
            this.MonitorClipBoardTabPage.ImageKey = "Options_Monitor_Clipboard";
            this.MonitorClipBoardTabPage.Location = new System.Drawing.Point(8, 39);
            this.MonitorClipBoardTabPage.Margin = new System.Windows.Forms.Padding(6);
            this.MonitorClipBoardTabPage.Name = "MonitorClipBoardTabPage";
            this.MonitorClipBoardTabPage.Size = new System.Drawing.Size(1812, 659);
            this.MonitorClipBoardTabPage.TabIndex = 7;
            this.MonitorClipBoardTabPage.Text = "Monitor: Clipboard";
            // 
            // monitorClipboardUserControl1
            // 
            this.monitorClipboardUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monitorClipboardUserControl1.Location = new System.Drawing.Point(0, 0);
            this.monitorClipboardUserControl1.Margin = new System.Windows.Forms.Padding(12);
            this.monitorClipboardUserControl1.Name = "monitorClipboardUserControl1";
            this.monitorClipboardUserControl1.Size = new System.Drawing.Size(1812, 659);
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
            this.TabsImageList.Images.SetKeyName(9, "Voice.ico");
            // 
            // OptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.OptionsTabControl);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "OptionsControl";
            this.Size = new System.Drawing.Size(1828, 706);
            this.OptionsTabControl.ResumeLayout(false);
            this.GeneralTabPage.ResumeLayout(false);
            this.CacheTabPage.ResumeLayout(false);
            this.WindowsVoicesTabPage.ResumeLayout(false);
            this.GoogleVoicesTabPage.ResumeLayout(false);
            this.AmazonVoicesTabPage.ResumeLayout(false);
            this.ElevanLabsVoicesTabPage.ResumeLayout(false);
            this.MonitorDispalyTabPage.ResumeLayout(false);
            this.MonitorNetworkTabPage.ResumeLayout(false);
            this.MonitorServerTabPage.ResumeLayout(false);
            this.MonitorClipBoardTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TabPage GeneralTabPage;
		private System.Windows.Forms.TabPage CacheTabPage;
		private System.Windows.Forms.TabPage GoogleVoicesTabPage;
		private System.Windows.Forms.TabPage WindowsVoicesTabPage;
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
		private Options.AmazonVoicesUserControl AmazonVoicesPanel;
		public System.Windows.Forms.TabControl OptionsTabControl;
		public System.Windows.Forms.TabPage AmazonVoicesTabPage;
		private Options.GeneralOptionsUserControl GeneralOptionsPanel;
        private System.Windows.Forms.TabPage ElevanLabsVoicesTabPage;
        private Options.ElevenLabsVoicesUserControl ElevenLabsVoicesPanel;
    }
}
