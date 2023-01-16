using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Domain.Entities.Common
{
    public interface IEnumModel<TModel, TModelIdType>
    {
        int Id { get; set; }
        string Name { get; set; }
    }
    public class EnumBase 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
