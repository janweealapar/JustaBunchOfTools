using AutoMapper;
using JBOT.Application.Dtos;
using JBOT.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.WPF
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<TestableObjectDto, TestableObjectModel>().ReverseMap();
        }
    }
}
