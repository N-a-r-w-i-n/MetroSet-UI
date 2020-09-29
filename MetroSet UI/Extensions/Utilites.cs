﻿/*
 * MetroSet UI - MetroSet UI Framework
 * 
 * The MIT License (MIT)
 * Copyright (c) 2017 Narwin, https://github.com/N-a-r-w-i-n
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"), to deal in the 
 * Software without restriction, including without limitation the rights to use, copy, 
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
 * and to permit persons to whom the Software is furnished to do so, subject to the 
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR alpha 
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

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
        /// <param name="red">Red.</param>
        /// <param name="green">Green.</param>
        /// <param name="blue">Blue.</param>
        /// <param name="alpha">Alpha.</param>
        /// <param name="size"></param>
        /// <returns>The Pen from given RGBA color.</returns>
        public Pen PenRGBColor(int red, int green, int blue, int alpha, float size)
        {
            return new Pen(Color.FromArgb(alpha, red, green, blue), size);
        }

        /// <summary>
        /// The Pen from HEX color.
        /// </summary>
        /// <param name="colorWithoutHash">HEX Color without hash.</param>
        /// <param name="size">The size of the pen.</param>
        /// <returns></returns>
        public Pen PenHTMlColor(string colorWithoutHash, float size = 1)
        {
            return new Pen(HexColor(colorWithoutHash), size);
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

        // Check and create handle of control
        // Credits :
        //     control invalidate does not trigger the paint event of hidden or invisible control
        //     see https://stackoverflow.com/questions/38137654
        //     force create handle
        //     see https://stackoverflow.com/questions/1807921/
        /// <summary>
        /// Initialize the Handle of Control and child controls if their handle were not created
        /// </summary>
        public void InitControlHandle(Control ctrl)
        {
	        if (ctrl.IsHandleCreated) return;
	        var unused = ctrl.Handle;
            foreach (Control child in ctrl.Controls)
            {
	            InitControlHandle(child);
            }
        }
    }
}