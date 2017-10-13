/**
 * MetroSet UI - MetroSet UI Framewrok
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
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace MetroSet_UI.Extensions
{
    internal class Methods
    {
        /// <summary>
        /// The Method to draw the image from encoded base64 string.
        /// </summary>
        /// <param name="G">The Graphics to draw the image.</param>
        /// <param name="Base64Image">The Encoded base64 image.</param>
        /// <param name="Rect">The Rectangle area for the image.</param>
        public void DrawImageFromBase64(Graphics G, string Base64Image, Rectangle Rect)
        {
            Image IM = null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Convert.FromBase64String(Base64Image)))
            {
                IM = Image.FromStream(ms);
                ms.Close();
            }
            G.DrawImage(IM, Rect);
        }

        /// <summary>
        /// The Method to draw the image with custom color.
        /// </summary>
        /// <param name="G"> The Graphic to draw the image.</param>
        /// <param name="R"> The Rectangle area of image.</param>
        /// <param name="_Image"> The image that the custom color applies on it.</param>
        /// <param name="C">The Color that be applied to the image.</param>
        /// <remarks></remarks>
        public void DrawImageWithColor(Graphics G, Rectangle R, Image _Image, Color C)
        {
            float[][] ptsArray = new float[][]
            {
            new float[] {Convert.ToSingle(C.R / 255.0), 0f, 0f, 0f, 0f},
            new float[] {0f, Convert.ToSingle(C.G / 255.0), 0f, 0f, 0f},
            new float[] {0f, 0f, Convert.ToSingle(C.B / 255.0), 0f, 0f},
            new float[] {0f, 0f, 0f, Convert.ToSingle(C.A / 255.0), 0f},
            new float[]
            {
                Convert.ToSingle( C.R/255.0),
                Convert.ToSingle( C.G/255.0),
                Convert.ToSingle( C.B/255.0), 0f,
                Convert.ToSingle( C.A/255.0)
            }
            };
            ImageAttributes imgAttribs = new ImageAttributes();
            imgAttribs.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Default);
            G.DrawImage(_Image, R, 0, 0, _Image.Width, _Image.Height, GraphicsUnit.Pixel, imgAttribs);
            _Image.Dispose();
        }

        /// <summary>
        /// The Method to draw the image with custom color.
        /// </summary>
        /// <param name="G"> The Graphic to draw the image.</param>
        /// <param name="R"> The Rectangle area of image.</param>
        /// <param name="_Image"> The Encoded base64 image that the custom color applies on it.</param>
        /// <param name="C">The Color that be applied to the image.</param>
        /// <remarks></remarks>
        public void DrawImageWithColor(Graphics G, Rectangle R, string _Image, Color C)
        {
            Image IM = ImageFromBase64(_Image);
            float[][] ptsArray = new float[][]
            {
            new float[] {Convert.ToSingle(C.R / 255.0), 0f, 0f, 0f, 0f},
            new float[] {0f, Convert.ToSingle(C.G / 255.0), 0f, 0f, 0f},
            new float[] {0f, 0f, Convert.ToSingle(C.B / 255.0), 0f, 0f},
            new float[] {0f, 0f, 0f, Convert.ToSingle(C.A / 255.0), 0f},
            new float[]
            {
                Convert.ToSingle( C.R/255.0),
                Convert.ToSingle( C.G/255.0),
                Convert.ToSingle( C.B/255.0), 0f,
                Convert.ToSingle( C.A/255.0)
            }
            };
            ImageAttributes imgAttribs = new ImageAttributes();
            imgAttribs.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Default);
            G.DrawImage(IM, R, 0, 0, IM.Width, IM.Height, GraphicsUnit.Pixel, imgAttribs);
        }

        /// <summary>
        /// The String format to provide the alignment.
        /// </summary>
        /// <param name="Horizontal">Horizontal alignment.</param>
        /// <param name="Vertical">Horizontal alignment. alignment.</param>
        /// <returns>The String format.</returns>
        public StringFormat SetPosition(StringAlignment Horizontal = StringAlignment.Center, StringAlignment Vertical = StringAlignment.Center)
        {
            return new StringFormat
            {
                Alignment = Horizontal,
                LineAlignment = Vertical
            };
        }

        /// <summary>
        /// The Matrix array of single from color.
        /// </summary>
        /// <param name="C">The Color.</param>
        /// /// <param name="alpha">The Opacity.</param>
        /// <returns>The Matrix array of single from the given color</returns>
        public float[][] ColorToMatrix(float alpha, Color C)
        {
            return new[]
            {
            new [] {Convert.ToSingle(C.R / 255),0,0,0,0},
            new [] {0,Convert.ToSingle(C.G / 255),0,0,0},
            new [] {0,0,Convert.ToSingle(C.B / 255),0,0},
            new [] {0,0,0,Convert.ToSingle(C.A / 255),0},
            new [] {
                Convert.ToSingle(C.R / 255),
                Convert.ToSingle(C.G / 255),
                Convert.ToSingle(C.B / 255),
                alpha,
                Convert.ToSingle(C.A / 255)
            }
        };
        }


        public void DrawImageWithTransparency(Graphics G, float alpha, Image image, Rectangle rect)
        {
            ColorMatrix colorMatrix = new ColorMatrix { Matrix33 = alpha };
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);
            G.DrawImage(image, new Rectangle(rect.X, rect.Y, image.Width, image.Height), rect.X, rect.Y, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
            imageAttributes.Dispose();
        }

        

        /// <summary>
        /// The Image from encoded base64 image.
        /// </summary>
        /// <param name="Base64Image">The Encoded base64 image</param>
        /// <returns>The Image from encoded base64.</returns>
        public Image ImageFromBase64(string Base64Image)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Convert.FromBase64String(Base64Image)))
            {
                return Image.FromStream(ms);
            }
        }

        /// <summary>
        /// Turns the rectangle to rounded rectangle.
        /// Credits : Aeonhack
        /// </summary>
        /// <param name="r">The Rectangle to fill.</param>
        /// <param name="Curve">The Rounding border radius.</param>
        /// <param name="TopLeft">Wether the top left of rectangle be round or not.</param>
        /// <param name="TopRight">Wether the top right of rectangle be round or not.</param>
        /// <param name="BottomLeft">Wether the bottom left of rectangle be round or not.</param>
        /// <param name="BottomRight">Wether the bottom right of rectangle be round or not.</param>
        /// <returns>the rounded rectangle base one given rectangle</returns>
        public GraphicsPath RoundRec(Rectangle r, int Curve, bool TopLeft = true, bool TopRight = true,
          bool BottomLeft = true, bool BottomRight = true)
        {
            GraphicsPath CreateRoundPath = new GraphicsPath(FillMode.Winding);
            if (TopLeft)
            {
                CreateRoundPath.AddArc(r.X, r.Y, Curve, Curve, 180f, 90f);
            }
            else
            {
                CreateRoundPath.AddLine(r.X, r.Y, r.X, r.Y);
            }
            if (TopRight)
            {
                CreateRoundPath.AddArc(r.Right - Curve, r.Y, Curve, Curve, 270f, 90f);
            }
            else
            {
                CreateRoundPath.AddLine(r.Right - r.Width, r.Y, r.Width, r.Y);
            }
            if (BottomRight)
            {
                CreateRoundPath.AddArc(r.Right - Curve, r.Bottom - Curve, Curve, Curve, 0f, 90f);
            }
            else
            {
                CreateRoundPath.AddLine(r.Right, r.Bottom, r.Right, r.Bottom);
            }
            if (BottomLeft)
            {
                CreateRoundPath.AddArc(r.X, r.Bottom - Curve, Curve, Curve, 90f, 90f);
            }
            else
            {
                CreateRoundPath.AddLine(r.X, r.Bottom, r.X, r.Bottom);
            }
            CreateRoundPath.CloseFigure();
            return CreateRoundPath;
        }

        /// <summary>
        /// Turns the rectangle to rounded rectangle.
        /// Credits : Aeonhack
        /// </summary>
        /// <param name="x">The x-coordinate of the upper-left corner of this Rectangle/param>
        /// <param name="y">The y-coordinate of the upper-left corner of this Rectangle</param>
        /// <param name="width">The Width of the rectangle</param>
        /// <param name="height">The Height of the rectangle</param>
        /// <param name="Curve">The Rounding border radius.</param>
        /// <param name="TopLeft">Wether the top left of rectangle be round or not.</param>
        /// <param name="TopRight">Wether the top right of rectangle be round or not.</param>
        /// <param name="BottomLeft">Wether the bottom left of rectangle be round or not.</param>
        /// <param name="BottomRight">Wether the bottom right of rectangle be round or not.</param>
        /// <returns>the rounded rectangle base one given dimensions</returns>
        /// <returns>the rounded rectangle base one given details</returns>
        public GraphicsPath RoundRec(int x, int y, int width, int height, int Curve, bool TopLeft = true, bool TopRight = true,
          bool BottomLeft = true, bool BottomRight = true)
        {
            Rectangle r = new Rectangle(x, y, width, height);
            GraphicsPath CreateRoundPath = new GraphicsPath(FillMode.Winding);
            if (TopLeft)
            {
                CreateRoundPath.AddArc(r.X, r.Y, Curve, Curve, 180f, 90f);
            }
            else
            {
                CreateRoundPath.AddLine(r.X, r.Y, r.X, r.Y);
            }
            if (TopRight)
            {
                CreateRoundPath.AddArc(r.Right - Curve, r.Y, Curve, Curve, 270f, 90f);
            }
            else
            {
                CreateRoundPath.AddLine(r.Right - r.Width, r.Y, r.Width, r.Y);
            }
            if (BottomRight)
            {
                CreateRoundPath.AddArc(r.Right - Curve, r.Bottom - Curve, Curve, Curve, 0f, 90f);
            }
            else
            {
                CreateRoundPath.AddLine(r.Right, r.Bottom, r.Right, r.Bottom);
            }
            if (BottomLeft)
            {
                CreateRoundPath.AddArc(r.X, r.Bottom - Curve, Curve, Curve, 90f, 90f);
            }
            else
            {
                CreateRoundPath.AddLine(r.X, r.Bottom, r.X, r.Bottom);
            }
            CreateRoundPath.CloseFigure();
            return CreateRoundPath;
        }
    }
}