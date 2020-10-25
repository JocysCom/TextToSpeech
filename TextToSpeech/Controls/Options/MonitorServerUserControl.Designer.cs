namespace JocysCom.TextToSpeech.Monitor.Controls
{
    partial class MonitorServerUserControl
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
			this.UdpEnabledCheckBox = new System.Windows.Forms.CheckBox();
			this.UdpPortNumberNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.TestUdpPortTestButton = new System.Windows.Forms.Button();
			this.UdpPortMessagesTextBox = new System.Windows.Forms.TextBox();
			this.UdpPortNumberLabel = new System.Windows.Forms.Label();
			this.MainSettingsGroupBox = new System.Windows.Forms.GroupBox();
			this.UdpMessagesLabel = new System.Windows.Forms.Label();
			this.MonitorMessageTextBox = new System.Windows.Forms.TextBox();
			this.SapiMessageTextBox = new System.Windows.Forms.TextBox();
			this.TestMessageGroupBox = new System.Windows.Forms.GroupBox();
			this.SapiVoiceRadioButton = new System.Windows.Forms.RadioButton();
			this.MonitorMessageRadioButton = new System.Windows.Forms.RadioButton();
			this.CodeExampleTextBox = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.UdpPortNumberNumericUpDown)).BeginInit();
			this.MainSettingsGroupBox.SuspendLayout();
			this.TestMessageGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// UdpEnabledCheckBox
			// 
			this.UdpEnabledCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.UdpEnabledCheckBox.AutoSize = true;
			this.UdpEnabledCheckBox.Location = new System.Drawing.Point(105, 19);
			this.UdpEnabledCheckBox.Name = "UdpEnabledCheckBox";
			this.UdpEnabledCheckBox.Size = new System.Drawing.Size(59, 17);
			this.UdpEnabledCheckBox.TabIndex = 0;
			this.UdpEnabledCheckBox.Text = "Enable";
			this.UdpEnabledCheckBox.UseVisualStyleBackColor = true;
			// 
			// UdpPortNumberNumericUpDown
			// 
			this.UdpPortNumberNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.UdpPortNumberNumericUpDown.Location = new System.Drawing.Point(105, 68);
			this.UdpPortNumberNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.UdpPortNumberNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.UdpPortNumberNumericUpDown.Name = "UdpPortNumberNumericUpDown";
			this.UdpPortNumberNumericUpDown.Size = new System.Drawing.Size(58, 20);
			this.UdpPortNumberNumericUpDown.TabIndex = 1;
			this.UdpPortNumberNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.UdpPortNumberNumericUpDown.Value = new decimal(new int[] {
            42500,
            0,
            0,
            0});
			this.UdpPortNumberNumericUpDown.ValueChanged += new System.EventHandler(this.UdpPortNumberNumericUpDown_ValueChanged);
			// 
			// TestUdpPortTestButton
			// 
			this.TestUdpPortTestButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.TestUdpPortTestButton.Location = new System.Drawing.Point(105, 91);
			this.TestUdpPortTestButton.Name = "TestUdpPortTestButton";
			this.TestUdpPortTestButton.Size = new System.Drawing.Size(58, 23);
			this.TestUdpPortTestButton.TabIndex = 2;
			this.TestUdpPortTestButton.Text = "Test";
			this.TestUdpPortTestButton.UseVisualStyleBackColor = true;
			this.TestUdpPortTestButton.Click += new System.EventHandler(this.MonitorUdpPortTestButton_Click);
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
			// UdpPortNumberLabel
			// 
			this.UdpPortNumberLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.UdpPortNumberLabel.AutoSize = true;
			this.UdpPortNumberLabel.Location = new System.Drawing.Point(10, 70);
			this.UdpPortNumberLabel.Name = "UdpPortNumberLabel";
			this.UdpPortNumberLabel.Size = new System.Drawing.Size(95, 13);
			this.UdpPortNumberLabel.TabIndex = 4;
			this.UdpPortNumberLabel.Text = "UDP Port Number:";
			// 
			// MainSettingsGroupBox
			// 
			this.MainSettingsGroupBox.Controls.Add(this.UdpMessagesLabel);
			this.MainSettingsGroupBox.Controls.Add(this.TestUdpPortTestButton);
			this.MainSettingsGroupBox.Controls.Add(this.UdpEnabledCheckBox);
			this.MainSettingsGroupBox.Controls.Add(this.UdpPortMessagesTextBox);
			this.MainSettingsGroupBox.Controls.Add(this.UdpPortNumberNumericUpDown);
			this.MainSettingsGroupBox.Controls.Add(this.UdpPortNumberLabel);
			this.MainSettingsGroupBox.Location = new System.Drawing.Point(3, 3);
			this.MainSettingsGroupBox.Name = "MainSettingsGroupBox";
			this.MainSettingsGroupBox.Size = new System.Drawing.Size(170, 120);
			this.MainSettingsGroupBox.TabIndex = 5;
			this.MainSettingsGroupBox.TabStop = false;
			this.MainSettingsGroupBox.Text = "Main Settings";
			// 
			// UdpMessagesLabel
			// 
			this.UdpMessagesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.UdpMessagesLabel.AutoSize = true;
			this.UdpMessagesLabel.Location = new System.Drawing.Point(41, 45);
			this.UdpMessagesLabel.Name = "UdpMessagesLabel";
			this.UdpMessagesLabel.Size = new System.Drawing.Size(58, 13);
			this.UdpMessagesLabel.TabIndex = 6;
			this.UdpMessagesLabel.Text = "Messages:";
			// 
			// MonitorMessageTextBox
			// 
			this.MonitorMessageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MonitorMessageTextBox.Location = new System.Drawing.Point(142, 51);
			this.MonitorMessageTextBox.Multiline = true;
			this.MonitorMessageTextBox.Name = "MonitorMessageTextBox";
			this.MonitorMessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.MonitorMessageTextBox.Size = new System.Drawing.Size(596, 37);
			this.MonitorMessageTextBox.TabIndex = 3;
			this.MonitorMessageTextBox.Text = "<message name=\"Marshal McBride\" gender=\"Male\" effect=\"Humanoid\" group=\"Quest\" pit" +
    "ch=\"0\" rate=\"1\" volume=\"100\" command=\"Play\"><part>Test Monitor text to speech.</" +
    "part></message>";
			// 
			// SapiMessageTextBox
			// 
			this.SapiMessageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SapiMessageTextBox.Location = new System.Drawing.Point(142, 10);
			this.SapiMessageTextBox.Multiline = true;
			this.SapiMessageTextBox.Name = "SapiMessageTextBox";
			this.SapiMessageTextBox.ReadOnly = true;
			this.SapiMessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.SapiMessageTextBox.Size = new System.Drawing.Size(596, 35);
			this.SapiMessageTextBox.TabIndex = 3;
			this.SapiMessageTextBox.Text = "<voice required=\"name=IVONA 2 Brian\"><volume level=\"100\"><rate absspeed=\"0\"><pitc" +
    "h absmiddle=\"0\">Test SAPI text to speech.</pitch></rate></volume></voice>";
			// 
			// TestMessageGroupBox
			// 
			this.TestMessageGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TestMessageGroupBox.Controls.Add(this.SapiVoiceRadioButton);
			this.TestMessageGroupBox.Controls.Add(this.MonitorMessageRadioButton);
			this.TestMessageGroupBox.Controls.Add(this.MonitorMessageTextBox);
			this.TestMessageGroupBox.Controls.Add(this.SapiMessageTextBox);
			this.TestMessageGroupBox.Location = new System.Drawing.Point(3, 129);
			this.TestMessageGroupBox.Name = "TestMessageGroupBox";
			this.TestMessageGroupBox.Size = new System.Drawing.Size(741, 107);
			this.TestMessageGroupBox.TabIndex = 5;
			this.TestMessageGroupBox.TabStop = false;
			this.TestMessageGroupBox.Text = "Test Message";
			this.TestMessageGroupBox.Enter += new System.EventHandler(this.TestMessageGroupBox_Enter);
			// 
			// SapiVoiceRadioButton
			// 
			this.SapiVoiceRadioButton.AutoSize = true;
			this.SapiVoiceRadioButton.Location = new System.Drawing.Point(6, 20);
			this.SapiVoiceRadioButton.Name = "SapiVoiceRadioButton";
			this.SapiVoiceRadioButton.Size = new System.Drawing.Size(103, 17);
			this.SapiVoiceRadioButton.TabIndex = 4;
			this.SapiVoiceRadioButton.Text = "Use SAPI voice:";
			this.SapiVoiceRadioButton.UseVisualStyleBackColor = true;
			this.SapiVoiceRadioButton.CheckedChanged += new System.EventHandler(this.SapiVoiceRadioButton_CheckedChanged);
			// 
			// MonitorMessageRadioButton
			// 
			this.MonitorMessageRadioButton.AutoSize = true;
			this.MonitorMessageRadioButton.Checked = true;
			this.MonitorMessageRadioButton.Location = new System.Drawing.Point(6, 61);
			this.MonitorMessageRadioButton.Name = "MonitorMessageRadioButton";
			this.MonitorMessageRadioButton.Size = new System.Drawing.Size(130, 17);
			this.MonitorMessageRadioButton.TabIndex = 4;
			this.MonitorMessageRadioButton.TabStop = true;
			this.MonitorMessageRadioButton.Text = "Use Monitor message:";
			this.MonitorMessageRadioButton.UseVisualStyleBackColor = true;
			// 
			// CodeExampleTextBox
			// 
			this.CodeExampleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CodeExampleTextBox.Location = new System.Drawing.Point(179, 3);
			this.CodeExampleTextBox.Multiline = true;
			this.CodeExampleTextBox.Name = "CodeExampleTextBox";
			this.CodeExampleTextBox.ReadOnly = true;
			this.CodeExampleTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.CodeExampleTextBox.Size = new System.Drawing.Size(565, 120);
			this.CodeExampleTextBox.TabIndex = 6;
			// 
			// MonitorServerUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.CodeExampleTextBox);
			this.Controls.Add(this.TestMessageGroupBox);
			this.Controls.Add(this.MainSettingsGroupBox);
			this.Name = "MonitorServerUserControl";
			this.Size = new System.Drawing.Size(747, 240);
			this.Load += new System.EventHandler(this.MonitorServerUserControl_Load);
			((System.ComponentModel.ISupportInitialize)(this.UdpPortNumberNumericUpDown)).EndInit();
			this.MainSettingsGroupBox.ResumeLayout(false);
			this.MainSettingsGroupBox.PerformLayout();
			this.TestMessageGroupBox.ResumeLayout(false);
			this.TestMessageGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox UdpEnabledCheckBox;
        private System.Windows.Forms.NumericUpDown UdpPortNumberNumericUpDown;
        private System.Windows.Forms.Button TestUdpPortTestButton;
        private System.Windows.Forms.TextBox UdpPortMessagesTextBox;
        private System.Windows.Forms.Label UdpPortNumberLabel;
        private System.Windows.Forms.GroupBox MainSettingsGroupBox;
        private System.Windows.Forms.Label UdpMessagesLabel;
        private System.Windows.Forms.TextBox MonitorMessageTextBox;
        private System.Windows.Forms.TextBox SapiMessageTextBox;
        private System.Windows.Forms.GroupBox TestMessageGroupBox;
        private System.Windows.Forms.RadioButton SapiVoiceRadioButton;
        private System.Windows.Forms.RadioButton MonitorMessageRadioButton;
        private System.Windows.Forms.TextBox CodeExampleTextBox;
    }
}
