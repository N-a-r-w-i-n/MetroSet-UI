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
using MetroSet_UI.Native;

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
            Cursor = Cursors.Hand;
            Font = MetroSetFonts.Light(10);
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
                    ForeColor = Color.Black;
                    BackColor = Color.Transparent;
                    ActiveLinkColor = Color.FromArgb(85, 197, 245);
                    LinkColor = Color.FromArgb(65, 177, 225);
                    VisitedLinkColor = Color.FromArgb(45, 157, 205);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;

                case Style.Dark:
                    ForeColor = Color.FromArgb(170, 170, 170);
                    BackColor = Color.Transparent;
                    ActiveLinkColor = Color.FromArgb(85, 197, 245);
                    LinkColor = Color.FromArgb(65, 177, 225);
                    VisitedLinkColor = Color.FromArgb(45, 157, 205);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.LinkLabelDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "ForeColor":
                                    ForeColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "BackColor":
                                    BackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "LinkColor":
                                    LinkColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "ActiveLinkColor":
                                    ActiveLinkColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "VisitedLinkColor":
                                    VisitedLinkColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "LinkBehavior":
                                    if ((string)varkey.Value == "HoverUnderline")
                                    {
                                        LinkBehavior = LinkBehavior.HoverUnderline;
                                    }
                                    else if ((string)varkey.Value == "AlwaysUnderline")
                                    {
                                        LinkBehavior = LinkBehavior.AlwaysUnderline;
                                    }
                                    else if ((string)varkey.Value == "NeverUnderline")
                                    {
                                        LinkBehavior = LinkBehavior.NeverUnderline;
                                    }
                                    else if ((string)varkey.Value == "SystemDefault")
                                    {
                                        LinkBehavior = LinkBehavior.SystemDefault;
                                    }
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
                throw new Exception(ex.Message);
            }
        }

        #endregion ApplyTheme

        #region Events

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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets forecolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the form forecolor.")]
        public override Color ForeColor { get; set; } = Color.Black;

        // <summary>
        /// Gets or sets the form backcolor.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the form backcolor.")]
        public override Color BackColor { get; set; } = Color.Transparent;

        /// <summary>
        /// Gets or sets LinkColor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets LinkColor used by the control.")]
        public new Color LinkColor { get; set; } = Color.FromArgb(65, 177, 225);

        /// <summary>
        /// Gets or sets ActiveLinkColor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets ActiveLinkColor used by the control.")]
        public new Color ActiveLinkColor { get; set; } = Color.FromArgb(85, 197, 245);

        /// <summary>
        /// Gets or sets VisitedLinkColor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets VisitedLinkColor used by the control.")]
        public new Color VisitedLinkColor { get; set; } = Color.FromArgb(45, 157, 205);

        /// <summary>
        /// Gets or sets LinkBehavior used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets LinkBehavior used by the control.")]
        public new LinkBehavior LinkBehavior { get; set; }

        /// <summary>
        /// Gets or sets DisabledLinkColor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets DisabledLinkColor used by the control.")]
        public new Color DisabledLinkColor { get; set; } = Color.FromArgb(133, 133, 133);

        #endregion Properties

    }
}