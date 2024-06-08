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
    public class ProductoDetalleOrdenRepositorio
    {
        private QueryHelper _QueryHelper = new QueryHelper();
        private string PRODUCTO_PREFIX = "pro";
        private ProductoRepositorio productoRepositorio = new ProductoRepositorio();

        private string ProductoDetalleOrdenSelect(string prefixTable, string prefixColumn)
        {
            return $@"
{prefixTable}DETALLE_ORDENES.producto_costo AS '{prefixColumn}producto_costo',
{prefixTable}DETALLE_ORDENES.producto_porciones AS '{prefixColumn}producto_porciones',
{prefixTable}DETALLE_ORDENES.producto_precio AS '{prefixColumn}producto_precio',
{prefixTable}DETALLE_ORDENES.cantidad AS '{prefixColumn}cantidad',
{productoRepositorio.GetSelect(prefixColumn + PRODUCTO_PREFIX)}
";
        }

        private string ProductoDetalleOrdenJoin(string prefixTable)
        {
            string aliasProducto = prefixTable + PRODUCTO_PREFIX + "_PRODUCTOS";
            return $@"
INNER JOIN PRODUCTOS AS {aliasProducto} ON {prefixTable}DETALLE_ORDENES.ID_PRODUCTO = {aliasProducto}.ID_PRODUCTO
{productoRepositorio.GetJoin(prefixTable + PRODUCTO_PREFIX)}
            ";
        }

        private ProductoDetalleOrdenEntidad ProductoDetalleOrdenReader(DataRow row, string prefix = "")
        {
            return new ProductoDetalleOrdenEntidad
            {
                cantidad = (int)row[$"{prefix}cantidad"],
                producto_porciones = (int)row[$"{prefix}producto_porciones"],
                producto_costo = (decimal)row[$"{prefix}producto_costo"],
                producto_precio = (decimal)row[$"{prefix}producto_precio"],

                producto = productoRepositorio.GetEntity(row, prefix + PRODUCTO_PREFIX)
            };
        }

        public string GetSelect(string prefix = "")
        {
            return _QueryHelper.BuildSelect(prefix, ProductoDetalleOrdenSelect);
        }

        public string GetJoin(string prefix = "")
        {
            return _QueryHelper.BuildJoin(prefix, ProductoDetalleOrdenJoin);
        }

        public Entidades.ProductoDetalleOrdenEntidad GetEntity(DataRow row, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(row, prefix, ProductoDetalleOrdenReader);
        }

        public List<ProductoDetalleOrdenModelo> ObtenerDetallePorOrden(int id)
        {
            List<ProductoDetalleOrdenModelo> productos = new List<ProductoDetalleOrdenModelo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                SqlCommand cmd = new SqlCommand($@"
SELECT 
{GetSelect()}
FROM DETALLE_ORDENES
{GetJoin()}
WHERE DETALLE_ORDENES.ID_ORDEN = @id
                    ");
                cmd.Parameters.AddWithValue("@id", id);

                DataTable response = datos.ExecuteQuery(cmd);
                foreach (DataRow row in response.Rows)
                {
                    productos.Add(Mappers.ProductoDetalleOrdenMapper.EntidadAModelo(GetEntity(row)));
                }
                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlCommand GuardarDetalleCommand(ProductoDetalleOrdenEntidad detalle, int idOrden)
        {
            SqlCommand cmd = new SqlCommand($@"
INSERT INTO DETALLE_ORDENES (id_orden, id_producto, cantidad, producto_costo, producto_porciones, producto_precio)
VALUES (@id_orden, @id_producto, @cantidad, @producto_costo, @producto_porciones, @producto_precio)
");
            cmd.Parameters.AddWithValue("@id_orden", idOrden);
            cmd.Parameters.AddWithValue("@id_producto", detalle.producto.id_producto);
            cmd.Parameters.AddWithValue("@cantidad", detalle.cantidad);
            cmd.Parameters.AddWithValue("@producto_costo", detalle.producto_costo);
            cmd.Parameters.AddWithValue("@producto_porciones", detalle.producto_porciones);
            cmd.Parameters.AddWithValue("@producto_precio", detalle.producto_precio);
            return cmd;
        }

        public SqlCommand EliminarDetalleCommand(int idOrden)
        {
            SqlCommand cmd = new SqlCommand($@"
DELETE FROM DETALLE_ORDENES
WHERE ID_ORDEN = @id_orden
");
            cmd.Parameters.AddWithValue("@id_orden", idOrden);
            return cmd;
        }


    }
}
