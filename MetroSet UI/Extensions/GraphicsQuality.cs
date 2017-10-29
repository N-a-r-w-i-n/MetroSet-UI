using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;

namespace MetroSet_UI.Extensions
{
    public class GraphicsQuality
    {

        /// <summary>
        /// sets The quality of the drawing to the given graphics.
        /// </summary>
        /// <param name="e">The Graphics to set the quality</param>
        /// <param name="smoothingMode">Gets or sets the rendering quality for this System.Drawing.Graphics.</param>
        /// <param name="pixelOffsetMode">Gets or set a value specifying how pixels are offset during rendering of this </param>
        /// <param name="interpolationMode">Gets or sets the interpolation mode associated with this System.Drawing.Graphics.</param>
        /// <param name="compositingQuality">Gets or sets the rendering quality of composited images drawn to this System.Drawing.Graphics.</param>
        public void SetQuality(Graphics e, 
            SmoothingMode smoothingMode = SmoothingMode.Default,
            TextRenderingHint textRenderingHint = TextRenderingHint.ClearTypeGridFit, 
            PixelOffsetMode pixelOffsetMode = PixelOffsetMode.Default,
            InterpolationMode interpolationMode = InterpolationMode.Default, 
            CompositingQuality compositingQuality = CompositingQuality.Default)
        {
            try
            {
                e.SmoothingMode = smoothingMode;
                e.PixelOffsetMode = pixelOffsetMode;
                e.InterpolationMode = interpolationMode;
                e.CompositingQuality = compositingQuality;
                e.TextRenderingHint = textRenderingHint;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

    }

}
