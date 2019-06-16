using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieClient.Other
{
    public class BaseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event Action ExecuteEvent;

        public BaseCommand(Action action)
        {
            ExecuteEvent += action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ExecuteEvent?.Invoke();
        }
    }
}
