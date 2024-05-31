using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class ProductoMapper
    {
        internal static Dominio.Modelos.ProductoModelo EntidadAModelo(Entidades.ProductoEntidad productoEntidad)
        {
            return new Dominio.Modelos.ProductoModelo
            {
                IdProducto = productoEntidad.id_producto,
                Nombre = productoEntidad.nombre,
                Descripcion = productoEntidad.descripcion,
                Porciones = productoEntidad.porciones,
                HorasTrabajo = productoEntidad.horas_trabajo,
                TipoPrecio = productoEntidad.tipo_precio,
                ValorPrecio = productoEntidad.valor_precio,
                Categoria = CategoriaMapper.EntidadAModelo(productoEntidad.categoria),
            };
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
