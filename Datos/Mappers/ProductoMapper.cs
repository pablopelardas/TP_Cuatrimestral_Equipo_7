using Datos.EF;
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
        internal static Dominio.Modelos.ProductoModelo EntidadAModelo(PRODUCTO productoEntidad)
        {
            ProductoModelo producto = new ProductoModelo
            {
                Descripcion = productoEntidad.descripcion,
                HorasTrabajo = decimal.Round(productoEntidad.horas_trabajo, 2),
                IdProducto = productoEntidad.id_producto,
                Nombre = productoEntidad.nombre,
                Porciones = productoEntidad.porciones,
                TipoPrecio = productoEntidad.tipo_precio,
                ValorPrecio = decimal.Round(productoEntidad.valor_precio, 2)
            };  

            if (productoEntidad.CATEGORIA != null)
            {
                producto.Categoria = CategoriaMapper.EntidadAModelo(productoEntidad.CATEGORIA);
            }

            return producto;
        }

        internal static PRODUCTO ModeloAEntidad(Dominio.Modelos.ProductoModelo productoModelo)
        {
            return new PRODUCTO
            {
                descripcion = productoModelo.Descripcion,
                horas_trabajo = productoModelo.HorasTrabajo,
                id_producto = productoModelo.IdProducto,
                nombre = productoModelo.Nombre,
                porciones = productoModelo.Porciones,
                tipo_precio = productoModelo.TipoPrecio,
                valor_precio = productoModelo.ValorPrecio,
                id_categoria = productoModelo.Categoria.Id
            };          
        }

        internal static void ActualizarEntidad(ref PRODUCTO productoEntidad, Dominio.Modelos.ProductoModelo productoModelo)
        {
            productoEntidad.descripcion = productoModelo.Descripcion;
            productoEntidad.horas_trabajo = productoModelo.HorasTrabajo;
            productoEntidad.nombre = productoModelo.Nombre;
            productoEntidad.porciones = productoModelo.Porciones;
            productoEntidad.tipo_precio = productoModelo.TipoPrecio;
            productoEntidad.valor_precio = productoModelo.ValorPrecio;
            productoEntidad.id_producto = productoModelo.IdProducto;
            productoEntidad.id_categoria = productoModelo.Categoria.Id;
        }
    }
}
