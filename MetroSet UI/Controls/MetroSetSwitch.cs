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

using MetroSet_UI.Animates;
using MetroSet_UI.Design;
using MetroSet_UI.Enums;
using MetroSet_UI.Extensions;
using MetroSet_UI.Interfaces;
using MetroSet_UI.Native;
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
    public class MetroSetSwitch : Control, iControl, IDisposable
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

        private readonly Utilites _utl;

        #endregion Global Vars

        #region Internal Vars

        private StyleManager _styleManager;
        private bool _switched;
        private Style _style;
        private int _switchlocation = 0;
        private IntAnimate _animator;

        #endregion Internal Vars

        #region Constructors

        public MetroSetSwitch()
        {
            SetStyle(
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
            Cursor = Cursors.Hand;
            _utl = new Utilites();
            _animator = new IntAnimate();
            _animator.Setting(100, 0, 132, EasingType.Linear);
            _animator.Update = (alpha) =>
            {
                _switchlocation = alpha;
                Invalidate(false);
            };
            ApplyTheme();
        }

        #endregion Constructors

        #region ApplyTheme

        private void UpdateProperties()
        {
            Invalidate();
        }

        /// <summary>
        /// Gets or sets the style provided by the user.
        /// </summary>
        /// <param name="style">The Style.</param>
        private void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    ForeColor = Color.Black;
                    BackColor = Color.White;
                    BorderColor = Color.FromArgb(165, 159, 147);
                    DisabledBorderColor = Color.FromArgb(205, 205, 205);
                    SymbolColor = Color.FromArgb(92, 92, 92);
                    UnCheckColor = Color.FromArgb(155, 155, 155);
                    CheckColor = Color.FromArgb(65, 177, 225);
                    DisabledUnCheckColor = Color.FromArgb(200, 205, 205, 205);
                    DisabledCheckColor = Color.FromArgb(100, 65, 177, 225);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;

                case Style.Dark:
                    ForeColor = Color.FromArgb(170, 170, 170);
                    BackColor = Color.FromArgb(30, 30, 30);
                    BorderColor = Color.FromArgb(155, 155, 155);
                    DisabledBorderColor = Color.FromArgb(85, 85, 85);
                    SymbolColor = Color.FromArgb(92, 92, 92);
                    UnCheckColor = Color.FromArgb(155, 155, 155);
                    CheckColor = Color.FromArgb(65, 177, 225);
                    DisabledUnCheckColor = Color.FromArgb(200, 205, 205, 205);
                    DisabledCheckColor = Color.FromArgb(100, 65, 177, 225);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;

                case Style.Custom:

                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.SwitchBoxDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "BackColor":
                                    BackColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "BorderColor":
                                    BorderColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledBorderColor":
                                    DisabledBorderColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "SymbolColor":
                                    SymbolColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "UnCheckColor":
                                    UnCheckColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "CheckColor":
                                    CheckColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledUnCheckColor":
                                    DisabledUnCheckColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledCheckColor":
                                    DisabledCheckColor = _utl.HexColor((string)varkey.Value);
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

        #endregion ApplyTheme

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            var rect = new Rectangle(1, 1, 56, 20);

            var rect2 = new Rectangle(3, 3, 52, 16);

            using (var backBrush = new SolidBrush(BackgroundColor))
            {
                using (var checkback = new SolidBrush(Enabled ? Switched ? CheckColor : UnCheckColor : Switched ? DisabledCheckColor : DisabledUnCheckColor))
                {
                    using (var checkMarkBrush = new SolidBrush(SymbolColor))
                    {
                        using (var p = new Pen(Enabled ? BorderColor : DisabledBorderColor, 2))
                        {
                            G.FillRectangle(backBrush, rect);

                            G.FillRectangle(checkback, rect2);

                            G.DrawRectangle(p, rect);

                            G.FillRectangle(checkMarkBrush, new Rectangle((Convert.ToInt32(rect.Width * (_switchlocation / 180.0))), 0, 16, 22));
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
        /// Here we will handle the checking state in runtime.
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Switched = !Switched;
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
            get => _switched;
            set
            {
                _switched = value;
                SwitchedChanged?.Invoke(this);
                _animator.Reverse(!value);
                CheckState = value != true ? Enums.CheckState.Unchecked : Enums.CheckState.Checked;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the border color.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the border color.")]
        public Color BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the Checkd backColor.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the Checkd backColor.")]
        public Color CheckColor { get; set; }

        /// <summary>
        /// Gets or sets the border color while the control disabled.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the border color while the control disabled.")]
        public Color DisabledBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the CheckdBackColor while disabled.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the CheckdBackColor while disabled.")]
        public Color DisabledCheckColor { get; set; }

        /// <summary>
        /// Gets or sets the Un-Checkd BackColor while disabled.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the Un-Checkd BackColor while disabled.")]
        public Color DisabledUnCheckColor { get; set; }

        /// <summary>
        /// Gets or sets forecolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets forecolor used by the control.")]
        public override Color ForeColor { get; set; }

        /// <summary>
        /// I make backcolor inaccessible cause I want it to be just transparent and I used another property for the same job in following properties. 
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor => Color.Transparent;

        /// <summary>
        /// Gets or sets the control backcolor.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the control backcolor.")]
        [DisplayName("BackColor")]
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the check symbol.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the color of the check symbol.")]
        public Color SymbolColor { get; set; }

        /// <summary>
        /// Gets or sets the Un-Checkd backColor.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the Un-Checkd backColor.")]
        public Color UnCheckColor { get; set; }

        #endregion Properties

        #region Disposing

        /// <summary>
        /// Disposing Methods.
        /// </summary>
        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion

    }
}