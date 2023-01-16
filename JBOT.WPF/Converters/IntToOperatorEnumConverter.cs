using JBOT.Application.Helpers;
using JBOT.Domain.Entities;
using JBOT.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace JBOT.WPF.Converters
{
    public class EnumToModelOperatorConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value!= null)
            {
                var opEnum = ((OperatorEnums) value);
                return (int)opEnum;
            }
                
            return null;

        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return (OperatorEnums)((int)value);
            return null;
        }
    }
}
