using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class OrdenEstadoPagoRepositorio
    {
        public static Entidades.OrdenEstadoPagoEntidad getEntidadFromReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            Entidades.OrdenEstadoPagoEntidad entidad = new Entidades.OrdenEstadoPagoEntidad();
            entidad.id_orden_pago_estado = (int)reader[$"{prefix}id_orden_pago_estado"];
            entidad.nombre = (string)reader[$"{prefix}nombre"];
            return entidad;
        }

    }
}
