using Datos.Entidades;
using Datos.Repositorios;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class ProductoMapper
    {
        internal static Dominio.Modelos.ProductoModelo EntidadAModelo(Entidades.ProductoEntidad productoEntidad, bool incluyeDetalle = false)
        {
            ProductoModelo producto = new ProductoModelo();
            ItemDetalleProductoRepositorio itemDetalleProductoRepositorio = new ItemDetalleProductoRepositorio();

            producto.IdProducto = productoEntidad.id_producto;
            producto.Nombre = productoEntidad.nombre;
            producto.Descripcion = productoEntidad.descripcion;
            producto.Porciones = productoEntidad.porciones;
            producto.HorasTrabajo = productoEntidad.horas_trabajo;
            producto.TipoPrecio = productoEntidad.tipo_precio;
            producto.ValorPrecio = productoEntidad.valor_precio;
            producto.Categoria = CategoriaMapper.EntidadAModelo(productoEntidad.categoria);
            if (incluyeDetalle)
                producto.Items = itemDetalleProductoRepositorio.ObtenerDetalleProducto(productoEntidad.id_producto);

            return producto;
        }

        internal static Entidades.ProductoEntidad ModeloAEntidad(Dominio.Modelos.ProductoModelo productoModelo)
        {
            return new Entidades.ProductoEntidad
            {
                categoria = CategoriaMapper.ModeloAEntidad(productoModelo.Categoria),
                descripcion = productoModelo.Descripcion,
                horas_trabajo = productoModelo.HorasTrabajo,
                id_producto = productoModelo.IdProducto,
                nombre = productoModelo.Nombre,
                porciones = productoModelo.Porciones,
                tipo_precio = productoModelo.TipoPrecio,
                valor_precio = productoModelo.ValorPrecio,
            };          
        }
    }
}
