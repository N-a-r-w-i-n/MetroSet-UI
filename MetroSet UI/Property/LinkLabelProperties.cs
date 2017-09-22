using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MetroSet_UI.Property
{
    internal class LinkLabelProperties
    {
        /// <summary>
        /// Gets or sets whether the control enabled.
        /// </summary>
        public bool Enabled { get; set; }

        ///// <summary>
        ///// Gets or sets the font used by the control.
        ///// </summary>
        public string Font { get; set; }

        /// <summary>
        /// Gets or sets the font size used by the control.
        /// </summary>
        public float FontSize { get; set; }

        /// <summary>
        /// Gets or sets forecolor used by the control
        /// </summary>
        public Color ForeColor { get; set; }

        /// <summary>
        /// Gets or sets backcolor used by the control
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// Gets or sets LinkColor used by the control
        /// </summary>
        public Color LinkColor { get; set; }

        /// <summary>
        /// Gets or sets ActiveLinkColor used by the control
        /// </summary>
        public Color ActiveLinkColor { get; set; }

        /// <summary>
        /// Gets or sets VisitedLinkColor used by the control
        /// </summary>
        public Color VisitedLinkColor { get; set; }

        /// <summary>
        /// Gets or sets LinkBehavior used by the control
        /// </summary>
        public LinkBehavior LinkBehavior { get; set; }

    }
}
