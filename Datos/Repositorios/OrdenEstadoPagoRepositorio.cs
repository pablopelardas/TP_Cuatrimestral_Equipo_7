using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class OrdenEstadoPagoRepositorio
    {
        private Helpers.QueryHelper _QueryHelper = new Helpers.QueryHelper();

        private string OrdenEstadoPagoSelect(string prefixTable, string prefixColumn)
        {
            return $@"
{prefixTable}ORDENES_PAGO_ESTADOS.id_orden_pago_estado as '{prefixColumn}id_orden_pago_estado',
{prefixTable}ORDENES_PAGO_ESTADOS.nombre as '{prefixColumn}nombre'";
        }

        private Entidades.OrdenEstadoPagoEntidad OrdenEstadoPagoReader (System.Data.SqlClient.SqlDataReader reader, string prefixColumn = "")
        {
            Entidades.OrdenEstadoPagoEntidad entidad = new Entidades.OrdenEstadoPagoEntidad();
            entidad.id_orden_pago_estado = (int)reader[$"{prefixColumn}id_orden_pago_estado"];
            entidad.nombre = (string)reader[$"{prefixColumn}nombre"];
            return entidad;
        }

        public string GetSelect(string prefix = "")
        {
            return _QueryHelper.BuildSelect(prefix, OrdenEstadoPagoSelect);
        }
        public Entidades.OrdenEstadoPagoEntidad GetEntity(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(reader, prefix, OrdenEstadoPagoReader);
        }

    }
}
