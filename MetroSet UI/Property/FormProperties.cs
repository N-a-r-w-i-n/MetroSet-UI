using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using MetroSet_UI.Design;

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
