using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace MetroSet_UI.Extensions
{
    public class Global_Font
    {

        /// <summary>
        /// Gets the font for the most of controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe WP Semilight font.</param>
        /// <returns>The Segoe WP Semilight font with the given size.</returns>
        public static Font SemiLight(float size)
        {
            using (PrivateFontCollection privateFontCollection = new PrivateFontCollection())
            {
                byte[] fnt = Properties.Resources.SegoeWP_Semilight;
                IntPtr buffer = Marshal.AllocCoTaskMem(fnt.Length);
                Marshal.Copy(fnt, 0, buffer, fnt.Length);
                privateFontCollection.AddMemoryFont(buffer, fnt.Length);
                return new Font(privateFontCollection.Families[0].Name, size);
            }
        }

        /// <summary>
        /// Gets the font for the most of controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe WP Light font.</param>
        /// <returns>The Segoe WP Light font with the given size.</returns>
        public static Font Light(float size)
        {
            using (PrivateFontCollection privateFontCollection = new PrivateFontCollection())
            {
                byte[] fnt = Properties.Resources.SegoeWP_Light;
                IntPtr buffer = Marshal.AllocCoTaskMem(fnt.Length);
                Marshal.Copy(fnt, 0, buffer, fnt.Length);
                privateFontCollection.AddMemoryFont(buffer, fnt.Length);
                return new Font(privateFontCollection.Families[0].Name, size);
            }
        }

        /// <summary>
        /// Gets the font for the most of controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe WP SemiBold font.</param>
        /// <returns>The Segoe WP SemiBold font with the given size.</returns>
        public static Font SemiBold(float size)
        {
            using (PrivateFontCollection privateFontCollection = new PrivateFontCollection())
            {
                byte[] fnt = Properties.Resources.SegoeWP_Semibold;
                IntPtr buffer = Marshal.AllocCoTaskMem(fnt.Length);
                Marshal.Copy(fnt, 0, buffer, fnt.Length);
                privateFontCollection.AddMemoryFont(buffer, fnt.Length);
                return new Font(privateFontCollection.Families[0].Name, size);
            }
        }

        /// <summary>
        /// Gets the font for the most of controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe WP Bold font.</param>
        /// <returns>The Segoe WP Bold font with the given size.</returns>
        public static Font Bold(float size)
        {
            using (PrivateFontCollection privateFontCollection = new PrivateFontCollection())
            {
                byte[] fnt = Properties.Resources.SegoeWP_Bold;
                IntPtr buffer = Marshal.AllocCoTaskMem(fnt.Length);
                Marshal.Copy(fnt, 0, buffer, fnt.Length);
                privateFontCollection.AddMemoryFont(buffer, fnt.Length);
                return new Font(privateFontCollection.Families[0].Name, size);
            }
        }


        /// <summary>
        /// Gets or sets the font of the most controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe WP font.</param>
        /// <returns>The Segoe WP font with the given size.</returns>
        public static Font Regular(float size)
        {
            using (PrivateFontCollection privateFontCollection = new PrivateFontCollection())
            {
                byte[] fnt = Properties.Resources.SegoeWP;
                IntPtr buffer = Marshal.AllocCoTaskMem(fnt.Length);
                Marshal.Copy(fnt, 0, buffer, fnt.Length);
                privateFontCollection.AddMemoryFont(buffer, fnt.Length);
                return new Font(privateFontCollection.Families[0].Name, size);
            }
        }

    }
}