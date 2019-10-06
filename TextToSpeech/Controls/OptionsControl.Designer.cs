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
			this.AddSilenceGroupBox = new System.Windows.Forms.GroupBox();
			this.PlaybackDeviceComboBox = new System.Windows.Forms.ComboBox();
			this.RefreshPlaybackDevices = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.AddSilenceAfterLabel = new System.Windows.Forms.Label();
			this.AddSilenceAfterNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.SilenceAfterTagLabel = new System.Windows.Forms.Label();
			this.AddSilcenceBeforeNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.AddSilenceBeforeLabel = new System.Windows.Forms.Label();
			this.SilenceBeforeTagLabel = new System.Windows.Forms.Label();
			this.OptionsTabControl = new System.Windows.Forms.TabControl();
			this.GeneralTabPage = new System.Windows.Forms.TabPage();
			this.CacheTabPage = new System.Windows.Forms.TabPage();
			this.CachePanel = new JocysCom.TextToSpeech.Monitor.Controls.OptionsCacheUserControl();
			this.GoogleCloudTabPage = new System.Windows.Forms.TabPage();
			this.GoogleCloudPanel = new JocysCom.TextToSpeech.Monitor.Google.OptionsGoogleCloudUserControl();
			this.MicrosoftCortanaTabPage = new System.Windows.Forms.TabPage();
			this.MicrosoftCortanaPanel = new JocysCom.TextToSpeech.Monitor.Controls.OptionsMicrosoftCortanaUserControl();
			this.MonitorDispalyTabPage = new System.Windows.Forms.TabPage();
			this.monitorDisplayUserControl1 = new JocysCom.TextToSpeech.Monitor.Controls.MonitorDisplayUserControl();
			this.MonitorNetworkTabPage = new System.Windows.Forms.TabPage();
			this.CapturingPanel = new JocysCom.TextToSpeech.Monitor.Controls.MonitorNetworkUserControl();
			this.MonitorServerTabPage = new System.Windows.Forms.TabPage();
			this.monitorUdpPortUserControl1 = new JocysCom.TextToSpeech.Monitor.Controls.MonitorServerUserControl();
			this.MonitorClipBoardTabPage = new System.Windows.Forms.TabPage();
			this.monitorClipboardUserControl1 = new JocysCom.TextToSpeech.Monitor.Controls.MonitorClipboardUserControl();
			this.AddSilenceGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.AddSilenceAfterNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.AddSilcenceBeforeNumericUpDown)).BeginInit();
			this.OptionsTabControl.SuspendLayout();
			this.GeneralTabPage.SuspendLayout();
			this.CacheTabPage.SuspendLayout();
			this.GoogleCloudTabPage.SuspendLayout();
			this.MicrosoftCortanaTabPage.SuspendLayout();
			this.MonitorDispalyTabPage.SuspendLayout();
			this.MonitorNetworkTabPage.SuspendLayout();
			this.MonitorServerTabPage.SuspendLayout();
			this.MonitorClipBoardTabPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// AddSilenceGroupBox
			// 
			this.AddSilenceGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.AddSilenceGroupBox.Controls.Add(this.PlaybackDeviceComboBox);
			this.AddSilenceGroupBox.Controls.Add(this.RefreshPlaybackDevices);
			this.AddSilenceGroupBox.Controls.Add(this.label1);
			this.AddSilenceGroupBox.Controls.Add(this.label2);
			this.AddSilenceGroupBox.Controls.Add(this.AddSilenceAfterLabel);
			this.AddSilenceGroupBox.Controls.Add(this.AddSilenceAfterNumericUpDown);
			this.AddSilenceGroupBox.Controls.Add(this.SilenceAfterTagLabel);
			this.AddSilenceGroupBox.Controls.Add(this.AddSilcenceBeforeNumericUpDown);
			this.AddSilenceGroupBox.Controls.Add(this.AddSilenceBeforeLabel);
			this.AddSilenceGroupBox.Controls.Add(this.SilenceBeforeTagLabel);
			this.AddSilenceGroupBox.Location = new System.Drawing.Point(6, 6);
			this.AddSilenceGroupBox.Name = "AddSilenceGroupBox";
			this.AddSilenceGroupBox.Size = new System.Drawing.Size(734, 103);
			this.AddSilenceGroupBox.TabIndex = 0;
			this.AddSilenceGroupBox.TabStop = false;
			this.AddSilenceGroupBox.Text = "Audio Options";
			// 
			// PlaybackDeviceComboBox
			// 
			this.PlaybackDeviceComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PlaybackDeviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PlaybackDeviceComboBox.FormattingEnabled = true;
			this.PlaybackDeviceComboBox.Location = new System.Drawing.Point(91, 71);
			this.PlaybackDeviceComboBox.Name = "PlaybackDeviceComboBox";
			this.PlaybackDeviceComboBox.Size = new System.Drawing.Size(461, 21);
			this.PlaybackDeviceComboBox.TabIndex = 33;
			this.PlaybackDeviceComboBox.SelectedIndexChanged += new System.EventHandler(this.PlaybackDeviceComboBox_SelectedIndexChanged);
			// 
			// RefreshPlaybackDevices
			// 
			this.RefreshPlaybackDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RefreshPlaybackDevices.Location = new System.Drawing.Point(558, 70);
			this.RefreshPlaybackDevices.Name = "RefreshPlaybackDevices";
			this.RefreshPlaybackDevices.Size = new System.Drawing.Size(75, 23);
			this.RefreshPlaybackDevices.TabIndex = 5;
			this.RefreshPlaybackDevices.Text = "Refresh";
			this.RefreshPlaybackDevices.UseVisualStyleBackColor = true;
			this.RefreshPlaybackDevices.Click += new System.EventHandler(this.RefreshPlaybackDevices_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 74);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "Output Device:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 47);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(135, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Add Silence After Message";
			// 
			// AddSilenceAfterLabel
			// 
			this.AddSilenceAfterLabel.AutoSize = true;
			this.AddSilenceAfterLabel.Location = new System.Drawing.Point(146, 47);
			this.AddSilenceAfterLabel.Name = "AddSilenceAfterLabel";
			this.AddSilenceAfterLabel.Size = new System.Drawing.Size(130, 13);
			this.AddSilenceAfterLabel.TabIndex = 6;
			this.AddSilenceAfterLabel.Text = "( default value is 0 ) [ ms ]:";
			// 
			// AddSilenceAfterNumericUpDown
			// 
			this.AddSilenceAfterNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.AddSilenceAfterNumericUpDown.Location = new System.Drawing.Point(279, 45);
			this.AddSilenceAfterNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.AddSilenceAfterNumericUpDown.Name = "AddSilenceAfterNumericUpDown";
			this.AddSilenceAfterNumericUpDown.Size = new System.Drawing.Size(114, 20);
			this.AddSilenceAfterNumericUpDown.TabIndex = 5;
			this.AddSilenceAfterNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.AddSilenceAfterNumericUpDown.ValueChanged += new System.EventHandler(this.AddSilenceAfterNumericUpDown_ValueChanged);
			// 
			// SilenceAfterTagLabel
			// 
			this.SilenceAfterTagLabel.AutoSize = true;
			this.SilenceAfterTagLabel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SilenceAfterTagLabel.ForeColor = System.Drawing.SystemColors.ActiveBorder;
			this.SilenceAfterTagLabel.Location = new System.Drawing.Point(399, 48);
			this.SilenceAfterTagLabel.Name = "SilenceAfterTagLabel";
			this.SilenceAfterTagLabel.Size = new System.Drawing.Size(161, 14);
			this.SilenceAfterTagLabel.TabIndex = 8;
			this.SilenceAfterTagLabel.Text = "<silence msec=\"3000\"/>";
			// 
			// AddSilcenceBeforeNumericUpDown
			// 
			this.AddSilcenceBeforeNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.AddSilcenceBeforeNumericUpDown.Location = new System.Drawing.Point(279, 19);
			this.AddSilcenceBeforeNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.AddSilcenceBeforeNumericUpDown.Name = "AddSilcenceBeforeNumericUpDown";
			this.AddSilcenceBeforeNumericUpDown.Size = new System.Drawing.Size(114, 20);
			this.AddSilcenceBeforeNumericUpDown.TabIndex = 5;
			this.AddSilcenceBeforeNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.AddSilcenceBeforeNumericUpDown.ValueChanged += new System.EventHandler(this.AddSilcenceBeforeNumericUpDown_ValueChanged);
			// 
			// AddSilenceBeforeLabel
			// 
			this.AddSilenceBeforeLabel.AutoSize = true;
			this.AddSilenceBeforeLabel.Location = new System.Drawing.Point(6, 21);
			this.AddSilenceBeforeLabel.Name = "AddSilenceBeforeLabel";
			this.AddSilenceBeforeLabel.Size = new System.Drawing.Size(270, 13);
			this.AddSilenceBeforeLabel.TabIndex = 6;
			this.AddSilenceBeforeLabel.Text = "Add Silence Before Message ( default value is 0 ) [ ms ]:";
			// 
			// SilenceBeforeTagLabel
			// 
			this.SilenceBeforeTagLabel.AutoSize = true;
			this.SilenceBeforeTagLabel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SilenceBeforeTagLabel.ForeColor = System.Drawing.SystemColors.ActiveBorder;
			this.SilenceBeforeTagLabel.Location = new System.Drawing.Point(399, 22);
			this.SilenceBeforeTagLabel.Name = "SilenceBeforeTagLabel";
			this.SilenceBeforeTagLabel.Size = new System.Drawing.Size(161, 14);
			this.SilenceBeforeTagLabel.TabIndex = 7;
			this.SilenceBeforeTagLabel.Text = "<silence msec=\"3000\"/>";
			// 
			// OptionsTabControl
			// 
			this.OptionsTabControl.Controls.Add(this.GeneralTabPage);
			this.OptionsTabControl.Controls.Add(this.CacheTabPage);
			this.OptionsTabControl.Controls.Add(this.GoogleCloudTabPage);
			this.OptionsTabControl.Controls.Add(this.MicrosoftCortanaTabPage);
			this.OptionsTabControl.Controls.Add(this.MonitorDispalyTabPage);
			this.OptionsTabControl.Controls.Add(this.MonitorNetworkTabPage);
			this.OptionsTabControl.Controls.Add(this.MonitorServerTabPage);
			this.OptionsTabControl.Controls.Add(this.MonitorClipBoardTabPage);
			this.OptionsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OptionsTabControl.Location = new System.Drawing.Point(0, 0);
			this.OptionsTabControl.Name = "OptionsTabControl";
			this.OptionsTabControl.SelectedIndex = 0;
			this.OptionsTabControl.Size = new System.Drawing.Size(754, 367);
			this.OptionsTabControl.TabIndex = 11;
			// 
			// GeneralTabPage
			// 
			this.GeneralTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.GeneralTabPage.Controls.Add(this.AddSilenceGroupBox);
			this.GeneralTabPage.Location = new System.Drawing.Point(4, 22);
			this.GeneralTabPage.Name = "GeneralTabPage";
			this.GeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.GeneralTabPage.Size = new System.Drawing.Size(746, 341);
			this.GeneralTabPage.TabIndex = 0;
			this.GeneralTabPage.Text = "General";
			// 
			// CacheTabPage
			// 
			this.CacheTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.CacheTabPage.Controls.Add(this.CachePanel);
			this.CacheTabPage.Location = new System.Drawing.Point(4, 22);
			this.CacheTabPage.Name = "CacheTabPage";
			this.CacheTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.CacheTabPage.Size = new System.Drawing.Size(746, 341);
			this.CacheTabPage.TabIndex = 1;
			this.CacheTabPage.Text = "Cache";
			// 
			// CachePanel
			// 
			this.CachePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CachePanel.Location = new System.Drawing.Point(3, 3);
			this.CachePanel.Name = "CachePanel";
			this.CachePanel.Size = new System.Drawing.Size(740, 335);
			this.CachePanel.TabIndex = 0;
			// 
			// GoogleCloudTabPage
			// 
			this.GoogleCloudTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.GoogleCloudTabPage.Controls.Add(this.GoogleCloudPanel);
			this.GoogleCloudTabPage.Location = new System.Drawing.Point(4, 22);
			this.GoogleCloudTabPage.Name = "GoogleCloudTabPage";
			this.GoogleCloudTabPage.Size = new System.Drawing.Size(746, 341);
			this.GoogleCloudTabPage.TabIndex = 2;
			this.GoogleCloudTabPage.Text = "Google Cloud";
			// 
			// GoogleCloudPanel
			// 
			this.GoogleCloudPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GoogleCloudPanel.Location = new System.Drawing.Point(0, 0);
			this.GoogleCloudPanel.Name = "GoogleCloudPanel";
			this.GoogleCloudPanel.Size = new System.Drawing.Size(746, 341);
			this.GoogleCloudPanel.TabIndex = 0;
			// 
			// MicrosoftCortanaTabPage
			// 
			this.MicrosoftCortanaTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.MicrosoftCortanaTabPage.Controls.Add(this.MicrosoftCortanaPanel);
			this.MicrosoftCortanaTabPage.Location = new System.Drawing.Point(4, 22);
			this.MicrosoftCortanaTabPage.Name = "MicrosoftCortanaTabPage";
			this.MicrosoftCortanaTabPage.Size = new System.Drawing.Size(746, 341);
			this.MicrosoftCortanaTabPage.TabIndex = 3;
			this.MicrosoftCortanaTabPage.Text = "Microsoft Cortana";
			// 
			// MicrosoftCortanaPanel
			// 
			this.MicrosoftCortanaPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MicrosoftCortanaPanel.Location = new System.Drawing.Point(0, 0);
			this.MicrosoftCortanaPanel.Name = "MicrosoftCortanaPanel";
			this.MicrosoftCortanaPanel.Size = new System.Drawing.Size(746, 341);
			this.MicrosoftCortanaPanel.TabIndex = 0;
			// 
			// MonitorDispalyTabPage
			// 
			this.MonitorDispalyTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.MonitorDispalyTabPage.Controls.Add(this.monitorDisplayUserControl1);
			this.MonitorDispalyTabPage.Location = new System.Drawing.Point(4, 22);
			this.MonitorDispalyTabPage.Name = "MonitorDispalyTabPage";
			this.MonitorDispalyTabPage.Size = new System.Drawing.Size(746, 341);
			this.MonitorDispalyTabPage.TabIndex = 6;
			this.MonitorDispalyTabPage.Text = "Monitor: Display";
			// 
			// monitorDisplayUserControl1
			// 
			this.monitorDisplayUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.monitorDisplayUserControl1.Location = new System.Drawing.Point(0, 0);
			this.monitorDisplayUserControl1.Name = "monitorDisplayUserControl1";
			this.monitorDisplayUserControl1.Size = new System.Drawing.Size(746, 341);
			this.monitorDisplayUserControl1.TabIndex = 0;
			// 
			// MonitorNetworkTabPage
			// 
			this.MonitorNetworkTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.MonitorNetworkTabPage.Controls.Add(this.CapturingPanel);
			this.MonitorNetworkTabPage.Location = new System.Drawing.Point(4, 22);
			this.MonitorNetworkTabPage.Name = "MonitorNetworkTabPage";
			this.MonitorNetworkTabPage.Size = new System.Drawing.Size(746, 341);
			this.MonitorNetworkTabPage.TabIndex = 4;
			this.MonitorNetworkTabPage.Text = "Monitor: Network";
			// 
			// CapturingPanel
			// 
			this.CapturingPanel._CapturingType = JocysCom.TextToSpeech.Monitor.Capturing.CapturingType.SocPcap;
			this.CapturingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CapturingPanel.Location = new System.Drawing.Point(0, 0);
			this.CapturingPanel.Name = "CapturingPanel";
			this.CapturingPanel.Size = new System.Drawing.Size(746, 341);
			this.CapturingPanel.TabIndex = 0;
			// 
			// MonitorServerTabPage
			// 
			this.MonitorServerTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.MonitorServerTabPage.Controls.Add(this.monitorUdpPortUserControl1);
			this.MonitorServerTabPage.Location = new System.Drawing.Point(4, 22);
			this.MonitorServerTabPage.Name = "MonitorServerTabPage";
			this.MonitorServerTabPage.Size = new System.Drawing.Size(746, 341);
			this.MonitorServerTabPage.TabIndex = 5;
			this.MonitorServerTabPage.Text = "Monitor: Server";
			// 
			// monitorUdpPortUserControl1
			// 
			this.monitorUdpPortUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.monitorUdpPortUserControl1.Location = new System.Drawing.Point(0, 0);
			this.monitorUdpPortUserControl1.Name = "monitorUdpPortUserControl1";
			this.monitorUdpPortUserControl1.Size = new System.Drawing.Size(746, 341);
			this.monitorUdpPortUserControl1.TabIndex = 0;
			// 
			// MonitorClipBoardTabPage
			// 
			this.MonitorClipBoardTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.MonitorClipBoardTabPage.Controls.Add(this.monitorClipboardUserControl1);
			this.MonitorClipBoardTabPage.Location = new System.Drawing.Point(4, 22);
			this.MonitorClipBoardTabPage.Name = "MonitorClipBoardTabPage";
			this.MonitorClipBoardTabPage.Size = new System.Drawing.Size(746, 341);
			this.MonitorClipBoardTabPage.TabIndex = 7;
			this.MonitorClipBoardTabPage.Text = "Monitor: Clipboard";
			// 
			// monitorClipboardUserControl1
			// 
			this.monitorClipboardUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.monitorClipboardUserControl1.Location = new System.Drawing.Point(0, 0);
			this.monitorClipboardUserControl1.Name = "monitorClipboardUserControl1";
			this.monitorClipboardUserControl1.Size = new System.Drawing.Size(746, 341);
			this.monitorClipboardUserControl1.TabIndex = 0;
			// 
			// OptionsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.OptionsTabControl);
			this.Name = "OptionsControl";
			this.Size = new System.Drawing.Size(754, 367);
			this.AddSilenceGroupBox.ResumeLayout(false);
			this.AddSilenceGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.AddSilenceAfterNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.AddSilcenceBeforeNumericUpDown)).EndInit();
			this.OptionsTabControl.ResumeLayout(false);
			this.GeneralTabPage.ResumeLayout(false);
			this.CacheTabPage.ResumeLayout(false);
			this.GoogleCloudTabPage.ResumeLayout(false);
			this.MicrosoftCortanaTabPage.ResumeLayout(false);
			this.MonitorDispalyTabPage.ResumeLayout(false);
			this.MonitorNetworkTabPage.ResumeLayout(false);
			this.MonitorServerTabPage.ResumeLayout(false);
			this.MonitorClipBoardTabPage.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox AddSilenceGroupBox;
		private System.Windows.Forms.NumericUpDown AddSilcenceBeforeNumericUpDown;
		private System.Windows.Forms.NumericUpDown AddSilenceAfterNumericUpDown;
		private System.Windows.Forms.Label AddSilenceAfterLabel;
		private System.Windows.Forms.Label AddSilenceBeforeLabel;
        private System.Windows.Forms.Label SilenceBeforeTagLabel;
        private System.Windows.Forms.Label SilenceAfterTagLabel;
		private System.Windows.Forms.ComboBox PlaybackDeviceComboBox;
		private System.Windows.Forms.Button RefreshPlaybackDevices;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TabControl OptionsTabControl;
		private System.Windows.Forms.TabPage GeneralTabPage;
		private System.Windows.Forms.TabPage CacheTabPage;
		private System.Windows.Forms.TabPage GoogleCloudTabPage;
		private System.Windows.Forms.TabPage MicrosoftCortanaTabPage;
		private System.Windows.Forms.TabPage MonitorNetworkTabPage;
		private OptionsMicrosoftCortanaUserControl MicrosoftCortanaPanel;
		private Google.OptionsGoogleCloudUserControl GoogleCloudPanel;
		private OptionsCacheUserControl CachePanel;
		private MonitorNetworkUserControl CapturingPanel;
		private System.Windows.Forms.TabPage MonitorServerTabPage;
		private MonitorServerUserControl monitorUdpPortUserControl1;
		private System.Windows.Forms.TabPage MonitorDispalyTabPage;
		private MonitorDisplayUserControl monitorDisplayUserControl1;
		private System.Windows.Forms.TabPage MonitorClipBoardTabPage;
		private MonitorClipboardUserControl monitorClipboardUserControl1;
	}
}
