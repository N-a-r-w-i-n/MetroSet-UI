using MetroSet_UI.Child;
using MetroSet_UI.Controls;
using MetroSet_UI.Design;

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

using System.ComponentModel;
using System.ComponentModel.Design;

namespace MetroSet_UI.Tasks
{
    internal class MetroSetListBoxActionList : DesignerActionList
    {
        private readonly MetroSetListBox _metroSetListBox;

        public MetroSetListBoxActionList(IComponent component) : base(component)
        {
            _metroSetListBox = (MetroSetListBox)component;
        }

        public Style Style
        {
            get => _metroSetListBox.Style;
            set => _metroSetListBox.Style = value;
        }

        public string ThemeAuthor => _metroSetListBox.ThemeAuthor;

        public string ThemeName => _metroSetListBox.ThemeName;

        public StyleManager StyleManager
        {
            get => _metroSetListBox.StyleManager;
            set => _metroSetListBox.StyleManager = value;
        }

        [TypeConverter(typeof(CollectionConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public MetroSetItemCollection Items => _metroSetListBox.Items;

        public int ItemHeight
        {
            get => _metroSetListBox.ItemHeight;
            set => _metroSetListBox.ItemHeight = value;
        }

        public bool MultiSelect
        {
            get => _metroSetListBox.MultiSelect;
            set => _metroSetListBox.MultiSelect = value;
        }

        public bool ShowScrollBar
        {
            get => _metroSetListBox.ShowScrollBar;
            set => _metroSetListBox.ShowScrollBar = value;
        }

        public bool ShowBorder
        {
            get => _metroSetListBox.ShowBorder;
            set => _metroSetListBox.ShowBorder = value;
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection
        {
            new DesignerActionHeaderItem("MetroSet Framework"),
            new DesignerActionPropertyItem("StyleManager", "StyleManager", "MetroSet Framework", "Gets or sets the stylemanager for the control."),
            new DesignerActionPropertyItem("Style", "Style", "MetroSet Framework", "Gets or sets the style."),

            new DesignerActionHeaderItem("Informations"),
            new DesignerActionPropertyItem("ThemeName", "ThemeName", "Informations", "Gets or sets the The Theme name associated with the theme."),
            new DesignerActionPropertyItem("ThemeAuthor", "ThemeAuthor", "Informations", "Gets or sets the The Author name associated with the theme."),

            new DesignerActionHeaderItem("Appearance"),
            new DesignerActionPropertyItem("Items", "Items", "Appearance", "Gets the items of the ListBox."),
            new DesignerActionPropertyItem("ItemHeight", "ItemHeight", "Appearance", "Gets or sets the height of an item in the ListBox."),
            new DesignerActionPropertyItem("MultiSelect", "MultiSelect", "Appearance", "Gets or sets a value indicating whether the ListBox supports multiple rows."),
            new DesignerActionPropertyItem("ShowScrollBar", "ShowScrollBar", "Appearance", "Gets or sets a value indicating whether the vertical scroll bar is shown or not."),
            new DesignerActionPropertyItem("ShowBorder", "ShowBorder", "Appearance", "Gets or sets a value indicating whether the border shown or not."),
        };

            return items;
        }
    }
}