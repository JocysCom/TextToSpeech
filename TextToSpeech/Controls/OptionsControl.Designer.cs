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
			this.LoggingGroupBox = new System.Windows.Forms.GroupBox();
			this.LoggingPlaySoundCheckBox = new System.Windows.Forms.CheckBox();
			this.LogFolderLabel = new System.Windows.Forms.Label();
			this.FilterTextLabel = new System.Windows.Forms.Label();
			this.LoggingFolderTextBox = new System.Windows.Forms.TextBox();
			this.HowToButton = new System.Windows.Forms.Button();
			this.OpenButton = new System.Windows.Forms.Button();
			this.LoggingTextBox = new System.Windows.Forms.TextBox();
			this.LoggingCheckBox = new System.Windows.Forms.CheckBox();
			this.CaptureGroupBox = new System.Windows.Forms.GroupBox();
			this.CaptureWinButton = new System.Windows.Forms.RadioButton();
			this.CaptureSocButton = new System.Windows.Forms.RadioButton();
			this.CacheOptionsGroupBox = new System.Windows.Forms.GroupBox();
			this.OpenCacheButton = new System.Windows.Forms.Button();
			this.CacheDataGeneralizeCheckBox = new System.Windows.Forms.CheckBox();
			this.CacheDataReadCheckBox = new System.Windows.Forms.CheckBox();
			this.CacheDataWriteCheckBox = new System.Windows.Forms.CheckBox();
			this.CacheLabel = new System.Windows.Forms.Label();
			this.OptionsTabControl = new System.Windows.Forms.TabControl();
			this.GeneralTabPage = new System.Windows.Forms.TabPage();
			this.NetworkTabPage = new System.Windows.Forms.TabPage();
			this.CacheTabPage = new System.Windows.Forms.TabPage();
			this.GoogleCloudTabPage = new System.Windows.Forms.TabPage();
			this.MicrosoftCortanaTabPage = new System.Windows.Forms.TabPage();
			this.cortanaUserControl1 = new JocysCom.TextToSpeech.Monitor.Controls.CortanaUserControl();
			this.AddSilenceGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.AddSilenceAfterNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.AddSilcenceBeforeNumericUpDown)).BeginInit();
			this.LoggingGroupBox.SuspendLayout();
			this.CaptureGroupBox.SuspendLayout();
			this.CacheOptionsGroupBox.SuspendLayout();
			this.OptionsTabControl.SuspendLayout();
			this.GeneralTabPage.SuspendLayout();
			this.NetworkTabPage.SuspendLayout();
			this.CacheTabPage.SuspendLayout();
			this.MicrosoftCortanaTabPage.SuspendLayout();
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
			this.AddSilenceGroupBox.Size = new System.Drawing.Size(721, 103);
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
			this.PlaybackDeviceComboBox.Size = new System.Drawing.Size(448, 21);
			this.PlaybackDeviceComboBox.TabIndex = 33;
			this.PlaybackDeviceComboBox.SelectedIndexChanged += new System.EventHandler(this.PlaybackDeviceComboBox_SelectedIndexChanged);
			// 
			// RefreshPlaybackDevices
			// 
			this.RefreshPlaybackDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RefreshPlaybackDevices.Location = new System.Drawing.Point(545, 70);
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
			// LoggingGroupBox
			// 
			this.LoggingGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.LoggingGroupBox.Controls.Add(this.LoggingPlaySoundCheckBox);
			this.LoggingGroupBox.Controls.Add(this.LogFolderLabel);
			this.LoggingGroupBox.Controls.Add(this.FilterTextLabel);
			this.LoggingGroupBox.Controls.Add(this.LoggingFolderTextBox);
			this.LoggingGroupBox.Controls.Add(this.HowToButton);
			this.LoggingGroupBox.Controls.Add(this.OpenButton);
			this.LoggingGroupBox.Controls.Add(this.LoggingTextBox);
			this.LoggingGroupBox.Controls.Add(this.LoggingCheckBox);
			this.LoggingGroupBox.Location = new System.Drawing.Point(6, 115);
			this.LoggingGroupBox.Name = "LoggingGroupBox";
			this.LoggingGroupBox.Size = new System.Drawing.Size(721, 128);
			this.LoggingGroupBox.TabIndex = 9;
			this.LoggingGroupBox.TabStop = false;
			this.LoggingGroupBox.Text = "Log Network Packets Test ( Plugin Helper )";
			// 
			// LoggingPlaySoundCheckBox
			// 
			this.LoggingPlaySoundCheckBox.AutoSize = true;
			this.LoggingPlaySoundCheckBox.Checked = true;
			this.LoggingPlaySoundCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.LoggingPlaySoundCheckBox.Location = new System.Drawing.Point(72, 22);
			this.LoggingPlaySoundCheckBox.Name = "LoggingPlaySoundCheckBox";
			this.LoggingPlaySoundCheckBox.Size = new System.Drawing.Size(328, 17);
			this.LoggingPlaySoundCheckBox.TabIndex = 11;
			this.LoggingPlaySoundCheckBox.Text = "Play “Radio2” sound, when filter text is found in network packet.";
			this.LoggingPlaySoundCheckBox.UseVisualStyleBackColor = true;
			// 
			// LogFolderLabel
			// 
			this.LogFolderLabel.AutoSize = true;
			this.LogFolderLabel.Location = new System.Drawing.Point(84, 42);
			this.LogFolderLabel.Name = "LogFolderLabel";
			this.LogFolderLabel.Size = new System.Drawing.Size(60, 13);
			this.LogFolderLabel.TabIndex = 10;
			this.LogFolderLabel.Text = "Log Folder:";
			// 
			// FilterTextLabel
			// 
			this.FilterTextLabel.AutoSize = true;
			this.FilterTextLabel.Location = new System.Drawing.Point(6, 42);
			this.FilterTextLabel.Name = "FilterTextLabel";
			this.FilterTextLabel.Size = new System.Drawing.Size(56, 13);
			this.FilterTextLabel.TabIndex = 10;
			this.FilterTextLabel.Text = "Filter Text:";
			// 
			// LoggingFolderTextBox
			// 
			this.LoggingFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LoggingFolderTextBox.Location = new System.Drawing.Point(91, 58);
			this.LoggingFolderTextBox.Name = "LoggingFolderTextBox";
			this.LoggingFolderTextBox.ReadOnly = true;
			this.LoggingFolderTextBox.Size = new System.Drawing.Size(543, 20);
			this.LoggingFolderTextBox.TabIndex = 7;
			// 
			// HowToButton
			// 
			this.HowToButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.HowToButton.Location = new System.Drawing.Point(640, 16);
			this.HowToButton.Name = "HowToButton";
			this.HowToButton.Size = new System.Drawing.Size(75, 23);
			this.HowToButton.TabIndex = 5;
			this.HowToButton.Text = "How To...";
			this.HowToButton.UseVisualStyleBackColor = true;
			this.HowToButton.Click += new System.EventHandler(this.HowToButton_Click);
			// 
			// OpenButton
			// 
			this.OpenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.OpenButton.Location = new System.Drawing.Point(640, 56);
			this.OpenButton.Name = "OpenButton";
			this.OpenButton.Size = new System.Drawing.Size(75, 23);
			this.OpenButton.TabIndex = 5;
			this.OpenButton.Text = "Open...";
			this.OpenButton.UseVisualStyleBackColor = true;
			this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
			// 
			// LoggingTextBox
			// 
			this.LoggingTextBox.Location = new System.Drawing.Point(6, 58);
			this.LoggingTextBox.Name = "LoggingTextBox";
			this.LoggingTextBox.Size = new System.Drawing.Size(79, 20);
			this.LoggingTextBox.TabIndex = 1;
			this.LoggingTextBox.Text = "me66age";
			// 
			// LoggingCheckBox
			// 
			this.LoggingCheckBox.AutoSize = true;
			this.LoggingCheckBox.Location = new System.Drawing.Point(9, 22);
			this.LoggingCheckBox.Name = "LoggingCheckBox";
			this.LoggingCheckBox.Size = new System.Drawing.Size(59, 17);
			this.LoggingCheckBox.TabIndex = 0;
			this.LoggingCheckBox.Text = "Enable";
			this.LoggingCheckBox.UseVisualStyleBackColor = true;
			// 
			// CaptureGroupBox
			// 
			this.CaptureGroupBox.Controls.Add(this.CaptureWinButton);
			this.CaptureGroupBox.Controls.Add(this.CaptureSocButton);
			this.CaptureGroupBox.Location = new System.Drawing.Point(3, 3);
			this.CaptureGroupBox.Name = "CaptureGroupBox";
			this.CaptureGroupBox.Size = new System.Drawing.Size(211, 70);
			this.CaptureGroupBox.TabIndex = 10;
			this.CaptureGroupBox.TabStop = false;
			this.CaptureGroupBox.Text = "Packet Capture Librarry";
			// 
			// CaptureWinButton
			// 
			this.CaptureWinButton.AutoSize = true;
			this.CaptureWinButton.Location = new System.Drawing.Point(6, 46);
			this.CaptureWinButton.Name = "CaptureWinButton";
			this.CaptureWinButton.Size = new System.Drawing.Size(69, 17);
			this.CaptureWinButton.TabIndex = 0;
			this.CaptureWinButton.TabStop = true;
			this.CaptureWinButton.Text = "WinPcap";
			this.CaptureWinButton.UseVisualStyleBackColor = true;
			// 
			// CaptureSocButton
			// 
			this.CaptureSocButton.AutoSize = true;
			this.CaptureSocButton.Location = new System.Drawing.Point(6, 20);
			this.CaptureSocButton.Name = "CaptureSocButton";
			this.CaptureSocButton.Size = new System.Drawing.Size(138, 17);
			this.CaptureSocButton.TabIndex = 0;
			this.CaptureSocButton.TabStop = true;
			this.CaptureSocButton.Text = "Microsoft .NET Sockets";
			this.CaptureSocButton.UseVisualStyleBackColor = true;
			// 
			// CacheOptionsGroupBox
			// 
			this.CacheOptionsGroupBox.Controls.Add(this.OpenCacheButton);
			this.CacheOptionsGroupBox.Controls.Add(this.CacheDataGeneralizeCheckBox);
			this.CacheOptionsGroupBox.Controls.Add(this.CacheDataReadCheckBox);
			this.CacheOptionsGroupBox.Controls.Add(this.CacheDataWriteCheckBox);
			this.CacheOptionsGroupBox.Controls.Add(this.CacheLabel);
			this.CacheOptionsGroupBox.Location = new System.Drawing.Point(6, 6);
			this.CacheOptionsGroupBox.Name = "CacheOptionsGroupBox";
			this.CacheOptionsGroupBox.Size = new System.Drawing.Size(211, 128);
			this.CacheOptionsGroupBox.TabIndex = 10;
			this.CacheOptionsGroupBox.TabStop = false;
			this.CacheOptionsGroupBox.Text = "Cache Options (Create TTS files)";
			// 
			// OpenCacheButton
			// 
			this.OpenCacheButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.OpenCacheButton.Location = new System.Drawing.Point(130, 19);
			this.OpenCacheButton.Name = "OpenCacheButton";
			this.OpenCacheButton.Size = new System.Drawing.Size(75, 23);
			this.OpenCacheButton.TabIndex = 6;
			this.OpenCacheButton.Text = "Open...";
			this.OpenCacheButton.UseVisualStyleBackColor = true;
			this.OpenCacheButton.Click += new System.EventHandler(this.OpenCacheButton_Click);
			// 
			// CacheDataGeneralizeCheckBox
			// 
			this.CacheDataGeneralizeCheckBox.AutoSize = true;
			this.CacheDataGeneralizeCheckBox.Location = new System.Drawing.Point(6, 69);
			this.CacheDataGeneralizeCheckBox.Name = "CacheDataGeneralizeCheckBox";
			this.CacheDataGeneralizeCheckBox.Size = new System.Drawing.Size(198, 17);
			this.CacheDataGeneralizeCheckBox.TabIndex = 0;
			this.CacheDataGeneralizeCheckBox.Text = "Replace Class && Name to \"Traveler\"";
			this.CacheDataGeneralizeCheckBox.UseVisualStyleBackColor = true;
			// 
			// CacheDataReadCheckBox
			// 
			this.CacheDataReadCheckBox.AutoSize = true;
			this.CacheDataReadCheckBox.Location = new System.Drawing.Point(6, 46);
			this.CacheDataReadCheckBox.Name = "CacheDataReadCheckBox";
			this.CacheDataReadCheckBox.Size = new System.Drawing.Size(138, 17);
			this.CacheDataReadCheckBox.TabIndex = 0;
			this.CacheDataReadCheckBox.Text = "Read Files (MP3, WAV)";
			this.CacheDataReadCheckBox.UseVisualStyleBackColor = true;
			// 
			// CacheDataWriteCheckBox
			// 
			this.CacheDataWriteCheckBox.AutoSize = true;
			this.CacheDataWriteCheckBox.Location = new System.Drawing.Point(6, 23);
			this.CacheDataWriteCheckBox.Name = "CacheDataWriteCheckBox";
			this.CacheDataWriteCheckBox.Size = new System.Drawing.Size(109, 17);
			this.CacheDataWriteCheckBox.TabIndex = 0;
			this.CacheDataWriteCheckBox.Text = "Write Files (WAV)";
			this.CacheDataWriteCheckBox.UseVisualStyleBackColor = true;
			// 
			// CacheLabel
			// 
			this.CacheLabel.AutoSize = true;
			this.CacheLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.CacheLabel.ForeColor = System.Drawing.SystemColors.GrayText;
			this.CacheLabel.Location = new System.Drawing.Point(3, 112);
			this.CacheLabel.Name = "CacheLabel";
			this.CacheLabel.Size = new System.Drawing.Size(91, 13);
			this.CacheLabel.TabIndex = 3;
			this.CacheLabel.Text = "Size: {0} files ({1})";
			// 
			// OptionsTabControl
			// 
			this.OptionsTabControl.Controls.Add(this.GeneralTabPage);
			this.OptionsTabControl.Controls.Add(this.NetworkTabPage);
			this.OptionsTabControl.Controls.Add(this.CacheTabPage);
			this.OptionsTabControl.Controls.Add(this.GoogleCloudTabPage);
			this.OptionsTabControl.Controls.Add(this.MicrosoftCortanaTabPage);
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
			this.GeneralTabPage.Controls.Add(this.LoggingGroupBox);
			this.GeneralTabPage.Location = new System.Drawing.Point(4, 22);
			this.GeneralTabPage.Name = "GeneralTabPage";
			this.GeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.GeneralTabPage.Size = new System.Drawing.Size(733, 313);
			this.GeneralTabPage.TabIndex = 0;
			this.GeneralTabPage.Text = "General";
			// 
			// NetworkTabPage
			// 
			this.NetworkTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.NetworkTabPage.Controls.Add(this.CaptureGroupBox);
			this.NetworkTabPage.Location = new System.Drawing.Point(4, 22);
			this.NetworkTabPage.Name = "NetworkTabPage";
			this.NetworkTabPage.Size = new System.Drawing.Size(733, 313);
			this.NetworkTabPage.TabIndex = 4;
			this.NetworkTabPage.Text = "Network";
			// 
			// CacheTabPage
			// 
			this.CacheTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.CacheTabPage.Controls.Add(this.CacheOptionsGroupBox);
			this.CacheTabPage.Location = new System.Drawing.Point(4, 22);
			this.CacheTabPage.Name = "CacheTabPage";
			this.CacheTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.CacheTabPage.Size = new System.Drawing.Size(733, 313);
			this.CacheTabPage.TabIndex = 1;
			this.CacheTabPage.Text = "Cache";
			// 
			// GoogleCloudTabPage
			// 
			this.GoogleCloudTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.GoogleCloudTabPage.Location = new System.Drawing.Point(4, 22);
			this.GoogleCloudTabPage.Name = "GoogleCloudTabPage";
			this.GoogleCloudTabPage.Size = new System.Drawing.Size(733, 313);
			this.GoogleCloudTabPage.TabIndex = 2;
			this.GoogleCloudTabPage.Text = "Google Cloud";
			// 
			// MicrosoftCortanaTabPage
			// 
			this.MicrosoftCortanaTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.MicrosoftCortanaTabPage.Controls.Add(this.cortanaUserControl1);
			this.MicrosoftCortanaTabPage.Location = new System.Drawing.Point(4, 22);
			this.MicrosoftCortanaTabPage.Name = "MicrosoftCortanaTabPage";
			this.MicrosoftCortanaTabPage.Size = new System.Drawing.Size(746, 341);
			this.MicrosoftCortanaTabPage.TabIndex = 3;
			this.MicrosoftCortanaTabPage.Text = "Microsoft Cortana";
			// 
			// cortanaUserControl1
			// 
			this.cortanaUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cortanaUserControl1.Location = new System.Drawing.Point(0, 0);
			this.cortanaUserControl1.Name = "cortanaUserControl1";
			this.cortanaUserControl1.Size = new System.Drawing.Size(746, 341);
			this.cortanaUserControl1.TabIndex = 0;
			// 
			// OptionsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.OptionsTabControl);
			this.Name = "OptionsControl";
			this.Size = new System.Drawing.Size(754, 367);
			this.Load += new System.EventHandler(this.OptionsControl_Load);
			this.AddSilenceGroupBox.ResumeLayout(false);
			this.AddSilenceGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.AddSilenceAfterNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.AddSilcenceBeforeNumericUpDown)).EndInit();
			this.LoggingGroupBox.ResumeLayout(false);
			this.LoggingGroupBox.PerformLayout();
			this.CaptureGroupBox.ResumeLayout(false);
			this.CaptureGroupBox.PerformLayout();
			this.CacheOptionsGroupBox.ResumeLayout(false);
			this.CacheOptionsGroupBox.PerformLayout();
			this.OptionsTabControl.ResumeLayout(false);
			this.GeneralTabPage.ResumeLayout(false);
			this.NetworkTabPage.ResumeLayout(false);
			this.CacheTabPage.ResumeLayout(false);
			this.MicrosoftCortanaTabPage.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox LoggingGroupBox;
        private System.Windows.Forms.TextBox LoggingTextBox;
        private System.Windows.Forms.CheckBox LoggingCheckBox;
        private System.Windows.Forms.TextBox LoggingFolderTextBox;
        private System.Windows.Forms.Label LogFolderLabel;
        private System.Windows.Forms.Label FilterTextLabel;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.CheckBox LoggingPlaySoundCheckBox;
		private System.Windows.Forms.GroupBox CaptureGroupBox;
		private System.Windows.Forms.RadioButton CaptureWinButton;
		private System.Windows.Forms.RadioButton CaptureSocButton;
		private System.Windows.Forms.GroupBox CacheOptionsGroupBox;
		private System.Windows.Forms.CheckBox CacheDataWriteCheckBox;
		private System.Windows.Forms.Button OpenCacheButton;
		private System.Windows.Forms.Label CacheLabel;
		private System.Windows.Forms.Button HowToButton;
		private System.Windows.Forms.ComboBox PlaybackDeviceComboBox;
		private System.Windows.Forms.Button RefreshPlaybackDevices;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox CacheDataGeneralizeCheckBox;
		private System.Windows.Forms.CheckBox CacheDataReadCheckBox;
		private System.Windows.Forms.TabControl OptionsTabControl;
		private System.Windows.Forms.TabPage GeneralTabPage;
		private System.Windows.Forms.TabPage CacheTabPage;
		private System.Windows.Forms.TabPage GoogleCloudTabPage;
		private System.Windows.Forms.TabPage MicrosoftCortanaTabPage;
		private System.Windows.Forms.TabPage NetworkTabPage;
		private CortanaUserControl cortanaUserControl1;
	}
}
