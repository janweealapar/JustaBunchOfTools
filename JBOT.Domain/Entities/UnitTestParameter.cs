using JBOT.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Domain.Entities
{
    public class UnitTestParameter : BaseEntity<int>
    {
        public int ParameterId { get; set; }
        public string ParameterName { get; set; }
        public string ParameterType { get; set; }
        public int MaxLength { get; set; }
        public int Precision { get; set; }
        public int Scale { get; set; }
        public bool IsOutput { get; set; }
        public string Value { get; set; }
        public int? UnitTestId { get; set; }
        public virtual UnitTest UnitTest { get; set; }
    }
}
