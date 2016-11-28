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
			SettingsControl.DefaultEditColumn = KeyColumn;
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
			var newValue = e.FormattedValue.ToString().Trim().ToLower();
			newValue = string.IsNullOrEmpty(newValue) ? null : newValue;
			var list = SettingsManager.Current.Acronyms;
			// If Group Name changed then...
			if (item.IsEmpty && string.IsNullOrEmpty(newValue))
			{
				SettingsControl.Data.Remove(item);
				return;
			}
			if (e.ColumnIndex == GroupColumn.Index && string.Compare(newValue, item.Group, true) != 0)
			{
				if (list.Items.Any(x => string.Compare(x.Group, newValue, true) == 0 && string.Compare(x.Key, item.Key, true) == 0))
				{
					error = "Group/Key must be unique!";
				}
			}
			else if (e.ColumnIndex == KeyColumn.Index && string.Compare(newValue, item.Key, true) != 0)
			{
				if (string.IsNullOrEmpty(newValue))
				{
					error = "Key field must be not empty!";
				}
				else if (list.Items.Any(x => string.Compare(x.Group, item.Group, true) == 0 && string.Compare(x.Key, newValue, true) == 0))
				{
					error = "Group/Key must be unique!";
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
