using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Migrations
{
    public class GuidGenerator
    {
        public static Guid Generate()
        {
            //var r = new Random(seed);
            //var guid = new byte[16];
            //r.NextBytes(guid);

            return Guid.NewGuid();
        }
    }
}
