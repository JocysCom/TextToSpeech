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
			this.settingsUserControl1 = new JocysCom.ClassLibrary.Configuration.SettingsUserControl();
			this.Column1sdfdsfs = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SuspendLayout();
			// 
			// settingsUserControl1
			// 
			this.settingsUserControl1.Location = new System.Drawing.Point(24, 12);
			this.settingsUserControl1.DataGridViewColumns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1sdfdsfs});
			this.settingsUserControl1.Name = "settingsUserControl1";
			this.settingsUserControl1.Size = new System.Drawing.Size(551, 280);
			this.settingsUserControl1.TabIndex = 0;
			// 
			// Column1sdfdsfs
			// 
			this.Column1sdfdsfs.HeaderText = "Column1";
			this.Column1sdfdsfs.Name = "Column1sdfdsfs";
			// 
			// AcronymsUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.settingsUserControl1);
			this.Name = "AcronymsUserControl";
			this.Size = new System.Drawing.Size(641, 384);
			this.ResumeLayout(false);

		}

		#endregion

		private ClassLibrary.Configuration.SettingsUserControl settingsUserControl1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1sdfdsfs;
	}
}
