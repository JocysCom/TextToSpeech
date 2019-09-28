namespace JocysCom.TextToSpeech.Monitor.Controls
{
	partial class MonitorNetworkUserControl
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
			this.WinPcapRadioButton = new System.Windows.Forms.RadioButton();
			this.SocPcapRadioButton = new System.Windows.Forms.RadioButton();
			this.CaptureGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// CaptureGroupBox
			// 
			this.CaptureGroupBox.Controls.Add(this.WinPcapRadioButton);
			this.CaptureGroupBox.Controls.Add(this.SocPcapRadioButton);
			this.CaptureGroupBox.Location = new System.Drawing.Point(3, 3);
			this.CaptureGroupBox.Name = "CaptureGroupBox";
			this.CaptureGroupBox.Size = new System.Drawing.Size(240, 67);
			this.CaptureGroupBox.TabIndex = 11;
			this.CaptureGroupBox.TabStop = false;
			this.CaptureGroupBox.Text = "Packet Capture Librarry";
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
			// MonitorNetworkUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.CaptureGroupBox);
			this.Name = "MonitorNetworkUserControl";
			this.Size = new System.Drawing.Size(623, 266);
			this.CaptureGroupBox.ResumeLayout(false);
			this.CaptureGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox CaptureGroupBox;
		private System.Windows.Forms.RadioButton WinPcapRadioButton;
		private System.Windows.Forms.RadioButton SocPcapRadioButton;
	}
}
