namespace JocysCom.TextToSpeech.Monitor.Controls
{
	partial class OptionsControl
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
            this.AddSilenceGroupBox = new System.Windows.Forms.GroupBox();
            this.AddSilenceAfterLabel = new System.Windows.Forms.Label();
            this.AddSilenceBeforeLabel = new System.Windows.Forms.Label();
            this.AddSilenceAfterNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.AddSilcenceBeforeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.SilenceBeforeTagLabel = new System.Windows.Forms.Label();
            this.SilenceAfterTagLabel = new System.Windows.Forms.Label();
            this.AddSilenceGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddSilenceAfterNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddSilcenceBeforeNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // AddSilenceGroupBox
            // 
            this.AddSilenceGroupBox.Controls.Add(this.AddSilenceAfterLabel);
            this.AddSilenceGroupBox.Controls.Add(this.AddSilenceBeforeLabel);
            this.AddSilenceGroupBox.Location = new System.Drawing.Point(3, 3);
            this.AddSilenceGroupBox.Name = "AddSilenceGroupBox";
            this.AddSilenceGroupBox.Size = new System.Drawing.Size(406, 79);
            this.AddSilenceGroupBox.TabIndex = 0;
            this.AddSilenceGroupBox.TabStop = false;
            this.AddSilenceGroupBox.Text = "Silence";
            // 
            // AddSilenceAfterLabel
            // 
            this.AddSilenceAfterLabel.AutoSize = true;
            this.AddSilenceAfterLabel.Location = new System.Drawing.Point(15, 48);
            this.AddSilenceAfterLabel.Name = "AddSilenceAfterLabel";
            this.AddSilenceAfterLabel.Size = new System.Drawing.Size(261, 13);
            this.AddSilenceAfterLabel.TabIndex = 6;
            this.AddSilenceAfterLabel.Text = "Add Silence After Message ( default value is 0 ) [ ms ]:";
            // 
            // AddSilenceBeforeLabel
            // 
            this.AddSilenceBeforeLabel.AutoSize = true;
            this.AddSilenceBeforeLabel.Location = new System.Drawing.Point(6, 22);
            this.AddSilenceBeforeLabel.Name = "AddSilenceBeforeLabel";
            this.AddSilenceBeforeLabel.Size = new System.Drawing.Size(270, 13);
            this.AddSilenceBeforeLabel.TabIndex = 6;
            this.AddSilenceBeforeLabel.Text = "Add Silence Before Message ( default value is 0 ) [ ms ]:";
            // 
            // AddSilenceAfterNumericUpDown
            // 
            this.AddSilenceAfterNumericUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::JocysCom.TextToSpeech.Monitor.Properties.Settings.Default, "DelayBeforeValue", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.AddSilenceAfterNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.AddSilenceAfterNumericUpDown.Location = new System.Drawing.Point(282, 48);
            this.AddSilenceAfterNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.AddSilenceAfterNumericUpDown.Name = "AddSilenceAfterNumericUpDown";
            this.AddSilenceAfterNumericUpDown.Size = new System.Drawing.Size(114, 20);
            this.AddSilenceAfterNumericUpDown.TabIndex = 5;
            this.AddSilenceAfterNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AddSilenceAfterNumericUpDown.Value = global::JocysCom.TextToSpeech.Monitor.Properties.Settings.Default.DelayBeforeValue;
            this.AddSilenceAfterNumericUpDown.ValueChanged += new System.EventHandler(this.AddSilenceAfterNumericUpDown_ValueChanged);
            // 
            // AddSilcenceBeforeNumericUpDown
            // 
            this.AddSilcenceBeforeNumericUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::JocysCom.TextToSpeech.Monitor.Properties.Settings.Default, "AddSilcenceBeforeMessage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.AddSilcenceBeforeNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.AddSilcenceBeforeNumericUpDown.Location = new System.Drawing.Point(282, 22);
            this.AddSilcenceBeforeNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.AddSilcenceBeforeNumericUpDown.Name = "AddSilcenceBeforeNumericUpDown";
            this.AddSilcenceBeforeNumericUpDown.Size = new System.Drawing.Size(114, 20);
            this.AddSilcenceBeforeNumericUpDown.TabIndex = 5;
            this.AddSilcenceBeforeNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AddSilcenceBeforeNumericUpDown.Value = global::JocysCom.TextToSpeech.Monitor.Properties.Settings.Default.AddSilcenceBeforeMessage;
            this.AddSilcenceBeforeNumericUpDown.ValueChanged += new System.EventHandler(this.AddSilcenceBeforeNumericUpDown_ValueChanged);
            // 
            // SilenceBeforeTagLabel
            // 
            this.SilenceBeforeTagLabel.AutoSize = true;
            this.SilenceBeforeTagLabel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SilenceBeforeTagLabel.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.SilenceBeforeTagLabel.Location = new System.Drawing.Point(417, 25);
            this.SilenceBeforeTagLabel.Name = "SilenceBeforeTagLabel";
            this.SilenceBeforeTagLabel.Size = new System.Drawing.Size(161, 14);
            this.SilenceBeforeTagLabel.TabIndex = 7;
            this.SilenceBeforeTagLabel.Text = "<silence msec=\"3000\"/>";
            // 
            // SilenceAfterTagLabel
            // 
            this.SilenceAfterTagLabel.AutoSize = true;
            this.SilenceAfterTagLabel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SilenceAfterTagLabel.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.SilenceAfterTagLabel.Location = new System.Drawing.Point(417, 51);
            this.SilenceAfterTagLabel.Name = "SilenceAfterTagLabel";
            this.SilenceAfterTagLabel.Size = new System.Drawing.Size(161, 14);
            this.SilenceAfterTagLabel.TabIndex = 8;
            this.SilenceAfterTagLabel.Text = "<silence msec=\"3000\"/>";
            // 
            // OptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SilenceAfterTagLabel);
            this.Controls.Add(this.AddSilenceAfterNumericUpDown);
            this.Controls.Add(this.AddSilcenceBeforeNumericUpDown);
            this.Controls.Add(this.SilenceBeforeTagLabel);
            this.Controls.Add(this.AddSilenceGroupBox);
            this.Name = "OptionsControl";
            this.Size = new System.Drawing.Size(639, 434);
            this.AddSilenceGroupBox.ResumeLayout(false);
            this.AddSilenceGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddSilenceAfterNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddSilcenceBeforeNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox AddSilenceGroupBox;
		private System.Windows.Forms.NumericUpDown AddSilcenceBeforeNumericUpDown;
		private System.Windows.Forms.NumericUpDown AddSilenceAfterNumericUpDown;
		private System.Windows.Forms.Label AddSilenceAfterLabel;
		private System.Windows.Forms.Label AddSilenceBeforeLabel;
        private System.Windows.Forms.Label SilenceBeforeTagLabel;
        private System.Windows.Forms.Label SilenceAfterTagLabel;
    }
}
