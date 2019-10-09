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
			this.SecurityGroupBox.SuspendLayout();
			this.HelpGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// SecretKeyLabel
			// 
			this.SecretKeyLabel.AutoSize = true;
			this.SecretKeyLabel.Location = new System.Drawing.Point(6, 48);
			this.SecretKeyLabel.Name = "SecretKeyLabel";
			this.SecretKeyLabel.Size = new System.Drawing.Size(62, 13);
			this.SecretKeyLabel.TabIndex = 15;
			this.SecretKeyLabel.Text = "Secret Key:";
			// 
			// AccessKeyLabel
			// 
			this.AccessKeyLabel.AutoSize = true;
			this.AccessKeyLabel.Location = new System.Drawing.Point(6, 22);
			this.AccessKeyLabel.Name = "AccessKeyLabel";
			this.AccessKeyLabel.Size = new System.Drawing.Size(66, 13);
			this.AccessKeyLabel.TabIndex = 16;
			this.AccessKeyLabel.Text = "Access Key:";
			// 
			// SecretKeyTextBox
			// 
			this.SecretKeyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SecretKeyTextBox.Location = new System.Drawing.Point(78, 45);
			this.SecretKeyTextBox.Name = "SecretKeyTextBox";
			this.SecretKeyTextBox.Size = new System.Drawing.Size(297, 20);
			this.SecretKeyTextBox.TabIndex = 13;
			this.SecretKeyTextBox.UseSystemPasswordChar = true;
			// 
			// AccessKeyTextBox
			// 
			this.AccessKeyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.AccessKeyTextBox.Location = new System.Drawing.Point(78, 19);
			this.AccessKeyTextBox.Name = "AccessKeyTextBox";
			this.AccessKeyTextBox.Size = new System.Drawing.Size(297, 20);
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
			this.SecurityGroupBox.Size = new System.Drawing.Size(381, 100);
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
			this.HelpGroupBox.Size = new System.Drawing.Size(370, 167);
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
			this.Help2Label.Size = new System.Drawing.Size(358, 96);
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
			// AmazonPollyUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.HelpGroupBox);
			this.Controls.Add(this.SecurityGroupBox);
			this.Name = "AmazonPollyUserControl";
			this.Size = new System.Drawing.Size(763, 220);
			this.SecurityGroupBox.ResumeLayout(false);
			this.SecurityGroupBox.PerformLayout();
			this.HelpGroupBox.ResumeLayout(false);
			this.HelpGroupBox.PerformLayout();
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
	}
}
