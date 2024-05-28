using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    public class ProductoMapper
    {
        public static Dominio.Modelos.ProductoModelo EntidadAModelo(Entidades.ProductoEntidad productoEntidad)
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
            };
        }

        public static Entidades.ProductoEntidad ModeloAEntidad(Dominio.Modelos.ProductoModelo productoModelo)
        {
            return new Entidades.ProductoEntidad
            {
                id_categoria = productoModelo.Categoria.Id,
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
