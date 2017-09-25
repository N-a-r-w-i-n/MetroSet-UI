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
            this.metroSetSwitch1 = new MetroSet_UI.Controls.MetroSetSwitch();
            this.SuspendLayout();
            // 
            // metroSetButton1
            // 
            this.metroSetButton1.Font = new System.Drawing.Font("Segoe WP SemiLight", 10F);
            this.metroSetButton1.Location = new System.Drawing.Point(279, 182);
            this.metroSetButton1.Name = "metroSetButton1";
            this.metroSetButton1.Size = new System.Drawing.Size(188, 40);
            this.metroSetButton1.Style = MetroSet_UI.Design.Style.Light;
            this.metroSetButton1.StyleManager = this.styleManager1;
            this.metroSetButton1.TabIndex = 1;
            this.metroSetButton1.Text = "metroSetButton1";
            this.metroSetButton1.ThemeAuthor = "Narwin";
            this.metroSetButton1.ThemeName = "MetroLite";
            this.metroSetButton1.Click += new System.EventHandler(this.metroSetButton1_Click);
            // 
            // styleManager1
            // 
            this.styleManager1.CustomTheme = "C:\\Users\\Stick\\Desktop\\MetroSet Theme.xml";
            this.styleManager1.MetroForm = this;
            this.styleManager1.Style = MetroSet_UI.Design.Style.Light;
            this.styleManager1.ThemeAuthor = "Narwin";
            this.styleManager1.ThemeName = "MetroLite";
            // 
            // metroSetSwitch1
            // 
            this.metroSetSwitch1.BackColor = System.Drawing.Color.Transparent;
            this.metroSetSwitch1.CheckState = MetroSet_UI.Enums.CheckState.Unchecked;
            this.metroSetSwitch1.ForeColor = System.Drawing.Color.Black;
            this.metroSetSwitch1.Location = new System.Drawing.Point(488, 200);
            this.metroSetSwitch1.Name = "metroSetSwitch1";
            this.metroSetSwitch1.Size = new System.Drawing.Size(58, 22);
            this.metroSetSwitch1.Style = MetroSet_UI.Design.Style.Light;
            this.metroSetSwitch1.StyleManager = this.styleManager1;
            this.metroSetSwitch1.Switched = false;
            this.metroSetSwitch1.TabIndex = 2;
            this.metroSetSwitch1.Text = "metroSetSwitch1";
            this.metroSetSwitch1.ThemeAuthor = "Narwin";
            this.metroSetSwitch1.ThemeName = "MetroLite";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 415);
            this.Controls.Add(this.metroSetSwitch1);
            this.Controls.Add(this.metroSetButton1);
            this.Name = "Form1";
            this.SmallRectThickness = 2;
            this.StyleManager = this.styleManager1;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroSet_UI.Controls.MetroSetButton metroSetButton1;
        private MetroSet_UI.StyleManager styleManager1;
        private MetroSet_UI.Controls.MetroSetSwitch metroSetSwitch1;
    }
}