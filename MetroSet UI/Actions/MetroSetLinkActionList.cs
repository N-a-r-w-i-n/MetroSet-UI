﻿/*
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
using System.Drawing;
using System.Windows.Forms;
using MetroSet_UI.Components;
using MetroSet_UI.Controls;
using MetroSet_UI.Enums;
using static System.Windows.Forms.LinkLabel;

namespace MetroSet_UI.Actions
{
	class MetroSetLinkActionList : DesignerActionList
	{
		private readonly MetroSetLink _metroSetLink;

		public MetroSetLinkActionList(IComponent component) : base(component)
		{
			_metroSetLink = (MetroSetLink)component;
		}

		public Style Style
		{
			get => _metroSetLink.Style;
			set => _metroSetLink.Style = value;
		}

		public string ThemeAuthor => _metroSetLink.ThemeAuthor;

		public string ThemeName => _metroSetLink.ThemeName;

		public StyleManager StyleManager
		{
			get => _metroSetLink.StyleManager;
			set => _metroSetLink.StyleManager = value;
		}

		public string Text
		{
			get => _metroSetLink.Text;
			set => _metroSetLink.Text = value;
		}

		public Font Font
		{
			get => _metroSetLink.Font;
			set => _metroSetLink.Font = value;
		}

		public LinkBehavior LinkBehaviour
		{
			get => _metroSetLink.LinkBehavior;
			set => _metroSetLink.LinkBehavior = value;
		}

		public Color LinkColor
		{
			get => _metroSetLink.LinkColor;
			set => _metroSetLink.LinkColor = value;
		}

		public Color ActiveLinkColor
		{
			get => _metroSetLink.ActiveLinkColor;
			set => _metroSetLink.ActiveLinkColor = value;
		}

		public Color VisitedLinkColor
		{
			get => _metroSetLink.VisitedLinkColor;
			set => _metroSetLink.VisitedLinkColor = value;
		}

		public bool LinkVisited
		{
			get => _metroSetLink.LinkVisited;
			set => _metroSetLink.LinkVisited = value;
		}

		public LinkCollection Links => _metroSetLink.Links;

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
