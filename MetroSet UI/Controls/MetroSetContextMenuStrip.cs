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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetContextMenuStrip), "Bitmaps.ContextMenu.bmp")]
    [DefaultEvent("Opening")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetContextMenuStrip : ContextMenuStrip, iControl
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
        private ToolStripItemClickedEventArgs ClickedEventArgs;

        #endregion Internal Vars

        #region Constructors

        public MetroSetContextMenuStrip()
        {
            mth = new Methods();
            utl = new Utilites();
            ApplyTheme();
            Renderer = new MetroSetToolStripRender();
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
                    ForegroundColor = Color.FromArgb(170, 170, 170);
                    BackgroundColor = Color.White;
                    ArrowColor = Color.Gray;
                    SelectedItemBackColor = Color.FromArgb(65, 177, 225);
                    SelectedItemColor = Color.White;
                    SeparatorColor = Color.LightGray;
                    DisabledForeColor = Color.Silver;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;

                case Style.Dark:
                    ForegroundColor = Color.FromArgb(170, 170, 170);
                    BackgroundColor = Color.FromArgb(30, 30, 30);
                    ArrowColor = Color.Gray;
                    SelectedItemBackColor = Color.FromArgb(65, 177, 225);
                    SelectedItemColor = Color.White;
                    SeparatorColor = Color.Gray;
                    DisabledForeColor = Color.Silver;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.ContextMenuDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "ForeColor":
                                    ForegroundColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "BackColor":
                                    BackgroundColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "ArrowColor":
                                    ArrowColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "SeparatorColor":
                                    SeparatorColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "SelectedItemColor":
                                    SelectedItemColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "SelectedItemBackColor":
                                    SelectedItemBackColor = utl.HexColor((string)varkey.Value);
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

        #region Properties

        /// <summary>
        /// Gets or sets forecolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets forecolor used by the control.")]
        [DisplayName("ForeColor")]
        public static Color ForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets backcolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets backcolor used by the control.")]
        [DisplayName("BackColor")]
        public static Color BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets separator color used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets separator color used by the control.")]
        public static Color SeparatorColor { get; set; }

        /// <summary>
        /// Gets or sets arrowcolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets arrowcolor used by the control.")]
        public static Color ArrowColor { get; set; }

        /// <summary>
        /// Gets or sets selecteditem color used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets selecteditem color used by the control.")]
        public static Color SelectedItemColor { get; set; }

        /// <summary>
        /// Gets or sets selecteditem backcolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets selecteditem backcolor used by the control.")]
        public static Color SelectedItemBackColor { get; set; }

        /// <summary>
        /// Gets or sets disabled forecolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets disabled forecolor used by the control.")]
        public static Color DisabledForeColor { get; set; }

        public static new Font Font { get { return MetroSetFonts.UIRegular(10); } }

        #endregion

        #region Events

        public event ClickedEventHandler Clicked;
        public delegate void ClickedEventHandler(object sender);

        /// <summary>
        /// Here we handle whenever and item clicked.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
        {
            if ((e.ClickedItem != null) && !(e.ClickedItem is ToolStripSeparator))
            {
                if (ReferenceEquals(e, ClickedEventArgs))
                    OnItemClicked(e);
                else
                {
                    ClickedEventArgs = e; Clicked?.Invoke(this);
                }
            }
        }

        /// <summary>
        /// Here we handle mouse hover event.
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            Cursor = Cursors.Hand;
            Invalidate();
        }

        /// <summary>
        /// Here we handle mouse up event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Cursor = Cursors.Hand;
            Invalidate();
        }

        #endregion
        
        #region Child

        public sealed class MetroSetToolStripRender : ToolStripProfessionalRenderer
        {
        
            #region Drawing Text

            /// <summary>
            /// Here we draw item text.
            /// </summary>
            /// <param name="e"></param>
            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                Rectangle textRect = new Rectangle(25, e.Item.ContentRectangle.Y, e.Item.ContentRectangle.Width - (24 + 16), e.Item.ContentRectangle.Height - 4);
                using (SolidBrush B = new SolidBrush(e.Item.Enabled ? e.Item.Selected ? SelectedItemColor : ForegroundColor : DisabledForeColor))
                {
                    e.Graphics.DrawString(e.Text, Font, B, textRect);
                }
            }

            #endregion Drawing Text

            #region Drawing Backgrounds

            /// <summary>
            /// Here we draw toolstrip background.
            /// </summary>
            /// <param name="e"></param>
            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                base.OnRenderToolStripBackground(e);
                e.Graphics.Clear(BackgroundColor);
            }

            /// <summary>
            /// Here we draw toolstrip menu item background.
            /// </summary>
            /// <param name="e"></param>
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                e.Graphics.InterpolationMode = InterpolationMode.High;
                e.Graphics.Clear(BackgroundColor);
                Rectangle R = new Rectangle(0, e.Item.ContentRectangle.Y - 2, e.Item.ContentRectangle.Width + 4, e.Item.ContentRectangle.Height + 3);
                using (SolidBrush B = new SolidBrush(e.Item.Selected && e.Item.Enabled ? SelectedItemBackColor : BackgroundColor))
                {
                    e.Graphics.FillRectangle(B, R);
                }
            }

            #endregion Drawing Backgrounds

            #region Set Image Margin

            protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
            {
                //MyBase.OnRenderImageMargin(e)
                //I Make above line comment which makes users to be able to add images to ToolStrips
            }

            #endregion Set Image Margin

            #region Drawing Seperators & Borders

            /// <summary>
            /// Here we draw toolstrip separators.
            /// </summary>
            /// <param name="e"></param>
            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                using (Pen P = new Pen(SeparatorColor))
                {
                    e.Graphics.DrawLine(P, new Point(e.Item.Bounds.Left, e.Item.Bounds.Height / 2), new Point(e.Item.Bounds.Right - 5, e.Item.Bounds.Height / 2));
                }
            }

            #endregion Drawing Seperators & Borders

            #region Drawing DropDown Arrows

            /// <summary>
            /// Here we draw the toolstrip arrows.
            /// </summary>
            /// <param name="e">ToolStripArrowRenderEventArgs</param>
            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                int ArrowX, ArrowY;
                ArrowX = e.ArrowRectangle.X + e.ArrowRectangle.Width / 2;
                ArrowY = e.ArrowRectangle.Y + e.ArrowRectangle.Height / 2;
                Point[] ArrowPoints = new Point[]
                {
                new Point(ArrowX - 5, ArrowY - 5),
                new Point(ArrowX, ArrowY),
                new Point(ArrowX - 5, ArrowY + 5)
                };

                using (SolidBrush ArrowBrush = new SolidBrush(e.Item.Enabled ? ArrowColor : DisabledForeColor))
                {
                    e.Graphics.FillPolygon(ArrowBrush, ArrowPoints);
                }
            }

            #endregion Drawing DropDown Arrows
        }

#endregion

    }
}