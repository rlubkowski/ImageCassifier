using System;
using System.Windows.Input;

namespace FruitClassifier.Core
{
    public class Command : ICommand
    {
        private Func<object, bool> canExecuteWithParamDelegate;
        private Action<object> executeWithParamDelegate;

        private Func<bool> canExecuteDelegate;
        private Action executeDelegate;

        public Command(Func<bool> canExecute = null, Action execute = null)
        {
            canExecuteDelegate = canExecute;
            executeDelegate = execute;
        }

        public Command(Func<object, bool> canExecute = null, Action<object> execute = null)
        {
            canExecuteWithParamDelegate = canExecute;
            executeWithParamDelegate = execute;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecuteDelegate != null)
                return canExecuteDelegate();
            if (canExecuteWithParamDelegate != null)
                return canExecuteWithParamDelegate(parameter);
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            executeDelegate?.Invoke();
            executeWithParamDelegate?.Invoke(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
