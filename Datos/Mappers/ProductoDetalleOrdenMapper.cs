using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class ProductoDetalleOrdenMapper
    {
        public static Dominio.Modelos.ProductoDetalleOrdenModelo EntidadAModelo(Datos.Entidades.ProductoDetalleOrdenEntidad entidad)
        {
            return new Dominio.Modelos.ProductoDetalleOrdenModelo
            {
                Producto = new Dominio.Modelos.ProductoModelo
                {
                    IdProducto = entidad.id_producto,
                    Nombre = entidad.producto_nombre,
                    Descripcion = entidad.descripcion,
                    Porciones = entidad.producto_porciones,
                    HorasTrabajo = entidad.horas_trabajo,
                    TipoPrecio = entidad.tipo_precio,
                    ValorPrecio = entidad.producto_precio,
                    Categoria = new Dominio.Modelos.CategoriaModelo
                    {
                        Id = entidad.id_categoria,
                        Nombre = entidad.nombre,
                        Tipo = entidad.tipo
                    }
                },
                Cantidad = entidad.cantidad,
                CostoUnitarioActual = entidad.producto_costo,
                PrecioUnitarioActual = entidad.producto_precio,
                Porciones = entidad.producto_porciones
            };
        }

        public static Datos.Entidades.ProductoDetalleOrdenEntidad ModeloAEntidad(Dominio.Modelos.ProductoDetalleOrdenModelo modelo)
        {
            return new Datos.Entidades.ProductoDetalleOrdenEntidad
            {
                id_producto = modelo.Producto.IdProducto,
                producto_nombre = modelo.Producto.Nombre,
                descripcion = modelo.Producto.Descripcion,
                producto_porciones = modelo.Porciones,
                horas_trabajo = modelo.Producto.HorasTrabajo,
                tipo_precio = modelo.Producto.TipoPrecio,
                producto_precio = modelo.PrecioUnitarioActual,
                producto_costo = modelo.CostoUnitarioActual,
                id_categoria = modelo.Producto.Categoria.Id,
                tipo = modelo.Producto.Categoria.Tipo,
                nombre = modelo.Producto.Categoria.Nombre,
                cantidad = modelo.Cantidad
            };
        }
    }
}
