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
			this.CacheOptionsGroupBox.SuspendLayout();
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
			this.CacheOptionsGroupBox.Size = new System.Drawing.Size(211, 128);
			this.CacheOptionsGroupBox.TabIndex = 11;
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
			// OptionsCacheUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.CacheOptionsGroupBox);
			this.Name = "OptionsCacheUserControl";
			this.Size = new System.Drawing.Size(316, 181);
			this.Load += new System.EventHandler(this.OptionsCacheUserControl_Load);
			this.CacheOptionsGroupBox.ResumeLayout(false);
			this.CacheOptionsGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox CacheOptionsGroupBox;
		private System.Windows.Forms.Button OpenCacheButton;
		private System.Windows.Forms.CheckBox CacheDataGeneralizeCheckBox;
		private System.Windows.Forms.CheckBox CacheDataReadCheckBox;
		private System.Windows.Forms.CheckBox CacheDataWriteCheckBox;
		private System.Windows.Forms.Label CacheLabel;
	}
}
