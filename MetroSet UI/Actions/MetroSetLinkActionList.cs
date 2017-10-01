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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace MetroSet_UI.Tasks
{
    class MetroSetLinkActionList : DesignerActionList
    {
        private readonly MetroSetLink metroSetLink;

        public MetroSetLinkActionList(IComponent component) : base(component)
        {
            metroSetLink = (MetroSetLink)component;
        }

        public Style Style
        {
            get { return metroSetLink.Style; }
            set { metroSetLink.Style = value; }
        }

        public string ThemeAuthor
        {
            get { return metroSetLink.ThemeAuthor; }
        }

        public string ThemeName
        {
            get { return metroSetLink.ThemeName; }
        }

        public StyleManager StyleManager
        {
            get { return metroSetLink.StyleManager; }
            set { metroSetLink.StyleManager = value; }
        }

        public string Text
        {
            get { return metroSetLink.Text; }
            set { metroSetLink.Text = value; }
        }

        public Font Font
        {
            get { return metroSetLink.Font; }
            set { metroSetLink.Font = value; }
        }

        public LinkBehavior LinkBehaviour
        {
            get
            {
                return metroSetLink.LinkBehavior;
            }
            set
            {
                metroSetLink.LinkBehavior = value;
            }
        }
            
        public Color LinkColor
        {
            get { return metroSetLink.LinkColor; }
            set { metroSetLink.LinkColor = value; }
        }

        public Color ActiveLinkColor
        {
            get { return metroSetLink.ActiveLinkColor; }
            set { metroSetLink.ActiveLinkColor = value; }
        }

        public Color VisitedLinkColor
        {
            get { return metroSetLink.VisitedLinkColor; }
            set { metroSetLink.VisitedLinkColor = value; }
        }

        public bool LinkVisited
        {
            get { return metroSetLink.LinkVisited; }
            set { metroSetLink.LinkVisited = value; }
        }

        public LinkCollection Links
        {
            get { return metroSetLink.Links; }
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
            new DesignerActionPropertyItem("LinkVisited", "LinkVisited", "Appearance", "Gets or sets a value indicating whether a link should be displayed as though it were visited."),
            new DesignerActionPropertyItem("LinkColor", "LinkColor", "Appearance", "Gets or sets the color used when displaying a normal link."),
            new DesignerActionPropertyItem("ActiveLinkColor", "ActiveLinkColor", "Appearance", "Gets or sets the color used to display an active link."),
            new DesignerActionPropertyItem("VisitedLinkColor", "VisitedLinkColor", "Appearance", "Gets or sets the color used when displaying a link that that has been previously visited."),
            
            new DesignerActionHeaderItem("Behaviour"),
            new DesignerActionPropertyItem("LinkBehaviour", "LinkBehaviour", "Behaviour", "Gets or sets a value that represents the behavior of a link."),
            new DesignerActionPropertyItem("Links", "Links", "Behaviour", "Gets the collection of links contained within the LinkLabel.")
            };

            return items;
        }

    }
}
