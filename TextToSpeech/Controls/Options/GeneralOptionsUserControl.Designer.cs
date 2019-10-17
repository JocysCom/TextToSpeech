namespace JocysCom.TextToSpeech.Monitor.Controls.Options
{
	partial class GeneralOptionsUserControl
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
			this.AddSilenceBeforeNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.AddSilenceBeforeLabel = new System.Windows.Forms.Label();
			this.SilenceBeforeTagLabel = new System.Windows.Forms.Label();
			this.SplitMessageIntoSentencesCheckBox = new System.Windows.Forms.CheckBox();
			this.SynthesizingOptionsGroupBox = new System.Windows.Forms.GroupBox();
			this.ModifyVolumeLocallyCheckBox = new System.Windows.Forms.CheckBox();
			this.ModifyRateLocallyCheckBox = new System.Windows.Forms.CheckBox();
			this.ModifyPitchLocallyCheckBox = new System.Windows.Forms.CheckBox();
			this.ModifyLocallyGroupBox = new System.Windows.Forms.GroupBox();
			this.AddSilenceGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.AddSilenceAfterNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.AddSilenceBeforeNumericUpDown)).BeginInit();
			this.SynthesizingOptionsGroupBox.SuspendLayout();
			this.ModifyLocallyGroupBox.SuspendLayout();
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
			this.AddSilenceGroupBox.Controls.Add(this.AddSilenceBeforeNumericUpDown);
			this.AddSilenceGroupBox.Controls.Add(this.AddSilenceBeforeLabel);
			this.AddSilenceGroupBox.Controls.Add(this.SilenceBeforeTagLabel);
			this.AddSilenceGroupBox.Location = new System.Drawing.Point(3, 3);
			this.AddSilenceGroupBox.Name = "AddSilenceGroupBox";
			this.AddSilenceGroupBox.Size = new System.Drawing.Size(585, 101);
			this.AddSilenceGroupBox.TabIndex = 1;
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
			this.PlaybackDeviceComboBox.Size = new System.Drawing.Size(407, 21);
			this.PlaybackDeviceComboBox.TabIndex = 33;
			// 
			// RefreshPlaybackDevices
			// 
			this.RefreshPlaybackDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RefreshPlaybackDevices.Location = new System.Drawing.Point(504, 70);
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
			this.label1.Size = new System.Drawing.Size(76, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "Output Device";
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
			this.AddSilenceAfterLabel.Size = new System.Drawing.Size(127, 13);
			this.AddSilenceAfterLabel.TabIndex = 6;
			this.AddSilenceAfterLabel.Text = "( default value is 0 ) [ ms ]";
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
			// AddSilenceBeforeNumericUpDown
			// 
			this.AddSilenceBeforeNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.AddSilenceBeforeNumericUpDown.Location = new System.Drawing.Point(279, 19);
			this.AddSilenceBeforeNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.AddSilenceBeforeNumericUpDown.Name = "AddSilenceBeforeNumericUpDown";
			this.AddSilenceBeforeNumericUpDown.Size = new System.Drawing.Size(114, 20);
			this.AddSilenceBeforeNumericUpDown.TabIndex = 5;
			this.AddSilenceBeforeNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// AddSilenceBeforeLabel
			// 
			this.AddSilenceBeforeLabel.AutoSize = true;
			this.AddSilenceBeforeLabel.Location = new System.Drawing.Point(6, 21);
			this.AddSilenceBeforeLabel.Name = "AddSilenceBeforeLabel";
			this.AddSilenceBeforeLabel.Size = new System.Drawing.Size(267, 13);
			this.AddSilenceBeforeLabel.TabIndex = 6;
			this.AddSilenceBeforeLabel.Text = "Add Silence Before Message ( default value is 0 ) [ ms ]";
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
			// SplitMessageIntoSentencesCheckBox
			// 
			this.SplitMessageIntoSentencesCheckBox.AutoSize = true;
			this.SplitMessageIntoSentencesCheckBox.Location = new System.Drawing.Point(6, 19);
			this.SplitMessageIntoSentencesCheckBox.Name = "SplitMessageIntoSentencesCheckBox";
			this.SplitMessageIntoSentencesCheckBox.Size = new System.Drawing.Size(240, 17);
			this.SplitMessageIntoSentencesCheckBox.TabIndex = 2;
			this.SplitMessageIntoSentencesCheckBox.Text = "Split message into sentences for faster results";
			this.SplitMessageIntoSentencesCheckBox.UseVisualStyleBackColor = true;
			// 
			// SynthesizingOptionsGroupBox
			// 
			this.SynthesizingOptionsGroupBox.Controls.Add(this.SplitMessageIntoSentencesCheckBox);
			this.SynthesizingOptionsGroupBox.Location = new System.Drawing.Point(118, 110);
			this.SynthesizingOptionsGroupBox.Name = "SynthesizingOptionsGroupBox";
			this.SynthesizingOptionsGroupBox.Size = new System.Drawing.Size(253, 44);
			this.SynthesizingOptionsGroupBox.TabIndex = 3;
			this.SynthesizingOptionsGroupBox.TabStop = false;
			this.SynthesizingOptionsGroupBox.Text = "Synthesizing Options";
			// 
			// ModifyVolumeLocallyCheckBox
			// 
			this.ModifyVolumeLocallyCheckBox.AutoSize = true;
			this.ModifyVolumeLocallyCheckBox.Location = new System.Drawing.Point(6, 65);
			this.ModifyVolumeLocallyCheckBox.Name = "ModifyVolumeLocallyCheckBox";
			this.ModifyVolumeLocallyCheckBox.Size = new System.Drawing.Size(61, 17);
			this.ModifyVolumeLocallyCheckBox.TabIndex = 2;
			this.ModifyVolumeLocallyCheckBox.Text = "Volume";
			this.ModifyVolumeLocallyCheckBox.UseVisualStyleBackColor = true;
			// 
			// ModifyRateLocallyCheckBox
			// 
			this.ModifyRateLocallyCheckBox.AutoSize = true;
			this.ModifyRateLocallyCheckBox.Location = new System.Drawing.Point(6, 42);
			this.ModifyRateLocallyCheckBox.Name = "ModifyRateLocallyCheckBox";
			this.ModifyRateLocallyCheckBox.Size = new System.Drawing.Size(49, 17);
			this.ModifyRateLocallyCheckBox.TabIndex = 2;
			this.ModifyRateLocallyCheckBox.Text = "Rate";
			this.ModifyRateLocallyCheckBox.UseVisualStyleBackColor = true;
			// 
			// ModifyPitchLocallyCheckBox
			// 
			this.ModifyPitchLocallyCheckBox.AutoSize = true;
			this.ModifyPitchLocallyCheckBox.Location = new System.Drawing.Point(6, 19);
			this.ModifyPitchLocallyCheckBox.Name = "ModifyPitchLocallyCheckBox";
			this.ModifyPitchLocallyCheckBox.Size = new System.Drawing.Size(50, 17);
			this.ModifyPitchLocallyCheckBox.TabIndex = 2;
			this.ModifyPitchLocallyCheckBox.Text = "Pitch";
			this.ModifyPitchLocallyCheckBox.UseVisualStyleBackColor = true;
			// 
			// ModifyLocallyGroupBox
			// 
			this.ModifyLocallyGroupBox.Controls.Add(this.ModifyVolumeLocallyCheckBox);
			this.ModifyLocallyGroupBox.Controls.Add(this.ModifyPitchLocallyCheckBox);
			this.ModifyLocallyGroupBox.Controls.Add(this.ModifyRateLocallyCheckBox);
			this.ModifyLocallyGroupBox.Location = new System.Drawing.Point(3, 110);
			this.ModifyLocallyGroupBox.Name = "ModifyLocallyGroupBox";
			this.ModifyLocallyGroupBox.Size = new System.Drawing.Size(109, 89);
			this.ModifyLocallyGroupBox.TabIndex = 3;
			this.ModifyLocallyGroupBox.TabStop = false;
			this.ModifyLocallyGroupBox.Text = "Modify Locally";
			// 
			// GeneralOptionsUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ModifyLocallyGroupBox);
			this.Controls.Add(this.SynthesizingOptionsGroupBox);
			this.Controls.Add(this.AddSilenceGroupBox);
			this.Name = "GeneralOptionsUserControl";
			this.Size = new System.Drawing.Size(591, 283);
			this.Load += new System.EventHandler(this.GeneralOptionsUserControl_Load);
			this.AddSilenceGroupBox.ResumeLayout(false);
			this.AddSilenceGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.AddSilenceAfterNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.AddSilenceBeforeNumericUpDown)).EndInit();
			this.SynthesizingOptionsGroupBox.ResumeLayout(false);
			this.SynthesizingOptionsGroupBox.PerformLayout();
			this.ModifyLocallyGroupBox.ResumeLayout(false);
			this.ModifyLocallyGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox AddSilenceGroupBox;
		private System.Windows.Forms.ComboBox PlaybackDeviceComboBox;
		private System.Windows.Forms.Button RefreshPlaybackDevices;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label AddSilenceAfterLabel;
		private System.Windows.Forms.NumericUpDown AddSilenceAfterNumericUpDown;
		private System.Windows.Forms.Label SilenceAfterTagLabel;
		private System.Windows.Forms.NumericUpDown AddSilenceBeforeNumericUpDown;
		private System.Windows.Forms.Label AddSilenceBeforeLabel;
		private System.Windows.Forms.Label SilenceBeforeTagLabel;
		private System.Windows.Forms.CheckBox SplitMessageIntoSentencesCheckBox;
		private System.Windows.Forms.GroupBox SynthesizingOptionsGroupBox;
		private System.Windows.Forms.CheckBox ModifyPitchLocallyCheckBox;
		private System.Windows.Forms.CheckBox ModifyVolumeLocallyCheckBox;
		private System.Windows.Forms.CheckBox ModifyRateLocallyCheckBox;
		private System.Windows.Forms.GroupBox ModifyLocallyGroupBox;
	}
}
