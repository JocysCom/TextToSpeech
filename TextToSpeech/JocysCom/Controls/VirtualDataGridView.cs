using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace JocysCom.ClassLibrary.Configuration
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
		public new IList DataSource
		{
			get { return _Data; }
			set
			{
				if (value != null)
					_type = value.GetType().GetGenericArguments()[0];
				_Data = value;
				// Set the row count, including the row for new records.
				RowCount = _Data.Count;
			}
		}
		IList _Data;

		//https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/implementing-virtual-mode-wf-datagridview-control

		// Declare a Customer object to store data for a row being edited.
		private ISettingsItem editItem;

		// Declare a variable to store the index of a row being edited. 
		// A value of -1 indicates that there is no row currently in edit. 
		private int editIndex = -1;

		// Declare a variable to indicate the commit scope. 
		// Set this value to false to use cell-level commit scope. 
		private bool rowScopeCommit = true;

		// This event occurs whenever the DataGridView control needs to paint a cell.
		void _grid_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
		{
			// If this is the row for new records, no values are needed.
			if (e.RowIndex == RowCount - 1)
				return;
			// Set the cell value to paint using the Customer object retrieved.
			var propertyName = Columns[e.ColumnIndex].DataPropertyName;
			if (string.IsNullOrEmpty(propertyName))
				return;
			// Store a reference to the Customer object for the row being painted.
			var item = (e.RowIndex == editIndex)
				? editItem
				: (ISettingsItem)_Data[e.RowIndex];
			var p = _type.GetProperty(propertyName);
			e.Value = p.GetValue(item, null);
		}


		// This event occurs whenever the user commits a cell value change.
		void _grid_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
		{
			ISettingsItem item = null;
			// Store a reference to the Customer object for the row being edited.
			if (e.RowIndex < _Data.Count)
			{
				// If the user is editing a new row, create a new Customer object.
				if (editItem == null)
				{
					var currentItem = _Data[e.RowIndex];
					editItem = (ISettingsItem)Activator.CreateInstance(_type);
					var ei = (ISettingsItem)_Data[e.RowIndex];
					Runtime.Helper.CopyProperties(ei, editItem);
				}
				item = editItem;
				editIndex = e.RowIndex;
			}
			else
			{
				item = editItem;
			}
			// Set the cell value to paint using the Customer object retrieved.
			var propertyName = Columns[e.ColumnIndex].DataPropertyName;
			var p = _type.GetProperty(propertyName);
			p.SetValue(item, e.Value, null);
		}

		/// <summary>
		/// This event occurs whenever the user enters the row for new records.
		/// </summary>
		private void _grid_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
		{
			// Create a new Customer object when the user edits
			// the row for new records.
			editItem = (ISettingsItem)Activator.CreateInstance(_type);
			editIndex = Rows.Count - 1;
		}


		/// <summary>
		/// This event occurs whenever the user changes the current row.
		/// </summary>
		private void _grid_RowValidated(object sender, DataGridViewCellEventArgs e)
		{
			// Save row changes if any were made and release the edited 
			// Customer object if there is one.
			if (e.RowIndex >= _Data.Count &&
				e.RowIndex != Rows.Count - 1)
			{
				// Add the new Customer object to the data store.
				_Data.Add(editItem);
				editItem = null;
				editIndex = -1;
			}
			else if (editItem != null &&
				e.RowIndex < _Data.Count)
			{
				// Save the modified Customer object in the data store.
				_Data[e.RowIndex] = editItem;
				editItem = null;
				editIndex = -1;
			}
			else if (ContainsFocus)
			{
				editItem = null;
				editIndex = -1;
			}
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
				editItem = (ISettingsItem)Activator.CreateInstance(_type);
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
