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
    p.id_producto,
    p.nombre as producto_nombre,
    p.descripcion,
    p.horas_trabajo,
    p.tipo_precio,
    c.*
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
                    entidad.producto_costo = (decimal)datos.Lector["producto_costo"];
                    entidad.producto_porciones = (int)datos.Lector["producto_porciones"];
                    entidad.producto_precio = (decimal)datos.Lector["producto_precio"];
                    entidad.id_producto = (int)datos.Lector["id_producto"];
                    entidad.producto_nombre = (string)datos.Lector["producto_nombre"];
                    entidad.descripcion = (string)datos.Lector["descripcion"];
                    entidad.horas_trabajo = (decimal)datos.Lector["horas_trabajo"];
                    entidad.tipo_precio = (string)datos.Lector["tipo_precio"];
                    entidad.id_categoria = (int)datos.Lector["id_categoria"];
                    entidad.tipo = (string)datos.Lector["tipo"];
                    entidad.nombre = (string)datos.Lector["nombre"];
                    entidad.cantidad = (int)datos.Lector["cantidad"];

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
