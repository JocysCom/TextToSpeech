namespace JocysCom.TextToSpeech.Monitor.Controls
{
	partial class VoiceOverridesUserControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.VoicesOverridesDataGridView = new System.Windows.Forms.DataGridView();
            this.EnabledColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GenderColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EffectColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PitchColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VolumeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoicesOverridesToolStrip = new System.Windows.Forms.ToolStrip();
            this.AddButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteButton = new System.Windows.Forms.ToolStripButton();
            this.ExportButton = new System.Windows.Forms.ToolStripButton();
            this.ImportButton = new System.Windows.Forms.ToolStripButton();
            this.ImportOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ExportSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.VoicesOverridesDataGridView)).BeginInit();
            this.VoicesOverridesToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // VoicesOverridesDataGridView
            // 
            this.VoicesOverridesDataGridView.AllowUserToAddRows = false;
            this.VoicesOverridesDataGridView.AllowUserToDeleteRows = false;
            this.VoicesOverridesDataGridView.AllowUserToResizeColumns = false;
            this.VoicesOverridesDataGridView.AllowUserToResizeRows = false;
            this.VoicesOverridesDataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.VoicesOverridesDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.VoicesOverridesDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.VoicesOverridesDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.VoicesOverridesDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.VoicesOverridesDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.VoicesOverridesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.VoicesOverridesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EnabledColumn,
            this.NameColumn,
            this.GenderColumn,
            this.EffectColumn,
            this.PitchColumn,
            this.RateColumn,
            this.VolumeColumn,
            this.EmptyColumn});
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.VoicesOverridesDataGridView.DefaultCellStyle = dataGridViewCellStyle17;
            this.VoicesOverridesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VoicesOverridesDataGridView.EnableHeadersVisualStyles = false;
            this.VoicesOverridesDataGridView.GridColor = System.Drawing.SystemColors.Control;
            this.VoicesOverridesDataGridView.Location = new System.Drawing.Point(0, 25);
            this.VoicesOverridesDataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.VoicesOverridesDataGridView.Name = "VoicesOverridesDataGridView";
            this.VoicesOverridesDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.VoicesOverridesDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.VoicesOverridesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.VoicesOverridesDataGridView.Size = new System.Drawing.Size(551, 255);
            this.VoicesOverridesDataGridView.TabIndex = 2;
            this.VoicesOverridesDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.VoicesGridView_CellClick);
            this.VoicesOverridesDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.VoicesDataGridView_CellEndEdit);
            this.VoicesOverridesDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.VoicesDataGridView_CellFormatting);
            this.VoicesOverridesDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.VoicesDataGridView_CellValidating);
            this.VoicesOverridesDataGridView.MouseHover += new System.EventHandler(this.VoicesOverrides_MouseHover);
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
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NameColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.MinimumWidth = 120;
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.Width = 120;
            // 
            // GenderColumn
            // 
            this.GenderColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.GenderColumn.DataPropertyName = "gender";
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.GenderColumn.DefaultCellStyle = dataGridViewCellStyle12;
            this.GenderColumn.HeaderText = "Gender";
            this.GenderColumn.Name = "GenderColumn";
            this.GenderColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GenderColumn.Width = 50;
            // 
            // EffectColumn
            // 
            this.EffectColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.EffectColumn.DataPropertyName = "effect";
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            this.EffectColumn.DefaultCellStyle = dataGridViewCellStyle13;
            this.EffectColumn.HeaderText = "Effect";
            this.EffectColumn.Name = "EffectColumn";
            this.EffectColumn.Width = 62;
            // 
            // PitchColumn
            // 
            this.PitchColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PitchColumn.DataPropertyName = "pitch";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            this.PitchColumn.DefaultCellStyle = dataGridViewCellStyle14;
            this.PitchColumn.HeaderText = "Pitch";
            this.PitchColumn.Name = "PitchColumn";
            this.PitchColumn.Width = 58;
            // 
            // RateColumn
            // 
            this.RateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RateColumn.DataPropertyName = "rate";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            this.RateColumn.DefaultCellStyle = dataGridViewCellStyle15;
            this.RateColumn.HeaderText = "Rate";
            this.RateColumn.Name = "RateColumn";
            this.RateColumn.Width = 57;
            // 
            // VolumeColumn
            // 
            this.VolumeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.VolumeColumn.DataPropertyName = "volume";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window;
            this.VolumeColumn.DefaultCellStyle = dataGridViewCellStyle16;
            this.VolumeColumn.HeaderText = "Volume";
            this.VolumeColumn.Name = "VolumeColumn";
            this.VolumeColumn.Width = 69;
            // 
            // EmptyColumn
            // 
            this.EmptyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EmptyColumn.HeaderText = "";
            this.EmptyColumn.Name = "EmptyColumn";
            this.EmptyColumn.ReadOnly = true;
            // 
            // VoicesOverridesToolStrip
            // 
            this.VoicesOverridesToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.VoicesOverridesToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddButton,
            this.DeleteButton,
            this.ExportButton,
            this.ImportButton});
            this.VoicesOverridesToolStrip.Location = new System.Drawing.Point(0, 0);
            this.VoicesOverridesToolStrip.Name = "VoicesOverridesToolStrip";
            this.VoicesOverridesToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.VoicesOverridesToolStrip.Size = new System.Drawing.Size(551, 25);
            this.VoicesOverridesToolStrip.TabIndex = 3;
            this.VoicesOverridesToolStrip.Text = "toolStrip1";
            this.VoicesOverridesToolStrip.MouseLeave += new System.EventHandler(this.VoicesOverrides_MouseLeave);
            this.VoicesOverridesToolStrip.MouseHover += new System.EventHandler(this.VoicesOverrides_MouseHover);
            // 
            // AddButton
            // 
            this.AddButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.add_16x16;
            this.AddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(76, 22);
            this.AddButton.Text = "Add New";
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.delete_16x16;
            this.DeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(60, 22);
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // ExportButton
            // 
            this.ExportButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ExportButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.data_out_16x16;
            this.ExportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(69, 22);
            this.ExportButton.Text = "Export...";
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // ImportButton
            // 
            this.ImportButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ImportButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.data_into_16x16;
            this.ImportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(72, 22);
            this.ImportButton.Text = "Import...";
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // VoiceOverridesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.VoicesOverridesDataGridView);
            this.Controls.Add(this.VoicesOverridesToolStrip);
            this.Name = "VoiceOverridesUserControl";
            this.Size = new System.Drawing.Size(551, 280);
            ((System.ComponentModel.ISupportInitialize)(this.VoicesOverridesDataGridView)).EndInit();
            this.VoicesOverridesToolStrip.ResumeLayout(false);
            this.VoicesOverridesToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView VoicesOverridesDataGridView;
		private System.Windows.Forms.ToolStrip VoicesOverridesToolStrip;
		private System.Windows.Forms.ToolStripButton AddButton;
		private System.Windows.Forms.ToolStripButton DeleteButton;
		private System.Windows.Forms.ToolStripButton ExportButton;
		private System.Windows.Forms.ToolStripButton ImportButton;
		private System.Windows.Forms.OpenFileDialog ImportOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog ExportSaveFileDialog;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EnabledColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GenderColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EffectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PitchColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn VolumeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmptyColumn;
	}
}
