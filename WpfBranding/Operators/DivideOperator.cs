namespace WpfBranding.Operators
{
    public class DivideOperator : IOperator
    {
        public double Calculate(double leftValue, double rightValue)
        {
            return leftValue / rightValue;
        }
    }
}