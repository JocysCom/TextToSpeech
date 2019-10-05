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
			this.LogGroupBox = new System.Windows.Forms.GroupBox();
			this.LogPlaySoundCheckBox = new System.Windows.Forms.CheckBox();
			this.LogFolderLabel = new System.Windows.Forms.Label();
			this.FilterTextLabel = new System.Windows.Forms.Label();
			this.LogFolderTextBox = new System.Windows.Forms.TextBox();
			this.HowToButton = new System.Windows.Forms.Button();
			this.OpenButton = new System.Windows.Forms.Button();
			this.LogFilterTextTextBox = new System.Windows.Forms.TextBox();
			this.LogEnabledCheckBox = new System.Windows.Forms.CheckBox();
			this.MainSettingsGroupBox = new System.Windows.Forms.GroupBox();
			this.UdpMessagesLabel = new System.Windows.Forms.Label();
			this.UdpPortMessagesTextBox = new System.Windows.Forms.TextBox();
			this.RunAsAdministratorLabel = new System.Windows.Forms.Label();
			this.AsAdministratorPanel = new System.Windows.Forms.Panel();
			this.CaptureGroupBox.SuspendLayout();
			this.LogGroupBox.SuspendLayout();
			this.MainSettingsGroupBox.SuspendLayout();
			this.AsAdministratorPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// CaptureGroupBox
			// 
			this.CaptureGroupBox.Controls.Add(this.WinPcapRadioButton);
			this.CaptureGroupBox.Controls.Add(this.SocPcapRadioButton);
			this.CaptureGroupBox.Location = new System.Drawing.Point(179, 27);
			this.CaptureGroupBox.Name = "CaptureGroupBox";
			this.CaptureGroupBox.Size = new System.Drawing.Size(240, 73);
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
			// LogGroupBox
			// 
			this.LogGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LogGroupBox.Controls.Add(this.LogPlaySoundCheckBox);
			this.LogGroupBox.Controls.Add(this.LogFolderLabel);
			this.LogGroupBox.Controls.Add(this.FilterTextLabel);
			this.LogGroupBox.Controls.Add(this.LogFolderTextBox);
			this.LogGroupBox.Controls.Add(this.HowToButton);
			this.LogGroupBox.Controls.Add(this.OpenButton);
			this.LogGroupBox.Controls.Add(this.LogFilterTextTextBox);
			this.LogGroupBox.Location = new System.Drawing.Point(3, 106);
			this.LogGroupBox.Name = "LogGroupBox";
			this.LogGroupBox.Size = new System.Drawing.Size(634, 122);
			this.LogGroupBox.TabIndex = 12;
			this.LogGroupBox.TabStop = false;
			this.LogGroupBox.Text = "Log Network Packets Test ( Plugin Helper )";
			// 
			// LogPlaySoundCheckBox
			// 
			this.LogPlaySoundCheckBox.AutoSize = true;
			this.LogPlaySoundCheckBox.Checked = true;
			this.LogPlaySoundCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.LogPlaySoundCheckBox.Location = new System.Drawing.Point(6, 20);
			this.LogPlaySoundCheckBox.Name = "LogPlaySoundCheckBox";
			this.LogPlaySoundCheckBox.Size = new System.Drawing.Size(328, 17);
			this.LogPlaySoundCheckBox.TabIndex = 11;
			this.LogPlaySoundCheckBox.Text = "Play “Radio2” sound, when filter text is found in network packet.";
			this.LogPlaySoundCheckBox.UseVisualStyleBackColor = true;
			// 
			// LogFolderLabel
			// 
			this.LogFolderLabel.AutoSize = true;
			this.LogFolderLabel.Location = new System.Drawing.Point(84, 42);
			this.LogFolderLabel.Name = "LogFolderLabel";
			this.LogFolderLabel.Size = new System.Drawing.Size(60, 13);
			this.LogFolderLabel.TabIndex = 10;
			this.LogFolderLabel.Text = "Log Folder:";
			// 
			// FilterTextLabel
			// 
			this.FilterTextLabel.AutoSize = true;
			this.FilterTextLabel.Location = new System.Drawing.Point(6, 42);
			this.FilterTextLabel.Name = "FilterTextLabel";
			this.FilterTextLabel.Size = new System.Drawing.Size(56, 13);
			this.FilterTextLabel.TabIndex = 10;
			this.FilterTextLabel.Text = "Filter Text:";
			// 
			// LogFolderTextBox
			// 
			this.LogFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LogFolderTextBox.Location = new System.Drawing.Point(91, 58);
			this.LogFolderTextBox.Name = "LogFolderTextBox";
			this.LogFolderTextBox.ReadOnly = true;
			this.LogFolderTextBox.Size = new System.Drawing.Size(456, 20);
			this.LogFolderTextBox.TabIndex = 7;
			// 
			// HowToButton
			// 
			this.HowToButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.HowToButton.Location = new System.Drawing.Point(553, 16);
			this.HowToButton.Name = "HowToButton";
			this.HowToButton.Size = new System.Drawing.Size(75, 23);
			this.HowToButton.TabIndex = 5;
			this.HowToButton.Text = "How To...";
			this.HowToButton.UseVisualStyleBackColor = true;
			// 
			// OpenButton
			// 
			this.OpenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.OpenButton.Location = new System.Drawing.Point(553, 56);
			this.OpenButton.Name = "OpenButton";
			this.OpenButton.Size = new System.Drawing.Size(75, 23);
			this.OpenButton.TabIndex = 5;
			this.OpenButton.Text = "Open...";
			this.OpenButton.UseVisualStyleBackColor = true;
			// 
			// LogFilterTextTextBox
			// 
			this.LogFilterTextTextBox.Location = new System.Drawing.Point(6, 58);
			this.LogFilterTextTextBox.Name = "LogFilterTextTextBox";
			this.LogFilterTextTextBox.Size = new System.Drawing.Size(79, 20);
			this.LogFilterTextTextBox.TabIndex = 1;
			this.LogFilterTextTextBox.Text = "me66age";
			// 
			// LogEnabledCheckBox
			// 
			this.LogEnabledCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LogEnabledCheckBox.AutoSize = true;
			this.LogEnabledCheckBox.Location = new System.Drawing.Point(105, 19);
			this.LogEnabledCheckBox.Name = "LogEnabledCheckBox";
			this.LogEnabledCheckBox.Size = new System.Drawing.Size(59, 17);
			this.LogEnabledCheckBox.TabIndex = 0;
			this.LogEnabledCheckBox.Text = "Enable";
			this.LogEnabledCheckBox.UseVisualStyleBackColor = true;
			// 
			// MainSettingsGroupBox
			// 
			this.MainSettingsGroupBox.Controls.Add(this.UdpMessagesLabel);
			this.MainSettingsGroupBox.Controls.Add(this.UdpPortMessagesTextBox);
			this.MainSettingsGroupBox.Controls.Add(this.LogEnabledCheckBox);
			this.MainSettingsGroupBox.Location = new System.Drawing.Point(3, 27);
			this.MainSettingsGroupBox.Name = "MainSettingsGroupBox";
			this.MainSettingsGroupBox.Size = new System.Drawing.Size(170, 73);
			this.MainSettingsGroupBox.TabIndex = 13;
			this.MainSettingsGroupBox.TabStop = false;
			this.MainSettingsGroupBox.Text = "Main Settings";
			// 
			// UdpMessagesLabel
			// 
			this.UdpMessagesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.UdpMessagesLabel.AutoSize = true;
			this.UdpMessagesLabel.Location = new System.Drawing.Point(41, 44);
			this.UdpMessagesLabel.Name = "UdpMessagesLabel";
			this.UdpMessagesLabel.Size = new System.Drawing.Size(58, 13);
			this.UdpMessagesLabel.TabIndex = 6;
			this.UdpMessagesLabel.Text = "Messages:";
			// 
			// UdpPortMessagesTextBox
			// 
			this.UdpPortMessagesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.UdpPortMessagesTextBox.Location = new System.Drawing.Point(105, 42);
			this.UdpPortMessagesTextBox.Name = "UdpPortMessagesTextBox";
			this.UdpPortMessagesTextBox.ReadOnly = true;
			this.UdpPortMessagesTextBox.Size = new System.Drawing.Size(58, 20);
			this.UdpPortMessagesTextBox.TabIndex = 3;
			this.UdpPortMessagesTextBox.Text = "0";
			this.UdpPortMessagesTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// RunAsAdministratorLabel
			// 
			this.RunAsAdministratorLabel.AutoSize = true;
			this.RunAsAdministratorLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.RunAsAdministratorLabel.Location = new System.Drawing.Point(0, 0);
			this.RunAsAdministratorLabel.Name = "RunAsAdministratorLabel";
			this.RunAsAdministratorLabel.Padding = new System.Windows.Forms.Padding(3);
			this.RunAsAdministratorLabel.Size = new System.Drawing.Size(444, 19);
			this.RunAsAdministratorLabel.TabIndex = 14;
			this.RunAsAdministratorLabel.Text = "You must run this application as Administrator in order for “Network Monitor” fea" +
    "ture to work.";
			// 
			// AsAdministratorPanel
			// 
			this.AsAdministratorPanel.AutoSize = true;
			this.AsAdministratorPanel.BackColor = System.Drawing.SystemColors.Info;
			this.AsAdministratorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.AsAdministratorPanel.Controls.Add(this.RunAsAdministratorLabel);
			this.AsAdministratorPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.AsAdministratorPanel.Location = new System.Drawing.Point(0, 0);
			this.AsAdministratorPanel.Name = "AsAdministratorPanel";
			this.AsAdministratorPanel.Size = new System.Drawing.Size(640, 21);
			this.AsAdministratorPanel.TabIndex = 15;
			// 
			// MonitorNetworkUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.AsAdministratorPanel);
			this.Controls.Add(this.MainSettingsGroupBox);
			this.Controls.Add(this.LogGroupBox);
			this.Controls.Add(this.CaptureGroupBox);
			this.Name = "MonitorNetworkUserControl";
			this.Size = new System.Drawing.Size(640, 280);
			this.CaptureGroupBox.ResumeLayout(false);
			this.CaptureGroupBox.PerformLayout();
			this.LogGroupBox.ResumeLayout(false);
			this.LogGroupBox.PerformLayout();
			this.MainSettingsGroupBox.ResumeLayout(false);
			this.MainSettingsGroupBox.PerformLayout();
			this.AsAdministratorPanel.ResumeLayout(false);
			this.AsAdministratorPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox CaptureGroupBox;
		private System.Windows.Forms.RadioButton WinPcapRadioButton;
		private System.Windows.Forms.RadioButton SocPcapRadioButton;
		private System.Windows.Forms.GroupBox LogGroupBox;
		private System.Windows.Forms.CheckBox LogPlaySoundCheckBox;
		private System.Windows.Forms.Label LogFolderLabel;
		private System.Windows.Forms.Label FilterTextLabel;
		private System.Windows.Forms.TextBox LogFolderTextBox;
		private System.Windows.Forms.Button HowToButton;
		private System.Windows.Forms.Button OpenButton;
		private System.Windows.Forms.TextBox LogFilterTextTextBox;
		private System.Windows.Forms.CheckBox LogEnabledCheckBox;
		private System.Windows.Forms.GroupBox MainSettingsGroupBox;
		private System.Windows.Forms.Label UdpMessagesLabel;
		private System.Windows.Forms.TextBox UdpPortMessagesTextBox;
		private System.Windows.Forms.Label RunAsAdministratorLabel;
		private System.Windows.Forms.Panel AsAdministratorPanel;
	}
}
