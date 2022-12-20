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
        public string ObjectName { get; set; }
        public string Arrange { get; set; }
        public string Act { get; set; }
        public bool Assert { get; set; }
        public string Status { get; set; }
    }
}
