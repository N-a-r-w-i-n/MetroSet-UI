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

        private Style style;
        private StyleManager _StyleManager;
        private int minimum;
        private int maximum;
        private int _value;
        private int val;
        private Rectangle bar;
        private Rectangle thumb;
        private bool showThumb;
        private int thumbSize;
        private MouseMode _ThumbState;

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
            DoubleBuffered = true;
            UpdateStyles();
            SetDefaults();
            mth = new Methods();
            utl = new Utilites();
            ApplyTheme();

        }

        void SetDefaults()
        {
            minimum = 0;
            maximum = 100;
            _value = 0;
            thumbSize = 20;
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
                                    ForeColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "BackColor":
                                    BackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledBackColor":
                                    DisabledBackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledForeColor":
                                    DisabledForeColor = utl.HexColor((string)varkey.Value);
                                    break;

                                default:
                                    return;
                            }
                        }
                    UpdateProperties();
                    break;
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
            Graphics G = e.Graphics;

            Rectangle r = new Rectangle(0, 0, Width, Height);

            using (SolidBrush BG = new SolidBrush(Enabled ? BackColor : DisabledBackColor))
            {
                using (SolidBrush ThumbBrush = new SolidBrush(Enabled ? ForeColor : DisabledForeColor))
                {
                    G.FillRectangle(BG, r);
                    G.FillRectangle(ThumbBrush, thumb);
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
            get { return minimum; }
            set
            {
                minimum = value;
                if (value > _value)
                {
                    _value = value;
                }
                else if (value > maximum)
                {
                    maximum = value;
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
            get { return maximum; }
            set
            {
               if (value < _value)
                {
                    _value = value;
                }
                else if (value > minimum)
                {
                    maximum = value;
                }
                if (Orientation == ScrollOrientate.Vertical)
                {
                    if (value > Height)
                        thumbSize = Convert.ToInt32(Height * (Height / (double)maximum));
                    else
                        thumbSize = 0;
                }
                else if (Orientation == ScrollOrientate.Horizontal)
                {
                    if (value > Width)
                        thumbSize = Convert.ToInt32(Width * (Width / (double)maximum));
                    else
                        thumbSize = 0;
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
            get { return _value; }
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
            bar = new Rectangle(0, 0, Width, Height);
            showThumb = (Maximum - Minimum) > 0;
            switch (Orientation)
            {
                case ScrollOrientate.Vertical:
                    if (showThumb) 
                        thumb = new Rectangle(0, 0, Width, thumbSize);
                    break;
                case ScrollOrientate.Horizontal:
                    if (showThumb)
                        thumb = new Rectangle(0, 0, Width, thumbSize);
                    break;
            }
            
            Scroll?.Invoke(this);
            InvalidatePosition();
        }

        public event ScrollEventHandler Scroll;
        public delegate void ScrollEventHandler(object sender);

        /// <summary>
        /// Updating the thumb location.
        /// </summary>
        public void InvalidatePosition()
        {
            switch (Orientation)
            {
                case ScrollOrientate.Vertical:
                    thumb.Y = Convert.ToInt32(CurrentValue() * (bar.Height - thumbSize));
                    break;
                case ScrollOrientate.Horizontal:
                    thumb.X = Convert.ToInt32((CurrentValue() * (bar.Width - thumbSize)));
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
            if (e.Button == MouseButtons.Left && showThumb)
            {

                if (thumb.Contains(e.Location))
                {
                    _ThumbState = MouseMode.Pushed;
                    Invalidate();
                    return;
                }
                switch (Orientation)
                {
                    case ScrollOrientate.Vertical:
                        val = (e.Y < thumb.Y) ? Value - LargeChange : Value + LargeChange;
                        break;
                    case ScrollOrientate.Horizontal:
                        val = (e.X < thumb.X) ? Value - LargeChange : Value + LargeChange;
                        break;
                }

                Value = Math.Min(Math.Max(val, Minimum), Maximum);
                InvalidatePosition();
            }
        }

        /// <summary>
        /// Handling the mouse move event so that we can set the value of the thumb.
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_ThumbState == MouseMode.Pushed | !showThumb)
            {
                int thumbPosition;
                int thumbBounds = bar.Height - thumbSize;
                switch (Orientation)
                {
                    case ScrollOrientate.Vertical:
                        thumbPosition = (e.Y) - (thumbSize / 2);
                        thumbBounds = bar.Height - thumbSize;
                        val = Convert.ToInt32(((double)(thumbPosition) / thumbBounds) * (Maximum - Minimum)) - Minimum;
                        break;

                    case ScrollOrientate.Horizontal:
                        thumbPosition = (e.X) - (thumbSize / 2);
                        thumbBounds = bar.Width - thumbSize;
                        val = Convert.ToInt32(((double)(thumbPosition) / thumbBounds) * (Maximum - Minimum)) - Minimum;
                        break;
                }

                Value = Math.Min(Math.Max(val, Minimum), Maximum);
                InvalidatePosition();
            }

        }

        /// <summary>
        /// Handling the mouse up event and determine the state of the thumb.
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            _ThumbState = thumb.Contains(e.Location) ? MouseMode.Hovered : MouseMode.Normal;
            switch (Orientation)
            {
                case ScrollOrientate.Vertical:
                    _ThumbState = ((e.Location.Y < 16) | (e.Location.Y > (Width - 16))) ? MouseMode.Hovered : MouseMode.Normal;
                    break;
                case ScrollOrientate.Horizontal:
                    _ThumbState = (e.Location.X < 16 | e.Location.X > Width - 16) ? MouseMode.Hovered : MouseMode.Normal;
                    break;
            }
            Invalidate();
        }              

        /// <summary>
        /// Handling the mouse leave event and releasing the thumb state.
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            _ThumbState = MouseMode.Normal;
            Invalidate();
        }

        /// <summary>
        /// The Method for finding out the current value of the scrollbar.
        /// </summary>
        /// <returns>the Current value of the scrollbar.</returns>
        private double CurrentValue()
        {
            return ((double)(Value - Minimum)) / (Maximum - Minimum);
        }

        #endregion

    }

    

}