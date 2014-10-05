namespace WpfBranding.MathematicalOperators
{
    public class SubtractOperator : IMathematicalOperator
    {
        public double Calculate(double leftValue, double rightValue)
        {
            return leftValue - rightValue;
        }
    }
}