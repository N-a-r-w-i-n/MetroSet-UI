using MetroSet_UI.Controls;
using MetroSet_UI.Interfaces;
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void metroSetTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroSetSwitch1_SwitchedChanged(object sender)
        {
            //foreach (Control C in ParentForm.Controls)
            //{
            //    if (C is iControl)
            //    {
            //        if (metroSetSwitch1.Switched)
            //        {

            //            ((iControl)C).Style = MetroSet_UI.Design.Style.Dark;

            //        }
            //        else
            //        {
            //            ((iControl)C).Style = MetroSet_UI.Design.Style.Light;
            //        }
            //    }
            //}
            if (metroSetSwitch1.Switched)
            {

               metroSetTabControl1.Style = MetroSet_UI.Design.Style.Dark;

            }
            else
            {
                metroSetTabControl1.Style = MetroSet_UI.Design.Style.Light;
            }
        }
    }
}
