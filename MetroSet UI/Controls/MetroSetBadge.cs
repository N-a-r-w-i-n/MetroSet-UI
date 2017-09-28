using MetroSet_UI.Design;
using MetroSet_UI.Enums;
using MetroSet_UI.Extensions;
using MetroSet_UI.Interfaces;
using MetroSet_UI.Property;
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
    [ToolboxBitmap(typeof(MetroSetBadge), "Bitmaps.Button.bmp")]
    [Designer(typeof(MetroSetBadgeDesigner))]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetBadge : Control, iControl
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
        private static BadgeProperties prop;

        #endregion Internal Vars

        #region Constructors

        public MetroSetBadge()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            BackColor = Color.Transparent;
            prop = new BadgeProperties();
            Font = MetroSetFonts.Light(10);
            utl = new Utilites();
            mth = new Methods();
            style = Style.Light;
            ApplyTheme();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the badge alignment associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the badge alignment associated with the control.")]
        public BadgeAlign BadgeAlignment { get; set; } = BadgeAlign.TopRight;

        /// <summary>
        /// Gets or sets the badge text associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the badge text associated with the control.")]
        public string BadgeText { get; set; } = "3";
        
        [Category("MetroSet Framework")]
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


        #endregion Properties

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            Rectangle r = default(Rectangle);
            Rectangle badge = default(Rectangle);
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            switch (BadgeAlignment)
            {
                case BadgeAlign.Topleft:
                    r = new Rectangle(18, 18, Width - 21, Height - 21);
                    badge = new Rectangle(5, 5, 29, 29);
                    break;

                case BadgeAlign.TopRight:
                    r = new Rectangle(0, 18, Width - 18, Height - 21);
                    badge = new Rectangle(Width - 35, 1, 29, 29);
                    break;

                case BadgeAlign.BottmLeft:
                    r = new Rectangle(18, 0, Width - 19, Height - 18);
                    badge = new Rectangle(1, Height - 35, 29, 29);
                    break;

                case BadgeAlign.BottomRight:
                    r = new Rectangle(0, 0, Width - 19, Height - 18);
                    badge = new Rectangle(Width - 35, Height - 35, 29, 29);
                    break;
            }

            switch (State)
            {
                case MouseMode.Normal:

                    using (SolidBrush BG = new SolidBrush(prop.NormalColor))
                    using (Pen P = new Pen(prop.NormalBorderColor))
                    using (SolidBrush TB = new SolidBrush(prop.NormalTextColor))
                    using (SolidBrush bdgBrush = new SolidBrush(prop.NormalBadgeColor))
                    using (SolidBrush bdgtxtBrush = new SolidBrush(prop.NormalBadgeTextColor))
                    {
                        G.FillRectangle(BG, r);
                        G.DrawRectangle(P, r);
                        G.DrawString(Text, Font, TB, r, mth.SetPosition());
                        SmoothingType(G);
                        G.FillEllipse(bdgBrush, badge);
                        G.DrawString(BadgeText, Font, bdgtxtBrush, badge, mth.SetPosition());
                    }

                    break;

                case MouseMode.Hovered:

                    Cursor = Cursors.Hand;
                    using (SolidBrush BG = new SolidBrush(prop.HoverColor))
                    using (Pen P = new Pen(prop.HoverBorderColor))
                    using (SolidBrush TB = new SolidBrush(prop.HoverTextColor))
                    using (SolidBrush bdgBrush = new SolidBrush(prop.HoverBadgeColor))
                    using (SolidBrush bdgtxtBrush = new SolidBrush(prop.HoverBadgeTextColor))
                    {
                        G.FillRectangle(BG, r);
                        G.DrawRectangle(P, r);
                        G.DrawString(Text, Font, TB, r, mth.SetPosition());
                        SmoothingType(G);
                        G.FillEllipse(bdgBrush, badge);
                        G.DrawString(BadgeText, Font, bdgtxtBrush, badge, mth.SetPosition());
                    }

                    break;

                case MouseMode.Pushed:

                    using (SolidBrush BG = new SolidBrush(prop.PressColor))
                    using (Pen P = new Pen(prop.PressBorderColor))
                    using (SolidBrush TB = new SolidBrush(prop.PressTextColor))
                    using (SolidBrush bdgBrush = new SolidBrush(prop.PressBadgeColor))
                    using (SolidBrush bdgtxtBrush = new SolidBrush(prop.PressBadgeTextColor))
                    {
                        G.FillRectangle(BG, r);
                        G.DrawRectangle(P, r);
                        G.DrawString(Text, Font, TB, r, mth.SetPosition());
                        SmoothingType(G);
                        G.FillEllipse(bdgBrush, badge);
                        G.DrawString(BadgeText, Font, bdgtxtBrush, badge, mth.SetPosition());
                    }

                    break;

                case MouseMode.Disabled:

                    using (SolidBrush BG = new SolidBrush(prop.DisabledBackColor))
                    using (Pen P = new Pen(prop.DisabledBorderColor))
                    using (SolidBrush TB = new SolidBrush(prop.DisabledForeColor))
                    using (SolidBrush bdgBrush = new SolidBrush(prop.PressBadgeColor))
                    using (SolidBrush bdgtxtBrush = new SolidBrush(prop.PressBadgeTextColor))
                    {
                        G.FillRectangle(BG, r);
                        G.DrawRectangle(P, r);
                        G.DrawString(Text, Font, TB, r, mth.SetPosition());
                        SmoothingType(G);
                        G.FillEllipse(bdgBrush, badge);
                        G.DrawString(BadgeText, Font, bdgtxtBrush, badge, mth.SetPosition());
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
                    prop.NormalColor = Color.FromArgb(238, 238, 238);
                    prop.NormalBorderColor = Color.FromArgb(204, 204, 204);
                    prop.NormalTextColor = Color.Black;
                    prop.HoverColor = Color.FromArgb(102, 102, 102);
                    prop.HoverBorderColor = Color.FromArgb(102, 102, 102);
                    prop.HoverTextColor = Color.White;
                    prop.PressColor = Color.FromArgb(51, 51, 51);
                    prop.PressBorderColor = Color.FromArgb(51, 51, 51);
                    prop.PressTextColor = Color.White;
                    prop.NormalBadgeColor = Color.FromArgb(65, 177, 225);
                    prop.NormalBadgeTextColor = Color.White;
                    prop.HoverBadgeColor = Color.FromArgb(85, 187, 245);
                    prop.HoverBadgeTextColor = Color.White;
                    prop.PressBadgeColor = Color.FromArgb(45, 147, 205);
                    prop.PressBadgeTextColor = Color.White;
                    prop.DisabledBackColor = Color.FromArgb(204, 204, 204);
                    prop.DisabledBorderColor = Color.FromArgb(155, 155, 155);
                    prop.DisabledForeColor = Color.FromArgb(136, 136, 136);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    break;

                case Style.Dark:
                    prop.NormalColor = Color.FromArgb(32, 32, 32);
                    prop.NormalBorderColor = Color.FromArgb(64, 64, 64);
                    prop.NormalTextColor = Color.FromArgb(204, 204, 204);
                    prop.HoverColor = Color.FromArgb(170, 170, 170);
                    prop.HoverBorderColor = Color.FromArgb(170, 170, 170);
                    prop.HoverTextColor = Color.White;
                    prop.PressColor = Color.FromArgb(240, 240, 240);
                    prop.PressBorderColor = Color.FromArgb(240, 240, 240);
                    prop.PressTextColor = Color.White;
                    prop.NormalBadgeColor = Color.FromArgb(65, 177, 225);
                    prop.NormalBadgeTextColor = Color.White;
                    prop.HoverBadgeColor = Color.FromArgb(85, 187, 245);
                    prop.HoverBadgeTextColor = Color.White;
                    prop.PressBadgeColor = Color.FromArgb(45, 147, 205);
                    prop.PressBadgeTextColor = Color.White;
                    prop.DisabledBackColor = Color.FromArgb(80, 80, 80);
                    prop.DisabledBorderColor = Color.FromArgb(109, 109, 109);
                    prop.DisabledForeColor = Color.FromArgb(109, 109, 109);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.BadgeDictionary)
                        {
                            if ((varkey.Key == null) || varkey.Key == null)
                            {
                                return;
                            }

                            if (varkey.Key == "NormalColor")
                            {
                                prop.NormalColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "NormalBorderColor")
                            {
                                prop.NormalBorderColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "NormalTextColor")
                            {
                                prop.NormalTextColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "HoverColor")
                            {
                                prop.HoverColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "HoverBorderColor")
                            {
                                prop.HoverBorderColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "HoverTextColor")
                            {
                                prop.HoverTextColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "PressColor")
                            {
                                prop.PressColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "PressBorderColor")
                            {
                                prop.PressBorderColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "PressTextColor")
                            {
                                prop.PressTextColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "NormalBadgeColor")
                            {
                                prop.NormalBadgeColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "NormalBadgeTextColor")
                            {
                                prop.NormalBadgeTextColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "HoverBadgeColor")
                            {
                                prop.HoverBadgeColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "HoverBadgeTextColor")
                            {
                                prop.HoverBadgeTextColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "PressBadgeColor")
                            {
                                prop.PressBadgeColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "PressBadgeTextColor")
                            {
                                prop.PressBadgeTextColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "DisabledBackColor")
                            {
                                prop.DisabledBackColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "DisabledBorderColor")
                            {
                                prop.DisabledBorderColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "DisabledForeColor")
                            {
                                prop.DisabledForeColor = utl.HexColor((string)varkey.Value);
                            }
                        }
                    Invalidate();
                    break;
            }
        }

        #endregion Theme Changing

        #region Events

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseMode.Hovered;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseMode.Pushed;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseMode.Hovered;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseMode.Normal;
            Invalidate();
        }

        #endregion Events

        #region Methods

        /// <summary>
        /// Sets the smoothingmode the the specific graphics.
        /// </summary>
        /// <param name="e">Graphics to Set the effect.</param>
        /// <param name="state">state of smoothingmode.</param>
        public void SmoothingType(Graphics e, SmoothingMode state = SmoothingMode.AntiAlias)
        {
            e.SmoothingMode = state;
        }

        #endregion Methods

    }
}