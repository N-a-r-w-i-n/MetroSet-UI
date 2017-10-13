namespace MetroSet_UI_Example
{
    partial class MetroSetForm1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MetroSetForm1));
            this.metroSetButton1 = new MetroSet_UI.Controls.MetroSetButton();
            this.styleManager1 = new MetroSet_UI.StyleManager();
            this.SuspendLayout();
            // 
            // metroSetButton1
            // 
            this.metroSetButton1.Font = new System.Drawing.Font("Segoe WP SemiLight", 10F);
            this.metroSetButton1.Location = new System.Drawing.Point(520, 113);
            this.metroSetButton1.Name = "metroSetButton1";
            this.metroSetButton1.Size = new System.Drawing.Size(163, 39);
            this.metroSetButton1.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetButton1.StyleManager = this.styleManager1;
            this.metroSetButton1.TabIndex = 0;
            this.metroSetButton1.Text = "metroSetButton1";
            this.metroSetButton1.ThemeAuthor = "Narwin";
            this.metroSetButton1.ThemeName = "MetroDark";
            this.metroSetButton1.Click += new System.EventHandler(this.metroSetButton1_Click);
            // 
            // styleManager1
            // 
            this.styleManager1.CustomTheme = "C:\\Users\\Stick\\Desktop\\MetroSet Theme.xml";
            this.styleManager1.MetroForm = this;
            this.styleManager1.Style = MetroSet_UI.Design.Style.Dark;
            this.styleManager1.ThemeAuthor = "Narwin";
            this.styleManager1.ThemeName = "MetroDark";
            // 
            // MetroSetForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageTransparency = 0.08F;
            this.ClientSize = new System.Drawing.Size(785, 475);
            this.Controls.Add(this.metroSetButton1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "MetroSetForm1";
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.Text = "MetroSetForm1";
            this.ThemeName = "MetroDark";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroSet_UI.Controls.MetroSetButton metroSetButton1;
        private MetroSet_UI.StyleManager styleManager1;
    }
}