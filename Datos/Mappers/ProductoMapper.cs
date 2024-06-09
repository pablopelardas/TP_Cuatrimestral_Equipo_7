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
        internal static Dominio.Modelos.ProductoModelo EntidadAModelo(PRODUCTOS productoEntidad)
        {
            ProductoModelo producto = new ProductoModelo
            {
                Descripcion = productoEntidad.descripcion,
                HorasTrabajo = productoEntidad.horas_trabajo,
                IdProducto = productoEntidad.id_producto,
                Nombre = productoEntidad.nombre,
                Porciones = productoEntidad.porciones,
                TipoPrecio = productoEntidad.tipo_precio,
                ValorPrecio = productoEntidad.valor_precio,
            };  

            if (productoEntidad.CATEGORIAS != null)
            {
                producto.Categoria = CategoriaMapper.EntidadAModelo(productoEntidad.CATEGORIAS);
            }

            return producto;
        }

        internal static PRODUCTOS ModeloAEntidad(Dominio.Modelos.ProductoModelo productoModelo)
        {
            return new PRODUCTOS
            {
                CATEGORIAS = CategoriaMapper.ModeloAEntidad(productoModelo.Categoria),
                descripcion = productoModelo.Descripcion,
                horas_trabajo = productoModelo.HorasTrabajo,
                id_producto = productoModelo.IdProducto,
                nombre = productoModelo.Nombre,
                porciones = productoModelo.Porciones,
                tipo_precio = productoModelo.TipoPrecio,
                valor_precio = productoModelo.ValorPrecio,
            };          
        }

        internal static void ActualizarEntidad(ref PRODUCTOS productoEntidad, Dominio.Modelos.ProductoModelo productoModelo)
        {
            productoEntidad.descripcion = productoModelo.Descripcion;
            productoEntidad.horas_trabajo = productoModelo.HorasTrabajo;
            productoEntidad.nombre = productoModelo.Nombre;
            productoEntidad.porciones = productoModelo.Porciones;
            productoEntidad.tipo_precio = productoModelo.TipoPrecio;
            productoEntidad.valor_precio = productoModelo.ValorPrecio;
            productoEntidad.id_producto = productoModelo.IdProducto;
            productoEntidad.CATEGORIAS = CategoriaMapper.ModeloAEntidad(productoModelo.Categoria);
        }
    }
}
