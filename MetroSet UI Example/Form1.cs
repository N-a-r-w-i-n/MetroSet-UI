using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MetroSet_UI.Forms;
namespace MetroSet_UI_Example
{
    public partial class Form1 : MetroSetForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void MetroSetButton1_Click(object sender, EventArgs e)
        {
            if (styleManager1.Style == MetroSet_UI.Design.Style.Light)
            {
                styleManager1.Style = MetroSet_UI.Design.Style.Dark;
            }
            else
            {
                styleManager1.Style = MetroSet_UI.Design.Style.Light;
            }
        }
    }
}
