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
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetTile), "Bitmaps.Button.bmp")]
    [Designer(typeof(MetroSetTileDesigner))]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetTile : Control, iControl
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

        public MetroSetTile()
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
            Rectangle r = new Rectangle(1, 1, Width - 2, Height - 2);
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            StringFormat SF = new StringFormat();

            switch (TileAlign)
            {
                case TileAlign.BottmLeft:
                    SF = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far };
                    break;
                case TileAlign.BottomRight:
                    SF = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far };
                    break;
                case TileAlign.Topleft:
                    SF = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
                    break;
                case TileAlign.TopRight:
                    SF = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Near };
                    break;
                case TileAlign.TopCenter:
                    SF = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };
                    break;
                case TileAlign.BottomCenter:
                    SF = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far };
                    break;
            }

            switch (State)
            {
                case MouseMode.Normal:

                    using (SolidBrush BG = new SolidBrush(NormalColor))
                    {
                        using (Pen P = new Pen(NormalBorderColor, 2))
                        {
                            using (SolidBrush TB = new SolidBrush(NormalTextColor))
                            {
                                if (BackgroundImage != null)
                                {
                                    G.DrawImage(BackgroundImage, r);
                                }
                                else
                                {
                                    G.FillRectangle(BG, r);
                                    G.DrawRectangle(P, r);
                                }
                                G.DrawString(Text, Font, TB, r, SF);                                
                            }
                        }
                    }
                    break;

                case MouseMode.Hovered:

                    Cursor = Cursors.Hand;
                    using (SolidBrush BG = new SolidBrush(HoverColor))
                    {
                        using (Pen P = new Pen(HoverBorderColor, 2))
                        {
                            using (SolidBrush TB = new SolidBrush(HoverTextColor))
                            {
                                if (BackgroundImage != null)
                                {
                                    G.DrawImage(BackgroundImage, r);
                                }
                                else
                                {
                                    G.FillRectangle(BG, r);
                                }
                                G.DrawString(Text, Font, TB, r, SF);
                                G.DrawRectangle(P, r);
                            }
                        }
                    }
                    break;

                case MouseMode.Pushed:

                    using (SolidBrush BG = new SolidBrush(PressColor))
                    {
                        using (Pen P = new Pen(PressBorderColor, 2))
                        {
                            using (SolidBrush TB = new SolidBrush(PressTextColor))
                            {
                                if (BackgroundImage != null)
                                {
                                    G.DrawImage(BackgroundImage, r);
                                }
                                else
                                {
                                    G.FillRectangle(BG, r);
                                }
                                G.DrawString(Text, Font, TB, r, SF);
                                G.DrawRectangle(P, r);

                            }
                        }
                    }
                    break;

                case MouseMode.Disabled:
                    using (SolidBrush BG = new SolidBrush(DisabledBackColor))
                    {
                        using (Pen P = new Pen(DisabledBorderColor))
                        {
                            using (SolidBrush TB = new SolidBrush(DisabledForeColor))
                            {
                                G.FillRectangle(BG, r);
                                G.DrawString(Text, Font, TB, r, SF);
                                G.DrawRectangle(P, r);
                            }
                        }
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
                    HoverColor = Color.FromArgb(65, 177, 225);
                    HoverBorderColor = Color.FromArgb(230, 230, 230);
                    HoverTextColor = Color.White;
                    PressColor = Color.FromArgb(65, 177, 225);
                    PressBorderColor = Color.FromArgb(65, 177, 225);
                    PressTextColor = Color.White;
                    DisabledBackColor = Color.FromArgb(204, 204, 204);
                    DisabledBorderColor = Color.FromArgb(155, 155, 155);
                    DisabledForeColor = Color.FromArgb(136, 136, 136);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    break;

                case Style.Dark:
                    NormalColor = Color.FromArgb(65, 177, 225);
                    NormalBorderColor = Color.FromArgb(65, 177, 225);
                    NormalTextColor = Color.White;
                    HoverColor = Color.FromArgb(65, 177, 225);
                    HoverBorderColor = Color.FromArgb(102, 102, 102);
                    HoverTextColor = Color.White;
                    PressColor = Color.FromArgb(65, 177, 225);
                    PressBorderColor = Color.FromArgb(51, 51, 51);
                    PressTextColor = Color.White;
                    DisabledBackColor = Color.FromArgb(80, 80, 80);
                    DisabledBorderColor = Color.FromArgb(109, 109, 109);
                    DisabledForeColor = Color.FromArgb(109, 109, 109);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.TileDictionary)
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
        /// Gets the background color.
        /// </summary>
        [Browsable(false)]
        public override Color BackColor
        {
            get { return Color.Transparent; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control can respond to user interaction.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets a value indicating whether the control can respond to user interaction.")]
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
        /// Gets or sets the BackgroundImage associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the BackgroundImage associated with the control.")]
        public override Image BackgroundImage { get => base.BackgroundImage; set => base.BackgroundImage = value; }

        /// <summary>
        /// Gets or sets the TileAlign associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the TileAlign associated with the control.")]
        public TileAlign TileAlign { get; set; } = TileAlign.BottmLeft;

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

    }

}