namespace JocysCom.ClassLibrary.Controls
{
	partial class MessageBoxForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Button3 = new System.Windows.Forms.Button();
			this.Button2 = new System.Windows.Forms.Button();
			this.Button1 = new System.Windows.Forms.Button();
			this.IconPictureBox = new System.Windows.Forms.PictureBox();
			this.TextLabel = new System.Windows.Forms.Label();
			this.CopyMenuStrip = new System.Windows.Forms.MenuStrip();
			this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.IconPictureBox)).BeginInit();
			this.CopyMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// Button3
			// 
			this.Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Button3.Location = new System.Drawing.Point(253, 105);
			this.Button3.Name = "Button3";
			this.Button3.Size = new System.Drawing.Size(75, 25);
			this.Button3.TabIndex = 9;
			this.Button3.Text = "Button3";
			// 
			// Button2
			// 
			this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Button2.Location = new System.Drawing.Point(172, 105);
			this.Button2.Name = "Button2";
			this.Button2.Size = new System.Drawing.Size(75, 25);
			this.Button2.TabIndex = 8;
			this.Button2.Text = "Button2";
			// 
			// Button1
			// 
			this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Button1.Location = new System.Drawing.Point(91, 105);
			this.Button1.Name = "Button1";
			this.Button1.Size = new System.Drawing.Size(75, 25);
			this.Button1.TabIndex = 6;
			this.Button1.Text = "Button1";
			// 
			// IconPictureBox
			// 
			this.IconPictureBox.BackColor = System.Drawing.Color.Transparent;
			this.IconPictureBox.Location = new System.Drawing.Point(12, 12);
			this.IconPictureBox.Name = "IconPictureBox";
			this.IconPictureBox.Size = new System.Drawing.Size(32, 32);
			this.IconPictureBox.TabIndex = 7;
			this.IconPictureBox.TabStop = false;
			// 
			// TextLabel
			// 
			this.TextLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TextLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
			this.TextLabel.Location = new System.Drawing.Point(50, 12);
			this.TextLabel.Name = "TextLabel";
			this.TextLabel.Size = new System.Drawing.Size(278, 85);
			this.TextLabel.TabIndex = 5;
			this.TextLabel.Text = "[TextLabel]";
			// 
			// CopyMenuStrip
			// 
			this.CopyMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.CopyMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyToolStripMenuItem});
			this.CopyMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.CopyMenuStrip.Name = "CopyMenuStrip";
			this.CopyMenuStrip.Size = new System.Drawing.Size(340, 24);
			this.CopyMenuStrip.TabIndex = 10;
			this.CopyMenuStrip.Text = "MenuStrip";
			// 
			// CopyToolStripMenuItem
			// 
			this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
			this.CopyToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
			this.CopyToolStripMenuItem.Text = "Copy";
			// 
			// MessageBoxForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(340, 142);
			this.Controls.Add(this.Button3);
			this.Controls.Add(this.Button2);
			this.Controls.Add(this.Button1);
			this.Controls.Add(this.IconPictureBox);
			this.Controls.Add(this.TextLabel);
			this.Controls.Add(this.CopyMenuStrip);
			this.Name = "MessageBoxForm";
			this.Text = "MessageBoxForm";
			((System.ComponentModel.ISupportInitialize)(this.IconPictureBox)).EndInit();
			this.CopyMenuStrip.ResumeLayout(false);
			this.CopyMenuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		internal System.Windows.Forms.Button Button3;
		internal System.Windows.Forms.Button Button2;
		internal System.Windows.Forms.Button Button1;
		internal System.Windows.Forms.PictureBox IconPictureBox;
		internal System.Windows.Forms.Label TextLabel;
		internal System.Windows.Forms.MenuStrip CopyMenuStrip;
		internal System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;


	}
}