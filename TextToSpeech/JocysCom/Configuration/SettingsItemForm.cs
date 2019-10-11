using System.Windows.Forms;

namespace JocysCom.ClassLibrary.Configuration
{
	public partial class SettingsItemForm : Form
	{
		public SettingsItemForm()
		{
			InitializeComponent();
			var info = new AssemblyInfo();
			Text = info.GetTitle(true, true, true, true, false);
		}
	}
}
