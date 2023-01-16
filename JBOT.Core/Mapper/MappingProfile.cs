using AutoMapper;
using JBOT.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BaseParameterDto, ParameterDto>();
            CreateMap<ParameterDto, BaseParameterDto>();

            CreateMap<BaseParameterDto, OutputParameterDto>();
            CreateMap<OutputParameterDto, BaseParameterDto>();
        }

        
    }
}
