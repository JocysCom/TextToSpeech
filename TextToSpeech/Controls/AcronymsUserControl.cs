using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JocysCom.ClassLibrary.Configuration;
using JocysCom.ClassLibrary.ComponentModel;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
	public partial class AcronymsUserControl : UserControl
	{
		public AcronymsUserControl()
		{
			InitializeComponent();
			if (IsDesignMode)
				return;
			SettingsControl.DefaultEditColumn = KeyColumn;
		}

		public bool IsDesignMode
		{
			get { return DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime; }
		}

		private void SettingsControl_Load(object sender, EventArgs e)
		{
			if (IsDesignMode)
				return;
			var data = SettingsManager.Current.Acronyms;
			SettingsControl.Data = data;
			//data.Items.ListChanged += Items_ListChanged;
			ApplyFilter();
			SettingsControl.SettingsDataGridView.CellValidating += SettingsDataGridView_CellValidating;
			SettingsControl.FilterChanged += SettingsControl_FilterChanged;
			SettingsControl.SettingsDataGridView.ContextMenuStrip = GridContextMenuStrip;
		}

		private void Items_ListChanged(object sender, ListChangedEventArgs e)
		{
			//ApplyFilter();
		}

		void ApplyFilter()
		{
			var text = SettingsControl.FilterTextBox.Text;
			var items = SettingsManager.Current.Acronyms.Items;
			var newList = items.Where(x => x.IsMatch(text)).ToArray();
			var filteredItems = new ClassLibrary.ComponentModel.SortableBindingList<Acronym>(newList);
			var changed = newList.Count() != items.Count();
			SettingsControl.UpdateOnly = changed;

			var data = changed ? filteredItems : items;
			data.SynchronizingObject = SettingsControl.DataGridView;
			SettingsControl.DataGridView.DataSource = data;
		}

		private void SettingsControl_FilterChanged(object sender, EventArgs e)
		{
			ApplyFilter();
		}

		private void SettingsDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			var grid = (ClassLibrary.Controls.VirtualDataGridView)sender;
			//var grid = SettingsControl.DataGridView;
			var row = grid.Rows[e.RowIndex];
			row.ErrorText = "";
			// Don't try to validate the 'new row' until finished  
			// editing since there is no point in validating its initial values. 
			if (row.IsNewRow) { return; }
			var list = (IBindingList)grid.DataSource;
			if (e.RowIndex >= list.Count)
				return;
			var item = (Acronym)list[e.RowIndex]; ;
			var column = grid.Columns[e.ColumnIndex];
			string error = null;
			var newValue = e.FormattedValue.ToString().Trim().ToLower();
			newValue = string.IsNullOrEmpty(newValue) ? null : newValue;
			// If Group Name changed then...
			if (item.IsEmpty && string.IsNullOrEmpty(newValue))
			{
				// Use begin invoke or removal will fail
				BeginInvoke((MethodInvoker)delegate ()
				{
					SettingsControl.DataGridView.RemoveItems(item);
				});
				return;
			}
			else if (e.ColumnIndex == KeyColumn.Index && string.Compare(newValue, item.Key, true) != 0)
			{
				if (string.IsNullOrEmpty(newValue))
				{
					error = "Key field must be not empty!";
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

		void CheckSelected(bool check)
		{
			var items = SettingsControl.GetSelectedItems().Cast<Acronym>().ToArray();
			foreach (var item in items)
			{
				item.Enabled = check;
			}
		}

		private void CheckSelectedMenuItem_Click(object sender, EventArgs e)
		{
			CheckSelected(true);
		}

		private void UncheckSelectedMenuItem_Click(object sender, EventArgs e)
		{
			CheckSelected(false);
		}

		private void SelectAllMenuItem_Click(object sender, EventArgs e)
		{
			SettingsControl.SettingsDataGridView.SelectAll();
		}

		private void DelectAllMenuItem_Click(object sender, EventArgs e)
		{
			SettingsControl.SettingsDataGridView.ClearSelection();
		}
	}
}
