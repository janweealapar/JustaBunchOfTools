using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using JBOT.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.WPF.DtosWithCommand
{
    public class UnitTestDtoWithCommand : UnitTestDto
    {
        public IRelayCommand<object> EditCommand { get; set; }
        public IAsyncRelayCommand<object> RemoveCommand { get; set; }
    }

}
