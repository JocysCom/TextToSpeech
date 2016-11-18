﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
			if (column.ReadOnly)
			{
				return;
			}
			if (column is DataGridViewCheckBoxColumn)
			{
				var cell = (DataGridViewCheckBoxCell)grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
				cell.Value = !(bool)cell.FormattedValue;
				cell.Value = !(bool)cell.FormattedValue;
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

		object[] GetSelectedActions()
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
			var type = list.GetType().GenericTypeArguments[0];
			var selectedActions = GetSelectedActions();
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
			//var dialog = SettingsExportSaveFileDialog;
			//dialog.DefaultExt = "*.xml";
			//dialog.Filter = "Settings (*.xml)|*.xml|Compressed Settings (*.xml.gz)|*.xml.gz|All files (*.*)|*.*";
			//dialog.FilterIndex = 1;
			//dialog.RestoreDirectory = true;
			//if (string.IsNullOrEmpty(dialog.FileName)) dialog.FileName = "Settings.Settings";
			//if (string.IsNullOrEmpty(dialog.InitialDirectory)) dialog.InitialDirectory = SettingsManager.Current.Settings.XmlFile.Directory.FullName;
			//dialog.Title = "Export Settings (Settings) File";
			//var result = dialog.ShowDialog();
			//if (result == System.Windows.Forms.DialogResult.OK)
			//{
			//    var programs = SettingsManager.Current.Settings.Items.ToList();
			//    if (dialog.FileName.EndsWith(".gz"))
			//    {
			//        var s = Serializer.SerializeToXmlString(programs, System.Text.Encoding.UTF8);
			//        var bytes = System.Text.Encoding.UTF8.GetBytes(s);
			//        var compressedBytes = MainHelper.Compress(bytes);
			//        System.IO.File.WriteAllBytes(dialog.FileName, compressedBytes);
			//    }
			//    else
			//    {
			//        Serializer.SerializeToXmlFile(programs, dialog.FileName, System.Text.Encoding.UTF8, true);
			//    }
			//}
		}

		private void SettingsDeleteButton_Click(object sender, EventArgs e)
		{
			//var grid = SettingsDataGridView;
			//var selection = ControlsHelper.GetSelection<string>(grid, "group");
			//var itemsToDelete = SettingsManager.Current.Settings.Items.Where(x => selection.Contains(x.Group)).ToArray();
			//MessageBoxForm form = new MessageBoxForm();
			//form.StartPosition = FormStartPosition.CenterParent;
			//string setting;
			//if (itemsToDelete.Length == 1)
			//{
			//    var item = itemsToDelete[0];
			//    setting = string.Format("Are you sure you want to delete settings for?\r\n\r\nGroup: {0}",
			//        item.Group);
			//}
			//else
			//{
			//    setting = string.Format("Delete {0} setting(s)?", itemsToDelete.Length);
			//}
			//var result = form.ShowForm(setting, "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
			//if (result == DialogResult.OK)
			//{
			//    foreach (var item in itemsToDelete)
			//    {
			//        SettingsManager.Current.Settings.Items.Remove(item);
			//    }
			//    SettingsManager.Current.Save();
			//}
		}

		private void SettingsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			//    if (e.RowIndex == -1) return;
			//    var grid = (DataGridView)sender;
			//    var row = grid.Rows[e.RowIndex];
			//    var snd = (Setting)row.DataBoundItem;
			//    // If this is not row header then...
			//    if (e.ColumnIndex > -1)
			//    {
			//        var column = SettingsDataGridView.Columns[e.ColumnIndex];
			//    }
			//    e.CellStyle.ForeColor = snd.Enabled
			//        ? SettingsDataGridView.DefaultCellStyle.ForeColor
			//        : System.Drawing.SystemColors.ControlDark;
			//    e.CellStyle.SelectionBackColor = snd.Enabled
			//     ? SettingsDataGridView.DefaultCellStyle.SelectionBackColor
			//     : System.Drawing.SystemColors.ControlDark;
			//    row.HeaderCell.Style.SelectionBackColor = snd.Enabled
			//     ? SettingsDataGridView.DefaultCellStyle.SelectionBackColor
			//     : System.Drawing.SystemColors.ControlDark;

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

		private void SettingsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void BrowseButton_Click(object sender, EventArgs e)
		{
			//MainHelper.OpenUrl(SettingsManager.Current.Settings.XmlFile.Directory.FullName);
		}

		private void ResetButton_Click(object sender, EventArgs e)
		{
			//var result = MessageBox.Show("Do you want to reset Intro Settings to default?", "Reset Intro Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			//if (result == DialogResult.Yes)
			//{
			//	var success = SettingsManager.Current.Settings.ResetToDefault();
			//	if (success) SettingsManager.Current.Settings.Save();
			//}
		}
	}
}