﻿using System.ComponentModel;
using System.ComponentModel.Design;
using MetroSet_UI.Components;
using MetroSet_UI.Controls;
using MetroSet_UI.Enums;

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

namespace MetroSet_UI.Actions
{
    internal class MetroSetControlBoxActionList : DesignerActionList
    {
        private readonly MetroSetControlBox _metroSetControBox;

        public MetroSetControlBoxActionList(IComponent component) : base(component)
        {
            _metroSetControBox = (MetroSetControlBox)component;
        }

        public Style Style
        {
            get => _metroSetControBox.Style;
            set => _metroSetControBox.Style = value;
        }

        public string ThemeAuthor => _metroSetControBox.ThemeAuthor;

        public string ThemeName => _metroSetControBox.ThemeName;

        public StyleManager StyleManager
        {
            get => _metroSetControBox.StyleManager;
            set => _metroSetControBox.StyleManager = value;
        }

        public bool MaximizeBox
        {
            get => _metroSetControBox.MaximizeBox;
            set => _metroSetControBox.MaximizeBox = value;
        }

        public bool MinimizeBox
        {
            get => _metroSetControBox.MinimizeBox;
            set => _metroSetControBox.MinimizeBox = value;
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
            new DesignerActionPropertyItem("MaximizeBox", "Enable MaximizeBox", "Appearance", "Gets or sets a value indicating whether the Maximize button is Enabled in the caption bar of the form."),
            new DesignerActionPropertyItem("MinimizeBox", "Enable MinimizeBox", "Appearance", "Gets or sets a value indicating whether the Minimize button is Enabled in the caption bar of the form.")
        };

            return items;
        }
    }
}