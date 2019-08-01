/**
 * MetroSet UI - MetroSet UI Framewrok
 *
 * The MIT License (MIT)
 * Copyright (c) 2017 Narwin, https://github.com/N-a-r-w-i-n
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

using MetroSet_UI.Animates;
using MetroSet_UI.Child;
using MetroSet_UI.Design;
using MetroSet_UI.Enums;
using MetroSet_UI.Extensions;
using MetroSet_UI.Interfaces;
using MetroSet_UI.Native;
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
            get => StyleManager?.Style ?? _style;
            set
            {
                _style = value;
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
            get => _styleManager;
            set { _styleManager = value; Invalidate(); }
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

        private readonly Methods _mth;
        private readonly Utilites _utl;

        #endregion Global Vars

        #region Internal Vars

        private Style _style;
        private StyleManager _styleManager;
        private PointFAnimate _slideAnimator;
        private Graphics _slideGraphics;
        private Bitmap _slideBitmap;

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
            UpdateStyles();
            ItemSize = new Size(100, 38);
            Font = MetroSetFonts.UIRegular(8);
            _mth = new Methods();
            _utl = new Utilites();
            _slideAnimator = new PointFAnimate();
            ApplyTheme();
        }

        #endregion Constructors

        #region ApplyTheme

        /// <summary>
        /// Gets or sets the style provided by the user.
        /// </summary>
        /// <param name="style">The Style.</param>
        private void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    ForeroundColor = Color.FromArgb(65, 177, 225);
                    BackgroungColor = Color.White;
                    UnselectedTextColor = Color.Gray;
                    SelectedTextColor = Color.White;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;

                case Style.Dark:
                    ForeroundColor = Color.FromArgb(65, 177, 225);
                    BackgroungColor = Color.FromArgb(30, 30, 30);
                    UnselectedTextColor = Color.Gray;
                    SelectedTextColor = Color.White;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.TabControlDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "ForeColor":
                                    ForeroundColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "BackColor":
                                    BackgroungColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "UnselectedTextColor":
                                    UnselectedTextColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "SelectedTextColor":
                                    SelectedTextColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                default:
                                    return;
                            }
                        }
                    UpdateProperties();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
        }

        private void UpdateProperties()
        {
            try
            {
                InvalidateTabPage(BackgroungColor);
                Invalidate();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        #endregion ApplyTheme

        #region Properties

        /// <summary>
        /// Get or set slide animate time(ms).
        /// </summary>
        [Category("MetroSet Framework"), Description("Get or set slide animate time(ms).")]
        public int AnimateTime
        {
            get;
            set;
        } = 200;
        /// <summary>
        /// Get or set slide animate easing type
        /// </summary>
        [Category("MetroSet Framework"), Description("Get or set slide animate easing type")]
        public EasingType AnimateEasingType
        {
            get;
            set;
        } = EasingType.CubeOut;

        /// <summary>
        /// Gets the collection of tab pages in this tab control.
        /// </summary>
        [Category("MetroSet Framework")]
        [Editor(typeof(MetroSetTabPageCollectionEditor), typeof(UITypeEditor))]
        public new TabPageCollection TabPages => base.TabPages;

        /// <summary>
        /// Gets or sets wether the tabcontrol use animation or not.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets wether the tabcontrol use animation or not.")]
        public bool UseAnimation { get; set; } = true;

        /// <summary>
        /// Gets or sets the size of the control's tabs.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the size of the control's tabs.")]
        public new Size ItemSize
        {
            get => base.ItemSize;
            set
            {
                base.ItemSize = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the font used when displaying text in the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the font used when displaying text in the control.")]
        public new Font Font { get; set; }

        /// <summary>
        /// Gets or sets the area of the control (for example, along the top) where the tabs are aligned.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the area of the control (for example, along the top) where the tabs are aligned.")]
        public new TabAlignment Alignment => TabAlignment.Top;

        /// <summary>
        /// Gets or sets the speed of transition.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the speed of transition.")]
        public int Speed { get; set; } = 20;

        /// <summary>
        /// Gets or sets which control borders are docked to its parent control and determines how a control is resized with its parent.
        /// </summary>
        [Category("MetroSet Framework")]
        public override DockStyle Dock
        {
            get => base.Dock; set => base.Dock = value;
        }

        /// <summary>
        /// Gets or sets the way that the control's tabs are sized.
        /// </summary>
        [Category("MetroSet Framework")]
        [Browsable(false)]
        public new TabSizeMode SizeMode { get; set; } = TabSizeMode.Fixed;

        /// <summary>
        /// Gets or sets the way that the control's tabs are drawn.
        /// </summary>
        [Category("MetroSet Framework")]
        [Browsable(false)]
        public new TabDrawMode DrawMode { get; set; } = TabDrawMode.Normal;

        /// <summary>
        /// Gets or sets the backgorund color.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the backgorund color.")]
        private Color BackgroungColor { get; set; }

        /// <summary>
        /// Gets or sets the foregorund color.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the foregorund color.")]
        private Color ForeroundColor { get; set; }

        /// <summary>
        /// Gets or sets the tabpage text while un-selected.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the tabpage text while un-selected.")]
        private Color UnselectedTextColor { get; set; }

        /// <summary>
        /// Gets or sets the tabpage text while selected.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the tabpage text while selected.")]
        private Color SelectedTextColor { get; set; }

        /// <summary>
        /// Gets or sets the tancontrol apperance style
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the tancontrol apperance style.")]
        public TabStyle TabStyle { get; set; } = TabStyle.Style1;

        #endregion Properties

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;

            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            G.Clear(BackgroungColor);

            var h = ItemSize.Height + 2;

            switch (TabStyle)
            {
                case TabStyle.Style1:

                    using (var sb = new Pen(ForeroundColor, 2))
                    {
                        G.DrawLine(sb, 2, h, Width - 3, h);
                    }

                    for (var i = 0; i <= TabCount - 1; i++)
                    {
                        var r = GetTabRect(i);

                        if (i == SelectedIndex)
                        {
                            using (var sb = new SolidBrush(ForeroundColor))
                            {
                                G.FillRectangle(sb, r);
                            }
                        }
                        using (var tb = new SolidBrush(i == SelectedIndex ? SelectedTextColor : UnselectedTextColor))

                        {
                            G.DrawString(TabPages[i].Text, Font, tb, r, _mth.SetPosition());
                        }
                    }
                    break;
                case TabStyle.Style2:

                    for (var i = 0; i <= TabCount - 1; i++)
                    {
                        var r = GetTabRect(i);

                        if (i == SelectedIndex)
                        {
                            using (var sb = new Pen(ForeroundColor, 2))
                            {
                                G.DrawLine(sb, r.X, r.Height, r.X + r.Width, r.Height);
                            }
                        }
                        using (var tb = new SolidBrush(UnselectedTextColor))
                        {
                            G.DrawString(TabPages[i].Text, Font, tb, r, _mth.SetPosition());
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        #endregion Draw Control

        #region Events

        /// <summary>
        /// Handling mouse move event of the cotnrol, chnaging the cursor to hande whenever mouse located in a tab page.
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            for (var i = 0; i <= TabCount - 1; i++)
            {
                var r = GetTabRect(i);
                if (!r.Contains(e.Location)) continue;
                Cursor = Cursors.Hand;
                Invalidate();
            }
        }

        /// <summary>
        /// Handling mouse leave event and releasing hand cursor.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Cursor = Cursors.Default;
            Invalidate();
        }

        /// <summary>
        /// Here we set the smooth mouse hand.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == User32.WM_SETCURSOR)
            {
                User32.SetCursor(User32.LoadCursor(IntPtr.Zero, User32.IDC_HAND));
                m.Result = IntPtr.Zero;
                return;
            }

            base.WndProc(ref m);
        }

        #region Animation

        // Credits : Mavamaarten

        private int _oldIndex;

        private void DoSlideAnimate(TabPage control1, TabPage control2, bool moveback)
        {
            // initialize control and child controls when control first painted
            _utl.InitControlHandle(control1);
            _utl.InitControlHandle(control2);
            _slideGraphics = Graphics.FromHwnd(control2.Handle);
            _slideBitmap = new Bitmap(control1.Width + control2.Width, control1.Height + control2.Height);

            if (moveback)
            {
                control2.DrawToBitmap(_slideBitmap, new Rectangle(0, 0, control2.Width, control2.Height));
                control1.DrawToBitmap(_slideBitmap, new Rectangle(control2.Width, 0, control1.Width, control1.Height));
            }
            else
            {
                control1.DrawToBitmap(_slideBitmap, new Rectangle(0, 0, control1.Width, control1.Height));
                control2.DrawToBitmap(_slideBitmap, new Rectangle(control1.Width, 0, control2.Width, control2.Height));
            }

            foreach (Control c in control2.Controls)
            {
                c.Hide();
            }
            
            _slideAnimator.Update = (alpha) =>
            {
                _slideGraphics.DrawImage(_slideBitmap, alpha);
            };
            _slideAnimator.Complete = () =>
            {
                SelectedTab = control2;
                foreach (Control c in control2.Controls)
                {
                    c.Show();
                }
            };
            _slideAnimator.Start(
                AnimateTime,
                new Point(moveback ? -control2.Width : 0, 0),
                new Point(moveback ? 0 : -control1.Width, 0),
                AnimateEasingType
            );
        }

        protected override void OnSelecting(TabControlCancelEventArgs e)
        {
            if (!UseAnimation) return;
            if (_slideAnimator.Active)
            {
                e.Cancel = true;
                return;
            }
            DoSlideAnimate(TabPages[_oldIndex], TabPages[e.TabPageIndex], _oldIndex > e.TabPageIndex);
        }

        protected override void OnDeselecting(TabControlCancelEventArgs e)
        {
            _oldIndex = e.TabPageIndex;
        }

        private void DoAnimationScrollRight(Control control1, Control control2)
        {
            var G = control1.CreateGraphics();
            var p1 = new Bitmap(control1.Width, control1.Height);
            var p2 = new Bitmap(control2.Width, control2.Height);
            control1.DrawToBitmap(p1, new Rectangle(0, 0, control1.Width, control1.Height));
            control2.DrawToBitmap(p2, new Rectangle(0, 0, control2.Width, control2.Height));

            foreach (Control c in control1.Controls)
            {
                c.Hide();
            }

            var slide = control1.Width - (control1.Width % Speed);

            int a;
            for (a = 0; a >= -slide; a += -Speed)
            {
                G.DrawImage(p1, new Rectangle(a, 0, control1.Width, control1.Height));
                G.DrawImage(p2, new Rectangle(a + control2.Width, 0, control2.Width, control2.Height));
            }
            a = control1.Width;
            G.DrawImage(p1, new Rectangle(a, 0, control1.Width, control1.Height));
            G.DrawImage(p2, new Rectangle(a + control2.Width, 0, control2.Width, control2.Height));

            SelectedTab = (TabPage)control2;

            foreach (Control c in control2.Controls)
            {
                c.Show();
            }

            foreach (Control c in control1.Controls)
            {
                c.Show();
            }
        }

        #endregion Animation

        #endregion Events

        #region Methods

        /// <summary>
        /// The Method that provide the specific color for every single tab page in the tab control.
        /// </summary>
        /// <param name="c"></param>
        private void InvalidateTabPage(Color c)
        {
            foreach (MetroSetTabPage T in TabPages)
            {
                T.Style = Style;
                T.BaseColor = c;
                T.Invalidate();
            }
        }

        #endregion

    }
}