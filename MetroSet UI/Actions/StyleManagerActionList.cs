using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms;
using MetroSet_UI.Design;

namespace MetroSet_UI.Tasks
{
    class StyleManagerActionList : DesignerActionList
    {
        private readonly StyleManager styleManger;

        public StyleManagerActionList(IComponent component) : base(component)
        {
            styleManger = (StyleManager)component;
        }

        public Style Style
        {
            get { return styleManger.Style; }
            set { styleManger.Style = value; }
        }

        public string ThemeAuthor
        {
            get { return styleManger.ThemeAuthor; }
        }

        public string ThemeName
        {
            get { return styleManger.ThemeName; }
        }

        [Editor(typeof(StyleManager.FileNamesEditor), typeof(UITypeEditor)), Category("MetroSet Framework"), Description("Gets or sets the custom theme file.")]
        public string CustomTheme
        {
            get { return styleManger.CustomTheme; }
            set { styleManger.CustomTheme = value; }
        }

        public Form OwnerForm
        {
            get { return styleManger.MetroForm; }
            set { styleManger.MetroForm = value; }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection
        {        
            new DesignerActionHeaderItem("MetroSet Framework"),
            new DesignerActionPropertyItem("OwnerForm", "OwnerForm", "MetroSet Framework", "Gets or sets the form (MetroForm) to Apply themes for."),
            new DesignerActionPropertyItem("Style", "Style", "MetroSet Framework", "Gets or sets the style."),
            new DesignerActionPropertyItem("CustomTheme", "CustomTheme", "MetroSet Framework", "Gets or sets the custom theme file."),

            new DesignerActionHeaderItem("Informations"),
            new DesignerActionPropertyItem("ThemeName", "ThemeName", "Informations", "Gets or sets the The Theme name associated with the theme."),
            new DesignerActionPropertyItem("ThemeAuthor", "ThemeAuthor", "Informations", "Gets or sets the The Author name associated with the theme."),

        };

            return items;
        }

    }

}
