using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Application.Helpers
{
    public static class ListHelper
    {
        public static string Concat(this List<string> list, string separator)
        {
            return string.Join(separator, list);
        }
    }
}
