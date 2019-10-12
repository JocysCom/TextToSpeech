namespace JocysCom.TextToSpeech.Monitor.Controls
{
	partial class VoicesUserControl
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
			this.VoicesDataGridView = new System.Windows.Forms.DataGridView();
			this.EnabledColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.MaleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FemaleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.NeutralColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.GenderColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CultureColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LcidColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LanguageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.AgeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.VoicesToolStrip = new System.Windows.Forms.ToolStrip();
			this.AddLocalVoicesButton = new System.Windows.Forms.ToolStripButton();
			this.AddAmazonNeuralVoicesButton = new System.Windows.Forms.ToolStripButton();
			this.AddAmazonStandardVoicesButton = new System.Windows.Forms.ToolStripButton();
			this.RemoveButton = new System.Windows.Forms.ToolStripButton();
			((System.ComponentModel.ISupportInitialize)(this.VoicesDataGridView)).BeginInit();
			this.VoicesToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// VoicesDataGridView
			// 
			this.VoicesDataGridView.AllowUserToAddRows = false;
			this.VoicesDataGridView.AllowUserToDeleteRows = false;
			this.VoicesDataGridView.AllowUserToResizeColumns = false;
			this.VoicesDataGridView.AllowUserToResizeRows = false;
			this.VoicesDataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
			this.VoicesDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.VoicesDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.VoicesDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.VoicesDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle14.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
			dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.VoicesDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
			this.VoicesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.VoicesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EnabledColumn,
            this.MaleColumn,
            this.FemaleColumn,
            this.NeutralColumn,
            this.GenderColumn,
            this.NameColumn,
            this.CultureColumn,
            this.LcidColumn,
            this.LanguageColumn,
            this.AgeColumn,
            this.DescriptionColumn});
			dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.VoicesDataGridView.DefaultCellStyle = dataGridViewCellStyle25;
			this.VoicesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.VoicesDataGridView.EnableHeadersVisualStyles = false;
			this.VoicesDataGridView.GridColor = System.Drawing.SystemColors.Control;
			this.VoicesDataGridView.Location = new System.Drawing.Point(0, 25);
			this.VoicesDataGridView.Margin = new System.Windows.Forms.Padding(0);
			this.VoicesDataGridView.MultiSelect = false;
			this.VoicesDataGridView.Name = "VoicesDataGridView";
			this.VoicesDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.VoicesDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle26;
			this.VoicesDataGridView.RowHeadersVisible = false;
			this.VoicesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.VoicesDataGridView.Size = new System.Drawing.Size(742, 201);
			this.VoicesDataGridView.TabIndex = 1;
			this.VoicesDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.VoicesDataGridView_CellClick);
			this.VoicesDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.VoicesDataGridView_CellFormatting);
			this.VoicesDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.VoicesDataGridView_DataError);
			// 
			// EnabledColumn
			// 
			this.EnabledColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.EnabledColumn.DataPropertyName = "Enabled";
			this.EnabledColumn.HeaderText = "ON";
			this.EnabledColumn.Name = "EnabledColumn";
			this.EnabledColumn.ReadOnly = true;
			this.EnabledColumn.Width = 31;
			// 
			// MaleColumn
			// 
			this.MaleColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.MaleColumn.DataPropertyName = "Male";
			dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle15.Format = "0";
			this.MaleColumn.DefaultCellStyle = dataGridViewCellStyle15;
			this.MaleColumn.HeaderText = "Male";
			this.MaleColumn.MaxInputLength = 3;
			this.MaleColumn.MinimumWidth = 50;
			this.MaleColumn.Name = "MaleColumn";
			this.MaleColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.MaleColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.MaleColumn.ToolTipText = "Voice popularity. Value from 0 (don\'t use) to 100 (often usage)";
			this.MaleColumn.Width = 50;
			// 
			// FemaleColumn
			// 
			this.FemaleColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.FemaleColumn.DataPropertyName = "Female";
			dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle16.Format = "0";
			this.FemaleColumn.DefaultCellStyle = dataGridViewCellStyle16;
			this.FemaleColumn.HeaderText = "Female";
			this.FemaleColumn.MaxInputLength = 3;
			this.FemaleColumn.MinimumWidth = 50;
			this.FemaleColumn.Name = "FemaleColumn";
			this.FemaleColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.FemaleColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.FemaleColumn.ToolTipText = "Voice popularity. Value from 0 (don\'t use) to 100 (often usage)";
			this.FemaleColumn.Width = 50;
			// 
			// NeutralColumn
			// 
			this.NeutralColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.NeutralColumn.DataPropertyName = "Neutral";
			dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle17.Format = "0";
			this.NeutralColumn.DefaultCellStyle = dataGridViewCellStyle17;
			this.NeutralColumn.HeaderText = "Neutral";
			this.NeutralColumn.MaxInputLength = 3;
			this.NeutralColumn.MinimumWidth = 50;
			this.NeutralColumn.Name = "NeutralColumn";
			this.NeutralColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.NeutralColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.NeutralColumn.ToolTipText = "Voice popularity. Value from 0 (don\'t use) to 100 (often usage)";
			this.NeutralColumn.Width = 50;
			// 
			// GenderColumn
			// 
			this.GenderColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.GenderColumn.DataPropertyName = "Gender";
			dataGridViewCellStyle18.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.GenderColumn.DefaultCellStyle = dataGridViewCellStyle18;
			this.GenderColumn.HeaderText = "Gender";
			this.GenderColumn.Name = "GenderColumn";
			this.GenderColumn.ReadOnly = true;
			this.GenderColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.GenderColumn.Width = 50;
			// 
			// NameColumn
			// 
			this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.NameColumn.DataPropertyName = "Name";
			dataGridViewCellStyle19.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.NameColumn.DefaultCellStyle = dataGridViewCellStyle19;
			this.NameColumn.HeaderText = "Name";
			this.NameColumn.Name = "NameColumn";
			this.NameColumn.ReadOnly = true;
			this.NameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.NameColumn.Width = 43;
			// 
			// CultureColumn
			// 
			this.CultureColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.CultureColumn.DataPropertyName = "CultureName";
			dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle20.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.CultureColumn.DefaultCellStyle = dataGridViewCellStyle20;
			this.CultureColumn.HeaderText = "Culture";
			this.CultureColumn.Name = "CultureColumn";
			this.CultureColumn.ReadOnly = true;
			this.CultureColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.CultureColumn.Width = 48;
			// 
			// LcidColumn
			// 
			this.LcidColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.LcidColumn.DataPropertyName = "CultureLCID";
			dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle21.Format = "X";
			dataGridViewCellStyle21.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.LcidColumn.DefaultCellStyle = dataGridViewCellStyle21;
			this.LcidColumn.HeaderText = "LCID";
			this.LcidColumn.Name = "LcidColumn";
			this.LcidColumn.ReadOnly = true;
			this.LcidColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.LcidColumn.Width = 39;
			// 
			// LanguageColumn
			// 
			this.LanguageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.LanguageColumn.DataPropertyName = "Language";
			dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle22.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.LanguageColumn.DefaultCellStyle = dataGridViewCellStyle22;
			this.LanguageColumn.HeaderText = "Language";
			this.LanguageColumn.Name = "LanguageColumn";
			this.LanguageColumn.ReadOnly = true;
			this.LanguageColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.LanguageColumn.Width = 63;
			// 
			// AgeColumn
			// 
			this.AgeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.AgeColumn.DataPropertyName = "Age";
			dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle23.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.AgeColumn.DefaultCellStyle = dataGridViewCellStyle23;
			this.AgeColumn.HeaderText = "Age";
			this.AgeColumn.Name = "AgeColumn";
			this.AgeColumn.ReadOnly = true;
			this.AgeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.AgeColumn.Width = 34;
			// 
			// DescriptionColumn
			// 
			this.DescriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.DescriptionColumn.DataPropertyName = "Description";
			dataGridViewCellStyle24.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.DescriptionColumn.DefaultCellStyle = dataGridViewCellStyle24;
			this.DescriptionColumn.HeaderText = "Description";
			this.DescriptionColumn.Name = "DescriptionColumn";
			this.DescriptionColumn.ReadOnly = true;
			this.DescriptionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// VoicesToolStrip
			// 
			this.VoicesToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.VoicesToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddLocalVoicesButton,
            this.AddAmazonNeuralVoicesButton,
            this.AddAmazonStandardVoicesButton,
            this.RemoveButton});
			this.VoicesToolStrip.Location = new System.Drawing.Point(0, 0);
			this.VoicesToolStrip.Name = "VoicesToolStrip";
			this.VoicesToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.VoicesToolStrip.Size = new System.Drawing.Size(742, 25);
			this.VoicesToolStrip.TabIndex = 4;
			// 
			// AddLocalVoicesButton
			// 
			this.AddLocalVoicesButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Local_Add_16x16;
			this.AddLocalVoicesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddLocalVoicesButton.Name = "AddLocalVoicesButton";
			this.AddLocalVoicesButton.Size = new System.Drawing.Size(125, 22);
			this.AddLocalVoicesButton.Text = "Add Local Voices...";
			this.AddLocalVoicesButton.Click += new System.EventHandler(this.AddLocalVoicesButton_Click);
			// 
			// AddAmazonNeuralVoicesButton
			// 
			this.AddAmazonNeuralVoicesButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Amazon_Add_Neural_16x16;
			this.AddAmazonNeuralVoicesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddAmazonNeuralVoicesButton.Name = "AddAmazonNeuralVoicesButton";
			this.AddAmazonNeuralVoicesButton.Size = new System.Drawing.Size(179, 22);
			this.AddAmazonNeuralVoicesButton.Text = "Add Amazon Neural Voices...";
			this.AddAmazonNeuralVoicesButton.Click += new System.EventHandler(this.AddAmazonNeuralVoicesButton_Click);
			// 
			// AddAmazonStandardVoicesButton
			// 
			this.AddAmazonStandardVoicesButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Amazon_Add_Standard_16x16;
			this.AddAmazonStandardVoicesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddAmazonStandardVoicesButton.Name = "AddAmazonStandardVoicesButton";
			this.AddAmazonStandardVoicesButton.Size = new System.Drawing.Size(191, 22);
			this.AddAmazonStandardVoicesButton.Text = "Add Amazon Standard Voices...";
			this.AddAmazonStandardVoicesButton.Click += new System.EventHandler(this.AddAmazonStandardVoicesButton_Click);
			// 
			// RemoveButton
			// 
			this.RemoveButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.RemoveButton.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.Remove_16x16;
			this.RemoveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.RemoveButton.Name = "RemoveButton";
			this.RemoveButton.Size = new System.Drawing.Size(70, 22);
			this.RemoveButton.Text = "Remove";
			// 
			// VoicesUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.VoicesDataGridView);
			this.Controls.Add(this.VoicesToolStrip);
			this.Name = "VoicesUserControl";
			this.Size = new System.Drawing.Size(742, 226);
			((System.ComponentModel.ISupportInitialize)(this.VoicesDataGridView)).EndInit();
			this.VoicesToolStrip.ResumeLayout(false);
			this.VoicesToolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView VoicesDataGridView;
		private System.Windows.Forms.DataGridViewCheckBoxColumn EnabledColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn MaleColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn FemaleColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn NeutralColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn GenderColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn CultureColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn LcidColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn LanguageColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn AgeColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn DescriptionColumn;
		private System.Windows.Forms.ToolStrip VoicesToolStrip;
		private System.Windows.Forms.ToolStripButton AddAmazonStandardVoicesButton;
		private System.Windows.Forms.ToolStripButton RemoveButton;
		private System.Windows.Forms.ToolStripButton AddLocalVoicesButton;
		private System.Windows.Forms.ToolStripButton AddAmazonNeuralVoicesButton;
	}
}
