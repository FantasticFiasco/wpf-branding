namespace WpfBranding.MathematicalOperators
{
    public class MultiplyOperator : IMathematicalOperator
    {
        public double Calculate(double leftValue, double rightValue)
        {
            return leftValue * rightValue;
        }
    }
}