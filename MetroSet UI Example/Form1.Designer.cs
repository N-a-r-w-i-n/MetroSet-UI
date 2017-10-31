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
            this.SuspendLayout();
            // 
            // metroSetButton1
            // 
            this.metroSetButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.metroSetButton1.Location = new System.Drawing.Point(36, 267);
            this.metroSetButton1.Name = "metroSetButton1";
            this.metroSetButton1.Size = new System.Drawing.Size(200, 33);
            this.metroSetButton1.Style = MetroSet_UI.Design.Style.Light;
            this.metroSetButton1.StyleManager = this.styleManager1;
            this.metroSetButton1.TabIndex = 1;
            this.metroSetButton1.Text = "metroSetButton1";
            this.metroSetButton1.ThemeAuthor = null;
            this.metroSetButton1.ThemeName = null;
            this.metroSetButton1.Click += new System.EventHandler(this.MetroSetButton1_Click);
            // 
            // styleManager1
            // 
            this.styleManager1.CustomTheme = "C:\\Users\\Halloween\\Desktop\\MetroSet Theme.xml";
            this.styleManager1.MetroForm = this;
            this.styleManager1.Style = MetroSet_UI.Design.Style.Light;
            this.styleManager1.ThemeAuthor = null;
            this.styleManager1.ThemeName = null;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 449);
            this.Controls.Add(this.metroSetButton1);
            this.Name = "Form1";
            this.StyleManager = this.styleManager1;
            this.Text = "Form1";
            this.ThemeAuthor = null;
            this.ThemeName = null;
            this.ResumeLayout(false);

        }

        #endregion
        private MetroSet_UI.Controls.MetroSetButton metroSetButton1;
        private MetroSet_UI.StyleManager styleManager1;
    }
}