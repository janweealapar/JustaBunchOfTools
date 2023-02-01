using AutoMapper;
using JBOT.Application.Dtos;
using JBOT.Domain.Entities;
using JBOT.Domain.Entities.Enums;
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
            #region  CreateMap<BaseParameterDto, ParameterDto>()
            CreateMap<BaseParameterDto, ParameterDto>().ReverseMap();
            #endregion

            #region CreateMap<BaseParameterDto, OutputParameterDto>()
            CreateMap<BaseParameterDto, OutputParameterDto>().ReverseMap();
            #endregion

            #region CreateMap<ParameterDto, UnitTestParameter>()
            CreateMap<ParameterDto, UnitTestParameter>()
                .ForMember(
                    model => model.Id,
                    opt => opt.Ignore())
                .ForMember(
                    model => model.ParameterId,
                    opt => opt.MapFrom(dto => dto.Id))
                .ForMember(
                    model => model.ParameterName,
                    opt => opt.MapFrom(dto => dto.Name))
                .ForMember(
                    model => model.ParameterType,
                    opt => opt.MapFrom(dto => dto.DataType))
                .ReverseMap();
            #endregion

            #region CreateMap<UnitTestParameter, ParameterDto>()
            CreateMap<UnitTestParameter, ParameterDto>()
                .ForMember(
                    dto => dto.Id,
                    opt => opt.MapFrom(model => model.ParameterId))
                .ForMember(
                    dto => dto.Name,
                    opt => opt.MapFrom(model => model.ParameterName))
                .ForMember(
                    dto => dto.DataType,
                    opt => opt.MapFrom(model => model.ParameterType));
            #endregion

            #region CreateMap<OutputParameterDto, UnitTestParameter>()
            CreateMap<OutputParameterDto, UnitTestParameter>()
                .ForMember(
                    model => model.Id,
                    opt => opt.Ignore())
                .ForMember(
                    model => model.ParameterId,
                    opt => opt.MapFrom(dto => dto.Id))
                .ForMember(
                    model => model.ParameterName,
                    opt => opt.MapFrom(dto => dto.Name))
                .ForMember(
                    model => model.ParameterType,
                    opt => opt.MapFrom(dto => dto.DataType))
                .ReverseMap();
            #endregion

            #region CreateMap<TestableObjectDetailsDto, UnitTest>()
            CreateMap<TestableObjectDetailsDto, UnitTest>()
                .ForMember(
                    model => model.ObjectName,
                    opt => opt.MapFrom(dto => dto.Name))
                .ForMember(
                    model => model.ObjectType,
                    opt => opt.MapFrom(dto => dto.Type))
                .ForMember(
                    model => model.StatusId,
                    opt => opt.MapFrom(dto => (int)dto.Status))
                .ForMember(
                    model => model.Parameters,
                    opt => opt.Ignore())
                .ForMember(
                    model => model.Status,
                    opt => opt.Ignore());

            CreateMap<UnitTest, TestableObjectDetailsDto>()
                .ForMember(
                    dto => dto.Name,
                    opt => opt.MapFrom(model => model.ObjectName))
                .ForMember(
                    dto => dto.Type,
                    opt => opt.MapFrom(model => model.ObjectType))
                .ForMember(
                    dto => dto.Status,
                    opt => opt.MapFrom(model => (StatusEnums)model.StatusId))
                .ForMember(
                    dto => dto.Parameters,
                    opt => opt.Ignore())
                .ForMember(
                    dto => dto.InputParameters,
                    opt => opt.Ignore())
                .ForMember(
                    dto => dto.OutputParameters,
                    opt => opt.Ignore());
            #endregion

            #region CreateMap<UnitTestDto, UnitTest>()
            CreateMap<UnitTest, UnitTestDto>()
                .ForMember(
                    dto => dto.Parameters,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dto => dto.Output,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dto => dto.Status,
                    opt => opt.MapFrom(model => (StatusEnums)model.StatusId));
            #endregion

            #region MyRegion
            CreateMap<UnitTestAssertation, OutputParameterDto>()
                .ForMember(
                    dto => dto.Id,
                    opt => opt.MapFrom(model => model.UnitTestParameter.ParameterId)
                )
                .ForMember(
                    dto => dto.Name,
                    opt => opt.MapFrom(model => model.UnitTestParameter.ParameterName)
                )
                .ForMember(
                    dto => dto.DataType,
                    opt => opt.MapFrom(model => model.UnitTestParameter.ParameterType)
                )
                .ForMember(
                    dto => dto.MaxLength,
                    opt => opt.MapFrom(model => model.UnitTestParameter.MaxLength)
                )
                .ForMember(
                    dto => dto.Precision,
                    opt => opt.MapFrom(model => model.UnitTestParameter.Precision)
                )
                .ForMember(
                    dto => dto.Scale,
                    opt => opt.MapFrom(model => model.UnitTestParameter.Scale)
                )
                .ForMember(
                    dto => dto.IsOutput,
                    opt => opt.MapFrom(model => model.UnitTestParameter.IsOutput)
                )
                .ForMember(
                    dto => dto.Operator,
                    opt => opt.MapFrom(model => (OperatorEnums)model.OperatorId)
                )
                .ForMember(
                    dto => dto.Expected,
                    opt => opt.MapFrom(model => model.ExpectedValue)
                )
                .ForMember(
                    dto => dto.Actual,
                    opt => opt.MapFrom(model => model.ActualValue)
                )
                .ForMember(
                    dto => dto.IsSuccess,
                    opt => opt.MapFrom(model => (StatusEnums)model.StatusId == StatusEnums.Success)
                );
            #endregion
        }


    }
}
