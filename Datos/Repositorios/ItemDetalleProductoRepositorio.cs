using Datos.Entidades;
using Datos.Helpers;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
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
        private ItemDetalleProductoEntidad ItemDetalleProductoReader(DataRow row, string prefixColumn = "")
        {
            ItemDetalleProductoEntidad entidad = new ItemDetalleProductoEntidad();

            entidad.cantidad = (int)row[$"{prefixColumn}cantidad"];
            entidad.receta = row[$"{prefixColumn + RECETA_PREFIX}.id_receta"] == DBNull.Value ? null : recetaRepositorio.GetEntity(row, prefixColumn + RECETA_PREFIX);
            entidad.suministro = row[$"{prefixColumn + SUMINISTRO_PREFIX}.id_suministro"] == DBNull.Value ? null : suministroRepositorio.GetEntity(row, prefixColumn + SUMINISTRO_PREFIX);

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

        public ItemDetalleProductoEntidad GetEntity(DataRow row, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(row, prefix, ItemDetalleProductoReader);
        }

        public List<ItemDetalleProductoModelo> ObtenerDetalleProducto(int id)
        {
            List<ItemDetalleProductoModelo> items = new List<ItemDetalleProductoModelo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                SqlCommand cmd = new SqlCommand($@"
SELECT
{GetSelect()}
FROM DETALLE_PRODUCTO
{GetJoin()}
WHERE DETALLE_PRODUCTOS.id_producto = @id
");
                cmd.Parameters.AddWithValue("@id", id);

                DataTable response = datos.ExecuteQuery(cmd);
                if (response != null)
                {
                    foreach (DataRow row in response.Rows)
                        items.Add(Mappers.ItemDetalleProductoMapper.EntidadAModelo(GetEntity(row)));
                }
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
