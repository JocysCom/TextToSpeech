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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.SettingsControl = new JocysCom.ClassLibrary.Configuration.SettingsUserControl();
			this.EnabledColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.GroupColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.KeyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.RxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.EmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SuspendLayout();
			// 
			// SettingsControl
			// 
			this.SettingsControl.Data = null;
			this.SettingsControl.DataGridViewColumns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EnabledColumn,
            this.GroupColumn,
            this.KeyColumn,
            this.ValueColumn,
            this.RxColumn,
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
			// ValueColumn
			// 
			this.ValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.ValueColumn.DataPropertyName = "Value";
			this.ValueColumn.HeaderText = "Value";
			this.ValueColumn.Name = "ValueColumn";
			this.ValueColumn.Width = 61;
			// 
			// RxColumn
			// 
			this.RxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.RxColumn.DataPropertyName = "Rx";
			this.RxColumn.HeaderText = "RX";
			this.RxColumn.Name = "RxColumn";
			this.RxColumn.Width = 49;
			// 
			// EmptyColumn
			// 
			this.EmptyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			this.EmptyColumn.DefaultCellStyle = dataGridViewCellStyle1;
			this.EmptyColumn.HeaderText = "";
			this.EmptyColumn.Name = "EmptyColumn";
			this.EmptyColumn.ReadOnly = true;
			// 
			// AcronymsUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.SettingsControl);
			this.Name = "AcronymsUserControl";
			this.Size = new System.Drawing.Size(640, 320);
			this.ResumeLayout(false);

		}

		#endregion

		private ClassLibrary.Configuration.SettingsUserControl SettingsControl;
		private System.Windows.Forms.DataGridViewCheckBoxColumn EnabledColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn GroupColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn KeyColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn ValueColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn RxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn EmptyColumn;
	}
}
