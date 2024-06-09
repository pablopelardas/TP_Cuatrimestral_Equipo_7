using Datos.Repositorios;
using Dominio.Modelos;
using Datos.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Datos.Mappers
{
    internal class ItemDetalleProductoMapper
    {
        public static ItemDetalleProductoModelo EntidadAModelo(DETALLE_PRODUCTOS entidad)
        {
            ItemDetalleProductoModelo detalle = new ItemDetalleProductoModelo();

            if (entidad.SUMINISTROS != null)
            {
                detalle.Suministro = SuministroMapper.EntidadAModelo(entidad.SUMINISTROS);
            }

            if (entidad.RECETAS != null)
            {
                detalle.Receta = RecetaMapper.EntidadAModelo(entidad.RECETAS);
            }

            if (entidad.cantidad != 0)
            {
                detalle.Cantidad = (int)entidad.cantidad;
            }


            return detalle;

        }



        public static DETALLE_PRODUCTOS ModeloAEntidad(ItemDetalleProductoModelo item)
        {
            DETALLE_PRODUCTOS entidad = new DETALLE_PRODUCTOS();

            if (item.Suministro != null)
            {
                entidad.SUMINISTROS = SuministroMapper.ModeloAEntidad(item.Suministro);
            }

            if (item.Receta != null)
            {
                entidad.RECETAS = RecetaMapper.ModeloAEntidad(item.Receta);
            }

            if (item.Cantidad != 0)
            {
                entidad.cantidad = item.Cantidad;
            }

            return entidad;
            
        }
    }
}
