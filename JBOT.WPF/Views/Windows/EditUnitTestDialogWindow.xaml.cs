using JBOT.WPF.Models;
using JBOT.WPF.Services;
using JBOT.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace JBOT.WPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for EditUnitTestDialogWindow.xaml
    /// </summary>
    public partial class EditUnitTestDialogWindow : UiWindow
    {
        public EditUnitTestDialogViewModel ViewModel { get; }
        public EditUnitTestDialogWindow(IApiService apiService, ICurrentConnections currentConnections, int unitTestId)
        {
            ViewModel = new EditUnitTestDialogViewModel(apiService, currentConnections, unitTestId);
            DataContext = this;
            InitializeComponent();
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = ViewModel._isReload;
        }
    }
}
