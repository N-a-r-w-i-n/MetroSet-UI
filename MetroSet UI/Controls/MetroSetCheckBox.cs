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
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetCheckBox), "Bitmaps.CheckBox.bmp")]
    [Designer(typeof(MetroSetCheckBoxDesigner))]
    [DefaultEvent("CheckedChanged")]
    [DefaultProperty("Checked")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetCheckBox : Control, iControl
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

        private static CheckProperties prop;
        private Methods mth;
        private Utilites utl;

        #endregion Global Vars

        #region Internal Vars

        private Style style;
        private StyleManager _StyleManager;
        private bool _Checked;
        private Timer timer;
        private int Alpha = 0;

        #endregion Internal Vars

        #region Constructors

        public MetroSetCheckBox()
        {
            SetStyle(
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            Font = MetroSetFonts.SemiBold(10);
            BackColor = Color.Transparent;
            prop = new CheckProperties();
            mth = new Methods();
            utl = new Utilites();
            timer = new Timer()
            {
                Interval = 10,
                Enabled = false
            };
            timer.Tick += SetCheckedChanged;
            style = Style.Light;
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
                    prop.Enabled = Enabled;
                    prop.ForeColor = Color.Black;
                    prop.BackColor = Color.White;
                    prop.BorderColor = Color.FromArgb(155, 155, 155);
                    prop.DisabledBorderColor = Color.FromArgb(205, 205, 205);
                    prop.CheckSignColor = Color.FromArgb(65, 177, 225);
                    prop.CheckedStyle = SignStyle.Sign;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    SetProperties();
                    break;

                case Style.Dark:
                    prop.Enabled = Enabled;
                    prop.ForeColor = Color.FromArgb(170, 170, 170);
                    prop.BackColor = Color.FromArgb(30, 30, 30);
                    prop.BorderColor = Color.FromArgb(155, 155, 155);
                    prop.DisabledBorderColor = Color.FromArgb(85, 85, 85);
                    prop.CheckSignColor = Color.FromArgb(65, 177, 225);
                    prop.CheckedStyle = SignStyle.Sign;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    SetProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.CheckBoxDictionary)
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

                                case "DisabledBorderColor":
                                    prop.DisabledBorderColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "CheckColor":
                                    prop.CheckSignColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "CheckedStyle":
                                    switch ((string)varkey.Value)
                                    {
                                        case "Sign":
                                            prop.CheckedStyle = SignStyle.Sign;
                                            break;

                                        case "Shape":
                                            prop.CheckedStyle = SignStyle.Shape;
                                            break;
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
                ForeColor = prop.ForeColor;
                SignStyle = prop.CheckedStyle;
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
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            Rectangle rect = new Rectangle(0, 0, 16, 15);

            if (Enabled)
            {
                using (SolidBrush BackBrush = new SolidBrush(prop.BackColor))
                {
                    using (Pen CheckMarkPen = new Pen(Checked ? Color.FromArgb(Alpha, prop.CheckSignColor) : prop.BackColor, 2))
                    {
                        using (SolidBrush CheckMarkBrush = new SolidBrush(Checked ? Color.FromArgb(Alpha, prop.CheckSignColor) : prop.BackColor))
                        {
                            using (Pen P = new Pen(prop.BorderColor))
                            {
                                using (StringFormat SF = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center })
                                {
                                    using (SolidBrush TB = new SolidBrush(ForeColor))
                                    {
                                        G.FillRectangle(BackBrush, rect);
                                        G.DrawRectangle(P, rect);
                                        DrawSymbol(G, CheckMarkPen, CheckMarkBrush);

                                        G.DrawString(Text, Font, TB, new Rectangle(19, 2, Width, Height - 4), SF);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                using (Brush BackBrush = new SolidBrush(Color.FromArgb(238, 238, 238)))
                {
                    using (Pen CheckMarkPen = new Pen(Color.FromArgb(Alpha, prop.DisabledBorderColor)))
                    {
                        using (SolidBrush CheckMarkBrush = new SolidBrush(prop.DisabledBorderColor))
                        {
                            using (StringFormat SF = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center })
                            {
                                using (SolidBrush TB = new SolidBrush(ForeColor))
                                {
                                    G.FillRectangle(BackBrush, rect);
                                    G.DrawRectangle(CheckMarkPen, rect);
                                    DrawSymbol(G, CheckMarkPen, CheckMarkBrush);
                                    G.DrawString(Text, Font, TB, new Rectangle(19, 2, Width, Height - 4), SF);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void DrawSymbol(Graphics G, Pen pen, SolidBrush solidBrush)
        {
            switch (SignStyle)
            {
                case SignStyle.Sign:
                    G.SmoothingMode = SmoothingMode.AntiAlias;
                    G.DrawLines(pen, new Point[]
                    {
                    new Point(3, 7),
                    new Point(7, 10),
                    new Point(13, 3)
                    });
                    G.SmoothingMode = SmoothingMode.None;
                    break;

                case SignStyle.Shape:
                    G.FillRectangle(solidBrush, new Rectangle(3, 3, 11, 10));
                    break;
            }
        }

        #endregion Draw Control

        #region Events

        public event CheckedChangedEventHandler CheckedChanged;

        public delegate void CheckedChangedEventHandler(object sender);


        /// <summary>
        /// The Method that increases and decreases the alpha of check symbol which it make the control animate.
        /// </summary>
        /// <param name="o">object</param>
        /// <param name="args">EventArgs</param>
        public void SetCheckedChanged(object o, EventArgs args)
        {
            if (Checked)
            {
                if (Alpha < 255)
                {
                    Alpha += 1;
                    Invalidate();
                }
            }
            else if (Alpha > 0)
            {
                Alpha -= 1;
                Invalidate();
            }
        }

        /// <summary>
        /// Here we will handle the checking state in runtime.
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (Checked)
            {
                Checked = false;
            }
            else
            {
                Checked = true;
            }
            Invalidate();
        }

        /// <summary>
        /// Here we will set the limited height for the control to avoid high and low of the text drawing.
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 16;
            Invalidate();
        }

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the control is checked.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets a value indicating whether the control is checked.")]
        public bool Checked
        {
            get { return _Checked; }
            set
            {
                _Checked = value;
                CheckedChanged?.Invoke(this);
                SetCheckedChanged(this, null);
                timer.Enabled = value;
                switch (value)
                {
                    case true:
                        CheckState = Enums.CheckState.Checked;
                        break;
                    case false:
                        CheckState = Enums.CheckState.Unchecked;
                        break;
                }
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the the sign style of check.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the the sign style of check.")]
        public SignStyle SignStyle { get; set; }

        /// <summary>
        /// Specifies the state of a control, such as a check box, that can be checked, unchecked.
        /// </summary>
        [Browsable(false)]
        public Enums.CheckState CheckState { get; set; }

        #endregion Properties

    }
}