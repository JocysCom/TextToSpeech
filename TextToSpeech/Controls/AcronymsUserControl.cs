using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
	public partial class AcronymsUserControl : UserControl
	{
		public AcronymsUserControl()
		{
			InitializeComponent();
		}

		private void SettingsControl_Load(object sender, EventArgs e)
		{
			var list = SettingsManager.Current.Acronyms;
			SettingsControl.DataGridView.DataSource = SettingsManager.Current.Acronyms.Items;
			SettingsControl.Data = SettingsManager.Current.Acronyms;
			SettingsControl.SettingsDataGridView.CellValidating += SettingsDataGridView_CellValidating;
		}

		private void SettingsDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			var grid = (DataGridView)sender;
			var row = grid.Rows[e.RowIndex];
			row.ErrorText = "";
			// Don't try to validate the 'new row' until finished  
			// editing since there is no point in validating its initial values. 
			if (row.IsNewRow) { return; }
			var item = (Acronym)row.DataBoundItem;
			var column = grid.Columns[e.ColumnIndex];
			string error = null;
			var newValue = e.FormattedValue.ToString().ToLower();
			var list = SettingsManager.Current.Acronyms;
			// If Group Name changed then...
			if (e.ColumnIndex == GroupColumn.Index && newValue != item.Group.ToLower())
			{
				var key = item.Key.ToLower();
				if (string.IsNullOrEmpty(newValue))
				{
					error = "Group field value must be not empty!";
				}
				else if (list.Items.Any(x => x.Group.ToLower() == newValue && x.Key.ToLower() == key))
				{
					error = "Group/Key value must be unique!";
				}
			}
			else if (e.ColumnIndex == KeyColumn.Index && newValue != item.Key.ToLower())
			{
				var group = item.Group.ToLower();
				if (string.IsNullOrEmpty(newValue))
				{
					error = "Key field value must be not empty!";
				}
				else if (list.Items.Any(x => x.Group.ToLower() == group && x.Key.ToLower() == newValue))
				{
					error = "Group/Key value must be unique!";
				}
			}
			if (!string.IsNullOrEmpty(error))
			{
				e.Cancel = true;
				row.ErrorText = error;
			}
		}

		private void SettingsControl_MouseHover(object sender, EventArgs e)
		{
			Program.TopForm.MainHelpLabel.Text = "Here you can manage acronym list wich will be automatically used when text is converted from text to speech.";
		}

		private void SettingsControl_MouseLeave(object sender, EventArgs e)
		{
			Program.TopForm.ResetHelpToDefault();
		}
	}
}
