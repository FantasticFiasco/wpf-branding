using System;
using System.Globalization;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WpfBranding.MathematicalOperators;

namespace WpfBranding
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ICommand numericInputCommand;
        private readonly ICommand numericSeparatorInputCommand;
        private readonly ICommand operatorCommand;
        private readonly ICommand calculateCommand;

        private string input;
        private string memory;
        private IMathematicalOperator mathematicalOperator;
        private bool clearOnNextNumericInput;

        public MainViewModel()
        {
            numericInputCommand = new RelayCommand<string>(ExecuteNumericInput);
            numericSeparatorInputCommand = new RelayCommand(ExecuteNumericSeparatorInput);
            operatorCommand = new RelayCommand<MathematicalOperatorType>(ExecuteOperator);
            calculateCommand = new RelayCommand(ExecuteCalculate);
            input = string.Empty;
        }

        public string Input
        {
            get { return input; }
            set
            {
                if (input == value)
                    return;

                RaisePropertyChanging();
                input = value;
                RaisePropertyChanged();
            }
        }

        public string NumberDecimalSeparator
        {
            get { return CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator; }
        }

        public ICommand NumericInputCommand
        {
            get { return numericInputCommand; }
        }

        public ICommand NumericSeparatorInputCommand
        {
            get { return numericSeparatorInputCommand; }
        }

        public ICommand OperatorCommand
        {
            get { return operatorCommand; }
        }

        public ICommand CalculateCommand
        {
            get { return calculateCommand; }
        }

        private void ExecuteNumericInput(string number)
        {
            ClearInputIfNeeded();

            Input += number;
        }

        private void ExecuteNumericSeparatorInput()
        {
            ClearInputIfNeeded();

            if (!Input.Contains(NumberDecimalSeparator))
            {
                Input += NumberDecimalSeparator;
            }
        }

        private void ExecuteOperator(MathematicalOperatorType mathematicalOperatorType)
        {
            if (!clearOnNextNumericInput)
            {
                memory = mathematicalOperator == null ?
                    Input :
                    Calculate();    
            }

            mathematicalOperator = CreateOperator(mathematicalOperatorType);
            clearOnNextNumericInput = true;
        }

        private void ExecuteCalculate()
        {
            Input = Calculate();
            mathematicalOperator = null;
        }

        private void ClearInputIfNeeded()
        {
            if (clearOnNextNumericInput)
            {
                Input = string.Empty;
                clearOnNextNumericInput = false;
            }
        }

        private string Calculate()
        {
            double parsedMemory = double.Parse(memory);
            double parsedInput = double.Parse(Input);
            double result = mathematicalOperator.Calculate(parsedMemory, parsedInput);

            return result.ToString(CultureInfo.CurrentCulture);
        }

        private IMathematicalOperator CreateOperator(MathematicalOperatorType mathematicalOperatorType)
        {
            switch (mathematicalOperatorType)
            {
                case MathematicalOperatorType.Add:
                    return new AddOperator();

                case MathematicalOperatorType.Subtract:
                    return new SubtractOperator();

                case MathematicalOperatorType.Multiply:
                    return new MultiplyOperator();

                case MathematicalOperatorType.Divide:
                    return new DivideOperator();

                default:
                    throw new ArgumentException("Unsupported mathematical operator type: " + mathematicalOperatorType, "mathematicalOperatorType");
            }
        }
    }
}