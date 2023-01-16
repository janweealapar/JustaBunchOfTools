using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.WPF.Models
{
    public interface ICurrentConnections
    {
        int DatabaseId { get; set; }
        string DatabaseName { get; set; }
    }
    public class CurrentConnections : ICurrentConnections
    {
        public int DatabaseId { get; set; }
        public string DatabaseName { get; set; }
    }
}
