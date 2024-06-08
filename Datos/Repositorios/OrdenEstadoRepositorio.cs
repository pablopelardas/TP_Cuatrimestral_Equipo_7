using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class OrdenEstadoRepositorio
    {
        private Helpers.QueryHelper _QueryHelper = new Helpers.QueryHelper();

        private string OrdenEstadoSelect(string prefixTable, string prefix)
        {
            return $@"
{prefixTable}ORDENES_ESTADOS.id_orden_estado as '{prefix}id_orden_estado',
{prefixTable}ORDENES_ESTADOS.nombre as '{prefix}nombre'";
        }

        private Entidades.OrdenEstadoEntidad OrdenEstadoReader (DataRow row, string prefix = "")
        {
            Entidades.OrdenEstadoEntidad entidad = new Entidades.OrdenEstadoEntidad();
            entidad.id_orden_estado = (int)row[$"{prefix}id_orden_estado"];
            entidad.nombre = (string)row[$"{prefix}nombre"];
            return entidad;
        }
        public string GetSelect(string prefix = "")
        {
            return _QueryHelper.BuildSelect(prefix, OrdenEstadoSelect);
        }
        public Entidades.OrdenEstadoEntidad GetEntity(DataRow row, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(row, prefix, OrdenEstadoReader);
        }
    }
}
