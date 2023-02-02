using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JBOT.Application.Dtos;
using JBOT.Application.Helpers;
using JBOT.Domain.Entities;
using JBOT.Domain.Entities.Enums;
using JBOT.WPF.Models;
using JBOT.WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace JBOT.WPF.ViewModels
{
    public partial class EditUnitTestDialogViewModel : BaseDialogViewModel
    {
        public bool _isInitialized = false;
        public bool _isReload = false;
        private readonly IApiService _apiService;
        private readonly int _currentDatabaseId;
        private readonly ICurrentConnections _currentConnections;

        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        private IEnumerable<Operator> _operators;

        [ObservableProperty]
        private TestableObjectDto _selectedTestableObject = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(QuickRunCommand))]
        //[NotifyCanExecuteChangedFor(nameof(AddCommand))]
        private TestableObjectDetailsDto _testableObjectDetails = new();

        public EditUnitTestDialogViewModel(IApiService apiService, ICurrentConnections currentConnections, int unitTestId, UiWindow ownerPage)
            : base(ownerPage)
        {
            if (!_isInitialized)
            {
                Title = "Edit Unit Test";
                _currentDatabaseId = currentConnections?.DatabaseId ?? 0;
                _currentConnections = currentConnections;
                _apiService = apiService;
                InitializeViewModel();
                SetSelectedTestableObjectCommand.Execute(unitTestId);
            }
        }

        public void InitializeViewModel()
        {
            this.Operators = EnumHelper.EnumToModel<Operator, OperatorEnums>();
            SetSelectedTestableObjectCommand = new AsyncRelayCommand<int>(SetSelectedTestableObject);
            QuickRunCommand = new AsyncRelayCommand(QuickRun, CanExecuteQuickRun);
            UpdateCommand = new AsyncRelayCommand(Update);
            _isInitialized = true;
        }

        public IAsyncRelayCommand SetSelectedTestableObjectCommand { get; set; }
        public async Task SetSelectedTestableObject(int unitTestId)
        {
            TestableObjectDetails = await _apiService.GetUnitTestById(unitTestId);
        }
        public IAsyncRelayCommand QuickRunCommand { get; set; }
        public async Task QuickRun()
        {
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

        public IAsyncRelayCommand UpdateCommand { get; set; }
        public async Task Update()
        {
            TestableObjectDetails = await _apiService.Update(TestableObjectDetails);
            _isReload = TestableObjectDetails.Status == StatusEnums.Success;
        }

        public bool CanUpdate()
        {
            if (UpdateCommand.IsRunning)
                return false;
            if ((TestableObjectDetails.Status ?? StatusEnums.Failed) == StatusEnums.Failed)
                return false;

            return true;
            
        }

        protected override void Clear()
        {
            SetSelectedTestableObjectCommand.Execute(TestableObjectDetails.Id);
        }
    }
}
