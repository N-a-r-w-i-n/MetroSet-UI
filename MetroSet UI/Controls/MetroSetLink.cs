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
using MetroSet_UI.Property;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetLink), "Bitmaps.LinkLabel.bmp")]
    [Designer(typeof(MetroSetLinkDesigner))]
    [DefaultProperty("Text")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetLink : LinkLabel, iControl
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

        private LinkLabelProperties prop;
        private Methods mth;
        private Utilites utl;

        #endregion Global Vars

        #region Internal Vars

        private Style style;
        private StyleManager _StyleManager;

        #endregion Internal Vars

        #region Constructors

        public MetroSetLink()
        {
            SetStyle(
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true
                );
            DoubleBuffered = true;
            UpdateStyles();
            BackColor = Color.Transparent;
            Font = MetroSetFonts.Light(10);
            prop = new LinkLabelProperties();
            mth = new Methods();
            utl = new Utilites();
            style = Style.Dark;
            ApplyTheme();
            LinkBehavior = LinkBehavior.HoverUnderline;
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
                    prop.Enabled = Enabled;
                    prop.ForeColor = Color.Black;
                    prop.BackColor = Color.Transparent;
                    prop.ActiveLinkColor = utl.HexColor("#55c5f5");
                    prop.LinkColor = utl.HexColor("#41b1e1");
                    prop.VisitedLinkColor = utl.HexColor("#2d9dcd");
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    SetProperties();
                    break;

                case Style.Dark:
                    prop.Enabled = Enabled;
                    prop.ForeColor = Color.FromArgb(170, 170, 170);
                    prop.BackColor = Color.Transparent;
                    prop.ActiveLinkColor = utl.HexColor("#55c5f5");
                    prop.LinkColor = utl.HexColor("#41b1e1");
                    prop.VisitedLinkColor = utl.HexColor("#2d9dcd");
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    SetProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.LinkLabelDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "Enabled":
                                    prop.Enabled = bool.Parse((string)varkey.Value);
                                    break;

                                case "ForeColor":
                                    prop.ForeColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "BackColor":
                                    prop.BackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "LinkColor":
                                    prop.LinkColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "ActiveLinkColor":
                                    prop.ActiveLinkColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "VisitedLinkColor":
                                    prop.VisitedLinkColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "LinkBehavior":
                                    if ((string)varkey.Value == "HoverUnderline")
                                    {
                                        prop.LinkBehavior = LinkBehavior.HoverUnderline;
                                    }
                                    else if ((string)varkey.Value == "AlwaysUnderline")
                                    {
                                        prop.LinkBehavior = LinkBehavior.AlwaysUnderline;
                                    }
                                    else if ((string)varkey.Value == "NeverUnderline")
                                    {
                                        prop.LinkBehavior = LinkBehavior.NeverUnderline;
                                    }
                                    else if ((string)varkey.Value == "SystemDefault")
                                    {
                                        prop.LinkBehavior = LinkBehavior.SystemDefault;
                                    }
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
                BackColor = prop.BackColor;
                ForeColor = prop.ForeColor;
                LinkColor = prop.LinkColor;
                ActiveLinkColor = prop.ActiveLinkColor;
                VisitedLinkColor = prop.VisitedLinkColor;
                LinkBehavior = prop.LinkBehavior;
                Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion ApplyTheme
    }
}