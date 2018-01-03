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
    [ToolboxBitmap(typeof(MetroSetProgressBar), "Bitmaps.Progress.bmp")]
    [Designer(typeof(MetroSetProgressBarDesigner))]
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetProgressBar : Control, iControl
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
        private int _value;
        private int _currentValue;

        #endregion Internal Vars

        #region Constructors

        public MetroSetProgressBar()
        {
            SetStyle(
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
            _utl = new Utilites();
            ApplyTheme();
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
                    ProgressColor = Color.FromArgb(65, 177, 225);
                    BorderColor = Color.FromArgb(238, 238, 238);
                    BackgroundColor = Color.FromArgb(238, 238, 238);
                    DisabledProgressColor = Color.FromArgb(120, 65, 177, 225);
                    DisabledBorderColor = Color.FromArgb(238, 238, 238);
                    DisabledBackColor = Color.FromArgb(238, 238, 238);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;

                case Style.Dark:
                    ProgressColor = Color.FromArgb(65, 177, 225);
                    BackgroundColor = Color.FromArgb(38, 38, 38);
                    BorderColor = Color.FromArgb(38, 38, 38);
                    DisabledProgressColor = Color.FromArgb(120, 65, 177, 225);
                    DisabledBackColor = Color.FromArgb(38, 38, 38);
                    DisabledBorderColor = Color.FromArgb(38, 38, 38);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.ProgressDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "ProgressColor":
                                    ProgressColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "BorderColor":
                                    BorderColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "BackColor":
                                    BackgroundColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledBackColor":
                                    DisabledBackColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledBorderColor":
                                    DisabledBorderColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledProgressColor":
                                    DisabledProgressColor = _utl.HexColor((string)varkey.Value);
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

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);

            using (var bg = new SolidBrush(Enabled ? BackgroundColor : DisabledBackColor))
            {
                using (var p = new Pen(Enabled ? BorderColor : DisabledBorderColor))
                {
                    using (var ps = new SolidBrush(Enabled ? ProgressColor : DisabledProgressColor))
                    {
                        G.FillRectangle(bg, rect);
                        if (_currentValue != 0)
                        {
                            switch (Orientation)
                            {
                                case ProgressOrientation.Horizontal:
                                    G.FillRectangle(ps, new Rectangle(0, 0, _currentValue - 1, Height - 1));
                                    break;
                                case ProgressOrientation.Vertical:
                                    G.FillRectangle(ps, new Rectangle(0, Height - _currentValue, Width - 1, _currentValue - 1));
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                        G.DrawRectangle(p, rect);
                    }
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current position of the progressbar.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the current position of the progressbar.")]
        public int Value
        {
            get => _value < 0 ? 0 : _value;
            set
            {
                if (value > Maximum)
                {
                    value = Maximum;
                }
                _value = value;
                RenewCurrentValue();
                ValueChanged?.Invoke(this);
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the maximum value of the progressbar.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the maximum value of the progressbar.")]
        public int Maximum { get; set; } = 100;

        /// <summary>
        /// Gets or sets the minimum value of the progressbar.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the minimum value of the progressbar.")]
        public int Minimum { get; set; } = 0;

        [Browsable(false)]
        public override Color BackColor => Color.Transparent;

        /// <summary>
        /// Gets or sets the minimum value of the progressbar.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the minimum value of the progressbar.")]
        public ProgressOrientation Orientation { get; set; } = ProgressOrientation.Horizontal;

        /// <summary>
        /// Gets or sets the control backcolor.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the control backcolor.")]
        [DisplayName("BackColor")]
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the border color.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the border color.")]
        public Color BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the progress color of the cotnrol.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the progress color of the cotnrol.")]
        public Color ProgressColor { get; set; }

        /// <summary>
        /// Gets or sets the progresscolor of the control whenever while disabled
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the progresscolor of the control whenever while disabled.")]
        public Color DisabledProgressColor { get; set; }

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

        #endregion

        #region Events

        public event ValueChangedEventHandler ValueChanged;
        public delegate void ValueChangedEventHandler(object sender);

        /// <summary>
        /// Here we handle the current value.
        /// </summary>
        private void RenewCurrentValue()
        {
            if (Orientation == ProgressOrientation.Horizontal)
                _currentValue = (int)Math.Round((Value - Minimum) / (double)(Maximum - Minimum) * (Width - 1));
            else
                _currentValue = Convert.ToInt32(Value / (double)Maximum * Height - 1);
        }

        #endregion

    }
}