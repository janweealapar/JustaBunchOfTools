using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wpf.Ui.Controls;

namespace JBOT.WPF.ViewModels
{
    public abstract class BaseDialogViewModel : ObservableObject
    {
        public BaseDialogViewModel(UiWindow ownerPage)
        {
            OwnerPage = ownerPage;
        }
        public UiWindow OwnerPage { get; private set; }

        public ICommand CloseCommand => new RelayCommand(CloseOwnerPage);

        public void CloseOwnerPage()
        {
            OwnerPage.Close();
        }

        public ICommand ClearCommand => new RelayCommand(Clear);

        protected abstract void Clear();
    }
}
