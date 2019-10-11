using JocysCom.ClassLibrary.Configuration;
using JocysCom.ClassLibrary.Controls;
using JocysCom.ClassLibrary.Runtime;
using JocysCom.TextToSpeech.Monitor.Capturing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
	public partial class SoundsUserControl : UserControl
	{
		public SoundsUserControl()
		{
			InitializeComponent();
			SoundsDataGridView.AutoGenerateColumns = false;
			SoundsDataGridView.DataSource = SettingsFile.Current.Sounds;
			Audio.Global.IntroSoundSelected += AudioGlobal_IntroSoundSelected;
		}
		private void AudioGlobal_IntroSoundSelected(object sender, ClassLibrary.EventArgs<sound> e)
		{
			SelectRow(e.Data.group);
		}
		private void SoundsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;
			var grid = (DataGridView)sender;
			if (e.ColumnIndex == grid.Columns[EnabledColumn.Name].Index)
			{
				var snd = (sound)grid.Rows[e.RowIndex].DataBoundItem;
				snd.enabled = !snd.enabled;
				SoundsDataGridView.Invalidate();
			}
			if (e.ColumnIndex == grid.Columns[GroupColumn.Name].Index) SoundsDataGridView.BeginEdit(true);
			if (e.ColumnIndex == grid.Columns[FileColumn.Name].Index) SoundsDataGridView.BeginEdit(true);
		}

		public void SoundsAddNewRecord()
		{
			var dialog = SoundsImportOpenFileDialog;
			dialog.DefaultExt = "*.xml";
			dialog.Filter = "Audio File (*.wav)|*.wav|All files (*.*)|*.*";
			dialog.FilterIndex = 1;
			dialog.RestoreDirectory = true;
			if (string.IsNullOrEmpty(dialog.FileName)) dialog.FileName = "";
			if (string.IsNullOrEmpty(dialog.InitialDirectory)) dialog.InitialDirectory = SettingsFile.Current.FolderPath;
			dialog.Title = "Add Audio File";
			var result = dialog.ShowDialog();
			if (result == System.Windows.Forms.DialogResult.OK)
			{
				var grid = SoundsDataGridView;
				int i = 0;
				string group;
				// Get all groups in lowercase.
				var groups = SettingsFile.Current.Sounds.Select(x => x.group.ToLower()).ToArray();
				// Loop until free group is found.
				while (true)
				{
					i++;
					group = string.Format("Group{0}", i);
					// If group is unique and not in the list then break loop.
					if (!groups.Contains(group.ToLower())) break;
				}

				var snd = new sound();
				snd.group = group;
				//snd.file = MainHelper.ConvertToSpecialFoldersPattern(dialog.FileName);
				snd.file = dialog.FileName;
				SettingsFile.Current.Sounds.Add(snd);
				grid.BeginEdit(true);
			}
		}

		public void SelectRow(string group)
		{
			if (string.IsNullOrEmpty(group))
			{
				SoundsDataGridView.ClearSelection();
				return;
			}
			foreach (DataGridViewRow row in SoundsDataGridView.Rows)
			{
				var item = (sound)row.DataBoundItem;
				if (item.group.ToLower() == group.ToLower() && !row.Selected)
				{
					SoundsDataGridView.ClearSelection();
					row.Selected = true;
					SoundsDataGridView.FirstDisplayedCell = row.Cells[0];
					break;
				}
			}
		}


		private void SoundsAddButton_Click(object sender, EventArgs e)
		{
			SoundsAddNewRecord();
		}
		private void SoundsImportButton_Click(object sender, EventArgs e)
		{
			var dialog = SoundsImportOpenFileDialog;
			dialog.DefaultExt = "*.xml";
			dialog.Filter = "Game Settings (*.xml;*.xml.gz)|*.xml;*.gz|All files (*.*)|*.*";
			dialog.FilterIndex = 1;
			dialog.RestoreDirectory = true;
			if (string.IsNullOrEmpty(dialog.FileName)) dialog.FileName = "Settings.Sounds";
			if (string.IsNullOrEmpty(dialog.InitialDirectory)) dialog.InitialDirectory = SettingsFile.Current.FolderPath;
			dialog.Title = "Import Settings (Sounds) File";
			var result = dialog.ShowDialog();
			if (result == System.Windows.Forms.DialogResult.OK)
			{
				List<sound> items;
				if (dialog.FileName.EndsWith(".gz"))
				{
					var compressedBytes = System.IO.File.ReadAllBytes(dialog.FileName);
					var bytes = SettingsHelper.Decompress(compressedBytes);
					items = Serializer.DeserializeFromXmlBytes<List<sound>>(bytes);
				}
				else
				{
					items = Serializer.DeserializeFromXmlFile<List<sound>>(dialog.FileName);
				}
				foreach (var item in items)
				{
					var oldItem = SettingsFile.Current.Sounds.FirstOrDefault(x => x.group == item.group);
					// If old item was not found then...
					if (oldItem == null)
					{
						// Add as new.
						SettingsFile.Current.Sounds.Add(item);
					}
					else
					{
						// Udate old item.
						oldItem.group = item.group;

					}
				}
			}
		}

		private void SoundsExportButton_Click(object sender, EventArgs e)
		{
			var dialog = SoundsExportSaveFileDialog;
			dialog.DefaultExt = "*.xml";
			dialog.Filter = "Settings (*.xml)|*.xml|Compressed Settings (*.xml.gz)|*.xml.gz|All files (*.*)|*.*";
			dialog.FilterIndex = 1;
			dialog.RestoreDirectory = true;
			if (string.IsNullOrEmpty(dialog.FileName)) dialog.FileName = "Settings.Sounds";
			if (string.IsNullOrEmpty(dialog.InitialDirectory)) dialog.InitialDirectory = SettingsFile.Current.FolderPath;
			dialog.Title = "Export Settings (Sounds) File";
			var result = dialog.ShowDialog();
			if (result == System.Windows.Forms.DialogResult.OK)
			{
				var programs = SettingsFile.Current.Sounds.ToList();
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

		private void SoundsDeleteButton_Click(object sender, EventArgs e)
		{
			var grid = SoundsDataGridView;
			var selection = ControlsHelper.GetSelection<string>(grid, "group");
			var itemsToDelete = SettingsFile.Current.Sounds.Where(x => selection.Contains(x.group)).ToArray();
			MessageBoxForm form = new MessageBoxForm();
			form.StartPosition = FormStartPosition.CenterParent;
			string sound;
			if (itemsToDelete.Length == 1)
			{
				var item = itemsToDelete[0];
				sound = string.Format("Are you sure you want to delete settings for?\r\n\r\nGroup: {0}",
					item.group);
			}
			else
			{
				sound = string.Format("Delete {0} setting(s)?", itemsToDelete.Length);
			}
			var result = form.ShowForm(sound, "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
			if (result == DialogResult.OK)
			{
				foreach (var item in itemsToDelete)
				{
					SettingsFile.Current.Sounds.Remove(item);
				}
				SettingsFile.Current.Save();
			}
		}

		private void SoundsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex == -1) return;
			var grid = (DataGridView)sender;
			var row = grid.Rows[e.RowIndex];
			var snd = (sound)row.DataBoundItem;
			// If this is not row header then...
			if (e.ColumnIndex > -1)
			{
				var column = SoundsDataGridView.Columns[e.ColumnIndex];
			}
			e.CellStyle.ForeColor = snd.enabled
				? SoundsDataGridView.DefaultCellStyle.ForeColor
				: System.Drawing.SystemColors.ControlDark;
			e.CellStyle.SelectionBackColor = snd.enabled
			 ? SoundsDataGridView.DefaultCellStyle.SelectionBackColor
			 : System.Drawing.SystemColors.ControlDark;
			row.HeaderCell.Style.SelectionBackColor = snd.enabled
			 ? SoundsDataGridView.DefaultCellStyle.SelectionBackColor
			 : System.Drawing.SystemColors.ControlDark;

		}

		private void SoundsDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			var grid = (DataGridView)sender;
			var row = grid.Rows[e.RowIndex];
			row.ErrorText = "";
			// Don't try to validate the 'new row' until finished  
			// editing since there is no point in validating its initial values. 
			if (row.IsNewRow) { return; }
			var snd = (sound)row.DataBoundItem;
			var column = grid.Columns[e.ColumnIndex];
			string error = null;
			var value = e.FormattedValue.ToString();
			// If Group Name changed then...
			if (e.ColumnIndex == grid.Columns[GroupColumn.Name].Index && value.ToLower() != snd.group.ToLower())
			{
				var groups = SettingsFile.Current.Sounds.Select(x => x.group.ToLower()).ToArray();
				if (string.IsNullOrEmpty(value) || groups.Contains(value.ToLower()))
				{
					error = "Group field value must be unique and not empty!";
				}
			}
			// If Audio File then...
			else if (e.ColumnIndex == grid.Columns[FileColumn.Name].Index && !string.IsNullOrEmpty(value))
			{
				if (string.IsNullOrEmpty(value))
				{
					error = "File path value must be not empty!";
				}
			}
			if (!string.IsNullOrEmpty(error))
			{
				e.Cancel = true;
				row.ErrorText = error;
			}
		}

		private void SoundsDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			// Clear the row error in case the user presses ESC.   
			var grid = (DataGridView)sender;
			grid.Rows[e.RowIndex].ErrorText = String.Empty;
		}

		//Tooltip MouseHover.
		private void Sounds_MouseHover(object sender, EventArgs e)
		{
			Program.TopForm.MainHelpLabel.Text = "Here you can add groups ( like \"Whisper\" ) and assign sounds to them ( like \"Radio\" -- listed in \"Default Intro Sound\" drop-down box ) or set paths to wav files ( like \"C:\\Windows\\Media\\notify.wav\" ).";
		}

		// Tooltip Main.
		private void Sounds_MouseLeave(object sender, EventArgs e)
		{
			Program.TopForm.ResetHelpToDefault();
		}

		private void SoundsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void BrowseButton_Click(object sender, EventArgs e)
		{
			MainHelper.OpenUrl(SettingsFile.Current.FolderPath);
		}

		private void ResetButton_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Do you want to reset Intro Sounds to default?", "Reset Intro Sounds", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (result == DialogResult.Yes)
			{
				SettingsFile.Current.ResetSoundsToDefault();
			}
		}
	}
}
