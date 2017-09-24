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
            this.metroSetCheckBox1 = new MetroSet_UI.Controls.MetroSetCheckBox();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.CustomTheme = "C:\\Users\\Stick\\Desktop\\MetroSet Theme.xml";
            this.styleManager1.MetroForm = this;
            this.styleManager1.Style = MetroSet_UI.Design.Style.Custom;
            this.styleManager1.ThemeAuthor = "Narwin";
            this.styleManager1.ThemeName = "DarkUI";
            // 
            // metroSetCheckBox1
            // 
            this.metroSetCheckBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.metroSetCheckBox1.Checked = true;
            this.metroSetCheckBox1.CheckState = MetroSet_UI.Enums.CheckState.Checked;
            this.metroSetCheckBox1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.metroSetCheckBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.metroSetCheckBox1.Location = new System.Drawing.Point(329, 227);
            this.metroSetCheckBox1.Name = "metroSetCheckBox1";
            this.metroSetCheckBox1.SignStyle = MetroSet_UI.Enums.SignStyle.Sign;
            this.metroSetCheckBox1.Size = new System.Drawing.Size(199, 16);
            this.metroSetCheckBox1.Style = MetroSet_UI.Design.Style.Custom;
            this.metroSetCheckBox1.StyleManager = this.styleManager1;
            this.metroSetCheckBox1.TabIndex = 0;
            this.metroSetCheckBox1.Text = "metroSetCheckBox1";
            this.metroSetCheckBox1.ThemeAuthor = "Narwin";
            this.metroSetCheckBox1.ThemeName = "DarkUI";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 356);
            this.Controls.Add(this.metroSetCheckBox1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.Name = "Form1";
            this.SmallRectThickness = 2;
            this.Style = MetroSet_UI.Design.Style.Custom;
            this.StyleManager = this.styleManager1;
            this.Text = "Form1";
            this.ThemeAuthor = "Narwin";
            this.ThemeName = "DarkUI";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroSet_UI.StyleManager styleManager1;
        private MetroSet_UI.Controls.MetroSetCheckBox metroSetCheckBox1;
    }
}