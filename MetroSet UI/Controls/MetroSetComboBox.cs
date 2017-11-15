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
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetComboBox), "Bitmaps.ComoBox.bmp")]
    [DefaultEvent("SelectedIndexChanged")]
    [DefaultProperty("Items")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetComboBox : ComboBox, iControl
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

        private Methods mth;
        private Utilites utl;

        #endregion Global Vars

        #region Internal Vars

        private Style style;
        private StyleManager _StyleManager;

        private int _StartIndex;

        #endregion Internal Vars

        #region Constructors

        public MetroSetComboBox()
        {
            SetStyle
                (
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                true);
            DoubleBuffered = true;
            UpdateStyles();
            Font = MetroSetFonts.Regular(11);
            BackColor = Color.Transparent;
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 20;
            DoubleBuffered = true;
            _StartIndex = 0;
            CausesValidation = false;
            AllowDrop = true;
            DropDownStyle = ComboBoxStyle.DropDownList;
            mth = new Methods();
            utl = new Utilites();

            ApplyTheme();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the index specifying the currently selected item.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the index specifying the currently selected item.")]
        private int StartIndex
        {
            get { return _StartIndex; }
            set
            {
                _StartIndex = value;
                try
                {
                    base.SelectedIndex = value;
                }
                catch
                {
                }
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets forecolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the form forecolor.")]
        public override Color ForeColor { get; set; }

        /// <summary>
        /// I make backcolor inaccessible cause I want it to be just transparent and I used another property for the same job in following properties. 
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor
        {
            get { return Color.Transparent; }
        }

        /// <summary>
        /// Gets or sets the form backcolor.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the form backcolor.")]
        [DisplayName("BackColor")]
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets border color used by the control
        /// </summary>
        public Color BorderColor { get; set; }

        /// <summary>
        /// Gets or sets arrow color used by the control
        /// </summary>
        public Color ArrowColor { get; set; }

        /// <summary>
        /// Gets or sets forecolor of the selected item used by the control
        /// </summary>
        public Color SelectedItemForeColor { get; set; }

        /// <summary>
        /// Gets or sets backcolor of the selected item used by the control
        /// </summary>
        public Color SelectedItemBackColor { get; set; }

        /// <summary>
        /// Gets or sets backcolor used by the control while disabled.
        /// </summary>
        public Color DisabledBackColor { get; set; }

        /// <summary>
        /// Gets or sets the forecolor of the control whenever while disabled.
        /// </summary>
        public Color DisabledForeColor { get; set; }

        /// <summary>
        /// Gets or sets the border color of the control while disabled.
        /// </summary>
        public Color DisabledBorderColor { get; set; }

        #endregion

        #region Draw Control

        /// <summary>
        /// Here we draw the items.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Graphics G = e.Graphics;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            try
            {
                var itemState = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
                using (SolidBrush BG = new SolidBrush(itemState ? SelectedItemBackColor : BackgroundColor))
                using (SolidBrush TC = new SolidBrush(itemState ? SelectedItemForeColor : ForeColor))
                {
                    using (Font F = new Font(Font.Name, 9))
                    {
                        G.FillRectangle(BG, e.Bounds);
                        G.DrawString(GetItemText(Items[e.Index]), F, TC, e.Bounds, mth.SetPosition(StringAlignment.Near));
                    }
                }

            }
            catch
            {
            }
        }

        /// <summary>
        /// Here we draw the container.
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            char downArrow = '▼';
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            if (Enabled)
            {
                using (SolidBrush BG = new SolidBrush(BackgroundColor))
                {
                    using (Pen P = new Pen(BorderColor))
                    {
                        using (SolidBrush S = new SolidBrush(ArrowColor)) 
                        {
                            using (SolidBrush TB = new SolidBrush(ForeColor))
                            {
                                using (Font F = MetroSetFonts.SemiBold(8))
                                {
                                    G.FillRectangle(BG, rect);
                                    G.TextRenderingHint = TextRenderingHint.AntiAlias;
                                    G.DrawString(downArrow.ToString(), F, S, new Point(Width - 22, 8));
                                    G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                                    G.DrawString(Text, F, TB, new Rectangle(7, 0, Width - 1, Height - 1), mth.SetPosition(StringAlignment.Near));
                                    G.DrawRectangle(P, rect);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                using (SolidBrush BG = new SolidBrush(DisabledBackColor))
                {
                    using (Pen P = new Pen(DisabledBorderColor))
                    {
                        using (SolidBrush S = new SolidBrush(DisabledForeColor))
                        {
                            using (SolidBrush TB = new SolidBrush(DisabledForeColor))
                            {
                                using (Font F = MetroSetFonts.SemiBold(8))
                                {
                                    G.FillRectangle(BG, rect);
                                    G.TextRenderingHint = TextRenderingHint.AntiAlias;
                                    G.DrawString(downArrow.ToString(), F, S, new Point(Width - 22, 8));
                                    G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                                    G.DrawString(Text, Font, TB, new Rectangle(7, 1, Width - 1, Height - 1), mth.SetPosition(StringAlignment.Near));
                                    G.DrawRectangle(P, rect);
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

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
                    ForeColor = Color.FromArgb(20, 20, 20);
                    BackgroundColor = Color.FromArgb(238, 238, 238);
                    BorderColor = Color.FromArgb(150, 150, 150);
                    ArrowColor = Color.FromArgb(150, 150, 150);
                    SelectedItemBackColor = Color.FromArgb(65, 177, 225);
                    SelectedItemForeColor = Color.White;
                    DisabledBackColor = Color.FromArgb(204, 204, 204);
                    DisabledBorderColor = Color.FromArgb(155, 155, 155);
                    DisabledForeColor = Color.FromArgb(136, 136, 136);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;

                case Style.Dark:
                    ForeColor = Color.FromArgb(204, 204, 204);
                    BackgroundColor = Color.FromArgb(34, 34, 34);
                    BorderColor = Color.FromArgb(110, 110, 110);
                    ArrowColor = Color.FromArgb(110, 110, 110);
                    SelectedItemBackColor = Color.FromArgb(65, 177, 225);
                    SelectedItemForeColor = Color.White;
                    DisabledBackColor = Color.FromArgb(80, 80, 80);
                    DisabledBorderColor = Color.FromArgb(109, 109, 109);
                    DisabledForeColor = Color.FromArgb(109, 109, 109);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.ComboBoxDictionary)
                        {
                            switch (varkey.Key)
                            {

                                case "ForeColor":
                                    ForeColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "BackColor":
                                    BackgroundColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "BorderColor":
                                    BorderColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "ArrowColor":
                                    ArrowColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "SelectedItemBackColor":
                                    SelectedItemBackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "SelectedItemForeColor":
                                    SelectedItemForeColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledBackColor":
                                    DisabledBackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledBorderColor":
                                    DisabledBorderColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledForeColor":
                                    DisabledForeColor = utl.HexColor((string)varkey.Value);
                                    break;

                                default:
                                    return;
                            }
                        }
                    UpdateProperties();
                    break;
            }
        }

        public void UpdateProperties()
        {
            try
            {
                Invalidate();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        #endregion Theme Changing

    }
}