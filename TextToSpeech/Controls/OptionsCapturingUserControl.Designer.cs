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
			this.CaptureGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).BeginInit();
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
			this.CreateImageButton.Location = new System.Drawing.Point(58, 138);
			this.CreateImageButton.Name = "CreateImageButton";
			this.CreateImageButton.Size = new System.Drawing.Size(99, 23);
			this.CreateImageButton.TabIndex = 13;
			this.CreateImageButton.Text = "Create Image";
			this.CreateImageButton.UseVisualStyleBackColor = true;
			this.CreateImageButton.Click += new System.EventHandler(this.CreateImageButton_Click);
			// 
			// ImagePictureBox
			// 
			this.ImagePictureBox.Location = new System.Drawing.Point(381, 112);
			this.ImagePictureBox.Name = "ImagePictureBox";
			this.ImagePictureBox.Size = new System.Drawing.Size(32, 32);
			this.ImagePictureBox.TabIndex = 14;
			this.ImagePictureBox.TabStop = false;
			// 
			// TestTextBox
			// 
			this.TestTextBox.Location = new System.Drawing.Point(58, 112);
			this.TestTextBox.Name = "TestTextBox";
			this.TestTextBox.Size = new System.Drawing.Size(317, 20);
			this.TestTextBox.TabIndex = 15;
			this.TestTextBox.Text = "Hello World!";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17, 115);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(28, 13);
			this.label1.TabIndex = 16;
			this.label1.Text = "Text";
			// 
			// EnableCapturingCheckBox
			// 
			this.EnableCapturingCheckBox.AutoSize = true;
			this.EnableCapturingCheckBox.Location = new System.Drawing.Point(268, 142);
			this.EnableCapturingCheckBox.Name = "EnableCapturingCheckBox";
			this.EnableCapturingCheckBox.Size = new System.Drawing.Size(107, 17);
			this.EnableCapturingCheckBox.TabIndex = 17;
			this.EnableCapturingCheckBox.Text = "Enable Capturing";
			this.EnableCapturingCheckBox.UseVisualStyleBackColor = true;
			// 
			// StatusTextBox
			// 
			this.StatusTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.StatusTextBox.Location = new System.Drawing.Point(58, 167);
			this.StatusTextBox.Name = "StatusTextBox";
			this.StatusTextBox.Size = new System.Drawing.Size(317, 20);
			this.StatusTextBox.TabIndex = 15;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 170);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(37, 13);
			this.label2.TabIndex = 16;
			this.label2.Text = "Status";
			// 
			// CaptureImageButton
			// 
			this.CaptureImageButton.Location = new System.Drawing.Point(163, 138);
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
			this.ResultsTextBox.Location = new System.Drawing.Point(58, 193);
			this.ResultsTextBox.Name = "ResultsTextBox";
			this.ResultsTextBox.Size = new System.Drawing.Size(317, 20);
			this.ResultsTextBox.TabIndex = 15;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 196);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 13);
			this.label3.TabIndex = 16;
			this.label3.Text = "Results";
			// 
			// OptionsCapturingUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.EnableCapturingCheckBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ResultsTextBox);
			this.Controls.Add(this.StatusTextBox);
			this.Controls.Add(this.TestTextBox);
			this.Controls.Add(this.ImagePictureBox);
			this.Controls.Add(this.CaptureImageButton);
			this.Controls.Add(this.CreateImageButton);
			this.Controls.Add(this.CaptureGroupBox);
			this.Name = "OptionsCapturingUserControl";
			this.Size = new System.Drawing.Size(521, 223);
			this.CaptureGroupBox.ResumeLayout(false);
			this.CaptureGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).EndInit();
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
	}
}
