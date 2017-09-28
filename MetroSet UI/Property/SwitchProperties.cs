/**
 * MetroSet UI - MetroSet UI Framewrok
 * 
 * The MIT License (MIT)
 * Copyright (c) 2011 Narwin, https://github.com/N-a-r-w-i-n
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
    internal class SwitchProperties
    {
        /// <summary>
        /// Gets or sets the background color in normal mouse sate.
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// Gets or sets the border color.
        /// </summary>
        public Color BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the CheckdBackColor.
        /// </summary>
        public Color CheckColor { get; set; }

        /// <summary>
        /// Gets or sets the border color while the control disabled.
        /// </summary>
        public Color DisabledBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the CheckdBackColor while disabled.
        /// </summary>
        public Color DisabledCheckColor { get; set; }

        /// <summary>
        /// Gets or sets the UnCheckdBackColor while disabled.
        /// </summary>
        public Color DisabledUnCheckColor { get; set; }

        /// <summary>
        /// Gets or sets whether the control enabled.
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// Gets or sets the foreground color in normal mouse sate.
        /// </summary>
        public Color ForeColor { get; set; }
        /// <summary>
        /// Gets or sets the color of the check symbol.
        /// </summary>
        public Color SymbolColor { get; set; }

        /// <summary>
        /// Gets or sets the UnCheckdBackColor.
        /// </summary>
        public Color UnCheckColor { get; set; }
    }
}