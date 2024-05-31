using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class OrdenEstadoRepositorio
    {

        public static string GetSelect(string prefix = "")
        {
            string prefixTable = prefix.Length > 0 ? prefix.Replace(".", "_") + '_' : "";
            prefix = prefix.Length > 0 ? prefix + "." : "";
            return $@"
{prefixTable}ORDENES_ESTADOS.id_orden_estado as '{prefix}id_orden_estado',
{prefixTable}ORDENES_ESTADOS.nombre as '{prefix}nombre'
            ";
        }
        public static Entidades.OrdenEstadoEntidad GetEntidadFromReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            prefix = prefix.Length > 0 ? prefix + "." : "";
            Entidades.OrdenEstadoEntidad entidad = new Entidades.OrdenEstadoEntidad();
            entidad.id_orden_estado = (int)reader[$"{prefix}id_orden_estado"];
            entidad.nombre = (string)reader[$"{prefix}nombre"];
            return entidad;
        }
    }
}
