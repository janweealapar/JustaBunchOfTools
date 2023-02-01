using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Application.Helpers
{
    public static class ListHelper
    {
        public static string ConcatList(this List<string> list, string separator)
        {
            return string.Join(separator, list);
        }

        public static string ConcatList(this IEnumerable<string> list, string separator)
        {
            return string.Join(separator, list);
        }
    }
}
