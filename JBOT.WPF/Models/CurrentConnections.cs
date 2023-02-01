using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.WPF.Models
{
    public interface ICurrentConnections
    {
        string? Server { get; }
        int? DatabaseId { get; }
        string? DatabaseName { get; }
    }
    public class CurrentConnections : ICurrentConnections
    {
        public CurrentConnections(string? server, int? databaseId, string? databaseName)
        {
            this.Server = server;
            DatabaseId = databaseId;
            DatabaseName = databaseName;
        }

        public string? Server { get; private set; }
        public int? DatabaseId { get; private set; }
        public string? DatabaseName {  get; private set; }
    }
}
