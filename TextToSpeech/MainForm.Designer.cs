namespace JocysCom.TextToSpeech.Monitor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			this.TabsImageList = new System.Windows.Forms.ImageList(this.components);
			this.MessagesTabControl = new System.Windows.Forms.TabControl();
			this.VoicesTabPage = new System.Windows.Forms.TabPage();
			this.VoicesPanel = new JocysCom.TextToSpeech.Monitor.Controls.VoicesUserControl();
			this.VoiceErrorLabel = new System.Windows.Forms.Label();
			this.VoiceDefaultsTabPage = new System.Windows.Forms.TabPage();
			this.VoiceDefaultsPanel = new JocysCom.TextToSpeech.Monitor.Controls.VoicesDefaultsUserControl();
			this.SoundsTabPage = new System.Windows.Forms.TabPage();
			this.SoundsPanel = new JocysCom.TextToSpeech.Monitor.Controls.SoundsUserControl();
			this.AcronymsTabPage = new System.Windows.Forms.TabPage();
			this.acronymsUserControl1 = new JocysCom.TextToSpeech.Monitor.Controls.AcronymsUserControl();
			this.EffectsPresetsEditorTabPage = new System.Windows.Forms.TabPage();
			this.EffectPresetsEditorSoundEffectsControl = new JocysCom.TextToSpeech.Monitor.Controls.SoundEffectsControl();
			this.OptionsTabPage = new System.Windows.Forms.TabPage();
			this.OptionsPanel = new JocysCom.TextToSpeech.Monitor.Controls.OptionsControl();
			this.HelpTabPage = new System.Windows.Forms.TabPage();
			this.AboutRichTextBox = new System.Windows.Forms.RichTextBox();
			this.UpdateTabPage = new System.Windows.Forms.TabPage();
			this.UpdateButton = new System.Windows.Forms.Button();
			this.UpdateLabel = new System.Windows.Forms.Label();
			this.UpdateWebBrowser = new System.Windows.Forms.WebBrowser();
			this.EffectTabControl = new System.Windows.Forms.TabControl();
			this.EffectPresetsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.EffectTabPage = new System.Windows.Forms.TabPage();
			this.EffectsPresetsDataGridView = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.MessagesDataGridView = new System.Windows.Forms.DataGridView();
			this.DestinationAddressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SequenceNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.WowDataLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.VoiceXmlColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.MessagesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.MessagesClearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.RateLabel = new System.Windows.Forms.Label();
			this.PitchLabel = new System.Windows.Forms.Label();
			this.VolumeLabel = new System.Windows.Forms.Label();
			this.TextXmlTabControl = new System.Windows.Forms.TabControl();
			this.SandBoxTabPage = new System.Windows.Forms.TabPage();
			this.SandBoxTextBox = new System.Windows.Forms.TextBox();
			this.MessagesTabPage = new System.Windows.Forms.TabPage();
			this.SapiTabPage = new System.Windows.Forms.TabPage();
			this.SapiTextBox = new System.Windows.Forms.TextBox();
			this.PlayListTabPage = new System.Windows.Forms.TabPage();
			this.PlayListDataGridView = new System.Windows.Forms.DataGridView();
			this.PlayListStatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CommentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PlayListDurationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PlayListTextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
			this.ProcessStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.ErrorStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.FilterStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.EmptyStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.StateStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.PacketsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.AudioBitsPerSampleComboBox = new System.Windows.Forms.ComboBox();
			this.AudioBitsPerSampleLabel = new System.Windows.Forms.Label();
			this.AudioSampleRateComboBox = new System.Windows.Forms.ComboBox();
			this.AudioSampleRateLabel = new System.Windows.Forms.Label();
			this.AudioChannelsComboBox = new System.Windows.Forms.ComboBox();
			this.AudioChannelsLabel = new System.Windows.Forms.Label();
			this.RecognizeButton = new System.Windows.Forms.Button();
			this.MainHelpLabel = new System.Windows.Forms.Label();
			this.MonitorClipboardLabel = new System.Windows.Forms.Label();
			this.ProductPictureBox = new System.Windows.Forms.PictureBox();
			this.PitchTextBox = new System.Windows.Forms.TextBox();
			this.RateTextBox = new System.Windows.Forms.TextBox();
			this.VolumeTrackBar = new System.Windows.Forms.TrackBar();
			this.StopButton = new System.Windows.Forms.Button();
			this.SpeakButton = new System.Windows.Forms.Button();
			this.VolumeTextBox = new System.Windows.Forms.TextBox();
			this.IncomingGroupBox = new System.Windows.Forms.GroupBox();
			this.InCommandLabel = new System.Windows.Forms.Label();
			this.InVolumeLabel = new System.Windows.Forms.Label();
			this.InRateLabel = new System.Windows.Forms.Label();
			this.InPitchLabel = new System.Windows.Forms.Label();
			this.InGroupLabel = new System.Windows.Forms.Label();
			this.InEffectLabel = new System.Windows.Forms.Label();
			this.InGenderLabel = new System.Windows.Forms.Label();
			this.InNameLabel = new System.Windows.Forms.Label();
			this.InLanguageLabel = new System.Windows.Forms.Label();
			this.InPartLabel = new System.Windows.Forms.Label();
			this.InLanguageTextBox = new System.Windows.Forms.TextBox();
			this.InGroupTextBox = new System.Windows.Forms.TextBox();
			this.InPartTextBox = new System.Windows.Forms.TextBox();
			this.InCommandTextBox = new System.Windows.Forms.TextBox();
			this.InVolumeTextBox = new System.Windows.Forms.TextBox();
			this.InEffectTextBox = new System.Windows.Forms.TextBox();
			this.InPitchTextBox = new System.Windows.Forms.TextBox();
			this.InRateTextBox = new System.Windows.Forms.TextBox();
			this.InGenderTextBox = new System.Windows.Forms.TextBox();
			this.InNameTextBox = new System.Windows.Forms.TextBox();
			this.DefaultGenderLabel = new System.Windows.Forms.Label();
			this.DefaultIntroSoundLabel = new System.Windows.Forms.Label();
			this.DefaultIntroSoundComboBox = new System.Windows.Forms.ComboBox();
			this.MonitorsEnabledCheckBox = new System.Windows.Forms.CheckBox();
			this.MonitorClipboardComboBox = new System.Windows.Forms.ComboBox();
			this.GenderComboBox = new System.Windows.Forms.ComboBox();
			this.PitchMaxComboBox = new System.Windows.Forms.ComboBox();
			this.RateMaxComboBox = new System.Windows.Forms.ComboBox();
			this.PitchMinComboBox = new System.Windows.Forms.ComboBox();
			this.RateMinComboBox = new System.Windows.Forms.ComboBox();
			this.ProgramComboBox = new System.Windows.Forms.ComboBox();
			this.MessagesTabControl.SuspendLayout();
			this.VoicesTabPage.SuspendLayout();
			this.VoiceDefaultsTabPage.SuspendLayout();
			this.SoundsTabPage.SuspendLayout();
			this.AcronymsTabPage.SuspendLayout();
			this.EffectsPresetsEditorTabPage.SuspendLayout();
			this.OptionsTabPage.SuspendLayout();
			this.HelpTabPage.SuspendLayout();
			this.UpdateTabPage.SuspendLayout();
			this.EffectTabControl.SuspendLayout();
			this.EffectPresetsContextMenuStrip.SuspendLayout();
			this.EffectTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.EffectsPresetsDataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.MessagesDataGridView)).BeginInit();
			this.MessagesContextMenuStrip.SuspendLayout();
			this.TextXmlTabControl.SuspendLayout();
			this.SandBoxTabPage.SuspendLayout();
			this.MessagesTabPage.SuspendLayout();
			this.SapiTabPage.SuspendLayout();
			this.PlayListTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PlayListDataGridView)).BeginInit();
			this.MainStatusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ProductPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.VolumeTrackBar)).BeginInit();
			this.IncomingGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// TabsImageList
			// 
			this.TabsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TabsImageList.ImageStream")));
			this.TabsImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.TabsImageList.Images.SetKeyName(0, "About.png");
			this.TabsImageList.Images.SetKeyName(1, "Businessman.png");
			this.TabsImageList.Images.SetKeyName(2, "BusinessPeople.png");
			this.TabsImageList.Images.SetKeyName(3, "Code_Edit.png");
			this.TabsImageList.Images.SetKeyName(4, "Download.png");
			this.TabsImageList.Images.SetKeyName(5, "eye.png");
			this.TabsImageList.Images.SetKeyName(6, "Information.png");
			this.TabsImageList.Images.SetKeyName(7, "Message_Incoming.png");
			this.TabsImageList.Images.SetKeyName(8, "Message_SAPI.png");
			this.TabsImageList.Images.SetKeyName(9, "Music.png");
			this.TabsImageList.Images.SetKeyName(10, "Play_List.png");
			this.TabsImageList.Images.SetKeyName(11, "Window_Effects_Editor.png");
			this.TabsImageList.Images.SetKeyName(12, "Window_Effects_Presets.png");
			this.TabsImageList.Images.SetKeyName(13, "Options_16x16.png");
			this.TabsImageList.Images.SetKeyName(14, "Acronyms_16x16.png");
			// 
			// MessagesTabControl
			// 
			this.MessagesTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MessagesTabControl.Controls.Add(this.VoicesTabPage);
			this.MessagesTabControl.Controls.Add(this.VoiceDefaultsTabPage);
			this.MessagesTabControl.Controls.Add(this.SoundsTabPage);
			this.MessagesTabControl.Controls.Add(this.AcronymsTabPage);
			this.MessagesTabControl.Controls.Add(this.EffectsPresetsEditorTabPage);
			this.MessagesTabControl.Controls.Add(this.OptionsTabPage);
			this.MessagesTabControl.Controls.Add(this.HelpTabPage);
			this.MessagesTabControl.Controls.Add(this.UpdateTabPage);
			this.MessagesTabControl.ImageList = this.TabsImageList;
			this.MessagesTabControl.Location = new System.Drawing.Point(6, 34);
			this.MessagesTabControl.Margin = new System.Windows.Forms.Padding(10);
			this.MessagesTabControl.Name = "MessagesTabControl";
			this.MessagesTabControl.Padding = new System.Drawing.Point(6, 5);
			this.MessagesTabControl.SelectedIndex = 0;
			this.MessagesTabControl.Size = new System.Drawing.Size(824, 297);
			this.MessagesTabControl.TabIndex = 1;
			// 
			// VoicesTabPage
			// 
			this.VoicesTabPage.Controls.Add(this.VoicesPanel);
			this.VoicesTabPage.Controls.Add(this.VoiceErrorLabel);
			this.VoicesTabPage.ImageKey = "Businessman.png";
			this.VoicesTabPage.Location = new System.Drawing.Point(4, 27);
			this.VoicesTabPage.Name = "VoicesTabPage";
			this.VoicesTabPage.Size = new System.Drawing.Size(816, 266);
			this.VoicesTabPage.TabIndex = 3;
			this.VoicesTabPage.Text = "Voices";
			// 
			// VoicesPanel
			// 
			this.VoicesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.VoicesPanel.Location = new System.Drawing.Point(0, 0);
			this.VoicesPanel.Name = "VoicesPanel";
			this.VoicesPanel.Size = new System.Drawing.Size(816, 247);
			this.VoicesPanel.TabIndex = 2;
			// 
			// VoiceErrorLabel
			// 
			this.VoiceErrorLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.VoiceErrorLabel.ForeColor = System.Drawing.Color.DarkRed;
			this.VoiceErrorLabel.Location = new System.Drawing.Point(0, 247);
			this.VoiceErrorLabel.Name = "VoiceErrorLabel";
			this.VoiceErrorLabel.Padding = new System.Windows.Forms.Padding(3);
			this.VoiceErrorLabel.Size = new System.Drawing.Size(816, 19);
			this.VoiceErrorLabel.TabIndex = 1;
			this.VoiceErrorLabel.Text = "Voice Error";
			this.VoiceErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.VoiceErrorLabel.Visible = false;
			// 
			// VoiceDefaultsTabPage
			// 
			this.VoiceDefaultsTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.VoiceDefaultsTabPage.Controls.Add(this.VoiceDefaultsPanel);
			this.VoiceDefaultsTabPage.ImageKey = "BusinessPeople.png";
			this.VoiceDefaultsTabPage.Location = new System.Drawing.Point(4, 27);
			this.VoiceDefaultsTabPage.Name = "VoiceDefaultsTabPage";
			this.VoiceDefaultsTabPage.Size = new System.Drawing.Size(816, 266);
			this.VoiceDefaultsTabPage.TabIndex = 5;
			this.VoiceDefaultsTabPage.Text = "Voice Defaults";
			// 
			// VoiceDefaultsPanel
			// 
			this.VoiceDefaultsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.VoiceDefaultsPanel.Location = new System.Drawing.Point(0, 0);
			this.VoiceDefaultsPanel.Name = "VoiceDefaultsPanel";
			this.VoiceDefaultsPanel.Size = new System.Drawing.Size(816, 266);
			this.VoiceDefaultsPanel.TabIndex = 0;
			// 
			// SoundsTabPage
			// 
			this.SoundsTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.SoundsTabPage.Controls.Add(this.SoundsPanel);
			this.SoundsTabPage.ImageKey = "Music.png";
			this.SoundsTabPage.Location = new System.Drawing.Point(4, 27);
			this.SoundsTabPage.Name = "SoundsTabPage";
			this.SoundsTabPage.Size = new System.Drawing.Size(816, 266);
			this.SoundsTabPage.TabIndex = 7;
			this.SoundsTabPage.Text = "Intro Sounds";
			// 
			// SoundsPanel
			// 
			this.SoundsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SoundsPanel.Location = new System.Drawing.Point(0, 0);
			this.SoundsPanel.Name = "SoundsPanel";
			this.SoundsPanel.Size = new System.Drawing.Size(816, 266);
			this.SoundsPanel.TabIndex = 0;
			// 
			// AcronymsTabPage
			// 
			this.AcronymsTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.AcronymsTabPage.Controls.Add(this.acronymsUserControl1);
			this.AcronymsTabPage.ImageKey = "Acronyms_16x16.png";
			this.AcronymsTabPage.Location = new System.Drawing.Point(4, 27);
			this.AcronymsTabPage.Name = "AcronymsTabPage";
			this.AcronymsTabPage.Size = new System.Drawing.Size(816, 266);
			this.AcronymsTabPage.TabIndex = 9;
			this.AcronymsTabPage.Text = "Acronyms";
			// 
			// acronymsUserControl1
			// 
			this.acronymsUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.acronymsUserControl1.Location = new System.Drawing.Point(0, 0);
			this.acronymsUserControl1.Name = "acronymsUserControl1";
			this.acronymsUserControl1.Size = new System.Drawing.Size(816, 266);
			this.acronymsUserControl1.TabIndex = 0;
			// 
			// EffectsPresetsEditorTabPage
			// 
			this.EffectsPresetsEditorTabPage.Controls.Add(this.EffectPresetsEditorSoundEffectsControl);
			this.EffectsPresetsEditorTabPage.ImageKey = "Window_Effects_Editor.png";
			this.EffectsPresetsEditorTabPage.Location = new System.Drawing.Point(4, 27);
			this.EffectsPresetsEditorTabPage.Name = "EffectsPresetsEditorTabPage";
			this.EffectsPresetsEditorTabPage.Size = new System.Drawing.Size(816, 266);
			this.EffectsPresetsEditorTabPage.TabIndex = 1;
			this.EffectsPresetsEditorTabPage.Text = "Efect Preset Editor";
			// 
			// EffectPresetsEditorSoundEffectsControl
			// 
			this.EffectPresetsEditorSoundEffectsControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.EffectPresetsEditorSoundEffectsControl.Location = new System.Drawing.Point(0, 0);
			this.EffectPresetsEditorSoundEffectsControl.Name = "EffectPresetsEditorSoundEffectsControl";
			this.EffectPresetsEditorSoundEffectsControl.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.EffectPresetsEditorSoundEffectsControl.Size = new System.Drawing.Size(816, 266);
			this.EffectPresetsEditorSoundEffectsControl.TabIndex = 0;
			this.EffectPresetsEditorSoundEffectsControl.Load += new System.EventHandler(this.EffectPresetsEditorSoundEffectsControl_Load);
			// 
			// OptionsTabPage
			// 
			this.OptionsTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.OptionsTabPage.Controls.Add(this.OptionsPanel);
			this.OptionsTabPage.ImageKey = "Options_16x16.png";
			this.OptionsTabPage.Location = new System.Drawing.Point(4, 27);
			this.OptionsTabPage.Name = "OptionsTabPage";
			this.OptionsTabPage.Size = new System.Drawing.Size(816, 266);
			this.OptionsTabPage.TabIndex = 8;
			this.OptionsTabPage.Text = "Options";
			// 
			// OptionsPanel
			// 
			this.OptionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OptionsPanel.Location = new System.Drawing.Point(0, 0);
			this.OptionsPanel.Name = "OptionsPanel";
			this.OptionsPanel.silenceAfter = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.OptionsPanel.silenceBefore = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.OptionsPanel.Size = new System.Drawing.Size(816, 266);
			this.OptionsPanel.TabIndex = 0;
			// 
			// HelpTabPage
			// 
			this.HelpTabPage.Controls.Add(this.AboutRichTextBox);
			this.HelpTabPage.ImageKey = "About.png";
			this.HelpTabPage.Location = new System.Drawing.Point(4, 27);
			this.HelpTabPage.Name = "HelpTabPage";
			this.HelpTabPage.Size = new System.Drawing.Size(816, 266);
			this.HelpTabPage.TabIndex = 2;
			this.HelpTabPage.Text = "Help";
			// 
			// AboutRichTextBox
			// 
			this.AboutRichTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.AboutRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.AboutRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AboutRichTextBox.Location = new System.Drawing.Point(0, 0);
			this.AboutRichTextBox.Name = "AboutRichTextBox";
			this.AboutRichTextBox.ReadOnly = true;
			this.AboutRichTextBox.Size = new System.Drawing.Size(816, 266);
			this.AboutRichTextBox.TabIndex = 18;
			this.AboutRichTextBox.Text = "";
			this.AboutRichTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.AboutRichTextBox_LinkClicked);
			// 
			// UpdateTabPage
			// 
			this.UpdateTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.UpdateTabPage.Controls.Add(this.UpdateButton);
			this.UpdateTabPage.Controls.Add(this.UpdateLabel);
			this.UpdateTabPage.Controls.Add(this.UpdateWebBrowser);
			this.UpdateTabPage.ImageKey = "Download.png";
			this.UpdateTabPage.Location = new System.Drawing.Point(4, 27);
			this.UpdateTabPage.Name = "UpdateTabPage";
			this.UpdateTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.UpdateTabPage.Size = new System.Drawing.Size(816, 266);
			this.UpdateTabPage.TabIndex = 4;
			this.UpdateTabPage.Text = "Update ";
			this.UpdateTabPage.Click += new System.EventHandler(this.UpdateTabPage_Click);
			// 
			// UpdateButton
			// 
			this.UpdateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.UpdateButton.Location = new System.Drawing.Point(675, 237);
			this.UpdateButton.Name = "UpdateButton";
			this.UpdateButton.Size = new System.Drawing.Size(135, 23);
			this.UpdateButton.TabIndex = 3;
			this.UpdateButton.Text = "Check for updates";
			this.UpdateButton.UseVisualStyleBackColor = true;
			this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
			// 
			// UpdateLabel
			// 
			this.UpdateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.UpdateLabel.Location = new System.Drawing.Point(10, 5);
			this.UpdateLabel.Margin = new System.Windows.Forms.Padding(0);
			this.UpdateLabel.Name = "UpdateLabel";
			this.UpdateLabel.Size = new System.Drawing.Size(804, 13);
			this.UpdateLabel.TabIndex = 2;
			this.UpdateLabel.Text = "UpdateLabelText";
			this.UpdateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// UpdateWebBrowser
			// 
			this.UpdateWebBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.UpdateWebBrowser.Location = new System.Drawing.Point(6, 21);
			this.UpdateWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.UpdateWebBrowser.Name = "UpdateWebBrowser";
			this.UpdateWebBrowser.ScrollBarsEnabled = false;
			this.UpdateWebBrowser.Size = new System.Drawing.Size(804, 210);
			this.UpdateWebBrowser.TabIndex = 1;
			this.UpdateWebBrowser.Url = new System.Uri("http://www.jocys.com/files/updates/JocysCom.TextToSpeech.Monitor.html", System.UriKind.Absolute);
			// 
			// EffectTabControl
			// 
			this.EffectTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.EffectTabControl.ContextMenuStrip = this.EffectPresetsContextMenuStrip;
			this.EffectTabControl.Controls.Add(this.EffectTabPage);
			this.EffectTabControl.ImageList = this.TabsImageList;
			this.EffectTabControl.Location = new System.Drawing.Point(834, 34);
			this.EffectTabControl.MinimumSize = new System.Drawing.Size(100, 264);
			this.EffectTabControl.Name = "EffectTabControl";
			this.EffectTabControl.Padding = new System.Drawing.Point(6, 5);
			this.EffectTabControl.SelectedIndex = 0;
			this.EffectTabControl.Size = new System.Drawing.Size(236, 297);
			this.EffectTabControl.TabIndex = 2;
			// 
			// EffectPresetsContextMenuStrip
			// 
			this.EffectPresetsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.refreshToolStripMenuItem});
			this.EffectPresetsContextMenuStrip.Name = "EffectPresetsContextMenuStrip";
			this.EffectPresetsContextMenuStrip.Size = new System.Drawing.Size(124, 70);
			// 
			// addNewToolStripMenuItem
			// 
			this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
			this.addNewToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.addNewToolStripMenuItem.Text = "Add New";
			this.addNewToolStripMenuItem.Click += new System.EventHandler(this.addNewToolStripMenuItem_Click);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			// 
			// refreshToolStripMenuItem
			// 
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			this.refreshToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.refreshToolStripMenuItem.Text = "Refresh";
			this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
			// 
			// EffectTabPage
			// 
			this.EffectTabPage.Controls.Add(this.EffectsPresetsDataGridView);
			this.EffectTabPage.ImageKey = "Window_Effects_Presets.png";
			this.EffectTabPage.Location = new System.Drawing.Point(4, 27);
			this.EffectTabPage.Name = "EffectTabPage";
			this.EffectTabPage.Size = new System.Drawing.Size(228, 266);
			this.EffectTabPage.TabIndex = 0;
			this.EffectTabPage.Text = "Effect Presets";
			this.EffectTabPage.UseVisualStyleBackColor = true;
			// 
			// EffectsPresetsDataGridView
			// 
			this.EffectsPresetsDataGridView.AllowUserToAddRows = false;
			this.EffectsPresetsDataGridView.AllowUserToDeleteRows = false;
			this.EffectsPresetsDataGridView.AllowUserToResizeRows = false;
			this.EffectsPresetsDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
			this.EffectsPresetsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.EffectsPresetsDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.EffectsPresetsDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.EffectsPresetsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.EffectsPresetsDataGridView.ColumnHeadersVisible = false;
			this.EffectsPresetsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3});
			this.EffectsPresetsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.EffectsPresetsDataGridView.Location = new System.Drawing.Point(0, 0);
			this.EffectsPresetsDataGridView.MultiSelect = false;
			this.EffectsPresetsDataGridView.Name = "EffectsPresetsDataGridView";
			this.EffectsPresetsDataGridView.ReadOnly = true;
			this.EffectsPresetsDataGridView.RowHeadersVisible = false;
			this.EffectsPresetsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.EffectsPresetsDataGridView.Size = new System.Drawing.Size(228, 266);
			this.EffectsPresetsDataGridView.TabIndex = 0;
			this.EffectsPresetsDataGridView.SelectionChanged += new System.EventHandler(this.EffectsPresetsDataGridView_SelectionChanged);
			this.EffectsPresetsDataGridView.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.EffectsPresetsDataGridView.MouseHover += new System.EventHandler(this.MouseHover_EffectPresets);
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn3.DataPropertyName = "Name";
			this.dataGridViewTextBoxColumn3.HeaderText = "Name";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			// 
			// MessagesDataGridView
			// 
			this.MessagesDataGridView.AllowUserToAddRows = false;
			this.MessagesDataGridView.AllowUserToDeleteRows = false;
			this.MessagesDataGridView.AllowUserToResizeRows = false;
			this.MessagesDataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
			this.MessagesDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.MessagesDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.MessagesDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.MessagesDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.MessagesDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.MessagesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.MessagesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DestinationAddressColumn,
            this.SequenceNumberColumn,
            this.WowDataLength,
            this.VoiceXmlColumn});
			this.MessagesDataGridView.ContextMenuStrip = this.MessagesContextMenuStrip;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.MessagesDataGridView.DefaultCellStyle = dataGridViewCellStyle5;
			this.MessagesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MessagesDataGridView.EnableHeadersVisualStyles = false;
			this.MessagesDataGridView.GridColor = System.Drawing.SystemColors.Control;
			this.MessagesDataGridView.Location = new System.Drawing.Point(0, 0);
			this.MessagesDataGridView.Margin = new System.Windows.Forms.Padding(0);
			this.MessagesDataGridView.MultiSelect = false;
			this.MessagesDataGridView.Name = "MessagesDataGridView";
			this.MessagesDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.MessagesDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.MessagesDataGridView.RowHeadersVisible = false;
			this.MessagesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.MessagesDataGridView.Size = new System.Drawing.Size(606, 278);
			this.MessagesDataGridView.TabIndex = 0;
			this.MessagesDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.MessagesDataGridView_RowsAdded);
			this.MessagesDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.MessagesDataGridView_RowsRemoved);
			// 
			// DestinationAddressColumn
			// 
			this.DestinationAddressColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.DestinationAddressColumn.DataPropertyName = "DestinationAddress";
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.DestinationAddressColumn.DefaultCellStyle = dataGridViewCellStyle2;
			this.DestinationAddressColumn.HeaderText = "Destination";
			this.DestinationAddressColumn.Name = "DestinationAddressColumn";
			this.DestinationAddressColumn.ReadOnly = true;
			this.DestinationAddressColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.DestinationAddressColumn.Width = 68;
			// 
			// SequenceNumberColumn
			// 
			this.SequenceNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.SequenceNumberColumn.DataPropertyName = "SequenceNumber";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.SequenceNumberColumn.DefaultCellStyle = dataGridViewCellStyle3;
			this.SequenceNumberColumn.HeaderText = "Sequence";
			this.SequenceNumberColumn.Name = "SequenceNumberColumn";
			this.SequenceNumberColumn.ReadOnly = true;
			this.SequenceNumberColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.SequenceNumberColumn.Width = 64;
			// 
			// WowDataLength
			// 
			this.WowDataLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.WowDataLength.DataPropertyName = "VoiceXmlLength";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.WowDataLength.DefaultCellStyle = dataGridViewCellStyle4;
			this.WowDataLength.HeaderText = "Size";
			this.WowDataLength.Name = "WowDataLength";
			this.WowDataLength.ReadOnly = true;
			this.WowDataLength.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.WowDataLength.Width = 35;
			// 
			// VoiceXmlColumn
			// 
			this.VoiceXmlColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.VoiceXmlColumn.DataPropertyName = "VoiceXml";
			this.VoiceXmlColumn.HeaderText = "XML";
			this.VoiceXmlColumn.Name = "VoiceXmlColumn";
			this.VoiceXmlColumn.ReadOnly = true;
			this.VoiceXmlColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// MessagesContextMenuStrip
			// 
			this.MessagesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MessagesClearToolStripMenuItem});
			this.MessagesContextMenuStrip.Name = "MessagesContextMenuStrip";
			this.MessagesContextMenuStrip.Size = new System.Drawing.Size(102, 26);
			// 
			// MessagesClearToolStripMenuItem
			// 
			this.MessagesClearToolStripMenuItem.Name = "MessagesClearToolStripMenuItem";
			this.MessagesClearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
			this.MessagesClearToolStripMenuItem.Text = "Clear";
			this.MessagesClearToolStripMenuItem.Click += new System.EventHandler(this.MessagesClearToolStripMenuItem_Click);
			// 
			// RateLabel
			// 
			this.RateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.RateLabel.AutoSize = true;
			this.RateLabel.Location = new System.Drawing.Point(856, 569);
			this.RateLabel.Name = "RateLabel";
			this.RateLabel.Size = new System.Drawing.Size(92, 13);
			this.RateLabel.TabIndex = 208;
			this.RateLabel.Text = "Rate [ min - max ]:";
			this.RateLabel.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.RateLabel.MouseHover += new System.EventHandler(this.RateLabel_MouseHover);
			// 
			// PitchLabel
			// 
			this.PitchLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.PitchLabel.AutoSize = true;
			this.PitchLabel.Location = new System.Drawing.Point(855, 542);
			this.PitchLabel.Name = "PitchLabel";
			this.PitchLabel.Size = new System.Drawing.Size(93, 13);
			this.PitchLabel.TabIndex = 207;
			this.PitchLabel.Text = "Pitch [ min - max ]:";
			this.PitchLabel.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.PitchLabel.MouseHover += new System.EventHandler(this.PitchLabel_MouseHover);
			// 
			// VolumeLabel
			// 
			this.VolumeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.VolumeLabel.AutoSize = true;
			this.VolumeLabel.Location = new System.Drawing.Point(904, 600);
			this.VolumeLabel.Name = "VolumeLabel";
			this.VolumeLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.VolumeLabel.Size = new System.Drawing.Size(45, 13);
			this.VolumeLabel.TabIndex = 210;
			this.VolumeLabel.Text = "Volume:";
			// 
			// TextXmlTabControl
			// 
			this.TextXmlTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextXmlTabControl.Controls.Add(this.SandBoxTabPage);
			this.TextXmlTabControl.Controls.Add(this.MessagesTabPage);
			this.TextXmlTabControl.Controls.Add(this.SapiTabPage);
			this.TextXmlTabControl.Controls.Add(this.PlayListTabPage);
			this.TextXmlTabControl.ImageList = this.TabsImageList;
			this.TextXmlTabControl.Location = new System.Drawing.Point(6, 336);
			this.TextXmlTabControl.Name = "TextXmlTabControl";
			this.TextXmlTabControl.Padding = new System.Drawing.Point(6, 5);
			this.TextXmlTabControl.SelectedIndex = 0;
			this.TextXmlTabControl.Size = new System.Drawing.Size(614, 309);
			this.TextXmlTabControl.TabIndex = 3;
			this.TextXmlTabControl.SelectedIndexChanged += new System.EventHandler(this.TextXmlTabControl_SelectedIndexChanged);
			// 
			// SandBoxTabPage
			// 
			this.SandBoxTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.SandBoxTabPage.Controls.Add(this.SandBoxTextBox);
			this.SandBoxTabPage.ImageKey = "Code_Edit.png";
			this.SandBoxTabPage.Location = new System.Drawing.Point(4, 27);
			this.SandBoxTabPage.Name = "SandBoxTabPage";
			this.SandBoxTabPage.Size = new System.Drawing.Size(606, 278);
			this.SandBoxTabPage.TabIndex = 7;
			this.SandBoxTabPage.Text = "SandBox";
			// 
			// SandBoxTextBox
			// 
			this.SandBoxTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.SandBoxTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SandBoxTextBox.Location = new System.Drawing.Point(0, 0);
			this.SandBoxTextBox.Multiline = true;
			this.SandBoxTextBox.Name = "SandBoxTextBox";
			this.SandBoxTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.SandBoxTextBox.Size = new System.Drawing.Size(606, 278);
			this.SandBoxTextBox.TabIndex = 0;
			this.SandBoxTextBox.Text = resources.GetString("SandBoxTextBox.Text");
			this.SandBoxTextBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.SandBoxTextBox.MouseHover += new System.EventHandler(this.MouseHover_SandBox);
			// 
			// MessagesTabPage
			// 
			this.MessagesTabPage.Controls.Add(this.MessagesDataGridView);
			this.MessagesTabPage.ImageKey = "Message_Incoming.png";
			this.MessagesTabPage.Location = new System.Drawing.Point(4, 27);
			this.MessagesTabPage.Name = "MessagesTabPage";
			this.MessagesTabPage.Size = new System.Drawing.Size(606, 278);
			this.MessagesTabPage.TabIndex = 4;
			this.MessagesTabPage.Text = "1. Incoming Messages";
			// 
			// SapiTabPage
			// 
			this.SapiTabPage.Controls.Add(this.SapiTextBox);
			this.SapiTabPage.ImageKey = "Message_SAPI.png";
			this.SapiTabPage.Location = new System.Drawing.Point(4, 27);
			this.SapiTabPage.Name = "SapiTabPage";
			this.SapiTabPage.Size = new System.Drawing.Size(606, 278);
			this.SapiTabPage.TabIndex = 2;
			this.SapiTabPage.Text = "2. Formated SAPI XML Text";
			this.SapiTabPage.UseVisualStyleBackColor = true;
			// 
			// SapiTextBox
			// 
			this.SapiTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.SapiTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SapiTextBox.Location = new System.Drawing.Point(0, 0);
			this.SapiTextBox.Multiline = true;
			this.SapiTextBox.Name = "SapiTextBox";
			this.SapiTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.SapiTextBox.Size = new System.Drawing.Size(606, 278);
			this.SapiTextBox.TabIndex = 14;
			// 
			// PlayListTabPage
			// 
			this.PlayListTabPage.Controls.Add(this.PlayListDataGridView);
			this.PlayListTabPage.ImageKey = "Play_List.png";
			this.PlayListTabPage.Location = new System.Drawing.Point(4, 27);
			this.PlayListTabPage.Name = "PlayListTabPage";
			this.PlayListTabPage.Size = new System.Drawing.Size(606, 278);
			this.PlayListTabPage.TabIndex = 6;
			this.PlayListTabPage.Text = "3. Play List";
			// 
			// PlayListDataGridView
			// 
			this.PlayListDataGridView.AllowUserToAddRows = false;
			this.PlayListDataGridView.AllowUserToDeleteRows = false;
			this.PlayListDataGridView.AllowUserToResizeRows = false;
			this.PlayListDataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
			this.PlayListDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.PlayListDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.PlayListDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.PlayListDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.PlayListDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			this.PlayListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.PlayListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PlayListStatusColumn,
            this.CommentColumn,
            this.PlayListDurationColumn,
            this.PlayListTextColumn});
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.PlayListDataGridView.DefaultCellStyle = dataGridViewCellStyle10;
			this.PlayListDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PlayListDataGridView.EnableHeadersVisualStyles = false;
			this.PlayListDataGridView.GridColor = System.Drawing.SystemColors.Control;
			this.PlayListDataGridView.Location = new System.Drawing.Point(0, 0);
			this.PlayListDataGridView.MultiSelect = false;
			this.PlayListDataGridView.Name = "PlayListDataGridView";
			this.PlayListDataGridView.ReadOnly = true;
			this.PlayListDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.PlayListDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
			this.PlayListDataGridView.RowHeadersVisible = false;
			this.PlayListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.PlayListDataGridView.Size = new System.Drawing.Size(606, 278);
			this.PlayListDataGridView.TabIndex = 1;
			// 
			// PlayListStatusColumn
			// 
			this.PlayListStatusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.PlayListStatusColumn.DataPropertyName = "Status";
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.PlayListStatusColumn.DefaultCellStyle = dataGridViewCellStyle8;
			this.PlayListStatusColumn.HeaderText = "Status";
			this.PlayListStatusColumn.Name = "PlayListStatusColumn";
			this.PlayListStatusColumn.ReadOnly = true;
			this.PlayListStatusColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.PlayListStatusColumn.Width = 45;
			// 
			// CommentColumn
			// 
			this.CommentColumn.DataPropertyName = "IsComment";
			this.CommentColumn.HeaderText = "Comment";
			this.CommentColumn.Name = "CommentColumn";
			this.CommentColumn.ReadOnly = true;
			this.CommentColumn.Width = 64;
			// 
			// PlayListDurationColumn
			// 
			this.PlayListDurationColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
			this.PlayListDurationColumn.DataPropertyName = "Duration";
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.PlayListDurationColumn.DefaultCellStyle = dataGridViewCellStyle9;
			this.PlayListDurationColumn.HeaderText = "Duration";
			this.PlayListDurationColumn.MinimumWidth = 54;
			this.PlayListDurationColumn.Name = "PlayListDurationColumn";
			this.PlayListDurationColumn.ReadOnly = true;
			this.PlayListDurationColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.PlayListDurationColumn.Width = 54;
			// 
			// PlayListTextColumn
			// 
			this.PlayListTextColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.PlayListTextColumn.DataPropertyName = "Text";
			this.PlayListTextColumn.HeaderText = "Text";
			this.PlayListTextColumn.Name = "PlayListTextColumn";
			this.PlayListTextColumn.ReadOnly = true;
			this.PlayListTextColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// MainStatusStrip
			// 
			this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProcessStatusLabel,
            this.ErrorStatusLabel,
            this.FilterStatusLabel,
            this.EmptyStatusLabel,
            this.StateStatusLabel,
            this.PacketsStatusLabel});
			this.MainStatusStrip.Location = new System.Drawing.Point(0, 652);
			this.MainStatusStrip.Name = "MainStatusStrip";
			this.MainStatusStrip.Size = new System.Drawing.Size(1074, 24);
			this.MainStatusStrip.SizingGrip = false;
			this.MainStatusStrip.TabIndex = 17;
			this.MainStatusStrip.Text = "statusStrip1";
			// 
			// ProcessStatusLabel
			// 
			this.ProcessStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.ProcessStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.ProcessStatusLabel.Name = "ProcessStatusLabel";
			this.ProcessStatusLabel.Size = new System.Drawing.Size(108, 19);
			this.ProcessStatusLabel.Text = "Process: Unknown";
			// 
			// ErrorStatusLabel
			// 
			this.ErrorStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.ErrorStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.ErrorStatusLabel.Name = "ErrorStatusLabel";
			this.ErrorStatusLabel.Size = new System.Drawing.Size(71, 19);
			this.ErrorStatusLabel.Text = "Error: None";
			this.ErrorStatusLabel.Visible = false;
			this.ErrorStatusLabel.Click += new System.EventHandler(this.ErrorStatusLabel_Click);
			// 
			// FilterStatusLabel
			// 
			this.FilterStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.FilterStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.FilterStatusLabel.Name = "FilterStatusLabel";
			this.FilterStatusLabel.Size = new System.Drawing.Size(72, 19);
			this.FilterStatusLabel.Text = "Filter: None";
			this.FilterStatusLabel.Click += new System.EventHandler(this.FilterStatusLabel_Click);
			// 
			// EmptyStatusLabel
			// 
			this.EmptyStatusLabel.Name = "EmptyStatusLabel";
			this.EmptyStatusLabel.Size = new System.Drawing.Size(660, 19);
			this.EmptyStatusLabel.Spring = true;
			// 
			// StateStatusLabel
			// 
			this.StateStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.StateStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.StateStatusLabel.Name = "StateStatusLabel";
			this.StateStatusLabel.Size = new System.Drawing.Size(94, 19);
			this.StateStatusLabel.Text = "State: Unknown";
			// 
			// PacketsStatusLabel
			// 
			this.PacketsStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.PacketsStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.PacketsStatusLabel.Name = "PacketsStatusLabel";
			this.PacketsStatusLabel.Size = new System.Drawing.Size(125, 19);
			this.PacketsStatusLabel.Text = "Packets: 0 IPv4, 0 IPv6";
			// 
			// AudioBitsPerSampleComboBox
			// 
			this.AudioBitsPerSampleComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.AudioBitsPerSampleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.AudioBitsPerSampleComboBox.FormattingEnabled = true;
			this.AudioBitsPerSampleComboBox.Location = new System.Drawing.Point(954, 485);
			this.AudioBitsPerSampleComboBox.Name = "AudioBitsPerSampleComboBox";
			this.AudioBitsPerSampleComboBox.Size = new System.Drawing.Size(114, 21);
			this.AudioBitsPerSampleComboBox.TabIndex = 8;
			this.AudioBitsPerSampleComboBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.AudioBitsPerSampleComboBox.MouseHover += new System.EventHandler(this.MouseHover_AudioBitsPerSample);
			// 
			// AudioBitsPerSampleLabel
			// 
			this.AudioBitsPerSampleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.AudioBitsPerSampleLabel.AutoSize = true;
			this.AudioBitsPerSampleLabel.Location = new System.Drawing.Point(851, 488);
			this.AudioBitsPerSampleLabel.Name = "AudioBitsPerSampleLabel";
			this.AudioBitsPerSampleLabel.Size = new System.Drawing.Size(97, 13);
			this.AudioBitsPerSampleLabel.TabIndex = 205;
			this.AudioBitsPerSampleLabel.Text = "Audio Bits/Sample:";
			this.AudioBitsPerSampleLabel.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.AudioBitsPerSampleLabel.MouseHover += new System.EventHandler(this.MouseHover_AudioBitsPerSample);
			// 
			// AudioSampleRateComboBox
			// 
			this.AudioSampleRateComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.AudioSampleRateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.AudioSampleRateComboBox.FormattingEnabled = true;
			this.AudioSampleRateComboBox.Location = new System.Drawing.Point(954, 458);
			this.AudioSampleRateComboBox.Name = "AudioSampleRateComboBox";
			this.AudioSampleRateComboBox.Size = new System.Drawing.Size(114, 21);
			this.AudioSampleRateComboBox.TabIndex = 7;
			this.AudioSampleRateComboBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.AudioSampleRateComboBox.MouseHover += new System.EventHandler(this.MouseHover_AudioSampleRate);
			// 
			// AudioSampleRateLabel
			// 
			this.AudioSampleRateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.AudioSampleRateLabel.AutoSize = true;
			this.AudioSampleRateLabel.Location = new System.Drawing.Point(847, 461);
			this.AudioSampleRateLabel.Name = "AudioSampleRateLabel";
			this.AudioSampleRateLabel.Size = new System.Drawing.Size(101, 13);
			this.AudioSampleRateLabel.TabIndex = 204;
			this.AudioSampleRateLabel.Text = "Audio Sample Rate:";
			this.AudioSampleRateLabel.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.AudioSampleRateLabel.MouseHover += new System.EventHandler(this.MouseHover_AudioSampleRate);
			// 
			// AudioChannelsComboBox
			// 
			this.AudioChannelsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.AudioChannelsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.AudioChannelsComboBox.FormattingEnabled = true;
			this.AudioChannelsComboBox.Location = new System.Drawing.Point(954, 431);
			this.AudioChannelsComboBox.Name = "AudioChannelsComboBox";
			this.AudioChannelsComboBox.Size = new System.Drawing.Size(114, 21);
			this.AudioChannelsComboBox.TabIndex = 6;
			this.AudioChannelsComboBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.AudioChannelsComboBox.MouseHover += new System.EventHandler(this.MouseHover_AudioChannels);
			// 
			// AudioChannelsLabel
			// 
			this.AudioChannelsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.AudioChannelsLabel.AutoSize = true;
			this.AudioChannelsLabel.Location = new System.Drawing.Point(864, 434);
			this.AudioChannelsLabel.Name = "AudioChannelsLabel";
			this.AudioChannelsLabel.Size = new System.Drawing.Size(84, 13);
			this.AudioChannelsLabel.TabIndex = 203;
			this.AudioChannelsLabel.Text = "Audio Channels:";
			this.AudioChannelsLabel.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.AudioChannelsLabel.MouseHover += new System.EventHandler(this.MouseHover_AudioChannels);
			// 
			// RecognizeButton
			// 
			this.RecognizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RecognizeButton.Location = new System.Drawing.Point(737, 333);
			this.RecognizeButton.Name = "RecognizeButton";
			this.RecognizeButton.Size = new System.Drawing.Size(93, 23);
			this.RecognizeButton.TabIndex = 10;
			this.RecognizeButton.TabStop = false;
			this.RecognizeButton.Text = "Recognize";
			this.RecognizeButton.UseVisualStyleBackColor = true;
			this.RecognizeButton.Visible = false;
			this.RecognizeButton.Click += new System.EventHandler(this.RecognizeButton_Click);
			// 
			// MainHelpLabel
			// 
			this.MainHelpLabel.BackColor = System.Drawing.SystemColors.Info;
			this.MainHelpLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.MainHelpLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MainHelpLabel.Location = new System.Drawing.Point(0, 0);
			this.MainHelpLabel.Name = "MainHelpLabel";
			this.MainHelpLabel.Padding = new System.Windows.Forms.Padding(32, 7, 6, 6);
			this.MainHelpLabel.Size = new System.Drawing.Size(1074, 28);
			this.MainHelpLabel.TabIndex = 1;
			this.MainHelpLabel.Text = "MainHelpLabel";
			// 
			// MonitorClipboardLabel
			// 
			this.MonitorClipboardLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.MonitorClipboardLabel.AutoSize = true;
			this.MonitorClipboardLabel.Location = new System.Drawing.Point(856, 380);
			this.MonitorClipboardLabel.Name = "MonitorClipboardLabel";
			this.MonitorClipboardLabel.Size = new System.Drawing.Size(92, 13);
			this.MonitorClipboardLabel.TabIndex = 202;
			this.MonitorClipboardLabel.Text = "Monitor Clipboard:";
			this.MonitorClipboardLabel.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.MonitorClipboardLabel.MouseHover += new System.EventHandler(this.MouseHover_MonitorClipboardComboBox);
			// 
			// ProductPictureBox
			// 
			this.ProductPictureBox.BackColor = System.Drawing.SystemColors.Info;
			this.ProductPictureBox.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Information;
			this.ProductPictureBox.Location = new System.Drawing.Point(12, 6);
			this.ProductPictureBox.Name = "ProductPictureBox";
			this.ProductPictureBox.Size = new System.Drawing.Size(16, 16);
			this.ProductPictureBox.TabIndex = 22;
			this.ProductPictureBox.TabStop = false;
			// 
			// PitchTextBox
			// 
			this.PitchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.PitchTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.PitchTextBox.Enabled = false;
			this.PitchTextBox.Location = new System.Drawing.Point(999, 540);
			this.PitchTextBox.Name = "PitchTextBox";
			this.PitchTextBox.ReadOnly = true;
			this.PitchTextBox.Size = new System.Drawing.Size(23, 20);
			this.PitchTextBox.TabIndex = 11;
			this.PitchTextBox.TabStop = false;
			this.PitchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// RateTextBox
			// 
			this.RateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.RateTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.RateTextBox.Enabled = false;
			this.RateTextBox.Location = new System.Drawing.Point(999, 567);
			this.RateTextBox.Name = "RateTextBox";
			this.RateTextBox.Size = new System.Drawing.Size(23, 20);
			this.RateTextBox.TabIndex = 14;
			this.RateTextBox.TabStop = false;
			this.RateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// VolumeTrackBar
			// 
			this.VolumeTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.VolumeTrackBar.Location = new System.Drawing.Point(946, 588);
			this.VolumeTrackBar.Maximum = 100;
			this.VolumeTrackBar.Name = "VolumeTrackBar";
			this.VolumeTrackBar.Size = new System.Drawing.Size(130, 45);
			this.VolumeTrackBar.TabIndex = 16;
			this.VolumeTrackBar.TickFrequency = 5;
			this.VolumeTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.VolumeTrackBar.Value = 100;
			// 
			// StopButton
			// 
			this.StopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.StopButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Square_Glass_Red;
			this.StopButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.StopButton.Location = new System.Drawing.Point(953, 622);
			this.StopButton.Name = "StopButton";
			this.StopButton.Size = new System.Drawing.Size(116, 23);
			this.StopButton.TabIndex = 18;
			this.StopButton.Text = "   Stop";
			this.StopButton.UseVisualStyleBackColor = true;
			this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
			// 
			// SpeakButton
			// 
			this.SpeakButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.SpeakButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.SpeakButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Triangle_Glass_Blue;
			this.SpeakButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.SpeakButton.Location = new System.Drawing.Point(833, 622);
			this.SpeakButton.Name = "SpeakButton";
			this.SpeakButton.Size = new System.Drawing.Size(116, 23);
			this.SpeakButton.TabIndex = 17;
			this.SpeakButton.Text = "   Speak";
			this.SpeakButton.UseVisualStyleBackColor = true;
			this.SpeakButton.Click += new System.EventHandler(this.SpeakButton_Click);
			// 
			// VolumeTextBox
			// 
			this.VolumeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.VolumeTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.VolumeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.VolumeTextBox.Location = new System.Drawing.Point(875, 600);
			this.VolumeTextBox.Name = "VolumeTextBox";
			this.VolumeTextBox.ReadOnly = true;
			this.VolumeTextBox.Size = new System.Drawing.Size(27, 13);
			this.VolumeTextBox.TabIndex = 209;
			this.VolumeTextBox.TabStop = false;
			this.VolumeTextBox.Text = "100%";
			this.VolumeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// IncomingGroupBox
			// 
			this.IncomingGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.IncomingGroupBox.Controls.Add(this.InCommandLabel);
			this.IncomingGroupBox.Controls.Add(this.InVolumeLabel);
			this.IncomingGroupBox.Controls.Add(this.InRateLabel);
			this.IncomingGroupBox.Controls.Add(this.InPitchLabel);
			this.IncomingGroupBox.Controls.Add(this.InGroupLabel);
			this.IncomingGroupBox.Controls.Add(this.InEffectLabel);
			this.IncomingGroupBox.Controls.Add(this.InGenderLabel);
			this.IncomingGroupBox.Controls.Add(this.InNameLabel);
			this.IncomingGroupBox.Controls.Add(this.InLanguageLabel);
			this.IncomingGroupBox.Controls.Add(this.InPartLabel);
			this.IncomingGroupBox.Controls.Add(this.InLanguageTextBox);
			this.IncomingGroupBox.Controls.Add(this.InGroupTextBox);
			this.IncomingGroupBox.Controls.Add(this.InPartTextBox);
			this.IncomingGroupBox.Controls.Add(this.InCommandTextBox);
			this.IncomingGroupBox.Controls.Add(this.InVolumeTextBox);
			this.IncomingGroupBox.Controls.Add(this.InEffectTextBox);
			this.IncomingGroupBox.Controls.Add(this.InPitchTextBox);
			this.IncomingGroupBox.Controls.Add(this.InRateTextBox);
			this.IncomingGroupBox.Controls.Add(this.InGenderTextBox);
			this.IncomingGroupBox.Controls.Add(this.InNameTextBox);
			this.IncomingGroupBox.ForeColor = System.Drawing.SystemColors.GrayText;
			this.IncomingGroupBox.Location = new System.Drawing.Point(624, 362);
			this.IncomingGroupBox.Name = "IncomingGroupBox";
			this.IncomingGroupBox.Size = new System.Drawing.Size(204, 282);
			this.IncomingGroupBox.TabIndex = 100;
			this.IncomingGroupBox.TabStop = false;
			this.IncomingGroupBox.Text = "Incoming message values";
			this.IncomingGroupBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.IncomingGroupBox.MouseHover += new System.EventHandler(this.MouseHover_IncomingGroupBox);
			// 
			// InCommandLabel
			// 
			this.InCommandLabel.AutoSize = true;
			this.InCommandLabel.Location = new System.Drawing.Point(6, 196);
			this.InCommandLabel.Name = "InCommandLabel";
			this.InCommandLabel.Size = new System.Drawing.Size(53, 13);
			this.InCommandLabel.TabIndex = 111;
			this.InCommandLabel.Text = "command";
			// 
			// InVolumeLabel
			// 
			this.InVolumeLabel.AutoSize = true;
			this.InVolumeLabel.Location = new System.Drawing.Point(6, 174);
			this.InVolumeLabel.Name = "InVolumeLabel";
			this.InVolumeLabel.Size = new System.Drawing.Size(41, 13);
			this.InVolumeLabel.TabIndex = 111;
			this.InVolumeLabel.Text = "volume";
			// 
			// InRateLabel
			// 
			this.InRateLabel.AutoSize = true;
			this.InRateLabel.Location = new System.Drawing.Point(6, 152);
			this.InRateLabel.Name = "InRateLabel";
			this.InRateLabel.Size = new System.Drawing.Size(25, 13);
			this.InRateLabel.TabIndex = 111;
			this.InRateLabel.Text = "rate";
			// 
			// InPitchLabel
			// 
			this.InPitchLabel.AutoSize = true;
			this.InPitchLabel.Location = new System.Drawing.Point(6, 130);
			this.InPitchLabel.Name = "InPitchLabel";
			this.InPitchLabel.Size = new System.Drawing.Size(30, 13);
			this.InPitchLabel.TabIndex = 111;
			this.InPitchLabel.Text = "pitch";
			// 
			// InGroupLabel
			// 
			this.InGroupLabel.AutoSize = true;
			this.InGroupLabel.Location = new System.Drawing.Point(6, 108);
			this.InGroupLabel.Name = "InGroupLabel";
			this.InGroupLabel.Size = new System.Drawing.Size(34, 13);
			this.InGroupLabel.TabIndex = 111;
			this.InGroupLabel.Text = "group";
			// 
			// InEffectLabel
			// 
			this.InEffectLabel.AutoSize = true;
			this.InEffectLabel.Location = new System.Drawing.Point(6, 86);
			this.InEffectLabel.Name = "InEffectLabel";
			this.InEffectLabel.Size = new System.Drawing.Size(34, 13);
			this.InEffectLabel.TabIndex = 111;
			this.InEffectLabel.Text = "effect";
			// 
			// InGenderLabel
			// 
			this.InGenderLabel.AutoSize = true;
			this.InGenderLabel.Location = new System.Drawing.Point(6, 64);
			this.InGenderLabel.Name = "InGenderLabel";
			this.InGenderLabel.Size = new System.Drawing.Size(40, 13);
			this.InGenderLabel.TabIndex = 111;
			this.InGenderLabel.Text = "gender";
			// 
			// InNameLabel
			// 
			this.InNameLabel.AutoSize = true;
			this.InNameLabel.Location = new System.Drawing.Point(6, 42);
			this.InNameLabel.Name = "InNameLabel";
			this.InNameLabel.Size = new System.Drawing.Size(33, 13);
			this.InNameLabel.TabIndex = 111;
			this.InNameLabel.Text = "name";
			// 
			// InLanguageLabel
			// 
			this.InLanguageLabel.AutoSize = true;
			this.InLanguageLabel.Location = new System.Drawing.Point(6, 20);
			this.InLanguageLabel.Name = "InLanguageLabel";
			this.InLanguageLabel.Size = new System.Drawing.Size(51, 13);
			this.InLanguageLabel.TabIndex = 111;
			this.InLanguageLabel.Text = "language";
			// 
			// InPartLabel
			// 
			this.InPartLabel.AutoSize = true;
			this.InPartLabel.Location = new System.Drawing.Point(6, 218);
			this.InPartLabel.Name = "InPartLabel";
			this.InPartLabel.Size = new System.Drawing.Size(25, 13);
			this.InPartLabel.TabIndex = 111;
			this.InPartLabel.Text = "part";
			// 
			// InLanguageTextBox
			// 
			this.InLanguageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.InLanguageTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.InLanguageTextBox.Enabled = false;
			this.InLanguageTextBox.Location = new System.Drawing.Point(63, 17);
			this.InLanguageTextBox.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
			this.InLanguageTextBox.Multiline = true;
			this.InLanguageTextBox.Name = "InLanguageTextBox";
			this.InLanguageTextBox.ReadOnly = true;
			this.InLanguageTextBox.Size = new System.Drawing.Size(135, 20);
			this.InLanguageTextBox.TabIndex = 110;
			this.InLanguageTextBox.TabStop = false;
			// 
			// InGroupTextBox
			// 
			this.InGroupTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.InGroupTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.InGroupTextBox.Enabled = false;
			this.InGroupTextBox.Location = new System.Drawing.Point(63, 105);
			this.InGroupTextBox.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
			this.InGroupTextBox.Name = "InGroupTextBox";
			this.InGroupTextBox.ReadOnly = true;
			this.InGroupTextBox.Size = new System.Drawing.Size(135, 20);
			this.InGroupTextBox.TabIndex = 109;
			this.InGroupTextBox.TabStop = false;
			// 
			// InPartTextBox
			// 
			this.InPartTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.InPartTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.InPartTextBox.Enabled = false;
			this.InPartTextBox.Location = new System.Drawing.Point(63, 215);
			this.InPartTextBox.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
			this.InPartTextBox.Multiline = true;
			this.InPartTextBox.Name = "InPartTextBox";
			this.InPartTextBox.ReadOnly = true;
			this.InPartTextBox.Size = new System.Drawing.Size(135, 56);
			this.InPartTextBox.TabIndex = 101;
			this.InPartTextBox.TabStop = false;
			this.InPartTextBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.InPartTextBox.MouseHover += new System.EventHandler(this.MouseHover_IncomingGroupBox);
			// 
			// InCommandTextBox
			// 
			this.InCommandTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.InCommandTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.InCommandTextBox.Enabled = false;
			this.InCommandTextBox.Location = new System.Drawing.Point(63, 193);
			this.InCommandTextBox.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
			this.InCommandTextBox.Name = "InCommandTextBox";
			this.InCommandTextBox.ReadOnly = true;
			this.InCommandTextBox.Size = new System.Drawing.Size(135, 20);
			this.InCommandTextBox.TabIndex = 108;
			this.InCommandTextBox.TabStop = false;
			this.InCommandTextBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.InCommandTextBox.MouseHover += new System.EventHandler(this.MouseHover_IncomingGroupBox);
			// 
			// InVolumeTextBox
			// 
			this.InVolumeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.InVolumeTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.InVolumeTextBox.Enabled = false;
			this.InVolumeTextBox.Location = new System.Drawing.Point(63, 171);
			this.InVolumeTextBox.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
			this.InVolumeTextBox.Name = "InVolumeTextBox";
			this.InVolumeTextBox.ReadOnly = true;
			this.InVolumeTextBox.Size = new System.Drawing.Size(135, 20);
			this.InVolumeTextBox.TabIndex = 107;
			this.InVolumeTextBox.TabStop = false;
			this.InVolumeTextBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.InVolumeTextBox.MouseHover += new System.EventHandler(this.MouseHover_IncomingGroupBox);
			// 
			// InEffectTextBox
			// 
			this.InEffectTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.InEffectTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.InEffectTextBox.Enabled = false;
			this.InEffectTextBox.Location = new System.Drawing.Point(63, 83);
			this.InEffectTextBox.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
			this.InEffectTextBox.Name = "InEffectTextBox";
			this.InEffectTextBox.ReadOnly = true;
			this.InEffectTextBox.Size = new System.Drawing.Size(135, 20);
			this.InEffectTextBox.TabIndex = 104;
			this.InEffectTextBox.TabStop = false;
			this.InEffectTextBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.InEffectTextBox.MouseHover += new System.EventHandler(this.MouseHover_IncomingGroupBox);
			// 
			// InPitchTextBox
			// 
			this.InPitchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.InPitchTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.InPitchTextBox.Enabled = false;
			this.InPitchTextBox.Location = new System.Drawing.Point(63, 127);
			this.InPitchTextBox.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
			this.InPitchTextBox.Name = "InPitchTextBox";
			this.InPitchTextBox.ReadOnly = true;
			this.InPitchTextBox.Size = new System.Drawing.Size(135, 20);
			this.InPitchTextBox.TabIndex = 105;
			this.InPitchTextBox.TabStop = false;
			this.InPitchTextBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.InPitchTextBox.MouseHover += new System.EventHandler(this.MouseHover_IncomingGroupBox);
			// 
			// InRateTextBox
			// 
			this.InRateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.InRateTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.InRateTextBox.Enabled = false;
			this.InRateTextBox.Location = new System.Drawing.Point(63, 149);
			this.InRateTextBox.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
			this.InRateTextBox.Name = "InRateTextBox";
			this.InRateTextBox.ReadOnly = true;
			this.InRateTextBox.Size = new System.Drawing.Size(135, 20);
			this.InRateTextBox.TabIndex = 106;
			this.InRateTextBox.TabStop = false;
			this.InRateTextBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.InRateTextBox.MouseHover += new System.EventHandler(this.MouseHover_IncomingGroupBox);
			// 
			// InGenderTextBox
			// 
			this.InGenderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.InGenderTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.InGenderTextBox.Enabled = false;
			this.InGenderTextBox.Location = new System.Drawing.Point(63, 61);
			this.InGenderTextBox.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
			this.InGenderTextBox.Name = "InGenderTextBox";
			this.InGenderTextBox.ReadOnly = true;
			this.InGenderTextBox.Size = new System.Drawing.Size(135, 20);
			this.InGenderTextBox.TabIndex = 103;
			this.InGenderTextBox.TabStop = false;
			this.InGenderTextBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.InGenderTextBox.MouseHover += new System.EventHandler(this.MouseHover_IncomingGroupBox);
			// 
			// InNameTextBox
			// 
			this.InNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.InNameTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.InNameTextBox.Enabled = false;
			this.InNameTextBox.Location = new System.Drawing.Point(63, 39);
			this.InNameTextBox.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
			this.InNameTextBox.Name = "InNameTextBox";
			this.InNameTextBox.ReadOnly = true;
			this.InNameTextBox.Size = new System.Drawing.Size(135, 20);
			this.InNameTextBox.TabIndex = 102;
			this.InNameTextBox.TabStop = false;
			this.InNameTextBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.InNameTextBox.MouseHover += new System.EventHandler(this.MouseHover_IncomingGroupBox);
			// 
			// DefaultGenderLabel
			// 
			this.DefaultGenderLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DefaultGenderLabel.AutoSize = true;
			this.DefaultGenderLabel.Location = new System.Drawing.Point(843, 515);
			this.DefaultGenderLabel.Name = "DefaultGenderLabel";
			this.DefaultGenderLabel.Size = new System.Drawing.Size(105, 13);
			this.DefaultGenderLabel.TabIndex = 206;
			this.DefaultGenderLabel.Text = "Default · My Gender:";
			this.DefaultGenderLabel.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.DefaultGenderLabel.MouseHover += new System.EventHandler(this.MouseHover_GenderComboBox);
			// 
			// DefaultIntroSoundLabel
			// 
			this.DefaultIntroSoundLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DefaultIntroSoundLabel.AutoSize = true;
			this.DefaultIntroSoundLabel.Location = new System.Drawing.Point(846, 407);
			this.DefaultIntroSoundLabel.Name = "DefaultIntroSoundLabel";
			this.DefaultIntroSoundLabel.Size = new System.Drawing.Size(102, 13);
			this.DefaultIntroSoundLabel.TabIndex = 213;
			this.DefaultIntroSoundLabel.Text = "Default Intro Sound:";
			// 
			// DefaultIntroSoundComboBox
			// 
			this.DefaultIntroSoundComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DefaultIntroSoundComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DefaultIntroSoundComboBox.FormattingEnabled = true;
			this.DefaultIntroSoundComboBox.Location = new System.Drawing.Point(954, 404);
			this.DefaultIntroSoundComboBox.Name = "DefaultIntroSoundComboBox";
			this.DefaultIntroSoundComboBox.Size = new System.Drawing.Size(114, 21);
			this.DefaultIntroSoundComboBox.TabIndex = 212;
			// 
			// MonitorsEnabledCheckBox
			// 
			this.MonitorsEnabledCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.MonitorsEnabledCheckBox.AutoSize = true;
			this.MonitorsEnabledCheckBox.Checked = true;
			this.MonitorsEnabledCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.MonitorsEnabledCheckBox.Location = new System.Drawing.Point(887, 352);
			this.MonitorsEnabledCheckBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.MonitorsEnabledCheckBox.Name = "MonitorsEnabledCheckBox";
			this.MonitorsEnabledCheckBox.Size = new System.Drawing.Size(64, 17);
			this.MonitorsEnabledCheckBox.TabIndex = 211;
			this.MonitorsEnabledCheckBox.Text = "Monitor:";
			this.MonitorsEnabledCheckBox.UseVisualStyleBackColor = true;
			this.MonitorsEnabledCheckBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.MonitorsEnabledCheckBox.MouseHover += new System.EventHandler(this.EnableMonitorsCheckBox_MouseEnter);
			// 
			// MonitorClipboardComboBox
			// 
			this.MonitorClipboardComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.MonitorClipboardComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.MonitorClipboardComboBox.FormattingEnabled = true;
			this.MonitorClipboardComboBox.Location = new System.Drawing.Point(954, 377);
			this.MonitorClipboardComboBox.Name = "MonitorClipboardComboBox";
			this.MonitorClipboardComboBox.Size = new System.Drawing.Size(114, 21);
			this.MonitorClipboardComboBox.TabIndex = 5;
			this.MonitorClipboardComboBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.MonitorClipboardComboBox.MouseHover += new System.EventHandler(this.MouseHover_MonitorClipboardComboBox);
			// 
			// GenderComboBox
			// 
			this.GenderComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.GenderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.GenderComboBox.FormattingEnabled = true;
			this.GenderComboBox.Location = new System.Drawing.Point(954, 512);
			this.GenderComboBox.Name = "GenderComboBox";
			this.GenderComboBox.Size = new System.Drawing.Size(114, 21);
			this.GenderComboBox.TabIndex = 9;
			this.GenderComboBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.GenderComboBox.MouseHover += new System.EventHandler(this.MouseHover_GenderComboBox);
			// 
			// PitchMaxComboBox
			// 
			this.PitchMaxComboBox.AccessibleDescription = "";
			this.PitchMaxComboBox.AccessibleName = "";
			this.PitchMaxComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.PitchMaxComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PitchMaxComboBox.FormattingEnabled = true;
			this.PitchMaxComboBox.Location = new System.Drawing.Point(1028, 539);
			this.PitchMaxComboBox.Name = "PitchMaxComboBox";
			this.PitchMaxComboBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.PitchMaxComboBox.Size = new System.Drawing.Size(40, 21);
			this.PitchMaxComboBox.TabIndex = 12;
			this.PitchMaxComboBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.PitchMaxComboBox.MouseHover += new System.EventHandler(this.MouseHover_PitchMax);
			// 
			// RateMaxComboBox
			// 
			this.RateMaxComboBox.AccessibleDescription = "";
			this.RateMaxComboBox.AccessibleName = "";
			this.RateMaxComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.RateMaxComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RateMaxComboBox.FormattingEnabled = true;
			this.RateMaxComboBox.Location = new System.Drawing.Point(1028, 566);
			this.RateMaxComboBox.Name = "RateMaxComboBox";
			this.RateMaxComboBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.RateMaxComboBox.Size = new System.Drawing.Size(40, 21);
			this.RateMaxComboBox.TabIndex = 15;
			this.RateMaxComboBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.RateMaxComboBox.MouseHover += new System.EventHandler(this.MouseHover_RateMax);
			// 
			// PitchMinComboBox
			// 
			this.PitchMinComboBox.AccessibleDescription = "";
			this.PitchMinComboBox.AccessibleName = "";
			this.PitchMinComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.PitchMinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PitchMinComboBox.FormattingEnabled = true;
			this.PitchMinComboBox.Location = new System.Drawing.Point(954, 539);
			this.PitchMinComboBox.Name = "PitchMinComboBox";
			this.PitchMinComboBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.PitchMinComboBox.Size = new System.Drawing.Size(40, 21);
			this.PitchMinComboBox.TabIndex = 10;
			this.PitchMinComboBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.PitchMinComboBox.MouseHover += new System.EventHandler(this.MouseHover_PitchMin);
			// 
			// RateMinComboBox
			// 
			this.RateMinComboBox.AccessibleDescription = "";
			this.RateMinComboBox.AccessibleName = "";
			this.RateMinComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.RateMinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RateMinComboBox.FormattingEnabled = true;
			this.RateMinComboBox.Location = new System.Drawing.Point(954, 566);
			this.RateMinComboBox.Name = "RateMinComboBox";
			this.RateMinComboBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.RateMinComboBox.Size = new System.Drawing.Size(40, 21);
			this.RateMinComboBox.TabIndex = 13;
			this.RateMinComboBox.MouseLeave += new System.EventHandler(this.MouseLeave_MainHelpLabel);
			this.RateMinComboBox.MouseHover += new System.EventHandler(this.MouseHover_RateMin);
			// 
			// ProgramComboBox
			// 
			this.ProgramComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ProgramComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ProgramComboBox.FormattingEnabled = true;
			this.ProgramComboBox.Items.AddRange(new object[] {
            "Disabled",
            "For <message> tags",
            "For all text"});
			this.ProgramComboBox.Location = new System.Drawing.Point(953, 350);
			this.ProgramComboBox.Name = "ProgramComboBox";
			this.ProgramComboBox.Size = new System.Drawing.Size(114, 21);
			this.ProgramComboBox.TabIndex = 214;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1074, 676);
			this.Controls.Add(this.MonitorClipboardComboBox);
			this.Controls.Add(this.ProgramComboBox);
			this.Controls.Add(this.DefaultIntroSoundLabel);
			this.Controls.Add(this.DefaultIntroSoundComboBox);
			this.Controls.Add(this.MonitorsEnabledCheckBox);
			this.Controls.Add(this.DefaultGenderLabel);
			this.Controls.Add(this.GenderComboBox);
			this.Controls.Add(this.IncomingGroupBox);
			this.Controls.Add(this.RecognizeButton);
			this.Controls.Add(this.PitchMaxComboBox);
			this.Controls.Add(this.RateMaxComboBox);
			this.Controls.Add(this.EffectTabControl);
			this.Controls.Add(this.VolumeTextBox);
			this.Controls.Add(this.MonitorClipboardLabel);
			this.Controls.Add(this.SpeakButton);
			this.Controls.Add(this.StopButton);
			this.Controls.Add(this.VolumeTrackBar);
			this.Controls.Add(this.RateTextBox);
			this.Controls.Add(this.PitchMinComboBox);
			this.Controls.Add(this.PitchTextBox);
			this.Controls.Add(this.ProductPictureBox);
			this.Controls.Add(this.RateMinComboBox);
			this.Controls.Add(this.MainHelpLabel);
			this.Controls.Add(this.AudioChannelsComboBox);
			this.Controls.Add(this.MainStatusStrip);
			this.Controls.Add(this.AudioBitsPerSampleComboBox);
			this.Controls.Add(this.TextXmlTabControl);
			this.Controls.Add(this.MessagesTabControl);
			this.Controls.Add(this.AudioSampleRateComboBox);
			this.Controls.Add(this.RateLabel);
			this.Controls.Add(this.AudioBitsPerSampleLabel);
			this.Controls.Add(this.VolumeLabel);
			this.Controls.Add(this.AudioSampleRateLabel);
			this.Controls.Add(this.PitchLabel);
			this.Controls.Add(this.AudioChannelsLabel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(1090, 714);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.MessagesTabControl.ResumeLayout(false);
			this.VoicesTabPage.ResumeLayout(false);
			this.VoiceDefaultsTabPage.ResumeLayout(false);
			this.SoundsTabPage.ResumeLayout(false);
			this.AcronymsTabPage.ResumeLayout(false);
			this.EffectsPresetsEditorTabPage.ResumeLayout(false);
			this.OptionsTabPage.ResumeLayout(false);
			this.HelpTabPage.ResumeLayout(false);
			this.UpdateTabPage.ResumeLayout(false);
			this.EffectTabControl.ResumeLayout(false);
			this.EffectPresetsContextMenuStrip.ResumeLayout(false);
			this.EffectTabPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.EffectsPresetsDataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.MessagesDataGridView)).EndInit();
			this.MessagesContextMenuStrip.ResumeLayout(false);
			this.TextXmlTabControl.ResumeLayout(false);
			this.SandBoxTabPage.ResumeLayout(false);
			this.SandBoxTabPage.PerformLayout();
			this.MessagesTabPage.ResumeLayout(false);
			this.SapiTabPage.ResumeLayout(false);
			this.SapiTabPage.PerformLayout();
			this.PlayListTabPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PlayListDataGridView)).EndInit();
			this.MainStatusStrip.ResumeLayout(false);
			this.MainStatusStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ProductPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.VolumeTrackBar)).EndInit();
			this.IncomingGroupBox.ResumeLayout(false);
			this.IncomingGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
		private System.Windows.Forms.DataGridView MessagesDataGridView;
		private System.Windows.Forms.Label RateLabel;
		private System.Windows.Forms.Label PitchLabel;
        private System.Windows.Forms.Label VolumeLabel;
        private System.Windows.Forms.TabControl TextXmlTabControl;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel ErrorStatusLabel;
        private System.Windows.Forms.TabPage SapiTabPage;
        private System.Windows.Forms.TextBox SapiTextBox;
        private System.Windows.Forms.ContextMenuStrip MessagesContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MessagesClearToolStripMenuItem;
        private System.Windows.Forms.TabPage EffectsPresetsEditorTabPage;
        private Controls.SoundEffectsControl EffectPresetsEditorSoundEffectsControl;
        private System.Windows.Forms.TabControl EffectTabControl;
        private System.Windows.Forms.TabPage EffectTabPage;
        private System.Windows.Forms.DataGridView EffectsPresetsDataGridView;
        private System.Windows.Forms.ContextMenuStrip EffectPresetsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel StateStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel PacketsStatusLabel;
        private System.Windows.Forms.ComboBox AudioBitsPerSampleComboBox;
        private System.Windows.Forms.Label AudioBitsPerSampleLabel;
        private System.Windows.Forms.ComboBox AudioSampleRateComboBox;
        private System.Windows.Forms.Label AudioSampleRateLabel;
        private System.Windows.Forms.ComboBox AudioChannelsComboBox;
        private System.Windows.Forms.Label AudioChannelsLabel;
        private System.Windows.Forms.DataGridView PlayListDataGridView;
        private System.Windows.Forms.Button RecognizeButton;
        private System.Windows.Forms.ImageList TabsImageList;
        private System.Windows.Forms.TabPage VoicesTabPage;
        private System.Windows.Forms.TabPage MessagesTabPage;
        private System.Windows.Forms.TabPage PlayListTabPage;
        private System.Windows.Forms.ComboBox RateMinComboBox;
        private System.Windows.Forms.PictureBox ProductPictureBox;
        private System.Windows.Forms.TabPage HelpTabPage;
        private System.Windows.Forms.Label MonitorClipboardLabel;
        private System.Windows.Forms.TabPage SandBoxTabPage;
        private System.Windows.Forms.TextBox SandBoxTextBox;
        private System.Windows.Forms.TextBox PitchTextBox;
        private System.Windows.Forms.ComboBox PitchMinComboBox;
        private System.Windows.Forms.TextBox RateTextBox;
        private System.Windows.Forms.TrackBar VolumeTrackBar;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button SpeakButton;
        private System.Windows.Forms.TextBox VolumeTextBox;
        private System.Windows.Forms.RichTextBox AboutRichTextBox;
        private System.Windows.Forms.ComboBox RateMaxComboBox;
        private System.Windows.Forms.ComboBox PitchMaxComboBox;
        private System.Windows.Forms.GroupBox IncomingGroupBox;
        private System.Windows.Forms.TextBox InVolumeTextBox;
        private System.Windows.Forms.TextBox InEffectTextBox;
        private System.Windows.Forms.TextBox InPitchTextBox;
        private System.Windows.Forms.TextBox InRateTextBox;
        private System.Windows.Forms.TextBox InGenderTextBox;
        private System.Windows.Forms.TextBox InNameTextBox;
        private System.Windows.Forms.TextBox InCommandTextBox;
        private System.Windows.Forms.TextBox InPartTextBox;
        private System.Windows.Forms.ComboBox GenderComboBox;
        private System.Windows.Forms.Label DefaultGenderLabel;
        private System.Windows.Forms.ComboBox MonitorClipboardComboBox;
        private System.Windows.Forms.Label VoiceErrorLabel;
        private System.Windows.Forms.TabPage UpdateTabPage;
        private System.Windows.Forms.WebBrowser UpdateWebBrowser;
        private System.Windows.Forms.Label UpdateLabel;
        private System.Windows.Forms.Button UpdateButton;
		private System.Windows.Forms.TabPage VoiceDefaultsTabPage;
		private Controls.VoicesDefaultsUserControl VoiceDefaultsPanel;
        private Controls.SoundsUserControl SoundsPanel;
        public System.Windows.Forms.Label MainHelpLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlayListStatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CommentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlayListDurationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlayListTextColumn;
        private System.Windows.Forms.TextBox InGroupTextBox;
		private System.Windows.Forms.CheckBox MonitorsEnabledCheckBox;
        private System.Windows.Forms.TextBox InLanguageTextBox;
        private System.Windows.Forms.TabPage SoundsTabPage;
        private System.Windows.Forms.ComboBox DefaultIntroSoundComboBox;
        private System.Windows.Forms.Label DefaultIntroSoundLabel;
		private System.Windows.Forms.ComboBox ProgramComboBox;
		private System.Windows.Forms.ToolStripStatusLabel ProcessStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel EmptyStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel FilterStatusLabel;
		private System.Windows.Forms.TabPage AcronymsTabPage;
		private Controls.AcronymsUserControl acronymsUserControl1;
		private System.Windows.Forms.DataGridViewTextBoxColumn DestinationAddressColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn SequenceNumberColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn WowDataLength;
		private System.Windows.Forms.DataGridViewTextBoxColumn VoiceXmlColumn;
		private System.Windows.Forms.Label InPartLabel;
		private System.Windows.Forms.Label InCommandLabel;
		private System.Windows.Forms.Label InVolumeLabel;
		private System.Windows.Forms.Label InRateLabel;
		private System.Windows.Forms.Label InPitchLabel;
		private System.Windows.Forms.Label InGroupLabel;
		private System.Windows.Forms.Label InEffectLabel;
		private System.Windows.Forms.Label InGenderLabel;
		private System.Windows.Forms.Label InNameLabel;
		private System.Windows.Forms.Label InLanguageLabel;
		private Controls.VoicesUserControl VoicesPanel;
		public System.Windows.Forms.TabControl MessagesTabControl;
		public System.Windows.Forms.TabPage OptionsTabPage;
		public Controls.OptionsControl OptionsPanel;
	}
}

