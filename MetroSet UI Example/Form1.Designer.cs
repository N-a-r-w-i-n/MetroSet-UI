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
            this.metroSetLink1 = new MetroSet_UI.Controls.MetroSetLink();
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
            // metroSetLink1
            // 
            this.metroSetLink1.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(197)))), ((int)(((byte)(245)))));
            this.metroSetLink1.BackColor = System.Drawing.Color.Transparent;
            this.metroSetLink1.Font = new System.Drawing.Font("Segoe WP Light", 10F);
            this.metroSetLink1.ForeColor = System.Drawing.Color.Black;
            this.metroSetLink1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.metroSetLink1.Location = new System.Drawing.Point(379, 187);
            this.metroSetLink1.Name = "metroSetLink1";
            this.metroSetLink1.Size = new System.Drawing.Size(100, 23);
            this.metroSetLink1.Style = MetroSet_UI.Design.Style.Light;
            this.metroSetLink1.StyleManager = this.styleManager1;
            this.metroSetLink1.TabIndex = 0;
            this.metroSetLink1.TabStop = true;
            this.metroSetLink1.Text = "metroSetLink1";
            this.metroSetLink1.ThemeAuthor = "Narwin";
            this.metroSetLink1.ThemeName = "DarkUI";
            this.metroSetLink1.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(157)))), ((int)(((byte)(205)))));
            // 
            // metroSetBadge1
            // 
            this.metroSetBadge1.BackColor = System.Drawing.Color.Transparent;
            this.metroSetBadge1.BadgeAlignent = MetroSet_UI.Controls.MetroSetBadge.BadgeAlign.BottmLeft;
            this.metroSetBadge1.BadgeText = "3";
            this.metroSetBadge1.Font = new System.Drawing.Font("Segoe WP Light", 10F);
            this.metroSetBadge1.Location = new System.Drawing.Point(243, 213);
            this.metroSetBadge1.Name = "metroSetBadge1";
            this.metroSetBadge1.Size = new System.Drawing.Size(245, 64);
            this.metroSetBadge1.Style = MetroSet_UI.Design.Style.Light;
            this.metroSetBadge1.StyleManager = this.styleManager1;
            this.metroSetBadge1.TabIndex = 1;
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
            this.Controls.Add(this.metroSetLink1);
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
        private MetroSet_UI.Controls.MetroSetLink metroSetLink1;
        private MetroSet_UI.Controls.MetroSetBadge metroSetBadge1;
    }
}