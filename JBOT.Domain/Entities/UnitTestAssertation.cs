using JBOT.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Domain.Entities
{
    public class UnitTestAssertation :BaseEntity<int>
    {
        public int? UnitTestId { get; set; }
        public virtual UnitTest UnitTest { get; set; }

        public int? UnitTestParameterId { get; set; }
        public virtual UnitTestParameter UnitTestParameter { get; set; }

        public string ExpectedValue { get; set; }
        public int? OperatorId { get; set; }
        public virtual Operator Operator { get; set; }
        public string ActualValue { get; set; }
        public int? StatusId { get; set; }
        public virtual Status Status { get; set; }
    }
}
