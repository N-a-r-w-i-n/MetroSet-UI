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
using MetroSet_UI.Property;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetTextBox), "Bitmaps.TextBox.bmp")]
    [Designer(typeof(MetroSetTextBoxDesigner))]
    [DefaultProperty("Text")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetTextBox : Control, iControl
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

        private static TextBoxProperties prop;
        private Methods mth;
        private Utilites utl;

        #endregion Global Vars

        #region Internal Vars

        private Style style;
        private StyleManager _StyleManager;

        private HorizontalAlignment _TextAlign;
        private int _MaxLength;
        private bool _ReadOnly;
        private bool _UseSystemPasswordChar;
        private string _WatermarkText;
        private Image _Image;
        private MouseMode State;
        private AutoCompleteSource _AutoCompleteSource;
        private AutoCompleteMode _AutoCompleteMode;
        private AutoCompleteStringCollection _AutoCompleteCustomSource;
        private bool _Multiline;
        private string[] _Lines;
        private Color _BackColor;
        private Color _ForeColor;
        private Color _BorderColor;
        private Color _HoverColor;

        #region Base TextBox

        private TextBox T = new TextBox();
        private TextBox _T
        {
            get { return _T; }
            set
            {
                if (_T != null)
                {
                    _T.MouseHover -= T_MouseHover;
                    _T.MouseUp -= T_MouseUp;
                    _T.MouseLeave -= T_Leave;
                    _T.MouseEnter -= T_Enter;
                    _T.KeyDown -= T_KeyDown;
                    _T.TextChanged -= T_TextChanged;
                }
                _T = value;
                if (_T != null)
                {
                    _T.MouseHover += T_MouseHover;
                    _T.MouseUp += T_MouseUp;
                    _T.Leave += T_Leave;
                    _T.Enter += T_Enter;
                    _T.KeyDown += T_KeyDown;
                    _T.TextChanged += T_TextChanged;
                }
            }
        }

        #endregion

        #endregion Internal Vars

        #region Constructors

        public MetroSetTextBox()
        {
            SetStyle(
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            Font = MetroSetFonts.Regular(10);
            EvaluateVars();
            ApplyTheme();
            T_Defaults();
            Size = new Size(135, 30);
        }

        public void EvaluateVars()
        {
            prop = new TextBoxProperties();
            mth = new Methods();
            utl = new Utilites();
        }

        void T_Defaults()
        {

            _BackColor = prop.BackColor;
            _ForeColor = prop.ForeColor;
            _BorderColor = prop.BorderColor;
            _HoverColor = prop.HoverColor;

            _WatermarkText = string.Empty;
            _UseSystemPasswordChar = false;
            _ReadOnly = false;
            _MaxLength = 32767;
            _TextAlign = HorizontalAlignment.Left;
            State = MouseMode.Normal;
            _AutoCompleteMode = AutoCompleteMode.None;
            _AutoCompleteSource = AutoCompleteSource.None;
            _Lines = null;
            _Multiline = false;
            T.Multiline = _Multiline;
            T.Cursor = Cursors.IBeam;
            T.BackColor = BackColor;

            T.ForeColor = ForeColor;
            T.BorderStyle = BorderStyle.None;
            T.Location = new Point(7, 8);
            T.Font = Font;
            T.UseSystemPasswordChar = UseSystemPasswordChar;
            if (Multiline)
            {
                T.Height = Height - 11;
            }
            else
            {
                Height = T.Height + 11;
            }
        }

        #endregion Constructors

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if (Enabled)
            {
                using (SolidBrush BG = new SolidBrush(BackColor))
                {
                    using (Pen P = new Pen(BorderColor))
                    {
                        using (Pen PH = new Pen(HoverColor))
                        {
                            G.FillRectangle(BG, Rect);
                            switch (State)
                            {
                                case MouseMode.Normal:
                                    G.DrawRectangle(P, Rect);
                                    break;
                                case MouseMode.Hovered:
                                    G.DrawRectangle(PH, Rect);
                                    break;
                            }
                            T.ForeColor = ForeColor;
                            T.ForeColor = ForeColor;
                        }
                    }
                }
            }
            else
            {
                using (SolidBrush BG = new SolidBrush(prop.DisabledBackColor))
                {
                    using (Pen P = new Pen(prop.DisabledBorderColor))
                    {
                        G.FillRectangle(BG, Rect);
                        G.DrawRectangle(P, Rect);
                        T.BackColor = prop.DisabledBackColor;
                        T.ForeColor = prop.DisabledForeColor;
                    }
                }
            }
            if (Image != null)
            {
                T.Location = new Point(31, 4);
                T.Width = Width - 60;
                G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                G.DrawImage(Image, new Rectangle(7, 6, 18, 18));
            }
            else
            {
                T.Location = new Point(7, 4);
                T.Width = Width - 10;
            }

        }

        #endregion

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
                    prop.ForeColor = Color.FromArgb(20, 20, 20);
                    prop.BackColor = Color.FromArgb(238, 238, 238);
                    prop.HoverColor = Color.FromArgb(102, 102, 102);
                    prop.BorderColor = Color.FromArgb(155, 155, 155);
                    prop.WatermarkText = "";
                    prop.ReadOnly = false;
                    prop.UseSystemPasswordChar = false;
                    prop.Multiline = false;
                    prop.DisabledBackColor = Color.FromArgb(204, 204, 204);
                    prop.DisabledBorderColor = Color.FromArgb(155, 155, 155);
                    prop.DisabledForeColor = Color.FromArgb(136, 136, 136);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    SetProperties();
                    break;

                case Style.Dark:
                    prop.Enabled = Enabled;
                    prop.ForeColor = Color.FromArgb(204, 204, 204);
                    prop.BackColor = Color.FromArgb(34, 34, 34);
                    prop.HoverColor = Color.FromArgb(65, 177, 225);
                    prop.BorderColor = Color.FromArgb(110, 110, 110);
                    prop.WatermarkText = "";
                    prop.ReadOnly = false;
                    prop.UseSystemPasswordChar = false;
                    prop.Multiline = false;
                    prop.DisabledBackColor = Color.FromArgb(80, 80, 80);
                    prop.DisabledBorderColor = Color.FromArgb(109, 109, 109);
                    prop.DisabledForeColor = Color.FromArgb(109, 109, 109);

                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    SetProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.TextBoxDictionary)
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

                                case "HoverColor":
                                    prop.HoverColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "BorderColor":
                                    prop.BorderColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "WatermarkText":
                                    prop.WatermarkText = (string)varkey.Value;
                                    break;

                                case "ReadOnly":
                                    prop.ReadOnly = Convert.ToBoolean(varkey.Value);
                                    break;

                                case "UseSystemPasswordChar":
                                    prop.UseSystemPasswordChar = Convert.ToBoolean(varkey.Value);
                                    break;

                                case "Multiline":
                                    prop.Multiline = Convert.ToBoolean(varkey.Value);
                                    break;

                                case "DisabledBackColor":
                                    prop.DisabledBackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledBorderColor":
                                    prop.DisabledBorderColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledForeColor":
                                    prop.DisabledForeColor = utl.HexColor((string)varkey.Value);
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
                BorderColor = prop.BorderColor;
                HoverColor = prop.HoverColor;
                WatermarkText = prop.WatermarkText;
                ReadOnly = prop.ReadOnly;
                UseSystemPasswordChar = prop.UseSystemPasswordChar;
                Multiline = prop.Multiline;
                Invalidate();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        #endregion Theme Changing

        #region Events

        public new event TextChangedEventHandler TextChanged;
        public delegate void TextChangedEventHandler(object sender);


        /// <summary>
        /// Raises the Control.Leave event.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        public void T_Leave(object sender, EventArgs e)
        {
            base.OnMouseLeave(e);
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseMode.Normal;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseMode.Hovered;
            Invalidate();
        }

        public void T_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseMode.Hovered;
            Invalidate();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            State = MouseMode.Hovered;
            Invalidate();
        }


        public void T_MouseHover(object sender, EventArgs e)
        {
            base.OnMouseHover(e);
            Invalidate();
        }

        /// <summary>
        /// Raises the Control.Resize event.
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            T.Size = new Size(Width - 10, Height - 10);
        }


        /// <summary>
        /// Raises the Control.Enter event.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        public void T_Enter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);
            Invalidate();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">KeyEventArgs</param>
        private void T_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                e.SuppressKeyPress = true;
            if (e.Control && e.KeyCode == Keys.C)
            {
                T.Copy();
                e.SuppressKeyPress = true;
            }
        }


        /// <summary>
        /// An System.EventArgs that contains the event data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T_TextChanged(object sender, EventArgs e)
        {
            Text = T.Text;
            TextChanged?.Invoke(this);
            Invalidate();
        }

        /// <summary>
        /// override the control creating , here we add the base textbox to the main control.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!Controls.Contains(T))
                Controls.Add(T);
            if (string.IsNullOrEmpty(T.Text) && !string.IsNullOrEmpty(WatermarkText))
            {
                T.Text = WatermarkText;
            }
        }


        /// <summary>
        /// Appends text to the current text of a text box.
        /// </summary>
        /// <param name="text"></param>
        public void AppendText(string text)
        {
            if (T != null)
            {
                T.AppendText(text);
            }
        }


        /// <summary>
        /// Undoes the last edit operation in the text box.
        /// </summary>
        public void Undo()
        {
            if (T != null)
            {
                if (T.CanUndo)
                {
                    T.Undo();
                }
            }
        }


        /// <summary>
        /// Retrieves the line number from the specified character position within the text of the control.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetLineFromCharIndex(int index)
        {
            if (T != null)
            {
                return T.GetLineFromCharIndex(index);
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// Retrieves the location within the control at the specified character index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Point GetPositionFromCharIndex(int index)
        {
            return T.GetPositionFromCharIndex(index);
        }


        /// <summary>
        /// Retrieves the index of the character nearest to the specified location.
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public int GetCharIndexFromPosition(Point pt)
        {
            if (T == null)
                return 0;
            return T.GetCharIndexFromPosition(pt);
        }


        /// <summary>
        /// Clears information about the most recent operation from the undo buffer of the text box.
        /// </summary>
        public void ClearUndo()
        {
            if (T == null)
                return;
            T.ClearUndo();
        }


        /// <summary>
        /// Copies the current selection in the text box to the Clipboard.
        /// </summary>
        public void Copy()
        {
            if (T == null)
                return;
            T.Copy();
        }


        /// <summary>
        /// Moves the current selection in the text box to the Clipboard.
        /// </summary>
        public void Cut()
        {
            if (T == null)
                return;
            T.Cut();
        }


        /// <summary>
        /// Selects all text in the text box.
        /// </summary>
        public void SelectAll()
        {
            if (T == null)
                return;
            T.SelectAll();
        }


        /// <summary>
        /// Specifies that the value of the TextBoxBase.SelectionLength property is zero so that no characters are selected in the control.
        /// </summary>
        public void DeselectAll()
        {
            if (T == null)
                return;
            T.DeselectAll();
        }


        /// <summary>
        /// Replaces the current selection in the text box with the contents of the Clipboard.
        /// </summary>
        /// <param name="clipFormat"></param>
        public void Paste(string clipFormat)
        {
            if (T == null)
                return;
            T.Paste(clipFormat);
        }


        /// <summary>
        /// Selects a range of text in the text box.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        public void Select(int start, int length)
        {
            if (T == null)
                return;
            T.Select(start, length);
        }

        #endregion

        #region Properties

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BorderStyle BorderStyle
        {
            get { return BorderStyle.None; }
        }

        /// <summary>
        /// Gets or sets how text is aligned in a TextBox control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets how text is aligned in a TextBox control.")]
        public HorizontalAlignment TextAlign
        {
            get { return _TextAlign; }
            set
            {
                _TextAlign = value;
                TextBox _text = T;
                if (_text != null)
                {
                    _text.TextAlign = value;
                }
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets how text is aligned in a TextBox control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets how text is aligned in a TextBox control.")]
        public int MaxLength
        {
            get { return _MaxLength; }
            set
            {
                _MaxLength = value;
                if (T != null)
                {
                    T.MaxLength = value;
                }
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the background color of the control.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the background color of the control.")]
        [Browsable(false)]
        public override Color BackColor
        {
            get { return _BackColor; }
            set
            {
                base.BackColor = value;
                _BackColor = value;
                T.BackColor = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the color of the control whenever hovered.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the color of the control whenever hovered.")]
        [Browsable(false)]
        public Color HoverColor
        {
            get { return _HoverColor; }
            set
            {
                _HoverColor = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the border color of the control.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the border color of the control.")]
        [Browsable(false)]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set
            {
                _BorderColor = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        [Category("MetroSet Framework")]
        [Description("Gets or sets the foreground color of the control.")]
        [Browsable(false)]
        public override Color ForeColor
        {
            get { return _ForeColor; }
            set
            {
                base.ForeColor = value;
                _ForeColor = value;
                T.ForeColor = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether text in the text box is read-only.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets a value indicating whether text in the text box is read-only.")]
        public bool ReadOnly
        {
            get { return _ReadOnly; }
            set
            {
                _ReadOnly = value;
                if (T != null)
                {
                    T.ReadOnly = value;
                }
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether the text in the TextBox control should appear as the default password character.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets a value indicating whether the text in the TextBox control should appear as the default password character.")]
        public bool UseSystemPasswordChar
        {
            get { return _UseSystemPasswordChar; }
            set
            {
                _UseSystemPasswordChar = value;
                if (T != null)
                {
                    T.UseSystemPasswordChar = value;
                }
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether this is a multiline TextBox control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets a value indicating whether this is a multiline TextBox control.")]
        public bool Multiline
        {
            get { return _Multiline; }
            set
            {
                _Multiline = value;
                if (T == null)
                { return; }
                T.Multiline = value;
                if (value)
                {
                    T.Height = Height - 10;
                }
                else
                {
                    Height = T.Height + 10;
                }
            }
        }


        /// <summary>
        /// Gets or sets the background image.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image BackgroundImage
        {
            get { return null; }
        }


        /// <summary>
        /// Gets or sets the current text in the TextBox.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the current text in the TextBox.")]
        public override string Text
        {
            get { return T.Text; }
            set
            {
                base.Text = value;
                if (T != null)
                {
                    T.Text = value;
                }
            }
        }


        /// <summary>
        /// Gets or sets the text in the TextBox while being empty.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the text in the TextBox while being empty.")]
        public string WatermarkText
        {
            get { return _WatermarkText; }
            set
            {
                _WatermarkText = value;
                User32.SendMessage(T.Handle, 5377, 0, value);
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the image of the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the image of the control.")]
        public Image Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets a value specifying the source of complete strings used for automatic completion.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets a value specifying the source of complete strings used for automatic completion.")]
        public AutoCompleteSource AutoCompleteSource
        {
            get { return _AutoCompleteSource; }
            set
            {
                _AutoCompleteSource = value;
                if (T != null)
                {
                    T.AutoCompleteSource = value;
                }
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets a value specifying the source of complete strings used for automatic completion.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets a value specifying the source of complete strings used for automatic completion.")]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get { return _AutoCompleteCustomSource; }
            set
            {
                _AutoCompleteCustomSource = value;
                if (T != null)
                {
                    T.AutoCompleteCustomSource = value;
                }
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets an option that controls how automatic completion works for the TextBox.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets an option that controls how automatic completion works for the TextBox.")]
        public AutoCompleteMode AutoCompleteMode
        {
            get { return _AutoCompleteMode; }
            set
            {
                _AutoCompleteMode = value;
                if (T != null)
                {
                    T.AutoCompleteMode = value;
                }
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the font of the text displayed by the control.")]
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                if (T == null)
                    return;
                T.Font = value;
                T.Location = new Point(5, 5);
                T.Width = Width - 8;
                if (!Multiline)
                    Height = T.Height + 11;
            }
        }

        /// <summary>
        /// Gets or sets the lines of text in the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the lines of text in the control.")]
        public string[] Lines
        {
            get { return _Lines; }
            set
            {
                _Lines = value;
                if (T != null)
                    T.Lines = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the ContextMenuStrip associated with this control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the ContextMenuStrip associated with this control.")]
        public override ContextMenuStrip ContextMenuStrip
        {
            get { return base.ContextMenuStrip; }
            set
            {
                base.ContextMenuStrip = value;
                if (T == null)
                    return;
                T.ContextMenuStrip = value;
                Invalidate();
            }
        }


        #endregion

    }
}