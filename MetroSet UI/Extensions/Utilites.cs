using System.Drawing;
using System.Drawing.Drawing2D;

namespace MetroSet_UI.Extensions
{
    internal class Utilites
    {
        /// <summary>
        /// The Brush with two colors one center another surounding the center based on the given rectangle area.
        /// </summary>
        /// <param name="CenterColor">The Center color of the rectangle.</param>
        /// <param name="SurroundColor">The Surrounding color of the rectangle.</param>
        /// <param name="P">The Point of surrounding.</param>
        /// <param name="Rect">The Rectangle of the brush.</param>
        /// <returns>The Brush with two colors one center another surounding the center.</returns>
        public static PathGradientBrush GlowBrush(Color CenterColor, Color SurroundColor, Point P, Rectangle Rect)
        {
            GraphicsPath GP = new GraphicsPath { FillMode = FillMode.Winding };
            GP.AddRectangle(Rect);
            return new PathGradientBrush(GP)
            {
                CenterColor = CenterColor,
                SurroundColors = new [] { SurroundColor },
                FocusScales = P
            };
        }

        /// <summary>
        /// The Brush from RGBA color.
        /// </summary>
        /// <param name="R">Red.</param>
        /// <param name="G">Green.</param>
        /// <param name="B">Blue.</param>
        /// <param name="A">Alpha.</param>
        /// <returns>The Brush from given RGBA color.</returns>
        public SolidBrush SolidBrushRGBColor(int R, int G, int B, int A = 0)
        {
            return new SolidBrush(Color.FromArgb(A, R, G, B));
        }

        /// <summary>
        /// The Brush from HEX color.
        /// </summary>
        /// <param name="C_WithoutHash">HEX Color without hash.</param>
        /// <returns>The Brush from given HEX color.</returns>
        public SolidBrush SolidBrushHTMlColor(string C_WithoutHash)
        {
            return new SolidBrush(HexColor(C_WithoutHash));
        }

        /// <summary>
        /// The Pen from RGBA color.
        /// </summary>
        /// <param name="R">Red.</param>
        /// <param name="G">Green.</param>
        /// <param name="B">Blue.</param>
        /// <param name="A">Alpha.</param>
        /// <returns>The Pen from given RGBA color.</returns>
        public Pen PenRGBColor(int R, int G, int B, int A, float Size)
        {
            return new Pen(Color.FromArgb(A, R, G, B), Size);
        }

        /// <summary>
        /// The Pen from HEX color.
        /// </summary>
        /// <param name="C_WithoutHash">HEX Color without hash.</param>
        /// <param name="Size">The Size of the pen.</param>
        /// <returns></returns>
        public Pen PenHTMlColor(string C_WithoutHash, float Size = 1)
        {
            return new Pen(HexColor(C_WithoutHash), Size);
        }

        /// <summary>
        /// Gets Color based on given hex color string.
        /// </summary>
        /// <param name="hexColor">Hex Color</param>
        /// <returns>The Color based on given hex color string</returns>
        public Color HexColor(string hexColor)
        {
            return ColorTranslator.FromHtml(hexColor);
        }

        /// <summary>
        /// The Color from HEX by alpha property.
        /// </summary>
        /// <param name="alpha">Alpha.</param>
        /// <param name="hexColor">HEX Color with hash.</param>
        /// <returns>The Color from HEX with given ammount of transparency</returns>
        public Color GetAlphaHexColor(int alpha, string hexColor)
        {
            return Color.FromArgb(alpha, ColorTranslator.FromHtml(hexColor));
        }
    }
}