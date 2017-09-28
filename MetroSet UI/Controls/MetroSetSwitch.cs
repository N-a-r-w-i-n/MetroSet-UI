/**
 * MetroSet UI - MetroSet UI Framewrok
 * 
 * The MIT License (MIT)
 * Copyright (c) 2011 Narwin, https://github.com/N-a-r-w-i-n
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
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetSwitch), "Bitmaps.Switch.bmp")]
    [Designer(typeof(MetroSetSwitchDesigner))]
    [DefaultEvent("SwitchedChanged")]
    [DefaultProperty("Switched")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetSwitch : Control, iControl
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

        private SwitchProperties prop;
        private StyleManager _StyleManager;
        private bool _Switched;
        private Style style;
        private int Switchlocation = 0;
        private Timer timer;

        #endregion Internal Vars

        #region Constructors

        public MetroSetSwitch()
        {
            SetStyle(
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            BackColor = Color.Transparent;
            prop = new SwitchProperties();
            mth = new Methods();
            utl = new Utilites();
            timer = new Timer()
            {
                Interval = 1,
                Enabled = false
            };
            timer.Tick += SetCheckedChanged;
            style = Style.Light;
            ApplyTheme();
        }

        #endregion Constructors

        #region ApplyTheme

        public void SetProperties()
        {
            try
            {
                Enabled = prop.Enabled;
                Invalidate();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

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
                    prop.BorderColor = Color.FromArgb(165, 159, 147);
                    prop.DisabledBorderColor = Color.FromArgb(205, 205, 205);
                    prop.SymbolColor = Color.FromArgb(92, 92, 92);
                    prop.UnCheckColor = Color.FromArgb(155, 155, 155);
                    prop.CheckColor = Color.FromArgb(65, 177, 225);
                    prop.DisabledUnCheckColor = Color.FromArgb(200, 205, 205, 205);
                    prop.DisabledCheckColor = Color.FromArgb(100, 65, 177, 225);
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
                    prop.SymbolColor = Color.FromArgb(92, 92, 92);
                    prop.UnCheckColor = Color.FromArgb(155, 155, 155);
                    prop.CheckColor = Color.FromArgb(126, 56, 120);
                    prop.DisabledUnCheckColor = Color.FromArgb(200, 205, 205, 205);
                    prop.DisabledCheckColor = Color.FromArgb(100, 126, 56, 120);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    SetProperties();
                    break;

                case Style.Custom:

                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.SwitchBoxDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "Enabled":
                                    prop.Enabled = Convert.ToBoolean(varkey.Value);
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

                                case "SymbolColor":
                                    prop.SymbolColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "UnCheckColor":
                                    prop.UnCheckColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "CheckColor":
                                    prop.CheckColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledUnCheckColor":
                                    prop.DisabledUnCheckColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledCheckColor":
                                    prop.DisabledCheckColor = utl.HexColor((string)varkey.Value);
                                    break;

                                default:
                                    return;
                            }
                        }
                    SetProperties();
                    break;
            }
        }

        #endregion ApplyTheme

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            Rectangle rect = new Rectangle(1, 1, 56, 20);

            Rectangle rect2 = new Rectangle(3, 3, 52, 16);

            if (Enabled)
            {
                using (SolidBrush BackBrush = new SolidBrush(prop.BackColor))
                {
                    using (SolidBrush Checkback = new SolidBrush(Switched ? prop.CheckColor : prop.UnCheckColor))
                    {
                        using (SolidBrush CheckMarkBrush = new SolidBrush(prop.SymbolColor))
                        {
                            using (Pen P = new Pen(prop.BorderColor, 2))
                            {
                                G.FillRectangle(BackBrush, rect);

                                G.FillRectangle(Checkback, rect2);

                                G.DrawRectangle(P, rect);

                                G.FillRectangle(CheckMarkBrush, new Rectangle((Convert.ToInt32(rect.Width * (Switchlocation / 180.0))), 0, 16, 22));
                            }
                        }
                    }
                }
            }
            else
            {
                using (Brush BackBrush = new SolidBrush(prop.BackColor))
                {
                    using (Pen CheckMarkPen = new Pen(prop.DisabledBorderColor, 2))
                    {
                        using (SolidBrush Checkback = new SolidBrush(Switched ? prop.DisabledCheckColor : prop.DisabledUnCheckColor))
                        {
                            using (SolidBrush CheckMarkBrush = new SolidBrush(prop.SymbolColor))
                            {
                                G.FillRectangle(BackBrush, rect);

                                G.FillRectangle(Checkback, rect2);

                                G.DrawRectangle(CheckMarkPen, rect);

                                G.FillRectangle(CheckMarkBrush, new Rectangle((Convert.ToInt32(rect.Width * (Switchlocation / 180.0))), 0, 16, 22));
                            }
                        }
                    }
                }
            }
        }

        #endregion Draw Control

        #region Events

        public delegate void SwitchedChangedEventHandler(object sender);

        public event SwitchedChangedEventHandler SwitchedChanged;

        /// <summary>
        /// The Method that increases and decreases the location symbol which it make the control animate.
        /// </summary>
        /// <param name="o">object</param>
        /// <param name="args">EventArgs</param>
        public void SetCheckedChanged(object o, EventArgs args)
        {
            if (Switched)
            {
                if (Switchlocation < 131)
                {
                    Switchlocation += 5;
                    Invalidate(false);
                    if (Switchlocation == 132)
                        timer.Enabled = false;
                }
            }
            else
            {
                if (Switchlocation > 0)
                {
                    Switchlocation -= 5;
                    Invalidate(false);
                    if (Switchlocation == 0)
                        timer.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Here we will handle the checking state in runtime.
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (Switched)
            {
                Switched = false;
            }
            else
            {
                Switched = true;
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
            Size = new Size(58, 22);
            Invalidate();
        }

        #endregion Events

        #region Properties

        /// <summary>
        /// Specifies the state of a control, such as a check box, that can be checked, unchecked.
        /// </summary>
        [Browsable(false)]
        public Enums.CheckState CheckState { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the control is checked.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets a value indicating whether the control is checked.")]
        public bool Switched
        {
            get { return _Switched; }
            set
            {
                _Switched = value;
                SwitchedChanged?.Invoke(this);
                SetCheckedChanged(this, null);
                timer.Enabled = true;
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

        #endregion Properties
    }
}