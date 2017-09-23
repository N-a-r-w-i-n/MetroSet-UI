using MetroSet_UI.Controls;
using MetroSet_UI.Design;
using MetroSet_UI.Enums;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace MetroSet_UI.Tasks
{
    internal class MetroSetDividerActionList : DesignerActionList
    {
        private readonly MetroSetDivider metroSetDivider;

        public MetroSetDividerActionList(IComponent component) : base(component)
        {
            metroSetDivider = (MetroSetDivider)component;
        }

        public Style Style
        {
            get { return metroSetDivider.Style; }
            set { metroSetDivider.Style = value; }
        }

        public string ThemeAuthor
        {
            get { return metroSetDivider.ThemeAuthor; }
        }

        public string ThemeName
        {
            get { return metroSetDivider.ThemeName; }
        }

        public StyleManager StyleManager
        {
            get { return metroSetDivider.StyleManager; }
            set { metroSetDivider.StyleManager = value; }
        }

        public DividerStyle Orientation
        {
            get { return metroSetDivider.Orientation; }
            set { metroSetDivider.Orientation = value; }
        }

        public int Thickness
        {
            get { return metroSetDivider.Thickness; }
            set { metroSetDivider.Thickness = value; }
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
            new DesignerActionPropertyItem("Orientation", "Orientation", "Appearance", "Gets or sets Orientation of the control."),
            new DesignerActionPropertyItem("Thickness", "Thickness", "Appearance", "Gets or sets the divider thickness."),
            };

            return items;
        }
    }
}