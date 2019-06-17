using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

// Реализация паттерна MVVM (Model-View-ViewModel) (позволяет отделить логику приложения от визуальной части)

namespace MovieClient.Other
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Page Page { get; set; }
        public Window Window { get; set; }
    }
}
