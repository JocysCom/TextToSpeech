using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JocysCom.TextToSpeech.Monitor.Capturing;
using JocysCom.ClassLibrary.ComponentModel;
using JocysCom.ClassLibrary.Runtime;
using JocysCom.ClassLibrary.Controls;
using JocysCom.ClassLibrary.Configuration;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
	public partial class VoicesDefaultsUserControl : UserControl
	{
		public VoicesDefaultsUserControl()
		{
			InitializeComponent();
			VoicesDefaultsDataGridView.AutoGenerateColumns = false;
			VoicesDefaultsDataGridView.DataSource = SettingsFile.Current.Defaults;
		}

		private void VoicesGridView_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;
			var grid = (DataGridView)sender;
			if (e.ColumnIndex == grid.Columns[EnabledColumn.Name].Index)
			{
				var msg = (message)grid.Rows[e.RowIndex].DataBoundItem;
				msg.enabled = !msg.enabled;
				VoicesDefaultsDataGridView.Invalidate();
			}
			if (e.ColumnIndex == grid.Columns[NameColumn.Name].Index) VoicesDefaultsDataGridView.BeginEdit(true);
			if (e.ColumnIndex == grid.Columns[GenderColumn.Name].Index) VoicesDefaultsDataGridView.BeginEdit(true);
			if (e.ColumnIndex == grid.Columns[EffectColumn.Name].Index) VoicesDefaultsDataGridView.BeginEdit(true);
			if (e.ColumnIndex == grid.Columns[PitchColumn.Name].Index) VoicesDefaultsDataGridView.BeginEdit(true);
			if (e.ColumnIndex == grid.Columns[RateColumn.Name].Index) VoicesDefaultsDataGridView.BeginEdit(true);
			if (e.ColumnIndex == grid.Columns[VolumeColumn.Name].Index) VoicesDefaultsDataGridView.BeginEdit(true);
			if (e.ColumnIndex == grid.Columns[LanguageColumn.Name].Index) VoicesDefaultsDataGridView.BeginEdit(true);
            if (e.ColumnIndex == grid.Columns[VoiceColumn.Name].Index) VoicesDefaultsDataGridView.BeginEdit(true);
        }
		public void AddNewRecord()
		{
			var grid = VoicesDefaultsDataGridView;
			int i = 0;
			string name;
			// Get all names in lowercase.
			var names = SettingsFile.Current.Defaults.Select(x => x.name.ToLower()).ToArray();
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
			SettingsFile.Current.Defaults.Add(msg);
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
			if (string.IsNullOrEmpty(dialog.FileName)) dialog.FileName = "Settings.Defaults";
			if (string.IsNullOrEmpty(dialog.InitialDirectory)) dialog.InitialDirectory = SettingsFile.Current.FolderPath;
			dialog.Title = "Import Settings (Defaults) File";
			var result = dialog.ShowDialog();
			if (result == System.Windows.Forms.DialogResult.OK)
			{
				List<message> items;
				if (dialog.FileName.EndsWith(".gz"))
				{
					var compressedBytes = System.IO.File.ReadAllBytes(dialog.FileName);
					var bytes = SettingsHelper.Decompress(compressedBytes);
					items = Serializer.DeserializeFromXmlBytes<List<message>>(bytes);
				}
				else
				{
					items = Serializer.DeserializeFromXmlFile<List<message>>(dialog.FileName);
				}
				foreach (var item in items)
				{
					var oldItem = SettingsFile.Current.Defaults.FirstOrDefault(x => x.name == item.name);
					// If old item was not found then...
					if (oldItem == null)
					{
						// Add as new.
						SettingsFile.Current.Defaults.Add(item);
					}
					else
					{
						// Update old item.
						oldItem.UpdateFrom(item);
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
			if (string.IsNullOrEmpty(dialog.FileName)) dialog.FileName = "Settings.Defaults";
			if (string.IsNullOrEmpty(dialog.InitialDirectory)) dialog.InitialDirectory = SettingsFile.Current.FolderPath;
			dialog.Title = "Export Settings (Defaults) File";
			var result = dialog.ShowDialog();
			if (result == System.Windows.Forms.DialogResult.OK)
			{
				var programs = SettingsFile.Current.Defaults.ToList();
				if (dialog.FileName.EndsWith(".gz"))
				{
					var s = Serializer.SerializeToXmlString(programs, System.Text.Encoding.UTF8);
					var bytes = System.Text.Encoding.UTF8.GetBytes(s);
					var compressedBytes = SettingsHelper.Compress(bytes);
					System.IO.File.WriteAllBytes(dialog.FileName, compressedBytes);
				}
				else
				{
					Serializer.SerializeToXmlFile(programs, dialog.FileName, System.Text.Encoding.UTF8, true);
				}
			}
		}

		private void DeleteButton_Click(object sender, EventArgs e)
		{
			var grid = VoicesDefaultsDataGridView;
			var selection = ControlsHelper.GetSelection<string>(grid, "name");
			var itemsToDelete = SettingsFile.Current.Defaults.Where(x => selection.Contains(x.name)).ToArray();
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
					SettingsFile.Current.Defaults.Remove(item);
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
				var column = VoicesDefaultsDataGridView.Columns[e.ColumnIndex];
			}
			e.CellStyle.ForeColor = msg.enabled
				? VoicesDefaultsDataGridView.DefaultCellStyle.ForeColor
				: System.Drawing.SystemColors.ControlDark;
			e.CellStyle.SelectionBackColor = msg.enabled
			 ? VoicesDefaultsDataGridView.DefaultCellStyle.SelectionBackColor
			 : System.Drawing.SystemColors.ControlDark;
			row.HeaderCell.Style.SelectionBackColor = msg.enabled
			 ? VoicesDefaultsDataGridView.DefaultCellStyle.SelectionBackColor
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
				var names = SettingsFile.Current.Defaults.Select(x => x.name.ToLower()).ToArray();
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
		private void VoicesDefaults_MouseHover(object sender, EventArgs e)
		{
			Program.TopForm.MainHelpLabel.Text = "Here you can add names and assign values to them. Values in this list have priority over incoming ( submited ) values.";
		}

		// Tooltip Main.
		private void VoicesDefaults_MouseLeave(object sender, EventArgs e)
		{
			Program.TopForm.ResetHelpToDefault();
		}

		private void BrowseButton_Click(object sender, EventArgs e)
		{
			MainHelper.OpenUrl(SettingsFile.Current.FolderPath);
		}

        private void ExportSaveFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
