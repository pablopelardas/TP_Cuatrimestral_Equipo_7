using Datos.Entidades;
using Datos.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class ItemDetalleProductoRepositorio
    {
        private Helpers.QueryHelper _QueryHelper = new Helpers.QueryHelper();
        private string RECETA_PREFIX = "rec";
        private string SUMINISTRO_PREFIX = "sum";

        private RecetaRepositorio recetaRepositorio = new RecetaRepositorio();
        private SuministroRepositorio suministroRepositorio = new SuministroRepositorio();

        private string ItemDetalleProductoSelect(string prefixTable, string prefixColumn)
        {
            return $@"
{prefixTable}DETALLE_PRODUCTO.cantidad as '{prefixColumn}cantidad',
{recetaRepositorio.GetSelect(prefixColumn + RECETA_PREFIX)},
{suministroRepositorio.GetSelect(prefixColumn + SUMINISTRO_PREFIX)}
";
        }

        private string ItemDetalleProductoJoin(string prefixTable)
        {
            string recetaAlias = prefixTable + RECETA_PREFIX + "_RECETAS";
            string suministroAlias = prefixTable + SUMINISTRO_PREFIX + "_SUMINISTROS";

            return $@"
    LEFT JOIN RECETAS as {recetaAlias} ON {prefixTable}DETALLE_PRODUCTOS.id_receta = {recetaAlias}.id_receta
    LEFT JOIN SUMINISTROS AS {suministroAlias} ON {prefixTable}DETALLE_PRODUCTOS.id_suministro = {recetaAlias}.id_suministro
";
        }
        private ItemDetalleProductoEntidad ItemDetalleProductoReader(SqlDataReader reader, string prefixColumn = "")
        {
            ItemDetalleProductoEntidad entidad = new ItemDetalleProductoEntidad();

            entidad.cantidad = (int)reader[$"{prefixColumn}cantidad"];
            entidad.receta = reader[$"{prefixColumn + RECETA_PREFIX}.id_receta"] == DBNull.Value ? null : recetaRepositorio.GetEntity(reader, prefixColumn + RECETA_PREFIX);
            entidad.suministro = reader[$"{prefixColumn + SUMINISTRO_PREFIX}.id_suministro"] == DBNull.Value ? null : suministroRepositorio.GetEntity(reader, prefixColumn + SUMINISTRO_PREFIX);

            return entidad;
        }
        public string GetSelect(string prefix = "")
        {
            return _QueryHelper.BuildSelect(prefix, ItemDetalleProductoSelect);
        }

        public string GetJoin(string prefix = "")
        {
            return _QueryHelper.BuildJoin(prefix, ItemDetalleProductoJoin);
        }

        public ItemDetalleProductoEntidad GetEntity(SqlDataReader reader, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(reader, prefix, ItemDetalleProductoReader);
        }
    }
}
