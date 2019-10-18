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
using Timer = System.Timers.Timer;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetNumeric), "Bitmaps.Numeric.bmp")]
    [Designer(typeof(MetroSetNumericDesigner))]
    [DefaultProperty("Text")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetNumeric : Control, iControl
    {

        #region Interfaces

        /// <summary>
        /// Gets or sets the style associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the style associated with the control.")]
        public Style Style
        {
            get => StyleManager?.Style ?? _style;
            set
            {
                _style = value;
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
            get => _styleManager;
            set { _styleManager = value; Invalidate(); }
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

        private readonly Methods _mth;
        private readonly Utilites _utl;

        #endregion Global Vars

        #region Internal Vars

        private Style _style;
        private StyleManager _styleManager;
        private Point _point;
        private int _value;
        private readonly Timer _holdTimer;

        #endregion Internal Vars

        #region Constructors

        public MetroSetNumeric()
        {
            SetStyle(
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
            Font = MetroSetFonts.SemiLight(10);
            BackColor = Color.Transparent;
            _mth = new Methods();
            _utl = new Utilites();
            ApplyTheme();
            _point = new Point(0, 0);
            _holdTimer = new Timer()
            {
                Interval = 10,
                AutoReset = true,
                Enabled = false
            };
            _holdTimer.Elapsed += HoldTimer_Tick;
        }

        #endregion Constructors

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);

            const char plus = '+';
            const char minus = '-';

            using (var bg = new SolidBrush(Enabled ? BackColor : DisabledBackColor))
            {
                using (var p = new Pen(Enabled ? BorderColor : DisabledBorderColor))
                {
                    using (var s = new SolidBrush(Enabled ? SymbolsColor : DisabledForeColor))
                    {
                        using (var tb = new SolidBrush(Enabled ? ForeColor : DisabledForeColor))
                        {
                            using (var f2 = MetroSetFonts.SemiBold(18))
                            {
                                using (var sf = new StringFormat { LineAlignment = StringAlignment.Center })
                                {
                                    G.FillRectangle(bg, rect);
                                    G.DrawString(plus.ToString(), f2, s, new Rectangle(Width - 45, 1, 25, Height - 1), sf);
                                    G.DrawString(minus.ToString(), f2, s, new Rectangle(Width - 25, -1, 20, Height - 1), sf);
                                    G.DrawString(Value.ToString(), Font, tb, new Rectangle(0, 0, Width - 50, Height - 1), _mth.SetPosition(StringAlignment.Far));
                                    G.DrawRectangle(p, rect);
                                }
                            }
                        }
                    }
                }
            }

        }

        #endregion

        #region ApplyTheme

        /// <summary>
        /// Gets or sets the style provided by the user.
        /// </summary>
        /// <param name="style">The Style.</param>
        private void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    ForeColor = Color.FromArgb(20, 20, 20);
                    BackColor = Color.White;
                    BorderColor = Color.FromArgb(150, 150, 150);
                    SymbolsColor = Color.FromArgb(128, 128, 128);
                    DisabledBackColor = Color.FromArgb(204, 204, 204);
                    DisabledBorderColor = Color.FromArgb(155, 155, 155);
                    DisabledForeColor = Color.FromArgb(136, 136, 136);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;

                case Style.Dark:
                    ForeColor = Color.FromArgb(204, 204, 204);
                    BackColor = Color.FromArgb(34, 34, 34);
                    BorderColor = Color.FromArgb(110, 110, 110);
                    SymbolsColor = Color.FromArgb(110, 110, 110);
                    DisabledBackColor = Color.FromArgb(80, 80, 80);
                    DisabledBorderColor = Color.FromArgb(109, 109, 109);
                    DisabledForeColor = Color.FromArgb(109, 109, 109);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.NumericDictionary)
                        {
                            switch (varkey.Key)
                            {

                                case "ForeColor":
                                    ForeColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "BackColor":
                                    BackColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "BorderColor":
                                    BorderColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "SymbolsColor":
                                    SymbolsColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledBackColor":
                                    DisabledBackColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledBorderColor":
                                    DisabledBorderColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledForeColor":
                                    DisabledForeColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                default:
                                    return;
                            }
                        }
                    UpdateProperties();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
        }

        private void UpdateProperties()
        {
            Invalidate();
        }

        #endregion Theme Changing

        #region Properties

        /// <summary>
        /// Gets or sets the maximum number of the Numeric.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the maximum number of the Numeric.")]
        public int Maximum { get; set; } = 100;

        /// <summary>
        /// Gets or sets the minimum number of the Numeric.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the minimum number of the Numeric.")]
        public int Minimum { get; set; } = 0;

        /// <summary>
        /// Gets or sets the current number of the Numeric.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the current number of the Numeric.")]
        public int Value
        {
            get => _value;
            set
            {
                if (value <= Maximum & value >= Minimum)
                    _value = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        public sealed override Color BackColor => Color.Transparent;

        /// <summary>
        /// Gets or sets the control backcolor.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the control backcolor.")]
        [DisplayName("BackColor")]
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the forecolor of the control whenever while disabled
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the forecolor of the control whenever while disabled.")]
        public Color DisabledForeColor { get; set; }

        /// <summary>
        /// Gets or sets disabled backcolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets disabled backcolor used by the control.")]
        public Color DisabledBackColor { get; set; }

        /// <summary>
        /// Gets or sets the border color while the control disabled.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the border color while the control disabled.")]
        public Color DisabledBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the border color.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the border color.")]
        public Color BorderColor { get; set; }

        /// <summary>
        /// Gets or sets forecolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets forecolor used by the control.")]
        public override Color ForeColor { get; set; }

        /// <summary>
        /// Gets or sets arrow color used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets arrow color used by the control.")]
        public Color SymbolsColor { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Handling the mouse moving event so that we can detect if the cursor located in the postion of our need.
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            _point = e.Location;
            Invalidate();
            Cursor = _point.X > Width - 50 ? Cursors.Hand : Cursors.IBeam;


        }

        /// <summary>
        /// Handling on click event so that we can increase or decrease the value.
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Revaluate();
        }

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

        /// <summary>
        /// Here we handle the height of the control while resizing, we provide the fixed height.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 26;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (_point.X <= Width - 45 || _point.X >= Width - 3) return;
            if (e.Button == MouseButtons.Left)
            {
                _holdTimer.Enabled = true;
            }

            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _holdTimer.Enabled = false;
        }

        private void HoldTimer_Tick(object sender, EventArgs args)
        {
            Revaluate();
        }

        private void Revaluate()
        {
            if (_point.X <= Width - 45 || _point.X >= Width - 3) return;
            if (_point.X > Width - 45 && _point.X < Width - 25)
            {
                if (Value + 1 <= Maximum)
                    Value += 1;
            }
            else
            {
                if (Value - 1 >= Minimum)
                    Value -= 1;
            }
        }

        #endregion

    }
}