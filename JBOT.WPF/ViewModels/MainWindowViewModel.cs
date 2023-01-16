using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JBOT.Application.Dtos;
using JBOT.WPF.Models;
using JBOT.WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using IRelayCommand = CommunityToolkit.Mvvm.Input.IRelayCommand;
using RelayCommand = CommunityToolkit.Mvvm.Input.RelayCommand;

namespace JBOT.WPF.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly IApiService _apiService;
        private readonly ICurrentConnections _currentConnections;
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

        //[ObservableProperty]
        //[NotifyCanExecuteChangedFor(nameof(SetCurrentDatabaseCommand))]
        private DatabaseDto _selectedDatabase = new();
        public DatabaseDto SelectedDatabase // because combo box doesn't directly supports command
        {
            get => _selectedDatabase;
            set 
            {
                if (SetProperty(ref _selectedDatabase, value))
                {
                    SetCurrentDatabaseCommand.Execute(null);
                }
            }
        }

        public MainWindowViewModel(INavigationService navigationService, IApiService apiService, ICurrentConnections currentConnections)
        {
            if (!_isInitialized)
            {
                _apiService = apiService;
                _currentConnections = currentConnections;

                InitializeViewModel();

                GetDatabaseCommand.Execute(null);
            }
        }

        public IAsyncRelayCommand GetDatabaseCommand => new AsyncRelayCommand(GetDatabases);
        public IRelayCommand SetCurrentDatabaseCommand => new RelayCommand(SetCurrentDatabase);

        public void SetCurrentDatabase()
        {
            _currentConnections.DatabaseId = SelectedDatabase.Id;
            _currentConnections.DatabaseName = SelectedDatabase.Name;
        }
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
                },
                new NavigationItem()
                {
                    Content = "Unit Tests",
                    PageTag = "unit tests",
                    Icon = SymbolRegular.Beaker32,
                    PageType = typeof(Views.Pages.UnitTestPage)
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
