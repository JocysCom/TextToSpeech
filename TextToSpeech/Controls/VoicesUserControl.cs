using JocysCom.ClassLibrary.Controls;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor.Controls
{
	public partial class VoicesUserControl : UserControl
	{
		public VoicesUserControl()
		{
			InitializeComponent();
			if (ControlsHelper.IsDesignMode(this))
				return;
			VoicesDataGridView.AutoGenerateColumns = false;
			// Enable double buffering to make redraw faster.
			typeof(DataGridView).InvokeMember("DoubleBuffered",
			BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
			null, VoicesDataGridView, new object[] { true });
		}

		public DataGridView VoicesGridView
		{
			get { return VoicesDataGridView; }
		}

		public void EnableGrid(bool enabled)
		{
			VoicesDataGridView.Enabled = enabled;
			VoicesDataGridView.DefaultCellStyle.SelectionBackColor = enabled
				? SystemColors.Highlight
				: SystemColors.ControlDark;
		}

		public InstalledVoiceEx GetSelectedItem()
		{
			var selectedItem = VoicesGridView.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
			if (selectedItem != null)
				return (InstalledVoiceEx)selectedItem.DataBoundItem;
			return null;
		}

		public void SelectItem(InstalledVoiceEx item)
		{
			foreach (DataGridViewRow row in VoicesDataGridView.Rows)
			{
				if (!row.DataBoundItem.Equals(item))
					continue;
				row.Selected = true;
				VoicesDataGridView.FirstDisplayedCell = row.Cells[0];
				break;
			}
		}

		#region VoicesDataGridView

		private void VoicesDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			//if (e.RowIndex == -1) return;
			//var grid = (DataGridView)sender;
			//var voice = (InstalledVoiceEx)grid.Rows[e.RowIndex].DataBoundItem;
			//var column = VoicesDataGridView.Columns[e.ColumnIndex];
			e.Cancel = true;
		}

		private void VoicesDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex == -1) return;
			var grid = (DataGridView)sender;
			var voice = (InstalledVoiceEx)grid.Rows[e.RowIndex].DataBoundItem;
			var column = VoicesDataGridView.Columns[e.ColumnIndex];
			if (e.ColumnIndex == grid.Columns[AgeColumn.Name].Index)
			{
				if (voice.Age.ToString() == "NotSet") e.Value = "...";
			}
			e.CellStyle.ForeColor = voice.Enabled
				? VoicesDataGridView.DefaultCellStyle.ForeColor
				: SystemColors.ControlDark;
			e.CellStyle.SelectionBackColor = voice.Enabled
			 ? VoicesDataGridView.DefaultCellStyle.SelectionBackColor
			 : SystemColors.ControlDark;
		}

		private void VoicesDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;
			var grid = (DataGridView)sender;
			//var column = VoicesDataGridView.Columns[e.ColumnIndex];
			if (e.ColumnIndex == grid.Columns[EnabledColumn.Name].Index)
			{
				var voice = (InstalledVoiceEx)grid.Rows[e.RowIndex].DataBoundItem;
				voice.Enabled = !voice.Enabled;
				VoicesDataGridView.Invalidate();
			}
			if (e.ColumnIndex == grid.Columns[FemaleColumn.Name].Index) VoicesDataGridView.BeginEdit(true);
			if (e.ColumnIndex == grid.Columns[MaleColumn.Name].Index) VoicesDataGridView.BeginEdit(true);
			if (e.ColumnIndex == grid.Columns[NeutralColumn.Name].Index) VoicesDataGridView.BeginEdit(true);
		}


		#endregion
	}
}
