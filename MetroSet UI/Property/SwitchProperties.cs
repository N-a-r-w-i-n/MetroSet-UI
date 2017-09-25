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