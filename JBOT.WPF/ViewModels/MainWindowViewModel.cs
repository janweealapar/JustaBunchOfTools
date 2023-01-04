using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JBOT.Application.Dtos;
using JBOT.WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace JBOT.WPF.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly IApiService _apiService;
        private bool _isInitialized = false;

        [ObservableProperty]
        private string _applicationTitle = String.Empty;

        [ObservableProperty]
        private ObservableCollection<INavigationControl> _navigationItems = new();

        [ObservableProperty]
        private ObservableCollection<INavigationControl> _navigationFooter = new();

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new();

        [ObservableProperty]
        private List<DatabaseDto> _databases = new();

        [ObservableProperty]
        private DatabaseDto _selectedDatabase = new();

        //public List<DatabaseDto> Databases
        //{
        //    get => _databases;
        //    set => SetProperty(ref _databases, value);
        //}

        public MainWindowViewModel(INavigationService navigationService, IApiService apiService)
        {
            if (!_isInitialized)
            {
                _apiService = apiService;
                InitializeViewModel();

                GetDatabaseCommand.Execute(null);
            }
                
        }

        public IAsyncRelayCommand GetDatabaseCommand => new AsyncRelayCommand(GetDatabases);

        public async Task GetDatabases()
        {
            Databases = await _apiService.GetDatabaseDtos();
        }
        private void InitializeViewModel()
        {
            ApplicationTitle = "Toolbox";

            NavigationItems = new ObservableCollection<INavigationControl>
            {
                new NavigationItem()
                {
                    Content = "Dashboard",
                    PageTag = "dashboard",
                    Icon = SymbolRegular.Home24,
                    PageType = typeof(Views.Pages.DashboardPage)
                },
                new NavigationItem()
                {
                    Content = "Data",
                    PageTag = "data",
                    Icon = SymbolRegular.DataHistogram24,
                    PageType = typeof(Views.Pages.DataPage)
                }
            };

            NavigationFooter = new ObservableCollection<INavigationControl>
            {
                new NavigationItem()
                {
                    Content = "Settings",
                    PageTag = "settings",
                    Icon = SymbolRegular.Settings24,
                    PageType = typeof(Views.Pages.SettingsPage)
                }
            };

            TrayMenuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem
                {
                    Header = "Home",
                    Tag = "tray_home"
                }
            };

            _isInitialized = true;
        }
    }
}
