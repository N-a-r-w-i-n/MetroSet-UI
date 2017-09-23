using MetroSet_UI.Enums;
using System.Drawing;

namespace MetroSet_UI.Property
{
    internal class DividerProperties
    {
        /// <summary>
        /// Gets or sets Orientation of the control.
        /// </summary>
        public DividerStyle Orientation { get; set; }

        /// <summary>
        /// Gets or sets forecolor used by the control
        /// </summary>
        public Color ForeColor { get; set; }

        /// <summary>
        /// Gets or sets backcolor used by the control
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// Gets or sets the divider thickness.
        /// </summary>
        public int Thickness { get; set; }

    }
}