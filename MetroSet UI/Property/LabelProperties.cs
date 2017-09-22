using System.Drawing;

namespace MetroSet_UI.Property
{
    internal class LabelProperties
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
    }
}