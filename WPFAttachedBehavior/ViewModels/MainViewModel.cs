using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WPFAttachedBehavior.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            MyCommand = new RelayCommand<object>(OnMyCommand);
        }

        public ICommand MyCommand { get; }

        private void OnMyCommand(object parameter)
        {
            MessageBox.Show($"命令执行，参数：{parameter}");
        }

        [ObservableProperty] private int _id = 0;
        [RelayCommand]
        private void My1()
        {
            this.Id++;
        }
    }
}

