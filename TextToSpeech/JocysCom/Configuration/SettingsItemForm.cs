using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JocysCom.ClassLibrary.Configuration
{
    public partial class SettingsItemForm : Form
    {
        public SettingsItemForm()
        {
            InitializeComponent();
            var info = new AssemblyInfo();
            Text = info.GetTitle(true, true, true, true, false);
        }
    }
}
