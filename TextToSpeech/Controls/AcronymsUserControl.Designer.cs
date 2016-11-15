namespace JocysCom.TextToSpeech.Monitor.Controls
{
    partial class AcronymsUserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer acronyms = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (acronyms != null))
			{
				acronyms.Dispose();
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.AcronymsDataGridView = new System.Windows.Forms.DataGridView();
			this.AcronymsToolStrip = new System.Windows.Forms.ToolStrip();
			this.AcronymsAddButton = new System.Windows.Forms.ToolStripButton();
			this.AcronymsDeleteButton = new System.Windows.Forms.ToolStripButton();
			this.BrowseButton = new System.Windows.Forms.ToolStripButton();
			this.AcronymsExportButton = new System.Windows.Forms.ToolStripButton();
			this.AcronymsImportButton = new System.Windows.Forms.ToolStripButton();
			this.ResetButton = new System.Windows.Forms.ToolStripButton();
			this.AcronymsImportOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.AcronymsExportSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.AudioFileOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.EnabledColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.GroupColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.KeyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.EmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.AcronymsDataGridView)).BeginInit();
			this.AcronymsToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// AcronymsDataGridView
			// 
			this.AcronymsDataGridView.AllowUserToAddRows = false;
			this.AcronymsDataGridView.AllowUserToDeleteRows = false;
			this.AcronymsDataGridView.AllowUserToResizeColumns = false;
			this.AcronymsDataGridView.AllowUserToResizeRows = false;
			this.AcronymsDataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
			this.AcronymsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.AcronymsDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.AcronymsDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.AcronymsDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.AcronymsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.AcronymsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.AcronymsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EnabledColumn,
            this.GroupColumn,
            this.KeyColumn,
            this.ValueColumn,
            this.EmptyColumn});
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.AcronymsDataGridView.DefaultCellStyle = dataGridViewCellStyle4;
			this.AcronymsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AcronymsDataGridView.EnableHeadersVisualStyles = false;
			this.AcronymsDataGridView.GridColor = System.Drawing.SystemColors.Control;
			this.AcronymsDataGridView.Location = new System.Drawing.Point(0, 25);
			this.AcronymsDataGridView.Margin = new System.Windows.Forms.Padding(0);
			this.AcronymsDataGridView.Name = "AcronymsDataGridView";
			this.AcronymsDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.AcronymsDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
			this.AcronymsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.AcronymsDataGridView.Size = new System.Drawing.Size(551, 255);
			this.AcronymsDataGridView.TabIndex = 2;
			this.AcronymsDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AcronymsGridView_CellClick);
			this.AcronymsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AcronymsDataGridView_CellContentClick);
			this.AcronymsDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.AcronymsDataGridView_CellEndEdit);
			this.AcronymsDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.AcronymsDataGridView_CellFormatting);
			this.AcronymsDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.AcronymsDataGridView_CellValidating);
			this.AcronymsDataGridView.MouseHover += new System.EventHandler(this.Acronyms_MouseHover);
			// 
			// AcronymsToolStrip
			// 
			this.AcronymsToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.AcronymsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AcronymsAddButton,
            this.AcronymsDeleteButton,
            this.BrowseButton,
            this.AcronymsExportButton,
            this.AcronymsImportButton,
            this.ResetButton});
			this.AcronymsToolStrip.Location = new System.Drawing.Point(0, 0);
			this.AcronymsToolStrip.Name = "AcronymsToolStrip";
			this.AcronymsToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.AcronymsToolStrip.Size = new System.Drawing.Size(551, 25);
			this.AcronymsToolStrip.TabIndex = 3;
			this.AcronymsToolStrip.Text = "toolStrip1";
			this.AcronymsToolStrip.MouseLeave += new System.EventHandler(this.Acronyms_MouseLeave);
			this.AcronymsToolStrip.MouseHover += new System.EventHandler(this.Acronyms_MouseHover);
			// 
			// AcronymsAddButton
			// 
			this.AcronymsAddButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Plus;
			this.AcronymsAddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AcronymsAddButton.Name = "AcronymsAddButton";
			this.AcronymsAddButton.Size = new System.Drawing.Size(76, 22);
			this.AcronymsAddButton.Text = "Add New";
			this.AcronymsAddButton.Click += new System.EventHandler(this.AcronymsAddButton_Click);
			// 
			// AcronymsDeleteButton
			// 
			this.AcronymsDeleteButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Delete;
			this.AcronymsDeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AcronymsDeleteButton.Name = "AcronymsDeleteButton";
			this.AcronymsDeleteButton.Size = new System.Drawing.Size(60, 22);
			this.AcronymsDeleteButton.Text = "Delete";
			this.AcronymsDeleteButton.Click += new System.EventHandler(this.AcronymsDeleteButton_Click);
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
			// AcronymsExportButton
			// 
			this.AcronymsExportButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.AcronymsExportButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Data_Out;
			this.AcronymsExportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AcronymsExportButton.Name = "AcronymsExportButton";
			this.AcronymsExportButton.Size = new System.Drawing.Size(69, 22);
			this.AcronymsExportButton.Text = "Export...";
			this.AcronymsExportButton.Click += new System.EventHandler(this.AcronymsExportButton_Click);
			// 
			// AcronymsImportButton
			// 
			this.AcronymsImportButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.AcronymsImportButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Data_Into;
			this.AcronymsImportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AcronymsImportButton.Name = "AcronymsImportButton";
			this.AcronymsImportButton.Size = new System.Drawing.Size(72, 22);
			this.AcronymsImportButton.Text = "Import...";
			this.AcronymsImportButton.Click += new System.EventHandler(this.AcronymsImportButton_Click);
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
			// EnabledColumn
			// 
			this.EnabledColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.EnabledColumn.DataPropertyName = "enabled";
			this.EnabledColumn.HeaderText = "ON";
			this.EnabledColumn.Name = "EnabledColumn";
			this.EnabledColumn.ReadOnly = true;
			this.EnabledColumn.Width = 31;
			// 
			// GroupColumn
			// 
			this.GroupColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.GroupColumn.DataPropertyName = "Group";
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.GroupColumn.DefaultCellStyle = dataGridViewCellStyle2;
			this.GroupColumn.HeaderText = "Group";
			this.GroupColumn.MinimumWidth = 120;
			this.GroupColumn.Name = "GroupColumn";
			this.GroupColumn.Width = 120;
			// 
			// KeyColumn
			// 
			this.KeyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.KeyColumn.DataPropertyName = "Key";
			this.KeyColumn.HeaderText = "Key";
			this.KeyColumn.Name = "KeyColumn";
			this.KeyColumn.Width = 52;
			// 
			// ValueColumn
			// 
			this.ValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.ValueColumn.DataPropertyName = "Value";
			this.ValueColumn.HeaderText = "Value";
			this.ValueColumn.Name = "ValueColumn";
			this.ValueColumn.Width = 61;
			// 
			// EmptyColumn
			// 
			this.EmptyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			this.EmptyColumn.DefaultCellStyle = dataGridViewCellStyle3;
			this.EmptyColumn.HeaderText = "";
			this.EmptyColumn.Name = "EmptyColumn";
			// 
			// AcronymsUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.AcronymsDataGridView);
			this.Controls.Add(this.AcronymsToolStrip);
			this.Name = "AcronymsUserControl";
			this.Size = new System.Drawing.Size(551, 280);
			((System.ComponentModel.ISupportInitialize)(this.AcronymsDataGridView)).EndInit();
			this.AcronymsToolStrip.ResumeLayout(false);
			this.AcronymsToolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ToolStrip AcronymsToolStrip;
		private System.Windows.Forms.ToolStripButton AcronymsAddButton;
		private System.Windows.Forms.ToolStripButton AcronymsDeleteButton;
		private System.Windows.Forms.ToolStripButton AcronymsExportButton;
		private System.Windows.Forms.ToolStripButton AcronymsImportButton;
		private System.Windows.Forms.OpenFileDialog AcronymsImportOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog AcronymsExportSaveFileDialog;
        private System.Windows.Forms.OpenFileDialog AudioFileOpenFileDialog;
        public System.Windows.Forms.DataGridView AcronymsDataGridView;
		private System.Windows.Forms.ToolStripButton BrowseButton;
		private System.Windows.Forms.ToolStripButton ResetButton;
		private System.Windows.Forms.DataGridViewCheckBoxColumn EnabledColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn GroupColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn KeyColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn ValueColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn EmptyColumn;
	}
}
