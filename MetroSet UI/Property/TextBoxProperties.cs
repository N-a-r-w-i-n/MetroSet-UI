using System.Drawing;

namespace MetroSet_UI.Property
{
    internal class TextBoxProperties
    {
        
        /// <summary>
        /// Gets or sets whether the control enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets forecolor used by the control.
        /// </summary>
        public Color ForeColor { get; set; }

        /// <summary>
        /// Gets or sets backcolor used by the control.
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the control whenever hovered.
        /// </summary>
        public Color HoverColor { get; set; }

        /// <summary>
        /// Gets or sets the border color of the control.
        /// </summary>
        public Color BorderColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether text in the text box is read-only.
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the text in the TextBox control should appear as the default password character.
        /// </summary>
        public bool UseSystemPasswordChar { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is a multiline TextBox control.
        /// </summary>
        public bool Multiline { get; set; }

        /// <summary>
        /// Gets or sets the text in the TextBox while being empty.
        /// </summary>
        public string WatermarkText { get; set; }

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