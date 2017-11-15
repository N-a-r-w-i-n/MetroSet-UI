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
using MetroSet_UI.Enums;
using MetroSet_UI.Extensions;
using MetroSet_UI.Interfaces;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetBadge), "Bitmaps.Button.bmp")]
    [Designer(typeof(MetroSetBadgeDesigner))]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetBadge : Control, iControl
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

        public MetroSetBadge()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            BackColor = Color.Transparent;
            Font = MetroSetFonts.Light(10);
            utl = new Utilites();
            mth = new Methods();
            ApplyTheme();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the badge alignment associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the badge alignment associated with the control.")]
        public BadgeAlign BadgeAlignment { get; set; } = BadgeAlign.TopRight;

        /// <summary>
        /// Gets or sets the badge text associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the badge text associated with the control.")]
        public string BadgeText { get; set; } = "3";

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
        /// Gets or sets the control background color in normal mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the control background color in normal mouse sate.")]
        public Color NormalColor { get; set; }

        /// <summary>
        /// Gets or sets the control border color in normal mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the control border color in normal mouse sate.")]
        public Color NormalBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the control Text color in normal mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the control Text color in normal mouse sate.")]
        public Color NormalTextColor { get; set; }

        /// <summary>
        /// Gets or sets the control background color in hover mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the control background color in hover mouse sate.")]
        public Color HoverColor { get; set; }

        /// <summary>
        /// Gets or sets the control border color in hover mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the control border color in hover mouse sate.")]
        public Color HoverBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the control Text color in hover mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the control Text color in hover mouse sate.")]
        public Color HoverTextColor { get; set; }

        /// <summary>
        /// Gets or sets the control background color in pushed mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the control background color in pushed mouse sate.")]
        public Color PressColor { get; set; }

        /// <summary>
        /// Gets or sets the control border color in pushed mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the control border color in pushed mouse sate.")]
        public Color PressBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the control Text color in pushed mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the control Text color in pushed mouse sate.")]
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

        /// <summary>
        /// Gets or sets the Badge background color in normal mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the Badge background color in normal mouse sate.")]
        public Color NormalBadgeColor { get; set; }

        /// <summary>
        /// Gets or sets the Badge Text color in normal mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the Badge Text color in normal mouse sate.")]
        public Color NormalBadgeTextColor { get; set; }

        /// <summary>
        /// Gets or sets the Badge background color in hover mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the Badge background color in hover mouse sate.")]
        public Color HoverBadgeColor { get; set; }

        /// <summary>
        /// Gets or sets the Badge Text color in hover mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the Badge Text color in hover mouse sate.")]
        public Color HoverBadgeTextColor { get; set; }

        /// <summary>
        /// Gets or sets the Badge background color in pushed mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the Badge background color in pushed mouse sate.")]
        public Color PressBadgeColor { get; set; }

        /// <summary>
        /// Gets or sets the Badge Text color in pushed mouse sate.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the Badge Text color in pushed mouse sate.")]
        public Color PressBadgeTextColor { get; set; }

        #endregion Properties

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            Rectangle r = default(Rectangle);
            Rectangle badge = default(Rectangle);
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            switch (BadgeAlignment)
            {
                case BadgeAlign.Topleft:
                    r = new Rectangle(18, 18, Width - 21, Height - 21);
                    badge = new Rectangle(5, 5, 29, 29);
                    break;

                case BadgeAlign.TopRight:
                    r = new Rectangle(0, 18, Width - 18, Height - 21);
                    badge = new Rectangle(Width - 35, 1, 29, 29);
                    break;

                case BadgeAlign.BottmLeft:
                    r = new Rectangle(18, 0, Width - 19, Height - 18);
                    badge = new Rectangle(1, Height - 35, 29, 29);
                    break;

                case BadgeAlign.BottomRight:
                    r = new Rectangle(0, 0, Width - 19, Height - 18);
                    badge = new Rectangle(Width - 35, Height - 35, 29, 29);
                    break;
            }

            switch (State)
            {
                case MouseMode.Normal:

                    using (SolidBrush BG = new SolidBrush(NormalColor))
                    using (Pen P = new Pen(NormalBorderColor))
                    using (SolidBrush TB = new SolidBrush(NormalTextColor))
                    using (SolidBrush bdgBrush = new SolidBrush(NormalBadgeColor))
                    using (SolidBrush bdgtxtBrush = new SolidBrush(NormalBadgeTextColor))
                    {
                        G.FillRectangle(BG, r);
                        G.DrawRectangle(P, r);
                        G.DrawString(Text, Font, TB, r, mth.SetPosition());
                        SmoothingType(G);
                        G.FillEllipse(bdgBrush, badge);
                        G.DrawString(BadgeText, Font, bdgtxtBrush, badge, mth.SetPosition());
                    }

                    break;

                case MouseMode.Hovered:

                    Cursor = Cursors.Hand;
                    using (SolidBrush BG = new SolidBrush(HoverColor))
                    using (Pen P = new Pen(HoverBorderColor))
                    using (SolidBrush TB = new SolidBrush(HoverTextColor))
                    using (SolidBrush bdgBrush = new SolidBrush(HoverBadgeColor))
                    using (SolidBrush bdgtxtBrush = new SolidBrush(HoverBadgeTextColor))
                    {
                        G.FillRectangle(BG, r);
                        G.DrawRectangle(P, r);
                        G.DrawString(Text, Font, TB, r, mth.SetPosition());
                        SmoothingType(G);
                        G.FillEllipse(bdgBrush, badge);
                        G.DrawString(BadgeText, Font, bdgtxtBrush, badge, mth.SetPosition());
                    }

                    break;

                case MouseMode.Pushed:

                    using (SolidBrush BG = new SolidBrush(PressColor))
                    using (Pen P = new Pen(PressBorderColor))
                    using (SolidBrush TB = new SolidBrush(PressTextColor))
                    using (SolidBrush bdgBrush = new SolidBrush(PressBadgeColor))
                    using (SolidBrush bdgtxtBrush = new SolidBrush(PressBadgeTextColor))
                    {
                        G.FillRectangle(BG, r);
                        G.DrawRectangle(P, r);
                        G.DrawString(Text, Font, TB, r, mth.SetPosition());
                        SmoothingType(G);
                        G.FillEllipse(bdgBrush, badge);
                        G.DrawString(BadgeText, Font, bdgtxtBrush, badge, mth.SetPosition());
                    }

                    break;

                case MouseMode.Disabled:

                    using (SolidBrush BG = new SolidBrush(DisabledBackColor))
                    using (Pen P = new Pen(DisabledBorderColor))
                    using (SolidBrush TB = new SolidBrush(DisabledForeColor))
                    using (SolidBrush bdgBrush = new SolidBrush(PressBadgeColor))
                    using (SolidBrush bdgtxtBrush = new SolidBrush(PressBadgeTextColor))
                    {
                        G.FillRectangle(BG, r);
                        G.DrawRectangle(P, r);
                        G.DrawString(Text, Font, TB, r, mth.SetPosition());
                        SmoothingType(G);
                        G.FillEllipse(bdgBrush, badge);
                        G.DrawString(BadgeText, Font, bdgtxtBrush, badge, mth.SetPosition());
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
                    NormalColor = Color.FromArgb(238, 238, 238);
                    NormalBorderColor = Color.FromArgb(204, 204, 204);
                    NormalTextColor = Color.Black;
                    HoverColor = Color.FromArgb(102, 102, 102);
                    HoverBorderColor = Color.FromArgb(102, 102, 102);
                    HoverTextColor = Color.White;
                    PressColor = Color.FromArgb(51, 51, 51);
                    PressBorderColor = Color.FromArgb(51, 51, 51);
                    PressTextColor = Color.White;
                    NormalBadgeColor = Color.FromArgb(65, 177, 225);
                    NormalBadgeTextColor = Color.White;
                    HoverBadgeColor = Color.FromArgb(85, 187, 245);
                    HoverBadgeTextColor = Color.White;
                    PressBadgeColor = Color.FromArgb(45, 147, 205);
                    PressBadgeTextColor = Color.White;
                    DisabledBackColor = Color.FromArgb(204, 204, 204);
                    DisabledBorderColor = Color.FromArgb(155, 155, 155);
                    DisabledForeColor = Color.FromArgb(136, 136, 136);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    break;

                case Style.Dark:
                    NormalColor = Color.FromArgb(32, 32, 32);
                    NormalBorderColor = Color.FromArgb(64, 64, 64);
                    NormalTextColor = Color.FromArgb(204, 204, 204);
                    HoverColor = Color.FromArgb(170, 170, 170);
                    HoverBorderColor = Color.FromArgb(170, 170, 170);
                    HoverTextColor = Color.White;
                    PressColor = Color.FromArgb(240, 240, 240);
                    PressBorderColor = Color.FromArgb(240, 240, 240);
                    PressTextColor = Color.White;
                    NormalBadgeColor = Color.FromArgb(65, 177, 225);
                    NormalBadgeTextColor = Color.White;
                    HoverBadgeColor = Color.FromArgb(85, 187, 245);
                    HoverBadgeTextColor = Color.White;
                    PressBadgeColor = Color.FromArgb(45, 147, 205);
                    PressBadgeTextColor = Color.White;
                    DisabledBackColor = Color.FromArgb(80, 80, 80);
                    DisabledBorderColor = Color.FromArgb(109, 109, 109);
                    DisabledForeColor = Color.FromArgb(109, 109, 109);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.BadgeDictionary)
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
                            else if (varkey.Key == "NormalBadgeColor")
                            {
                                NormalBadgeColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "NormalBadgeTextColor")
                            {
                                NormalBadgeTextColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "HoverBadgeColor")
                            {
                                HoverBadgeColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "HoverBadgeTextColor")
                            {
                                HoverBadgeTextColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "PressBadgeColor")
                            {
                                PressBadgeColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "PressBadgeTextColor")
                            {
                                PressBadgeTextColor = utl.HexColor((string)varkey.Value);
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
                    Invalidate();
                    break;
            }
        }

        #endregion Theme Changing

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
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseMode.Hovered;
            Invalidate();
        }

        /// <summary>
        /// Handling mouse leave event of the cotnrol.
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseMode.Normal;
            Invalidate();
        }

        #endregion Events

        #region Methods

        /// <summary>
        /// Sets the smoothingmode the the specific graphics.
        /// </summary>
        /// <param name="e">Graphics to Set the effect.</param>
        /// <param name="state">state of smoothingmode.</param>
        public void SmoothingType(Graphics e, SmoothingMode state = SmoothingMode.AntiAlias)
        {
            e.SmoothingMode = state;
        }

        #endregion Methods

    }
}