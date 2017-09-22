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
            this.metroSetBadge1 = new MetroSet_UI.Controls.MetroSetBadge();
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
            // metroSetBadge1
            // 
            this.metroSetBadge1.BackColor = System.Drawing.Color.Transparent;
            this.metroSetBadge1.BadgeAlignment = MetroSet_UI.Enums.BadgeAlign.BottmLeft;
            this.metroSetBadge1.BadgeText = "5";
            this.metroSetBadge1.Font = new System.Drawing.Font("Segoe WP Light", 10F);
            this.metroSetBadge1.Location = new System.Drawing.Point(248, 180);
            this.metroSetBadge1.Name = "metroSetBadge1";
            this.metroSetBadge1.Size = new System.Drawing.Size(209, 66);
            this.metroSetBadge1.Style = MetroSet_UI.Design.Style.Light;
            this.metroSetBadge1.StyleManager = this.styleManager1;
            this.metroSetBadge1.TabIndex = 0;
            this.metroSetBadge1.Text = "metroSetBadge1";
            this.metroSetBadge1.ThemeAuthor = "Narwin";
            this.metroSetBadge1.ThemeName = "DarkUI";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 356);
            this.Controls.Add(this.metroSetBadge1);
            this.Name = "Form1";
            this.SmallRectThickness = 2;
            this.StyleManager = this.styleManager1;
            this.Text = "Form1";
            this.ThemeAuthor = "Narwin";
            this.ThemeName = "DarkUI";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private MetroSet_UI.Controls.MetroSetBadge metroSetBadge1;
    }
}