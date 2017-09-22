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
    [ToolboxBitmap(typeof(MetroSetLabel), "Bitmaps.Label.bmp")]
    [Designer(typeof(MetroSetLabelDesigner))]
    [DefaultProperty("Text")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetLabel : Label, iControl
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
        [Category("MetroSet Framework")]
        public string ThemeName { get; set; }

        #endregion Interfaces

        #region Global Vars

        private static LabelProperties prop;
        private Methods mth;
        private Utilites utl;

        #endregion Global Vars

        #region Internal Vars

        private Style style;
        private StyleManager _StyleManager;

        #endregion Internal Vars

        #region Constructors

        public MetroSetLabel()
        {
            SetStyle(
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();

            BackColor = Color.Transparent;
            Font = new Font("Segoe UI", 10);
            prop = new LabelProperties();
            style = Style.Light;
            ApplyTheme();
            mth = new Methods();
            utl = new Utilites();
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
                    prop.FontSize = 10;
                    prop.Enabled = Enabled;
                    prop.ForeColor = Color.Black;
                    prop.BackColor = Color.Transparent;
                    Font = MetroSetFonts.SemiBold(prop.FontSize);
                    SetProperties();
                    break;

                case Style.Dark:
                    prop.FontSize = 10;
                    prop.Enabled = Enabled;
                    prop.ForeColor = Color.FromArgb(170, 170, 170);
                    prop.BackColor = Color.Transparent;
                    Font = MetroSetFonts.SemiBold(prop.FontSize);
                    SetProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.LabelDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "FontSize":
                                    prop.FontSize = Convert.ToInt32(varkey.Value);
                                    break;

                                case "Font":
                                    prop.Font = (string)varkey.Value;
                                    break;

                                case "Enabled":
                                    prop.Enabled = Convert.ToBoolean(varkey.Value);
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
                Font = new Font(prop.Font, prop.FontSize);
                Enabled = prop.Enabled;
                BackColor = prop.BackColor;
                ForeColor = prop.ForeColor;
                Font = MetroSetFonts.SemiBold(prop.FontSize);
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