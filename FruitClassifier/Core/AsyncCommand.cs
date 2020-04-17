using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FruitClassifier.Core
{
    public class AsyncCommand : ICommand
    {
        private Func<bool> canExecuteDelegate;
        private Func<Task> executeDelegate;

        private Func<object, bool> canExecuteWithParamDelegate;
        private Func<object, Task> executeWithParamDelegate;

        public AsyncCommand(Func<object, bool> canExecute = null, Func<object, Task> execute = null)
        {
            canExecuteWithParamDelegate = canExecute;
            executeWithParamDelegate = execute;
        }

        public AsyncCommand(Func<bool> canExecute = null, Func<Task> execute = null)
        {
            canExecuteDelegate = canExecute;
            executeDelegate = execute;
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
