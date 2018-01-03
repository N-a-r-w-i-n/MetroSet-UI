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
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetScrollBar), "Bitmaps.ScrollBar.bmp")]
    [Designer(typeof(MetroSetScrollBarDesigner))]
    [DefaultEvent("Scroll")]
    [DefaultProperty("Value")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetScrollBar : Control, iControl
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

        private Style _style;
        private StyleManager _styleManager;
        private int _minimum;
        private int _maximum;
        private int _value;
        private int _val;
        private Rectangle _bar;
        private Rectangle _thumb;
        private bool _showThumb;
        private int _thumbSize;
        private MouseMode _thumbState;

        #endregion Internal Vars

        #region Constructors

        public MetroSetScrollBar()
        {
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint |
                ControlStyles.Selectable |
                ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
            SetDefaults();
            _utl = new Utilites();
            ApplyTheme();

        }

        void SetDefaults()
        {
            _minimum = 0;
            _maximum = 100;
            _value = 0;
            _thumbSize = 20;
        }

        #endregion Constructors

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
                    ForeColor = Color.FromArgb(65, 177, 225);
                    BackColor = Color.White;
                    DisabledBackColor = Color.FromArgb(204, 204, 204);
                    DisabledForeColor = Color.FromArgb(136, 136, 136);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;

                case Style.Dark:
                    ForeColor = Color.FromArgb(65, 177, 225);
                    BackColor = Color.FromArgb(30, 30, 30);
                    DisabledBackColor = Color.FromArgb(80, 80, 80);
                    DisabledForeColor = Color.FromArgb(109, 109, 109);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.ScrollBarDictionary)
                        {
                            switch (varkey.Key)
                            {

                                case "ForeColor":
                                    ForeColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "BackColor":
                                    BackColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledBackColor":
                                    DisabledBackColor = _utl.HexColor((string)varkey.Value);
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

        public void UpdateProperties()
        {
            Invalidate();
        }

        #endregion Theme Changing

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;

            var r = new Rectangle(0, 0, Width, Height);

            using (var bg = new SolidBrush(Enabled ? BackColor : DisabledBackColor))
            {
                using (var thumbBrush = new SolidBrush(Enabled ? ForeColor : DisabledForeColor))
                {
                    G.FillRectangle(bg, r);
                    G.FillRectangle(thumbBrush, _thumb);
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the lower limit of the scrollable range.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the lower limit of the scrollable range.")]
        public int Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                if (value > _value)
                {
                    _value = value;
                }
                else if (value > _maximum)
                {
                    _maximum = value;
                }
                InvalidateLayout();
            }
        }


        /// <summary>
        /// Gets or sets the upper limit of the scrollable range.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the upper limit of the scrollable range.")]
        public int Maximum
        {
            get => _maximum;
            set
            {
                if (value < _value)
                {
                    _value = value;
                }
                else if (value > _minimum)
                {
                    _maximum = value;
                }

                if (Orientation != ScrollOrientate.Vertical)
                {
                    if (Orientation == ScrollOrientate.Horizontal)
                    {
                        _thumbSize = value > Width ? Convert.ToInt32(Width * (Width / (double)_maximum)) : 0;
                    }
                }
                else
                {
                    _thumbSize = value > Height ? Convert.ToInt32(Height * (Height / (double)_maximum)) : 0;
                }

                InvalidateLayout();
            }
        }


        /// <summary>
        /// Gets or sets a numeric value that represents the current position of the scroll bar box.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets a numeric value that represents the current position of the scroll bar box.")]
        public int Value
        {
            get => _value;
            set
            {
                if (value > Maximum)
                {
                    _value = Maximum;
                }
                else if (value < Minimum)
                {
                    _value = Minimum;
                }
                else
                {
                    _value = value;
                }
                InvalidatePosition();
                Scroll?.Invoke(this);
            }
        }


        /// <summary>
        /// Gets or sets the distance to move a scroll bar in response to a small scroll command.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the distance to move a scroll bar in response to a small scroll command.")]
        public int SmallChange { get; set; } = 1;


        /// <summary>
        /// Gets or sets the distance to move a scroll bar in response to a large scroll command.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the distance to move a scroll bar in response to a large scroll command.")]
        public int LargeChange { get; set; } = 10;

        /// <summary>
        /// Gets or sets the scroll bar orientation.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the scroll bar orientation.")]
        public ScrollOrientate Orientation { get; set; } = ScrollOrientate.Horizontal;

        /// <summary>
        /// Gets or sets forecolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the scroll bar orientation.")]
        public override Color ForeColor { get; set; }

        /// <summary>
        /// Gets or sets backcolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets backcolor used by the control.")]
        public override Color BackColor { get; set; }

        /// <summary>
        /// Gets or sets disabled forecolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets disabled forecolor used by the control.")]
        public Color DisabledForeColor { get; set; }

        /// <summary>
        /// Gets or sets disabled backcolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets disabled backcolor used by the control.")]
        public Color DisabledBackColor { get; set; }

        #endregion

        #region Events

        protected override void OnSizeChanged(EventArgs e)
        {
            InvalidateLayout();
        }

        /// <summary>
        /// Updating the thumb rectangle.
        /// </summary>
        private void InvalidateLayout()
        {
            _bar = new Rectangle(0, 0, Width, Height);
            _showThumb = Maximum - Minimum > 0;
            switch (Orientation)
            {
                case ScrollOrientate.Vertical:
                    if (_showThumb)
                        _thumb = new Rectangle(0, 0, Width, _thumbSize);
                    break;
                case ScrollOrientate.Horizontal:
                    if (_showThumb)
                        _thumb = new Rectangle(0, 0, Width, _thumbSize);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Scroll?.Invoke(this);
            InvalidatePosition();
        }

        public event ScrollEventHandler Scroll;
        public delegate void ScrollEventHandler(object sender);

        /// <summary>
        /// Updating the thumb location.
        /// </summary>
        private void InvalidatePosition()
        {
            switch (Orientation)
            {
                case ScrollOrientate.Vertical:
                    _thumb.Y = Convert.ToInt32(CurrentValue() * (_bar.Height - _thumbSize));
                    break;
                case ScrollOrientate.Horizontal:
                    _thumb.X = Convert.ToInt32(CurrentValue() * (_bar.Width - _thumbSize));
                    break;
            }

            Invalidate();
        }

        /// <summary>
        /// Handling mouse down event so that we set the state of the thumb to pressed and ready to move.
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left || !_showThumb) return;
            if (_thumb.Contains(e.Location))
            {
                _thumbState = MouseMode.Pushed;
                Invalidate();
                return;
            }
            switch (Orientation)
            {
                case ScrollOrientate.Vertical:
                    _val = e.Y < _thumb.Y ? Value - LargeChange : Value + LargeChange;
                    break;
                case ScrollOrientate.Horizontal:
                    _val = e.X < _thumb.X ? Value - LargeChange : Value + LargeChange;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Value = Math.Min(Math.Max(_val, Minimum), Maximum);
            InvalidatePosition();
        }

        /// <summary>
        /// Handling the mouse move event so that we can set the value of the thumb.
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!(_thumbState == MouseMode.Pushed | !_showThumb)) return;
            int thumbPosition;
            int thumbBounds;
            switch (Orientation)
            {
                case ScrollOrientate.Vertical:
                    thumbPosition = e.Y - _thumbSize / 2;
                    thumbBounds = _bar.Height - _thumbSize;
                    _val = Convert.ToInt32((double)thumbPosition / thumbBounds * (Maximum - Minimum)) - Minimum;
                    break;

                case ScrollOrientate.Horizontal:
                    thumbPosition = e.X - _thumbSize / 2;
                    thumbBounds = _bar.Width - _thumbSize;
                    _val = Convert.ToInt32((double)thumbPosition / thumbBounds * (Maximum - Minimum)) - Minimum;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Value = Math.Min(Math.Max(_val, Minimum), Maximum);
            InvalidatePosition();

        }

        /// <summary>
        /// Handling the mouse up event and determine the state of the thumb.
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            _thumbState = _thumb.Contains(e.Location) ? MouseMode.Hovered : MouseMode.Normal;
            switch (Orientation)
            {
                case ScrollOrientate.Vertical:
                    _thumbState = (e.Location.Y < 16) | (e.Location.Y > Width - 16) ? MouseMode.Hovered : MouseMode.Normal;
                    break;
                case ScrollOrientate.Horizontal:
                    _thumbState = e.Location.X < 16 | e.Location.X > Width - 16 ? MouseMode.Hovered : MouseMode.Normal;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Invalidate();
        }

        /// <summary>
        /// Handling the mouse leave event and releasing the thumb state.
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            _thumbState = MouseMode.Normal;
            Invalidate();
        }

        /// <summary>
        /// The Method for finding out the current value of the scrollbar.
        /// </summary>
        /// <returns>the Current value of the scrollbar.</returns>
        private double CurrentValue()
        {
            return (double)(Value - Minimum) / (Maximum - Minimum);
        }

        #endregion

    }



}