namespace JocysCom.TextToSpeech.Monitor.Controls
{
	partial class AcronymsUserControl
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.GridContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.SettingsControl = new JocysCom.ClassLibrary.Configuration.SettingsUserControl();
			this.EnabledColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.GroupColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.KeyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.RxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.EmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DelectAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SelectAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CheckSelectedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.UncheckSelectedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.GridContextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// GridContextMenuStrip
			// 
			this.GridContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectAllMenuItem,
            this.DelectAllMenuItem,
            this.CheckSelectedMenuItem,
            this.UncheckSelectedMenuItem});
			this.GridContextMenuStrip.Name = "contextMenuStrip1";
			this.GridContextMenuStrip.Size = new System.Drawing.Size(168, 92);
			// 
			// SettingsControl
			// 
			this.SettingsControl.Data = null;
			this.SettingsControl.DataGridViewColumns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EnabledColumn,
            this.GroupColumn,
            this.KeyColumn,
            this.RxColumn,
            this.ValueColumn,
            this.EmptyColumn});
			this.SettingsControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SettingsControl.Location = new System.Drawing.Point(0, 0);
			this.SettingsControl.Name = "SettingsControl";
			this.SettingsControl.Size = new System.Drawing.Size(640, 320);
			this.SettingsControl.TabIndex = 0;
			this.SettingsControl.Load += new System.EventHandler(this.SettingsControl_Load);
			this.SettingsControl.MouseLeave += new System.EventHandler(this.SettingsControl_MouseLeave);
			this.SettingsControl.MouseHover += new System.EventHandler(this.SettingsControl_MouseHover);
			// 
			// EnabledColumn
			// 
			this.EnabledColumn.DataPropertyName = "Enabled";
			this.EnabledColumn.HeaderText = "ON";
			this.EnabledColumn.Name = "EnabledColumn";
			this.EnabledColumn.ReadOnly = true;
			this.EnabledColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.EnabledColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.EnabledColumn.Width = 32;
			// 
			// GroupColumn
			// 
			this.GroupColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.GroupColumn.DataPropertyName = "Group";
			this.GroupColumn.HeaderText = "Group";
			this.GroupColumn.Name = "GroupColumn";
			this.GroupColumn.Width = 63;
			// 
			// KeyColumn
			// 
			this.KeyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.KeyColumn.DataPropertyName = "Key";
			this.KeyColumn.HeaderText = "Key";
			this.KeyColumn.Name = "KeyColumn";
			this.KeyColumn.Width = 52;
			// 
			// RxColumn
			// 
			this.RxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.RxColumn.DataPropertyName = "Rx";
			this.RxColumn.HeaderText = "Key-Regex";
			this.RxColumn.Name = "RxColumn";
			this.RxColumn.Width = 86;
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
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			this.EmptyColumn.DefaultCellStyle = dataGridViewCellStyle2;
			this.EmptyColumn.HeaderText = "";
			this.EmptyColumn.Name = "EmptyColumn";
			this.EmptyColumn.ReadOnly = true;
			// 
			// DelectAllMenuItem
			// 
			this.DelectAllMenuItem.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.SelectNone;
			this.DelectAllMenuItem.Name = "DelectAllMenuItem";
			this.DelectAllMenuItem.Size = new System.Drawing.Size(167, 22);
			this.DelectAllMenuItem.Text = "Deselect All";
			this.DelectAllMenuItem.Click += new System.EventHandler(this.DelectAllMenuItem_Click);
			// 
			// SelectAllMenuItem
			// 
			this.SelectAllMenuItem.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.SelectAll;
			this.SelectAllMenuItem.Name = "SelectAllMenuItem";
			this.SelectAllMenuItem.Size = new System.Drawing.Size(167, 22);
			this.SelectAllMenuItem.Text = "Select All";
			this.SelectAllMenuItem.Click += new System.EventHandler(this.SelectAllMenuItem_Click);
			// 
			// CheckSelectedMenuItem
			// 
			this.CheckSelectedMenuItem.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.CheckBox;
			this.CheckSelectedMenuItem.Name = "CheckSelectedMenuItem";
			this.CheckSelectedMenuItem.Size = new System.Drawing.Size(167, 22);
			this.CheckSelectedMenuItem.Text = "Check Selected";
			this.CheckSelectedMenuItem.Click += new System.EventHandler(this.CheckSelectedMenuItem_Click);
			// 
			// UncheckSelectedMenuItem
			// 
			this.UncheckSelectedMenuItem.Image = global::JocysCom.TextToSpeech.Monitor.Properties.Resources.CheckBox_Unchecked;
			this.UncheckSelectedMenuItem.Name = "UncheckSelectedMenuItem";
			this.UncheckSelectedMenuItem.Size = new System.Drawing.Size(167, 22);
			this.UncheckSelectedMenuItem.Text = "Uncheck Selected";
			this.UncheckSelectedMenuItem.Click += new System.EventHandler(this.UncheckSelectedMenuItem_Click);
			// 
			// AcronymsUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.SettingsControl);
			this.Name = "AcronymsUserControl";
			this.Size = new System.Drawing.Size(640, 320);
			this.GridContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private ClassLibrary.Configuration.SettingsUserControl SettingsControl;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EnabledColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmptyColumn;
		private System.Windows.Forms.ContextMenuStrip GridContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem CheckSelectedMenuItem;
		private System.Windows.Forms.ToolStripMenuItem UncheckSelectedMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SelectAllMenuItem;
		private System.Windows.Forms.ToolStripMenuItem DelectAllMenuItem;
	}
}
