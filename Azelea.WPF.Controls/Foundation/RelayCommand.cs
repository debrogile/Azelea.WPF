using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Azelea.WPF.Controls
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _Execute;
        private readonly Predicate<object> _CanExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _Execute = execute;
            _CanExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _CanExecute == null || _CanExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _Execute(parameter);
        }
    }
}
