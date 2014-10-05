namespace WpfBranding.Operators
{
    public class MultiplyOperator : IOperator
    {
        public double Calculate(double leftValue, double rightValue)
        {
            return leftValue * rightValue;
        }
    }
}