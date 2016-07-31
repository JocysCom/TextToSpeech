using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Controls
{

	public partial class OptionsControl : UserControl
	{

        private void SilenceBefore()
        {
            // Show or hide silence before message tag.
            int silenceIntBeforeTag = Decimal.ToInt32(AddSilcenceBeforeNumericUpDown.Value);
            string silenceStringBeforeTag = AddSilcenceBeforeNumericUpDown.Value.ToString();
            if (silenceIntBeforeTag > 0)
            {
                SilenceBeforeTagLabel.Text = "<silence msec=\"" + silenceStringBeforeTag + "\" />";
            }
            else
            {
                SilenceBeforeTagLabel.Text = "";
            }
        }

        private void SilenceAfter()
        {
            // Show or hide silence after message tag.
            int silenceIntAfterTag = Decimal.ToInt32(AddSilenceAfterNumericUpDown.Value);
            string silenceStringAfterTag = AddSilenceAfterNumericUpDown.Value.ToString();
            if (silenceIntAfterTag > 0)
            {
                SilenceAfterTagLabel.Text = "<silence msec=\"" + silenceStringAfterTag + "\" />";
            }
            else
            {
                SilenceAfterTagLabel.Text = "";
            }
        }

        public Decimal silenceBefore
        {
            get { return AddSilcenceBeforeNumericUpDown.Value; }
            set { AddSilcenceBeforeNumericUpDown.Value = value; }
        }

        public Decimal silenceAfter
        {
            get { return AddSilenceAfterNumericUpDown.Value; }
            set { AddSilenceAfterNumericUpDown.Value = value; }
        }

       public OptionsControl()
		{
			InitializeComponent();
            SilenceBefore();
            SilenceAfter();
        }

        private void AddSilcenceBeforeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            SilenceBefore();
        }

        private void AddSilenceAfterNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            SilenceAfter();
        }

        string DefaultLoggingFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\JocysComMonitorLog.txt";
        private void OptionsControl_Load(object sender, EventArgs e)
        {
            if (LoggingFolderTextBox.Text.Length == 0)
            {
                LoggingFolderTextBox.Text = DefaultLoggingFolder;
            }

        }

        private void LoggingButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoggingFolderTextBox.Text = fbd.SelectedPath + "\\JocysComMonitorLog.txt";
            }
        }

        private void LoggingDefaultButton_Click(object sender, EventArgs e)
        {
            LoggingFolderTextBox.Text = DefaultLoggingFolder;
        }


    }
}
