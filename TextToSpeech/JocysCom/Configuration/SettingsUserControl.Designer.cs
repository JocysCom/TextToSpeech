namespace JocysCom.ClassLibrary.Configuration
{
    partial class SettingsUserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer settings = null;

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsUserControl));
            this.SettingsDataGridView = new System.Windows.Forms.DataGridView();
            this.SettingsToolStrip = new System.Windows.Forms.ToolStrip();
            this.AddButton = new System.Windows.Forms.ToolStripButton();
            this.EditButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteButton = new System.Windows.Forms.ToolStripButton();
            this.SaveButton = new System.Windows.Forms.ToolStripButton();
            this.ShowInFolderButton = new System.Windows.Forms.ToolStripButton();
            this.ExportButton = new System.Windows.Forms.ToolStripButton();
            this.ImportButton = new System.Windows.Forms.ToolStripButton();
            this.ResetButton = new System.Windows.Forms.ToolStripButton();
            this.FilterTextBox = new System.Windows.Forms.ToolStripTextBox();
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SettingsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.SettingsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SettingsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsDataGridView.EnableHeadersVisualStyles = false;
            this.SettingsDataGridView.GridColor = System.Drawing.SystemColors.Control;
            this.SettingsDataGridView.Location = new System.Drawing.Point(0, 25);
            this.SettingsDataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.SettingsDataGridView.Name = "SettingsDataGridView";
            this.SettingsDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.SettingsDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.SettingsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SettingsDataGridView.Size = new System.Drawing.Size(671, 255);
            this.SettingsDataGridView.TabIndex = 2;
            this.SettingsDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SettingsGridView_CellClick);
            this.SettingsDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.SettingsDataGridView_CellEndEdit);
            this.SettingsDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.SettingsDataGridView_CellFormatting);
            // 
            // SettingsToolStrip
            // 
            this.SettingsToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.SettingsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddButton,
            this.EditButton,
            this.DeleteButton,
            this.SaveButton,
            this.ShowInFolderButton,
            this.ExportButton,
            this.ImportButton,
            this.ResetButton,
            this.FilterTextBox});
            this.SettingsToolStrip.Location = new System.Drawing.Point(0, 0);
            this.SettingsToolStrip.Name = "SettingsToolStrip";
            this.SettingsToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.SettingsToolStrip.Size = new System.Drawing.Size(671, 25);
            this.SettingsToolStrip.TabIndex = 3;
            this.SettingsToolStrip.Text = "MainToolStrip";
            // 
            // AddButton
            // 
            this.AddButton.Image = ((System.Drawing.Image)(resources.GetObject("AddButton.Image")));
            this.AddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(49, 22);
            this.AddButton.Text = "Add";
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // EditButton
            // 
            this.EditButton.Image = ((System.Drawing.Image)(resources.GetObject("EditButton.Image")));
            this.EditButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(47, 22);
            this.EditButton.Text = "Edit";
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteButton.Image")));
            this.DeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(60, 22);
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveButton.Image")));
            this.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(51, 22);
            this.SaveButton.Text = "Save";
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ShowInFolderButton
            // 
            this.ShowInFolderButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ShowInFolderButton.Image = ((System.Drawing.Image)(resources.GetObject("ShowInFolderButton.Image")));
            this.ShowInFolderButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ShowInFolderButton.Name = "ShowInFolderButton";
            this.ShowInFolderButton.Size = new System.Drawing.Size(105, 22);
            this.ShowInFolderButton.Text = "Show in Folder";
            this.ShowInFolderButton.Click += new System.EventHandler(this.ShowInFolderButton_Click);
            // 
            // ExportButton
            // 
            this.ExportButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ExportButton.Image = ((System.Drawing.Image)(resources.GetObject("ExportButton.Image")));
            this.ExportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(69, 22);
            this.ExportButton.Text = "Export...";
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // ImportButton
            // 
            this.ImportButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ImportButton.Image = ((System.Drawing.Image)(resources.GetObject("ImportButton.Image")));
            this.ImportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(72, 22);
            this.ImportButton.Text = "Import...";
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
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
            // FilterTextBox
            // 
            this.FilterTextBox.Name = "FilterTextBox";
            this.FilterTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // SettingsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SettingsDataGridView);
            this.Controls.Add(this.SettingsToolStrip);
            this.Name = "SettingsUserControl";
            this.Size = new System.Drawing.Size(671, 280);
            ((System.ComponentModel.ISupportInitialize)(this.SettingsDataGridView)).EndInit();
            this.SettingsToolStrip.ResumeLayout(false);
            this.SettingsToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ToolStrip SettingsToolStrip;
		private System.Windows.Forms.ToolStripButton AddButton;
		private System.Windows.Forms.ToolStripButton DeleteButton;
		private System.Windows.Forms.ToolStripButton ExportButton;
		private System.Windows.Forms.ToolStripButton ImportButton;
		private System.Windows.Forms.OpenFileDialog SettingsImportOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog SettingsExportSaveFileDialog;
        private System.Windows.Forms.OpenFileDialog AudioFileOpenFileDialog;
        public System.Windows.Forms.DataGridView SettingsDataGridView;
		private System.Windows.Forms.ToolStripButton ShowInFolderButton;
		private System.Windows.Forms.ToolStripButton ResetButton;
		private System.Windows.Forms.ToolStripButton SaveButton;
		public System.Windows.Forms.ToolStripTextBox FilterTextBox;
        private System.Windows.Forms.ToolStripButton EditButton;
    }
}
