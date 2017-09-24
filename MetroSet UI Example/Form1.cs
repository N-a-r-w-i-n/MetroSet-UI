using MetroSet_UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MetroSet_UI_Example
{
    public partial class Form1 : MetroSetForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void metroSetButton1_Click(object sender, EventArgs e)
        {
            styleManager1.OpenTheme();
        }

        private void metroSetCheckBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
