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
using MetroSet_UI.Property;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetTrackBar), "Bitmaps.Slider.bmp")]
    [Designer(typeof(MetroSetTrackBarDesigner))]
    [DefaultProperty("Value")]
    [DefaultEvent("Scroll")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetTrackBar : Control, iControl
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

        private static TrackBarProperties prop;
        private Methods mth;
        private Utilites utl;

        #endregion Global Vars

        #region Internal Vars

        private Style style;
        private StyleManager _StyleManager;
        protected bool Variable;
        private Rectangle Track;
        protected int _Maximum;
        private int _Minimum;
        private int _Value;
        private int CurrentValue;

        #endregion Internal Vars

        #region Constructors

        public MetroSetTrackBar()
        {
            SetStyle(
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            _Maximum = 100;
            _Minimum = 0;
            _Value = 0;
            CurrentValue = Convert.ToInt32(Value / (double)(Maximum) - (2 * Width));
            UpdateStyles();
            BackColor = Color.Transparent;
            prop = new TrackBarProperties();
            mth = new Methods();
            utl = new Utilites();

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
                    prop.HandlerColor = Color.FromArgb(180, 180, 180);
                    prop.BackColor = Color.FromArgb(205, 205, 205);
                    prop.ValueColor = Color.FromArgb(65, 177, 225);
                    prop.DisabledBackColor = Color.FromArgb(235, 235, 235);
                    prop.DisabledValueColor = Color.FromArgb(205, 205, 205);
                    prop.DisabledHandlerColor = Color.FromArgb(196, 196, 196);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;

                case Style.Dark:
                    prop.Enabled = Enabled;
                    prop.HandlerColor = Color.FromArgb(143, 143, 143);
                    prop.BackColor = Color.FromArgb(90, 90, 90);
                    prop.ValueColor = Color.FromArgb(65, 177, 225);
                    prop.DisabledBackColor = Color.FromArgb(80, 80, 80);
                    prop.DisabledValueColor = Color.FromArgb(109, 109, 109);
                    prop.DisabledHandlerColor = Color.FromArgb(90, 90, 90);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.TrackBarDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "Enabled":
                                    prop.Enabled = Convert.ToBoolean(varkey.Value);
                                    break;

                                case "HandlerColor":
                                    prop.HandlerColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "BackColor":
                                    prop.BackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "ValueColor":
                                    prop.ValueColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledBackColor":
                                    prop.DisabledBackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledValueColor":
                                    prop.DisabledValueColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledHandlerColor":
                                    prop.DisabledHandlerColor = utl.HexColor((string)varkey.Value);
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

        #endregion Theme Changing

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            Cursor = Cursors.Hand;

            using (SolidBrush BG = new SolidBrush(Enabled ? prop.BackColor : prop.DisabledBackColor))
            {
                using (SolidBrush V = new SolidBrush(Enabled ? prop.ValueColor : prop.DisabledValueColor))
                {
                    using (SolidBrush VC = new SolidBrush(Enabled ? prop.HandlerColor : prop.DisabledHandlerColor))
                    {
                        G.FillRectangle(BG, new Rectangle(0, 6, Width, 4));
                        if (CurrentValue != 0)
                            G.FillRectangle(V, new Rectangle(0, 6, CurrentValue, 4));
                        G.FillRectangle(VC, Track);
                    }
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the upper limit of the range this TrackBar is working with.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the upper limit of the range this TrackBar is working with.")]
        public int Maximum
        {
            get { return _Maximum; }
            set
            {
                _Maximum = value;
                RenewCurrentValue();
                MoveTrack();
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the lower limit of the range this TrackBar is working with.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the lower limit of the range this TrackBar is working with.")]
        public int Minimum
        {
            get { return _Minimum; }
            set
            {
                if (!(value < 0))
                {
                    _Minimum = value;
                    RenewCurrentValue();
                    MoveTrack();
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a numeric value that represents the current position of the scroll box on the track bar.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets a numeric value that represents the current position of the scroll box on the track bar.")]
        public int Value
        {
            get { return _Value; }
            set
            {
                if (value != _Value)
                {
                    _Value = value;
                    RenewCurrentValue();
                    MoveTrack();
                    Invalidate();
                    Scroll?.Invoke(this);
                }
            }
        }

        #endregion

        #region Events

        public event ScrollEventHandler Scroll;
        public delegate void ScrollEventHandler(object sender);

        /// <summary>
        /// Handling mouse move event so that we can handle the thumb value.
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Variable && e.X > -1 && e.X < Width + 1)
            {
                Value = Minimum + (int)Math.Round((double)(Maximum - Minimum) * e.X / Width);
            }
            base.OnMouseMove(e);
        }

        /// <summary>
        /// Handling mouse down event so that we can put the thumb in clicked state.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Height > 0)
            {
                RenewCurrentValue();
                Track = new Rectangle(CurrentValue, 0, 6, 16);
                Variable = new Rectangle(CurrentValue, 0, 6, 16).Contains(e.Location);
            }
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Handling mouse up event.
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Variable = false;
            base.OnMouseUp(e);
        }

        /// <summary>
        /// Handling key press event so that we can change the track value by keys.
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left)
            {
                if (Value != 0)
                {
                    Value -= 1;
                }

            }
            else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Up || e.KeyCode == Keys.Right)
            {
                if (Value != Maximum)
                {
                    Value += 1;
                }

            }
            base.OnKeyDown(e);
        }

        /// <summary>
        /// Handling the height and value of the track while resizing the control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            RenewCurrentValue();
            MoveTrack();
            Height = 16;
            Invalidate();
            base.OnResize(e);
        }

        /// <summary>
        /// The Method to provide the track value.
        /// </summary>
        private void MoveTrack()
        {
            Track = new Rectangle(CurrentValue, 0, 6, 16);
        }

        /// <summary>
        /// The Method to renew the value of the track.
        /// </summary>
        public void RenewCurrentValue()
        {
            CurrentValue = Convert.ToInt32(Math.Round((double)(Value - Minimum) / (Maximum - Minimum) * (Width - 6)));
            Invalidate();
        }

        #endregion

    }
}