using MetroSet_UI.Design;
using MetroSet_UI.Extensions;
using MetroSet_UI.Interfaces;
using MetroSet_UI.Property;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true), ToolboxBitmap(typeof(MetroSetButton), "Button.bmp")]
    public class MetroSetButton : Control, iControl
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

        #endregion Global Vars

        #region Internal Vars

        private MouseMode State;
        private Style style;
        private StyleManager _StyleManager;
        private static ButtonProperties prop;

        #endregion Internal Vars

        #region Constructors

        public MetroSetButton()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            prop = new ButtonProperties();
            BackColor = Color.Transparent;
            Font = new Font("Segoe UI", 10);
            style = Style.Light;
            ApplyTheme();
            mth = new Methods();
        }

        #endregion Constructors

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            Rectangle r = new Rectangle(0, 0, Width - 1, Height - 1);
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            switch (State)
            {
                case MouseMode.Normal:

                    using (SolidBrush BG = new SolidBrush(prop.NormalColor))
                    using (Pen P = new Pen(prop.NormalBorderColor))
                    using (SolidBrush TB = new SolidBrush(prop.NormalTextColor))
                    {
                        G.FillRectangle(BG, r);
                        G.DrawRectangle(P, r);
                        G.DrawString(Text, Font, TB, new Rectangle(0, 0, Width, Height), mth.SetPosition());
                    }

                    break;

                case MouseMode.Hovered:

                    Cursor = Cursors.Hand;
                    using (SolidBrush BG = new SolidBrush(prop.HoverColor))
                    using (Pen P = new Pen(prop.HoverBorderColor))
                    using (SolidBrush TB = new SolidBrush(prop.HoverTextColor))
                    {
                        G.FillRectangle(BG, r);
                        G.DrawRectangle(P, r);
                        G.DrawString(Text, Font, TB, new Rectangle(0, 0, Width, Height), mth.SetPosition());
                    }

                    break;

                case MouseMode.Pushed:

                    using (SolidBrush BG = new SolidBrush(prop.PressColor))
                    using (Pen P = new Pen(prop.PressBorderColor))
                    using (SolidBrush TB = new SolidBrush(prop.PressTextColor))
                    {
                        G.FillRectangle(BG, r);
                        G.DrawRectangle(P, r);
                        G.DrawString(Text, Font, TB, new Rectangle(0, 0, Width, Height), mth.SetPosition());
                    }

                    break;
            }
        }

        #endregion Draw Control

        #region Theme Changing

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
                    prop.FontSize = 10;
                    prop.NormalColor = Color.FromArgb(65, 177, 225);
                    prop.NormalBorderColor = Color.Black;
                    prop.NormalTextColor = Color.White;
                    prop.HoverColor = Color.FromArgb(75, 187, 235);
                    prop.HoverBorderColor = Color.Black;
                    prop.HoverTextColor = Color.White;
                    prop.PressColor = Color.FromArgb(55, 167, 215);
                    prop.PressBorderColor = Color.Black;
                    prop.PressTextColor = Color.White;
                    Font = Global_Font.Regular(prop.FontSize);
                    break;

                case Style.Dark:
                    prop.FontSize = 10;
                    prop.NormalColor = Color.FromArgb(255, 196, 13);
                    prop.NormalBorderColor = Color.Black;
                    prop.NormalTextColor = Color.White;
                    prop.HoverColor = Color.FromArgb(75, 187, 235);
                    prop.HoverBorderColor = Color.Black;
                    prop.HoverTextColor = Color.White;
                    prop.PressColor = Color.FromArgb(55, 167, 215);
                    prop.PressBorderColor = Color.Black;
                    prop.PressTextColor = Color.White;
                    Font = Global_Font.Regular(prop.FontSize);
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.ButtonDictionary)
                        {
                            if ((varkey.Key == null) || varkey.Key == null)
                            {
                                return;
                            }

                            if (varkey.Key == "FontSize")
                            {
                                prop.FontSize = Convert.ToInt32(varkey.Value);
                            }
                            else if (varkey.Key == "NormalColor")
                            {
                                prop.NormalColor = colorC((string)varkey.Value);
                            }
                            else if (varkey.Key == "NormalBorderColor")
                            {
                                prop.NormalBorderColor = colorC((string)varkey.Value);
                            }
                            else if (varkey.Key == "NormalTextColor")
                            {
                                prop.NormalTextColor = colorC((string)varkey.Value);
                            }
                            else if (varkey.Key == "HoverColor")
                            {
                                prop.HoverColor = colorC((string)varkey.Value);
                            }
                            else if (varkey.Key == "HoverBorderColor")
                            {
                                prop.HoverBorderColor = colorC((string)varkey.Value);
                            }
                            else if (varkey.Key == "HoverTextColor")
                            {
                                prop.HoverTextColor = colorC((string)varkey.Value);
                            }
                            else if (varkey.Key == "PressColor")
                            {
                                prop.PressColor = colorC((string)varkey.Value);
                            }
                            else if (varkey.Key == "PressBorderColor")
                            {
                                prop.PressBorderColor = colorC((string)varkey.Value);
                            }
                            else if (varkey.Key == "PressTextColor")
                            {
                                prop.PressTextColor = colorC((string)varkey.Value);
                            }
                        }
                    Font = Global_Font.Light(prop.FontSize);
                    Refresh();
                    break;
            }
        }

        #endregion Theme Changing

        #region Helper Methods

        /// <summary>
        /// Gets the value of specific key.
        /// </summary>
        /// <param name="item">The Key</param>
        /// <returns></returns>

        private Color colorC(string htmlColor)
        {
            return ColorTranslator.FromHtml(htmlColor);
        }

        #endregion Helper Methods

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
    }
}