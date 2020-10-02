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
using MetroSet_UI.Components;
using MetroSet_UI.Enums;

namespace MetroSet_UI.Actions
{
	internal class MetroSetToolTipActionList : DesignerActionList
	{
		private readonly MetroSetSetToolTip _metroSetSetToolTip;

		public MetroSetToolTipActionList(IComponent component) : base(component)
		{
			_metroSetSetToolTip = (MetroSetSetToolTip)component;
		}

		public Style Style
		{
			get => _metroSetSetToolTip.Style;
			set => _metroSetSetToolTip.Style = value;
		}

		public string ThemeAuthor => _metroSetSetToolTip.ThemeAuthor;

		public string ThemeName => _metroSetSetToolTip.ThemeName;

		public StyleManager StyleManager
		{
			get => _metroSetSetToolTip.StyleManager;
			set => _metroSetSetToolTip.StyleManager = value;
		}

		public bool Active
		{
			get => _metroSetSetToolTip.Active;
			set => _metroSetSetToolTip.Active = value;
		}

		public int AutomaticDelay
		{
			get => _metroSetSetToolTip.AutomaticDelay;
			set => _metroSetSetToolTip.AutomaticDelay = value;
		}

		public int AutoPopDelay
		{
			get => _metroSetSetToolTip.AutoPopDelay;
			set => _metroSetSetToolTip.AutoPopDelay = value;
		}

		public int InitialDelay
		{
			get => _metroSetSetToolTip.InitialDelay;
			set => _metroSetSetToolTip.InitialDelay = value;
		}

		public bool StripAmpersands
		{
			get => _metroSetSetToolTip.StripAmpersands;
			set => _metroSetSetToolTip.StripAmpersands = value;
		}

		public bool UseAnimation
		{
			get => _metroSetSetToolTip.UseAnimation;
			set => _metroSetSetToolTip.UseAnimation = value;
		}

		public bool UseFading
		{
			get => _metroSetSetToolTip.UseFading;
			set => _metroSetSetToolTip.UseFading = value;
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

			new DesignerActionHeaderItem("Misc"),
				new DesignerActionPropertyItem("Active", "Active", "Misc", "Gets or sets a value indicating whether the ToolTip is currently active."),
				new DesignerActionPropertyItem("AutomaticDelay", "AutomaticDelay", "Misc", "Gets or sets the automatic delay for the ToolTip."),
				new DesignerActionPropertyItem("AutoPopDelay", "AutoPopDelay", "Misc", "Gets or sets the period of time the ToolTip remains visible if the pointer is stationary on a control with specified ToolTip text."),
				new DesignerActionPropertyItem("InitialDelay", "InitialDelay", "Misc", "Gets or sets the time that passes before the ToolTip appears."),
				new DesignerActionPropertyItem("StripAmpersands", "StripAmpersands", "Misc", "Gets or sets a value that determines how ampersand (&) characters are treated."),
				new DesignerActionPropertyItem("UseAnimation", "UseAnimation", "Misc", "Gets or sets a value determining whether an animation effect should be used when displaying the ToolTip."),
				new DesignerActionPropertyItem("UseFading", "UseFading", "Appearance", "Gets or sets a value determining whether a fade effect should be used when displaying the ToolTip."),
			};

			return items;
		}
	}
}