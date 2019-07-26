using System.Drawing;

namespace MetroSet_UI.Animates
{
    public class PointFAnimate : Animate<PointF>
    {
        public override PointF Value =>
            new PointF(
                (float)Interpolation.ValueAt(InitialValue.X, EndValue.X, Alpha, EasingType),
                (float)Interpolation.ValueAt(InitialValue.Y, EndValue.Y, Alpha, EasingType)
            );
    }
}
