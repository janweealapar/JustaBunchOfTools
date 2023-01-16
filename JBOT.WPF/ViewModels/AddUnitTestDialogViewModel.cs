using CommunityToolkit.Mvvm.ComponentModel;
using JBOT.WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Common;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Controls;
using JBOT.Application.Dtos;
using CommunityToolkit.Mvvm.Input;
using JBOT.WPF.Models;
using RelayCommand = CommunityToolkit.Mvvm.Input.RelayCommand;
using IRelayCommand = CommunityToolkit.Mvvm.Input.IRelayCommand;
using System.Windows.Input;
using DynamicData;
using JBOT.Domain.Entities;
using JBOT.Application.Helpers;
using JBOT.Domain.Entities.Enums;

namespace JBOT.WPF.ViewModels
{
    public partial class AddUnitTestDialogViewModel : ObservableObject
    {
        private bool _isInitialized = false;
        private readonly IApiService _apiService;
        private readonly int _currentDatabaseId;
        private readonly ICurrentConnections _currentConnections;

        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SetSelectedTestableObjectCommand))]
        private ObservableCollection<TestableObjectDto> _testableObjects = new();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CurrentObjectTitle))]
        [NotifyPropertyChangedFor(nameof(HasSelectedObject))]
        private TestableObjectDto _selectedTestableObject = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(QuickRunCommand))]
        private TestableObjectDetailsDto _testableObjectDetails = new();

        [ObservableProperty]
        private IEnumerable<Operator> _operators;

        public string CurrentObjectTitle => $"{SelectedTestableObject.Type} - {SelectedTestableObject.Name}";
        public bool HasSelectedObject => string.IsNullOrEmpty(SelectedTestableObject.Name);

        public AddUnitTestDialogViewModel(IApiService apiService, ICurrentConnections currentConnections)
        {
            Title = "Add Unit Test";
            if (!_isInitialized)
            {
                _currentDatabaseId = currentConnections.DatabaseId;
                _currentConnections = currentConnections;
                _apiService = apiService;
                InitializeViewModel();

                SetSelectedTestableObjectCommand = new AsyncRelayCommand<int>(SetSelectedTestableObject, CanExecuteSetSelectedTestableObjectCommand);
                QuickRunCommand = new AsyncRelayCommand(QuickRun, CanExecuteQuickRun);
            }
        }

        public IAsyncRelayCommand GetTestableObjectsCommand => new AsyncRelayCommand(GetTestableObjects);

        public IAsyncRelayCommand SetSelectedTestableObjectCommand { get; set; }

        public IAsyncRelayCommand QuickRunCommand { get; set; }

        public async Task GetTestableObjects()
        {
            TestableObjects.Clear();
            var testableObjectDtos = await _apiService.TestableObjectDtos(_currentDatabaseId);
            TestableObjects.AddRange(testableObjectDtos);
        }
        private bool CanExecuteSetSelectedTestableObjectCommand(int obj)
        {
            return obj > 0;
        }
        public async Task SetSelectedTestableObject(int objectId)
        {
            SelectedTestableObject = TestableObjects.FirstOrDefault(to=> to.ObjectId == objectId) ?? new();
            TestableObjectDetails = await _apiService.GetTestableObjectDetails(_currentDatabaseId, objectId);
        }

        public async Task QuickRun()
        {
            if (TestableObjectDetails.OutputParameters.Where(p => string.IsNullOrEmpty(p.Expected) || p.Operator == null).Any())
            {
                return;
            }

            TestableObjectDetails.DatabaseId = _currentDatabaseId;
            TestableObjectDetails.DatabaseName = _currentConnections.DatabaseName;

            TestableObjectDetails = await _apiService.QuickRun(TestableObjectDetails);
        }

        public bool CanExecuteQuickRun()
        {
            if (TestableObjectDetails.OutputParameters == null)
            {
                return false;
            }
            return true;
        }

        private void InitializeViewModel()
        {
            Operators = EnumHelper.EnumToModel<Operator, OperatorEnums>();
            GetTestableObjectsCommand.Execute(null);
            _isInitialized = true;
        }
    }
}
