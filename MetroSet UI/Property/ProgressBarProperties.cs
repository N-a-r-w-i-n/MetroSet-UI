using System.Drawing;

namespace MetroSet_UI.Property
{
    internal class ProgressBarProperties
    {
        /// <summary>
        /// Gets or sets the button background color color of the cotnrol.
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// Gets or sets the button border color color of the cotnrol.
        /// </summary>
        public Color BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the progress color of the cotnrol.
        /// </summary>
        public Color ProgressColor { get; set; }
        
        /// <summary>
        /// Gets or sets whether the control enabled.
        /// </summary>
        public bool Enabled { get; set; }
        
        /// <summary>
        /// Gets or sets backcolor used by the control while disabled.
        /// </summary>
        public Color DisabledBackColor { get; set; }

        /// <summary>
        /// Gets or sets the progresscolor of the control whenever while disabled.
        /// </summary>
        public Color DisabledProgressColor { get; set; }

        /// <summary>
        /// Gets or sets the border color of the control while disabled.
        /// </summary>
        public Color DisabledBorderColor { get; set; }


    }
}