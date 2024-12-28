using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFAttachedBehavior.ViewModels
{
    public partial class EnterViewModel : ObservableObject
    {
        [ObservableProperty] private int _id = 0;
        [RelayCommand]
        private void My()
        {
            this.Id++;
        }
    }
}
