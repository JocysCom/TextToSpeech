namespace JocysCom.TextToSpeech.Monitor.Controls
{
	partial class OptionsCapturingUserControl
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
			this.CaptureGroupBox = new System.Windows.Forms.GroupBox();
			this.DisplayRadioButton = new System.Windows.Forms.RadioButton();
			this.WinPcapRadioButton = new System.Windows.Forms.RadioButton();
			this.SocPcapRadioButton = new System.Windows.Forms.RadioButton();
			this.CreateImageButton = new System.Windows.Forms.Button();
			this.ImagePictureBox = new System.Windows.Forms.PictureBox();
			this.TestTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.EnableCapturingCheckBox = new System.Windows.Forms.CheckBox();
			this.StatusTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.CaptureImageButton = new System.Windows.Forms.Button();
			this.ResultsTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.ColorPrefixTextBox = new System.Windows.Forms.TextBox();
			this.ColorPrefixLabel = new System.Windows.Forms.Label();
			this.BoxSizeUpDown = new System.Windows.Forms.NumericUpDown();
			this.BoxSizeLabel = new System.Windows.Forms.Label();
			this.AddMessageTextCheckBox = new System.Windows.Forms.CheckBox();
			this.CopyWowTextButton = new System.Windows.Forms.Button();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.CaptureGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BoxSizeUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			this.SuspendLayout();
			// 
			// CaptureGroupBox
			// 
			this.CaptureGroupBox.Controls.Add(this.DisplayRadioButton);
			this.CaptureGroupBox.Controls.Add(this.WinPcapRadioButton);
			this.CaptureGroupBox.Controls.Add(this.SocPcapRadioButton);
			this.CaptureGroupBox.Location = new System.Drawing.Point(3, 3);
			this.CaptureGroupBox.Name = "CaptureGroupBox";
			this.CaptureGroupBox.Size = new System.Drawing.Size(240, 98);
			this.CaptureGroupBox.TabIndex = 11;
			this.CaptureGroupBox.TabStop = false;
			this.CaptureGroupBox.Text = "Packet Capture Librarry";
			// 
			// DisplayRadioButton
			// 
			this.DisplayRadioButton.AutoSize = true;
			this.DisplayRadioButton.Location = new System.Drawing.Point(6, 65);
			this.DisplayRadioButton.Name = "DisplayRadioButton";
			this.DisplayRadioButton.Size = new System.Drawing.Size(59, 17);
			this.DisplayRadioButton.TabIndex = 0;
			this.DisplayRadioButton.TabStop = true;
			this.DisplayRadioButton.Text = "Display";
			this.DisplayRadioButton.UseVisualStyleBackColor = true;
			// 
			// WinPcapRadioButton
			// 
			this.WinPcapRadioButton.AutoSize = true;
			this.WinPcapRadioButton.Location = new System.Drawing.Point(6, 42);
			this.WinPcapRadioButton.Name = "WinPcapRadioButton";
			this.WinPcapRadioButton.Size = new System.Drawing.Size(69, 17);
			this.WinPcapRadioButton.TabIndex = 0;
			this.WinPcapRadioButton.TabStop = true;
			this.WinPcapRadioButton.Text = "WinPcap";
			this.WinPcapRadioButton.UseVisualStyleBackColor = true;
			// 
			// SocPcapRadioButton
			// 
			this.SocPcapRadioButton.AutoSize = true;
			this.SocPcapRadioButton.Location = new System.Drawing.Point(6, 19);
			this.SocPcapRadioButton.Name = "SocPcapRadioButton";
			this.SocPcapRadioButton.Size = new System.Drawing.Size(225, 17);
			this.SocPcapRadioButton.TabIndex = 0;
			this.SocPcapRadioButton.TabStop = true;
			this.SocPcapRadioButton.Text = "SockRadioPcap - Microsoft .NET Sockets";
			this.SocPcapRadioButton.UseVisualStyleBackColor = true;
			// 
			// CreateImageButton
			// 
			this.CreateImageButton.Location = new System.Drawing.Point(85, 165);
			this.CreateImageButton.Name = "CreateImageButton";
			this.CreateImageButton.Size = new System.Drawing.Size(99, 23);
			this.CreateImageButton.TabIndex = 13;
			this.CreateImageButton.Text = "Create Image";
			this.CreateImageButton.UseVisualStyleBackColor = true;
			this.CreateImageButton.Click += new System.EventHandler(this.CreateImageButton_Click);
			// 
			// ImagePictureBox
			// 
			this.ImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ImagePictureBox.Location = new System.Drawing.Point(411, 162);
			this.ImagePictureBox.Name = "ImagePictureBox";
			this.ImagePictureBox.Size = new System.Drawing.Size(64, 16);
			this.ImagePictureBox.TabIndex = 14;
			this.ImagePictureBox.TabStop = false;
			// 
			// TestTextBox
			// 
			this.TestTextBox.Location = new System.Drawing.Point(85, 139);
			this.TestTextBox.Name = "TestTextBox";
			this.TestTextBox.Size = new System.Drawing.Size(317, 20);
			this.TestTextBox.TabIndex = 15;
			this.TestTextBox.Text = "Hello World!";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(51, 142);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(28, 13);
			this.label1.TabIndex = 16;
			this.label1.Text = "Text";
			// 
			// EnableCapturingCheckBox
			// 
			this.EnableCapturingCheckBox.AutoSize = true;
			this.EnableCapturingCheckBox.Location = new System.Drawing.Point(295, 169);
			this.EnableCapturingCheckBox.Name = "EnableCapturingCheckBox";
			this.EnableCapturingCheckBox.Size = new System.Drawing.Size(107, 17);
			this.EnableCapturingCheckBox.TabIndex = 17;
			this.EnableCapturingCheckBox.Text = "Enable Capturing";
			this.EnableCapturingCheckBox.UseVisualStyleBackColor = true;
			this.EnableCapturingCheckBox.CheckedChanged += new System.EventHandler(this.EnableCapturingCheckBox_CheckedChanged);
			// 
			// StatusTextBox
			// 
			this.StatusTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.StatusTextBox.Location = new System.Drawing.Point(85, 194);
			this.StatusTextBox.Name = "StatusTextBox";
			this.StatusTextBox.Size = new System.Drawing.Size(317, 20);
			this.StatusTextBox.TabIndex = 15;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(42, 197);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(37, 13);
			this.label2.TabIndex = 16;
			this.label2.Text = "Status";
			// 
			// CaptureImageButton
			// 
			this.CaptureImageButton.Location = new System.Drawing.Point(190, 165);
			this.CaptureImageButton.Name = "CaptureImageButton";
			this.CaptureImageButton.Size = new System.Drawing.Size(99, 23);
			this.CaptureImageButton.TabIndex = 13;
			this.CaptureImageButton.Text = "Capture Image";
			this.CaptureImageButton.UseVisualStyleBackColor = true;
			this.CaptureImageButton.Click += new System.EventHandler(this.CaptureImageButton_Click);
			// 
			// ResultsTextBox
			// 
			this.ResultsTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.ResultsTextBox.Location = new System.Drawing.Point(85, 220);
			this.ResultsTextBox.Name = "ResultsTextBox";
			this.ResultsTextBox.Size = new System.Drawing.Size(317, 20);
			this.ResultsTextBox.TabIndex = 15;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(37, 223);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 13);
			this.label3.TabIndex = 16;
			this.label3.Text = "Results";
			// 
			// ColorPrefixTextBox
			// 
			this.ColorPrefixTextBox.Location = new System.Drawing.Point(85, 113);
			this.ColorPrefixTextBox.Name = "ColorPrefixTextBox";
			this.ColorPrefixTextBox.Size = new System.Drawing.Size(317, 20);
			this.ColorPrefixTextBox.TabIndex = 15;
			this.ColorPrefixTextBox.Text = "#200000,#002000,#000020,#200000,#002000,#000020";
			// 
			// ColorPrefixLabel
			// 
			this.ColorPrefixLabel.AutoSize = true;
			this.ColorPrefixLabel.Location = new System.Drawing.Point(19, 116);
			this.ColorPrefixLabel.Name = "ColorPrefixLabel";
			this.ColorPrefixLabel.Size = new System.Drawing.Size(60, 13);
			this.ColorPrefixLabel.TabIndex = 16;
			this.ColorPrefixLabel.Text = "Color Prefix";
			// 
			// BoxSizeUpDown
			// 
			this.BoxSizeUpDown.Location = new System.Drawing.Point(462, 113);
			this.BoxSizeUpDown.Name = "BoxSizeUpDown";
			this.BoxSizeUpDown.Size = new System.Drawing.Size(41, 20);
			this.BoxSizeUpDown.TabIndex = 18;
			this.BoxSizeUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.BoxSizeUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// BoxSizeLabel
			// 
			this.BoxSizeLabel.AutoSize = true;
			this.BoxSizeLabel.Location = new System.Drawing.Point(408, 116);
			this.BoxSizeLabel.Name = "BoxSizeLabel";
			this.BoxSizeLabel.Size = new System.Drawing.Size(48, 13);
			this.BoxSizeLabel.TabIndex = 16;
			this.BoxSizeLabel.Text = "Box Size";
			// 
			// AddMessageTextCheckBox
			// 
			this.AddMessageTextCheckBox.AutoSize = true;
			this.AddMessageTextCheckBox.Location = new System.Drawing.Point(411, 139);
			this.AddMessageTextCheckBox.Name = "AddMessageTextCheckBox";
			this.AddMessageTextCheckBox.Size = new System.Drawing.Size(115, 17);
			this.AddMessageTextCheckBox.TabIndex = 17;
			this.AddMessageTextCheckBox.Text = "Add Message Text";
			this.AddMessageTextCheckBox.UseVisualStyleBackColor = true;
			// 
			// CopyWowTextButton
			// 
			this.CopyWowTextButton.Location = new System.Drawing.Point(411, 192);
			this.CopyWowTextButton.Name = "CopyWowTextButton";
			this.CopyWowTextButton.Size = new System.Drawing.Size(99, 23);
			this.CopyWowTextButton.TabIndex = 13;
			this.CopyWowTextButton.Text = "Copy WoW Text";
			this.CopyWowTextButton.UseVisualStyleBackColor = true;
			this.CopyWowTextButton.Click += new System.EventHandler(this.CopyWowTextButton_Click);
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(509, 113);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(41, 20);
			this.numericUpDown1.TabIndex = 18;
			this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Location = new System.Drawing.Point(556, 113);
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(41, 20);
			this.numericUpDown2.TabIndex = 18;
			this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// OptionsCapturingUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.numericUpDown2);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.BoxSizeUpDown);
			this.Controls.Add(this.AddMessageTextCheckBox);
			this.Controls.Add(this.EnableCapturingCheckBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.BoxSizeLabel);
			this.Controls.Add(this.ColorPrefixLabel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ResultsTextBox);
			this.Controls.Add(this.StatusTextBox);
			this.Controls.Add(this.ColorPrefixTextBox);
			this.Controls.Add(this.TestTextBox);
			this.Controls.Add(this.ImagePictureBox);
			this.Controls.Add(this.CaptureImageButton);
			this.Controls.Add(this.CopyWowTextButton);
			this.Controls.Add(this.CreateImageButton);
			this.Controls.Add(this.CaptureGroupBox);
			this.Name = "OptionsCapturingUserControl";
			this.Size = new System.Drawing.Size(623, 266);
			this.CaptureGroupBox.ResumeLayout(false);
			this.CaptureGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BoxSizeUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox CaptureGroupBox;
		private System.Windows.Forms.RadioButton WinPcapRadioButton;
		private System.Windows.Forms.RadioButton SocPcapRadioButton;
		private System.Windows.Forms.RadioButton DisplayRadioButton;
		private System.Windows.Forms.Button CreateImageButton;
		private System.Windows.Forms.PictureBox ImagePictureBox;
		private System.Windows.Forms.TextBox TestTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox EnableCapturingCheckBox;
		private System.Windows.Forms.TextBox StatusTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button CaptureImageButton;
		private System.Windows.Forms.TextBox ResultsTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox ColorPrefixTextBox;
		private System.Windows.Forms.Label ColorPrefixLabel;
		private System.Windows.Forms.NumericUpDown BoxSizeUpDown;
		private System.Windows.Forms.Label BoxSizeLabel;
		private System.Windows.Forms.CheckBox AddMessageTextCheckBox;
		private System.Windows.Forms.Button CopyWowTextButton;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.NumericUpDown numericUpDown2;
	}
}
