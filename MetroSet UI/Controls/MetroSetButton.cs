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

using MetroSet_UI.Design;
using MetroSet_UI.Extensions;
using MetroSet_UI.Interfaces;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetButton), "Bitmaps.Button.bmp")]
    [Designer(typeof(MetroSetButtonDesigner))]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetButton : Control, iControl
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
        /// Gets or sets the The Author name associated with the theme.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the The Author name associated with the theme.")]
        public string ThemeAuthor { get; set; }

        /// <summary>
        /// Gets or sets the The Theme name associated with the theme.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the The Theme name associated with the theme.")]
        public string ThemeName { get; set; }

        /// <summary>
        /// Gets or sets the Style Manager associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the Style Manager associated with the control.")]
        public StyleManager StyleManager
        {
            get { return _StyleManager; }
            set
            {
                _StyleManager = value;
                Invalidate();
            }
        }

        #endregion Interfaces

        #region Global Vars

        private Methods mth;
        private Utilites utl;

        #endregion Global Vars

        #region Internal Vars

        private MouseMode State;
        private Style style;
        private StyleManager _StyleManager;
        
        #endregion Internal Vars

        #region Constructors

        public MetroSetButton()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            Font = MetroSetFonts.Light(10);
            utl = new Utilites();
            mth = new Methods();

            ApplyTheme();
        }

        #endregion Constructors

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            Rectangle r = new Rectangle(0, 0, Width - 1, Height - 1);
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            switch (State)
            {
                case MouseMode.Normal:

                    using (SolidBrush BG = new SolidBrush(NormalColor))
                    using (Pen P = new Pen(NormalBorderColor))
                    using (SolidBrush TB = new SolidBrush(NormalTextColor))
                    {
                        G.FillRectangle(BG, r);
                        G.DrawRectangle(P, r);
                        G.DrawString(Text, Font, TB, r, mth.SetPosition());
                    }

                    break;

                case MouseMode.Hovered:

                    Cursor = Cursors.Hand;
                    using (SolidBrush BG = new SolidBrush(HoverColor))
                    using (Pen P = new Pen(HoverBorderColor))
                    using (SolidBrush TB = new SolidBrush(HoverTextColor))
                    {
                        G.FillRectangle(BG, r);
                        G.DrawRectangle(P, r);
                        G.DrawString(Text, Font, TB, r, mth.SetPosition());
                    }

                    break;

                case MouseMode.Pushed:

                    using (SolidBrush BG = new SolidBrush(PressColor))
                    using (Pen P = new Pen(PressBorderColor))
                    using (SolidBrush TB = new SolidBrush(PressTextColor))
                    {
                        G.FillRectangle(BG, r);
                        G.DrawRectangle(P, r);
                        G.DrawString(Text, Font, TB, r, mth.SetPosition());
                    }

                    break;

                case MouseMode.Disabled:

                    using (SolidBrush BG = new SolidBrush(DisabledBackColor))
                    using (Pen P = new Pen(DisabledBorderColor))
                    using (SolidBrush TB = new SolidBrush(DisabledForeColor))
                    {
                        G.FillRectangle(BG, r);
                        G.DrawRectangle(P, r);
                        G.DrawString(Text, Font, TB, r, mth.SetPosition());
                    }
                    break;
            }
        }

        #endregion Draw Control

        #region ApplyTheme

        /// <summary>
        /// Gets or sets the style provided by the user.
        /// </summary>
        /// <param name="style">The Style.</param>
        /// <param name="path">The path of the custom theme.</param>
        internal void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    NormalColor = Color.FromArgb(65, 177, 225);
                    NormalBorderColor = Color.FromArgb(65, 177, 225);
                    NormalTextColor = Color.White;
                    HoverColor = Color.FromArgb(95, 207, 255);
                    HoverBorderColor = Color.FromArgb(95, 207, 255);
                    HoverTextColor = Color.White;
                    PressColor = Color.FromArgb(35, 147, 195);
                    PressBorderColor = Color.FromArgb(35, 147, 195);
                    PressTextColor = Color.White;
                    DisabledBackColor = Color.FromArgb(120, 65, 177, 225);
                    DisabledBorderColor = Color.FromArgb(120, 65, 177, 225);
                    DisabledForeColor = Color.Gray;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    break;

                case Style.Dark:
                    NormalColor = Color.FromArgb(65, 177, 225);
                    NormalBorderColor = Color.FromArgb(65, 177, 225);
                    NormalTextColor = Color.White;
                    HoverColor = Color.FromArgb(95, 207, 255);
                    HoverBorderColor = Color.FromArgb(95, 207, 255);
                    HoverTextColor = Color.White;
                    PressColor = Color.FromArgb(35, 147, 195);
                    PressBorderColor = Color.FromArgb(35, 147, 195);
                    PressTextColor = Color.White;
                    DisabledBackColor = Color.FromArgb(120, 65, 177, 225);
                    DisabledBorderColor = Color.FromArgb(120, 65, 177, 225);
                    DisabledForeColor = Color.Gray;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    break;
                    
                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.ButtonDictionary)
                        {
                            if ((varkey.Key == null) || varkey.Key == null)
                            {
                                return;
                            }

                            if (varkey.Key == "NormalColor")
                            {
                                NormalColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "NormalBorderColor")
                            {
                                NormalBorderColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "NormalTextColor")
                            {
                                NormalTextColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "HoverColor")
                            {
                                HoverColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "HoverBorderColor")
                            {
                                HoverBorderColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "HoverTextColor")
                            {
                                HoverTextColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "PressColor")
                            {
                                PressColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "PressBorderColor")
                            {
                                PressBorderColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "PressTextColor")
                            {
                                PressTextColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "DisabledBackColor")
                            {
                                DisabledBackColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "DisabledBorderColor")
                            {
                                DisabledBorderColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "DisabledForeColor")
                            {
                                DisabledForeColor = utl.HexColor((string)varkey.Value);
                            }
                        }
                    Refresh();
                    break;
            }
        }

        #endregion Theme Changing

        #region Properties

        /// <summary>
        /// I make backcolor inaccessible cause we have not use of it. 
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor
        {
            get { return Color.Transparent; }
        }

        /// <summary>
        /// Handling Control Enable state to detect the disability state.
        /// </summary>
        [Category("MetroSet Framework")]
        public new bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                if (value == false)
                {
                    State = MouseMode.Disabled;
                }
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the button background color in normal mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the button background color in normal mouse sate.")]
        public Color NormalColor { get; set; }

        /// <summary>
        /// Gets or sets the button border color in normal mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the button border color in normal mouse sate.")]
        public Color NormalBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the button Text color in normal mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the button Text color in normal mouse sate.")]
        public Color NormalTextColor { get; set; }

        /// <summary>
        /// Gets or sets the button background color in hover mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the button background color in hover mouse sate.")]
        public Color HoverColor { get; set; }

        /// <summary>
        /// Gets or sets the button border color in hover mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the button border color in hover mouse sate.")]
        public Color HoverBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the button Text color in hover mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the button Text color in hover mouse sate.")]
        public Color HoverTextColor { get; set; }

        /// <summary>
        /// Gets or sets the button background color in pushed mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the button background color in pushed mouse sate.")]
        public Color PressColor { get; set; }

        /// <summary>
        /// Gets or sets the button border color in pushed mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the button border color in pushed mouse sate.")]
        public Color PressBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the button Text color in pushed mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the button Text color in pushed mouse sate.")]
        public Color PressTextColor { get; set; }

        /// <summary>
        /// Gets or sets backcolor used by the control while disabled.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets backcolor used by the control while disabled.")]
        public Color DisabledBackColor { get; set; }

        /// <summary>
        /// Gets or sets the forecolor of the control whenever while disabled.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the forecolor of the control whenever while disabled.")]
        public Color DisabledForeColor { get; set; }

        /// <summary>
        /// Gets or sets the border color of the control while disabled.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the border color of the control while disabled.")]
        public Color DisabledBorderColor { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Handling mouse up event of the cotnrol.
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseMode.Hovered;
            Invalidate();
        }

        /// <summary>
        /// Handling mouse down event of the cotnrol.
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseMode.Pushed;
            Invalidate();
        }

        /// <summary>
        /// Handling mouse entering event of the control.
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseMode.Hovered;
            Invalidate();
        }

        /// <summary>
        /// Handling mouse leave event of the cotnrol.
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseMode.Normal;
            Invalidate();
        }

        #endregion Events
    }
}