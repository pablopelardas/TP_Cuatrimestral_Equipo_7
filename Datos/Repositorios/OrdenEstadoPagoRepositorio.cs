using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class OrdenEstadoPagoRepositorio
    {
        public static string GetSelectOrdenesEstadosPago(string prefix = "")
        {
            return $@"
ORDENES_PAGO_ESTADOS.id_orden_pago_estado as '{prefix}id_orden_pago_estado',
ORDENES_PAGO_ESTADOS.nombre as '{prefix}nombre'
            ";
        }
        public static Entidades.OrdenEstadoPagoEntidad GetEntidadFromReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            Entidades.OrdenEstadoPagoEntidad entidad = new Entidades.OrdenEstadoPagoEntidad();
            entidad.id_orden_pago_estado = (int)reader[$"{prefix}id_orden_pago_estado"];
            entidad.nombre = (string)reader[$"{prefix}nombre"];
            return entidad;
        }

    }
}
