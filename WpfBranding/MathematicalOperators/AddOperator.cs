namespace WpfBranding.MathematicalOperators
{
    public class AddOperator : IMathematicalOperator
    {
        public double Calculate(double leftValue, double rightValue)
        {
            return leftValue + rightValue;
        }
    }
}