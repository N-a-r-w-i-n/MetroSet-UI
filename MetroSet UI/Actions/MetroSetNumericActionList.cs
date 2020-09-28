﻿using MetroSet_UI.Controls;
using MetroSet_UI.Design;

/*
 * MetroSet UI - MetroSet UI Framework
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
    internal class MetroSetNumericActionList : DesignerActionList
    {
        private readonly MetroSetNumeric _metroSetNumeric;

        public MetroSetNumericActionList(IComponent component) : base(component)
        {
            _metroSetNumeric = (MetroSetNumeric)component;
        }

        public Style Style
        {
            get => _metroSetNumeric.Style;
            set => _metroSetNumeric.Style = value;
        }

        public string ThemeAuthor => _metroSetNumeric.ThemeAuthor;

        public string ThemeName => _metroSetNumeric.ThemeName;

        public StyleManager StyleManager
        {
            get => _metroSetNumeric.StyleManager;
            set => _metroSetNumeric.StyleManager = value;
        }

        public int Maximum
        {
            get => _metroSetNumeric.Maximum;
            set => _metroSetNumeric.Maximum = value;
        }

        public int Minimum
        {
            get => _metroSetNumeric.Minimum;
            set => _metroSetNumeric.Minimum = value;
        }

        public int Value
        {
            get => _metroSetNumeric.Value;
            set => _metroSetNumeric.Value = value;
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
            new DesignerActionPropertyItem("Value", "Value", "Appearance", "Gets or sets the current number of the Numeric."),
            new DesignerActionPropertyItem("Minimum", "Minimum", "Appearance", "Gets or sets the minimum number of the Numeric."),
            new DesignerActionPropertyItem("Maximum", "Maximum", "Appearance", "Gets or sets the maximum number of the Numeric."),
        };

            return items;
        }
    }
}