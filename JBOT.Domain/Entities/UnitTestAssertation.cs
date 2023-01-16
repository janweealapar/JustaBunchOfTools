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
        public UnitTest UnitTest { get; set; }

        public int? UnitTestParameterId { get; set; }
        public UnitTestParameter UnitTestParameter { get; set; }

        public string ExpectedValue { get; set; }
        public int? OperatorId { get; set; }
        public Operator Operator { get; set; }
        public string ActualValue { get; set; }
        public int? StatusId { get; set; }
        public Status Status { get; set; }
    }
}
