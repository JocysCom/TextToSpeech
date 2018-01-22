using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace JocysCom.ClassLibrary.Controls
{
	public class VirtualDataGridView : DataGridView
	{

		Type _type;

		public VirtualDataGridView()
		{
			VirtualMode = true;
			// Connect the virtual-mode events to event handlers. 
			CellValueNeeded += _grid_CellValueNeeded;
			CellValuePushed += _grid_CellValuePushed;
			NewRowNeeded += _grid_NewRowNeeded;
			RowValidated += _grid_RowValidated;
			RowDirtyStateNeeded += _grid_RowDirtyStateNeeded;
			CancelRowEdit += _grid_CancelRowEdit;
			UserDeletingRow += _grid_UserDeletingRow;
		}

		/// <summary>
		/// Override default data souurce.
		/// </summary>
		[AttributeProvider(typeof(IListSource))]
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[IODescriptionAttribute("DataGridViewDataSourceDescr")]
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

		//https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/implementing-virtual-mode-wf-datagridview-control

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
			//object item = null;
			//// Store a reference to the Customer object for the row being edited.
			//if (e.RowIndex < _Data.Count)
			//{
			//	// If the user is editing a new row, create a new Customer object.
			//	if (editItem == null)
			//	{
			//		var currentItem = _Data[e.RowIndex];
			//		editItem = Activator.CreateInstance(_type);
			//		var ei = _Data[e.RowIndex];
			//		Runtime.Helper.CopyProperties(ei, editItem);
			//	}
			//	item = editItem;
			//	editIndex = e.RowIndex;
			//}
			//else
			//{
			//	item = editItem;
			//}
			//// Set the cell value to paint using the Customer object retrieved.
			//var propertyName = Columns[e.ColumnIndex].DataPropertyName;
			//var p = _type.GetProperty(propertyName);
			//p.SetValue(item, e.Value, null);
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
		/// This event occurs whenever the user changes the current row.
		/// </summary>
		private void _grid_RowValidated(object sender, DataGridViewCellEventArgs e)
		{
			//// Save row changes if any were made and release the edited 
			//// Customer object if there is one.
			//if (e.RowIndex >= _Data.Count &&
			//	e.RowIndex != Rows.Count - 1)
			//{
			//	// Add the new Customer object to the data store.
			//	_Data.Add(editItem);
			//	editItem = null;
			//	editIndex = -1;
			//}
			//else if (editItem != null &&
			//	e.RowIndex < _Data.Count)
			//{
			//	// Save the modified Customer object in the data store.
			//	_Data[e.RowIndex] = editItem;
			//	editItem = null;
			//	editIndex = -1;
			//}
			//else if (ContainsFocus)
			//{
			//	editItem = null;
			//	editIndex = -1;
			//}
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
				// If the user has canceled the edit of a newly created row, 
				// replace the corresponding Customer object with a new, empty one.
				editItem = Activator.CreateInstance(_type);
			}
			else
			{
				// If the user has canceled the edit of an existing row, 
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
