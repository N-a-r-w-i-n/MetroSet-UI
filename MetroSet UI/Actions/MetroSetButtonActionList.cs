using MetroSet_UI.Controls;
using MetroSet_UI.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace MetroSet_UI.Tasks
{
    public class MetroSetButtonActionList : DesignerActionList
    {
        private readonly MetroSetButton metroSetButton;

        public MetroSetButtonActionList(IComponent component) : base(component)
        {
            metroSetButton = (MetroSetButton)component;
        }

        public Style Style
        {
            get { return metroSetButton.Style; }
            set { metroSetButton.Style = value; }
        }

        public string ThemeAuthor
        {
            get { return metroSetButton.ThemeAuthor; }
        }

        public string ThemeName
        {
            get { return metroSetButton.ThemeName; }
        }

        public StyleManager StyleManager
        {
            get { return metroSetButton.StyleManager; }
            set { metroSetButton.StyleManager = value; }
        }

        public string Text
        {
            get { return metroSetButton.Text; }
            set { metroSetButton.Text = value; }
        }

        public Font Font
        {
            get { return metroSetButton.Font; }
            set { metroSetButton.Font = value; }
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