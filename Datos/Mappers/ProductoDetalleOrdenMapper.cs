using Datos.EF;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class ProductoDetalleOrdenMapper
    {
        internal static Dominio.Modelos.ProductoDetalleOrdenModelo EntidadAModelo(DETALLEORDEN entidad)
        {
            Dominio.Modelos.ProductoDetalleOrdenModelo modelo = new Dominio.Modelos.ProductoDetalleOrdenModelo
            {
                IdOrden = entidad.id_orden,
                Cantidad = entidad.cantidad,
                CostoUnitarioActual = decimal.Round(entidad.producto_costo, 2),
                PrecioUnitarioActual = decimal.Round(entidad.producto_precio, 2),
                Porciones = entidad.producto_porciones,
            };

            if (entidad.PRODUCTO != null)
            {
                modelo.Producto = ProductoMapper.EntidadAModelo(entidad.PRODUCTO);
            }

            return modelo;
        }

        internal static List<Dominio.Modelos.ProductoDetalleOrdenModelo> EntidadesAModelos(List<DETALLEORDEN> entidades)
        {
            return entidades.Select(entidad => EntidadAModelo(entidad)).ToList();
        }
        internal static DETALLEORDEN ModeloAEntidad(Dominio.Modelos.ProductoDetalleOrdenModelo modelo)
        {
            return new DETALLEORDEN
            {
                id_orden = modelo.IdOrden,
                id_producto = modelo.Producto.IdProducto,
                cantidad = modelo.Cantidad,
                producto_costo = modelo.CostoUnitarioActual,
                producto_precio = modelo.PrecioUnitarioActual,
                producto_porciones = modelo.Porciones,
            };
        }

        internal static List<DETALLEORDEN> ModelosAEntidades(List<Dominio.Modelos.ProductoDetalleOrdenModelo> modelos)
        {
            return modelos.Select(modelo => ModeloAEntidad(modelo)).ToList();
        }

        internal static void ActualizarEntidad(ref DETALLEORDEN entidad,
            Dominio.Modelos.ProductoDetalleOrdenModelo modelo)
        {
            entidad.id_orden = modelo.IdOrden;
            entidad.id_producto = modelo.Producto.IdProducto;
            entidad.cantidad = modelo.Cantidad;
            entidad.producto_costo = modelo.CostoUnitarioActual;
            entidad.producto_precio = modelo.PrecioUnitarioActual;
            entidad.producto_porciones = modelo.Porciones;
        }
    }
}
