using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Linq;
using System.Collections;
using JocysCom.ClassLibrary.Controls;

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
            InitDelayTimer();
            SettingsDataGridView.AutoGenerateColumns = false;
        }

        public DataGridViewColumn DefaultEditColumn;

        private void SettingsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // If column header clicked then return.
            if (e.RowIndex < 0)
                return;
            // If row header clicked then return.
            if (e.ColumnIndex < 0)
                return;
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

        //public void SelectRow(string group)
        //{
        //	if (string.IsNullOrEmpty(group))
        //	{
        //		SettingsDataGridView.ClearSelection();
        //		return;
        //	}
        //	foreach (DataGridViewRow row in SettingsDataGridView.Rows)
        //	{
        //		//var item = (Setting)row.DataBoundItem;
        //		//if (item.Group.ToLower() == group.ToLower() && !row.Selected)
        //		//{
        //		//    SettingsDataGridView.ClearSelection();
        //		//    row.Selected = true;
        //		//    SettingsDataGridView.FirstDisplayedCell = row.Cells[0];
        //		//    break;
        //		//}
        //	}
        //}

        object[] GetSelectedItems()
        {
            var grid = SettingsDataGridView;
            var list = (IList)grid.DataSource;
            var selectedActions = grid
            .SelectedRows
            .Cast<DataGridViewRow>()
            .Select(x => (object)x.DataBoundItem)
            // Make sure that selected actions are ordered like in the list.
            .OrderBy(x => list.IndexOf(x))
            .ToArray();
            return selectedActions;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var grid = SettingsDataGridView;
            var list = (IList)grid.DataSource;
            var type = list.GetType().GetGenericArguments()[0];
            var selectedActions = GetSelectedItems();
            var last = selectedActions.LastOrDefault();
            var insertIndex = (last == null)
                    ? -1 : list.IndexOf(last);
            var item = Activator.CreateInstance(type);
            // If there are no records or last row is selected then...
            if (insertIndex == -1 || insertIndex == (list.Count - 1))
            {
                list.Add(item);
            }
            else
            {
                list.Insert(insertIndex + 1, item);
            }
            DataGridViewRow rowToEdit = null;
            // Select new created item.
            foreach (DataGridViewRow row in grid.Rows)
            {
                var selected = (item == row.DataBoundItem);
                if (row.Selected != selected)
                {
                    // Select row
                    row.Selected = selected;
                    if (selected)
                    {
                        rowToEdit = row;
                    }
                }
            }
            if (rowToEdit != null)
            {
                var column = DefaultEditColumn == null
                    ? grid.Columns.Cast<DataGridViewColumn>().FirstOrDefault(x => !x.ReadOnly && x is DataGridViewTextBoxColumn)
                    : DefaultEditColumn;
                if (column != null)
                {
                    // Select column
                    var cell = rowToEdit.Cells[column.Name];
                    cell.Selected = true;
                    grid.CurrentCell = cell;
                    // Switch to edit mode.
                    grid.BeginEdit(true);
                }

            }
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            var dialog = SettingsImportOpenFileDialog;
            dialog.SupportMultiDottedExtensions = true;
            dialog.Filter = "Settings (*.xml;*.xml.gz)|*.xml;*.gz|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            var fi = Data.XmlFile;
            if (string.IsNullOrEmpty(dialog.FileName)) dialog.FileName = System.IO.Path.GetFileNameWithoutExtension(fi.Name);
            if (string.IsNullOrEmpty(dialog.InitialDirectory)) dialog.InitialDirectory = fi.Directory.FullName;
            dialog.Title = "Import Settings File";
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Data.LoadFrom(dialog.FileName);
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            var dialog = SettingsExportSaveFileDialog;
            dialog.SupportMultiDottedExtensions = true;
            dialog.Filter = "Settings (*.xml)|*.xml|Compressed Settings (*.xml.gz)|*.xml.gz|All files (*.*)|*.*";
            //dialog.DefaultExt = "*.xml";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            var fi = Data.XmlFile;
            if (string.IsNullOrEmpty(dialog.FileName)) dialog.FileName = fi.Name;
            if (string.IsNullOrEmpty(dialog.InitialDirectory)) dialog.InitialDirectory = fi.Directory.FullName;
            dialog.Title = "Export Settings File";
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Data.SaveAs(dialog.FileName);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var grid = SettingsDataGridView;
            var list = (IList)grid.DataSource;
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
                    list.Remove(item);
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

        private void SettingsDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Clear the row error in case the user presses ESC.   
            var grid = (DataGridView)sender;
            grid.Rows[e.RowIndex].ErrorText = String.Empty;
        }

        private void ShowInFolderButton_Click(object sender, EventArgs e)
        {
            OpenUrl(Data.XmlFile.Directory.FullName);
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you want to reset settings to default?", "Reset Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
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

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Data.Save();
        }

        #region Delay Timer

        System.Timers.Timer DelayTimer;
        object DelayTimerLock = new object();

        void InitDelayTimer()
        {
            DelayTimer = new System.Timers.Timer();
            DelayTimer.AutoReset = false;
            DelayTimer.Interval = 520;
            DelayTimer.Elapsed += DelayTimer_Elapsed;
            DelayTimer.SynchronizingObject = this;
            FilterTextBox.TextChanged += delegate (object sender, EventArgs e)
            {
                ResetDelayTimer();
            };
        }

        void DisposeDelayTimer()
        {
            lock (DelayTimerLock)
            {
                if (DelayTimer != null)
                {
                    DelayTimer.Dispose();
                    DelayTimer = null;
                }
            }
        }

        void ResetDelayTimer()
        {
            lock (DelayTimerLock)
            {
                if (DelayTimer != null)
                {
                    DelayTimer.Stop();
                    DelayTimer.Start();
                }
            }
        }

        string _CurrentFilter;

        private void DelayTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var filter = FilterTextBox.Text;
            if (filter == _CurrentFilter)
            {
                return;
            }
            _CurrentFilter = filter;
            var ev = FilterChanged;
            if (ev != null)
            {
                ev(this, new EventArgs());
            }
        }

        public bool UpdateOnly
        {
            set
            {
                AddButton.Enabled = !value;
                EditButton.Enabled = !value;
                DeleteButton.Enabled = !value;
                ImportButton.Enabled = !value;
                ExportButton.Enabled = !value;
                ResetButton.Enabled = !value;
            }
        }


        public event EventHandler<EventArgs> FilterChanged;

        #endregion

        #region IDisposable

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            DisposeDelayTimer();
            if (disposing && (settings != null))
            {
                settings.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        private void EditButton_Click(object sender, EventArgs e)
        {
            var item = GetSelectedItems().FirstOrDefault();
            if (item != null)
            {
                var form = new SettingsItemForm();
                form.StartPosition = FormStartPosition.CenterParent;
                var newItem = Activator.CreateInstance(item.GetType());
                Runtime.Helper.CopyProperties(item, newItem);
                form.MainPropertyGrid.SelectedObject = newItem;
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Runtime.Helper.CopyProperties(newItem, item);
                }

            }
        }
    }
}
