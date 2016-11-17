using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace JocysCom.ClassLibrary.Configuration
{
	/*

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

	*/

	public interface IDataGridView
	{
		DataGridView DataGridView { get; }
	}

	public class ExtendedDataGridViewColumnCollectionEditor : UITypeEditor
	{
		private Form dataGridViewColumnCollectionDialog;

		private ExtendedDataGridViewColumnCollectionEditor() { }

		private static Form CreateColumnCollectionDialog(IServiceProvider provider)
		{
			var assembly = Assembly.Load(typeof(ControlDesigner).Assembly.ToString());
			var type = assembly.GetType("System.Windows.Forms.Design.DataGridViewColumnCollectionDialog");

			var ctr = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
			return (Form)ctr.Invoke(new object[] { provider });
		}

		public static void SetLiveDataGridView(Form form, DataGridView grid)
		{
			var mi = form.GetType().GetMethod("SetLiveDataGridView", BindingFlags.NonPublic | BindingFlags.Instance);
			mi.Invoke(form, new object[] { grid });
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null && context != null)
			{
				var service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (service == null || context.Instance == null)
				{
					return value;
				}
				var host = (IDesignerHost)provider.GetService(typeof(IDesignerHost));
				if (host == null)
				{
					return value;
				}
				if (dataGridViewColumnCollectionDialog == null)
				{
					dataGridViewColumnCollectionDialog = CreateColumnCollectionDialog(provider);
				}
				var grid = ((IDataGridView)context.Instance).DataGridView;
				// Set Site property because it will be accessed inside SetLiveDataGridView() method.
				// By default it's usually null and if not set here, then exception will be thrown inside SetLiveDataGridView().
				var oldSite = grid.Site;
				grid.Site = ((UserControl)context.Instance).Site;
				// Use reflection to execute SetLiveDataGridView().
				SetLiveDataGridView(dataGridViewColumnCollectionDialog, grid);
				using (var transaction = host.CreateTransaction("DataGridViewColumnCollectionTransaction"))
				{
					if (service.ShowDialog(dataGridViewColumnCollectionDialog) == DialogResult.OK)
					{
						transaction.Commit();
					}
					else
					{
						transaction.Cancel();
					}
				}
				// Set Site property back to the previous value to prevent problems with serializing control.
				grid.Site = oldSite;
			}

			return value;
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}

}
