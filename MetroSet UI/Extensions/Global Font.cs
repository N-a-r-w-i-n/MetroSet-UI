using System.Drawing;

namespace MetroSet_UI.Extensions
{
    public class Global_Font
    {

        /// <summary>
        /// Gets the font for the most of controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe UI font.</param>
        /// <returns>The Segoe UI font with the given size.</returns>
        public static Font Regular(float size)
        {
            return new Font("Segoe UI", size);
        }

        /// <summary>
        /// Gets the font for the most of controls.
        /// </summary>
        /// <param name="fnt">The Font name.</param>
        /// <param name="size">The Size of the Segoe UI font.</param>
        /// <returns>The Segoe UI font with the given size.</returns>
        public static Font Normal(string fnt, float size)
        {
            return new Font(fnt, size);
        }

        /// <summary>
        /// Gets the font for the most of controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe UI font.</param>
        /// <returns>The Segoe UI font with the given size.</returns>
        public static Font Light(float size)
        {
            return new Font("Segoe UI Light", size);
        }

        /// <summary>
        /// Gets the font for the most of controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe UI font.</param>
        /// <returns>The Segoe UI font with the given size.</returns>
        public static Font Italic(float size)
        {
            return new Font("Segoe UI", size, FontStyle.Italic);
        }

        /// <summary>
        /// Gets the font for the most of controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe UI font.</param>
        /// <returns>The Segoe UI font with the given size.</returns>
        public static Font SemiBold(float size)
        {
            return new Font("Segoe UI semibold", size);
        }

        /// <summary>
        /// Gets the font for the most of controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe UI font.</param>
        /// <returns>The Segoe UI font with the given size.</returns>
        public static Font Bold(float size)
        {
            return new Font("Segoe UI", size, FontStyle.Bold);
        }

    }
}