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

using MetroSet_UI.Child;
using MetroSet_UI.Design;
using MetroSet_UI.Extensions;
using MetroSet_UI.Interfaces;
using MetroSet_UI.Native;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true)] 
    [ToolboxBitmap(typeof(MetroSetListBox), "Bitmaps.ListBox.bmp")]
    [Designer(typeof(MetroSetListBoxDesigner))]
    [DefaultProperty("Items")]
    [DefaultEvent("SelectedIndexChanged")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetListBox : Control, iControl
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
                SVS.Style = value;
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
            set
            {
                _StyleManager = value;
                Invalidate();
            }
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

        public MetroSetItemCollection _Items;
        private List<object> _SelectedItems;
        private List<object> _Indicates;
        private bool _MultiSelect;
        private int _SelectedIndex;
        private string _SelectedItem;
        private bool _ShowScrollBar;
        private bool _MultiKeyDown;
        private int _HoveredItem;

        public MetroSetScrollBar SVS;

        #endregion Internal Vars

        #region Constructors

        public MetroSetListBox()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.Selectable |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            BackColor = Color.Transparent;
            Font = MetroSetFonts.SemiBold(10);
            mth = new Methods();
            utl = new Utilites();
            ApplyTheme();
            SetDefaults();
        }

        private void SetDefaults()
        {
            SelectedIndex = -1;
            _HoveredItem = -1;
            _ShowScrollBar = false;
            _Items = new MetroSetItemCollection();
            _Items.ItemUpdated += InvalidateScroll;
            _SelectedItems = new List<object>();
            _Indicates = new List<object>();
            ItemHeight = 30;
            _MultiKeyDown = false;
            SVS = new MetroSetScrollBar()
            {
                Orientation = Enums.ScrollOrientate.Vertical,
                Size = new Size(12, Height),
                Maximum = _Items.Count * ItemHeight,
                SmallChange = 1,
                LargeChange = 5
            };
            SVS.Scroll += HandleScroll;
            SVS.MouseDown += VS_MouseDown;
            SVS.BackColor = Color.Transparent;
            if (!Controls.Contains(SVS))
            {
                Controls.Add(SVS);
            }
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
                    ForeColor = Color.Black;
                    BackColor = Color.White;
                    SelectedItemBackColor = Color.FromArgb(65, 177, 225);
                    SelectedItemColor = Color.White;
                    HoveredItemColor = Color.DimGray;
                    HoveredItemBackColor = Color.LightGray;
                    DisabledBackColor = Color.FromArgb(204, 204, 204);
                    DisabledForeColor = Color.FromArgb(136, 136, 136);
                    BorderColor = Color.LightGray;
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;

                case Style.Dark:
                    ForeColor = Color.FromArgb(170, 170, 170);
                    BackColor = Color.FromArgb(30, 30, 30);
                    SelectedItemBackColor = Color.FromArgb(65, 177, 225);
                    SelectedItemColor = Color.White;
                    HoveredItemColor = Color.DimGray;
                    HoveredItemBackColor = Color.LightGray;
                    DisabledBackColor = Color.FromArgb(80, 80, 80);
                    DisabledForeColor = Color.FromArgb(109, 109, 109);
                    BorderColor = Color.FromArgb(64, 64, 64);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.ListBoxDictionary)
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

                                case "HoveredItemBackColor":
                                    HoveredItemBackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "HoveredItemColor":
                                    HoveredItemColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "SelectedItemBackColor":
                                    SelectedItemBackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "SelectedItemColor":
                                    SelectedItemColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "BorderColor":
                                    BorderColor = utl.HexColor((string)varkey.Value);
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

        #endregion ApplyTheme

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            Rectangle mainRect = new Rectangle(0, 0, Width - (ShowBorder ? 1 : 0), Height - (ShowBorder ? 1 : 0));

            using (SolidBrush BG = new SolidBrush(Enabled ? BackColor : DisabledBackColor))
            {
                using (SolidBrush USIC = new SolidBrush(Enabled ? ForeColor : DisabledForeColor))
                {
                    using (SolidBrush SIC = new SolidBrush(SelectedItemColor))
                    {
                        using (SolidBrush SIBC = new SolidBrush(SelectedItemBackColor))
                        {
                            using (SolidBrush HIC = new SolidBrush(HoveredItemColor))
                            {
                                using (SolidBrush HIBC = new SolidBrush(HoveredItemBackColor))
                                {
                                    using (StringFormat SF = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center })
                                    {
                                        int FirstItem = (SVS.Value / ItemHeight) < 0 ? 0 : (SVS.Value / ItemHeight);
                                        int LastItem = (SVS.Value / ItemHeight) + (Height / ItemHeight) + 1 > Items.Count ? Items.Count : (SVS.Value / ItemHeight) + (Height / ItemHeight) + 1;

                                        G.FillRectangle(BG, mainRect);

                                        for (int i = FirstItem; i < LastItem; i++)
                                        {
                                            string itemText = (string)Items[i];

                                            Rectangle rect = new Rectangle(5, ((i - FirstItem) * ItemHeight), Width - 1, ItemHeight);
                                            G.DrawString(itemText, Font, USIC, rect, SF);
                                            if (MultiSelect && _Indicates.Count != 0)
                                            {
                                                if (i == _HoveredItem && !_Indicates.Contains(i))
                                                {
                                                    G.FillRectangle(HIBC, rect);
                                                    G.DrawString(itemText, Font, HIC, rect, SF);
                                                }
                                                else if (_Indicates.Contains(i))
                                                {
                                                    G.FillRectangle(SIBC, rect);
                                                    G.DrawString(itemText, Font, SIC, rect, SF);
                                                }
                                            }
                                            else
                                            {
                                                if (i == _HoveredItem && i != SelectedIndex)
                                                {
                                                    G.FillRectangle(HIBC, rect);
                                                    G.DrawString(itemText, Font, HIC, rect, SF);
                                                }
                                                else if (i == SelectedIndex)
                                                {
                                                    G.FillRectangle(SIBC, rect);
                                                    G.DrawString(itemText, Font, SIC, rect, SF);
                                                }
                                            }

                                        }
                                        if(ShowBorder)
                                        G.DrawRectangle(Pens.LightGray, mainRect);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion Draw Control

        #region Properties

        /// <summary>
        /// Gets the items of the ListBox.
        /// </summary>
        [TypeConverter(typeof(CollectionConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        [Category("MetroSet Framework"), Description("Gets the items of the ListBox.")]
        public MetroSetItemCollection Items
        {
            get { return _Items; }
        }

        /// <summary>
        /// Gets a collection containing the currently selected items in the ListBox.
        /// </summary>
        [Browsable(false)]
        [Category("MetroSet Framework"), Description("Gets a collection containing the currently selected items in the ListBox.")]
        public List<object> SelectedItems
        {
            get { return _SelectedItems; }
        }

        /// <summary>
        /// Gets or sets the height of an item in the ListBox.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the height of an item in the ListBox.")]
        public int ItemHeight { get; set; }

        /// <summary>
        /// Gets or sets the currently selected item in the ListBox.
        /// </summary>
        [Browsable(false), Category("MetroSet Framework"), Description("Gets or sets the currently selected item in the ListBox.")]
        public string SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the zero-based index of the currently selected item in a ListBox.
        /// </summary>
        [Browsable(false), Category("MetroSet Framework"), Description("Gets or sets the zero-based index of the currently selected item in a ListBox.")]
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                _SelectedIndex = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the ListBox supports multiple rows.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets a value indicating whether the ListBox supports multiple rows.")]
        public bool MultiSelect
        {
            get { return _MultiSelect; }
            set
            {
                _MultiSelect = value;

                if (_SelectedItems.Count > 1)
                    _SelectedItems.RemoveRange(1, _SelectedItems.Count - 1);

                Invalidate();
            }
        }

        /// <summary>
        /// Gets the the number of items stored in items collection.
        /// </summary>
        [Browsable(false)]
        public int Count
        {
            get { return _Items.Count; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the vertical scroll bar is shown or not.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets a value indicating whether the vertical scroll bar be shown or not.")]
        public bool ShowScrollBar
        {
            get { return _ShowScrollBar; }
            set
            {
                _ShowScrollBar = value;
                SVS.Visible = value ? true : false;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the border shown or not.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets a value indicating whether the border shown or not.")]
        public bool ShowBorder { get; set; } = false;

        /// <summary>
        /// Gets or sets backcolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets backcolor used by the control.")]
        public override Color BackColor { get; set; }

        /// <summary>
        /// Gets or sets forecolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets forecolor used by the control.")]
        public override Color ForeColor { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string Text { get => base.Text; set => base.Text = value; }

        /// <summary>
        /// Gets or sets selected item used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets selected item used by the control.")]
        public Color SelectedItemColor { get; set; }

        /// <summary>
        /// Gets or sets selected item backcolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets selected item backcolor used by the control.")]
        public Color SelectedItemBackColor { get; set; }

        /// <summary>
        /// Gets or sets hovered item used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets hovered item used by the control.")]
        public Color HoveredItemColor { get; set; }

        /// <summary>
        /// Gets or sets hovered item backcolor used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets hovered item backcolor used by the control.")]
        public Color HoveredItemBackColor { get; set; }

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

        /// <summary>
        /// Gets or sets border color used by the control
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets border color used by the control.")]
        public Color BorderColor { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds an item to collection.
        /// </summary>
        /// <param name="newItem">The Item to be added into the collection.</param>
        public void AddItem(string newItem)
        {
            _Items.Add(newItem);
            InvalidateScroll(this, null);
        }

        /// <summary>
        /// Adds the multiply items to collection.
        /// </summary>
        /// <param name="newItems">Items to be added into the collection.</param>
        public void AddItems(string[] newItems)
        {
            foreach (var str in newItems)
            {
                AddItem(str);
            }
            InvalidateScroll(this, null);
        }

        /// <summary>
        /// Removes the element at the specified index of the collection.
        /// </summary>
        /// <param name="index">The Index as the start point of removing.</param>
        public void RemoveItemAt(int index)
        {
            _Items.RemoveAt(index);
            InvalidateScroll(this, null);
        }

        /// <summary>
        /// Removes an item from collection.
        /// </summary>
        /// <param name="item">The Item to remove in collection.</param>
        public void RemoveItem(string item)
        {
            _Items.Remove(item);
            InvalidateScroll(this, null);
        }

        /// <summary>
        /// Gets the index of the item.
        /// </summary>
        /// <param name="value">The Item.</param>
        /// <returns>index of the item.</returns>
        public int IndexOf(string value)
        {
            return _Items.IndexOf(value);
        }

        /// <summary>
        /// Gets whether the collection cotnain a specific item.
        /// </summary>
        /// <param name="item">The Item to check whether exist in collection.</param>
        /// <returns>Whether the collection cotnain a specific item.</returns>
        public bool Contains(object item)
        {
            return _Items.Contains(item.ToString());
        }

        /// <summary>
        /// Removes multiply items in collection.
        /// </summary>
        /// <param name="itemsToRemove">Items to be removed in collection.</param>
        public void RemoveItems(string[] itemsToRemove)
        {
            foreach (string item in itemsToRemove)
            {
                _Items.Remove(item);
            }
            InvalidateScroll(this, null);
        }

        /// <summary>
        /// Clears the collection.
        /// </summary>
        public void Clear()
        {
            for (int i = _Items.Count - 1; i >= 0; i += -1)
            {
                _Items.RemoveAt(i);
            }
            InvalidateScroll(this, null);
        }

        #endregion Methods
                
        #region Events

        public event SelectedIndexChangedEventHandler SelectedIndexChanged;

        public delegate void SelectedIndexChangedEventHandler(object sender);

        /// <summary>
        /// Here we update the scrollbar and it's properties while user resizes the ListBox.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            InvalidateScroll(this, e);
            InvalidateLayout();
            base.OnSizeChanged(e);
        }

        /// <summary>
        /// Here we will handle the selction item(s).
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            if (e.Button == MouseButtons.Left)
            {
                int index = (SVS.Value / ItemHeight) + (e.Location.Y / ItemHeight);

                if (index >= 0 && index < _Items.Count)
                {
                    if (MultiSelect && _MultiKeyDown)
                    {
                        _Indicates.Add(index);
                        _SelectedItems.Add(Items[index]);
                    }
                    else
                    {
                        _Indicates.Clear();
                        _SelectedItems.Clear();
                        SelectedIndex = index;
                        SelectedIndexChanged?.Invoke(this);
                    }
                }
                Invalidate();
            }
            base.OnMouseDown(e);
        }

        /// <summary>
        /// The Method to update the scrollbar.
        /// </summary>
        /// <param name="sender">object</param>
        private void HandleScroll(object sender)
        {
            Invalidate();
        }

        /// <summary>
        /// The Method to update the Scrollbar maximum property.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">EventArgs</param>
        public void InvalidateScroll(object sender, EventArgs e)
        {
            SVS.Maximum = (_Items.Count * ItemHeight);
            Invalidate();
        }

        /// <summary>
        /// Here here we put scrollbar on focus while mouse clicked.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">MouseEventArgs</param>
        private void VS_MouseDown(object sender, MouseEventArgs e)
        {
            Focus();
        }

        /// <summary>
        /// The Method to update the size and locaion of the scrollbar.
        /// </summary>
        private void InvalidateLayout()
        {
            SVS.Size = new Size(12, Height - (ShowBorder ? 2 : 0));
            SVS.Location = new Point(Width - (SVS.Width + (ShowBorder ? 2 : 0)), (ShowBorder ? 1 : 0));
            Invalidate();
        }
        
        /// <summary>
        /// Here we handle the mouse wheel.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            SVS.Value -= (e.Delta / 4);
            base.OnMouseWheel(e);
        }
        
        /// <summary>
        /// Gets the Key that has been pressed by the user.
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns>The Key that has been pressed.</returns>
        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Down:
                    try
                    {
                        _SelectedItems.Remove(_Items[SelectedIndex]);
                        SelectedIndex += 1;
                        _SelectedItems.Add(_Items[SelectedIndex]);
                    }
                    catch { }
                    break;

                case Keys.Up:
                    try
                    {
                        _SelectedItems.Remove(_Items[SelectedIndex]);
                        SelectedIndex -= 1;
                        _SelectedItems.Add(_Items[SelectedIndex]);
                    }
                    catch { }
                    break;
            }
            Invalidate();
            return base.IsInputKey(keyData);
        }

        /// <summary>
        /// Here we set the handle the hovering.
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Cursor = Cursors.Hand;
            int index = (SVS.Value / ItemHeight) + (e.Location.Y / ItemHeight);

            if (index >= Items.Count)
                index = -1;

            if (index >= 0 && index < Items.Count)
            {
                _HoveredItem = index;
            }
            Invalidate();
        }

        /// <summary>
        /// Here we release the mouse state and hovering item to avoid filckering.
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            _HoveredItem = -1;
            Cursor = Cursors.Default;
            Invalidate();
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Here we put the scrollbar on right of the list box and also update the the thumb size of the scrollbar.
        /// </summary>
        /// <param name="e">Events</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SVS.Location = new Point(Width - (SVS.Width + (ShowBorder ? 2 : 0)), (ShowBorder ? 1 : 0));
            InvalidateScroll(this, e);            
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

    }
}