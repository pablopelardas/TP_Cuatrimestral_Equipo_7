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

        public static Entidades.ProductoDetalleOrdenEntidad getEntidadFromReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            Entidades.ProductoDetalleOrdenEntidad entidad = new Entidades.ProductoDetalleOrdenEntidad();
            entidad.producto = ProductoRepositorio.getEntidadFromReader(reader, "producto.");
            entidad.cantidad = (int)reader[$"{prefix}cantidad"];
            entidad.producto_porciones = (int)reader[$"{prefix}producto_porciones"];
            entidad.producto_costo = (decimal)reader[$"{prefix}producto_costo"];
            entidad.producto_precio = (decimal)reader[$"{prefix}producto_precio"];
            return entidad;
        }

        public List<ProductoDetalleOrdenModelo> ObtenerDetallePorOrden(int id)
        {
            List<ProductoDetalleOrdenModelo> productos = new List<ProductoDetalleOrdenModelo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string cmd = @"
SELECT 
    DO.producto_costo,
    DO.producto_porciones,
    DO.producto_precio,
    DO.cantidad,
    P.id_producto AS 'producto.id_producto',
    P.nombre AS 'producto.nombre',
    P.descripcion AS 'producto.descripcion',
    P.porciones AS 'producto.porciones',
    P.horas_trabajo AS 'producto.horas_trabajo',
    P.tipo_precio AS 'producto.tipo_precio',
    P.valor_precio AS 'producto.valor_precio',
    C.id_categoria AS 'producto.categoria.id_categoria',
    C.nombre AS 'producto.categoria.nombre',
    C.tipo AS 'producto.categoria.tipo'
FROM DETALLE_ORDENES DO
INNER JOIN PRODUCTOS P ON DO.ID_PRODUCTO = P.ID_PRODUCTO
INNER JOIN CATEGORIAS C ON P.ID_CATEGORIA = C.ID_CATEGORIA
WHERE DO.ID_ORDEN = @id
                    ";
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Entidades.ProductoDetalleOrdenEntidad entidad = new Entidades.ProductoDetalleOrdenEntidad();
                      entidad.producto = ProductoRepositorio.getEntidadFromReader(datos.Lector, "producto.");
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
