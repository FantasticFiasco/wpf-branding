using System;
using System.Globalization;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WpfBranding.Operators;

namespace WpfBranding
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ICommand numericInputCommand;
        private readonly ICommand numericSeparatorInputCommand;
        private readonly ICommand operatorCommand;

        private string input;
        private string leftValue;

        public MainViewModel()
        {
            numericInputCommand = new RelayCommand<string>(ExecuteNumericInput);
            numericSeparatorInputCommand = new RelayCommand(ExecuteNumericSeparatorInput);
            operatorCommand = new RelayCommand<Operator>(ExecuteOperator);
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

        private void ExecuteNumericInput(string number)
        {
            Input += number;
        }

        private void ExecuteNumericSeparatorInput()
        {
            if (!Input.Contains(NumberDecimalSeparator))
            {
                Input += NumberDecimalSeparator;
            }
        }

        private void ExecuteOperator(OperatorType operatorType)
        {
            if (leftValue == null)
            {
                leftValue = input;
                input = string.Empty;
                return;
            }
            
            IOperator @operator;

            switch (operatorType)
            {
                case OperatorType.Add:
                    @operator = new AddOperator();
                    break;

                case OperatorType.Subtract:
                    @operator = new SubtractOperator();
                    break;

                case OperatorType.Multiply:
                    @operator = new MultiplyOperator();
                    break;

                case OperatorType.Divide:
                    @operator = new DivideOperator();
                    break;

                default:
                    throw new ArgumentException("Unsupported operator type: " + operatorType, "operatorType");
            }


        }
    }
}