using Datos.Repositorios;
using Dominio.Modelos;
using Datos.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Datos.Entidades;

namespace Datos.Mappers
{
    internal class ItemDetalleProductoMapper
    {
        public static ItemDetalleProductoModelo EntidadAModelo(ItemDetalleProductoEntidad entidad)
        {
            ItemDetalleProductoModelo itemModelo = new ItemDetalleProductoModelo();

            itemModelo.Cantidad = entidad.cantidad;

            if(entidad.receta != null)
                itemModelo.Receta = RecetaMapper.EntidadAModelo(entidad.receta, true);


            if(entidad.suministro != null)
                itemModelo.Suministro = SuministroMapper.EntidadAModelo(entidad.suministro, true);
            
            return itemModelo;
        }



        public static ItemDetalleProductoEntidad ModeloAEntidad(ItemDetalleProductoModelo item)
        {
            ItemDetalleProductoEntidad entidad = new ItemDetalleProductoEntidad();

            entidad.cantidad = item.Cantidad;
            if (item.Receta != null)
                entidad.receta = RecetaMapper.ModeloAEntidad(item.Receta);
            if (item.Suministro != null) 
                entidad.suministro = SuministroMapper.ModeloAEntidad(item.Suministro);

            return entidad;
        }
    }
}
