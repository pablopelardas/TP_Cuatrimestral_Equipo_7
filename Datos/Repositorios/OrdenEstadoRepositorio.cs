using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class OrdenEstadoRepositorio
    {

        public static string GetSelectOrdenesEstados(string prefix = "")
        {
            return $@"
ORDENES_ESTADOS.id_orden_estado as '{prefix}id_orden_estado',
ORDENES_ESTADOS.nombre as '{prefix}nombre'
            ";
        }
        public static Entidades.OrdenEstadoEntidad GetEntidadFromReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            Entidades.OrdenEstadoEntidad entidad = new Entidades.OrdenEstadoEntidad();
            entidad.id_orden_estado = (int)reader[$"{prefix}id_orden_estado"];
            entidad.nombre = (string)reader[$"{prefix}nombre"];
            return entidad;
        }
    }
}
