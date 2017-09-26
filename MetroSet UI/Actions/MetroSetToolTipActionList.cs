using MetroSet_UI.Components;
using MetroSet_UI.Design;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace MetroSet_UI.Tasks
{
    internal class MetroSetToolTipActionList : DesignerActionList
    {
        private readonly MetroSetToolTip metroSetToolTip;

        public MetroSetToolTipActionList(IComponent component) : base(component)
        {
            metroSetToolTip = (MetroSetToolTip)component;
        }

        public Style Style
        {
            get { return metroSetToolTip.Style; }
            set { metroSetToolTip.Style = value; }
        }

        public string ThemeAuthor
        {
            get { return metroSetToolTip.ThemeAuthor; }
        }

        public string ThemeName
        {
            get { return metroSetToolTip.ThemeName; }
        }

        public StyleManager StyleManager
        {
            get { return metroSetToolTip.StyleManager; }
            set { metroSetToolTip.StyleManager = value; }
        }

        public bool Active
        {
            get { return metroSetToolTip.Active; }
            set { metroSetToolTip.Active = value; }
        }

        public int AutomaticDelay
        {
            get { return metroSetToolTip.AutomaticDelay; }
            set { metroSetToolTip.AutomaticDelay = value; }
        }

        public int AutoPopDelay
        {
            get { return metroSetToolTip.AutoPopDelay; }
            set { metroSetToolTip.AutoPopDelay = value; }
        }

        public int InitialDelay
        {
            get { return metroSetToolTip.InitialDelay; }
            set { metroSetToolTip.InitialDelay = value; }
        }

        public bool StripAmpersands
        {
            get { return metroSetToolTip.StripAmpersands; }
            set { metroSetToolTip.StripAmpersands = value; }
        }

        public bool UseAnimation
        {
            get { return metroSetToolTip.UseAnimation; }
            set { metroSetToolTip.UseAnimation = value; }
        }

        public bool UseFading
        {
            get { return metroSetToolTip.UseFading; }
            set { metroSetToolTip.UseFading = value; }
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

            new DesignerActionHeaderItem("Misc"),
                new DesignerActionPropertyItem("Active", "Active", "Misc", "Gets or sets a value indicating whether the ToolTip is currently active."),
                new DesignerActionPropertyItem("AutomaticDelay", "AutomaticDelay", "Misc", "Gets or sets the automatic delay for the ToolTip."),
                new DesignerActionPropertyItem("AutoPopDelay", "AutoPopDelay", "Misc", "Gets or sets the period of time the ToolTip remains visible if the pointer is stationary on a control with specified ToolTip text."),
                new DesignerActionPropertyItem("InitialDelay", "InitialDelay", "Misc", "Gets or sets the time that passes before the ToolTip appears."),
                new DesignerActionPropertyItem("StripAmpersands", "StripAmpersands", "Misc", "Gets or sets a value that determines how ampersand (&) characters are treated."),
                new DesignerActionPropertyItem("UseAnimation", "UseAnimation", "Misc", "Gets or sets a value determining whether an animation effect should be used when displaying the ToolTip."),
                new DesignerActionPropertyItem("UseFading", "UseFading", "Appearance", "Gets or sets a value determining whether a fade effect should be used when displaying the ToolTip."),
            };

            return items;
        }
    }
}