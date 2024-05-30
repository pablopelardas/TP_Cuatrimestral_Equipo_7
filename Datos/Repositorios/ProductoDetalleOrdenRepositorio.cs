using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    internal class ProductoDetalleOrdenRepositorio
    {
        public static string GetSelectDetalleProducto(string prefix = "")
        {
            string prefixTable = prefix.Length > 0 ? prefix.Replace(".", "_") + '_' : "";
            prefix = prefix.Length > 0 ? prefix + "." : "";
            return $@"
{prefixTable}DETALLE_ORDENES.producto_costo AS '{prefix}producto_costo',
{prefixTable}DETALLE_ORDENES.producto_porciones AS '{prefix}producto_porciones',
{prefixTable}DETALLE_ORDENES.producto_precio AS '{prefix}producto_precio',
{prefixTable}DETALLE_ORDENES.cantidad AS '{prefix}cantidad',
{ProductoRepositorio.GetSelectProductos("producto")}
";
        }

        public static string GetJoinDetalleProducto(string prefix = "")
        {
            prefix = prefix.Length > 0 ? prefix.Replace(".", "_") + '_' : "";
            string aliasProducto = prefix + "producto_" + "PRODUCTOS";

            return $@"
INNER JOIN PRODUCTOS AS {aliasProducto} ON {prefix}DETALLE_ORDENES.ID_PRODUCTO = {aliasProducto}.ID_PRODUCTO
{ProductoRepositorio.GetJoinProductos(prefix + "producto")}
            ";

        }

        public static Entidades.ProductoDetalleOrdenEntidad GetEntidadFromReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            prefix = prefix.Length > 0 ? prefix + "." : "";
            Entidades.ProductoDetalleOrdenEntidad entidad = new Entidades.ProductoDetalleOrdenEntidad();
            entidad.cantidad = (int)reader[$"{prefix}cantidad"];
            entidad.producto_porciones = (int)reader[$"{prefix}producto_porciones"];
            entidad.producto_costo = (decimal)reader[$"{prefix}producto_costo"];
            entidad.producto_precio = (decimal)reader[$"{prefix}producto_precio"];

            entidad.producto = ProductoRepositorio.GetEntidadFromReader(reader, "producto");
            return entidad;
        }

        public List<ProductoDetalleOrdenModelo> ObtenerDetallePorOrden(int id)
        {
            List<ProductoDetalleOrdenModelo> productos = new List<ProductoDetalleOrdenModelo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string cmd = $@"
SELECT 
{GetSelectDetalleProducto()}
FROM DETALLE_ORDENES
{GetJoinDetalleProducto()}
WHERE DETALLE_ORDENES.ID_ORDEN = @id
                    ";
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Entidades.ProductoDetalleOrdenEntidad entidad = new Entidades.ProductoDetalleOrdenEntidad();
                      entidad.producto = ProductoRepositorio.GetEntidadFromReader(datos.Lector, "producto.");
                      entidad.cantidad = (int)datos.Lector["cantidad"];
                      entidad.producto_porciones = (int)datos.Lector["producto_porciones"];
                      entidad.producto_costo = (decimal)datos.Lector["producto_costo"];
                      entidad.producto_precio = (decimal)datos.Lector["producto_precio"];
                    productos.Add(Mappers.ProductoDetalleOrdenMapper.EntidadAModelo(entidad));
                }
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

    }
}
