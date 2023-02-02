using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using JBOT.Application.Dtos;
using JBOT.WPF.Models;
using JBOT.WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JBOT.WPF.ViewModels
{
    public abstract class BaseViewModel : ObservableObject
    {
        public readonly IApiService _apiService;
        public bool _isInitialized = false;
        public BaseViewModel(IApiService apiService)
        {
            _apiService = apiService;
        }
        
        public ObservableCollection<string> Servers { get; set; } = new();
        public ObservableCollection<DatabaseDto> Databases { get; set; } = new();

        private string _currentServer;
        public string CurrentServer
        {
            get => _currentServer;
            set 
            {
                if(SetProperty(ref _currentServer, value))
                {
                    OnPropertyChanged(nameof(CurrentConnections));
                    GetDatabaseCommand.Execute(value);
                }
            } 
        }

        public DatabaseDto _currentDatabase;
        public DatabaseDto CurrentDatabase
        {
            get => _currentDatabase;
            set
            {
                if (SetProperty(ref _currentDatabase, value))
                {
                    OnDatabaseChange();
                }
            }
        }

        public ICurrentConnections CurrentConnections => new CurrentConnections(CurrentServer, CurrentDatabase?.Id, CurrentDatabase?.Name);
        protected virtual void InitializeViewModel()
        {
            _isInitialized = true;
            GetServersCommand.Execute(null);
        }

        public ICommand GetServersCommand => new AsyncRelayCommand(GetServers);
        public ICommand GetDatabaseCommand => new AsyncRelayCommand<string>(GetDatabases);

        public IAsyncRelayCommand LoadGridCommand { get; set; }

        protected abstract bool CanExecuteLoadGridCommand(object? obj);

        public async Task GetServers()
        {
            Servers.Clear();
            Servers.AddRange(await _apiService.GetServers());

            CurrentDatabase = new DatabaseDto();
        }

        public async Task GetDatabases(string server)
        {
            Databases.Clear();
            Databases.Add(await _apiService.GetDatabaseDtos(server));
            int start = Databases.Max(d => d.Id);
            for (int i = 0; i < 100; i++)
            {
                Databases.Add(new DatabaseDto { Id = start + i, Name = $"Database {start + i}" });
            }
        }

        protected virtual void OnDatabaseChange()
        {
            OnPropertyChanged(nameof(CurrentConnections));
        }
    }
}
