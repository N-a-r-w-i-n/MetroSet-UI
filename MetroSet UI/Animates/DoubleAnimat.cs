namespace MetroSet_UI.Animates
{
    public class DoubleAnimate : Animate<double>
    {
        public override double Value =>
            Interpolation.ValueAt(InitialValue, EndValue, Alpha, EasingType);
    }
}
