namespace JocysCom.ClassLibrary.Configuration
{
    partial class SettingsUserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer settings = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (settings != null))
			{
				settings.Dispose();
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsUserControl));
			this.SettingsDataGridView = new System.Windows.Forms.DataGridView();
			this.SettingsToolStrip = new System.Windows.Forms.ToolStrip();
			this.SettingsAddButton = new System.Windows.Forms.ToolStripButton();
			this.SettingsDeleteButton = new System.Windows.Forms.ToolStripButton();
			this.BrowseButton = new System.Windows.Forms.ToolStripButton();
			this.SettingsExportButton = new System.Windows.Forms.ToolStripButton();
			this.SettingsImportButton = new System.Windows.Forms.ToolStripButton();
			this.ResetButton = new System.Windows.Forms.ToolStripButton();
			this.SettingsImportOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.SettingsExportSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.AudioFileOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.SettingsDataGridView)).BeginInit();
			this.SettingsToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// SettingsDataGridView
			// 
			this.SettingsDataGridView.AllowUserToAddRows = false;
			this.SettingsDataGridView.AllowUserToDeleteRows = false;
			this.SettingsDataGridView.AllowUserToResizeColumns = false;
			this.SettingsDataGridView.AllowUserToResizeRows = false;
			this.SettingsDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
			this.SettingsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.SettingsDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.SettingsDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.SettingsDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.SettingsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
			this.SettingsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.SettingsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SettingsDataGridView.EnableHeadersVisualStyles = false;
			this.SettingsDataGridView.GridColor = System.Drawing.SystemColors.Control;
			this.SettingsDataGridView.Location = new System.Drawing.Point(0, 25);
			this.SettingsDataGridView.Margin = new System.Windows.Forms.Padding(0);
			this.SettingsDataGridView.Name = "SettingsDataGridView";
			this.SettingsDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			this.SettingsDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
			this.SettingsDataGridView.RowHeadersVisible = false;
			this.SettingsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.SettingsDataGridView.Size = new System.Drawing.Size(551, 255);
			this.SettingsDataGridView.TabIndex = 2;
			this.SettingsDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SettingsGridView_CellClick);
			this.SettingsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SettingsDataGridView_CellContentClick);
			this.SettingsDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.SettingsDataGridView_CellEndEdit);
			this.SettingsDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.SettingsDataGridView_CellFormatting);
			this.SettingsDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.SettingsDataGridView_CellValidating);
			this.SettingsDataGridView.MouseHover += new System.EventHandler(this.Settings_MouseHover);
			// 
			// SettingsToolStrip
			// 
			this.SettingsToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.SettingsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingsAddButton,
            this.SettingsDeleteButton,
            this.BrowseButton,
            this.SettingsExportButton,
            this.SettingsImportButton,
            this.ResetButton});
			this.SettingsToolStrip.Location = new System.Drawing.Point(0, 0);
			this.SettingsToolStrip.Name = "SettingsToolStrip";
			this.SettingsToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.SettingsToolStrip.Size = new System.Drawing.Size(551, 25);
			this.SettingsToolStrip.TabIndex = 3;
			this.SettingsToolStrip.Text = "toolStrip1";
			this.SettingsToolStrip.MouseLeave += new System.EventHandler(this.Settings_MouseLeave);
			this.SettingsToolStrip.MouseHover += new System.EventHandler(this.Settings_MouseHover);
			// 
			// SettingsAddButton
			// 
			this.SettingsAddButton.Image = ((System.Drawing.Image)(resources.GetObject("SettingsAddButton.Image")));
			this.SettingsAddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SettingsAddButton.Name = "SettingsAddButton";
			this.SettingsAddButton.Size = new System.Drawing.Size(76, 22);
			this.SettingsAddButton.Text = "Add New";
			this.SettingsAddButton.Click += new System.EventHandler(this.SettingsAddButton_Click);
			// 
			// SettingsDeleteButton
			// 
			this.SettingsDeleteButton.Image = ((System.Drawing.Image)(resources.GetObject("SettingsDeleteButton.Image")));
			this.SettingsDeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SettingsDeleteButton.Name = "SettingsDeleteButton";
			this.SettingsDeleteButton.Size = new System.Drawing.Size(60, 22);
			this.SettingsDeleteButton.Text = "Delete";
			this.SettingsDeleteButton.Click += new System.EventHandler(this.SettingsDeleteButton_Click);
			// 
			// BrowseButton
			// 
			this.BrowseButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.BrowseButton.Image = ((System.Drawing.Image)(resources.GetObject("BrowseButton.Image")));
			this.BrowseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.BrowseButton.Name = "BrowseButton";
			this.BrowseButton.Size = new System.Drawing.Size(105, 22);
			this.BrowseButton.Text = "Show in Folder";
			this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
			// 
			// SettingsExportButton
			// 
			this.SettingsExportButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.SettingsExportButton.Image = ((System.Drawing.Image)(resources.GetObject("SettingsExportButton.Image")));
			this.SettingsExportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SettingsExportButton.Name = "SettingsExportButton";
			this.SettingsExportButton.Size = new System.Drawing.Size(69, 22);
			this.SettingsExportButton.Text = "Export...";
			this.SettingsExportButton.Click += new System.EventHandler(this.SettingsExportButton_Click);
			// 
			// SettingsImportButton
			// 
			this.SettingsImportButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.SettingsImportButton.Image = ((System.Drawing.Image)(resources.GetObject("SettingsImportButton.Image")));
			this.SettingsImportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SettingsImportButton.Name = "SettingsImportButton";
			this.SettingsImportButton.Size = new System.Drawing.Size(72, 22);
			this.SettingsImportButton.Text = "Import...";
			this.SettingsImportButton.Click += new System.EventHandler(this.SettingsImportButton_Click);
			// 
			// ResetButton
			// 
			this.ResetButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.ResetButton.Image = ((System.Drawing.Image)(resources.GetObject("ResetButton.Image")));
			this.ResetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ResetButton.Name = "ResetButton";
			this.ResetButton.Size = new System.Drawing.Size(55, 22);
			this.ResetButton.Text = "Reset";
			this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
			// 
			// SettingsUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.SettingsDataGridView);
			this.Controls.Add(this.SettingsToolStrip);
			this.Name = "SettingsUserControl";
			this.Size = new System.Drawing.Size(551, 280);
			((System.ComponentModel.ISupportInitialize)(this.SettingsDataGridView)).EndInit();
			this.SettingsToolStrip.ResumeLayout(false);
			this.SettingsToolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ToolStrip SettingsToolStrip;
		private System.Windows.Forms.ToolStripButton SettingsAddButton;
		private System.Windows.Forms.ToolStripButton SettingsDeleteButton;
		private System.Windows.Forms.ToolStripButton SettingsExportButton;
		private System.Windows.Forms.ToolStripButton SettingsImportButton;
		private System.Windows.Forms.OpenFileDialog SettingsImportOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog SettingsExportSaveFileDialog;
        private System.Windows.Forms.OpenFileDialog AudioFileOpenFileDialog;
        public System.Windows.Forms.DataGridView SettingsDataGridView;
		private System.Windows.Forms.ToolStripButton BrowseButton;
		private System.Windows.Forms.ToolStripButton ResetButton;
	}
}
