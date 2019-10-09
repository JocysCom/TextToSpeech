namespace JocysCom.TextToSpeech.Monitor.Google
{
	partial class GoogleTTSUserControl
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
			this.GetKeyButton = new System.Windows.Forms.Button();
			this.TestButton = new System.Windows.Forms.Button();
			this.ApiKeyLabel = new System.Windows.Forms.Label();
			this.WebAppClientSecretLabel = new System.Windows.Forms.Label();
			this.CodeLabel = new System.Windows.Forms.Label();
			this.ApiKeyTextBox = new System.Windows.Forms.TextBox();
			this.WebAppClientSecretTextBox = new System.Windows.Forms.TextBox();
			this.CodeTextBox = new System.Windows.Forms.TextBox();
			this.GetCredentialsButton = new System.Windows.Forms.Button();
			this.AuthenticationGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// WebAppClientIdLabel
			// 
			this.WebAppClientIdLabel.AutoSize = true;
			this.WebAppClientIdLabel.Location = new System.Drawing.Point(6, 48);
			this.WebAppClientIdLabel.Name = "WebAppClientIdLabel";
			this.WebAppClientIdLabel.Size = new System.Drawing.Size(135, 13);
			this.WebAppClientIdLabel.TabIndex = 12;
			this.WebAppClientIdLabel.Text = "Google Web App Client ID:";
			// 
			// WebAppClientIdTextBox
			// 
			this.WebAppClientIdTextBox.Location = new System.Drawing.Point(167, 45);
			this.WebAppClientIdTextBox.Name = "WebAppClientIdTextBox";
			this.WebAppClientIdTextBox.Size = new System.Drawing.Size(392, 20);
			this.WebAppClientIdTextBox.TabIndex = 11;
			// 
			// AuthenticationGroupBox
			// 
			this.AuthenticationGroupBox.Controls.Add(this.GetCredentialsButton);
			this.AuthenticationGroupBox.Controls.Add(this.GetKeyButton);
			this.AuthenticationGroupBox.Controls.Add(this.TestButton);
			this.AuthenticationGroupBox.Controls.Add(this.ApiKeyLabel);
			this.AuthenticationGroupBox.Controls.Add(this.WebAppClientSecretLabel);
			this.AuthenticationGroupBox.Controls.Add(this.CodeLabel);
			this.AuthenticationGroupBox.Controls.Add(this.WebAppClientIdLabel);
			this.AuthenticationGroupBox.Controls.Add(this.ApiKeyTextBox);
			this.AuthenticationGroupBox.Controls.Add(this.WebAppClientSecretTextBox);
			this.AuthenticationGroupBox.Controls.Add(this.CodeTextBox);
			this.AuthenticationGroupBox.Controls.Add(this.WebAppClientIdTextBox);
			this.AuthenticationGroupBox.Location = new System.Drawing.Point(3, 3);
			this.AuthenticationGroupBox.Name = "AuthenticationGroupBox";
			this.AuthenticationGroupBox.Size = new System.Drawing.Size(565, 171);
			this.AuthenticationGroupBox.TabIndex = 13;
			this.AuthenticationGroupBox.TabStop = false;
			this.AuthenticationGroupBox.Text = "Authentication";
			// 
			// GetKeyButton
			// 
			this.GetKeyButton.Location = new System.Drawing.Point(167, 97);
			this.GetKeyButton.Name = "GetKeyButton";
			this.GetKeyButton.Size = new System.Drawing.Size(75, 23);
			this.GetKeyButton.TabIndex = 13;
			this.GetKeyButton.Text = "Get Key";
			this.GetKeyButton.UseVisualStyleBackColor = true;
			this.GetKeyButton.Click += new System.EventHandler(this.GetKeyButton_Click);
			// 
			// TestButton
			// 
			this.TestButton.Location = new System.Drawing.Point(329, 97);
			this.TestButton.Name = "TestButton";
			this.TestButton.Size = new System.Drawing.Size(75, 23);
			this.TestButton.TabIndex = 13;
			this.TestButton.Text = "Test";
			this.TestButton.UseVisualStyleBackColor = true;
			this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
			// 
			// ApiKeyLabel
			// 
			this.ApiKeyLabel.AutoSize = true;
			this.ApiKeyLabel.Location = new System.Drawing.Point(6, 129);
			this.ApiKeyLabel.Name = "ApiKeyLabel";
			this.ApiKeyLabel.Size = new System.Drawing.Size(109, 13);
			this.ApiKeyLabel.TabIndex = 12;
			this.ApiKeyLabel.Text = "Google TTS API Key:";
			// 
			// WebAppClientSecretLabel
			// 
			this.WebAppClientSecretLabel.AutoSize = true;
			this.WebAppClientSecretLabel.Location = new System.Drawing.Point(6, 74);
			this.WebAppClientSecretLabel.Name = "WebAppClientSecretLabel";
			this.WebAppClientSecretLabel.Size = new System.Drawing.Size(155, 13);
			this.WebAppClientSecretLabel.TabIndex = 12;
			this.WebAppClientSecretLabel.Text = "Google Web App Client Secret:";
			// 
			// CodeLabel
			// 
			this.CodeLabel.AutoSize = true;
			this.CodeLabel.Location = new System.Drawing.Point(6, 22);
			this.CodeLabel.Name = "CodeLabel";
			this.CodeLabel.Size = new System.Drawing.Size(72, 13);
			this.CodeLabel.TabIndex = 12;
			this.CodeLabel.Text = "Google Code:";
			// 
			// ApiKeyTextBox
			// 
			this.ApiKeyTextBox.Location = new System.Drawing.Point(167, 126);
			this.ApiKeyTextBox.Name = "ApiKeyTextBox";
			this.ApiKeyTextBox.Size = new System.Drawing.Size(392, 20);
			this.ApiKeyTextBox.TabIndex = 11;
			// 
			// WebAppClientSecretTextBox
			// 
			this.WebAppClientSecretTextBox.Location = new System.Drawing.Point(167, 71);
			this.WebAppClientSecretTextBox.Name = "WebAppClientSecretTextBox";
			this.WebAppClientSecretTextBox.Size = new System.Drawing.Size(392, 20);
			this.WebAppClientSecretTextBox.TabIndex = 11;
			// 
			// CodeTextBox
			// 
			this.CodeTextBox.Location = new System.Drawing.Point(167, 19);
			this.CodeTextBox.Name = "CodeTextBox";
			this.CodeTextBox.Size = new System.Drawing.Size(392, 20);
			this.CodeTextBox.TabIndex = 11;
			// 
			// GetCredentialsButton
			// 
			this.GetCredentialsButton.Location = new System.Drawing.Point(248, 97);
			this.GetCredentialsButton.Name = "GetCredentialsButton";
			this.GetCredentialsButton.Size = new System.Drawing.Size(75, 23);
			this.GetCredentialsButton.TabIndex = 13;
			this.GetCredentialsButton.Text = "Get Cred";
			this.GetCredentialsButton.UseVisualStyleBackColor = true;
			this.GetCredentialsButton.Click += new System.EventHandler(this.GetCredentialsButton_Click);
			// 
			// GoogleOptionsUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.AuthenticationGroupBox);
			this.Name = "GoogleOptionsUserControl";
			this.Size = new System.Drawing.Size(617, 246);
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
		private System.Windows.Forms.Label CodeLabel;
		private System.Windows.Forms.TextBox CodeTextBox;
		private System.Windows.Forms.Label ApiKeyLabel;
		private System.Windows.Forms.TextBox ApiKeyTextBox;
		private System.Windows.Forms.Button GetKeyButton;
		private System.Windows.Forms.Button GetCredentialsButton;
	}
}
