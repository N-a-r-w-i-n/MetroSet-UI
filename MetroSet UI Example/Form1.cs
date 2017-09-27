using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MetroSet_UI.Controls;

namespace MetroSet_UI_Example
{
    public partial class Form1 : MetroSetForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             for (int i = 0; i < 10; i++)
            {
                metroSetComboBox1.Items.Add($"MetroSetItem{i}");
            }
              
        }

        private void metroSetButton1_Click(object sender, EventArgs e)
        {
            styleManager1.OpenTheme();
        }
    }
}
