using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;
using MetroSet_UI.Controls;
using MetroSet_UI.Design;

namespace MetroSet_UI.Tasks
{
    class MetroSetNumericActionList : DesignerActionList
    {
        private readonly MetroSetNumeric metroSetNumeric;

        public MetroSetNumericActionList(IComponent component) : base(component)
        {
            metroSetNumeric = (MetroSetNumeric)component;
        }

        public Style Style
        {
            get { return metroSetNumeric.Style; }
            set { metroSetNumeric.Style = value; }
        }

        public string ThemeAuthor
        {
            get { return metroSetNumeric.ThemeAuthor; }
        }

        public string ThemeName
        {
            get { return metroSetNumeric.ThemeName; }
        }

        public StyleManager StyleManager
        {
            get { return metroSetNumeric.StyleManager; }
            set { metroSetNumeric.StyleManager = value; }
        }

        public int Maximum
        {
            get { return metroSetNumeric.Maximum; }
            set { metroSetNumeric.Maximum = value; }
        }

        public int Minimum
        {
            get { return metroSetNumeric.Minimum; }
            set { metroSetNumeric.Minimum = value; }
        }

        public int Value
        {
            get { return metroSetNumeric.Value; }
            set { metroSetNumeric.Value = value; }
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
