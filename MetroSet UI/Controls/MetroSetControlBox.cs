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

using MetroSet_UI.Design;
using MetroSet_UI.Extensions;
using MetroSet_UI.Interfaces;
using MetroSet_UI.Property;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetControlBox), "Bitmaps.ControlButton.bmp")]
    [Designer(typeof(MetroSetControBoxDesigner))]
    [DefaultProperty("Click")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetControlBox : Control, iControl
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

        private static ControlBoxProperties prop;
        private Methods mth;
        private Utilites utl;

        #endregion Global Vars

        #region Internal Vars

        private Style style;
        private StyleManager _StyleManager;

        #endregion Internal Vars

        #region Constructors

        public MetroSetControlBox()
        {
            SetStyle(
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            BackColor = Color.Transparent;
            prop = new ControlBoxProperties();
            mth = new Methods();
            utl = new Utilites();
            Anchor = AnchorStyles.Top | AnchorStyles.Right;
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
                    prop.CloseHoverBackColor = Color.FromArgb(183, 40, 40);
                    prop.CloseHoverForeColor = Color.White;
                    prop.CloseNormalForeColor = Color.Gray;
                    prop.MaximizeHoverBackColor = Color.FromArgb(238, 238, 238);
                    prop.MaximizeHoverForeColor = Color.Gray;
                    prop.MaximizeNormalForeColor = Color.Gray;
                    prop.MinimizeHoverBackColor = Color.FromArgb(238, 238, 238);
                    prop.MinimizeHoverForeColor = Color.Gray;
                    prop.MinimizeNormalForeColor = Color.Gray;
                    prop.DisabledForeColor = Color.DimGray;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    break;

                case Style.Dark:
                    prop.CloseHoverBackColor = Color.FromArgb(183, 40, 40);
                    prop.CloseHoverForeColor = Color.White;
                    prop.CloseNormalForeColor = Color.Gray;
                    prop.MaximizeHoverBackColor = Color.FromArgb(238, 238, 238);
                    prop.MaximizeHoverForeColor = Color.Gray;
                    prop.MaximizeNormalForeColor = Color.Gray;
                    prop.MinimizeHoverBackColor = Color.FromArgb(238, 238, 238);
                    prop.MinimizeHoverForeColor = Color.Gray;
                    prop.MinimizeNormalForeColor = Color.Gray;
                    prop.DisabledForeColor = Color.Silver;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.ControlBoxDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "CloseHoverBackColor":
                                    prop.CloseHoverBackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "CloseHoverForeColor":
                                    prop.CloseHoverForeColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "CloseNormalForeColor":
                                    prop.CloseNormalForeColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "MaximizeHoverBackColor":
                                    prop.MaximizeHoverBackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "MaximizeHoverForeColor":
                                    prop.MaximizeHoverForeColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "MaximizeNormalForeColor":
                                    prop.MaximizeNormalForeColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "MinimizeHoverBackColor":
                                    prop.MinimizeHoverBackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "MinimizeHoverForeColor":
                                    prop.MinimizeHoverForeColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "MinimizeNormalForeColor":
                                    prop.MinimizeNormalForeColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledForeColor":
                                    prop.DisabledForeColor = utl.HexColor((string)varkey.Value);
                                    break;

                                default:
                                    return;
                            }
                        }
                    ;
                    break;

            }
            Invalidate();
        }

        #endregion Theme Changing

        #region Properties

        #region Public

        /// <summary>
        /// Gets or sets a value indicating whether the Maximize button is Enabled in the caption bar of the form.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets a value indicating whether the Maximize button is Enabled in the caption bar of the form.")]
        public bool MaximizeBox { get; set; } = true;


        /// <summary>
        /// Gets or sets a value indicating whether the Minimize button is Enabled in the caption bar of the form.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets a value indicating whether the Minimize button is Enabled in the caption bar of the form.")]
        public bool MinimizeBox { get; set; } = true;

        #endregion

        #region Private 

        private bool MinimizeHovered { get; set; } = false;

        private bool MaximizeHovered { get; set; } = false;

        private bool CloseHovered { get; set; } = false;

        #endregion

        #endregion

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            using (SolidBrush CloseBoxState = new SolidBrush(CloseHovered ? prop.CloseHoverBackColor : Color.Transparent))
            {
                using (Font F = new Font("Marlett", 12))
                {
                    using (SolidBrush TB = new SolidBrush(CloseHovered ? prop.CloseHoverForeColor : prop.CloseNormalForeColor))
                    {
                        using (StringFormat SF = new StringFormat { Alignment = StringAlignment.Center })
                        {
                            G.FillRectangle(CloseBoxState, new Rectangle(70, 5, 27, Height));
                            G.DrawString("r", F, CloseHovered ? TB : Brushes.Gray, new Point(Width - 16, 8), SF);
                        }
                    }
                }
            }
            using (SolidBrush MaximizeBoxState = new SolidBrush(MaximizeBox ? MaximizeHovered ? prop.MaximizeHoverBackColor : Color.Transparent : Color.Transparent))
            {
                using (Font F = new Font("Marlett", 12))
                {
                    using (SolidBrush TB = new SolidBrush(MaximizeBox ? MaximizeHovered ? prop.MaximizeHoverBackColor : prop.MaximizeNormalForeColor : prop.DisabledForeColor))
                    {
                        using (StringFormat SF = new StringFormat { Alignment = StringAlignment.Center })
                        {
                            string maxSymbol = Parent.FindForm().WindowState == FormWindowState.Maximized ? "2" : "1";

                            G.FillRectangle(MaximizeBoxState, new Rectangle(38, 5, 24, Height));
                            G.DrawString(maxSymbol, F, TB, new Point(51, 7), SF);
                        }
                    }
                }
            }
            using (SolidBrush MinimizeBoxState = new SolidBrush(MinimizeBox ? MinimizeHovered ? prop.MinimizeHoverBackColor : Color.Transparent : Color.Transparent))
            {
                using (Font F = new Font("Marlett", 12))
                {
                    using (SolidBrush TB = new SolidBrush(MinimizeBox ? MinimizeHovered ? prop.MinimizeHoverBackColor : prop.MinimizeNormalForeColor : prop.DisabledForeColor))
                    {
                        using (StringFormat SF = new StringFormat { Alignment = StringAlignment.Center })
                        {
                            G.FillRectangle(MinimizeBoxState, new Rectangle(5, 5, 27, Height));
                            G.DrawString("0", F, Brushes.Gray, new Point(20, 7), SF);
                        }
                    }
                }
            }

        }

        #endregion

        #region Events

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new Size(100, 25);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Location.Y > 0 && e.Location.Y < (Height - 2))
            {
                if (e.Location.X > 0 && e.Location.X < 34)
                {
                    Cursor = Cursors.Hand;
                    MinimizeHovered = true;
                    MaximizeHovered = false;
                    CloseHovered = false;
                }
                else if (e.Location.X > 33 && e.Location.X < 65)
                {
                    Cursor = Cursors.Hand;
                    MinimizeHovered = false;
                    MaximizeHovered = true;
                    CloseHovered = false;
                }
                else if (e.Location.X > 64 && e.Location.X < Width)
                {
                    Cursor = Cursors.Hand;
                    MinimizeHovered = false;
                    MaximizeHovered = false;
                    CloseHovered = true;
                }
                else
                {
                    Cursor = Cursors.Arrow;
                    MinimizeHovered = false;
                    MaximizeHovered = false;
                    CloseHovered = false;
                }
            }
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (CloseHovered)
            {
                Parent.FindForm().Close();
            }
            else if (MinimizeHovered)
            {
                if (MinimizeBox)
                    Parent.FindForm().WindowState = FormWindowState.Minimized;

            }
            else if (MaximizeHovered)
            {
                if (MaximizeBox)
                {
                    if (Parent.FindForm().WindowState == FormWindowState.Normal)
                    {
                        Parent.FindForm().WindowState = FormWindowState.Maximized;
                    }
                    else
                    {
                        Parent.FindForm().WindowState = FormWindowState.Normal;
                    }
                }
            }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Cursor = Cursors.Arrow;
            MinimizeHovered = false;
            MaximizeHovered = false;
            CloseHovered = false;
            Invalidate();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
        }

        #endregion

    }
}