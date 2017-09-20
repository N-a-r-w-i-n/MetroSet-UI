using MetroSet_UI.Design;

namespace MetroSet_UI.Interfaces
{
    public interface iControl
    {
        /// <summary>
        /// Gets or sets the style associated with the control.
        /// </summary>
        Style Style { get; set; }

        /// <summary>
        /// Gets or sets the StyleManager associated with the control.
        /// </summary>
        StyleManager StyleManager { get; set; }

        /// <summary>
        /// Gets or sets the The Author name associated with the theme.
        /// </summary>
        string ThemeAuthor { get; set; }

        /// <summary>
        /// Gets or sets the The Theme name associated with the theme.
        /// </summary>
        string ThemeName { get; set; }
    }


}