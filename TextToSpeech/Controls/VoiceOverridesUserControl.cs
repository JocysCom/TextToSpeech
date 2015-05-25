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
    public partial class VoicesOverridesUserControl : UserControl
    {
        public VoicesOverridesUserControl()
        {
            InitializeComponent();
            VoicesOverridesDataGridView.AutoGenerateColumns = false;
            VoicesOverridesDataGridView.DataSource = SettingsFile.Current.Overrides;
        }

        private void VoicesGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var grid = (DataGridView)sender;
            if (e.ColumnIndex == grid.Columns[EnabledColumn.Name].Index)
            {
                var msg = (message)grid.Rows[e.RowIndex].DataBoundItem;
                msg.enabled = !msg.enabled;
                VoicesOverridesDataGridView.Invalidate();
            }
            if (e.ColumnIndex == grid.Columns[NameColumn.Name].Index) VoicesOverridesDataGridView.BeginEdit(true);
            if (e.ColumnIndex == grid.Columns[GenderColumn.Name].Index) VoicesOverridesDataGridView.BeginEdit(true);
            if (e.ColumnIndex == grid.Columns[EffectColumn.Name].Index) VoicesOverridesDataGridView.BeginEdit(true);
            if (e.ColumnIndex == grid.Columns[PitchColumn.Name].Index) VoicesOverridesDataGridView.BeginEdit(true);
            if (e.ColumnIndex == grid.Columns[RateColumn.Name].Index) VoicesOverridesDataGridView.BeginEdit(true);
            if (e.ColumnIndex == grid.Columns[VolumeColumn.Name].Index) VoicesOverridesDataGridView.BeginEdit(true);
            if (e.ColumnIndex == grid.Columns[LanguageColumn.Name].Index) VoicesOverridesDataGridView.BeginEdit(true);
        }

        public void UpsertRecord(message v)
        {
            // Try to find existing override record from the list.
            var msg = SettingsFile.Current.Overrides.FirstOrDefault(x => x.name.ToLower() == v.name.ToLower());
            if (msg == null)
            {
               msg = new message();
               msg.name = v.name;
               SettingsFile.Current.Overrides.Add(msg);
            }
            msg.OverrideFrom(v);
        }

        public void AddNewRecord()
        {
            var grid = VoicesOverridesDataGridView;
            int i = 0;
            string name;
            // Get all names in lowercase.
            var names = SettingsFile.Current.Overrides.Select(x => x.name.ToLower()).ToArray();
            // Loop until free name is found.
            while (true)
            {
                i++;
                name = string.Format("Name{0}", i);
                // If name is unique and not in the list then break loop.
                if (!names.Contains(name.ToLower())) break;
            }
            var msg = new message();
            msg.name = name;
            SettingsFile.Current.Overrides.Add(msg);
            grid.BeginEdit(true);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddNewRecord();
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            var dialog = ImportOpenFileDialog;
            dialog.DefaultExt = "*.xml";
            dialog.Filter = "Game Settings (*.xml;*.xml.gz)|*.xml;*.gz|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            if (string.IsNullOrEmpty(dialog.FileName)) dialog.FileName = "Settings.Overrides";
            if (string.IsNullOrEmpty(dialog.InitialDirectory)) dialog.InitialDirectory = SettingsFile.Current.FolderPath;
            dialog.Title = "Import Settings (Overrides) File";
            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                List<message> items;
                if (dialog.FileName.EndsWith(".gz"))
                {
                    var compressedBytes = System.IO.File.ReadAllBytes(dialog.FileName);
                    var bytes = MainHelper.Decompress(compressedBytes);
                    var xml = System.Text.Encoding.UTF8.GetString(bytes);
                    items = Serializer.DeserializeFromXmlString<List<message>>(xml, System.Text.Encoding.UTF8);
                }
                else
                {
                    items = Serializer.DeserializeFromXmlFile<List<message>>(dialog.FileName);
                }
                foreach (var item in items)
                {
                    var oldItem = SettingsFile.Current.Overrides.FirstOrDefault(x => x.name == item.name);
                    // If old item was not found then...
                    if (oldItem == null)
                    {
                        // Add as new.
                        SettingsFile.Current.Overrides.Add(item);
                    }
                    else
                    {
                        // Udate old item.
                        oldItem.language = item.language;
                        oldItem.effect = item.effect;
                        oldItem.gender = item.gender;
                        oldItem.pitch = item.pitch;
                        oldItem.rate = item.rate;
                        oldItem.volume = item.volume;
                    }
                }
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            var dialog = ExportSaveFileDialog;
            dialog.DefaultExt = "*.xml";
            dialog.Filter = "Settings (*.xml)|*.xml|Compressed Settings (*.xml.gz)|*.xml.gz|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            if (string.IsNullOrEmpty(dialog.FileName)) dialog.FileName = "Settings.Overrides";
            if (string.IsNullOrEmpty(dialog.InitialDirectory)) dialog.InitialDirectory = SettingsFile.Current.FolderPath;
            dialog.Title = "Export Settings (Overrides) File";
            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var programs = SettingsFile.Current.Overrides.ToList();
                if (dialog.FileName.EndsWith(".gz"))
                {
                    var s = Serializer.SerializeToXmlString(programs, System.Text.Encoding.UTF8);
                    var bytes = System.Text.Encoding.UTF8.GetBytes(s);
                    var compressedBytes = MainHelper.Compress(bytes);
                    System.IO.File.WriteAllBytes(dialog.FileName, compressedBytes);
                }
                else
                {
                    Serializer.SerializeToXmlFile(programs, dialog.FileName, System.Text.Encoding.UTF8);
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var grid = VoicesOverridesDataGridView;
            var selection = ControlsHelper.GetSelection<string>(grid, "name");
            var itemsToDelete = SettingsFile.Current.Overrides.Where(x => selection.Contains(x.name)).ToArray();
            MessageBoxForm form = new MessageBoxForm();
            form.StartPosition = FormStartPosition.CenterParent;
            string message;
            if (itemsToDelete.Length == 1)
            {
                var item = itemsToDelete[0];
                message = string.Format("Are you sure you want to delete settings for?\r\n\r\nName: {0}",
                    item.name);
            }
            else
            {
                message = string.Format("Delete {0} setting(s)?", itemsToDelete.Length);
            }
            var result = form.ShowForm(message, "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.OK)
            {
                foreach (var item in itemsToDelete)
                {
                    SettingsFile.Current.Overrides.Remove(item);
                }
                SettingsFile.Current.Save();
            }
        }

        private void VoicesDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == -1) return;
            var grid = (DataGridView)sender;
            var row = grid.Rows[e.RowIndex];
            var msg = (message)row.DataBoundItem;
            // If this is not row header then...
            if (e.ColumnIndex > -1)
            {
                var column = VoicesOverridesDataGridView.Columns[e.ColumnIndex];
            }
            e.CellStyle.ForeColor = msg.enabled
                ? VoicesOverridesDataGridView.DefaultCellStyle.ForeColor
                : System.Drawing.SystemColors.ControlDark;
            e.CellStyle.SelectionBackColor = msg.enabled
             ? VoicesOverridesDataGridView.DefaultCellStyle.SelectionBackColor
             : System.Drawing.SystemColors.ControlDark;
            row.HeaderCell.Style.SelectionBackColor = msg.enabled
             ? VoicesOverridesDataGridView.DefaultCellStyle.SelectionBackColor
             : System.Drawing.SystemColors.ControlDark;

        }

        private void VoicesDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var grid = (DataGridView)sender;
            var row = grid.Rows[e.RowIndex];
            row.ErrorText = "";
            // Don't try to validate the 'new row' until finished  
            // editing since there is no point in validating its initial values. 
            if (row.IsNewRow) { return; }
            var msg = (message)row.DataBoundItem;
            var column = grid.Columns[e.ColumnIndex];
            string error = null;
            bool success;
            var value = e.FormattedValue.ToString();
            // If name changed then...
            if (e.ColumnIndex == grid.Columns[NameColumn.Name].Index && value.ToLower() != msg.name.ToLower())
            {
                var names = SettingsFile.Current.Overrides.Select(x => x.name.ToLower()).ToArray();
                if (string.IsNullOrEmpty(value) || names.Contains(value.ToLower()))
                {
                    error = "Name must be unique and not empty!";
                }
            }
            else if (e.ColumnIndex == grid.Columns[PitchColumn.Name].Index && !string.IsNullOrEmpty(value))
            {
                int pitch;
                success = int.TryParse(value, out pitch);
                if (!success || pitch < -10 || pitch > 10) error = "Pitch must be number between -10 and 10!";
            }
            else if (e.ColumnIndex == grid.Columns[RateColumn.Name].Index && !string.IsNullOrEmpty(value))
            {
                int rate;
                success = int.TryParse(value, out rate);
                if (!success || rate < -10 || rate > 10) error = "Rate must be number between -10 and 10!";
            }
            else if (e.ColumnIndex == grid.Columns[VolumeColumn.Name].Index && !string.IsNullOrEmpty(value))
            {
                int volume;
                success = int.TryParse(value, out volume);
                if (!success || volume < 0 || volume > 100) error = "Volume must be number between 0 and 100!";
            }
            else if (e.ColumnIndex == grid.Columns[GenderColumn.Name].Index && !string.IsNullOrEmpty(value))
            {
                System.Speech.Synthesis.VoiceGender gender;
                success = Enum.TryParse<System.Speech.Synthesis.VoiceGender>(value, out gender);
                if (!success) error = "Gender must be: Male, Female or Neutral!";
            }
            if (!string.IsNullOrEmpty(error))
            {
                e.Cancel = true;
                row.ErrorText = error;

            }
        }

        private void VoicesDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Clear the row error in case the user presses ESC.   
            var grid = (DataGridView)sender;
            grid.Rows[e.RowIndex].ErrorText = String.Empty;
        }

        //Tooltip MouseHover.
        private void VoicesOverrides_MouseHover(object sender, EventArgs e)
        {
            var form = (MainForm)ParentForm;
            form.MainHelpLabel.Text = "Here you can add names and assign values to them. Values in this list have priority over incoming ( submited ) values.";
        }

        // Tooltip Main.
        private void VoicesOverrides_MouseLeave(object sender, EventArgs e)
        {
            var form = (MainForm)ParentForm;
            form.MainHelpLabel.Text = "Please download this tool only from trustworthy sources. Make sure that this tool is always signed by verified publisher ( Jocys.com ) with signature issue by trusted certificate authority.";
        }


    }
}
