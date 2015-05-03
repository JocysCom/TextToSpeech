namespace JocysCom.TextToSpeech.Monitor.Controls
{
    partial class MonitorsUserControl
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
            this.UdpEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.UdpPortNumberNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.TestUdpPortTestButton = new System.Windows.Forms.Button();
            this.UdpPortMessagesTextBox = new System.Windows.Forms.TextBox();
            this.UdpPortNumberLabel = new System.Windows.Forms.Label();
            this.MonitorUdpPortGroupBox = new System.Windows.Forms.GroupBox();
            this.UdpMessagesLabel = new System.Windows.Forms.Label();
            this.MonitorMessageTextBox = new System.Windows.Forms.TextBox();
            this.SapiMessageTextBox = new System.Windows.Forms.TextBox();
            this.TestMessageGroupBox = new System.Windows.Forms.GroupBox();
            this.SapiVoiceRadioButton = new System.Windows.Forms.RadioButton();
            this.MonitorMessageRadioButton = new System.Windows.Forms.RadioButton();
            this.CodeExampleTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.UdpPortNumberNumericUpDown)).BeginInit();
            this.MonitorUdpPortGroupBox.SuspendLayout();
            this.TestMessageGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // UdpEnabledCheckBox
            // 
            this.UdpEnabledCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UdpEnabledCheckBox.AutoSize = true;
            this.UdpEnabledCheckBox.Location = new System.Drawing.Point(101, 19);
            this.UdpEnabledCheckBox.Name = "UdpEnabledCheckBox";
            this.UdpEnabledCheckBox.Size = new System.Drawing.Size(59, 17);
            this.UdpEnabledCheckBox.TabIndex = 0;
            this.UdpEnabledCheckBox.Text = "Enable";
            this.UdpEnabledCheckBox.UseVisualStyleBackColor = true;
            this.UdpEnabledCheckBox.CheckedChanged += new System.EventHandler(this.MonitorUdpPortCheckBox_CheckedChanged);
            // 
            // UdpPortNumberNumericUpDown
            // 
            this.UdpPortNumberNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UdpPortNumberNumericUpDown.Location = new System.Drawing.Point(101, 68);
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
            this.TestUdpPortTestButton.Location = new System.Drawing.Point(102, 94);
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
            this.UdpPortMessagesTextBox.Location = new System.Drawing.Point(101, 42);
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
            this.UdpPortNumberLabel.Location = new System.Drawing.Point(26, 70);
            this.UdpPortNumberLabel.Name = "UdpPortNumberLabel";
            this.UdpPortNumberLabel.Size = new System.Drawing.Size(69, 13);
            this.UdpPortNumberLabel.TabIndex = 4;
            this.UdpPortNumberLabel.Text = "Port Number:";
            // 
            // MonitorUdpPortGroupBox
            // 
            this.MonitorUdpPortGroupBox.Controls.Add(this.UdpMessagesLabel);
            this.MonitorUdpPortGroupBox.Controls.Add(this.TestUdpPortTestButton);
            this.MonitorUdpPortGroupBox.Controls.Add(this.UdpEnabledCheckBox);
            this.MonitorUdpPortGroupBox.Controls.Add(this.UdpPortMessagesTextBox);
            this.MonitorUdpPortGroupBox.Controls.Add(this.UdpPortNumberLabel);
            this.MonitorUdpPortGroupBox.Controls.Add(this.UdpPortNumberNumericUpDown);
            this.MonitorUdpPortGroupBox.Location = new System.Drawing.Point(3, 3);
            this.MonitorUdpPortGroupBox.Name = "MonitorUdpPortGroupBox";
            this.MonitorUdpPortGroupBox.Size = new System.Drawing.Size(166, 130);
            this.MonitorUdpPortGroupBox.TabIndex = 5;
            this.MonitorUdpPortGroupBox.TabStop = false;
            this.MonitorUdpPortGroupBox.Text = "Monitor UDP Port (127.0.0.1)";
            // 
            // UdpMessagesLabel
            // 
            this.UdpMessagesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UdpMessagesLabel.AutoSize = true;
            this.UdpMessagesLabel.Location = new System.Drawing.Point(37, 45);
            this.UdpMessagesLabel.Name = "UdpMessagesLabel";
            this.UdpMessagesLabel.Size = new System.Drawing.Size(58, 13);
            this.UdpMessagesLabel.TabIndex = 6;
            this.UdpMessagesLabel.Text = "Messages:";
            // 
            // MonitorMessageTextBox
            // 
            this.MonitorMessageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MonitorMessageTextBox.Location = new System.Drawing.Point(142, 73);
            this.MonitorMessageTextBox.Multiline = true;
            this.MonitorMessageTextBox.Name = "MonitorMessageTextBox";
            this.MonitorMessageTextBox.Size = new System.Drawing.Size(617, 65);
            this.MonitorMessageTextBox.TabIndex = 3;
            this.MonitorMessageTextBox.Text = "<message name=\"Marshal McBride\" gender=\"Male\" effect=\"Humanoid\" group=\"Quest\" pit" +
    "ch=\"0\" rate=\"1\" volume=\"100\" command=\"Play\"><part>Test Monitor text to speech.</" +
    "part></message>";
            // 
            // SapiMessageTextBox
            // 
            this.SapiMessageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SapiMessageTextBox.Location = new System.Drawing.Point(142, 19);
            this.SapiMessageTextBox.Multiline = true;
            this.SapiMessageTextBox.Name = "SapiMessageTextBox";
            this.SapiMessageTextBox.ReadOnly = true;
            this.SapiMessageTextBox.Size = new System.Drawing.Size(617, 48);
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
            this.TestMessageGroupBox.Location = new System.Drawing.Point(3, 139);
            this.TestMessageGroupBox.Name = "TestMessageGroupBox";
            this.TestMessageGroupBox.Size = new System.Drawing.Size(765, 144);
            this.TestMessageGroupBox.TabIndex = 5;
            this.TestMessageGroupBox.TabStop = false;
            this.TestMessageGroupBox.Text = "Test Message";
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
            this.MonitorMessageRadioButton.Location = new System.Drawing.Point(6, 74);
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
            this.CodeExampleTextBox.Location = new System.Drawing.Point(176, 3);
            this.CodeExampleTextBox.Multiline = true;
            this.CodeExampleTextBox.Name = "CodeExampleTextBox";
            this.CodeExampleTextBox.ReadOnly = true;
            this.CodeExampleTextBox.Size = new System.Drawing.Size(592, 130);
            this.CodeExampleTextBox.TabIndex = 6;
            // 
            // MonitorsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CodeExampleTextBox);
            this.Controls.Add(this.TestMessageGroupBox);
            this.Controls.Add(this.MonitorUdpPortGroupBox);
            this.Name = "MonitorsUserControl";
            this.Size = new System.Drawing.Size(771, 286);
            ((System.ComponentModel.ISupportInitialize)(this.UdpPortNumberNumericUpDown)).EndInit();
            this.MonitorUdpPortGroupBox.ResumeLayout(false);
            this.MonitorUdpPortGroupBox.PerformLayout();
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
        private System.Windows.Forms.GroupBox MonitorUdpPortGroupBox;
        private System.Windows.Forms.Label UdpMessagesLabel;
        private System.Windows.Forms.TextBox MonitorMessageTextBox;
        private System.Windows.Forms.TextBox SapiMessageTextBox;
        private System.Windows.Forms.GroupBox TestMessageGroupBox;
        private System.Windows.Forms.RadioButton SapiVoiceRadioButton;
        private System.Windows.Forms.RadioButton MonitorMessageRadioButton;
        private System.Windows.Forms.TextBox CodeExampleTextBox;
    }
}
