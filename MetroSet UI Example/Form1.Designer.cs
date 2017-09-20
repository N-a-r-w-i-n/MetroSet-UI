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
            this.metroSetButton1 = new MetroSet_UI.Controls.MetroSetButton();
            this.styleManager1 = new MetroSet_UI.StyleManager();
            this.metroSetLabel1 = new MetroSet_UI.Controls.MetroSetLabel();
            this.SuspendLayout();
            // 
            // metroSetButton1
            // 
            this.metroSetButton1.BackColor = System.Drawing.Color.Transparent;
            this.metroSetButton1.Font = new System.Drawing.Font("Segoe WP", 12F);
            this.metroSetButton1.Location = new System.Drawing.Point(243, 190);
            this.metroSetButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroSetButton1.Name = "metroSetButton1";
            this.metroSetButton1.Size = new System.Drawing.Size(271, 63);
            this.metroSetButton1.Style = MetroSet_UI.Design.Style.Custom;
            this.metroSetButton1.StyleManager = this.styleManager1;
            this.metroSetButton1.TabIndex = 0;
            this.metroSetButton1.Text = "metroSetButton1";
            this.metroSetButton1.ThemeAuthor = "Narwin";
            this.metroSetButton1.ThemeName = "DarkUI";
            // 
            // styleManager1
            // 
            this.styleManager1.CustomTheme = "C:\\Users\\Stick\\Desktop\\MetroSet Theme.xml";
            this.styleManager1.MetroForm = this;
            this.styleManager1.Style = MetroSet_UI.Design.Style.Custom;
            this.styleManager1.ThemeAuthor = "Narwin";
            this.styleManager1.ThemeName = "DarkUI";
            // 
            // metroSetLabel1
            // 
            this.metroSetLabel1.BackColor = System.Drawing.Color.Transparent;
            this.metroSetLabel1.Font = new System.Drawing.Font("Segoe WP Light", 10F);
            this.metroSetLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.metroSetLabel1.Location = new System.Drawing.Point(323, 158);
            this.metroSetLabel1.Name = "metroSetLabel1";
            this.metroSetLabel1.Size = new System.Drawing.Size(130, 19);
            this.metroSetLabel1.Style = MetroSet_UI.Design.Style.Custom;
            this.metroSetLabel1.StyleManager = this.styleManager1;
            this.metroSetLabel1.TabIndex = 1;
            this.metroSetLabel1.Text = "metroSetLabel1";
            this.metroSetLabel1.ThemeAuthor = "Narwin";
            this.metroSetLabel1.ThemeName = "DarkUI";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 388);
            this.Controls.Add(this.metroSetLabel1);
            this.Controls.Add(this.metroSetButton1);
            this.Font = new System.Drawing.Font("Segoe WP", 12F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.SmallRectThickness = 4;
            this.Style = MetroSet_UI.Design.Style.Custom;
            this.StyleManager = this.styleManager1;
            this.Text = "Metro UI";
            this.ThemeAuthor = "Narwin";
            this.ThemeName = "DarkUI";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroSet_UI.Controls.MetroSetLabel metroSetLabel1;
        private MetroSet_UI.Controls.MetroSetButton metroSetButton1;
        private MetroSet_UI.StyleManager styleManager1;
    }
}