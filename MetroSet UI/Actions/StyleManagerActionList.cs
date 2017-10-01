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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms;
using MetroSet_UI.Design;

namespace MetroSet_UI.Tasks
{
    class StyleManagerActionList : DesignerActionList
    {
        private readonly StyleManager styleManger;

        public StyleManagerActionList(IComponent component) : base(component)
        {
            styleManger = (StyleManager)component;
        }

        public Style Style
        {
            get { return styleManger.Style; }
            set { styleManger.Style = value; }
        }

        public string ThemeAuthor
        {
            get { return styleManger.ThemeAuthor; }
        }

        public string ThemeName
        {
            get { return styleManger.ThemeName; }
        }

        [Editor(typeof(StyleManager.FileNamesEditor), typeof(UITypeEditor)), Category("MetroSet Framework"), Description("Gets or sets the custom theme file.")]
        public string CustomTheme
        {
            get { return styleManger.CustomTheme; }
            set { styleManger.CustomTheme = value; }
        }

        public Form OwnerForm
        {
            get { return styleManger.MetroForm; }
            set { styleManger.MetroForm = value; }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection
        {        
            new DesignerActionHeaderItem("MetroSet Framework"),
            new DesignerActionPropertyItem("OwnerForm", "OwnerForm", "MetroSet Framework", "Gets or sets the form (MetroForm) to Apply themes for."),
            new DesignerActionPropertyItem("Style", "Style", "MetroSet Framework", "Gets or sets the style."),
            new DesignerActionPropertyItem("CustomTheme", "CustomTheme", "MetroSet Framework", "Gets or sets the custom theme file."),

            new DesignerActionHeaderItem("Informations"),
            new DesignerActionPropertyItem("ThemeName", "ThemeName", "Informations", "Gets or sets the The Theme name associated with the theme."),
            new DesignerActionPropertyItem("ThemeAuthor", "ThemeAuthor", "Informations", "Gets or sets the The Author name associated with the theme."),

        };

            return items;
        }

    }

}
