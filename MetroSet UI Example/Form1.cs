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
             
        } 

        private void metroSetButton1_Click(object sender, EventArgs e)
        {
            //styleManager1.OpenTheme();
            //styleManager1.SetTheme("C:\\Users\\Stick\\Desktop\\MetroSet Theme.xml");
        }

        private void metroSetButton1_Click_1(object sender, EventArgs e)
        {
            styleManager1.OpenTheme();
        }
    }
}
