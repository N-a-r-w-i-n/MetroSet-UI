using MetroSet_UI.Controls;
using MetroSet_UI.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace MetroSet_UI.Tasks
{
    internal class MetroSetRichTextBoxActionList : DesignerActionList
    {
        private readonly MetroSetRichTextBox metroSetRichTextBox;

        public MetroSetRichTextBoxActionList(IComponent component) : base(component)
        {
            metroSetRichTextBox = (MetroSetRichTextBox)component;
        }

        public Style Style
        {
            get { return metroSetRichTextBox.Style; }
            set { metroSetRichTextBox.Style = value; }
        }

        public string ThemeAuthor
        {
            get { return metroSetRichTextBox.ThemeAuthor; }
        }

        public string ThemeName
        {
            get { return metroSetRichTextBox.ThemeName; }
        }

        public StyleManager StyleManager
        {
            get { return metroSetRichTextBox.StyleManager; }
            set { metroSetRichTextBox.StyleManager = value; }
        }

        public string Text
        {
            get { return metroSetRichTextBox.Text; }
            set { metroSetRichTextBox.Text = value; }
        }

        public Font Font
        {
            get { return metroSetRichTextBox.Font; }
            set { metroSetRichTextBox.Font = value; }
        }

        public bool ReadOnly
        {
            get { return metroSetRichTextBox.ReadOnly; }
            set { metroSetRichTextBox.ReadOnly = value; }
        }

        public ContextMenuStrip ContextMenuStrip
        {
            get { return metroSetRichTextBox.ContextMenuStrip; }
            set { metroSetRichTextBox.ContextMenuStrip = value; }
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
            new DesignerActionPropertyItem("ReadOnly", "ReadOnly", "Appearance", "Gets or sets a value indicating whether text in the rich text box is read-only."),
            new DesignerActionPropertyItem("ContextMenuStrip", "ContextMenuStrip", "Appearance", "Gets or sets the ContextMenuStrip associated with this control."),
        };

            return items;
        }
    }
}