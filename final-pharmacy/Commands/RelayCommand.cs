using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace final_pharmacy.Commands
{
    class RelayCommand : ICommand
    {
        Action<object> executeAction;
        Func<object, bool> canExecute;

        public RelayCommand()
        {
        }

        public RelayCommand(Action<object> executeAction, Func<object, bool> canExecute)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            //if (canExecute == null)
            //{
            //return true;
            //}
            //else
            //{
            //return canExecute(parameter);
            //}
            return true;
        }

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}
