using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace JocysCom.WoW.TextToSpeech.Controls
{
	public partial class AboutControl : UserControl
	{
		public AboutControl()
		{
			InitializeComponent();
		}

		void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			MainHelper.OpenUrl(((Control)sender).Text);
		}

		void AboutControl_Load(object sender, EventArgs e)
		{
			var stream = MainHelper.GetResource("About.txt");
			var sr = new StreamReader(stream);
			ChangeLogTextBox.Text = sr.ReadToEnd();
            AboutProductLabel.Text = MainHelper.GetProductFullName();
		}
	
	}
}
