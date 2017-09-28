using MetroSet_UI.Controls;
using MetroSet_UI.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace MetroSet_UI.Tasks
{
    public class MetroSetEllipseActionList : DesignerActionList
    {
        private readonly MetroSetEllipse metroSetEllipse;

        public MetroSetEllipseActionList(IComponent component) : base(component)
        {
            metroSetEllipse = (MetroSetEllipse)component;
        }

        public Style Style
        {
            get { return metroSetEllipse.Style; }
            set { metroSetEllipse.Style = value; }
        }

        public string ThemeAuthor
        {
            get { return metroSetEllipse.ThemeAuthor; }
        }

        public string ThemeName
        {
            get { return metroSetEllipse.ThemeName; }
        }

        public StyleManager StyleManager
        {
            get { return metroSetEllipse.StyleManager; }
            set { metroSetEllipse.StyleManager = value; }
        }

        public string Text
        {
            get { return metroSetEllipse.Text; }
            set { metroSetEllipse.Text = value; }
        }

        public Font Font
        {
            get { return metroSetEllipse.Font; }
            set { metroSetEllipse.Font = value; }
        }

        public int BorderThickness
        {
            get { return metroSetEllipse.BorderThickness; }
            set { metroSetEllipse.BorderThickness = value; }
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
            new DesignerActionPropertyItem("Font", "Font", "Appearance", "Gets or sets the The font associated with the control."),
            new DesignerActionPropertyItem("BorderThickness", "BorderThickness", "Appearance", "Gets or sets the border thickness associated with the control."),
        };

            return items;
        }
    }
}