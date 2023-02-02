using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Interfaces;
using Wpf.Ui.Common;
using JBOT.WPF.ViewModels;
using JBOT.WPF.Services;
using AutoMapper;
using JBOT.WPF.Models;

namespace JBOT.WPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for AddUnitTestDialogWindow.xaml
    /// </summary>
    public partial class AddUnitTestDialogWindow : UiWindow
    {
        public AddUnitTestDialogViewModel ViewModel { get; }
        public AddUnitTestDialogWindow(IApiService apiService, ICurrentConnections currentConnections)
        {
            ViewModel = new AddUnitTestDialogViewModel(apiService, currentConnections, this);
            DataContext = this;
            InitializeComponent();
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = ViewModel._isReload;
        }

    }
}
