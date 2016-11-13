namespace JocysCom.TextToSpeech.Monitor.Controls
{
    partial class SoundsUserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer sounds = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (sounds != null))
			{
				sounds.Dispose();
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			this.SoundsDataGridView = new System.Windows.Forms.DataGridView();
			this.EnabledColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.GroupColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FileColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SoundsToolStrip = new System.Windows.Forms.ToolStrip();
			this.SoundsImportOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.SoundsExportSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.AudioFileOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.SoundsAddButton = new System.Windows.Forms.ToolStripButton();
			this.SoundsDeleteButton = new System.Windows.Forms.ToolStripButton();
			this.BrowseButton = new System.Windows.Forms.ToolStripButton();
			this.SoundsExportButton = new System.Windows.Forms.ToolStripButton();
			this.SoundsImportButton = new System.Windows.Forms.ToolStripButton();
			this.ResetButton = new System.Windows.Forms.ToolStripButton();
			((System.ComponentModel.ISupportInitialize)(this.SoundsDataGridView)).BeginInit();
			this.SoundsToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// SoundsDataGridView
			// 
			this.SoundsDataGridView.AllowUserToAddRows = false;
			this.SoundsDataGridView.AllowUserToDeleteRows = false;
			this.SoundsDataGridView.AllowUserToResizeColumns = false;
			this.SoundsDataGridView.AllowUserToResizeRows = false;
			this.SoundsDataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
			this.SoundsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.SoundsDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.SoundsDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.SoundsDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.SoundsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			this.SoundsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.SoundsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EnabledColumn,
            this.GroupColumn,
            this.FileColumn});
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.SoundsDataGridView.DefaultCellStyle = dataGridViewCellStyle11;
			this.SoundsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SoundsDataGridView.EnableHeadersVisualStyles = false;
			this.SoundsDataGridView.GridColor = System.Drawing.SystemColors.Control;
			this.SoundsDataGridView.Location = new System.Drawing.Point(0, 25);
			this.SoundsDataGridView.Margin = new System.Windows.Forms.Padding(0);
			this.SoundsDataGridView.Name = "SoundsDataGridView";
			this.SoundsDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.SoundsDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
			this.SoundsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.SoundsDataGridView.Size = new System.Drawing.Size(551, 255);
			this.SoundsDataGridView.TabIndex = 2;
			this.SoundsDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SoundsGridView_CellClick);
			this.SoundsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SoundsDataGridView_CellContentClick);
			this.SoundsDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.SoundsDataGridView_CellEndEdit);
			this.SoundsDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.SoundsDataGridView_CellFormatting);
			this.SoundsDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.SoundsDataGridView_CellValidating);
			this.SoundsDataGridView.MouseHover += new System.EventHandler(this.Sounds_MouseHover);
			// 
			// EnabledColumn
			// 
			this.EnabledColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.EnabledColumn.DataPropertyName = "enabled";
			dataGridViewCellStyle8.NullValue = false;
			this.EnabledColumn.DefaultCellStyle = dataGridViewCellStyle8;
			this.EnabledColumn.HeaderText = "ON";
			this.EnabledColumn.Name = "EnabledColumn";
			this.EnabledColumn.ReadOnly = true;
			this.EnabledColumn.Width = 31;
			// 
			// GroupColumn
			// 
			this.GroupColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.GroupColumn.DataPropertyName = "group";
			dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.GroupColumn.DefaultCellStyle = dataGridViewCellStyle9;
			this.GroupColumn.HeaderText = "Group";
			this.GroupColumn.MinimumWidth = 120;
			this.GroupColumn.Name = "GroupColumn";
			this.GroupColumn.Width = 120;
			// 
			// FileColumn
			// 
			this.FileColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.FileColumn.DataPropertyName = "file";
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle10.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.FileColumn.DefaultCellStyle = dataGridViewCellStyle10;
			this.FileColumn.HeaderText = "Sound ( name  or  path to wav file )";
			this.FileColumn.Name = "FileColumn";
			this.FileColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// SoundsToolStrip
			// 
			this.SoundsToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.SoundsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SoundsAddButton,
            this.SoundsDeleteButton,
            this.BrowseButton,
            this.SoundsExportButton,
            this.SoundsImportButton,
            this.ResetButton});
			this.SoundsToolStrip.Location = new System.Drawing.Point(0, 0);
			this.SoundsToolStrip.Name = "SoundsToolStrip";
			this.SoundsToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.SoundsToolStrip.Size = new System.Drawing.Size(551, 25);
			this.SoundsToolStrip.TabIndex = 3;
			this.SoundsToolStrip.Text = "toolStrip1";
			this.SoundsToolStrip.MouseLeave += new System.EventHandler(this.Sounds_MouseLeave);
			this.SoundsToolStrip.MouseHover += new System.EventHandler(this.Sounds_MouseHover);
			// 
			// SoundsAddButton
			// 
			this.SoundsAddButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Plus;
			this.SoundsAddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SoundsAddButton.Name = "SoundsAddButton";
			this.SoundsAddButton.Size = new System.Drawing.Size(76, 22);
			this.SoundsAddButton.Text = "Add New";
			this.SoundsAddButton.Click += new System.EventHandler(this.SoundsAddButton_Click);
			// 
			// SoundsDeleteButton
			// 
			this.SoundsDeleteButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Delete;
			this.SoundsDeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SoundsDeleteButton.Name = "SoundsDeleteButton";
			this.SoundsDeleteButton.Size = new System.Drawing.Size(60, 22);
			this.SoundsDeleteButton.Text = "Delete";
			this.SoundsDeleteButton.Click += new System.EventHandler(this.SoundsDeleteButton_Click);
			// 
			// BrowseButton
			// 
			this.BrowseButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.BrowseButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Folder;
			this.BrowseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.BrowseButton.Name = "BrowseButton";
			this.BrowseButton.Size = new System.Drawing.Size(105, 22);
			this.BrowseButton.Text = "Show in Folder";
			this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
			// 
			// SoundsExportButton
			// 
			this.SoundsExportButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.SoundsExportButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Data_Out;
			this.SoundsExportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SoundsExportButton.Name = "SoundsExportButton";
			this.SoundsExportButton.Size = new System.Drawing.Size(69, 22);
			this.SoundsExportButton.Text = "Export...";
			this.SoundsExportButton.Click += new System.EventHandler(this.SoundsExportButton_Click);
			// 
			// SoundsImportButton
			// 
			this.SoundsImportButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.SoundsImportButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Data_Into;
			this.SoundsImportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SoundsImportButton.Name = "SoundsImportButton";
			this.SoundsImportButton.Size = new System.Drawing.Size(72, 22);
			this.SoundsImportButton.Text = "Import...";
			this.SoundsImportButton.Click += new System.EventHandler(this.SoundsImportButton_Click);
			// 
			// ResetButton
			// 
			this.ResetButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.ResetButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.reset_16x16;
			this.ResetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ResetButton.Name = "ResetButton";
			this.ResetButton.Size = new System.Drawing.Size(55, 22);
			this.ResetButton.Text = "Reset";
			this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
			// 
			// SoundsUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.SoundsDataGridView);
			this.Controls.Add(this.SoundsToolStrip);
			this.Name = "SoundsUserControl";
			this.Size = new System.Drawing.Size(551, 280);
			((System.ComponentModel.ISupportInitialize)(this.SoundsDataGridView)).EndInit();
			this.SoundsToolStrip.ResumeLayout(false);
			this.SoundsToolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ToolStrip SoundsToolStrip;
		private System.Windows.Forms.ToolStripButton SoundsAddButton;
		private System.Windows.Forms.ToolStripButton SoundsDeleteButton;
		private System.Windows.Forms.ToolStripButton SoundsExportButton;
		private System.Windows.Forms.ToolStripButton SoundsImportButton;
		private System.Windows.Forms.OpenFileDialog SoundsImportOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog SoundsExportSaveFileDialog;
        private System.Windows.Forms.OpenFileDialog AudioFileOpenFileDialog;
        public System.Windows.Forms.DataGridView SoundsDataGridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EnabledColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileColumn;
		private System.Windows.Forms.ToolStripButton BrowseButton;
		private System.Windows.Forms.ToolStripButton ResetButton;
	}
}
