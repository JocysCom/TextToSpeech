namespace JocysCom.TextToSpeech.Monitor.Controls
{
	partial class AddVoicesForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddVoicesForm));
			this.MainTabControl = new System.Windows.Forms.TabControl();
			this.VoicesTabPage = new System.Windows.Forms.TabPage();
			this.LogTabPage = new System.Windows.Forms.TabPage();
			this.BodyPanel = new System.Windows.Forms.Panel();
			this.StatusLabel = new System.Windows.Forms.Label();
			this.OkButton = new System.Windows.Forms.Button();
			this.CloseButton = new System.Windows.Forms.Button();
			this.RefreshButton = new System.Windows.Forms.Button();
			this.VoicesPanel = new JocysCom.TextToSpeech.Monitor.Controls.VoicesUserControl();
			this.MainTabControl.SuspendLayout();
			this.VoicesTabPage.SuspendLayout();
			this.LogTabPage.SuspendLayout();
			this.BodyPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainTabControl
			// 
			this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainTabControl.Controls.Add(this.VoicesTabPage);
			this.MainTabControl.Controls.Add(this.LogTabPage);
			this.MainTabControl.Location = new System.Drawing.Point(12, 12);
			this.MainTabControl.Name = "MainTabControl";
			this.MainTabControl.SelectedIndex = 0;
			this.MainTabControl.Size = new System.Drawing.Size(760, 388);
			this.MainTabControl.TabIndex = 1;
			// 
			// VoicesTabPage
			// 
			this.VoicesTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.VoicesTabPage.Controls.Add(this.VoicesPanel);
			this.VoicesTabPage.Location = new System.Drawing.Point(4, 22);
			this.VoicesTabPage.Name = "VoicesTabPage";
			this.VoicesTabPage.Size = new System.Drawing.Size(752, 362);
			this.VoicesTabPage.TabIndex = 0;
			this.VoicesTabPage.Text = "Voices";
			// 
			// LogTabPage
			// 
			this.LogTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.LogTabPage.Controls.Add(this.BodyPanel);
			this.LogTabPage.Location = new System.Drawing.Point(4, 22);
			this.LogTabPage.Name = "LogTabPage";
			this.LogTabPage.Size = new System.Drawing.Size(672, 362);
			this.LogTabPage.TabIndex = 1;
			this.LogTabPage.Text = "Log";
			// 
			// BodyPanel
			// 
			this.BodyPanel.AutoScroll = true;
			this.BodyPanel.AutoSize = true;
			this.BodyPanel.Controls.Add(this.StatusLabel);
			this.BodyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BodyPanel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BodyPanel.Location = new System.Drawing.Point(0, 0);
			this.BodyPanel.Name = "BodyPanel";
			this.BodyPanel.Size = new System.Drawing.Size(672, 362);
			this.BodyPanel.TabIndex = 6;
			// 
			// StatusLabel
			// 
			this.StatusLabel.AutoSize = true;
			this.StatusLabel.Location = new System.Drawing.Point(3, 3);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(25, 13);
			this.StatusLabel.TabIndex = 0;
			this.StatusLabel.Text = "...";
			// 
			// OkButton
			// 
			this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Add_16x16;
			this.OkButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.OkButton.Location = new System.Drawing.Point(560, 406);
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
			this.CloseButton.Location = new System.Drawing.Point(697, 406);
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
			this.RefreshButton.Location = new System.Drawing.Point(12, 406);
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
			this.VoicesPanel.Size = new System.Drawing.Size(752, 362);
			this.VoicesPanel.TabIndex = 0;
			// 
			// AddVoicesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 441);
			this.Controls.Add(this.RefreshButton);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.MainTabControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AddVoicesForm";
			this.Text = "Add Voices...";
			this.Load += new System.EventHandler(this.AddVoicesForm_Load);
			this.MainTabControl.ResumeLayout(false);
			this.VoicesTabPage.ResumeLayout(false);
			this.LogTabPage.ResumeLayout(false);
			this.LogTabPage.PerformLayout();
			this.BodyPanel.ResumeLayout(false);
			this.BodyPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private VoicesUserControl VoicesPanel;
		private System.Windows.Forms.TabControl MainTabControl;
		private System.Windows.Forms.TabPage VoicesTabPage;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.Button RefreshButton;
		private System.Windows.Forms.TabPage LogTabPage;
		private System.Windows.Forms.Label StatusLabel;
		private System.Windows.Forms.Panel BodyPanel;
	}
}