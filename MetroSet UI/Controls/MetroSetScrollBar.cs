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

        private static LabelProperties prop;
        private Methods mth;
        private Utilites utl;

        #endregion Global Vars

        #region Internal Vars

        private Style style;
        private StyleManager _StyleManager;
        private int minimum;
        private int maximum;
        private int value;
        private int smallChange;
        private int val;
        private int largeChange;
        private Point mouseLocation;
        private Rectangle shaft;
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
            Font = MetroSetFonts.SemiBold(10);
            prop = new LabelProperties();
            mth = new Methods();
            utl = new Utilites();
            style = Style.Light;
            ApplyTheme();

        }

        void SetDefaults()
        {
            minimum = 0;
            maximum = 100;
            value = 0;
            mouseLocation = new Point(0, 0);
            smallChange = 1;
            largeChange = 10;
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
                    prop.Enabled = Enabled;
                    prop.ForeColor = Color.FromArgb(65, 177, 225);
                    prop.BackColor = Color.White;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    SetProperties();
                    break;

                case Style.Dark:
                    prop.Enabled = Enabled;
                    prop.ForeColor = Color.FromArgb(65, 177, 225);
                    prop.BackColor = Color.FromArgb(30, 30, 30);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    SetProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.LabelDictionary)
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
                BackColor = prop.BackColor;
                ForeColor = prop.ForeColor;
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
            G.Clear(BackColor);
            using (SolidBrush ThumbBrush = new SolidBrush(ForeColor))
            {
                G.FillRectangle(ThumbBrush, thumb);
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
                if (value > this.value)
                {
                    this.value = value;
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
                if (value < this.value)
                {
                    this.value = value;
                }
                else if (value < minimum)
                {
                    minimum = value;
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
            get { return value; }
            set
            {
                if (value > maximum)
                {
                    throw new Exception("Already reached to the maximum value.");
                }
                else if (value < minimum)
                {
                    throw new Exception("Already reached to the minimum value.");
                }
                else
                {
                    this.value = value;
                }
                InvalidatePosition();
                Scroll?.Invoke(this);
            }
        }


        /// <summary>
        /// Gets or sets the distance to move a scroll bar in response to a small scroll command.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the distance to move a scroll bar in response to a small scroll command.")]
        public int SmallChange
        {
            get { return smallChange; }
            set
            {
                smallChange = value;
                Invalidate();

            }
        }


        /// <summary>
        /// Gets or sets the distance to move a scroll bar in response to a large scroll command.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the distance to move a scroll bar in response to a large scroll command.")]
        public int LargeChange
        {
            get { return largeChange; }
            set
            {
                largeChange = value;
                Invalidate();
            }
        }
        

        /// <summary>
        /// Gets or sets the scroll bar orientation.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the scroll bar orientation.")]
        public ScrollOrientate Orientation { get; set; } = ScrollOrientate.Horizontal;


        [Browsable(false)]
        public override Color ForeColor { get; set; }


        [Browsable(false)]
        public override Color BackColor { get; set; }

        #endregion

        #region Events

        protected override void OnSizeChanged(EventArgs e)
        {
            InvalidateLayout();
        }

        private void InvalidateLayout()
        {
            shaft = new Rectangle(0, 0, Width, Height);
            showThumb = (Maximum - Minimum) > 0;
            switch (Orientation)
            {
                case ScrollOrientate.Vertical:
                    if (showThumb)
                        thumb = new Rectangle(0, 0, Width, Convert.ToInt32(Height / 8));
                    break;
                case ScrollOrientate.Horizontal:
                    if (showThumb)
                        thumb = new Rectangle(0, 0, Convert.ToInt32(Width / 8) - 2, Height);
                    break;
            }

            Scroll?.Invoke(this);
            InvalidatePosition();
        }

        public event ScrollEventHandler Scroll;
        public delegate void ScrollEventHandler(object sender);

        public void InvalidatePosition()
        {
            switch (Orientation)
            {
                case ScrollOrientate.Vertical:
                    thumb.Y = Convert.ToInt32((double)(Value - Minimum) / (Maximum - Minimum) * (shaft.Height - thumbSize));
                    break;
                case ScrollOrientate.Horizontal:
                    thumb.X = Convert.ToInt32(((((double)(Value - Minimum)) / (Maximum - Minimum)) * (shaft.Width - thumbSize)));
                    break;
            }
            
            Invalidate(); 
        }

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
                Invalidate();
                InvalidatePosition();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_ThumbState == MouseMode.Pushed | !showThumb)
            {
                int thumbPosition;
                int thumbBounds = shaft.Height - thumbSize;
                switch (Orientation)
                {
                    case ScrollOrientate.Vertical:
                        thumbPosition = (e.Y) - (thumbSize / 2);
                        thumbBounds = shaft.Height - thumbSize;
                        val = Convert.ToInt32(((double)(thumbPosition) / thumbBounds) * (Maximum - Minimum)) - Minimum;
                        break;

                    case ScrollOrientate.Horizontal:
                        thumbPosition = (e.X) - (thumbSize / 2);
                        thumbBounds = shaft.Width - thumbSize;
                        val = Convert.ToInt32(((double)(thumbPosition) / thumbBounds) * (Maximum - Minimum)) - Minimum;
                        break;
                }

                Value = Math.Min(Math.Max(val, Minimum), Maximum);
                InvalidatePosition();
            }

        }

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

        protected override void OnMouseLeave(EventArgs e)
        {
            _ThumbState = MouseMode.Normal;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Invalidate();
        }

        #endregion

    }
}