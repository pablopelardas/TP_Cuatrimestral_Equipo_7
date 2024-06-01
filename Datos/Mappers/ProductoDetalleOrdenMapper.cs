using Datos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class ProductoDetalleOrdenMapper
    {
        internal static Dominio.Modelos.ProductoDetalleOrdenModelo EntidadAModelo(Datos.Entidades.ProductoDetalleOrdenEntidad entidad)
        {
            return new Dominio.Modelos.ProductoDetalleOrdenModelo
            {
                Producto = ProductoMapper.EntidadAModelo(entidad.producto),
                Cantidad = entidad.cantidad,
                CostoUnitarioActual = entidad.producto_costo,
                PrecioUnitarioActual = entidad.producto_precio,
                Porciones = entidad.producto_porciones
            };
        }

        internal static Datos.Entidades.ProductoDetalleOrdenEntidad ModeloAEntidad(Dominio.Modelos.ProductoDetalleOrdenModelo modelo)
        {
            return new Datos.Entidades.ProductoDetalleOrdenEntidad
            {
                producto = ProductoMapper.ModeloAEntidad(modelo.Producto),
                cantidad = modelo.Cantidad,
                producto_costo = modelo.CostoUnitarioActual,
                producto_precio = modelo.PrecioUnitarioActual,
                producto_porciones = modelo.Porciones
            };
        }
    }
}
