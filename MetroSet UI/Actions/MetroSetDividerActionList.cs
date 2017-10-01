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
using MetroSet_UI.Enums;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace MetroSet_UI.Tasks
{
    internal class MetroSetDividerActionList : DesignerActionList
    {
        private readonly MetroSetDivider metroSetDivider;

        public MetroSetDividerActionList(IComponent component) : base(component)
        {
            metroSetDivider = (MetroSetDivider)component;
        }

        public Style Style
        {
            get { return metroSetDivider.Style; }
            set { metroSetDivider.Style = value; }
        }

        public string ThemeAuthor
        {
            get { return metroSetDivider.ThemeAuthor; }
        }

        public string ThemeName
        {
            get { return metroSetDivider.ThemeName; }
        }

        public StyleManager StyleManager
        {
            get { return metroSetDivider.StyleManager; }
            set { metroSetDivider.StyleManager = value; }
        }

        public DividerStyle Orientation
        {
            get { return metroSetDivider.Orientation; }
            set { metroSetDivider.Orientation = value; }
        }

        public int Thickness
        {
            get { return metroSetDivider.Thickness; }
            set { metroSetDivider.Thickness = value; }
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
            new DesignerActionPropertyItem("Orientation", "Orientation", "Appearance", "Gets or sets Orientation of the control."),
            new DesignerActionPropertyItem("Thickness", "Thickness", "Appearance", "Gets or sets the divider thickness."),
            };

            return items;
        }
    }
}