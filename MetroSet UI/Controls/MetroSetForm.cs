using MetroSet_UI.Design;
using MetroSet_UI.Extensions;
using MetroSet_UI.Interfaces;
using MetroSet_UI.Native;
using MetroSet_UI.Property;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static MetroSet_UI.Native.User32;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(MetroSetForm), "Bitmaps.Form.bmp")]
    [DesignerCategory("Form")]
    [DefaultEvent("Load")]
    [DesignTimeVisible(false)]
    [ComVisible(true)]
    [InitializationEvent("Load")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetForm : Form, iForm
    {
        #region Properties

        /// <summary>
        /// Gets or sets the width of the small rectangle on top left of the window.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the width of the small rectangle on top left of the window.")]
        public int SmallRectThickness { get; set; } = 10;

        /// <summary>
        /// Gets or sets whether the border be shown.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets whether the border be shown."), DefaultValue(true)]
        public bool ShowBorder { get; set; } = true;

        /// <summary>
        /// Gets or sets the border thickness.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the border thickness.")]
        public float BorderThickness { get; set; } = 1;

        /// <summary>Gets or sets the border style of the form.</summary>
        [DefaultValue(FormBorderStyle.None)]
        [Browsable(false)]
        public new FormBorderStyle FormBorderStyle
        {
            get
            {
                return FormBorderStyle.None;
            }
            set
            {
                base.FormBorderStyle = FormBorderStyle.None;
            }
        }

        /// <summary>Gets or sets a value indicating whether the Maximize button is displayed in the caption bar of the form.</summary>
        /// <returns>true to display a Maximize button for the form; otherwise, false. The default is true.</returns>
        [Category("WindowStyle")]
        [Browsable(false)]
        [DefaultValue(false)]
        [Description("FormMaximizeBox")]
        public new bool MaximizeBox
        {
            get
            {
                return false;
            }
            set
            {
                value = false;
            }
        }

        /// <summary>Gets or sets a value indicating whether the Minimize button is displayed in the caption bar of the form.</summary>
        /// <returns>true to display a Minimize button for the form; otherwise, false. The default is true.</returns>
        [Category("WindowStyle")]
        [Browsable(false)]
        [DefaultValue(false)]
        [Description("FormMinimizeBox")]
        public new bool MinimizeBox
        {
            get
            {
                return false;
            }
            set
            {
                value = false;
            }
        }

        /// <summary>
        /// Gets or sets whether the title be shown.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets whether the title be shown.")]
        public bool ShowTitle { get; set; } = true;

        /// <summary>
        /// Gets or sets the title alignment.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the title alignment.")]
        public TextAlign TextAlign
        {
            get { return textAlign; }
            set { textAlign = value; Refresh(); }
        }


        /// <summary>
        /// Gets or sets whether show the header.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets whether show the header.")]
        public bool ShowHeader
        {
            get { return showHeader; }
            set
            {
                showHeader = value;
                if (value)
                {
                    ShowLeftRect = false;
                    Padding = new Padding(2, prop.HeaderHeight + 2, 2, 2);
                    Text = Text.ToUpper();
                    prop.TextColor = Color.White;
                }
                else
                {
                    Padding = new Padding(12, 70, 12, 12);
                }
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets whether the small rectangle on top left of the window be shown.
        /// </summary>
        [Category("MetroSet Framework"),
         Description("Gets or sets whether the small rectangle on top left of the window be shown.")]
        public bool ShowLeftRect
        {
            get { return showLeftRect; }
            set
            {
                showLeftRect = value;
                if (value)
                {
                    ShowHeader = false;
                }
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets whether the form can be move or not.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets whether the form can be move or not."), DefaultValue(true)]
        public bool Moveable { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the form use animation.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets whether the form use animation.")]
        public bool UseSlideAnimation { get; set; } = false;

        [Browsable(false)]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        #endregion Properties

        #region Constructor

        public MetroSetForm()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                        ControlStyles.ResizeRedraw |
                        ControlStyles.OptimizedDoubleBuffer |
                        ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            Padding = new Padding(12, 70, 12, 12);
            FormBorderStyle = FormBorderStyle.None;
            Font = MetroSetFonts.SemiLight(13);
            prop = new FormProperties();
            utl = new Utilites();
            user32 = new User32();
            textAlign = TextAlign.Left;
            showLeftRect = true;
            showHeader = false;
            style = Style.Light;
            ApplyTheme();
        }

        #endregion Constructor

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            e.Graphics.InterpolationMode = InterpolationMode.High;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

            using (SolidBrush B = new SolidBrush(prop.BackgroundColor))
            {
                e.Graphics.FillRectangle(B, new Rectangle(0, 0, Width, Height));
                if (BackgroundImage != null)
                {
                    using (TextureBrush tb = new TextureBrush(BackgroundImage))
                    {
                        e.Graphics.FillRectangle(tb, new Rectangle(0, 0, Width, Height));
                    }
                }
            }
            if (ShowBorder)
            {
                using (Pen P = new Pen(prop.BorderColor, BorderThickness))
                {
                    e.Graphics.DrawRectangle(P, new Rectangle(0, 0, Width - 1, Height - 1));
                }
            }
            

            if (ShowLeftRect)
            {
                using (LinearGradientBrush B = new LinearGradientBrush(new Rectangle(0, 25, SmallRectThickness, 35), prop.SmallLineColor1, prop.SmallLineColor2, 90))
                {
                    using (SolidBrush textBrush = new SolidBrush(prop.TextColor))
                    {
                        e.Graphics.FillRectangle(B, new Rectangle(0, 25, 10, 35));
                        e.Graphics.DrawString(Text, Font, textBrush, new Point(20, 32));
                    }
                }
            }
            else
            {
                int height = prop.HeaderHeight;
                if (ShowHeader)
                {
                    using (SolidBrush B = new SolidBrush(prop.HeaderColor))
                    {
                       
                        e.Graphics.FillRectangle(B, new Rectangle(1, 1, Width - 1, height));
                    }
                }

                SolidBrush textBrush = new SolidBrush(prop.TextColor);
                if (ShowTitle)
                {
                    switch (TextAlign)
                    {
                        case TextAlign.Left:
                            using (StringFormat stringFormat = new StringFormat() {LineAlignment = StringAlignment.Center})
                            {
                                e.Graphics.DrawString(Text, Font, textBrush, new Rectangle(12, 0, Width, height), stringFormat);
                            }
                            break;

                        case TextAlign.Center:
                            using (StringFormat stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center})
                            {
                                e.Graphics.DrawString(Text, Font, textBrush, new Rectangle(20, 0, Width - 21, height), stringFormat);
                            }
                            break;

                        case TextAlign.Right:
                            using (StringFormat stringFormat = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center })
                            {
                                e.Graphics.DrawString(Text, Font, textBrush, new Rectangle(20, 0, Width - 26, height), stringFormat);
                            }
                            break;
                    }
                }
                textBrush.Dispose();
            }
        }

        #endregion Draw Control

        #region Methods

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST)
            {
                if (Moveable)
                {
                    if ((int)m.Result == HTCLIENT)
                        m.Result = new IntPtr(HTCAPTION);
                }
            }
        }

        #endregion Methods

        #region Interfaces

        /// <summary>
        /// Gets or sets the style associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the style associated with the control."), DefaultValue(Style.Light)]
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
            set
            {
                _StyleManager = value;
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

        internal User32 User32 => User321;

        internal User32 User321 => user32;

        #endregion Interfaces

        #region Global Vars

        private readonly Utilites utl;
        private readonly User32 user32;

        #endregion Global Vars

        #region Internal Vars

        private Style style;
        private FormProperties prop;
        private TextAlign textAlign;
        private const int WM_NCHITTEST = 0x0084;
        private const int HTCLIENT = 0x01;
        private const int HTCAPTION = 0x02;
        private StyleManager _StyleManager;
        private bool showLeftRect;
        private bool showHeader;

        #endregion Internal Vars

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
                    prop.Enabled = Enabled;
                    prop.ForeColor = Color.Gray;
                    prop.BackgroundColor = Color.White;
                    prop.BorderColor = Color.FromArgb(65, 177, 225);                    
                    if (ShowHeader)
                    {
                        prop.TextColor = Color.White;
                    }
                    else
                    {
                        prop.TextColor = Color.Gray;
                    }
                    prop.DrawLeftRect = true;
                    prop.SmallLineColor1 = Color.FromArgb(65, 177, 225);
                    prop.SmallLineColor2 = Color.FromArgb(65, 177, 225);
                    prop.HeaderColor = Color.FromArgb(65, 177, 225);
                    prop.HeaderHeight = 35;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    SetProperties();
                    break;

                case Style.Dark:
                    prop.Enabled = Enabled;
                    prop.ForeColor = Color.White;
                    prop.BackgroundColor = Color.FromArgb(30, 30, 30);
                    prop.BorderColor = Color.FromArgb(126, 56, 120);
                    prop.SmallLineColor1 = Color.FromArgb(126, 56, 120);
                    prop.SmallLineColor2 = Color.FromArgb(126, 56, 120);
                    prop.HeaderColor = Color.FromArgb(126, 56, 120);
                    prop.HeaderHeight = 35;

                    if (ShowHeader)
                    {
                        prop.TextColor = Color.Gray;
                    }
                    else
                    {
                    prop.TextColor = Color.White;
                    }
                    prop.DrawLeftRect = true;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    SetProperties();
                    break;

                case Style.Custom:
                    if ((StyleManager != null))
                        foreach (var varkey in StyleManager.FormDictionary)
                        {
                            if ((String.Equals(varkey.Key, null, StringComparison.Ordinal)) || varkey.Key == null)
                            {
                                throw new Exception("FormDictionary is empty");
                            }
                            if (varkey.Key == "Enabled")
                            {
                                prop.Enabled = Convert.ToBoolean(varkey.Value);
                            }
                            else if (varkey.Key == "ForeColor")
                            {
                                prop.ForeColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "BackColor")
                            {
                                prop.BackgroundColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "BorderColor")
                            {
                                prop.BorderColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "TextColor")
                            {
                                prop.TextColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "DrawLeftRect")
                            {
                                prop.DrawLeftRect = Convert.ToBoolean(varkey.Value);
                            }
                            else if (varkey.Key == "ShowTitle")
                            {
                                prop.DisplayHeader = Convert.ToBoolean(varkey.Value);
                            }
                            else if (varkey.Key == "TextAlign")
                            {
                                switch (varkey.Value.ToString().ToLower())
                                {
                                    case "left":
                                        prop.TextAlign = TextAlign.Left;
                                        break;

                                    case "right":
                                        prop.TextAlign = TextAlign.Right;
                                        break;

                                    case "center":
                                        prop.TextAlign = TextAlign.Center;
                                        break;
                                }
                            }
                            else if (varkey.Key == "SmallLineColor1")
                            {
                                prop.SmallLineColor1 = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "SmallLineColor2")
                            {
                                prop.SmallLineColor2 = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "SmallRectThickness")
                            {
                                SmallRectThickness = int.Parse(varkey.Value.ToString());
                            }
                            else if (varkey.Key == "ShowHeader")
                            {
                                ShowHeader = Convert.ToBoolean(varkey.Value);
                            }
                            else if (varkey.Key == "HeaderColor")
                            {
                                prop.HeaderColor = utl.HexColor((string)varkey.Value);
                            }
                            else if (varkey.Key == "HeaderHeight")
                            {
                                prop.HeaderHeight = int.Parse(varkey.Value.ToString());
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
                ShowTitle = prop.DisplayHeader;
                ShowLeftRect = prop.DrawLeftRect;
                TextAlign = prop.TextAlign;
                ForeColor = prop.ForeColor;
                Refresh();
            }
            catch (Exception ex) { throw new Exception(ex.StackTrace); }
        }

        #endregion Theme Changing

        #region Events

        protected override void OnHandleCreated(EventArgs e)
        {
            AutoScaleMode = AutoScaleMode.None;
            base.OnHandleCreated(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // https://www.codeproject.com/Articles/30255/C-Fade-Form-Effect-With-the-AnimateWindow-API-Func
            AnimateWindow(Handle, 1000, AnimateWindowFlags.AW_ACTIVATE | (UseSlideAnimation ?
                  AnimateWindowFlags.AW_HOR_POSITIVE | AnimateWindowFlags.AW_SLIDE : AnimateWindowFlags.AW_BLEND));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            // https://www.codeproject.com/Articles/30255/C-Fade-Form-Effect-With-the-AnimateWindow-API-Func
            if (e.Cancel == false)
            {
                AnimateWindow(Handle, 1000, User32.AW_HIDE | (UseSlideAnimation ?
                              AnimateWindowFlags.AW_HOR_NEGATIVE | AnimateWindowFlags.AW_SLIDE : AnimateWindowFlags.AW_BLEND));
            }
        }

        #endregion

    }

}