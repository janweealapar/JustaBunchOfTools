using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JBOT.Application.Dtos;
using System.Collections.Generic;
using System.Windows.Documents;
using Wpf.Ui.Common.Interfaces;

namespace JBOT.WPF.ViewModels
{
    public partial class DashboardViewModel : ObservableObject, INavigationAware
    {
        [ObservableProperty]
        private int _counter = 0;

        private List<DatabaseDto> _databases = new List<DatabaseDto>();

        public List<DatabaseDto> Databases 
        {
            get => _databases;
            set => SetProperty(ref _databases, value);
        }

        public void OnNavigatedTo()
        {
            Databases = new List<DatabaseDto>
            {
                new DatabaseDto { Id = 1, Name = "Blossoms" },
                new DatabaseDto { Id = 2, Name = "Bloodmoss" },
                new DatabaseDto { Id = 3, Name = "Blowbill" },
                new DatabaseDto { Id = 4, Name = "Bryonia" },
                new DatabaseDto { Id = 5, Name = "Buckthorn" }
            };
        }

        public void OnNavigatedFrom()
        {
        }

        [RelayCommand]
        private void OnCounterIncrement()
        {
            Counter++;
        }
    }
}
