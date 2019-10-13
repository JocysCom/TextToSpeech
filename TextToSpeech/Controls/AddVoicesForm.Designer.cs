namespace JocysCom.TextToSpeech.Monitor.Controls
{
	partial class AddVoicesForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddVoicesForm));
			this.MainTabControl = new System.Windows.Forms.TabControl();
			this.MainTabPage = new System.Windows.Forms.TabPage();
			this.OkButton = new System.Windows.Forms.Button();
			this.CloseButton = new System.Windows.Forms.Button();
			this.RefreshButton = new System.Windows.Forms.Button();
			this.VoicesPanel = new JocysCom.TextToSpeech.Monitor.Controls.VoicesUserControl();
			this.MainTabControl.SuspendLayout();
			this.MainTabPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainTabControl
			// 
			this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainTabControl.Controls.Add(this.MainTabPage);
			this.MainTabControl.Location = new System.Drawing.Point(12, 12);
			this.MainTabControl.Name = "MainTabControl";
			this.MainTabControl.SelectedIndex = 0;
			this.MainTabControl.Size = new System.Drawing.Size(638, 321);
			this.MainTabControl.TabIndex = 1;
			// 
			// MainTabPage
			// 
			this.MainTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.MainTabPage.Controls.Add(this.VoicesPanel);
			this.MainTabPage.Location = new System.Drawing.Point(4, 22);
			this.MainTabPage.Name = "MainTabPage";
			this.MainTabPage.Size = new System.Drawing.Size(630, 295);
			this.MainTabPage.TabIndex = 0;
			this.MainTabPage.Text = "Voices";
			// 
			// OkButton
			// 
			this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Add_16x16;
			this.OkButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.OkButton.Location = new System.Drawing.Point(438, 339);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(131, 23);
			this.OkButton.TabIndex = 2;
			this.OkButton.Text = "Add Selected Voices";
			this.OkButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.OkButton.UseVisualStyleBackColor = true;
			this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// CloseButton
			// 
			this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CloseButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Close_16x16;
			this.CloseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.CloseButton.Location = new System.Drawing.Point(575, 339);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(75, 23);
			this.CloseButton.TabIndex = 2;
			this.CloseButton.Text = "Cancel";
			this.CloseButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.CloseButton.UseVisualStyleBackColor = true;
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// RefreshButton
			// 
			this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.RefreshButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.reset_16x16;
			this.RefreshButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.RefreshButton.Location = new System.Drawing.Point(12, 339);
			this.RefreshButton.Name = "RefreshButton";
			this.RefreshButton.Size = new System.Drawing.Size(75, 23);
			this.RefreshButton.TabIndex = 3;
			this.RefreshButton.Text = "Refresh";
			this.RefreshButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.RefreshButton.UseVisualStyleBackColor = true;
			this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
			// 
			// VoicesPanel
			// 
			this.VoicesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.VoicesPanel.GenderRatesVisible = false;
			this.VoicesPanel.Location = new System.Drawing.Point(0, 0);
			this.VoicesPanel.MenuButtonsVisible = false;
			this.VoicesPanel.Name = "VoicesPanel";
			this.VoicesPanel.Size = new System.Drawing.Size(630, 295);
			this.VoicesPanel.TabIndex = 0;
			// 
			// AddVoicesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(662, 374);
			this.Controls.Add(this.RefreshButton);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.MainTabControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AddVoicesForm";
			this.Text = "Add Voices...";
			this.Load += new System.EventHandler(this.AddVoicesForm_Load);
			this.MainTabControl.ResumeLayout(false);
			this.MainTabPage.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private VoicesUserControl VoicesPanel;
		private System.Windows.Forms.TabControl MainTabControl;
		private System.Windows.Forms.TabPage MainTabPage;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.Button RefreshButton;
	}
}