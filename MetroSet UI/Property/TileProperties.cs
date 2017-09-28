using System.Drawing;

namespace MetroSet_UI.Property
{
    internal class TileProperties
    {
        /// <summary>
        /// Gets or sets the Tile background color in normal mouse sate.
        /// </summary>
        public Color NormalColor { get; set; }

        /// <summary>
        /// Gets or sets the Tile border color in normal mouse sate.
        /// </summary>
        public Color NormalBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the Tile Text color in normal mouse sate.
        /// </summary>
        public Color NormalTextColor { get; set; }

        /// <summary>
        /// Gets or sets the Tile background color in hover mouse sate.
        /// </summary>
        public Color HoverColor { get; set; }

        /// <summary>
        /// Gets or sets the Tile border color in hover mouse sate.
        /// </summary>
        public Color HoverBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the button Text color in hover mouse sate.
        /// </summary>
        public Color HoverTextColor { get; set; }

        /// <summary>
        /// Gets or sets the Tile background color in pushed mouse sate.
        /// </summary>
        public Color PressColor { get; set; }

        /// <summary>
        /// Gets or sets the Tile border color in pushed mouse sate.
        /// </summary>
        public Color PressBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the Tile Text color in pushed mouse sate.
        /// </summary>
        public Color PressTextColor { get; set; }

        /// <summary>
        /// Gets or sets whether the control enabled.
        /// </summary>
        public bool Enabled { get; set; }
        
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