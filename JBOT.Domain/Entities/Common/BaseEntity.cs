using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Domain.Entities.Common
{
    public abstract class BaseEntity<T>
    {
        public virtual T Id { get; set; }
        public string RecordUser { get; set; }
        public DateTime DateRecorded { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }
    }
}
