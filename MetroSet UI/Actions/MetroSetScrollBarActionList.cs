﻿using System.ComponentModel;
using System.ComponentModel.Design;
using MetroSet_UI.Components;
using MetroSet_UI.Controls;
using MetroSet_UI.Enums; /*
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
	class MetroSetScrollBarActionList : DesignerActionList
	{
		private readonly MetroSetScrollBar _metroSetScrollBar;

		public MetroSetScrollBarActionList(IComponent component) : base(component)
		{
			_metroSetScrollBar = (MetroSetScrollBar)component;
		}

		public Style Style
		{
			get => _metroSetScrollBar.Style;
			set => _metroSetScrollBar.Style = value;
		}

		public string ThemeAuthor => _metroSetScrollBar.ThemeAuthor;

		public string ThemeName => _metroSetScrollBar.ThemeName;

		public StyleManager StyleManager
		{
			get => _metroSetScrollBar.StyleManager;
			set => _metroSetScrollBar.StyleManager = value;
		}

		public int Maximum
		{
			get => _metroSetScrollBar.Maximum;
			set => _metroSetScrollBar.Maximum = value;
		}

		public int Minimum
		{
			get => _metroSetScrollBar.Minimum;
			set => _metroSetScrollBar.Minimum = value;
		}


		public int Value
		{
			get => _metroSetScrollBar.Value;
			set => _metroSetScrollBar.Value = value;
		}


		public int SmallChange
		{
			get => _metroSetScrollBar.SmallChange;
			set => _metroSetScrollBar.SmallChange = value;
		}


		public int LargeChange
		{
			get => _metroSetScrollBar.LargeChange;
			set => _metroSetScrollBar.LargeChange = value;
		}

		public ScrollOrientate Orientation
		{
			get => _metroSetScrollBar.Orientation;
			set => _metroSetScrollBar.Orientation = value;
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
			new DesignerActionPropertyItem("Orientation", "Orientation", "Appearance", "Gets or sets the scroll bar orientation."),

			new DesignerActionHeaderItem("Behavior"),
			new DesignerActionPropertyItem("Maximum", "Maximum", "Behavior", "Gets or sets the upper limit of the scrollable range."),
			new DesignerActionPropertyItem("Minimum", "Minimum", "Behavior", "Gets or sets the lower limit of the scrollable range."),
			new DesignerActionPropertyItem("Value", "Value", "Behavior", "Gets or sets a numeric value that represents the current position of the scroll bar box."),
			new DesignerActionPropertyItem("LargeChange", "LargeChange", "Behavior", "Gets or sets the distance to move a scroll bar in response to a large scroll command."),
			new DesignerActionPropertyItem("SmallChange", "SmallChange", "Behavior", "Gets or sets the distance to move a scroll bar in response to a small scroll command."),

		};

			return items;
		}
	}
}
