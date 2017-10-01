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

using MetroSet_UI.Controls;
using MetroSet_UI.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace MetroSet_UI.Tasks
{
    internal class MetroSetTextBoxActionList : DesignerActionList
    {
        private readonly MetroSetTextBox metroSetTextBox;

        public MetroSetTextBoxActionList(IComponent component) : base(component)
        {
            metroSetTextBox = (MetroSetTextBox)component;
        }

        public Style Style
        {
            get { return metroSetTextBox.Style; }
            set { metroSetTextBox.Style = value; }
        }

        public string ThemeAuthor
        {
            get { return metroSetTextBox.ThemeAuthor; }
        }

        public string ThemeName
        {
            get { return metroSetTextBox.ThemeName; }
        }

        public StyleManager StyleManager
        {
            get { return metroSetTextBox.StyleManager; }
            set { metroSetTextBox.StyleManager = value; }
        }

        public string Text
        {
            get { return metroSetTextBox.Text; }
            set { metroSetTextBox.Text = value; }
        }

        public Font Font
        {
            get { return metroSetTextBox.Font; }
            set { metroSetTextBox.Font = value; }
        }

        public bool ReadOnly
        {
            get { return metroSetTextBox.ReadOnly; }
            set { metroSetTextBox.ReadOnly = value; }
        }

        public bool UseSystemPasswordChar
        {
            get { return metroSetTextBox.UseSystemPasswordChar; }
            set { metroSetTextBox.UseSystemPasswordChar = value; }
        }

        public bool Multiline
        {
            get { return metroSetTextBox.Multiline; }
            set { metroSetTextBox.Multiline = value; }
        }

        public string WatermarkText
        {
            get { return metroSetTextBox.WatermarkText; }
            set { metroSetTextBox.WatermarkText = value; }
        }

        public ContextMenuStrip ContextMenuStrip
        {
            get { return metroSetTextBox.ContextMenuStrip; }
            set { metroSetTextBox.ContextMenuStrip = value; }
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
            new DesignerActionPropertyItem("Text", "Text", "Appearance", "Gets or sets the The text associated with the control."),
            new DesignerActionPropertyItem("Font", "Font", "Appearance", "Gets or sets the The font associated with the control."),
            new DesignerActionPropertyItem("ReadOnly", "ReadOnly", "Appearance", "Gets or sets a value indicating whether text in the text box is read-only."),
            new DesignerActionPropertyItem("UseSystemPasswordChar", "UseSystemPasswordChar", "Appearance", "Gets or sets a value indicating whether the text in the TextBox control should appear as the default password character."),
            new DesignerActionPropertyItem("Multiline", "Multiline", "Appearance", "Gets or sets a value indicating whether this is a multiline TextBox control."),
            new DesignerActionPropertyItem("WatermarkText", "WatermarkText", "Appearance", "Gets or sets the text in the TextBox while being empty."),
            new DesignerActionPropertyItem("ContextMenuStrip", "ContextMenuStrip", "Appearance", "Gets or sets the ContextMenuStrip associated with this control."),
        };

            return items;
        }
    }
}