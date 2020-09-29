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
using MetroSet_UI.Components;
using MetroSet_UI.Controls;
using MetroSet_UI.Enums;

namespace MetroSet_UI.Actions
{
	internal class MetroSetCheckBoxActionList : DesignerActionList
	{
		private readonly MetroSetCheckBox _metroSetCheckBox;

		public MetroSetCheckBoxActionList(IComponent component) : base(component)
		{
			_metroSetCheckBox = (MetroSetCheckBox)component;
		}

		public Style Style
		{
			get => _metroSetCheckBox.Style;
			set => _metroSetCheckBox.Style = value;
		}

		public string ThemeAuthor => _metroSetCheckBox.ThemeAuthor;

		public string ThemeName => _metroSetCheckBox.ThemeName;

		public StyleManager StyleManager
		{
			get => _metroSetCheckBox.StyleManager;
			set => _metroSetCheckBox.StyleManager = value;
		}

		public string Text
		{
			get => _metroSetCheckBox.Text;
			set => _metroSetCheckBox.Text = value;
		}

		public bool Checked
		{
			get => _metroSetCheckBox.Checked;
			set => _metroSetCheckBox.Checked = value;
		}

		public SignStyle SignStyle
		{
			get => _metroSetCheckBox.SignStyle;
			set => _metroSetCheckBox.SignStyle = value;
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
			new DesignerActionPropertyItem("Checked", "Checked", "Appearance", "Gets or sets a value indicating whether the control is checked."),
			new DesignerActionPropertyItem("SignStyle", "SignStyle", "Appearance", "Gets or sets the the sign style of check.")
		};

			return items;
		}
	}
}