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

namespace MetroSet_UI.Tasks
{
    public class MetroSetTrackBarActionList : DesignerActionList
    {
        private readonly MetroSetTrackBar metroSetTrackBar;

        public MetroSetTrackBarActionList(IComponent component) : base(component)
        {
            metroSetTrackBar = (MetroSetTrackBar)component;
        }

        public Style Style
        {
            get { return metroSetTrackBar.Style; }
            set { metroSetTrackBar.Style = value; }
        }

        public string ThemeAuthor
        {
            get { return metroSetTrackBar.ThemeAuthor; }
        }

        public string ThemeName
        {
            get { return metroSetTrackBar.ThemeName; }
        }

        public StyleManager StyleManager
        {
            get { return metroSetTrackBar.StyleManager; }
            set { metroSetTrackBar.StyleManager = value; }
        }

        public int Maximum
        {
            get { return metroSetTrackBar.Maximum; }
            set { metroSetTrackBar.Maximum = value; }
        }

        public int Minimum
        {
            get { return metroSetTrackBar.Minimum; }
            set { metroSetTrackBar.Minimum = value; }
        }

        public int Value
        {
            get { return metroSetTrackBar.Value; }
            set { metroSetTrackBar.Value = value; }
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
            new DesignerActionPropertyItem("Maximum", "Maximum", "Appearance", "Gets or sets the upper limit of the range this TrackBar is working with."),
            new DesignerActionPropertyItem("Minimum", "Minimum", "Appearance", "Gets or sets the lower limit of the range this TrackBar is working with."),
            new DesignerActionPropertyItem("Value", "Value", "Appearance", "Gets or sets a numeric value that represents the current position of the scroll box on the track bar."),
        };

            return items;
        }
    }
}