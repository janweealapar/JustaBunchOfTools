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
        public int DatabaseId { get; set; }
        public string DatabaseName { get; set; }
        public int ObjectId { get; set; }
        public string ObjectName { get; set; }
        public List<UnitTestParameter> Parameters { get; set; }
        public string Act { get; set; }
        public List<UnitTestAssertation> Assertations { get; set; }
        public int? StatusId { get; set; }
        public Status Status { get; set; }
    }
}
