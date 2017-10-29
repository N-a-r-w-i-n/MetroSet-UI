using MetroSet_UI.Design;

namespace MetroSet_UI.Interfaces
{
    public interface iMetroSetForm
    {
        /// <summary>
        ///
        /// </summary>
        Style Style { get; set; }

        /// <summary>
        ///
        /// </summary>
        StyleManager StyleManager { get; set; }

        string ThemeAuthor { get; set; }

        string ThemeName { get; set; }

    }
}