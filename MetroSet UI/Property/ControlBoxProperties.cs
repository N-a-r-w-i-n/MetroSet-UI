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
    internal class ControlBoxProperties
    {
        /// <summary>
        /// Gets or sets Close forecolor used by the control
        /// </summary>
        public Color CloseNormalForeColor { get; set; }

        /// <summary>
        /// Gets or sets Close forecolor used by the control
        /// </summary>
        public Color CloseHoverForeColor { get; set; }

        /// <summary>
        /// Gets or sets Close backcolor used by the control
        /// </summary>
        public Color CloseHoverBackColor { get; set; }

        /// <summary>
        /// Gets or sets Maximize forecolor used by the control
        /// </summary>
        public Color MaximizeHoverForeColor { get; set; }

        /// <summary>
        /// Gets or sets Maximize backcolor used by the control
        /// </summary>
        public Color MaximizeHoverBackColor { get; set; }

        /// <summary>
        /// Gets or sets Maximize forecolor used by the control
        /// </summary>
        public Color MaximizeNormalForeColor { get; set; }

        /// <summary>
        /// Gets or sets Minimize forecolor used by the control
        /// </summary>
        public Color MinimizeHoverForeColor { get; set; }

        /// <summary>
        /// Gets or sets Minimize backcolor used by the control
        /// </summary>
        public Color MinimizeHoverBackColor { get; set; }

        /// <summary>
        /// Gets or sets Minimize forecolor used by the control
        /// </summary>
        public Color MinimizeNormalForeColor { get; set; }

        /// <summary>
        /// Gets or sets disabled forecolor used by the control
        /// </summary>
        public Color DisabledForeColor { get; set; }

    }
}