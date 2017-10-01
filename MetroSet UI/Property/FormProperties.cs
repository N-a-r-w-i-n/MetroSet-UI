using MetroSet_UI.Design;

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

using System.Drawing;

namespace MetroSet_UI.Property
{
    internal class FormProperties
    {
        /// <summary>
        /// Gets or sets whether the control enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the form backcolor.
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the form forecolor.
        /// </summary>
        public Color ForeColor { get; set; }

        /// <summary>
        /// Gets or sets the form bordercolor.
        /// </summary>
        public Color BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the form textcolor.
        /// </summary>
        public Color TextColor { get; set; }

        /// <summary>
        /// Gets or sets the form textcolor.
        /// </summary>
        public bool DrawLeftRect { get; set; }

        /// <summary>
        /// Gets or sets the form textcolor.
        /// </summary>
        public bool DisplayHeader { get; set; }

        /// <summary>
        /// Gets or sets the form textcolor.
        /// </summary>
        public TextAlign TextAlign { get; set; }

        /// <summary>
        /// Gets or sets the form small line color 1.
        /// </summary>
        public Color SmallLineColor1 { get; set; }

        /// <summary>
        /// Gets or sets the form small line color 2.
        /// </summary>
        public Color SmallLineColor2 { get; set; }

        /// <summary>
        /// Gets or sets the header color.
        /// </summary>
        public Color HeaderColor { get; set; }

        /// <summary>
        /// Gets or sets the header height.
        /// </summary>
        public int HeaderHeight { get; set; }
    }
}