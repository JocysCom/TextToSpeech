using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace JocysCom.ClassLibrary.Controls
{

	public class VirtualDataGridView : DataGridView
	{

		//https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/implementing-virtual-mode-wf-datagridview-control
		// Note: row.DataBoundItem is not assigned when cell formatting.
		// Must use DataSource in order to access original data item:
		//
		//private void VirtualDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		//{
		//	if (e.RowIndex < 0)
		//		return;
		//	var grid = (DataGridView)sender;
		//	var row = grid.Rows[e.RowIndex];
		//	var list = (IBindingList)grid.DataSource;
		//	var item = list[e.RowIndex];
		//}

		public object GetDataBoundItem(int rowIndex)
		{
			if (rowIndex < 0)
				return null;
			if (DataSource == null)
				return null;
			var list = DataSource as IBindingList;
			if (list == null)
				return null;
			var item = list[rowIndex];
			return item;
		}

		public VirtualDataGridView()
		{
			VirtualMode = true;
			// Connect the virtual-mode events to event handlers. 
			CellValueNeeded += _grid_CellValueNeeded;
			CellValuePushed += _grid_CellValuePushed;
			NewRowNeeded += _grid_NewRowNeeded;
			RowDirtyStateNeeded += _grid_RowDirtyStateNeeded;
			CancelRowEdit += _grid_CancelRowEdit;
			UserDeletingRow += _grid_UserDeletingRow;
		}

		Type _type;

		/// <summary>
		/// Override default data source.
		/// </summary>
		[AttributeProvider(typeof(IListSource))]
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[IODescription("DataGridViewDataSourceDescr")]
		public new object DataSource
		{
			get { return _Data; }
			set
			{
				if (value != null)
				{
					_type = value.GetType().GetGenericArguments()[0];
				}
				// If data is bound already.
				if (_Data != null)
				{
					// Remove old event.
					_Data.ListChanged -= _Data_ListChanged;
					RowCount = 0;
					_Data = null;
				}
				_Data = (IBindingList)value;
				_Data.ListChanged += _Data_ListChanged;
				// Set the row count, including the row for new records.
				RowCount = _Data.Count;
			}
		}
		IBindingList _Data;

		bool suspendItemDeleted;

		/// <summary>
		/// Allows to delete rows without refreshing grid on every item.
		/// </summary>
		/// <param name="items"></param>
		public void RemoveItems(params object[] items)
		{
			suspendItemDeleted = true;
			foreach (var item in items)
			{
				_Data.Remove(item);
			}
			RowCount = _Data.Count;
			suspendItemDeleted = false;
			InvalidateVisible();
		}

		private void _Data_ListChanged(object sender, ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
				case ListChangedType.Reset:
					RowCount = _Data.Count;
					break;
				case ListChangedType.ItemAdded:
					RowCount = _Data.Count;
					break;
				case ListChangedType.ItemDeleted:
					if (!suspendItemDeleted)
						RowCount = _Data.Count;
					break;
				case ListChangedType.ItemMoved:
					break;
				case ListChangedType.ItemChanged:
					InvalidateVisible(e.OldIndex, e.NewIndex);
					break;
				default:
					break;
			}

		}

		public void InvalidateVisible(params int[] indexes)
		{
			if (FirstDisplayedCell == null)
				return;
			var firstRowIndex = FirstDisplayedCell.RowIndex;
			var rowsCount = DisplayedRowCount(true);
			var lastRowIndex = (firstRowIndex + rowsCount) - 1;
			for (int i = firstRowIndex; i <= lastRowIndex; i++)
			{
				if (indexes.Length == 0 || indexes.Contains(i))
					InvalidateRow(i);
			}
		}

		// Declare a Customer object to store data for a row being edited.
		private object editItem;

		// Declare a variable to store the index of a row being edited. 
		// A value of -1 indicates that there is no row currently in edit. 
		private int editIndex = -1;

		// Declare a variable to indicate the commit scope. 
		// Set this value to false to use cell-level commit scope. 
		private bool rowScopeCommit = true;

		// This event occurs whenever the DataGridView control needs to paint a cell.
		void _grid_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
		{
			if (e.ColumnIndex < 0)
				return;
			if (e.RowIndex < 0)
				return;
			var propertyName = Columns[e.ColumnIndex].DataPropertyName;
			var p = _type.GetProperty(propertyName);
			if (p == null)
				return;
			if (e.RowIndex >= _Data.Count)
				return;
			// Get needed item.
			var item = _Data[e.RowIndex];
			e.Value = p.GetValue(item, null);
		}


		// This event occurs whenever the user commits a cell value change.
		void _grid_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
		{
			if (e.ColumnIndex < 0)
				return;
			if (e.RowIndex < 0)
				return;
			var propertyName = Columns[e.ColumnIndex].DataPropertyName;
			var p = _type.GetProperty(propertyName);
			if (p == null)
				return;
			// Get item to edit.
			var item = _Data[e.RowIndex];
			// Update item value.
			p.SetValue(item, e.Value, null);
		}

		/// <summary>
		/// This event occurs whenever the user enters the row for new records.
		/// </summary>
		private void _grid_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
		{
			// Create a new Customer object when the user edits
			// the row for new records.
			editItem = Activator.CreateInstance(_type);
			editIndex = Rows.Count - 1;
		}


		/// <summary>
		/// This event is useful when the commit scope is determined at run time.
		/// </summary>
		private void _grid_RowDirtyStateNeeded(object sender, QuestionEventArgs e)
		{
			if (!rowScopeCommit)
			{
				// In cell-level commit scope, indicate whether the value
				// of the current cell has been modified.
				e.Response = IsCurrentCellDirty;
			}
		}

		/// <summary>
		/// Event that discards the values of the Customer object representing the current row.
		/// </summary>
		private void _grid_CancelRowEdit(object sender, QuestionEventArgs e)
		{
			if (editIndex == Rows.Count - 2 &&
				editIndex == _Data.Count)
			{
				// If the user has cancelled the edit of a newly created row, 
				// replace the corresponding Customer object with a new, empty one.
				editItem = Activator.CreateInstance(_type);
			}
			else
			{
				// If the user has cancelled the edit of an existing row, 
				// release the corresponding Customer object.
				editItem = null;
				editIndex = -1;
			}
		}

		/// <summary>
		/// This event occurs whenever the user deletes a row by clicking a row header and pressing the DELETE key.
		/// </summary>
		private void _grid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			if (e.Row.Index < _Data.Count)
			{
				// If the user has deleted an existing row, remove the 
				// corresponding Customer object from the data store.
				_Data.RemoveAt(e.Row.Index);
			}

			if (e.Row.Index == editIndex)
			{
				// If the user has deleted a newly created row, release
				// the corresponding Customer object. 
				editIndex = -1;
				editItem = null;
			}
		}

	}

}
