using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JBOT.WPF.Models;
using JBOT.WPF.Services;
using JBOT.WPF.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using Wpf.Ui.Common.Interfaces;

namespace JBOT.WPF.ViewModels
{
    public class UnitTestViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly ICurrentConnections _currentConnections;
        public UnitTestViewModel(IApiService apiService, IMapper mapper, ICurrentConnections currentConnections)
        {
            _apiService = apiService;
            _mapper = mapper;
            _currentConnections = currentConnections;
        }
        public void OnNavigatedFrom()
        {
        }

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            
            _isInitialized = true;
        }

        public IRelayCommand ShowAddUnitTestDialogCommand => new RelayCommand(ShowAddUnitTestDialog);

        public void ShowAddUnitTestDialog()
        {
            var addUnitTestDialogWindow = new AddUnitTestDialogWindow(_apiService, _currentConnections);
            addUnitTestDialogWindow.ShowDialog();
            //dialog.ShowDialog();
        }
    }
}
