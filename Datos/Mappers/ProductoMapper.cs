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
                Codigo = productoEntidad.codigo,
                Nombre = productoEntidad.nombre,
                Descripcion = productoEntidad.descripcion,
                Categoria = productoEntidad.categoria,
                Porciones = productoEntidad.porciones,
                Horas = productoEntidad.horas,
                Recetas = productoEntidad.recetas,
                Suministros = productoEntidad.suministros,
                Costo = productoEntidad.costo,
                CostoPorPorcion = productoEntidad.costo_porcion,
                PrecioVenta = productoEntidad.precio_venta,
                TarifaImpuesto = productoEntidad.tarifa_impuesto,
                GananciaNeta = productoEntidad.ganancia_neta,
                //Imagenes =
            };
        }

        public static Entidades.ProductoEntidad ModeloAEntidad(Dominio.Modelos.ProductoModelo productoModelo)
        {
            return new Entidades.ProductoEntidad
            {
                codigo = productoModelo.Codigo,
                nombre = productoModelo.Nombre,
                descripcion = productoModelo.Descripcion,
                categoria = productoModelo.Categoria,
                porciones = productoModelo.Porciones,
                horas = productoModelo.Horas,
                recetas = productoModelo.Recetas,
                suministros = productoModelo.Suministros,
                costo = productoModelo.Costo,
                costo_porcion = productoModelo.CostoPorPorcion,
                precio_venta = productoModelo.PrecioVenta,
                tarifa_impuesto = productoModelo.TarifaImpuesto,
                ganancia_neta = productoModelo.GananciaNeta
            };          
        }
    }
}
