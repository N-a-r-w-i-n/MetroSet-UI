namespace MetroSet_UI.Animates
{
    public class IntAnimate : Animate<int>
    {
        public override int Value => 
            (int)Interpolation.ValueAt(InitialValue, EndValue, Alpha, EasingType);
    }
}
