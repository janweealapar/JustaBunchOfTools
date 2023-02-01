using JBOT.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace JBOT.Domain.Entities
{
    public class UnitTest : BaseEntity<int>
    {
        public string TestName { get; set; }
        public string Description { get; set; }
        public string Server { get; set; }
        public int DatabaseId { get; set; }
        public string DatabaseName { get; set; }
        public int ObjectId { get; set; }
        public string ObjectName { get; set; }
        public string ObjectType { get; set; }
        public virtual List<UnitTestParameter> Parameters { get; set; } = new();
        public virtual List<UnitTestAssertation> Assertations { get; set; } = new();
        public int? StatusId { get; set; }
        public virtual Status Status { get; set; }
    }
}
