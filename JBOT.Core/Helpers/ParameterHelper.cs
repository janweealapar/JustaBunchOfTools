using AutoMapper;
using Common =  JBOT.Application.Constants;
using JBOT.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JBOT.Application.Constants;

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
                        Name= "@return",
                        DataType = "default(string value)"
                    });
            }
            else
            {
                parmeters = new List<BaseParameterDto>();
            }
            return mapper.Map<List<OutputParameterDto>>(parmeters);
        }
    }
}
