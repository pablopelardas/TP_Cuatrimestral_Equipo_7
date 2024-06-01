using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Helpers
{
    public class QueryHelper
    {
        public string getPrefixTable(string prefix)
        {
            return prefix.Length > 0 ? prefix.Replace(".", "_") + '_' : "";
        }
        public string getPrefixColumn(string prefix) {
            return prefix.Length > 0 ? prefix + "." : "";
        }

        public string BuildSelect(string prefix = "", Func<string, string, string> action = null)
        {
            string prefixTable = prefix.Length > 0 ? prefix.Replace(".", "_") + '_' : "";
            prefix = prefix.Length > 0 ? prefix + "." : "";
            return action?.Invoke(prefixTable, prefix);
        }

        public string BuildJoin(string prefix = "", Func<string, string> action = null)
        {
            string prefixTable = prefix.Length > 0 ? prefix.Replace(".", "_") + '_' : "";
            prefix = prefix.Length > 0 ? prefix + "." : "";
            return action?.Invoke(prefixTable);
        }
        public T BuildEntityFromReader<T>(System.Data.SqlClient.SqlDataReader reader, string prefix = "", Func<System.Data.SqlClient.SqlDataReader, string, T> callback = null)
        {
            prefix = prefix.Length > 0 ? prefix + "." : "";
            return callback.Invoke(reader, prefix);
        }


    }
}
