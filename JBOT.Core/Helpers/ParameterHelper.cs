using AutoMapper;
using Common =  JBOT.Application.Constants;
using JBOT.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JBOT.Application.Constants;
using static System.Formats.Asn1.AsnWriter;
using System.ComponentModel.DataAnnotations;
using JBOT.Domain.Entities;
using MediatR;
using JBOT.Domain.Entities.Enums;

namespace JBOT.Application.Helpers
{
    public static class ParameterHelper
    {
        public static List<OutputParameterDto> InitializeOutputParameters(this TestableObjectDetailsDto testableObject, IMapper mapper)
        {
            string objectType = testableObject.Type;
            var parmeters = testableObject.Parameters.Where(p => p.IsOutput).ToList();
            if (parmeters.Any() && objectType == ObjectType.ScalarFunction)
            {
                var p = parmeters.FirstOrDefault(p => p.Name == string.Empty);
                if (p != null)
                    p.Name = "@return";
            }
            else if (objectType == ObjectType.Procedure)
            {
                parmeters.Insert(0,
                    new ParameterDto()
                    {
                        Name = "@return",
                        DataType = "default(string value)"
                    });
            }
            else
            {
                parmeters = new List<BaseParameterDto>();
            }
            return mapper.Map<List<OutputParameterDto>>(parmeters);
        }


        public static string GetDataTypeDisplayFormat(this BaseParameterDto baseParameter)
        {
            var floatingDataType = new List<string>() { "numeric", "float", "double", "decimal" };
            if (floatingDataType.Contains(baseParameter.DataType.ToLower()))
            {
                return $"{baseParameter.DataType}({baseParameter.Precision},{baseParameter.Scale})";
            }
            else if (baseParameter.DataType.ToLower().Contains("char"))
            {
                return $"{baseParameter.DataType}({baseParameter.MaxLength})";
            }
            else
            {
                return baseParameter.DataType;
            }
        }
        public static string GetParameterDisplayFormat(string parameterName,string dataType, int precision, int scale, int maxLength)
        {
            var floatingDataType = new List<string>() { "numeric", "float", "double", "decimal" };
            if (floatingDataType.Contains(dataType.ToLower()))
            {
                return $"{parameterName} {dataType}({precision},{scale})";
            }
            else if (dataType.ToLower().Contains("char"))
            {
                return $"{parameterName} {dataType}({maxLength})";
            }
            else
            {
                return $"{parameterName} {dataType}";
            }
        }

        public static void SetInputParameterValue(this List<ParameterDto> inputParameterList, UnitTest unitTest)
        {
            foreach (var inputParameter in inputParameterList)
            {
                var parameter = unitTest.Parameters.SingleOrDefault(p => p.ParameterId == inputParameter.Id);
                if (parameter != null)
                {
                    parameter.Value = inputParameter.Value;
                    parameter.DateModified = DateTime.Now;
                }
            }
        }

        public static void SetOutputParameterValue(this List<OutputParameterDto> outputParameterList, UnitTest unitTest)
        {
            foreach (var outputParameter in outputParameterList)
            {
                var assert = unitTest.Assertations.SingleOrDefault(p => p.UnitTestParameter.ParameterId == outputParameter.Id);
                if (assert != null)
                {
                    assert.ExpectedValue = outputParameter.Expected;
                    assert.OperatorId = (int)outputParameter.Operator;
                    assert.ActualValue = outputParameter.Actual;
                    assert.StatusId = outputParameter.IsSuccess ?? false  ? (int)StatusEnums.Success : (int)StatusEnums.Failed;
                    assert.DateModified = DateTime.Now;
                }
            }
        }

        public static string GetParameters(this UnitTest unitTest)
        {
            return
            unitTest.Parameters.Where(p => !p.IsOutput && p.ParameterId > 0)
                .Select(s =>
                     $"{ParameterHelper.GetParameterDisplayFormat(s.ParameterName, s.ParameterType, s.Precision, s.Scale, s.MaxLength)} = {s.Value}"
                ).ToList().ConcatList(",");
        }
    }
}
