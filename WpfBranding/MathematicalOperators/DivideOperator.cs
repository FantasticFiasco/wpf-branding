namespace WpfBranding.MathematicalOperators
{
    public class DivideOperator : IMathematicalOperator
    {
        public double Calculate(double leftValue, double rightValue)
        {
            return leftValue / rightValue;
        }
    }
}