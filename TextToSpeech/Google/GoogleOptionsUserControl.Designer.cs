namespace JocysCom.TextToSpeech.Monitor.Google
{
	partial class GoogleOptionsUserControl
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
			this.WebAppClientIdLabel = new System.Windows.Forms.Label();
			this.WebAppClientIdTextBox = new System.Windows.Forms.TextBox();
			this.AuthenticationGroupBox = new System.Windows.Forms.GroupBox();
			this.WebAppClientSecretTextBox = new System.Windows.Forms.TextBox();
			this.WebAppClientSecretLabel = new System.Windows.Forms.Label();
			this.TestButton = new System.Windows.Forms.Button();
			this.AuthenticationGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// WebAppClientIdLabel
			// 
			this.WebAppClientIdLabel.AutoSize = true;
			this.WebAppClientIdLabel.Location = new System.Drawing.Point(6, 22);
			this.WebAppClientIdLabel.Name = "WebAppClientIdLabel";
			this.WebAppClientIdLabel.Size = new System.Drawing.Size(135, 13);
			this.WebAppClientIdLabel.TabIndex = 12;
			this.WebAppClientIdLabel.Text = "Google Web App Client ID:";
			// 
			// WebAppClientIdTextBox
			// 
			this.WebAppClientIdTextBox.Location = new System.Drawing.Point(167, 19);
			this.WebAppClientIdTextBox.Name = "WebAppClientIdTextBox";
			this.WebAppClientIdTextBox.Size = new System.Drawing.Size(392, 20);
			this.WebAppClientIdTextBox.TabIndex = 11;
			// 
			// AuthenticationGroupBox
			// 
			this.AuthenticationGroupBox.Controls.Add(this.TestButton);
			this.AuthenticationGroupBox.Controls.Add(this.WebAppClientSecretLabel);
			this.AuthenticationGroupBox.Controls.Add(this.WebAppClientIdLabel);
			this.AuthenticationGroupBox.Controls.Add(this.WebAppClientSecretTextBox);
			this.AuthenticationGroupBox.Controls.Add(this.WebAppClientIdTextBox);
			this.AuthenticationGroupBox.Location = new System.Drawing.Point(3, 3);
			this.AuthenticationGroupBox.Name = "AuthenticationGroupBox";
			this.AuthenticationGroupBox.Size = new System.Drawing.Size(565, 171);
			this.AuthenticationGroupBox.TabIndex = 13;
			this.AuthenticationGroupBox.TabStop = false;
			this.AuthenticationGroupBox.Text = "Authentication";
			// 
			// WebAppClientSecretTextBox
			// 
			this.WebAppClientSecretTextBox.Location = new System.Drawing.Point(167, 45);
			this.WebAppClientSecretTextBox.Name = "WebAppClientSecretTextBox";
			this.WebAppClientSecretTextBox.Size = new System.Drawing.Size(392, 20);
			this.WebAppClientSecretTextBox.TabIndex = 11;
			// 
			// WebAppClientSecretLabel
			// 
			this.WebAppClientSecretLabel.AutoSize = true;
			this.WebAppClientSecretLabel.Location = new System.Drawing.Point(6, 48);
			this.WebAppClientSecretLabel.Name = "WebAppClientSecretLabel";
			this.WebAppClientSecretLabel.Size = new System.Drawing.Size(155, 13);
			this.WebAppClientSecretLabel.TabIndex = 12;
			this.WebAppClientSecretLabel.Text = "Google Web App Client Secret:";
			// 
			// TestButton
			// 
			this.TestButton.Location = new System.Drawing.Point(167, 71);
			this.TestButton.Name = "TestButton";
			this.TestButton.Size = new System.Drawing.Size(75, 23);
			this.TestButton.TabIndex = 13;
			this.TestButton.Text = "Test";
			this.TestButton.UseVisualStyleBackColor = true;
			this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
			// 
			// GoogleOptionsUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.AuthenticationGroupBox);
			this.Name = "GoogleOptionsUserControl";
			this.Size = new System.Drawing.Size(797, 399);
			this.Load += new System.EventHandler(this.GoogleOptionsUserControl_Load);
			this.AuthenticationGroupBox.ResumeLayout(false);
			this.AuthenticationGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label WebAppClientIdLabel;
		private System.Windows.Forms.TextBox WebAppClientIdTextBox;
		private System.Windows.Forms.GroupBox AuthenticationGroupBox;
		private System.Windows.Forms.Label WebAppClientSecretLabel;
		private System.Windows.Forms.TextBox WebAppClientSecretTextBox;
		private System.Windows.Forms.Button TestButton;
	}
}
