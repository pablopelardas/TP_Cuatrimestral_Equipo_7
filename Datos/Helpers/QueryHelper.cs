using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Helpers
{
    public class QueryHelper
    {

        public string BuildSelect(string prefix = "", Func<string, string, string> action = null)
        {
            string prefixTable = prefix.Length > 0 ? prefix.Replace(".", "_") + '_' : "";
            prefix = prefix.Length > 0 ? prefix + "." : "";
            return action?.Invoke(prefixTable, prefix);
        }

        public string BuildJoin(string prefix = "", Func<string, string> action = null)
        {
            string prefixTable = prefix.Length > 0 ? prefix.Replace(".", "_") + '_' : "";
            return action?.Invoke(prefixTable);
        }
        public T BuildEntityFromReader<T>(DataRow reader, string prefix = "", Func<DataRow, string, T> callback = null)
        {
            prefix = prefix.Length > 0 ? prefix + "." : "";
            return callback.Invoke(reader, prefix);
        }

    }
}
