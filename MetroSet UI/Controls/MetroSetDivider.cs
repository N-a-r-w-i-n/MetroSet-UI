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
using MetroSet_UI.Property;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetDivider), "Bitmaps.Divider.bmp")]
    [Designer(typeof(MetroSetDividerDesigner))]
    [DefaultProperty("Orientation")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetDivider : Control, iControl
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

        private static DividerProperties prop;
        private Methods mth;
        private Utilites utl;

        #endregion Global Vars

        #region Internal Vars

        private Style style;
        private StyleManager _StyleManager;

        #endregion Internal Vars

        #region Constructors

        public MetroSetDivider()
        {
            SetStyle(                
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            BackColor = Color.Transparent;
            prop = new DividerProperties();
            mth = new Methods();
            utl = new Utilites();
            prop.Orientation = DividerStyle.Horizontal;
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
                    prop.Thickness = 1;
                    prop.ForeColor = Color.Black;
                    prop.BackColor = Color.Transparent;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;

                case Style.Dark:
                    prop.Thickness = 1;
                    prop.ForeColor = Color.FromArgb(170, 170, 170);
                    prop.BackColor = Color.Transparent;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.DividerDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "Orientation":
                                    if ((string)varkey.Value == "Horizontal")
                                    {
                                        prop.Orientation = DividerStyle.Horizontal;
                                    }
                                    else if ((string)varkey.Value == "Vertical")
                                    {
                                        prop.Orientation = DividerStyle.Vertical;
                                    }
                                    break;

                                case "Thickness":
                                    prop.Thickness = ((int)varkey.Value);
                                    break;

                                case "ForeColor":
                                    prop.ForeColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "BackColor":
                                    prop.BackColor = utl.HexColor((string)varkey.Value);
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
                BackColor = prop.BackColor;
                ForeColor = prop.ForeColor;
                Invalidate();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        #endregion ApplyTheme

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            using (Pen P = new Pen(ForeColor, prop.Thickness))
            {
                switch (prop.Orientation)
                {
                    case DividerStyle.Horizontal:
                        G.DrawLine(P, 0, prop.Thickness, Width, prop.Thickness);
                        break;

                    case DividerStyle.Vertical:
                        G.DrawLine(P, prop.Thickness, 0, prop.Thickness, Height);
                        break;
                }
            }
        }

        #endregion Draw Control

        #region Properties

        /// <summary>
        /// Gets or sets the style associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets Orientation of the control.")]
        public DividerStyle Orientation
        {
            get { return prop.Orientation; }
            set
            {
                prop.Orientation = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the divider thickness.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the divider thickness.")]
        public int Thickness
        {
            get { return prop.Thickness; }
            set
            {
                prop.Thickness = value;
                Invalidate();
            }
        }

        #endregion Properties

        #region Events

        /// <summary>
        /// Here we handle the width and height while resizing.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (prop.Orientation == DividerStyle.Horizontal)
            {
                Height = prop.Thickness + 3;
            }
            else
            {
                Width = prop.Thickness + 3;
            }
        }

        #endregion Events
    }
}