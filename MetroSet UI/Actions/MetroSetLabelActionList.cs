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
    class MetroSetLabelActionList : DesignerActionList
    {
        private readonly MetroSetLabel metroSetLabel;

        public MetroSetLabelActionList(IComponent component) : base(component)
        {
            metroSetLabel = (MetroSetLabel)component;
        }

        public Style Style
        {
            get { return metroSetLabel.Style; }
            set { metroSetLabel.Style = value; }
        }

        public string ThemeAuthor
        {
            get { return metroSetLabel.ThemeAuthor; }
        }

        public string ThemeName
        {
            get { return metroSetLabel.ThemeName; }
        }

        public StyleManager StyleManager
        {
            get { return metroSetLabel.StyleManager; }
            set { metroSetLabel.StyleManager = value; }
        }

        public string Text
        {
            get { return metroSetLabel.Text; }
            set { metroSetLabel.Text = value; }
        }

        public Font Font
        {
            get { return metroSetLabel.Font; }
            set { metroSetLabel.Font = value; }
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
            new DesignerActionPropertyItem("Font", "Font", "Appearance", "Gets or sets the The font associated with the control.")
        };

            return items;
        }
    }
}
