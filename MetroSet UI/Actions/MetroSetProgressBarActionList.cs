using MetroSet_UI.Controls;
using MetroSet_UI.Design;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace MetroSet_UI.Tasks
{
    internal class MetroSetProgressBarActionList : DesignerActionList
    {
        private readonly MetroSetProgressBar metroSetProgressBar;

        public MetroSetProgressBarActionList(IComponent component) : base(component)
        {
            metroSetProgressBar = (MetroSetProgressBar)component;
        }

        public Style Style
        {
            get { return metroSetProgressBar.Style; }
            set { metroSetProgressBar.Style = value; }
        }

        public string ThemeAuthor
        {
            get { return metroSetProgressBar.ThemeAuthor; }
        }

        public string ThemeName
        {
            get { return metroSetProgressBar.ThemeName; }
        }

        public StyleManager StyleManager
        {
            get { return metroSetProgressBar.StyleManager; }
            set { metroSetProgressBar.StyleManager = value; }
        }

        public int Value
        {
            get { return metroSetProgressBar.Value; }
            set { metroSetProgressBar.Value = value; }
        }

        public int Maximum
        {
            get { return metroSetProgressBar.Maximum; }
            set { metroSetProgressBar.Maximum = value; }
        }

        public int Minimum
        {
            get { return metroSetProgressBar.Minimum; }
            set { metroSetProgressBar.Minimum = value; }
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
            new DesignerActionPropertyItem("Minimum", "Minimum", "Appearance", "Gets or sets the minimum value of the progressbar."),
            new DesignerActionPropertyItem("Maximum", "Maximum", "Appearance", "Gets or sets the maximum value of the progressbar."),
            new DesignerActionPropertyItem("Value", "Value", "Appearance", "Gets or sets the current position of the progressbar."),
        };

            return items;
        }
    }
}