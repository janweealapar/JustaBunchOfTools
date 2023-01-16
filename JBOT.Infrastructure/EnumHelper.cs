using JBOT.Domain.Entities.Common;
using JBOT.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Application.Helpers
{
    public static class EnumHelper
    {
        public static IEnumerable<TModel> EnumToModel<TModel, TEnum>() 
            where TModel : IEnumModel<TModel, int>, new()
            where TEnum : struct
        {
            var enums = new List<TModel>();
            foreach (var enumVar in (TEnum[])Enum.GetValues(typeof(TEnum)))
            {
                enums.Add(new TModel
                {
                    Id = Convert.ToInt32(enumVar),
                    Name = enumVar.GetEnumDescription()
                });
            }
            return enums;
        }

        public static string GetEnumDescription<TEnum>(this TEnum tenum) where TEnum: struct
        {
            string description = string.Empty;
            var enumType = typeof(TEnum);
            var members = enumType.GetMember(tenum.ToString());
            
            description = (members[0].GetCustomAttributes(typeof(DescriptionAttribute),
                                    false).FirstOrDefault() as DescriptionAttribute)?.Description ?? tenum.ToString();
            
            return description;
    }
    }
}
