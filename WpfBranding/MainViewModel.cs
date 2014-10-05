using System.Globalization;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace WpfBranding
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ICommand numericInputCommand;
        private readonly ICommand numericSeparatorInputCommand;

        private string input;

        public MainViewModel()
        {
            numericInputCommand = new RelayCommand<string>(ExecuteNumericInput);
            numericSeparatorInputCommand = new RelayCommand(ExecuteNumericSeparatorInput);
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
    }
}