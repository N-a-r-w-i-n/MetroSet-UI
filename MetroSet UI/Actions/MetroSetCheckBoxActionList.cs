using MetroSet_UI.Controls;
using MetroSet_UI.Design;
using MetroSet_UI.Enums;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace MetroSet_UI.Tasks
{
    internal class MetroSetCheckBoxActionList : DesignerActionList
    {
        private readonly MetroSetCheckBox metroSetCheckBox;

        public MetroSetCheckBoxActionList(IComponent component) : base(component)
        {
            metroSetCheckBox = (MetroSetCheckBox)component;
        }

        public Style Style
        {
            get { return metroSetCheckBox.Style; }
            set { metroSetCheckBox.Style = value; }
        }

        public string ThemeAuthor
        {
            get { return metroSetCheckBox.ThemeAuthor; }
        }

        public string ThemeName
        {
            get { return metroSetCheckBox.ThemeName; }
        }

        public StyleManager StyleManager
        {
            get { return metroSetCheckBox.StyleManager; }
            set { metroSetCheckBox.StyleManager = value; }
        }

        public string Text
        {
            get { return metroSetCheckBox.Text; }
            set { metroSetCheckBox.Text = value; }
        }

        public bool Checked
        {
            get { return metroSetCheckBox.Checked; }
            set { metroSetCheckBox.Checked = value; }
        }

        public SignStyle SignStyle
        {
            get { return metroSetCheckBox.SignStyle; }
            set { metroSetCheckBox.SignStyle = value; }
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
            new DesignerActionPropertyItem("Checked", "Checked", "Appearance", "Gets or sets a value indicating whether the control is checked."),
            new DesignerActionPropertyItem("SignStyle", "SignStyle", "Appearance", "Gets or sets the the sign style of check.")
        };

            return items;
        }
    }
}