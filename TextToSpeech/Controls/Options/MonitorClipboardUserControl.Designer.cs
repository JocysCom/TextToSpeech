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
			this.MainSettingsGroupBox = new System.Windows.Forms.GroupBox();
			this.MessagesLabel = new System.Windows.Forms.Label();
			this.MessagesTextBox = new System.Windows.Forms.TextBox();
			this.MonitorEnabledCheckBox = new System.Windows.Forms.CheckBox();
			this.CopyIntervalLabel = new System.Windows.Forms.Label();
			this.CopyIntervalUpDown = new System.Windows.Forms.NumericUpDown();
			this.MainSettingsGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.CopyIntervalUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// MainSettingsGroupBox
			// 
			this.MainSettingsGroupBox.Controls.Add(this.MessagesLabel);
			this.MainSettingsGroupBox.Controls.Add(this.MessagesTextBox);
			this.MainSettingsGroupBox.Controls.Add(this.MonitorEnabledCheckBox);
			this.MainSettingsGroupBox.Controls.Add(this.CopyIntervalLabel);
			this.MainSettingsGroupBox.Controls.Add(this.CopyIntervalUpDown);
			this.MainSettingsGroupBox.Location = new System.Drawing.Point(3, 3);
			this.MainSettingsGroupBox.Name = "MainSettingsGroupBox";
			this.MainSettingsGroupBox.Size = new System.Drawing.Size(170, 130);
			this.MainSettingsGroupBox.TabIndex = 6;
			this.MainSettingsGroupBox.TabStop = false;
			this.MainSettingsGroupBox.Text = "Main Settings";
			// 
			// MessagesLabel
			// 
			this.MessagesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.MessagesLabel.AutoSize = true;
			this.MessagesLabel.Location = new System.Drawing.Point(41, 45);
			this.MessagesLabel.Name = "MessagesLabel";
			this.MessagesLabel.Size = new System.Drawing.Size(58, 13);
			this.MessagesLabel.TabIndex = 8;
			this.MessagesLabel.Text = "Messages:";
			// 
			// MessagesTextBox
			// 
			this.MessagesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.MessagesTextBox.Location = new System.Drawing.Point(105, 42);
			this.MessagesTextBox.Name = "MessagesTextBox";
			this.MessagesTextBox.ReadOnly = true;
			this.MessagesTextBox.Size = new System.Drawing.Size(58, 20);
			this.MessagesTextBox.TabIndex = 7;
			this.MessagesTextBox.Text = "0";
			this.MessagesTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// MonitorEnabledCheckBox
			// 
			this.MonitorEnabledCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.MonitorEnabledCheckBox.AutoSize = true;
			this.MonitorEnabledCheckBox.Location = new System.Drawing.Point(105, 19);
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
			this.CopyIntervalLabel.Location = new System.Drawing.Point(8, 70);
			this.CopyIntervalLabel.Name = "CopyIntervalLabel";
			this.CopyIntervalLabel.Size = new System.Drawing.Size(91, 13);
			this.CopyIntervalLabel.TabIndex = 4;
			this.CopyIntervalLabel.Text = "Copy Interval (ms)";
			// 
			// CopyIntervalUpDown
			// 
			this.CopyIntervalUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CopyIntervalUpDown.Location = new System.Drawing.Point(105, 68);
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
			// MonitorClipboardUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.MainSettingsGroupBox);
			this.Name = "MonitorClipboardUserControl";
			this.Size = new System.Drawing.Size(640, 280);
			this.Load += new System.EventHandler(this.MonitorClipboardUserControl_Load);
			this.MainSettingsGroupBox.ResumeLayout(false);
			this.MainSettingsGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.CopyIntervalUpDown)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox MainSettingsGroupBox;
		private System.Windows.Forms.CheckBox MonitorEnabledCheckBox;
		private System.Windows.Forms.Label CopyIntervalLabel;
		private System.Windows.Forms.NumericUpDown CopyIntervalUpDown;
		private System.Windows.Forms.Label MessagesLabel;
		private System.Windows.Forms.TextBox MessagesTextBox;
	}
}
