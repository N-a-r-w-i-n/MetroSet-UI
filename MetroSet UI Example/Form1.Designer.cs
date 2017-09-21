namespace MetroSet_UI_Example
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.styleManager1 = new MetroSet_UI.StyleManager();
            this.metroSetLabel1 = new MetroSet_UI.Controls.MetroSetLabel();
            this.metroSetButton1 = new MetroSet_UI.Controls.MetroSetButton();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.CustomTheme = "C:\\Users\\Stick\\Desktop\\MetroSet Theme.xml";
            this.styleManager1.MetroForm = this;
            this.styleManager1.Style = MetroSet_UI.Design.Style.Light;
            this.styleManager1.ThemeAuthor = "Narwin";
            this.styleManager1.ThemeName = "DarkUI";
            // 
            // metroSetLabel1
            // 
            this.metroSetLabel1.BackColor = System.Drawing.Color.Transparent;
            this.metroSetLabel1.Font = new System.Drawing.Font("Segoe WP Light", 10F);
            this.metroSetLabel1.ForeColor = System.Drawing.Color.Gray;
            this.metroSetLabel1.Location = new System.Drawing.Point(399, 158);
            this.metroSetLabel1.Name = "metroSetLabel1";
            this.metroSetLabel1.Size = new System.Drawing.Size(100, 23);
            this.metroSetLabel1.Style = MetroSet_UI.Design.Style.Light;
            this.metroSetLabel1.StyleManager = this.styleManager1;
            this.metroSetLabel1.TabIndex = 1;
            this.metroSetLabel1.Text = "metroSetLabel1";
            this.metroSetLabel1.ThemeAuthor = "Narwin";
            this.metroSetLabel1.ThemeName = "DarkUI";
            this.metroSetLabel1.Click += new System.EventHandler(this.metroSetLabel1_Click);
            // 
            // metroSetButton1
            // 
            this.metroSetButton1.Font = new System.Drawing.Font("Segoe WP Light", 10F);
            this.metroSetButton1.Location = new System.Drawing.Point(361, 215);
            this.metroSetButton1.Name = "metroSetButton1";
            this.metroSetButton1.Size = new System.Drawing.Size(198, 42);
            this.metroSetButton1.Style = MetroSet_UI.Design.Style.Light;
            this.metroSetButton1.StyleManager = this.styleManager1;
            this.metroSetButton1.TabIndex = 2;
            this.metroSetButton1.Text = "metroSetButton1";
            this.metroSetButton1.ThemeAuthor = "Narwin";
            this.metroSetButton1.ThemeName = "DarkUI";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(812, 468);
            this.Controls.Add(this.metroSetButton1);
            this.Controls.Add(this.metroSetLabel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.SmallRectThickness = 2;
            this.StyleManager = this.styleManager1;
            this.Text = "Metro UI";
            this.ThemeAuthor = "Narwin";
            this.ThemeName = "DarkUI";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroSet_UI.StyleManager styleManager1;
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel1;
        private MetroSet_UI.Controls.MetroSetButton metroSetButton1;
    }
}