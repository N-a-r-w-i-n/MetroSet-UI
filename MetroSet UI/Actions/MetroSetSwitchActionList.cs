using MetroSet_UI.Controls;
using MetroSet_UI.Design;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace MetroSet_UI.Tasks
{
    internal class MetroSetSwitchActionList : DesignerActionList
    {
        private readonly MetroSetSwitch metroSetSwitch;

        public MetroSetSwitchActionList(IComponent component) : base(component)
        {
            metroSetSwitch = (MetroSetSwitch)component;
        }

        public Style Style
        {
            get { return metroSetSwitch.Style; }
            set { metroSetSwitch.Style = value; }
        }

        public string ThemeAuthor
        {
            get { return metroSetSwitch.ThemeAuthor; }
        }

        public string ThemeName
        {
            get { return metroSetSwitch.ThemeName; }
        }

        public StyleManager StyleManager
        {
            get { return metroSetSwitch.StyleManager; }
            set { metroSetSwitch.StyleManager = value; }
        }

        public string Text
        {
            get { return metroSetSwitch.Text; }
            set { metroSetSwitch.Text = value; }
        }

        public bool Switched
        {
            get { return metroSetSwitch.Switched; }
            set { metroSetSwitch.Switched = value; }
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
            new DesignerActionPropertyItem("Switched", "Switched", "Appearance", "Gets or sets a value indicating whether the control is switched."),
        };

            return items;
        }
    }
}