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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle51 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle52 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle47 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle48 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle49 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle50 = new System.Windows.Forms.DataGridViewCellStyle();
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
			this.VoiceErrorLabel = new System.Windows.Forms.Label();
			this.VoiceErrorSeparatorLabel = new System.Windows.Forms.Label();
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
			dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle40.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle40.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle40.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
			dataGridViewCellStyle40.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle40.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle40.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.VoicesDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle40;
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
			dataGridViewCellStyle51.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle51.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridViewCellStyle51.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle51.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle51.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle51.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle51.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.VoicesDataGridView.DefaultCellStyle = dataGridViewCellStyle51;
			this.VoicesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.VoicesDataGridView.EnableHeadersVisualStyles = false;
			this.VoicesDataGridView.GridColor = System.Drawing.SystemColors.Control;
			this.VoicesDataGridView.Location = new System.Drawing.Point(0, 25);
			this.VoicesDataGridView.Margin = new System.Windows.Forms.Padding(0);
			this.VoicesDataGridView.Name = "VoicesDataGridView";
			this.VoicesDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle52.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle52.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle52.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle52.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle52.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle52.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.VoicesDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle52;
			this.VoicesDataGridView.RowHeadersVisible = false;
			this.VoicesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.VoicesDataGridView.Size = new System.Drawing.Size(742, 182);
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
			dataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle41.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle41.Format = "0";
			this.MaleColumn.DefaultCellStyle = dataGridViewCellStyle41;
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
			dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle42.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle42.Format = "0";
			this.FemaleColumn.DefaultCellStyle = dataGridViewCellStyle42;
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
			dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle43.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle43.Format = "0";
			this.NeutralColumn.DefaultCellStyle = dataGridViewCellStyle43;
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
			dataGridViewCellStyle44.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.GenderColumn.DefaultCellStyle = dataGridViewCellStyle44;
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
			dataGridViewCellStyle45.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.NameColumn.DefaultCellStyle = dataGridViewCellStyle45;
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
			dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle46.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.CultureColumn.DefaultCellStyle = dataGridViewCellStyle46;
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
			dataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle47.Format = "X";
			dataGridViewCellStyle47.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.LcidColumn.DefaultCellStyle = dataGridViewCellStyle47;
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
			dataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle48.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.LanguageColumn.DefaultCellStyle = dataGridViewCellStyle48;
			this.LanguageColumn.HeaderText = "Language";
			this.LanguageColumn.Name = "LanguageColumn";
			this.LanguageColumn.ReadOnly = true;
			this.LanguageColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.LanguageColumn.Visible = false;
			// 
			// AgeColumn
			// 
			this.AgeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.AgeColumn.DataPropertyName = "Age";
			dataGridViewCellStyle49.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle49.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.AgeColumn.DefaultCellStyle = dataGridViewCellStyle49;
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
			dataGridViewCellStyle50.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.DescriptionColumn.DefaultCellStyle = dataGridViewCellStyle50;
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
			this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
			// 
			// VoiceErrorLabel
			// 
			this.VoiceErrorLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.VoiceErrorLabel.ForeColor = System.Drawing.Color.DarkRed;
			this.VoiceErrorLabel.Location = new System.Drawing.Point(0, 207);
			this.VoiceErrorLabel.Name = "VoiceErrorLabel";
			this.VoiceErrorLabel.Padding = new System.Windows.Forms.Padding(3);
			this.VoiceErrorLabel.Size = new System.Drawing.Size(742, 19);
			this.VoiceErrorLabel.TabIndex = 5;
			this.VoiceErrorLabel.Text = "Voice Error";
			this.VoiceErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.VoiceErrorLabel.Visible = false;
			// 
			// VoiceErrorSeparatorLabel
			// 
			this.VoiceErrorSeparatorLabel.BackColor = System.Drawing.SystemColors.Window;
			this.VoiceErrorSeparatorLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.VoiceErrorSeparatorLabel.Location = new System.Drawing.Point(0, 205);
			this.VoiceErrorSeparatorLabel.Name = "VoiceErrorSeparatorLabel";
			this.VoiceErrorSeparatorLabel.Padding = new System.Windows.Forms.Padding(3);
			this.VoiceErrorSeparatorLabel.Size = new System.Drawing.Size(742, 2);
			this.VoiceErrorSeparatorLabel.TabIndex = 6;
			this.VoiceErrorSeparatorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.VoiceErrorSeparatorLabel.Visible = false;
			// 
			// VoicesUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.VoiceErrorSeparatorLabel);
			this.Controls.Add(this.VoicesDataGridView);
			this.Controls.Add(this.VoiceErrorLabel);
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
		private System.Windows.Forms.ToolStrip VoicesToolStrip;
		private System.Windows.Forms.ToolStripButton AddAmazonStandardVoicesButton;
		private System.Windows.Forms.ToolStripButton RemoveButton;
		private System.Windows.Forms.ToolStripButton AddLocalVoicesButton;
		private System.Windows.Forms.ToolStripButton AddAmazonNeuralVoicesButton;
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
		private System.Windows.Forms.Label VoiceErrorLabel;
		private System.Windows.Forms.Label VoiceErrorSeparatorLabel;
	}
}
