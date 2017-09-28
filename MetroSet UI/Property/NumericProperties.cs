using System.Drawing;

namespace MetroSet_UI.Property
{
    internal class NumericProperties
    {
        /// <summary>
        /// Gets or sets whether the control enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets forecolor used by the control
        /// </summary>
        public Color ForeColor { get; set; }

        /// <summary>
        /// Gets or sets backcolor used by the control
        /// </summary>
        public Color BackColor { get; set; }
        
        /// <summary>
        /// Gets or sets border color used by the control
        /// </summary>
        public Color BorderColor { get; set; }

        /// <summary>
        /// Gets or sets arrow color used by the control
        /// </summary>
        public Color SymbolsColor { get; set; }

        /// <summary>
        /// Gets or sets backcolor used by the control while disabled.
        /// </summary>
        public Color DisabledBackColor { get; set; }

        /// <summary>
        /// Gets or sets the forecolor of the control whenever while disabled.
        /// </summary>
        public Color DisabledForeColor { get; set; }

        /// <summary>
        /// Gets or sets the border color of the control while disabled.
        /// </summary>
        public Color DisabledBorderColor { get; set; }

    }
}