using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Application.Dtos
{
    public class TestableObjectDto
    {
        public int ObjectId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Parameters { get; set; }
        public string ReturnType { get; set; }
    }
}
