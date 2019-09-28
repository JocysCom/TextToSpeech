namespace JocysCom.TextToSpeech.Monitor.Controls
{
	partial class MonitorClipboardUserControl
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
			this.SettingsGroupBox = new System.Windows.Forms.GroupBox();
			this.MonitorEnabledCheckBox = new System.Windows.Forms.CheckBox();
			this.CopyIntervalLabel = new System.Windows.Forms.Label();
			this.CopyIntervalUpDown = new System.Windows.Forms.NumericUpDown();
			this.MessagesLabel = new System.Windows.Forms.Label();
			this.MessagesTextBox = new System.Windows.Forms.TextBox();
			this.SettingsGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.CopyIntervalUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// SettingsGroupBox
			// 
			this.SettingsGroupBox.Controls.Add(this.MessagesLabel);
			this.SettingsGroupBox.Controls.Add(this.MessagesTextBox);
			this.SettingsGroupBox.Controls.Add(this.MonitorEnabledCheckBox);
			this.SettingsGroupBox.Controls.Add(this.CopyIntervalLabel);
			this.SettingsGroupBox.Controls.Add(this.CopyIntervalUpDown);
			this.SettingsGroupBox.Location = new System.Drawing.Point(3, 3);
			this.SettingsGroupBox.Name = "SettingsGroupBox";
			this.SettingsGroupBox.Size = new System.Drawing.Size(173, 130);
			this.SettingsGroupBox.TabIndex = 6;
			this.SettingsGroupBox.TabStop = false;
			this.SettingsGroupBox.Text = "Copy Clipboard";
			// 
			// MonitorEnabledCheckBox
			// 
			this.MonitorEnabledCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.MonitorEnabledCheckBox.AutoSize = true;
			this.MonitorEnabledCheckBox.Location = new System.Drawing.Point(108, 19);
			this.MonitorEnabledCheckBox.Name = "MonitorEnabledCheckBox";
			this.MonitorEnabledCheckBox.Size = new System.Drawing.Size(59, 17);
			this.MonitorEnabledCheckBox.TabIndex = 0;
			this.MonitorEnabledCheckBox.Text = "Enable";
			this.MonitorEnabledCheckBox.UseVisualStyleBackColor = true;
			// 
			// CopyIntervalLabel
			// 
			this.CopyIntervalLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CopyIntervalLabel.AutoSize = true;
			this.CopyIntervalLabel.Location = new System.Drawing.Point(11, 70);
			this.CopyIntervalLabel.Name = "CopyIntervalLabel";
			this.CopyIntervalLabel.Size = new System.Drawing.Size(91, 13);
			this.CopyIntervalLabel.TabIndex = 4;
			this.CopyIntervalLabel.Text = "Copy Interval (ms)";
			// 
			// CopyIntervalUpDown
			// 
			this.CopyIntervalUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CopyIntervalUpDown.Location = new System.Drawing.Point(108, 68);
			this.CopyIntervalUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.CopyIntervalUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.CopyIntervalUpDown.Name = "CopyIntervalUpDown";
			this.CopyIntervalUpDown.Size = new System.Drawing.Size(58, 20);
			this.CopyIntervalUpDown.TabIndex = 1;
			this.CopyIntervalUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.CopyIntervalUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
			// 
			// MessagesLabel
			// 
			this.MessagesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.MessagesLabel.AutoSize = true;
			this.MessagesLabel.Location = new System.Drawing.Point(44, 45);
			this.MessagesLabel.Name = "MessagesLabel";
			this.MessagesLabel.Size = new System.Drawing.Size(58, 13);
			this.MessagesLabel.TabIndex = 8;
			this.MessagesLabel.Text = "Messages:";
			// 
			// MessagesTextBox
			// 
			this.MessagesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.MessagesTextBox.Location = new System.Drawing.Point(108, 42);
			this.MessagesTextBox.Name = "MessagesTextBox";
			this.MessagesTextBox.ReadOnly = true;
			this.MessagesTextBox.Size = new System.Drawing.Size(58, 20);
			this.MessagesTextBox.TabIndex = 7;
			this.MessagesTextBox.Text = "0";
			this.MessagesTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// MonitorClipboardUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.SettingsGroupBox);
			this.Name = "MonitorClipboardUserControl";
			this.Size = new System.Drawing.Size(403, 235);
			this.SettingsGroupBox.ResumeLayout(false);
			this.SettingsGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.CopyIntervalUpDown)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox SettingsGroupBox;
		private System.Windows.Forms.CheckBox MonitorEnabledCheckBox;
		private System.Windows.Forms.Label CopyIntervalLabel;
		private System.Windows.Forms.NumericUpDown CopyIntervalUpDown;
		private System.Windows.Forms.Label MessagesLabel;
		private System.Windows.Forms.TextBox MessagesTextBox;
	}
}
