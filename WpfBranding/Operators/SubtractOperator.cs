namespace WpfBranding.Operators
{
    public class SubtractOperator : IOperator
    {
        public double Calculate(double leftValue, double rightValue)
        {
            return leftValue - rightValue;
        }
    }
}