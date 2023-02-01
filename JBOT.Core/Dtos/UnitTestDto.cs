using JBOT.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Application.Dtos
{
    public class UnitTestDto
    {
        public int Id { get; set; }
        public string TestName { get; set; }    
        public string TestDescription { get; set; }
        public string DatabaseName { get; set; }
        public string ObjectName { get; set; }
        public string ObjectType { get; set; }
        public string Parameters { get; set; }
        public string Output { get; set; }
        public StatusEnums? Status { get; set; }
    }
}
