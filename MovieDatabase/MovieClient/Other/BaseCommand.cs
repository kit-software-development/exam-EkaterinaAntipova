using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

//Для взаимодействия пользователя и приложения в MVVM используются команды. В WPF они представлены интерфейсом ICommand.
//WPF в качестве реализации этого интерфейса имеет класс System.Windows.Input.RoutedCommand, но т.к. он ограничен по функциональности, были реализованы свои собственные команды с помощью ICommand.

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

