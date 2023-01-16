using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Domain.Entities.Enums
{
    public enum StatusEnums
    {
        Failed = 1,
        Success = 2,
    }

    public enum OperatorEnums
    {
        Equal = 1,
        [Description("Not Equal")]
        NotEqual = 2,
        [Description("Greater Than")]
        GreaterThan = 3,
        [Description("Greater Than Or Equal To")]
        GreaterThanOrEqualTo = 4,
        [Description("LessThan")]
        LessThan = 5,
        [Description("Less Than Or Equal To")]
        LessThanOrEqualTo = 6
    }
}
