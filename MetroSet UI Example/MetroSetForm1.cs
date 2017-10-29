using MetroSet_UI.Controls;
using System;
using MetroSet_UI.Dialogs;
using System.Windows.Forms;
namespace MetroSet_UI_Example
{
    public partial class MetroSetForm1 : MetroSetForm 
    {
        public MetroSetForm1()
        {
            InitializeComponent();
        }

        private void MetroSetButton1_Click(object sender, EventArgs e)
        {
            MetroSetMessageBox.Show(this, "New updates has been found for this program. Would you like to install the new updates?", "Updates Available", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }

        private void metroSetButton1_Click_1(object sender, EventArgs e)
        {
            styleManager1.OpenTheme();
        }
    }
}
