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
            this.styleManager1 = new MetroSet_UI.StyleManager();
            this.metroSetControlBox1 = new MetroSet_UI.Controls.MetroSetControlBox();
            this.metroSetTabControl1 = new MetroSet_UI.Controls.MetroSetTabControl();
            this.metroSetTabPage1 = new MetroSet_UI.Child.MetroSetTabPage();
            this.metroSetPanel1 = new MetroSet_UI.Controls.MetroSetPanel();
            this.metroSetRadioButton1 = new MetroSet_UI.Controls.MetroSetRadioButton();
            this.metroSetCheckBox1 = new MetroSet_UI.Controls.MetroSetCheckBox();
            this.metroSetSwitch1 = new MetroSet_UI.Controls.MetroSetSwitch();
            this.metroSetButton1 = new MetroSet_UI.Controls.MetroSetButton();
            this.metroSetTabPage2 = new MetroSet_UI.Child.MetroSetTabPage();
            this.metroSetTabPage3 = new MetroSet_UI.Child.MetroSetTabPage();
            this.metroSetTabPage4 = new MetroSet_UI.Child.MetroSetTabPage();
            this.metroSetTabControl1.SuspendLayout();
            this.metroSetTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.CustomTheme = "C:\\Users\\Stick\\Desktop\\MetroSet Theme.xml";
            this.styleManager1.MetroForm = this;
            this.styleManager1.Style = MetroSet_UI.Design.Style.Dark;
            this.styleManager1.ThemeAuthor = "Narwin";
            this.styleManager1.ThemeName = "MetroDark";
            // 
            // metroSetControlBox1
            // 
            this.metroSetControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroSetControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.metroSetControlBox1.Location = new System.Drawing.Point(734, 11);
            this.metroSetControlBox1.MaximizeBox = true;
            this.metroSetControlBox1.MinimizeBox = true;
            this.metroSetControlBox1.Name = "metroSetControlBox1";
            this.metroSetControlBox1.Size = new System.Drawing.Size(100, 25);
            this.metroSetControlBox1.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetControlBox1.StyleManager = this.styleManager1;
            this.metroSetControlBox1.TabIndex = 0;
            this.metroSetControlBox1.Text = "metroSetControlBox1";
            this.metroSetControlBox1.ThemeAuthor = "Narwin";
            this.metroSetControlBox1.ThemeName = "MetroDark";
            // 
            // metroSetTabControl1
            // 
            this.metroSetTabControl1.Controls.Add(this.metroSetTabPage1);
            this.metroSetTabControl1.Controls.Add(this.metroSetTabPage2);
            this.metroSetTabControl1.Controls.Add(this.metroSetTabPage3);
            this.metroSetTabControl1.Controls.Add(this.metroSetTabPage4);
            this.metroSetTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroSetTabControl1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.metroSetTabControl1.ItemSize = new System.Drawing.Size(100, 38);
            this.metroSetTabControl1.Location = new System.Drawing.Point(12, 70);
            this.metroSetTabControl1.Name = "metroSetTabControl1";
            this.metroSetTabControl1.SelectedIndex = 0;
            this.metroSetTabControl1.Size = new System.Drawing.Size(822, 452);
            this.metroSetTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.metroSetTabControl1.Speed = 20;
            this.metroSetTabControl1.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetTabControl1.StyleManager = this.styleManager1;
            this.metroSetTabControl1.TabIndex = 1;
            this.metroSetTabControl1.TabStyle = MetroSet_UI.Enums.TabStyle.Style2;
            this.metroSetTabControl1.ThemeAuthor = "Narwin";
            this.metroSetTabControl1.ThemeName = "MetroDark";
            this.metroSetTabControl1.UseAnimation = true;
            // 
            // metroSetTabPage1
            // 
            this.metroSetTabPage1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.metroSetTabPage1.Controls.Add(this.metroSetPanel1);
            this.metroSetTabPage1.Controls.Add(this.metroSetRadioButton1);
            this.metroSetTabPage1.Controls.Add(this.metroSetCheckBox1);
            this.metroSetTabPage1.Controls.Add(this.metroSetSwitch1);
            this.metroSetTabPage1.Controls.Add(this.metroSetButton1);
            this.metroSetTabPage1.ImageIndex = 0;
            this.metroSetTabPage1.ImageKey = null;
            this.metroSetTabPage1.Location = new System.Drawing.Point(4, 42);
            this.metroSetTabPage1.Name = "metroSetTabPage1";
            this.metroSetTabPage1.Size = new System.Drawing.Size(814, 406);
            this.metroSetTabPage1.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetTabPage1.StyleManager = this.styleManager1;
            this.metroSetTabPage1.TabIndex = 0;
            this.metroSetTabPage1.Text = "Button Base";
            this.metroSetTabPage1.ThemeAuthor = "Narwin";
            this.metroSetTabPage1.ThemeName = "MetroDark";
            this.metroSetTabPage1.ToolTipText = null;
            // 
            // metroSetPanel1
            // 
            this.metroSetPanel1.BorderThickness = 1;
            this.metroSetPanel1.Location = new System.Drawing.Point(191, 56);
            this.metroSetPanel1.Name = "metroSetPanel1";
            this.metroSetPanel1.Size = new System.Drawing.Size(200, 100);
            this.metroSetPanel1.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetPanel1.StyleManager = this.styleManager1;
            this.metroSetPanel1.TabIndex = 7;
            this.metroSetPanel1.ThemeAuthor = "Narwin";
            this.metroSetPanel1.ThemeName = "MetroDark";
            // 
            // metroSetRadioButton1
            // 
            this.metroSetRadioButton1.BackColor = System.Drawing.Color.Transparent;
            this.metroSetRadioButton1.Checked = true;
            this.metroSetRadioButton1.CheckState = MetroSet_UI.Enums.CheckState.Checked;
            this.metroSetRadioButton1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.metroSetRadioButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.metroSetRadioButton1.Group = 1;
            this.metroSetRadioButton1.Location = new System.Drawing.Point(407, 120);
            this.metroSetRadioButton1.Name = "metroSetRadioButton1";
            this.metroSetRadioButton1.Size = new System.Drawing.Size(155, 17);
            this.metroSetRadioButton1.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetRadioButton1.StyleManager = this.styleManager1;
            this.metroSetRadioButton1.TabIndex = 6;
            this.metroSetRadioButton1.Text = "metroSetRadioButton1";
            this.metroSetRadioButton1.ThemeAuthor = "Narwin";
            this.metroSetRadioButton1.ThemeName = "MetroDark";
            // 
            // metroSetCheckBox1
            // 
            this.metroSetCheckBox1.BackColor = System.Drawing.Color.Transparent;
            this.metroSetCheckBox1.Checked = true;
            this.metroSetCheckBox1.CheckState = MetroSet_UI.Enums.CheckState.Checked;
            this.metroSetCheckBox1.Font = new System.Drawing.Font("Segoe WP Semibold", 10F);
            this.metroSetCheckBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.metroSetCheckBox1.Location = new System.Drawing.Point(407, 98);
            this.metroSetCheckBox1.Name = "metroSetCheckBox1";
            this.metroSetCheckBox1.SignStyle = MetroSet_UI.Enums.SignStyle.Sign;
            this.metroSetCheckBox1.Size = new System.Drawing.Size(155, 16);
            this.metroSetCheckBox1.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetCheckBox1.StyleManager = this.styleManager1;
            this.metroSetCheckBox1.TabIndex = 5;
            this.metroSetCheckBox1.Text = "metroSetCheckBox1";
            this.metroSetCheckBox1.ThemeAuthor = "Narwin";
            this.metroSetCheckBox1.ThemeName = "MetroDark";
            // 
            // metroSetSwitch1
            // 
            this.metroSetSwitch1.BackColor = System.Drawing.Color.Transparent;
            this.metroSetSwitch1.CheckState = MetroSet_UI.Enums.CheckState.Checked;
            this.metroSetSwitch1.Location = new System.Drawing.Point(407, 70);
            this.metroSetSwitch1.Name = "metroSetSwitch1";
            this.metroSetSwitch1.Size = new System.Drawing.Size(58, 22);
            this.metroSetSwitch1.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetSwitch1.StyleManager = this.styleManager1;
            this.metroSetSwitch1.Switched = true;
            this.metroSetSwitch1.TabIndex = 4;
            this.metroSetSwitch1.Text = "metroSetSwitch1";
            this.metroSetSwitch1.ThemeAuthor = "Narwin";
            this.metroSetSwitch1.ThemeName = "MetroDark";
            // 
            // metroSetButton1
            // 
            this.metroSetButton1.Font = new System.Drawing.Font("Segoe WP Light", 10F);
            this.metroSetButton1.Location = new System.Drawing.Point(3, 24);
            this.metroSetButton1.Name = "metroSetButton1";
            this.metroSetButton1.Size = new System.Drawing.Size(151, 37);
            this.metroSetButton1.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetButton1.StyleManager = this.styleManager1;
            this.metroSetButton1.TabIndex = 2;
            this.metroSetButton1.Text = "metroSetButton1";
            this.metroSetButton1.ThemeAuthor = "Narwin";
            this.metroSetButton1.ThemeName = "MetroDark";
            this.metroSetButton1.Click += new System.EventHandler(this.metroSetButton1_Click_1);
            // 
            // metroSetTabPage2
            // 
            this.metroSetTabPage2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.metroSetTabPage2.ImageIndex = 0;
            this.metroSetTabPage2.ImageKey = null;
            this.metroSetTabPage2.Location = new System.Drawing.Point(4, 42);
            this.metroSetTabPage2.Name = "metroSetTabPage2";
            this.metroSetTabPage2.Size = new System.Drawing.Size(814, 406);
            this.metroSetTabPage2.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetTabPage2.StyleManager = this.styleManager1;
            this.metroSetTabPage2.TabIndex = 1;
            this.metroSetTabPage2.Text = "metroSetTabPage2";
            this.metroSetTabPage2.ThemeAuthor = "Narwin";
            this.metroSetTabPage2.ThemeName = "MetroDark";
            this.metroSetTabPage2.ToolTipText = null;
            // 
            // metroSetTabPage3
            // 
            this.metroSetTabPage3.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.metroSetTabPage3.ImageIndex = 0;
            this.metroSetTabPage3.ImageKey = null;
            this.metroSetTabPage3.Location = new System.Drawing.Point(4, 42);
            this.metroSetTabPage3.Name = "metroSetTabPage3";
            this.metroSetTabPage3.Size = new System.Drawing.Size(814, 406);
            this.metroSetTabPage3.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetTabPage3.StyleManager = this.styleManager1;
            this.metroSetTabPage3.TabIndex = 2;
            this.metroSetTabPage3.Text = "metroSetTabPage3";
            this.metroSetTabPage3.ThemeAuthor = "Narwin";
            this.metroSetTabPage3.ThemeName = "MetroDark";
            this.metroSetTabPage3.ToolTipText = null;
            // 
            // metroSetTabPage4
            // 
            this.metroSetTabPage4.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.metroSetTabPage4.ImageIndex = 0;
            this.metroSetTabPage4.ImageKey = null;
            this.metroSetTabPage4.Location = new System.Drawing.Point(4, 42);
            this.metroSetTabPage4.Name = "metroSetTabPage4";
            this.metroSetTabPage4.Size = new System.Drawing.Size(814, 406);
            this.metroSetTabPage4.Style = MetroSet_UI.Design.Style.Dark;
            this.metroSetTabPage4.StyleManager = this.styleManager1;
            this.metroSetTabPage4.TabIndex = 3;
            this.metroSetTabPage4.Text = "metroSetTabPage4";
            this.metroSetTabPage4.ThemeAuthor = "Narwin";
            this.metroSetTabPage4.ThemeName = "MetroDark";
            this.metroSetTabPage4.ToolTipText = null;
            // 
            // MetroSetForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageTransparency = 0.08F;
            this.ClientSize = new System.Drawing.Size(846, 534);
            this.Controls.Add(this.metroSetControlBox1);
            this.Controls.Add(this.metroSetTabControl1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "MetroSetForm1";
            this.SmallRectThickness = 2;
            this.Style = MetroSet_UI.Design.Style.Dark;
            this.StyleManager = this.styleManager1;
            this.Text = "METROSETFORM1";
            this.ThemeName = "MetroDark";
            this.metroSetTabControl1.ResumeLayout(false);
            this.metroSetTabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroSet_UI.StyleManager styleManager1;
        private MetroSet_UI.Controls.MetroSetTabControl metroSetTabControl1;
        private MetroSet_UI.Child.MetroSetTabPage metroSetTabPage1;
        private MetroSet_UI.Child.MetroSetTabPage metroSetTabPage2;
        private MetroSet_UI.Child.MetroSetTabPage metroSetTabPage3;
        private MetroSet_UI.Child.MetroSetTabPage metroSetTabPage4;
        private MetroSet_UI.Controls.MetroSetControlBox metroSetControlBox1;
        private MetroSet_UI.Controls.MetroSetButton metroSetButton1;
        private MetroSet_UI.Controls.MetroSetRadioButton metroSetRadioButton1;
        private MetroSet_UI.Controls.MetroSetCheckBox metroSetCheckBox1;
        private MetroSet_UI.Controls.MetroSetSwitch metroSetSwitch1;
        private MetroSet_UI.Controls.MetroSetPanel metroSetPanel1;
    }
}