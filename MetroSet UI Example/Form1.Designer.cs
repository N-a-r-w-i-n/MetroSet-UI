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
            this.metroSetButton1 = new MetroSet_UI.Controls.MetroSetButton();
            this.metroSetComboBox1 = new MetroSet_UI.Controls.MetroSetComboBox();
            this.metroSetNumeric1 = new MetroSet_UI.Controls.MetroSetNumeric();
            this.metroSetTextBox1 = new MetroSet_UI.Controls.MetroSetTextBox();
            this.metroSetEllipse1 = new MetroSet_UI.Controls.MetroSetEllipse();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.CustomTheme = "C:\\Users\\Stick\\Desktop\\MetroSet Theme.xml";
            this.styleManager1.MetroForm = this;
            this.styleManager1.Style = MetroSet_UI.Design.Style.Light;
            this.styleManager1.ThemeAuthor = "Narwin";
            this.styleManager1.ThemeName = "MetroLite";
            // 
            // metroSetButton1
            // 
            this.metroSetButton1.Font = new System.Drawing.Font("Segoe WP Light", 10F);
            this.metroSetButton1.Location = new System.Drawing.Point(489, 104);
            this.metroSetButton1.Name = "metroSetButton1";
            this.metroSetButton1.Size = new System.Drawing.Size(193, 36);
            this.metroSetButton1.Style = MetroSet_UI.Design.Style.Light;
            this.metroSetButton1.StyleManager = this.styleManager1;
            this.metroSetButton1.TabIndex = 0;
            this.metroSetButton1.Text = "metroSetButton1";
            this.metroSetButton1.ThemeAuthor = "Narwin";
            this.metroSetButton1.ThemeName = "MetroLite";
            this.metroSetButton1.Click += new System.EventHandler(this.metroSetButton1_Click);
            // 
            // metroSetComboBox1
            // 
            this.metroSetComboBox1.AllowDrop = true;
            this.metroSetComboBox1.BackColor = System.Drawing.Color.Transparent;
            this.metroSetComboBox1.CausesValidation = false;
            this.metroSetComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.metroSetComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.metroSetComboBox1.Font = new System.Drawing.Font("Segoe WP SemiLight", 11F);
            this.metroSetComboBox1.FormattingEnabled = true;
            this.metroSetComboBox1.ItemHeight = 20;
            this.metroSetComboBox1.Location = new System.Drawing.Point(489, 203);
            this.metroSetComboBox1.Name = "metroSetComboBox1";
            this.metroSetComboBox1.Size = new System.Drawing.Size(193, 26);
            this.metroSetComboBox1.Style = MetroSet_UI.Design.Style.Light;
            this.metroSetComboBox1.StyleManager = this.styleManager1;
            this.metroSetComboBox1.TabIndex = 3;
            this.metroSetComboBox1.ThemeAuthor = "Narwin";
            this.metroSetComboBox1.ThemeName = "MetroLite";
            // 
            // metroSetNumeric1
            // 
            this.metroSetNumeric1.BackColor = System.Drawing.Color.Transparent;
            this.metroSetNumeric1.Font = new System.Drawing.Font("Segoe WP Semibold", 10F);
            this.metroSetNumeric1.Location = new System.Drawing.Point(489, 247);
            this.metroSetNumeric1.Maximum = 100;
            this.metroSetNumeric1.Minimum = 0;
            this.metroSetNumeric1.Name = "metroSetNumeric1";
            this.metroSetNumeric1.Size = new System.Drawing.Size(193, 26);
            this.metroSetNumeric1.Style = MetroSet_UI.Design.Style.Light;
            this.metroSetNumeric1.StyleManager = this.styleManager1;
            this.metroSetNumeric1.TabIndex = 4;
            this.metroSetNumeric1.Text = "metroSetNumeric1";
            this.metroSetNumeric1.ThemeAuthor = "Narwin";
            this.metroSetNumeric1.ThemeName = "MetroLite";
            this.metroSetNumeric1.Value = 0;
            // 
            // metroSetTextBox1
            // 
            this.metroSetTextBox1.AutoCompleteCustomSource = null;
            this.metroSetTextBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.metroSetTextBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.metroSetTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.metroSetTextBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.metroSetTextBox1.Font = new System.Drawing.Font("Segoe WP Light", 10F);
            this.metroSetTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.metroSetTextBox1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.metroSetTextBox1.Image = null;
            this.metroSetTextBox1.Lines = null;
            this.metroSetTextBox1.Location = new System.Drawing.Point(489, 156);
            this.metroSetTextBox1.MaxLength = 32767;
            this.metroSetTextBox1.Multiline = false;
            this.metroSetTextBox1.Name = "metroSetTextBox1";
            this.metroSetTextBox1.ReadOnly = false;
            this.metroSetTextBox1.Size = new System.Drawing.Size(193, 28);
            this.metroSetTextBox1.Style = MetroSet_UI.Design.Style.Light;
            this.metroSetTextBox1.StyleManager = this.styleManager1;
            this.metroSetTextBox1.TabIndex = 5;
            this.metroSetTextBox1.Text = "metroSetTextBox1";
            this.metroSetTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.metroSetTextBox1.ThemeAuthor = "Narwin";
            this.metroSetTextBox1.ThemeName = "MetroLite";
            this.metroSetTextBox1.UseSystemPasswordChar = false;
            this.metroSetTextBox1.WatermarkText = "";
            // 
            // metroSetEllipse1
            // 
            this.metroSetEllipse1.BorderThickness = 7;
            this.metroSetEllipse1.Font = new System.Drawing.Font("Segoe WP Semibold", 10F);
            this.metroSetEllipse1.Location = new System.Drawing.Point(295, 104);
            this.metroSetEllipse1.Name = "metroSetEllipse1";
            this.metroSetEllipse1.Size = new System.Drawing.Size(164, 140);
            this.metroSetEllipse1.Style = MetroSet_UI.Design.Style.Light;
            this.metroSetEllipse1.StyleManager = this.styleManager1;
            this.metroSetEllipse1.TabIndex = 6;
            this.metroSetEllipse1.Text = "Metro";
            this.metroSetEllipse1.ThemeAuthor = "Narwin";
            this.metroSetEllipse1.ThemeName = "MetroLite";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 353);
            this.Controls.Add(this.metroSetEllipse1);
            this.Controls.Add(this.metroSetTextBox1);
            this.Controls.Add(this.metroSetNumeric1);
            this.Controls.Add(this.metroSetComboBox1);
            this.Controls.Add(this.metroSetButton1);
            this.Name = "Form1";
            this.SmallRectThickness = 2;
            this.StyleManager = this.styleManager1;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroSet_UI.StyleManager styleManager1;
        private MetroSet_UI.Controls.MetroSetButton metroSetButton1;
        private MetroSet_UI.Controls.MetroSetComboBox metroSetComboBox1;
        private MetroSet_UI.Controls.MetroSetNumeric metroSetNumeric1;
        private MetroSet_UI.Controls.MetroSetTextBox metroSetTextBox1;
        private MetroSet_UI.Controls.MetroSetEllipse metroSetEllipse1;
    }
}