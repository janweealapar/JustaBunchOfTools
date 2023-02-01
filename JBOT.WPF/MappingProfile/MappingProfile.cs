using AutoMapper;
using JBOT.Application.Dtos;
using JBOT.WPF.DtosWithCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.WPF.MappingProfile
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        { 
            CreateMap<UnitTestDto,UnitTestDtoWithCommand>().ReverseMap();
        } 
    }
}
