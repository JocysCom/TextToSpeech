namespace JocysCom.TextToSpeech.Monitor.Controls
{
	partial class OptionsControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.AddSilenceGroupBox = new System.Windows.Forms.GroupBox();
            this.AddSilenceAfterLabel = new System.Windows.Forms.Label();
            this.AddSilenceBeforeLabel = new System.Windows.Forms.Label();
            this.AddSilenceAfterNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.AddSilcenceBeforeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.SilenceBeforeTagLabel = new System.Windows.Forms.Label();
            this.SilenceAfterTagLabel = new System.Windows.Forms.Label();
            this.LoggingGroupBox = new System.Windows.Forms.GroupBox();
            this.LoggingDefaultButton = new System.Windows.Forms.Button();
            this.LoggingFolderTextBox = new System.Windows.Forms.TextBox();
            this.LoggingButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LoggingLabel2 = new System.Windows.Forms.Label();
            this.LoggingLabel1 = new System.Windows.Forms.Label();
            this.LoggingTextBox = new System.Windows.Forms.TextBox();
            this.LoggingCheckBox = new System.Windows.Forms.CheckBox();
            this.AddSilenceGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddSilenceAfterNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddSilcenceBeforeNumericUpDown)).BeginInit();
            this.LoggingGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddSilenceGroupBox
            // 
            this.AddSilenceGroupBox.Controls.Add(this.AddSilenceAfterLabel);
            this.AddSilenceGroupBox.Controls.Add(this.AddSilenceBeforeLabel);
            this.AddSilenceGroupBox.Location = new System.Drawing.Point(3, 3);
            this.AddSilenceGroupBox.Name = "AddSilenceGroupBox";
            this.AddSilenceGroupBox.Size = new System.Drawing.Size(406, 79);
            this.AddSilenceGroupBox.TabIndex = 0;
            this.AddSilenceGroupBox.TabStop = false;
            this.AddSilenceGroupBox.Text = "Silence";
            // 
            // AddSilenceAfterLabel
            // 
            this.AddSilenceAfterLabel.AutoSize = true;
            this.AddSilenceAfterLabel.Location = new System.Drawing.Point(15, 48);
            this.AddSilenceAfterLabel.Name = "AddSilenceAfterLabel";
            this.AddSilenceAfterLabel.Size = new System.Drawing.Size(261, 13);
            this.AddSilenceAfterLabel.TabIndex = 6;
            this.AddSilenceAfterLabel.Text = "Add Silence After Message ( default value is 0 ) [ ms ]:";
            // 
            // AddSilenceBeforeLabel
            // 
            this.AddSilenceBeforeLabel.AutoSize = true;
            this.AddSilenceBeforeLabel.Location = new System.Drawing.Point(6, 22);
            this.AddSilenceBeforeLabel.Name = "AddSilenceBeforeLabel";
            this.AddSilenceBeforeLabel.Size = new System.Drawing.Size(270, 13);
            this.AddSilenceBeforeLabel.TabIndex = 6;
            this.AddSilenceBeforeLabel.Text = "Add Silence Before Message ( default value is 0 ) [ ms ]:";
            // 
            // AddSilenceAfterNumericUpDown
            // 
            this.AddSilenceAfterNumericUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::JocysCom.TextToSpeech.Monitor.Properties.Settings.Default, "DelayBeforeValue", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.AddSilenceAfterNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.AddSilenceAfterNumericUpDown.Location = new System.Drawing.Point(282, 48);
            this.AddSilenceAfterNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.AddSilenceAfterNumericUpDown.Name = "AddSilenceAfterNumericUpDown";
            this.AddSilenceAfterNumericUpDown.Size = new System.Drawing.Size(114, 20);
            this.AddSilenceAfterNumericUpDown.TabIndex = 5;
            this.AddSilenceAfterNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AddSilenceAfterNumericUpDown.Value = global::JocysCom.TextToSpeech.Monitor.Properties.Settings.Default.DelayBeforeValue;
            this.AddSilenceAfterNumericUpDown.ValueChanged += new System.EventHandler(this.AddSilenceAfterNumericUpDown_ValueChanged);
            // 
            // AddSilcenceBeforeNumericUpDown
            // 
            this.AddSilcenceBeforeNumericUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::JocysCom.TextToSpeech.Monitor.Properties.Settings.Default, "AddSilcenceBeforeMessage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.AddSilcenceBeforeNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.AddSilcenceBeforeNumericUpDown.Location = new System.Drawing.Point(282, 22);
            this.AddSilcenceBeforeNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.AddSilcenceBeforeNumericUpDown.Name = "AddSilcenceBeforeNumericUpDown";
            this.AddSilcenceBeforeNumericUpDown.Size = new System.Drawing.Size(114, 20);
            this.AddSilcenceBeforeNumericUpDown.TabIndex = 5;
            this.AddSilcenceBeforeNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AddSilcenceBeforeNumericUpDown.Value = global::JocysCom.TextToSpeech.Monitor.Properties.Settings.Default.AddSilcenceBeforeMessage;
            this.AddSilcenceBeforeNumericUpDown.ValueChanged += new System.EventHandler(this.AddSilcenceBeforeNumericUpDown_ValueChanged);
            // 
            // SilenceBeforeTagLabel
            // 
            this.SilenceBeforeTagLabel.AutoSize = true;
            this.SilenceBeforeTagLabel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SilenceBeforeTagLabel.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.SilenceBeforeTagLabel.Location = new System.Drawing.Point(417, 25);
            this.SilenceBeforeTagLabel.Name = "SilenceBeforeTagLabel";
            this.SilenceBeforeTagLabel.Size = new System.Drawing.Size(161, 14);
            this.SilenceBeforeTagLabel.TabIndex = 7;
            this.SilenceBeforeTagLabel.Text = "<silence msec=\"3000\"/>";
            // 
            // SilenceAfterTagLabel
            // 
            this.SilenceAfterTagLabel.AutoSize = true;
            this.SilenceAfterTagLabel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SilenceAfterTagLabel.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.SilenceAfterTagLabel.Location = new System.Drawing.Point(417, 51);
            this.SilenceAfterTagLabel.Name = "SilenceAfterTagLabel";
            this.SilenceAfterTagLabel.Size = new System.Drawing.Size(161, 14);
            this.SilenceAfterTagLabel.TabIndex = 8;
            this.SilenceAfterTagLabel.Text = "<silence msec=\"3000\"/>";
            // 
            // LoggingGroupBox
            // 
            this.LoggingGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoggingGroupBox.Controls.Add(this.LoggingDefaultButton);
            this.LoggingGroupBox.Controls.Add(this.LoggingFolderTextBox);
            this.LoggingGroupBox.Controls.Add(this.LoggingButton);
            this.LoggingGroupBox.Controls.Add(this.label1);
            this.LoggingGroupBox.Controls.Add(this.LoggingLabel2);
            this.LoggingGroupBox.Controls.Add(this.LoggingLabel1);
            this.LoggingGroupBox.Controls.Add(this.LoggingTextBox);
            this.LoggingGroupBox.Controls.Add(this.LoggingCheckBox);
            this.LoggingGroupBox.Location = new System.Drawing.Point(4, 89);
            this.LoggingGroupBox.Name = "LoggingGroupBox";
            this.LoggingGroupBox.Size = new System.Drawing.Size(632, 342);
            this.LoggingGroupBox.TabIndex = 9;
            this.LoggingGroupBox.TabStop = false;
            this.LoggingGroupBox.Text = "Logging ( incomplete - not working )";
            // 
            // LoggingDefaultButton
            // 
            this.LoggingDefaultButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoggingDefaultButton.Location = new System.Drawing.Point(551, 44);
            this.LoggingDefaultButton.Name = "LoggingDefaultButton";
            this.LoggingDefaultButton.Size = new System.Drawing.Size(75, 23);
            this.LoggingDefaultButton.TabIndex = 9;
            this.LoggingDefaultButton.Text = "Default";
            this.LoggingDefaultButton.UseVisualStyleBackColor = true;
            this.LoggingDefaultButton.Click += new System.EventHandler(this.LoggingDefaultButton_Click);
            // 
            // LoggingFolderTextBox
            // 
            this.LoggingFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoggingFolderTextBox.Location = new System.Drawing.Point(6, 46);
            this.LoggingFolderTextBox.Name = "LoggingFolderTextBox";
            this.LoggingFolderTextBox.Size = new System.Drawing.Size(444, 20);
            this.LoggingFolderTextBox.TabIndex = 7;
            // 
            // LoggingButton
            // 
            this.LoggingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoggingButton.Location = new System.Drawing.Point(456, 44);
            this.LoggingButton.Name = "LoggingButton";
            this.LoggingButton.Size = new System.Drawing.Size(89, 23);
            this.LoggingButton.TabIndex = 5;
            this.LoggingButton.Text = "Browse...";
            this.LoggingButton.UseVisualStyleBackColor = true;
            this.LoggingButton.Click += new System.EventHandler(this.LoggingButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(7, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(606, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "3. Information about founded packets with specified text ( for example: me66age )" +
    " will be logged to JocysComMonitorLog.txt file.";
            // 
            // LoggingLabel2
            // 
            this.LoggingLabel2.AutoSize = true;
            this.LoggingLabel2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.LoggingLabel2.Location = new System.Drawing.Point(7, 92);
            this.LoggingLabel2.Name = "LoggingLabel2";
            this.LoggingLabel2.Size = new System.Drawing.Size(471, 13);
            this.LoggingLabel2.TabIndex = 3;
            this.LoggingLabel2.Text = "2. Enter and send specified text message ( for example: me66age )  through game o" +
    "r program chat.";
            // 
            // LoggingLabel1
            // 
            this.LoggingLabel1.AutoSize = true;
            this.LoggingLabel1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.LoggingLabel1.Location = new System.Drawing.Point(7, 74);
            this.LoggingLabel1.Name = "LoggingLabel1";
            this.LoggingLabel1.Size = new System.Drawing.Size(92, 13);
            this.LoggingLabel1.TabIndex = 2;
            this.LoggingLabel1.Text = "1. Enable logging.";
            // 
            // LoggingTextBox
            // 
            this.LoggingTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoggingTextBox.Location = new System.Drawing.Point(242, 20);
            this.LoggingTextBox.Name = "LoggingTextBox";
            this.LoggingTextBox.Size = new System.Drawing.Size(208, 20);
            this.LoggingTextBox.TabIndex = 1;
            this.LoggingTextBox.Text = "me66age";
            // 
            // LoggingCheckBox
            // 
            this.LoggingCheckBox.AutoSize = true;
            this.LoggingCheckBox.Location = new System.Drawing.Point(7, 22);
            this.LoggingCheckBox.Name = "LoggingCheckBox";
            this.LoggingCheckBox.Size = new System.Drawing.Size(229, 17);
            this.LoggingCheckBox.TabIndex = 0;
            this.LoggingCheckBox.Text = "Enable logging and find packets with text...";
            this.LoggingCheckBox.UseVisualStyleBackColor = true;
            // 
            // OptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LoggingGroupBox);
            this.Controls.Add(this.SilenceAfterTagLabel);
            this.Controls.Add(this.AddSilenceAfterNumericUpDown);
            this.Controls.Add(this.AddSilcenceBeforeNumericUpDown);
            this.Controls.Add(this.SilenceBeforeTagLabel);
            this.Controls.Add(this.AddSilenceGroupBox);
            this.Name = "OptionsControl";
            this.Size = new System.Drawing.Size(639, 434);
            this.Load += new System.EventHandler(this.OptionsControl_Load);
            this.AddSilenceGroupBox.ResumeLayout(false);
            this.AddSilenceGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddSilenceAfterNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddSilcenceBeforeNumericUpDown)).EndInit();
            this.LoggingGroupBox.ResumeLayout(false);
            this.LoggingGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LoggingLabel2;
        private System.Windows.Forms.Label LoggingLabel1;
        private System.Windows.Forms.Button LoggingButton;
        private System.Windows.Forms.TextBox LoggingFolderTextBox;
        private System.Windows.Forms.Button LoggingDefaultButton;
    }
}
