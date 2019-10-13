using System;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
	public partial class AddVoicesForm : Form
	{
		public AddVoicesForm()
		{
			InitializeComponent();
		}

		public DataGridView VoicesGridView
		{
			get { return VoicesPanel.VoicesGridView; }
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void CloseButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void RefreshButton_Click(object sender, EventArgs e)
		{
			VoicesPanel.RefreshVoices(true);
		}

		private void AddVoicesForm_Load(object sender, EventArgs e)
		{
			VoicesPanel.RefreshVoices();
		}
	}
}
