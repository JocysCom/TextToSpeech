using JocysCom.ClassLibrary.Controls;
using JocysCom.TextToSpeech.Monitor.Audio;
using System;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Controls
{

	public partial class OptionsControl : UserControl
	{

		public OptionsControl()
		{
			InitializeComponent();
			if (ControlsHelper.IsDesignMode(this))
				return;
			var isElevated = JocysCom.ClassLibrary.Security.PermissionHelper.IsElevated;
			//// Hide clipboard for later.
			OptionsTabControl.TabPages.Remove(MonitorClipBoardTabPage);
			//// Make Google Cloud invisible, because it is not finished yet.
			OptionsTabControl.TabPages.Remove(GoogleTTSTabPage);
			//ControlsHelper.ApplyImageStyle(OptionsTabControl);
		}

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



	}
}
