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
            string prefixTable = prefix.Length > 0 ? prefix.Replace(".", "_") + '_' : "";
            prefix = prefix.Length > 0 ? prefix + "." : "";
            return $@"
{prefixTable}ORDENES_PAGO_ESTADOS.id_orden_pago_estado as '{prefix}id_orden_pago_estado',
{prefixTable}ORDENES_PAGO_ESTADOS.nombre as '{prefix}nombre'
            ";
        }
        public static Entidades.OrdenEstadoPagoEntidad GetEntidadFromReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            prefix = prefix.Length > 0 ? prefix + "." : "";
            Entidades.OrdenEstadoPagoEntidad entidad = new Entidades.OrdenEstadoPagoEntidad();
            entidad.id_orden_pago_estado = (int)reader[$"{prefix}id_orden_pago_estado"];
            entidad.nombre = (string)reader[$"{prefix}nombre"];
            return entidad;
        }

    }
}
