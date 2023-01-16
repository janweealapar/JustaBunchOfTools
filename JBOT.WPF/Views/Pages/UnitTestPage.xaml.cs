using JBOT.WPF.ViewModels;
using Wpf.Ui.Common.Interfaces;

namespace JBOT.WPF.Views.Pages
{
    /// <summary>
    /// Interaction logic for UnitTestPage.xaml
    /// </summary>
    public partial class UnitTestPage : INavigableView<UnitTestViewModel>
    {
        public UnitTestViewModel ViewModel { get; }
        public UnitTestPage(UnitTestViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }
    }
}
