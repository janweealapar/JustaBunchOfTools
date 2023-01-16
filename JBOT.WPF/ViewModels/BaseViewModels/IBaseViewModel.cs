using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.WPF.ViewModels.BaseViewModels
{
    public interface IBaseViewModel
    {
    }

    public abstract class BaseViewModel : ObservableObject, IBaseViewModel
    {
         
    }
}
