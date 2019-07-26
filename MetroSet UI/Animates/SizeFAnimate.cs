using System.Drawing;

namespace MetroSet_UI.Animates
{
    public class SizeFAnimate : Animate<SizeF>
    {
        public override SizeF Value =>
            new SizeF(
                (float)Interpolation.ValueAt(InitialValue.Width, EndValue.Width, Alpha, EasingType),
                (float)Interpolation.ValueAt(InitialValue.Height, EndValue.Height, Alpha, EasingType)
            );
    }
}
