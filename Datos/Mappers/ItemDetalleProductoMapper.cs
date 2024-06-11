using Datos.EF;
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
        public static ItemDetalleProductoModelo EntidadAModelo(DETALLEPRODUCTO entidad)
        {
            ItemDetalleProductoModelo detalle = new ItemDetalleProductoModelo();

            if (entidad.SUMINISTRO != null)
            {
                detalle.Suministro = SuministroMapper.EntidadAModelo(entidad.SUMINISTRO);
            }

            if (entidad.RECETA != null)
            {
                detalle.Receta = RecetaMapper.EntidadAModelo(entidad.RECETA);
            }

            if (entidad.cantidad != 0)
            {
                detalle.Cantidad = (int)entidad.cantidad;
            }


            return detalle;

        }



        public static DETALLEPRODUCTO ModeloAEntidad(ItemDetalleProductoModelo item)
        {
            DETALLEPRODUCTO entidad = new DETALLEPRODUCTO();

            if (item.Suministro != null)
            {
                entidad.id_suministro = item.Suministro.IdSuministro;
            }

            if (item.Receta != null)
            {
                entidad.id_receta = item.Receta.IdReceta;
            }

            if (item.Cantidad != 0)
            {
                entidad.cantidad = item.Cantidad;
            }

            return entidad;
            
        }
    }
}
