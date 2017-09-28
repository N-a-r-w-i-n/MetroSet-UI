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
using System.Drawing.Drawing2D;
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

        private static ComboBoxProperties prop;
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
            prop = new ComboBoxProperties();
            mth = new Methods();
            utl = new Utilites();
            style = Style.Light;
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


        #endregion

        #region Draw Control

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Graphics G = e.Graphics;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            try
            {
                var itemState = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
                using (SolidBrush BG = new SolidBrush(itemState ? prop.SelectedItemBackColor : prop.BackColor))
                using (SolidBrush TC = new SolidBrush(itemState ? prop.SelectedItemForeColor : prop.ForeColor))
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

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            char downArrow = '▼';
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if (Enabled)
            {
                using (SolidBrush BG = new SolidBrush(prop.BackColor))
                {
                    using (Pen P = new Pen(prop.BorderColor))
                    {
                        using (SolidBrush S = new SolidBrush(prop.ArrowColor))
                        { 
                            using (SolidBrush TB = new SolidBrush(prop.ForeColor))
                            {
                                using (Font F = new Font(Font.Name, 9))
                                {
                                    using (Font F2 = MetroSetFonts.SemiBold((float)7.5))
                                    {
                                        G.FillRectangle(BG, rect);
                                        G.DrawString(downArrow.ToString(), F2, S, new Point(Width - 22, 7));
                                        G.DrawString(Text, F, TB, new Rectangle(7, 0, Width - 1, Height - 1), mth.SetPosition(StringAlignment.Near));
                                        G.DrawRectangle(P, rect);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                using (SolidBrush BG = new SolidBrush(prop.DisabledBackColor))
                {
                    using (Pen P = new Pen(prop.DisabledBorderColor))
                    {
                        using (SolidBrush S = new SolidBrush(prop.DisabledForeColor))
                        {
                            using (SolidBrush TB = new SolidBrush(prop.DisabledForeColor))
                            {
                                using (Font F = MetroSetFonts.SemiBold((float)7.5))
                                {
                                    G.FillRectangle(BG, rect);
                                    G.SmoothingMode = SmoothingMode.AntiAlias;
                                    G.DrawString(downArrow.ToString(), F, S, new Point(Width - 22, 6));
                                    G.SmoothingMode = SmoothingMode.None;
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
                    prop.Enabled = Enabled;
                    prop.ForeColor = Color.FromArgb(20, 20, 20);
                    prop.BackColor = Color.FromArgb(238, 238, 238);
                    prop.BorderColor = Color.FromArgb(150, 150, 150);
                    prop.ArrowColor = Color.FromArgb(150, 150, 150);
                    prop.SelectedItemBackColor = Color.FromArgb(65, 177, 225);
                    prop.SelectedItemForeColor = Color.White;
                    prop.DisabledBackColor = Color.FromArgb(204, 204, 204);
                    prop.DisabledBorderColor = Color.FromArgb(155, 155, 155);
                    prop.DisabledForeColor = Color.FromArgb(136, 136, 136);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    SetProperties();
                    break;

                case Style.Dark:
                    prop.Enabled = Enabled;
                    prop.ForeColor = Color.FromArgb(204, 204, 204);
                    prop.BackColor = Color.FromArgb(34, 34, 34);
                    prop.BorderColor = Color.FromArgb(110, 110, 110);
                    prop.ArrowColor = Color.FromArgb(110, 110, 110);
                    prop.SelectedItemBackColor = Color.FromArgb(65, 177, 225);
                    prop.SelectedItemForeColor = Color.White;
                    prop.DisabledBackColor = Color.FromArgb(80, 80, 80);
                    prop.DisabledBorderColor = Color.FromArgb(109, 109, 109);
                    prop.DisabledForeColor = Color.FromArgb(109, 109, 109);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    SetProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.ComboBoxDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "Enabled":
                                    prop.Enabled = Convert.ToBoolean(varkey.Value);
                                    break;

                                case "ForeColor":
                                    prop.ForeColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "BackColor":
                                    prop.BackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "BorderColor":
                                    prop.BorderColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "ArrowColor":
                                    prop.ArrowColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "SelectedItemBackColor":
                                    prop.SelectedItemBackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "SelectedItemForeColor":
                                    prop.SelectedItemForeColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledBackColor":
                                    prop.DisabledBackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledBorderColor":
                                    prop.DisabledBorderColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledForeColor":
                                    prop.DisabledForeColor = utl.HexColor((string)varkey.Value);
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
                Enabled = prop.Enabled;
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