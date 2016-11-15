using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JocysCom.TextToSpeech.Monitor.Network;
using JocysCom.ClassLibrary.ComponentModel;
using JocysCom.ClassLibrary.Runtime;
using JocysCom.ClassLibrary.Controls;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
    public partial class AcronymsUserControl : UserControl
    {
        public AcronymsUserControl()
        {
            InitializeComponent();
            AcronymsDataGridView.AutoGenerateColumns = false;
            AcronymsDataGridView.DataSource = SettingsManager.Current.Acronyms;
        }

        private void AcronymsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var grid = (DataGridView)sender;
            if (e.ColumnIndex == grid.Columns[EnabledColumn.Name].Index)
            {
                var snd = (Acronym)grid.Rows[e.RowIndex].DataBoundItem;
                snd.Enabled = !snd.Enabled;
                AcronymsDataGridView.Invalidate();
            }
            if (e.ColumnIndex == grid.Columns[GroupColumn.Name].Index) AcronymsDataGridView.BeginEdit(true);
            //if (e.ColumnIndex == grid.Columns[FileColumn.Name].Index) AcronymsDataGridView.BeginEdit(true);
        }

        public void SelectRow(string group)
        {
            if (string.IsNullOrEmpty(group))
            {
                AcronymsDataGridView.ClearSelection();
                return;
            }
            foreach (DataGridViewRow row in AcronymsDataGridView.Rows)
            {
                var item = (Acronym)row.DataBoundItem;
                if (item.Group.ToLower() == group.ToLower() && !row.Selected)
                {
                    AcronymsDataGridView.ClearSelection();
                    row.Selected = true;
                    AcronymsDataGridView.FirstDisplayedCell = row.Cells[0];
                    break;
                }
            }
        }

        private void AcronymsAddButton_Click(object sender, EventArgs e)
        {
        }

        private void AcronymsImportButton_Click(object sender, EventArgs e)
        {
            var dialog = AcronymsImportOpenFileDialog;
            dialog.DefaultExt = "*.xml";
            dialog.Filter = "Settings (*.xml;*.xml.gz)|*.xml;*.gz|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
			dialog.RestoreDirectory = true;
			var fi = SettingsManager.Current.Acronyms.XmlFile;
			if (string.IsNullOrEmpty(dialog.FileName)) dialog.FileName = System.IO.Path.GetFileNameWithoutExtension(fi.Name);
            if (string.IsNullOrEmpty(dialog.InitialDirectory)) dialog.InitialDirectory = fi.Directory.FullName;
            dialog.Title = "Import Settings File";
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<Acronym> items;
                if (dialog.FileName.EndsWith(".gz"))
                {
                    var compressedBytes = System.IO.File.ReadAllBytes(dialog.FileName);
                    var bytes = MainHelper.Decompress(compressedBytes);
                    var xml = Encoding.UTF8.GetString(bytes);
                    items = Serializer.DeserializeFromXmlString<List<Acronym>>(xml, System.Text.Encoding.UTF8);
                }
                else
                {
                    items = Serializer.DeserializeFromXmlFile<List<Acronym>>(dialog.FileName);
                }
                foreach (var item in items)
                {
                    var oldItem = SettingsManager.Current.Acronyms.Items.FirstOrDefault(x => x.Group == item.Group);
                    // If old item was not found then...
                    if (oldItem == null)
                    {
                        // Add as new.
                        SettingsManager.Current.Acronyms.Items.Add(item);
                    }
                    else
                    {
                        // Udate old item.
                        oldItem.Group = item.Group;
                    }
                }
            }
        }

        private void AcronymsExportButton_Click(object sender, EventArgs e)
        {
            var dialog = AcronymsExportSaveFileDialog;
            dialog.DefaultExt = "*.xml";
            dialog.Filter = "Settings (*.xml)|*.xml|Compressed Settings (*.xml.gz)|*.xml.gz|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            if (string.IsNullOrEmpty(dialog.FileName)) dialog.FileName = "Settings.Acronyms";
            if (string.IsNullOrEmpty(dialog.InitialDirectory)) dialog.InitialDirectory = SettingsManager.Current.Acronyms.XmlFile.Directory.FullName;
            dialog.Title = "Export Settings (Acronyms) File";
            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var programs = SettingsManager.Current.Acronyms.Items.ToList();
                if (dialog.FileName.EndsWith(".gz"))
                {
                    var s = Serializer.SerializeToXmlString(programs, System.Text.Encoding.UTF8);
                    var bytes = System.Text.Encoding.UTF8.GetBytes(s);
                    var compressedBytes = MainHelper.Compress(bytes);
                    System.IO.File.WriteAllBytes(dialog.FileName, compressedBytes);
                }
                else
                {
                    Serializer.SerializeToXmlFile(programs, dialog.FileName, System.Text.Encoding.UTF8, true);
                }
            }
        }

        private void AcronymsDeleteButton_Click(object sender, EventArgs e)
        {
            var grid = AcronymsDataGridView;
            var selection = ControlsHelper.GetSelection<string>(grid, "group");
            var itemsToDelete = SettingsManager.Current.Acronyms.Items.Where(x => selection.Contains(x.Group)).ToArray();
            MessageBoxForm form = new MessageBoxForm();
            form.StartPosition = FormStartPosition.CenterParent;
            string acronym;
            if (itemsToDelete.Length == 1)
            {
                var item = itemsToDelete[0];
                acronym = string.Format("Are you sure you want to delete settings for?\r\n\r\nGroup: {0}",
                    item.Group);
            }
            else
            {
                acronym = string.Format("Delete {0} setting(s)?", itemsToDelete.Length);
            }
            var result = form.ShowForm(acronym, "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.OK)
            {
                foreach (var item in itemsToDelete)
                {
                    SettingsManager.Current.Acronyms.Items.Remove(item);
                }
                SettingsManager.Current.Save();
            }
        }

        private void AcronymsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == -1) return;
            var grid = (DataGridView)sender;
            var row = grid.Rows[e.RowIndex];
            var snd = (Acronym)row.DataBoundItem;
            // If this is not row header then...
            if (e.ColumnIndex > -1)
            {
                var column = AcronymsDataGridView.Columns[e.ColumnIndex];
            }
            e.CellStyle.ForeColor = snd.Enabled
                ? AcronymsDataGridView.DefaultCellStyle.ForeColor
                : System.Drawing.SystemColors.ControlDark;
            e.CellStyle.SelectionBackColor = snd.Enabled
             ? AcronymsDataGridView.DefaultCellStyle.SelectionBackColor
             : System.Drawing.SystemColors.ControlDark;
            row.HeaderCell.Style.SelectionBackColor = snd.Enabled
             ? AcronymsDataGridView.DefaultCellStyle.SelectionBackColor
             : System.Drawing.SystemColors.ControlDark;

        }

        private void AcronymsDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var grid = (DataGridView)sender;
            var row = grid.Rows[e.RowIndex];
            row.ErrorText = "";
            // Don't try to validate the 'new row' until finished  
            // editing since there is no point in validating its initial values. 
            if (row.IsNewRow) { return; }
            var snd = (Acronym)row.DataBoundItem;
            var column = grid.Columns[e.ColumnIndex];
            string error = null;
            var value = e.FormattedValue.ToString();
            // If Group Name changed then...
            if (e.ColumnIndex == grid.Columns[GroupColumn.Name].Index && value.ToLower() != snd.Group.ToLower())
            {
                var groups = SettingsManager.Current.Acronyms.Items.Select(x => x.Group.ToLower()).ToArray();
                if (string.IsNullOrEmpty(value) || groups.Contains(value.ToLower()))
                {
                    error = "Group field value must be unique and not empty!";
                }
            }
            // If Audio File then...
            //else if (e.ColumnIndex == grid.Columns[FileColumn.Name].Index && !string.IsNullOrEmpty(value))
            //{
            //    if (string.IsNullOrEmpty(value))
            //    {
            //        error = "File path value must be not empty!";
            //    }
            //}
            if (!string.IsNullOrEmpty(error))
            {
                e.Cancel = true;
                row.ErrorText = error;
            }
        }

        private void AcronymsDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Clear the row error in case the user presses ESC.   
            var grid = (DataGridView)sender;
            grid.Rows[e.RowIndex].ErrorText = String.Empty;
        }

        //Tooltip MouseHover.
        private void Acronyms_MouseHover(object sender, EventArgs e)
        {
			Program.TopForm.MainHelpLabel.Text = "Here you can add groups ( like \"Whisper\" ) and assign acronyms to them ( like \"Radio\" -- listed in \"Default Intro Acronym\" drop-down box ) or set paths to wav files ( like \"C:\\Windows\\Media\\notify.wav\" ).";
        }

        // Tooltip Main.
        private void Acronyms_MouseLeave(object sender, EventArgs e)
        {
			Program.TopForm.ResetHelpToDefault();
        }

        private void AcronymsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

		private void BrowseButton_Click(object sender, EventArgs e)
		{
			MainHelper.OpenUrl(SettingsManager.Current.Acronyms.XmlFile.Directory.FullName);
		}

		private void ResetButton_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Do you want to reset Intro Acronyms to default?", "Reset Intro Acronyms", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (result == DialogResult.Yes)
			{
				var success = SettingsManager.Current.Acronyms.ResetToDefault();
				if (success) SettingsManager.Current.Acronyms.Save();
			}
		}
	}
}
