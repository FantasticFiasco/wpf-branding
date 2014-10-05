namespace WpfBranding.Operators
{
    public class AddOperator : IOperator
    {
        public double Calculate(double leftValue, double rightValue)
        {
            return leftValue + rightValue;
        }
    }
}