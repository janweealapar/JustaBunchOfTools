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
    public partial class AddUnitTestDialogViewModel : BaseDialogViewModel
    {
        
        private bool _isInitialized = false;
        public bool _isReload = false;
        private readonly IApiService _apiService;
        private readonly int _currentDatabaseId;
        private readonly ICurrentConnections _currentConnections;

        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        private string _testName = string.Empty;

        [ObservableProperty]
        private string _testDescription = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SetSelectedTestableObjectCommand))]
        private ObservableCollection<TestableObjectDto> _testableObjects = new();


        private IEnumerable<TestableObjectDto> TestObjectsUnfiltered = new TestableObjectDto[] { };

        [ObservableProperty]
        private IEnumerable<string> _searchByObjectNameOptions;

        [ObservableProperty]
        public IEnumerable<string> _searchByTypeOptions;

        private string _searchByType = string.Empty;
        public string SearchByType 
        {
            get => _searchByType;
            set
            {
                if (SetProperty(ref _searchByType, value))
                {
                    FilterGridCommand.Execute(null);
                }
            }
        }

        private string _searchByName = string.Empty;
        public string SearchByName 
        {
            get => _searchByName;
            set
            {
                if (SetProperty(ref _searchByName, value))
                {
                    FilterGridCommand.Execute(null);
                }
            }
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CurrentObjectTitle))]
        [NotifyPropertyChangedFor(nameof(HasSelectedObject))]
        private TestableObjectDto _selectedTestableObject = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(QuickRunCommand))]
        [NotifyCanExecuteChangedFor(nameof(AddCommand))]
        private TestableObjectDetailsDto _testableObjectDetails = new();

        [ObservableProperty]
        private IEnumerable<Operator> _operators;

        public string CurrentObjectTitle => $"{SelectedTestableObject.Type} - {SelectedTestableObject.Name}";
        public bool HasSelectedObject => string.IsNullOrEmpty(SelectedTestableObject.Name);

        public AddUnitTestDialogViewModel(IApiService apiService, ICurrentConnections currentConnections, UiWindow ownerPage)
            :base(ownerPage)
        {
            if (!_isInitialized)
            {
                Title = "Add Unit Test";
                _currentDatabaseId = currentConnections.DatabaseId.Value;
                _currentConnections = currentConnections;
                _apiService = apiService;
                InitializeViewModel();

                SetSelectedTestableObjectCommand = new AsyncRelayCommand<int>(SetSelectedTestableObject, CanExecuteSetSelectedTestableObjectCommand);
                QuickRunCommand = new AsyncRelayCommand(QuickRun, CanExecuteQuickRun);
                AddCommand = new AsyncRelayCommand(Add, CanAdd);
                FilterGridCommand = new RelayCommand(FilterDataGrid);
            }
        }

        public IAsyncRelayCommand GetTestableObjectsCommand => new AsyncRelayCommand(GetTestableObjects);

        public IAsyncRelayCommand SetSelectedTestableObjectCommand { get; set; }

        public IAsyncRelayCommand QuickRunCommand { get; set; }
        public IAsyncRelayCommand AddCommand { get; set; }
        public IRelayCommand ClearCommand  => new RelayCommand(Clear);

        public IRelayCommand FilterGridCommand { get; set; }

        public void FilterDataGrid()
        {
            TestableObjects.Clear();
            var filterBy = new { Name = SearchByName, Type = SearchByType };
            var filtered = TestObjectsUnfiltered.Where(w => (w.Name.Contains(filterBy.Name) || string.IsNullOrEmpty(filterBy.Name))
                                      && (w.Type == filterBy.Type || string.IsNullOrEmpty(filterBy.Type) || filterBy.Type == "All"));

            if (filtered!=  null)
            {
                TestableObjects.AddRange(filtered);
            }
        }


        public async Task GetTestableObjects()
        {
            TestableObjects.Clear();
            SearchByObjectNameOptions = new Collection<string>();
            SearchByTypeOptions = new Collection<string>();

            TestObjectsUnfiltered = await _apiService.TestableObjectDtos(_currentConnections.Server,_currentDatabaseId);
            if (TestObjectsUnfiltered != null)
            {
                TestableObjects.AddRange(TestObjectsUnfiltered);
                SearchByObjectNameOptions = TestObjectsUnfiltered.Select(s => s.Name);

                var typeOptions = new List<string>();
                typeOptions.Add("All");
                typeOptions.AddRange(TestObjectsUnfiltered.Select(s => s.Type).Distinct());
                SearchByTypeOptions = typeOptions;
            }
        }
        private bool CanExecuteSetSelectedTestableObjectCommand(int obj)
        {
            return obj > 0;
        }
        public async Task SetSelectedTestableObject(int objectId)
        {
            SelectedTestableObject = TestableObjects.FirstOrDefault(to=> to.ObjectId == objectId) ?? new();
            TestableObjectDetails = await _apiService.GetTestableObjectDetails(_currentConnections.Server,_currentDatabaseId, objectId);
        }

        public async Task QuickRun()
        {
            if (TestableObjectDetails.OutputParameters.Where(p => string.IsNullOrEmpty(p.Expected) || p.Operator == null).Any())
            {
                return;
            }
            TestableObjectDetails.TestName = TestName; 
            TestableObjectDetails.Description= TestDescription;
            TestableObjectDetails.DatabaseId = _currentDatabaseId;
            TestableObjectDetails.DatabaseName = _currentConnections.DatabaseName;
            TestableObjectDetails.Server = _currentConnections.Server;
            TestableObjectDetails = await _apiService.QuickRun(TestableObjectDetails, _currentConnections?.Server?? string.Empty);
        }

        public bool CanExecuteQuickRun()
        {
            if (TestableObjectDetails.OutputParameters == null)
            {
                return false;
            }
            return true;
        }

        public async Task Add()
        {
            TestableObjectDetails = await _apiService.Add(TestableObjectDetails);
            if (TestableObjectDetails.Status == StatusEnums.Success)
            {
                Clear();
                _isReload = true;
            }
        }

        public bool CanAdd()
        {
            return (TestableObjectDetails.Status ?? StatusEnums.Failed) == StatusEnums.Success;
        }


        private void InitializeViewModel()
        {
            Operators = EnumHelper.EnumToModel<Operator, OperatorEnums>();
            GetTestableObjectsCommand.Execute(null);
            _isInitialized = true;
        }

        protected override void Clear()
        {
            TestableObjectDetails = new();
            TestName = string.Empty;
            TestDescription = string.Empty;
        }
    }
}
