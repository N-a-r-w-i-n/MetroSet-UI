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
        private int _startIndex;

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
            UpdateStyles();
            Font = MetroSetFonts.Regular(11);
            BackColor = Color.Transparent;
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 20;
            _startIndex = 0;
            CausesValidation = false;
            AllowDrop = true;
            DropDownStyle = ComboBoxStyle.DropDownList;
            _mth = new Methods();
            _utl = new Utilites();

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
            get => _startIndex;
            set
            {
                _startIndex = value;
                try
                {
                    SelectedIndex = value;
                }
                catch
                {
                    //
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
        public override Color BackColor => Color.Transparent;

        /// <summary>
        /// Gets or sets the form backcolor.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the form backcolor.")]
        [DisplayName("BackColor")]
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets border color used by the control
        /// </summary>
        [Category("MetroSet Framework")]
        public Color BorderColor { get; set; }

        /// <summary>
        /// Gets or sets arrow color used by the control
        /// </summary>
        [Category("MetroSet Framework")]
        public Color ArrowColor { get; set; }

        /// <summary>
        /// Gets or sets forecolor of the selected item used by the control
        /// </summary>
        [Category("MetroSet Framework")]
        public Color SelectedItemForeColor { get; set; }

        /// <summary>
        /// Gets or sets backcolor of the selected item used by the control
        /// </summary>
        [Category("MetroSet Framework")]
        public Color SelectedItemBackColor { get; set; }

        /// <summary>
        /// Gets or sets backcolor used by the control while disabled.
        /// </summary>
        [Category("MetroSet Framework")]
        public Color DisabledBackColor { get; set; }

        /// <summary>
        /// Gets or sets the forecolor of the control whenever while disabled.
        /// </summary>
        [Category("MetroSet Framework")]
        public Color DisabledForeColor { get; set; }

        /// <summary>
        /// Gets or sets the border color of the control while disabled.
        /// </summary>
        [Category("MetroSet Framework")]
        public Color DisabledBorderColor { get; set; }

        #endregion

        #region Draw Control

        /// <summary>
        /// Here we draw the items.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            var G = e.Graphics;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            if (e.Index == -1)
            {
                return;
            }

            var itemState = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            using (var bg = new SolidBrush(itemState ? SelectedItemBackColor : BackgroundColor))
            using (var tc = new SolidBrush(itemState ? SelectedItemForeColor : ForeColor))
            {
                using (var f = new Font(Font.Name, 9))
                {
                    G.FillRectangle(bg, e.Bounds);
                    G.DrawString(GetItemText(Items[e.Index]), f, tc, e.Bounds, _mth.SetPosition(StringAlignment.Near));
                }
            }
        }

        /// <summary>
        /// Here we draw the container.
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            var downArrow = '▼';
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            using (var bg = new SolidBrush(Enabled ? BackgroundColor : DisabledBackColor))
            {
                using (var p = new Pen(Enabled ? BorderColor : DisabledBorderColor))
                {
                    using (var s = new SolidBrush(Enabled ? ArrowColor : DisabledForeColor))
                    {
                        using (var tb = new SolidBrush(Enabled ? ForeColor : DisabledForeColor))
                        {
                            using (var f = MetroSetFonts.SemiBold(8))
                            {
                                G.FillRectangle(bg, rect);
                                G.TextRenderingHint = TextRenderingHint.AntiAlias;
                                G.DrawString(downArrow.ToString(), f, s, new Point(Width - 22, 8));
                                G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                                G.DrawString(Text, f, tb, new Rectangle(7, 0, Width - 1, Height - 1), _mth.SetPosition(StringAlignment.Near));
                                G.DrawRectangle(p, rect);
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
        private void ApplyTheme(Style style = Style.Light)
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
                                    ForeColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "BackColor":
                                    BackgroundColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "BorderColor":
                                    BorderColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "ArrowColor":
                                    ArrowColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "SelectedItemBackColor":
                                    SelectedItemBackColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "SelectedItemForeColor":
                                    SelectedItemForeColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledBackColor":
                                    DisabledBackColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledBorderColor":
                                    DisabledBorderColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledForeColor":
                                    DisabledForeColor = _utl.HexColor((string)varkey.Value);
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
            Invalidate();
        }

        #endregion Theme Changing

    }
}