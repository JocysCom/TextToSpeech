namespace JocysCom.TextToSpeech.Monitor.Controls
{
	partial class CacheOptionsUserControl
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
			this.CacheOptionsGroupBox = new System.Windows.Forms.GroupBox();
			this.OpenCacheButton = new System.Windows.Forms.Button();
			this.CacheDataGeneralizeCheckBox = new System.Windows.Forms.CheckBox();
			this.CacheDataReadCheckBox = new System.Windows.Forms.CheckBox();
			this.CacheDataWriteCheckBox = new System.Windows.Forms.CheckBox();
			this.CacheLabel = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.CacheAudioFormatComboBox = new System.Windows.Forms.ComboBox();
			this.CacheAudioChannelsComboBox = new System.Windows.Forms.ComboBox();
			this.CacheAudioBlockAlignComboBox = new System.Windows.Forms.ComboBox();
			this.CacheAudioAverageBitsPerSecondComboBox = new System.Windows.Forms.ComboBox();
			this.CacheAudioBitsPerSampleComboBox = new System.Windows.Forms.ComboBox();
			this.CacheAudioBlockAlignLabel = new System.Windows.Forms.Label();
			this.CacheAudioSampleRateComboBox = new System.Windows.Forms.ComboBox();
			this.CacheAudioAverageBytesPerSecondLabel = new System.Windows.Forms.Label();
			this.CacheAudioBitsPerSampleLabel = new System.Windows.Forms.Label();
			this.CacheAudioSampleRateLabel = new System.Windows.Forms.Label();
			this.CacheAudioChannelsLabel = new System.Windows.Forms.Label();
			this.CacheAudioConvertCheckBox = new System.Windows.Forms.CheckBox();
			this.CacheOptionsGroupBox.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// CacheOptionsGroupBox
			// 
			this.CacheOptionsGroupBox.Controls.Add(this.OpenCacheButton);
			this.CacheOptionsGroupBox.Controls.Add(this.CacheDataGeneralizeCheckBox);
			this.CacheOptionsGroupBox.Controls.Add(this.CacheDataReadCheckBox);
			this.CacheOptionsGroupBox.Controls.Add(this.CacheDataWriteCheckBox);
			this.CacheOptionsGroupBox.Controls.Add(this.CacheLabel);
			this.CacheOptionsGroupBox.Location = new System.Drawing.Point(3, 3);
			this.CacheOptionsGroupBox.Name = "CacheOptionsGroupBox";
			this.CacheOptionsGroupBox.Size = new System.Drawing.Size(235, 115);
			this.CacheOptionsGroupBox.TabIndex = 11;
			this.CacheOptionsGroupBox.TabStop = false;
			this.CacheOptionsGroupBox.Text = "Cache Options (Create TTS files)";
			// 
			// OpenCacheButton
			// 
			this.OpenCacheButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.OpenCacheButton.Location = new System.Drawing.Point(154, 19);
			this.OpenCacheButton.Name = "OpenCacheButton";
			this.OpenCacheButton.Size = new System.Drawing.Size(75, 23);
			this.OpenCacheButton.TabIndex = 6;
			this.OpenCacheButton.Text = "Open...";
			this.OpenCacheButton.UseVisualStyleBackColor = true;
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
			this.CacheLabel.Location = new System.Drawing.Point(3, 99);
			this.CacheLabel.Name = "CacheLabel";
			this.CacheLabel.Size = new System.Drawing.Size(91, 13);
			this.CacheLabel.TabIndex = 3;
			this.CacheLabel.Text = "Size: {0} files ({1})";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.CacheAudioFormatComboBox);
			this.groupBox1.Controls.Add(this.CacheAudioChannelsComboBox);
			this.groupBox1.Controls.Add(this.CacheAudioBlockAlignComboBox);
			this.groupBox1.Controls.Add(this.CacheAudioAverageBitsPerSecondComboBox);
			this.groupBox1.Controls.Add(this.CacheAudioBitsPerSampleComboBox);
			this.groupBox1.Controls.Add(this.CacheAudioBlockAlignLabel);
			this.groupBox1.Controls.Add(this.CacheAudioSampleRateComboBox);
			this.groupBox1.Controls.Add(this.CacheAudioAverageBytesPerSecondLabel);
			this.groupBox1.Controls.Add(this.CacheAudioBitsPerSampleLabel);
			this.groupBox1.Controls.Add(this.CacheAudioSampleRateLabel);
			this.groupBox1.Controls.Add(this.CacheAudioChannelsLabel);
			this.groupBox1.Controls.Add(this.CacheAudioConvertCheckBox);
			this.groupBox1.Location = new System.Drawing.Point(244, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(235, 184);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Output Format";
			// 
			// CacheAudioFormatComboBox
			// 
			this.CacheAudioFormatComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CacheAudioFormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CacheAudioFormatComboBox.FormattingEnabled = true;
			this.CacheAudioFormatComboBox.Location = new System.Drawing.Point(115, 19);
			this.CacheAudioFormatComboBox.Name = "CacheAudioFormatComboBox";
			this.CacheAudioFormatComboBox.Size = new System.Drawing.Size(114, 21);
			this.CacheAudioFormatComboBox.TabIndex = 206;
			// 
			// CacheAudioChannelsComboBox
			// 
			this.CacheAudioChannelsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CacheAudioChannelsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CacheAudioChannelsComboBox.FormattingEnabled = true;
			this.CacheAudioChannelsComboBox.Location = new System.Drawing.Point(115, 46);
			this.CacheAudioChannelsComboBox.Name = "CacheAudioChannelsComboBox";
			this.CacheAudioChannelsComboBox.Size = new System.Drawing.Size(114, 21);
			this.CacheAudioChannelsComboBox.TabIndex = 206;
			// 
			// CacheAudioBlockAlignComboBox
			// 
			this.CacheAudioBlockAlignComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CacheAudioBlockAlignComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CacheAudioBlockAlignComboBox.FormattingEnabled = true;
			this.CacheAudioBlockAlignComboBox.Location = new System.Drawing.Point(115, 154);
			this.CacheAudioBlockAlignComboBox.Name = "CacheAudioBlockAlignComboBox";
			this.CacheAudioBlockAlignComboBox.Size = new System.Drawing.Size(114, 21);
			this.CacheAudioBlockAlignComboBox.TabIndex = 208;
			// 
			// CacheAudioAverageBitsPerSecondComboBox
			// 
			this.CacheAudioAverageBitsPerSecondComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CacheAudioAverageBitsPerSecondComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CacheAudioAverageBitsPerSecondComboBox.FormattingEnabled = true;
			this.CacheAudioAverageBitsPerSecondComboBox.Location = new System.Drawing.Point(115, 127);
			this.CacheAudioAverageBitsPerSecondComboBox.Name = "CacheAudioAverageBitsPerSecondComboBox";
			this.CacheAudioAverageBitsPerSecondComboBox.Size = new System.Drawing.Size(114, 21);
			this.CacheAudioAverageBitsPerSecondComboBox.TabIndex = 207;
			// 
			// CacheAudioBitsPerSampleComboBox
			// 
			this.CacheAudioBitsPerSampleComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CacheAudioBitsPerSampleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CacheAudioBitsPerSampleComboBox.FormattingEnabled = true;
			this.CacheAudioBitsPerSampleComboBox.Location = new System.Drawing.Point(115, 100);
			this.CacheAudioBitsPerSampleComboBox.Name = "CacheAudioBitsPerSampleComboBox";
			this.CacheAudioBitsPerSampleComboBox.Size = new System.Drawing.Size(114, 21);
			this.CacheAudioBitsPerSampleComboBox.TabIndex = 208;
			// 
			// CacheAudioBlockAlignLabel
			// 
			this.CacheAudioBlockAlignLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.CacheAudioBlockAlignLabel.AutoSize = true;
			this.CacheAudioBlockAlignLabel.Location = new System.Drawing.Point(6, 157);
			this.CacheAudioBlockAlignLabel.Name = "CacheAudioBlockAlignLabel";
			this.CacheAudioBlockAlignLabel.Size = new System.Drawing.Size(60, 13);
			this.CacheAudioBlockAlignLabel.TabIndex = 211;
			this.CacheAudioBlockAlignLabel.Text = "Block Align";
			// 
			// CacheAudioSampleRateComboBox
			// 
			this.CacheAudioSampleRateComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CacheAudioSampleRateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CacheAudioSampleRateComboBox.FormattingEnabled = true;
			this.CacheAudioSampleRateComboBox.Location = new System.Drawing.Point(115, 73);
			this.CacheAudioSampleRateComboBox.Name = "CacheAudioSampleRateComboBox";
			this.CacheAudioSampleRateComboBox.Size = new System.Drawing.Size(114, 21);
			this.CacheAudioSampleRateComboBox.TabIndex = 207;
			// 
			// CacheAudioAverageBytesPerSecondLabel
			// 
			this.CacheAudioAverageBytesPerSecondLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.CacheAudioAverageBytesPerSecondLabel.AutoSize = true;
			this.CacheAudioAverageBytesPerSecondLabel.Location = new System.Drawing.Point(6, 130);
			this.CacheAudioAverageBytesPerSecondLabel.Name = "CacheAudioAverageBytesPerSecondLabel";
			this.CacheAudioAverageBytesPerSecondLabel.Size = new System.Drawing.Size(76, 13);
			this.CacheAudioAverageBytesPerSecondLabel.TabIndex = 210;
			this.CacheAudioAverageBytesPerSecondLabel.Text = "Average bits/s";
			// 
			// CacheAudioBitsPerSampleLabel
			// 
			this.CacheAudioBitsPerSampleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.CacheAudioBitsPerSampleLabel.AutoSize = true;
			this.CacheAudioBitsPerSampleLabel.Location = new System.Drawing.Point(6, 103);
			this.CacheAudioBitsPerSampleLabel.Name = "CacheAudioBitsPerSampleLabel";
			this.CacheAudioBitsPerSampleLabel.Size = new System.Drawing.Size(64, 13);
			this.CacheAudioBitsPerSampleLabel.TabIndex = 211;
			this.CacheAudioBitsPerSampleLabel.Text = "Bits/Sample";
			// 
			// CacheAudioSampleRateLabel
			// 
			this.CacheAudioSampleRateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.CacheAudioSampleRateLabel.AutoSize = true;
			this.CacheAudioSampleRateLabel.Location = new System.Drawing.Point(6, 76);
			this.CacheAudioSampleRateLabel.Name = "CacheAudioSampleRateLabel";
			this.CacheAudioSampleRateLabel.Size = new System.Drawing.Size(68, 13);
			this.CacheAudioSampleRateLabel.TabIndex = 210;
			this.CacheAudioSampleRateLabel.Text = "Sample Rate";
			// 
			// CacheAudioChannelsLabel
			// 
			this.CacheAudioChannelsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.CacheAudioChannelsLabel.AutoSize = true;
			this.CacheAudioChannelsLabel.Location = new System.Drawing.Point(6, 49);
			this.CacheAudioChannelsLabel.Name = "CacheAudioChannelsLabel";
			this.CacheAudioChannelsLabel.Size = new System.Drawing.Size(51, 13);
			this.CacheAudioChannelsLabel.TabIndex = 209;
			this.CacheAudioChannelsLabel.Text = "Channels";
			// 
			// CacheAudioConvertCheckBox
			// 
			this.CacheAudioConvertCheckBox.AutoSize = true;
			this.CacheAudioConvertCheckBox.Location = new System.Drawing.Point(9, 21);
			this.CacheAudioConvertCheckBox.Name = "CacheAudioConvertCheckBox";
			this.CacheAudioConvertCheckBox.Size = new System.Drawing.Size(79, 17);
			this.CacheAudioConvertCheckBox.TabIndex = 1;
			this.CacheAudioConvertCheckBox.Text = "Convert To";
			this.CacheAudioConvertCheckBox.UseVisualStyleBackColor = true;
			// 
			// CacheOptionsUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.CacheOptionsGroupBox);
			this.Name = "CacheOptionsUserControl";
			this.Size = new System.Drawing.Size(539, 257);
			this.Load += new System.EventHandler(this.OptionsCacheUserControl_Load);
			this.CacheOptionsGroupBox.ResumeLayout(false);
			this.CacheOptionsGroupBox.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox CacheOptionsGroupBox;
		private System.Windows.Forms.Button OpenCacheButton;
		private System.Windows.Forms.CheckBox CacheDataGeneralizeCheckBox;
		private System.Windows.Forms.CheckBox CacheDataReadCheckBox;
		private System.Windows.Forms.CheckBox CacheDataWriteCheckBox;
		private System.Windows.Forms.Label CacheLabel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox CacheAudioConvertCheckBox;
		private System.Windows.Forms.ComboBox CacheAudioChannelsComboBox;
		private System.Windows.Forms.ComboBox CacheAudioBitsPerSampleComboBox;
		private System.Windows.Forms.ComboBox CacheAudioSampleRateComboBox;
		private System.Windows.Forms.Label CacheAudioBitsPerSampleLabel;
		private System.Windows.Forms.Label CacheAudioSampleRateLabel;
		private System.Windows.Forms.Label CacheAudioChannelsLabel;
		private System.Windows.Forms.ComboBox CacheAudioFormatComboBox;
		private System.Windows.Forms.ComboBox CacheAudioBlockAlignComboBox;
		private System.Windows.Forms.ComboBox CacheAudioAverageBitsPerSecondComboBox;
		private System.Windows.Forms.Label CacheAudioBlockAlignLabel;
		private System.Windows.Forms.Label CacheAudioAverageBytesPerSecondLabel;
	}
}
