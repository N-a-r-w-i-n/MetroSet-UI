/**
 * MetroSet UI - MetroSet UI Framewrok
 *
 * The MIT License (MIT)
 * Copyright (c) 2011 Narwin, https://github.com/N-a-r-w-i-n
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in the
 * Software without restriction, including without limitation the rights to use, copy,
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the
 * following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using MetroSet_UI.Child;
using MetroSet_UI.Design;
using MetroSet_UI.Enums;
using MetroSet_UI.Extensions;
using MetroSet_UI.Interfaces;
using MetroSet_UI.Property;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetTabControl), "Bitmaps.TabControl.bmp")]
    [Designer(typeof(MetroSetTabControlDesigner))]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetTabControl : TabControl, iControl
    {
        #region Interfaces

        /// <summary>
        /// Gets or sets the style associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the style associated with the control.")]
        public Style Style
        {
            get
            {
                return StyleManager?.Style ?? style;
            }
            set
            {
                style = value;
                switch (value)
                {
                    case Style.Light:
                        ApplyTheme();
                        break;

                    case Style.Dark:
                        ApplyTheme(Style.Dark);
                        break;

                    case Style.Custom:
                        ApplyTheme(Style.Custom);
                        break;

                    default:
                        ApplyTheme();
                        break;
                }
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the Style Manager associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the Style Manager associated with the control.")]
        public StyleManager StyleManager
        {
            get { return _StyleManager; }
            set { _StyleManager = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the The Author name associated with the theme.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the The Author name associated with the theme.")]
        public string ThemeAuthor { get; set; }

        /// <summary>
        /// Gets or sets the The Theme name associated with the theme.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the The Theme name associated with the theme.")]
        public string ThemeName { get; set; }

        #endregion Interfaces

        #region Global Vars

        private static TabControlProperties prop;
        private Methods mth;
        private Utilites utl;

        #endregion Global Vars

        #region Internal Vars

        private Style style;
        private StyleManager _StyleManager;
        private TabStyle tabStyle;

        #endregion Internal Vars

        #region Constructors

        public MetroSetTabControl()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            tabStyle = TabStyle.Style1;
            ItemSize = new Size(100, 38);
            DrawMode = TabDrawMode.Normal;
            SizeMode = TabSizeMode.Fixed;
            Font = MetroSetFonts.UIRegular(8);
            prop = new TabControlProperties();
            mth = new Methods();
            utl = new Utilites();
            style = Style.Light;
            ApplyTheme();
        }

        #endregion Constructors

        #region ApplyTheme

        /// <summary>
        /// Gets or sets the style provided by the user.
        /// </summary>
        /// <param name="style">The Style.</param>
        internal void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    prop.ForeColor = Color.FromArgb(65, 177, 225);
                    prop.BackColor = Color.White;
                    prop.UnselectedTextColor = Color.Gray;
                    prop.SelectedTextColor = Color.White;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    SetProperties();
                    break;

                case Style.Dark:
                    prop.ForeColor = Color.FromArgb(65, 177, 225);
                    prop.BackColor = Color.FromArgb(30, 30, 30);
                    prop.UnselectedTextColor = Color.Gray;
                    prop.SelectedTextColor = Color.White;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark"; 
                    SetProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.TabControlDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "ForeColor":
                                    prop.ForeColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "BackColor":
                                    prop.BackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "UnselectedTextColor":
                                    prop.UnselectedTextColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "SelectedTextColor":
                                    prop.SelectedTextColor = utl.HexColor((string)varkey.Value);
                                    break;

                                default:
                                    return;
                            }
                        }
                    SetProperties();
                    break;
            }
        }

        public void SetProperties()
        {
            try
            {
                BaseColor = prop.BackColor;
                ForeroundColor = prop.ForeColor;
                UnselectedTextColor = prop.UnselectedTextColor;
                SelectedTextColor = prop.SelectedTextColor;
                InvalidateTabPage(BaseColor);
                Invalidate();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        #endregion ApplyTheme

        #region Properties

        [Category("MetroSet Framework")]
        [Editor(typeof(MetroSetTabPageCollectionEditor), typeof(UITypeEditor))]
        public new TabPageCollection TabPages
        {
            get { return base.TabPages; }
        }

        /// <summary>
        /// Gets or sets wether the tabcontrol use animation or not.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets wether the tabcontrol use animation or not.")]
        public bool UseAnimation { get; set; } = true;

        [Category("MetroSet Framework")]
        public new Size ItemSize
        {
            get { return base.ItemSize; }
            set
            {
                base.ItemSize = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the speed of transition.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the speed of transition.")] 
        public int Speed { get; set; } = 20;

        [Category("MetroSet Framework")]
        public override DockStyle Dock { get => base.Dock; set => base.Dock = value; }

        [Category("MetroSet Framework")]
        [Browsable(false)]
        public new TabSizeMode SizeMode { get; set; } = TabSizeMode.Fixed;

        [Category("MetroSet Framework")]
        [Browsable(false)]
        public new TabDrawMode DrawMode { get; set; } = TabDrawMode.Normal;

        [Category("MetroSet Framework")]
        [Browsable(false)]
        private Color BaseColor { get; set; }

        [Category("MetroSet Framework")]
        [Browsable(false)]
        private Color ForeroundColor { get; set; }

        [Category("MetroSet Framework")]
        [Browsable(false)]
        private Color UnselectedTextColor { get; set; }

        [Category("MetroSet Framework")]
        [Browsable(false)]
        private Color SelectedTextColor { get; set; }
        
        /// <summary>
        /// Gets or sets the tancontrol apperance style
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the tancontrol apperance style.")]
        public TabStyle TabStyle
        {
            get { return tabStyle; }
            set
            {
                tabStyle = value;
                Invalidate();
            }
        }

        #endregion Properties

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;

            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            G.Clear(BaseColor);                      

            var h = ItemSize.Height + 2;

            switch (TabStyle)
            {
                case TabStyle.Style1:
                    
                    using (Pen SB = new Pen(ForeroundColor, 2))
                    {
                        G.DrawLine(SB, 2, h, Width - 3, h);
                    }

                    for (int i = 0; i <= TabCount - 1; i++)
                    {
                        var r = GetTabRect(i);

                        if (i == SelectedIndex)
                        {
                            using (SolidBrush SB = new SolidBrush(ForeroundColor))
                            {
                                G.FillRectangle(SB, r);
                            }
                        }
                        using (SolidBrush TB = new SolidBrush(i == SelectedIndex ? SelectedTextColor : UnselectedTextColor))
                        {
                            G.DrawString(TabPages[i].Text, Font, TB, r, mth.SetPosition());
                        }
                    }
                    break;
                case TabStyle.Style2:

                    for (int i = 0; i <= TabCount - 1; i++)
                    {
                        var r = GetTabRect(i);

                        if (i == SelectedIndex)
                        {
                            using (Pen SB = new Pen(ForeroundColor, 2))
                            {
                                G.DrawLine(SB, r.X, r.Height, r.X + r.Width, r.Height);
                            }
                        }
                        using (SolidBrush TB = new SolidBrush(i == SelectedIndex ? SelectedTextColor : UnselectedTextColor))
                        {
                            G.DrawString(TabPages[i].Text, Font, TB, r, mth.SetPosition());
                        }
                    }
                    break;
            }

        }

        #endregion Draw Control

        #region Events

        #region Animation

        // Credits : Mavamaarten

        private int OldIndex;
                

        public void DoAnimationScrollLeft(Control Control1, Control Control2)
        {
            Graphics G = Control1.CreateGraphics();
            Bitmap P1 = new Bitmap(Control1.Width, Control1.Height);
            Bitmap P2 = new Bitmap(Control2.Width, Control2.Height);
            Control1.DrawToBitmap(P1, new Rectangle(0, 0, Control1.Width, Control1.Height));
            Control2.DrawToBitmap(P2, new Rectangle(0, 0, Control2.Width, Control2.Height));

            foreach (Control c in Control1.Controls)
            {
                c.Hide();
            }

            int Slide = Control1.Width - (Control1.Width % Speed);

            int a = 0;
            for (a = 0; a <= Slide; a += Speed)
            {
                G.DrawImage(P1, new Rectangle(a, 0, Control1.Width, Control1.Height));
                G.DrawImage(P2, new Rectangle(a - Control2.Width, 0, Control2.Width, Control2.Height));
            }
            a = Control1.Width;
            G.DrawImage(P1, new Rectangle(a, 0, Control1.Width, Control1.Height));
            G.DrawImage(P2, new Rectangle(a - Control2.Width, 0, Control2.Width, Control2.Height));

            SelectedTab = (TabPage)Control2;

            foreach (Control c in Control2.Controls)
            {
                c.Show();
            }

            foreach (Control c in Control1.Controls)
            {
                c.Show();
            }
        }

        protected override void OnSelecting(TabControlCancelEventArgs e)
        {
            if (UseAnimation)
            {
                if (OldIndex < e.TabPageIndex)
                {
                    DoAnimationScrollRight(TabPages[OldIndex], TabPages[e.TabPageIndex]);
                }
                else
                {
                    DoAnimationScrollLeft(TabPages[OldIndex], TabPages[e.TabPageIndex]);
                }
            }
        }

        protected override void OnDeselecting(TabControlCancelEventArgs e)
        {
            OldIndex = e.TabPageIndex;
        }

        public void DoAnimationScrollRight(Control Control1, Control Control2)
        {
            Graphics G = Control1.CreateGraphics();
            Bitmap P1 = new Bitmap(Control1.Width, Control1.Height);
            Bitmap P2 = new Bitmap(Control2.Width, Control2.Height);
            Control1.DrawToBitmap(P1, new Rectangle(0, 0, Control1.Width, Control1.Height));
            Control2.DrawToBitmap(P2, new Rectangle(0, 0, Control2.Width, Control2.Height));

            foreach (Control c in Control1.Controls)
            {
                c.Hide();
            }

            int Slide = Control1.Width - (Control1.Width % Speed);

            int a = 0;
            for (a = 0; a >= -Slide; a += -Speed)
            {
                G.DrawImage(P1, new Rectangle(a, 0, Control1.Width, Control1.Height));
                G.DrawImage(P2, new Rectangle(a + Control2.Width, 0, Control2.Width, Control2.Height));
            }
            a = Control1.Width;
            G.DrawImage(P1, new Rectangle(a, 0, Control1.Width, Control1.Height));
            G.DrawImage(P2, new Rectangle(a + Control2.Width, 0, Control2.Width, Control2.Height));

            SelectedTab = (TabPage)Control2;

            foreach (Control c in Control2.Controls)
            {
                c.Show();
            }

            foreach (Control c in Control1.Controls)
            {
                c.Show();
            }
        }

        #endregion Animation

        #endregion Events
        
        #region Methods

        private void InvalidateTabPage(Color C)
        {
            foreach (MetroSetTabPage T in TabPages)
            {
                T.Style = Style;
                T.BaseColor = BaseColor;
                T.Invalidate();
            }
        }

#endregion

    }
}