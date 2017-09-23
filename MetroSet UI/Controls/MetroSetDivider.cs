using MetroSet_UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MetroSet_UI.Design;
using MetroSet_UI.Property;
using System.ComponentModel;
using MetroSet_UI.Extensions;
using System.Drawing;
using MetroSet_UI.Enums;
using System.Runtime.InteropServices;

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
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            BackColor = Color.Transparent;
            prop = new DividerProperties();
            mth = new Methods();
            utl = new Utilites();
            style = Style.Light;
            ApplyTheme();
        }

        #endregion Constructors

        #region Theme Changing

        /// <summary>
        /// Gets or sets the style provided by the user.
        /// </summary>
        /// <param name="style">The Style.</param>
        internal void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    prop.Orientation = DividerStyle.Horizontal;
                    prop.Thickness = 1;
                    prop.ForeColor = Color.Black;
                    prop.BackColor = Color.Transparent;
                    SetProperties();
                    break;

                case Style.Dark:
                    prop.Orientation = DividerStyle.Horizontal;
                    prop.Thickness = 1;
                    prop.ForeColor = Color.FromArgb(170, 170, 170);
                    prop.BackColor = Color.Transparent;
                    SetProperties();
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
                                    else if((string)varkey.Value == "Vertical")
                                    {
                                        prop.Orientation = DividerStyle.Vertical;
                                    }
                                    break;

                                case "Thickness":
                                    prop.Thickness =((int)varkey.Value);
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
                    SetProperties();
                    break;
            }
        }

        public void SetProperties()
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

        #endregion Theme Changing

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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the style associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets Orientation of the control.")]
        public DividerStyle Orientation
        {
            get { return prop.Orientation; }
            set { prop.Orientation = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the divider thickness.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the divider thickness.")]
        public int Thickness
        {
            get { return prop.Thickness; }
            set { prop.Thickness = value; Invalidate(); }
        }

        #endregion

        #region Events

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

        #endregion

    }
}
