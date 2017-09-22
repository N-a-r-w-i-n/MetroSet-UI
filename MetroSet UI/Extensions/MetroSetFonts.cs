using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace MetroSet_UI.Extensions
{
    public class MetroSetFonts
    {

        /// <summary>
        /// Gets the font for the most of controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe WP Semilight font.</param>
        /// <returns>The Segoe WP Semilight font with the given size.</returns>
        public static Font SemiLight(float size)
        {
            return GetFont(Properties.Resources.SegoeWP_Semilight, size);
        }

        /// <summary>
        /// Gets the font for the most of controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe WP Light font.</param>
        /// <returns>The Segoe WP Light font with the given size.</returns>
        public static Font Light(float size)
        {
            return GetFont(Properties.Resources.SegoeWP_Light, size);
        }

        /// <summary>
        /// Gets the font for the most of controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe WP SemiBold font.</param>
        /// <returns>The Segoe WP SemiBold font with the given size.</returns>
        public static Font SemiBold(float size)
        {
            return GetFont(Properties.Resources.SegoeWP_Semibold, size);
        }

        /// <summary>
        /// Gets the font for the most of controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe WP Bold font.</param>
        /// <returns>The Segoe WP Bold font with the given size.</returns>
        public static Font Bold(float size)
        {
            return GetFont(Properties.Resources.SegoeWP_Bold, size);
        }


        /// <summary>
        /// Gets or sets the font of the most controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe WP font.</param>
        /// <returns>The Segoe WP font with the given size.</returns>
        public static Font Regular(float size)
        {
            return GetFont(Properties.Resources.SegoeWP, size);
        }

        /// <summary>
        /// Gets the font stored from resources.
        /// </summary>
        /// <param name="fontbyte">The Font stored from resources.</param>
        /// <param name="size">The Desired size for the font</param>
        /// <returns>The Font stored from resources with desired size.</returns>
        public static Font GetFont(byte[] fontbyte, float size)
        {
            using (PrivateFontCollection privateFontCollection = new PrivateFontCollection())
            {
                byte[] fnt = fontbyte;
                IntPtr buffer = Marshal.AllocCoTaskMem(fnt.Length);
                Marshal.Copy(fnt, 0, buffer, fnt.Length);
                privateFontCollection.AddMemoryFont(buffer, fnt.Length);
                return new Font(privateFontCollection.Families[0].Name, size);
            }
        }


    }
}