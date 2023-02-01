using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using JBOT.Application.Dtos;
using JBOT.Domain.Entities;
using JBOT.Domain.Entities.Enums;
using JBOT.WPF.DtosWithCommand;
using JBOT.WPF.Models;
using JBOT.WPF.Services;
using JBOT.WPF.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using Wpf.Ui.Common;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Services;
using IRelayCommand = CommunityToolkit.Mvvm.Input.IRelayCommand;
using RelayCommand = CommunityToolkit.Mvvm.Input.RelayCommand;

namespace JBOT.WPF.ViewModels
{
    public partial class UnitTestViewModel : BaseViewModel, INavigationAware
    {
        private readonly IMapper _mapper;

        public ObservableCollection<UnitTestDtoWithCommand> UnitTests { get; set; } = new ObservableCollection<UnitTestDtoWithCommand>();

        public UnitTestViewModel(IApiService apiService, IMapper mapper):base(apiService)
        {
            _mapper = mapper;
            LoadGridCommand = new AsyncRelayCommand<object>(LoadGrid, CanExecuteLoadGridCommand);
            RunTestCommand = new AsyncRelayCommand(RunTest, CanRunTest);
        }

        public void OnNavigatedFrom()
        {
        }

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        public IRelayCommand ShowAddUnitTestDialogCommand => new RelayCommand(ShowAddUnitTestDialog);
        
        public async Task LoadGrid(object? obj)
        {
            var currentConnections = obj as ICurrentConnections;
            UnitTests.Clear();
            var unitTest = await _apiService.GetUnitTests(currentConnections?.Server ?? string.Empty, currentConnections?.DatabaseName ?? string.Empty);
            UnitTests.AddRange(_mapper.Map<List<UnitTestDtoWithCommand>>(unitTest));
            Parallel.ForEach(UnitTests, item =>
            {
                item.EditCommand = new RelayCommand<object>(ShowEditUnitTestDialog, CanEdit);
                item.RemoveCommand = new AsyncRelayCommand<object>(Remove, CanRemove);
            });
        }

        public void ShowEditUnitTestDialog(object? obj)
        {
            var itemToEdit = obj as UnitTestDtoWithCommand;
            var editUnitTestDialogWindow = new EditUnitTestDialogWindow(_apiService,CurrentConnections, itemToEdit.Id);
            if (editUnitTestDialogWindow.ShowDialog() ?? false)
            {
                LoadGridCommand.Execute(CurrentConnections);
            }
        }

        public bool CanEdit(object? obj)
        {
            var itemToEdit = obj as UnitTestDtoWithCommand;
            if (itemToEdit != null)
            {
                return !itemToEdit.RemoveCommand.IsRunning;
            }
            else
                return true;
        }

        public void ShowAddUnitTestDialog()
        {
            var addUnitTestDialogWindow = new AddUnitTestDialogWindow(_apiService, CurrentConnections);
            if(addUnitTestDialogWindow.ShowDialog()?? false)
            {
                LoadGridCommand.Execute(CurrentConnections);
            }
        }
        public bool CanRemove(object? obj)
        {
            var itemToDelete = obj as UnitTestDtoWithCommand;
            if (itemToDelete?.RemoveCommand?.IsRunning ?? false)
                return false;
            
            return true;
        }
        public async Task Remove(object? obj)
        {
            var itemToDelete = obj as UnitTestDtoWithCommand;
            await _apiService.RemoveUnitTest(itemToDelete.Id);
            UnitTests.Remove(itemToDelete);
        }

        protected override bool CanExecuteLoadGridCommand(object? obj)
        {
            var currentConnections = obj as ICurrentConnections;
            if (LoadGridCommand.IsRunning)
                return false;
            if (currentConnections?.DatabaseId == null ||
                currentConnections?.Server == null ||
                currentConnections?.DatabaseName == null)
            {
                return false;
            }
            return true;
        }

        public IAsyncRelayCommand RunTestCommand { get; set; }
        public async Task RunTest()
        {
            var result = await _apiService.RunTest(
                CurrentConnections?.Server ?? string.Empty, CurrentConnections?.DatabaseName ?? string.Empty);

            if (result != null) 
            {
                foreach (var r in result)
                {
                    var dtoWithCommand = _mapper.Map<UnitTestDtoWithCommand>(r);
                    dtoWithCommand.EditCommand = new RelayCommand<object>(ShowEditUnitTestDialog, CanEdit);
                    dtoWithCommand.RemoveCommand = new AsyncRelayCommand<object>(Remove, CanRemove);

                    var currentItem = UnitTests.FirstOrDefault(u => u.Id == dtoWithCommand.Id);
                    if (currentItem == null) 
                    {
                        UnitTests.Add(dtoWithCommand);
                    }
                    else
                    {
                        UnitTests.Replace(currentItem, dtoWithCommand);
                    }
                }
            }
        }

        public bool CanRunTest()
        {
            if (RunTestCommand.IsRunning)
                return false;
            if (CurrentConnections?.DatabaseId == null ||
                CurrentConnections?.Server == null ||
                CurrentConnections?.DatabaseName == null)
            {
                return false;
            }

            return true;
        }

        protected override void OnDatabaseChange()
        {
            base.OnDatabaseChange();
            LoadGridCommand.NotifyCanExecuteChanged();
            RunTestCommand.NotifyCanExecuteChanged();
        }
    }
}
