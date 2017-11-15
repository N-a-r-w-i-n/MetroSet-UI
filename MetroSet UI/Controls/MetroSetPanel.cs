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
using System.Runtime.InteropServices; 
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetPanel), "Bitmaps.Panel.bmp")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetPanel : Panel, iControl
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

        public MetroSetPanel()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BorderStyle = BorderStyle.None;
            UpdateStyles();
            mth = new Methods();
            utl = new Utilites();
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
                    BorderColor = Color.FromArgb(150, 150, 150);
                    BackgroundColor = Color.White;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;

                case Style.Dark:
                    BorderColor = Color.FromArgb(110, 110, 110);
                    BackgroundColor = Color.FromArgb(30, 30, 30);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.LabelDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "BorderColor":
                                    BorderColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "BackColor":
                                    BackgroundColor = utl.HexColor((string)varkey.Value);
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
                Enabled = Enabled;
                Invalidate();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        #endregion Theme Changing

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            Rectangle r = new Rectangle(BorderThickness, BorderThickness, Width - ((BorderThickness * 2) + 1), Height - ((BorderThickness * 2) + 1));

            using (SolidBrush BG = new SolidBrush(BackgroundColor))
            {
                using (Pen P = new Pen(BorderColor , BorderThickness))
                {
                    G.FillRectangle(BG, r);
                    G.DrawRectangle(P, r);
                }
            }

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the background color.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor
        {
            get { return Color.Transparent; }
        }

        /// <summary>
        /// Gets the foreground color.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color ForeColor { get { return Color.Transparent; } }

        /// <summary>
        /// Gets or sets the border style.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new BorderStyle BorderStyle = BorderStyle.None;
               
        /// <summary>
        /// Gets or sets the border thickness the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the border thickness the control.")]
        public int BorderThickness { get; set; } = 1;

        /// <summary>
        /// Gets or sets bordercolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets bordercolor used by the control.")]
        public Color BorderColor { get; set; }

        /// <summary>
        /// Gets or sets backcolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets backcolor used by the control.")]
        [DisplayName("BackColor")]
        public Color BackgroundColor { get; set; }

        #endregion
    }
}