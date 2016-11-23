using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JocysCom.ClassLibrary.Controls;
using JocysCom.ClassLibrary.Runtime;

namespace JocysCom.ClassLibrary.Configuration
{

	public partial class SettingsUserControl : UserControl, IDataGridView
	{
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DataGridView DataGridView
		{
			get { return SettingsDataGridView; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(ExtendedDataGridViewColumnCollectionEditor), typeof(UITypeEditor))]
		[MergableProperty(false)]
		public DataGridViewColumnCollection DataGridViewColumns
		{
			get { return SettingsDataGridView.Columns; }
		}

		public ISettingsData Data
		{
			get { return _Data; }
			set { _Data = value; }
		}
		ISettingsData _Data;

		public SettingsUserControl()
		{
			InitializeComponent();
			SettingsDataGridView.AutoGenerateColumns = false;
		}

		private void SettingsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;
			var grid = (DataGridView)sender;
			var column = grid.Columns[e.ColumnIndex];
			if (column is DataGridViewCheckBoxColumn)
			{
				var cell = (DataGridViewCheckBoxCell)grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
				cell.Value = !(bool)cell.FormattedValue;
			}
			else if (column.ReadOnly)
			{
				return;
			}
			else
			{
				grid.BeginEdit(true);
			}
		}

		public void SelectRow(string group)
		{
			if (string.IsNullOrEmpty(group))
			{
				SettingsDataGridView.ClearSelection();
				return;
			}
			foreach (DataGridViewRow row in SettingsDataGridView.Rows)
			{
				//var item = (Setting)row.DataBoundItem;
				//if (item.Group.ToLower() == group.ToLower() && !row.Selected)
				//{
				//    SettingsDataGridView.ClearSelection();
				//    row.Selected = true;
				//    SettingsDataGridView.FirstDisplayedCell = row.Cells[0];
				//    break;
				//}
			}
		}

		object[] GetSelectedItems()
		{
			var list = (IList)SettingsDataGridView.DataSource;
			var selectedActions = SettingsDataGridView
			.SelectedRows
			.Cast<DataGridViewRow>()
			.Select(x => (object)x.DataBoundItem)
			// Make sure that selected actions are ordered like in the list.
			.OrderBy(x => list.IndexOf(x))
			.ToArray();
			return selectedActions;
		}

		private void SettingsAddButton_Click(object sender, EventArgs e)
		{
			var list = (IList)SettingsDataGridView.DataSource;
			var type = list.GetType().GetGenericArguments()[0];
			var selectedActions = GetSelectedItems();
			var last = selectedActions.LastOrDefault();
			var insertIndex = (last == null)
					? -1 : list.IndexOf(last);
			var action = Activator.CreateInstance(type);
			// If there are no records or last row is selected then...
			if (insertIndex == -1 || insertIndex == (list.Count - 1))
			{
				list.Add(action);
			}
			else
			{
				list.Insert(insertIndex + 1, action);
			}
			// Select new created item.
			foreach (DataGridViewRow row in SettingsDataGridView.Rows)
			{
				var selected = (action == row.DataBoundItem);
				if (row.Selected != selected)
				{
					row.Selected = selected;
				}
			}
		}

		private void SettingsImportButton_Click(object sender, EventArgs e)
		{
			//         var dialog = SettingsImportOpenFileDialog;
			//         dialog.DefaultExt = "*.xml";
			//         dialog.Filter = "Settings (*.xml;*.xml.gz)|*.xml;*.gz|All files (*.*)|*.*";
			//         dialog.FilterIndex = 1;
			//dialog.RestoreDirectory = true;
			//var fi = SettingsManager.Current.Settings.XmlFile;
			//if (string.IsNullOrEmpty(dialog.FileName)) dialog.FileName = System.IO.Path.GetFileNameWithoutExtension(fi.Name);
			//         if (string.IsNullOrEmpty(dialog.InitialDirectory)) dialog.InitialDirectory = fi.Directory.FullName;
			//         dialog.Title = "Import Settings File";
			//         var result = dialog.ShowDialog();
			//         if (result == DialogResult.OK)
			//         {
			//             List<Setting> items;
			//             if (dialog.FileName.EndsWith(".gz"))
			//             {
			//                 var compressedBytes = System.IO.File.ReadAllBytes(dialog.FileName);
			//                 var bytes = MainHelper.Decompress(compressedBytes);
			//                 var xml = Encoding.UTF8.GetString(bytes);
			//                 items = Serializer.DeserializeFromXmlString<List<Setting>>(xml, System.Text.Encoding.UTF8);
			//             }
			//             else
			//             {
			//                 items = Serializer.DeserializeFromXmlFile<List<Setting>>(dialog.FileName);
			//             }
			//             foreach (var item in items)
			//             {
			//                 var oldItem = SettingsManager.Current.Settings.Items.FirstOrDefault(x => x.Group == item.Group);
			//                 // If old item was not found then...
			//                 if (oldItem == null)
			//                 {
			//                     // Add as new.
			//                     SettingsManager.Current.Settings.Items.Add(item);
			//                 }
			//                 else
			//                 {
			//                     // Udate old item.
			//                     oldItem.Group = item.Group;
			//                 }
			//             }
			//         }
		}

