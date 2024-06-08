using Datos.Entidades;
using Datos.Helpers;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
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

        private ProductoDetalleOrdenEntidad ProductoDetalleOrdenReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            return new ProductoDetalleOrdenEntidad
            {
                cantidad = (int)reader[$"{prefix}cantidad"],
                producto_porciones = (int)reader[$"{prefix}producto_porciones"],
                producto_costo = (decimal)reader[$"{prefix}producto_costo"],
                producto_precio = (decimal)reader[$"{prefix}producto_precio"],

                producto = productoRepositorio.GetEntity(reader, prefix + PRODUCTO_PREFIX)
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

        public Entidades.ProductoDetalleOrdenEntidad GetEntity(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(reader, prefix, ProductoDetalleOrdenReader);
        }

        public List<ProductoDetalleOrdenModelo> ObtenerDetallePorOrden(int id)
        {
            List<ProductoDetalleOrdenModelo> productos = new List<ProductoDetalleOrdenModelo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string cmd = $@"
SELECT 
{GetSelect()}
FROM DETALLE_ORDENES
{GetJoin()}
WHERE DETALLE_ORDENES.ID_ORDEN = @id
                    ";
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                while (datos.Lector.Read()) productos.Add(Mappers.ProductoDetalleOrdenMapper.EntidadAModelo(GetEntity(datos.Lector)));
                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void EliminarDetalle(int IdOrden, SqlCommand dbCtx = null)
        {
            if (dbCtx == null)
            {
                AccesoDatos datos = new AccesoDatos();
                try
                {
                    string cmd = $@"DELETE FROM DETALLE_ORDENES WHERE id_orden = {IdOrden}";
                    datos.SetearConsulta(cmd);
                    datos.EjecutarAccion();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    datos.CerrarConexion();
                }
            }
            else
            {
                string cmd = $@"DELETE FROM DETALLE_ORDENES WHERE id_orden = {IdOrden}";
                dbCtx.CommandText = cmd;
                dbCtx.ExecuteNonQuery();
            }
        }

        public void AgregarListaDetalle(int IdOrden, List<ProductoDetalleOrdenEntidad> detalles, SqlCommand dbCtx = null)
        {
            if (dbCtx == null)
            {
                AccesoDatos datos = new AccesoDatos();
                try
                {
                    foreach (ProductoDetalleOrdenEntidad detalle in detalles)
                    {
                        string cmd = $@"
INSERT INTO DETALLE_ORDENES (id_orden, id_producto, cantidad, producto_costo, producto_porciones, producto_precio)
VALUES ({IdOrden}, {detalle.producto.id_producto}, {detalle.cantidad}, {detalle.producto_costo}, {detalle.producto_porciones}, {detalle.producto_precio})
                    ";
                        datos.SetearConsulta(cmd);

                        datos.EjecutarAccion();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    datos.CerrarConexion();
                }
            }
            else
            {
                foreach (ProductoDetalleOrdenEntidad detalle in detalles)
                {
                    string cmd = $@"
INSERT INTO DETALLE_ORDENES (id_orden, id_producto, cantidad, producto_costo, producto_porciones, producto_precio)
VALUES ({IdOrden}, {detalle.producto.id_producto}, {detalle.cantidad}, {detalle.producto_costo.ToString().Replace(',', '.')}, {detalle.producto_porciones}, {detalle.producto_precio.ToString().Replace(',', '.')})
                    ";
                    dbCtx.CommandText = cmd;

                    dbCtx.ExecuteNonQuery();
                }
            }
        }
    }
}
