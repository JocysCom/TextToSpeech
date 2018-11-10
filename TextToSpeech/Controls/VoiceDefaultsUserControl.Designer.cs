namespace JocysCom.TextToSpeech.Monitor.Controls
{
	partial class VoicesDefaultsUserControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.VoicesDefaultsDataGridView = new System.Windows.Forms.DataGridView();
            this.VoicesDefaultsToolStrip = new System.Windows.Forms.ToolStrip();
            this.AddButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteButton = new System.Windows.Forms.ToolStripButton();
            this.BrowseButton = new System.Windows.Forms.ToolStripButton();
            this.ExportButton = new System.Windows.Forms.ToolStripButton();
            this.ImportButton = new System.Windows.Forms.ToolStripButton();
            this.ImportOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ExportSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.EnabledColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GenderColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EffectColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PitchColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VolumeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LanguageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoiceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.VoicesDefaultsDataGridView)).BeginInit();
            this.VoicesDefaultsToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // VoicesDefaultsDataGridView
            // 
            this.VoicesDefaultsDataGridView.AllowUserToAddRows = false;
            this.VoicesDefaultsDataGridView.AllowUserToDeleteRows = false;
            this.VoicesDefaultsDataGridView.AllowUserToResizeColumns = false;
            this.VoicesDefaultsDataGridView.AllowUserToResizeRows = false;
            this.VoicesDefaultsDataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.VoicesDefaultsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.VoicesDefaultsDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.VoicesDefaultsDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.VoicesDefaultsDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.VoicesDefaultsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.VoicesDefaultsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.VoicesDefaultsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EnabledColumn,
            this.NameColumn,
            this.GenderColumn,
            this.EffectColumn,
            this.PitchColumn,
            this.RateColumn,
            this.VolumeColumn,
            this.LanguageColumn,
            this.VoiceColumn,
            this.EmptyColumn});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.VoicesDefaultsDataGridView.DefaultCellStyle = dataGridViewCellStyle10;
            this.VoicesDefaultsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VoicesDefaultsDataGridView.EnableHeadersVisualStyles = false;
            this.VoicesDefaultsDataGridView.GridColor = System.Drawing.SystemColors.Control;
            this.VoicesDefaultsDataGridView.Location = new System.Drawing.Point(0, 25);
            this.VoicesDefaultsDataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.VoicesDefaultsDataGridView.Name = "VoicesDefaultsDataGridView";
            this.VoicesDefaultsDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.VoicesDefaultsDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.VoicesDefaultsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.VoicesDefaultsDataGridView.Size = new System.Drawing.Size(551, 255);
            this.VoicesDefaultsDataGridView.TabIndex = 2;
            this.VoicesDefaultsDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.VoicesGridView_CellClick);
            this.VoicesDefaultsDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.VoicesDataGridView_CellEndEdit);
            this.VoicesDefaultsDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.VoicesDataGridView_CellFormatting);
            this.VoicesDefaultsDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.VoicesDataGridView_CellValidating);
            this.VoicesDefaultsDataGridView.MouseHover += new System.EventHandler(this.VoicesDefaults_MouseHover);
            // 
            // VoicesDefaultsToolStrip
            // 
            this.VoicesDefaultsToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.VoicesDefaultsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddButton,
            this.DeleteButton,
            this.BrowseButton,
            this.ExportButton,
            this.ImportButton});
            this.VoicesDefaultsToolStrip.Location = new System.Drawing.Point(0, 0);
            this.VoicesDefaultsToolStrip.Name = "VoicesDefaultsToolStrip";
            this.VoicesDefaultsToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.VoicesDefaultsToolStrip.Size = new System.Drawing.Size(551, 25);
            this.VoicesDefaultsToolStrip.TabIndex = 3;
            this.VoicesDefaultsToolStrip.Text = "toolStrip1";
            this.VoicesDefaultsToolStrip.MouseLeave += new System.EventHandler(this.VoicesDefaults_MouseLeave);
            this.VoicesDefaultsToolStrip.MouseHover += new System.EventHandler(this.VoicesDefaults_MouseHover);
            // 
            // AddButton
            // 
            this.AddButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Plus;
            this.AddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(76, 22);
            this.AddButton.Text = "Add New";
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Delete;
            this.DeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(60, 22);
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
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
            // ExportButton
            // 
            this.ExportButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ExportButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Data_Out;
            this.ExportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(69, 22);
            this.ExportButton.Text = "Export...";
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // ImportButton
            // 
            this.ImportButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ImportButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Data_Into;
            this.ImportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(72, 22);
            this.ImportButton.Text = "Import...";
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // ExportSaveFileDialog
            // 
            this.ExportSaveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.ExportSaveFileDialog_FileOk);
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
            // NameColumn
            // 
            this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.NameColumn.DataPropertyName = "name";
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NameColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.MinimumWidth = 120;
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.Width = 120;
            // 
            // GenderColumn
            // 
            this.GenderColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.GenderColumn.DataPropertyName = "gender";
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.GenderColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.GenderColumn.HeaderText = "Gender";
            this.GenderColumn.Name = "GenderColumn";
            this.GenderColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GenderColumn.Width = 50;
            // 
            // EffectColumn
            // 
            this.EffectColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.EffectColumn.DataPropertyName = "effect";
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            this.EffectColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.EffectColumn.HeaderText = "Effect";
            this.EffectColumn.Name = "EffectColumn";
            this.EffectColumn.Width = 62;
            // 
            // PitchColumn
            // 
            this.PitchColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PitchColumn.DataPropertyName = "pitch";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            this.PitchColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.PitchColumn.HeaderText = "Pitch";
            this.PitchColumn.Name = "PitchColumn";
            this.PitchColumn.Width = 58;
            // 
            // RateColumn
            // 
            this.RateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RateColumn.DataPropertyName = "rate";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            this.RateColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.RateColumn.HeaderText = "Rate";
            this.RateColumn.Name = "RateColumn";
            this.RateColumn.Width = 57;
            // 
            // VolumeColumn
            // 
            this.VolumeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.VolumeColumn.DataPropertyName = "volume";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            this.VolumeColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.VolumeColumn.HeaderText = "Volume";
            this.VolumeColumn.Name = "VolumeColumn";
            this.VolumeColumn.Width = 69;
            // 
            // LanguageColumn
            // 
            this.LanguageColumn.DataPropertyName = "language";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            this.LanguageColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.LanguageColumn.HeaderText = "Language";
            this.LanguageColumn.Name = "LanguageColumn";
            this.LanguageColumn.Width = 70;
            // 
            // VoiceColumn
            // 
            this.VoiceColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.VoiceColumn.DataPropertyName = "voice";
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            this.VoiceColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.VoiceColumn.HeaderText = "Voice";
            this.VoiceColumn.MinimumWidth = 120;
            this.VoiceColumn.Name = "VoiceColumn";
            this.VoiceColumn.Width = 120;
            // 
            // EmptyColumn
            // 
            this.EmptyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EmptyColumn.HeaderText = "";
            this.EmptyColumn.Name = "EmptyColumn";
            this.EmptyColumn.ReadOnly = true;
            // 
            // VoicesDefaultsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.VoicesDefaultsDataGridView);
            this.Controls.Add(this.VoicesDefaultsToolStrip);
            this.Name = "VoicesDefaultsUserControl";
            this.Size = new System.Drawing.Size(551, 280);
            ((System.ComponentModel.ISupportInitialize)(this.VoicesDefaultsDataGridView)).EndInit();
            this.VoicesDefaultsToolStrip.ResumeLayout(false);
            this.VoicesDefaultsToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView VoicesDefaultsDataGridView;
		private System.Windows.Forms.ToolStrip VoicesDefaultsToolStrip;
		private System.Windows.Forms.ToolStripButton AddButton;
		private System.Windows.Forms.ToolStripButton DeleteButton;
		private System.Windows.Forms.ToolStripButton ExportButton;
		private System.Windows.Forms.ToolStripButton ImportButton;
		private System.Windows.Forms.OpenFileDialog ImportOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog ExportSaveFileDialog;
		private System.Windows.Forms.ToolStripButton BrowseButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EnabledColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GenderColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EffectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PitchColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn VolumeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LanguageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoiceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmptyColumn;
    }
}
