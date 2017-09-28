using MetroSet_UI.Controls;
using MetroSet_UI.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace MetroSet_UI.Tasks
{
    public class MetroSetTileActionList : DesignerActionList
    {
        private readonly MetroSetTile metroSetTile;

        public MetroSetTileActionList(IComponent component) : base(component)
        {
            metroSetTile = (MetroSetTile)component;
        }

        public Style Style
        {
            get { return metroSetTile.Style; }
            set { metroSetTile.Style = value; }
        }

        public string ThemeAuthor
        {
            get { return metroSetTile.ThemeAuthor; }
        }

        public string ThemeName
        {
            get { return metroSetTile.ThemeName; }
        }

        public StyleManager StyleManager
        {
            get { return metroSetTile.StyleManager; }
            set { metroSetTile.StyleManager = value; }
        }

        public string Text
        {
            get { return metroSetTile.Text; }
            set { metroSetTile.Text = value; }
        }

        public Font Font
        {
            get { return metroSetTile.Font; }
            set { metroSetTile.Font = value; }
        }

        public Image BackgroundImage
        {
            get { return metroSetTile.BackgroundImage; }
            set { metroSetTile.BackgroundImage = value; }
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
            new DesignerActionPropertyItem("BackgroundImage", "BackgroundImage", "Appearance", "Gets or sets the BackgroundImage associated with the control."),
        };

            return items;
        }
    }
}