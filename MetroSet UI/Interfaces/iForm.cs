using MetroSet_UI.Design;

namespace MetroSet_UI.Interfaces
{
    public interface iForm
    {
        /// <summary>
        /// Gets or sets the style associated with the Form.
        /// </summary>
        Style Style { get; set; }

        /// <summary>
        /// Gets or sets the StyleManager associated with the Form.
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