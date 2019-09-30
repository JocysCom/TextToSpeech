using JocysCom.ClassLibrary;
using JocysCom.TextToSpeech.Monitor.PlugIns;
using System.ComponentModel;
using System.Windows.Forms;

namespace JocysCom.TextToSpeech.Monitor
{
	public partial class MainForm
	{

		#region Playing Messages Grid

		BindingList<VoiceListItem> MessagesVoiceItems = new BindingList<VoiceListItem>();

		bool ScrollMessagesGrid = false;
		object ScrollMessagesGridLock = new object();

		private void AudioGlobal_AddingVoiceListItem(object sender, EventArgs<VoiceListItem> e)
		{
			lock (ScrollMessagesGridLock)
			{
				ScrollMessagesGrid = ScrollGrid();
				// Leave maximum 100 items in the list.
				while (MessagesVoiceItems.Count > 100)
				{
					MessagesVoiceItems.RemoveAt(0);
				}
				// Add new item at the bottom.
				MessagesVoiceItems.Add(e.Data);
			}
		}

		bool ScrollGrid()
		{
			bool scroll;
			lock (ScrollMessagesGridLock)
			{
				var grid = MessagesDataGridView;
				int firstDisplayed = grid.FirstDisplayedScrollingRowIndex;
				int displayed = grid.DisplayedRowCount(true);
				int lastVisible = (firstDisplayed + displayed) - 1;
				int lastIndex = grid.RowCount - 1;
				int newIndex = firstDisplayed + 1;
				scroll = lastVisible == lastIndex;
			}
			return scroll;
		}

		void ScrollDownGrid(DataGridView grid)
		{
			lock (ScrollMessagesGridLock)
			{
				if (ScrollMessagesGrid) { grid.FirstDisplayedScrollingRowIndex = grid.RowCount - 1; }
			}
		}

		private void MessagesDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			ScrollDownGrid(MessagesDataGridView);
		}

		private void MessagesDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			ScrollDownGrid(MessagesDataGridView);
		}

		#endregion

	}
}