		private void SettingsExportButton_Click(object sender, EventArgs e)
		{
			var dialog = SettingsExportSaveFileDialog;
			dialog.DefaultExt = "*.xml";
			dialog.Filter = "Settings (*.xml)|*.xml|Compressed Settings (*.xml.gz)|*.xml.gz|All files (*.*)|*.*";
			dialog.FilterIndex = 1;
			dialog.RestoreDirectory = true;
			if (string.IsNullOrEmpty(dialog.FileName)) dialog.FileName = "Settings.Settings";
			if (string.IsNullOrEmpty(dialog.InitialDirectory)) dialog.InitialDirectory = Data.XmlFile.Directory.FullName;
			dialog.Title = "Export Settings File";
			var result = dialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				Data.SaveAs(dialog.FileName);
			}
		}

		private void SettingsDeleteButton_Click(object sender, EventArgs e)
		{
			var items = GetSelectedItems();
			if (items.Length == 0)
			{
				return;
			}
			var message = string.Format("Are you sure you want to delete {0} item{1}?",
					items.Length, items.Length == 1 ? "" : "s");
			MessageBoxForm form = new MessageBoxForm();
			form.StartPosition = FormStartPosition.CenterParent;
			var result = form.ShowForm(message, "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
			if (result == DialogResult.OK)
			{
				foreach (var item in items)
				{
					Data.Remove(item);
				}
				Data.Save();
			}
		}

		private void SettingsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex == -1) return;
			var grid = (DataGridView)sender;
			var row = grid.Rows[e.RowIndex];
			var enabled = ((ISettingsItem)row.DataBoundItem).Enabled;
			// If this is not row header then...
			if (e.ColumnIndex > -1)
			{
				var column = grid.Columns[e.ColumnIndex];
			}
			e.CellStyle.ForeColor = enabled
				? grid.DefaultCellStyle.ForeColor
				: System.Drawing.SystemColors.ControlDark;
			e.CellStyle.SelectionBackColor = enabled
				? grid.DefaultCellStyle.SelectionBackColor
				: System.Drawing.SystemColors.ControlDark;
			row.HeaderCell.Style.SelectionBackColor = enabled
				? grid.DefaultCellStyle.SelectionBackColor
				: System.Drawing.SystemColors.ControlDark;
		}

		private void SettingsDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			//var grid = (DataGridView)sender;
			//var row = grid.Rows[e.RowIndex];
			//row.ErrorText = "";
			//// Don't try to validate the 'new row' until finished  
			//// editing since there is no point in validating its initial values. 
			//if (row.IsNewRow) { return; }
			//var snd = (Setting)row.DataBoundItem;
			//var column = grid.Columns[e.ColumnIndex];
			//string error = null;
			//var value = e.FormattedValue.ToString();
			//// If Group Name changed then...
			//if (e.ColumnIndex == grid.Columns[GroupColumn.Name].Index && value.ToLower() != snd.Group.ToLower())
			//{
			//    var groups = SettingsManager.Current.Settings.Items.Select(x => x.Group.ToLower()).ToArray();
			//    if (string.IsNullOrEmpty(value) || groups.Contains(value.ToLower()))
			//    {
			//        error = "Group field value must be unique and not empty!";
			//    }
			//}
			//if (!string.IsNullOrEmpty(error))
			//{
			//    e.Cancel = true;
			//    row.ErrorText = error;
			//}
		}

		private void SettingsDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			// Clear the row error in case the user presses ESC.   
			var grid = (DataGridView)sender;
			grid.Rows[e.RowIndex].ErrorText = String.Empty;
		}

		//Tooltip MouseHover.
		private void Settings_MouseHover(object sender, EventArgs e)
		{
			//Program.TopForm.MainHelpLabel.Text = "Here you can add groups ( like \"Whisper\" ) and assign settings to them ( like \"Radio\" -- listed in \"Default Intro Setting\" drop-down box ) or set paths to wav files ( like \"C:\\Windows\\Media\\notify.wav\" ).";
		}

		// Tooltip Main.
		private void Settings_MouseLeave(object sender, EventArgs e)
		{
			//Program.TopForm.ResetHelpToDefault();
		}

		private void BrowseButton_Click(object sender, EventArgs e)
		{
			OpenUrl(Data.XmlFile.Directory.FullName);
		}

		private void ResetButton_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Do you want to reset Intro Settings to default?", "Reset Intro Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (result == DialogResult.Yes)
			{
				var success = Data.ResetToDefault();
				if (success) Data.Save();
			}
		}

		#region HelperFunctions

		public static void OpenUrl(string url)
		{
			try
			{
				System.Diagnostics.Process.Start(url);
			}
			catch (System.ComponentModel.Win32Exception noBrowser)
			{
				if (noBrowser.ErrorCode == -2147467259)
					MessageBox.Show(noBrowser.Message);
			}
			catch (System.Exception other)
			{
				MessageBox.Show(other.Message);
			}
		}

		#endregion
	}
}
