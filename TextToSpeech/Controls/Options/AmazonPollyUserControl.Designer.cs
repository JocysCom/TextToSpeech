namespace JocysCom.TextToSpeech.Monitor.Controls.Options
{
	partial class AmazonPollyUserControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AmazonPollyUserControl));
			this.SecretKeyLabel = new System.Windows.Forms.Label();
			this.AccessKeyLabel = new System.Windows.Forms.Label();
			this.SecretKeyTextBox = new System.Windows.Forms.TextBox();
			this.AccessKeyTextBox = new System.Windows.Forms.TextBox();
			this.SecurityGroupBox = new System.Windows.Forms.GroupBox();
			this.HelpLabel = new System.Windows.Forms.Label();
			this.HelpGroupBox = new System.Windows.Forms.GroupBox();
			this.Help2Label = new System.Windows.Forms.Label();
			this.AwsLinkLabel = new System.Windows.Forms.LinkLabel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.RegionComboBox = new System.Windows.Forms.ComboBox();
			this.AmazonEnabledCheckBox = new System.Windows.Forms.CheckBox();
			this.TextGroupBox = new System.Windows.Forms.GroupBox();
			this.VoicesComboBox = new System.Windows.Forms.ComboBox();
			this.MessageLabel = new System.Windows.Forms.Label();
			this.MessageTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.RefreshVoicesButton = new System.Windows.Forms.Button();
			this.SpeakButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SecurityGroupBox.SuspendLayout();
			this.HelpGroupBox.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.TextGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// SecretKeyLabel
			// 
			this.SecretKeyLabel.AutoSize = true;
			this.SecretKeyLabel.Location = new System.Drawing.Point(6, 48);
			this.SecretKeyLabel.Name = "SecretKeyLabel";
			this.SecretKeyLabel.Size = new System.Drawing.Size(59, 13);
			this.SecretKeyLabel.TabIndex = 15;
			this.SecretKeyLabel.Text = "Secret Key";
			// 
			// AccessKeyLabel
			// 
			this.AccessKeyLabel.AutoSize = true;
			this.AccessKeyLabel.Location = new System.Drawing.Point(6, 22);
			this.AccessKeyLabel.Name = "AccessKeyLabel";
			this.AccessKeyLabel.Size = new System.Drawing.Size(63, 13);
			this.AccessKeyLabel.TabIndex = 16;
			this.AccessKeyLabel.Text = "Access Key";
			// 
			// SecretKeyTextBox
			// 
			this.SecretKeyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SecretKeyTextBox.Location = new System.Drawing.Point(75, 45);
			this.SecretKeyTextBox.Name = "SecretKeyTextBox";
			this.SecretKeyTextBox.Size = new System.Drawing.Size(300, 20);
			this.SecretKeyTextBox.TabIndex = 13;
			this.SecretKeyTextBox.UseSystemPasswordChar = true;
			// 
			// AccessKeyTextBox
			// 
			this.AccessKeyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.AccessKeyTextBox.Location = new System.Drawing.Point(75, 19);
			this.AccessKeyTextBox.Name = "AccessKeyTextBox";
			this.AccessKeyTextBox.Size = new System.Drawing.Size(300, 20);
			this.AccessKeyTextBox.TabIndex = 14;
			// 
			// SecurityGroupBox
			// 
			this.SecurityGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SecurityGroupBox.Controls.Add(this.AccessKeyTextBox);
			this.SecurityGroupBox.Controls.Add(this.SecretKeyLabel);
			this.SecurityGroupBox.Controls.Add(this.SecretKeyTextBox);
			this.SecurityGroupBox.Controls.Add(this.AccessKeyLabel);
			this.SecurityGroupBox.Location = new System.Drawing.Point(3, 3);
			this.SecurityGroupBox.Name = "SecurityGroupBox";
			this.SecurityGroupBox.Size = new System.Drawing.Size(381, 75);
			this.SecurityGroupBox.TabIndex = 17;
			this.SecurityGroupBox.TabStop = false;
			this.SecurityGroupBox.Text = "Security";
			// 
			// HelpLabel
			// 
			this.HelpLabel.AutoSize = true;
			this.HelpLabel.Location = new System.Drawing.Point(6, 22);
			this.HelpLabel.Name = "HelpLabel";
			this.HelpLabel.Size = new System.Drawing.Size(149, 13);
			this.HelpLabel.TabIndex = 18;
			this.HelpLabel.Text = "Step 1: Create AWS Account:";
			// 
			// HelpGroupBox
			// 
			this.HelpGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.HelpGroupBox.Controls.Add(this.Help2Label);
			this.HelpGroupBox.Controls.Add(this.AwsLinkLabel);
			this.HelpGroupBox.Controls.Add(this.HelpLabel);
			this.HelpGroupBox.Location = new System.Drawing.Point(390, 3);
			this.HelpGroupBox.Name = "HelpGroupBox";
			this.HelpGroupBox.Size = new System.Drawing.Size(370, 156);
			this.HelpGroupBox.TabIndex = 19;
			this.HelpGroupBox.TabStop = false;
			this.HelpGroupBox.Text = "How To Use Amazon Polly";
			// 
			// Help2Label
			// 
			this.Help2Label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Help2Label.Location = new System.Drawing.Point(6, 68);
			this.Help2Label.Name = "Help2Label";
			this.Help2Label.Size = new System.Drawing.Size(358, 85);
			this.Help2Label.TabIndex = 20;
			this.Help2Label.Text = resources.GetString("Help2Label.Text");
			// 
			// AwsLinkLabel
			// 
			this.AwsLinkLabel.AutoSize = true;
			this.AwsLinkLabel.Location = new System.Drawing.Point(30, 48);
			this.AwsLinkLabel.Name = "AwsLinkLabel";
			this.AwsLinkLabel.Size = new System.Drawing.Size(125, 13);
			this.AwsLinkLabel.TabIndex = 19;
			this.AwsLinkLabel.TabStop = true;
			this.AwsLinkLabel.Text = "https://aws.amazon.com";
			this.AwsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AwsLinkLabel_LinkClicked);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.AmazonEnabledCheckBox);
			this.groupBox1.Controls.Add(this.RegionComboBox);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(3, 84);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(381, 75);
			this.groupBox1.TabIndex = 17;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Service";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 13);
			this.label2.TabIndex = 16;
			this.label2.Text = "Region";
			// 
			// RegionComboBox
			// 
			this.RegionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.RegionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RegionComboBox.FormattingEnabled = true;
			this.RegionComboBox.Location = new System.Drawing.Point(71, 42);
			this.RegionComboBox.Name = "RegionComboBox";
			this.RegionComboBox.Size = new System.Drawing.Size(304, 21);
			this.RegionComboBox.TabIndex = 17;
			// 
			// AmazonEnabledCheckBox
			// 
			this.AmazonEnabledCheckBox.AutoSize = true;
			this.AmazonEnabledCheckBox.Location = new System.Drawing.Point(71, 19);
			this.AmazonEnabledCheckBox.Name = "AmazonEnabledCheckBox";
			this.AmazonEnabledCheckBox.Size = new System.Drawing.Size(160, 17);
			this.AmazonEnabledCheckBox.TabIndex = 18;
			this.AmazonEnabledCheckBox.Text = "Enable Amazon Polly Voices";
			this.AmazonEnabledCheckBox.UseVisualStyleBackColor = true;
			// 
			// TextGroupBox
			// 
			this.TextGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextGroupBox.Controls.Add(this.label1);
			this.TextGroupBox.Controls.Add(this.SpeakButton);
			this.TextGroupBox.Controls.Add(this.RefreshVoicesButton);
			this.TextGroupBox.Controls.Add(this.VoicesComboBox);
			this.TextGroupBox.Controls.Add(this.MessageLabel);
			this.TextGroupBox.Controls.Add(this.MessageTextBox);
			this.TextGroupBox.Controls.Add(this.label4);
			this.TextGroupBox.Location = new System.Drawing.Point(3, 165);
			this.TextGroupBox.Name = "TextGroupBox";
			this.TextGroupBox.Size = new System.Drawing.Size(751, 150);
			this.TextGroupBox.TabIndex = 17;
			this.TextGroupBox.TabStop = false;
			this.TextGroupBox.Text = "Test";
			// 
			// VoicesComboBox
			// 
			this.VoicesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.VoicesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.VoicesComboBox.FormattingEnabled = true;
			this.VoicesComboBox.Location = new System.Drawing.Point(71, 16);
			this.VoicesComboBox.Name = "VoicesComboBox";
			this.VoicesComboBox.Size = new System.Drawing.Size(304, 21);
			this.VoicesComboBox.TabIndex = 17;
			// 
			// MessageLabel
			// 
			this.MessageLabel.AutoSize = true;
			this.MessageLabel.Location = new System.Drawing.Point(6, 46);
			this.MessageLabel.Name = "MessageLabel";
			this.MessageLabel.Size = new System.Drawing.Size(50, 13);
			this.MessageLabel.TabIndex = 15;
			this.MessageLabel.Text = "Message";
			// 
			// MessageTextBox
			// 
			this.MessageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MessageTextBox.Location = new System.Drawing.Point(71, 43);
			this.MessageTextBox.Name = "MessageTextBox";
			this.MessageTextBox.Size = new System.Drawing.Size(304, 20);
			this.MessageTextBox.TabIndex = 13;
			this.MessageTextBox.Text = "Hello world! Test Text to speech.";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 22);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 13);
			this.label4.TabIndex = 16;
			this.label4.Text = "Voices";
			// 
			// RefreshVoicesButton
			// 
			this.RefreshVoicesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RefreshVoicesButton.Location = new System.Drawing.Point(381, 15);
			this.RefreshVoicesButton.Name = "RefreshVoicesButton";
			this.RefreshVoicesButton.Size = new System.Drawing.Size(75, 23);
			this.RefreshVoicesButton.TabIndex = 18;
			this.RefreshVoicesButton.Text = "Refresh";
			this.RefreshVoicesButton.UseVisualStyleBackColor = true;
			this.RefreshVoicesButton.Click += new System.EventHandler(this.RefreshVoicesButton_Click);
			// 
			// SpeakButton
			// 
			this.SpeakButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SpeakButton.Location = new System.Drawing.Point(381, 41);
			this.SpeakButton.Name = "SpeakButton";
			this.SpeakButton.Size = new System.Drawing.Size(75, 23);
			this.SpeakButton.TabIndex = 18;
			this.SpeakButton.Text = "Speak";
			this.SpeakButton.UseVisualStyleBackColor = true;
			this.SpeakButton.Click += new System.EventHandler(this.SpeakButton_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(462, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(283, 69);
			this.label1.TabIndex = 20;
			this.label1.Text = "Neural voices are supported in the following Regions:\r\nUS East (N. Virginia): us-" +
    "east-1\r\nUS West (Oregon): us-west-2\r\nEU (Ireland): eu-west-1";
			// 
			// AmazonPollyUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.HelpGroupBox);
			this.Controls.Add(this.TextGroupBox);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.SecurityGroupBox);
			this.Name = "AmazonPollyUserControl";
			this.Size = new System.Drawing.Size(763, 318);
			this.SecurityGroupBox.ResumeLayout(false);
			this.SecurityGroupBox.PerformLayout();
			this.HelpGroupBox.ResumeLayout(false);
			this.HelpGroupBox.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.TextGroupBox.ResumeLayout(false);
			this.TextGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label SecretKeyLabel;
		private System.Windows.Forms.Label AccessKeyLabel;
		private System.Windows.Forms.TextBox SecretKeyTextBox;
		private System.Windows.Forms.TextBox AccessKeyTextBox;
		private System.Windows.Forms.GroupBox SecurityGroupBox;
		private System.Windows.Forms.Label HelpLabel;
		private System.Windows.Forms.GroupBox HelpGroupBox;
		private System.Windows.Forms.LinkLabel AwsLinkLabel;
		private System.Windows.Forms.Label Help2Label;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox RegionComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox AmazonEnabledCheckBox;
		private System.Windows.Forms.GroupBox TextGroupBox;
		private System.Windows.Forms.Button RefreshVoicesButton;
		private System.Windows.Forms.ComboBox VoicesComboBox;
		private System.Windows.Forms.Label MessageLabel;
		private System.Windows.Forms.TextBox MessageTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button SpeakButton;
		private System.Windows.Forms.Label label1;
	}
}
