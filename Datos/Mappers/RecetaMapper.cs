using Datos;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class RecetaMapper
    {

        internal static Dominio.Modelos.RecetaModelo EntidadAModelo(RECETA recetaEntidad)
        {
            RecetaModelo receta = new RecetaModelo
            {
                Descripcion = recetaEntidad.descripcion,
                IdReceta = recetaEntidad.id_receta,
                Nombre = recetaEntidad.nombre,
            };

            if (recetaEntidad.precio_personalizado != null)
            {
                receta.PrecioPersonalizado = (decimal)recetaEntidad.precio_personalizado;
            }

            if (recetaEntidad.CATEGORIA != null)
            {
                receta.Categoria = CategoriaMapper.EntidadAModelo(recetaEntidad.CATEGORIA);
            }

            if (recetaEntidad.DETALLE_RECETAS != null)
            {
                receta.DetalleRecetas = new List<IngredienteDetalleRecetaModelo>();
                foreach (var detalleReceta in recetaEntidad.DETALLE_RECETAS)
                {
                    receta.DetalleRecetas.Add(IngredienteDetalleRecetaMapper.EntidadAModelo(detalleReceta));
                }
            }


            return receta;
        }

        internal static RECETA ModeloAEntidad(Dominio.Modelos.RecetaModelo recetaModelo)
        {
            RECETA entidad = new RECETA
            {
                descripcion = recetaModelo.Descripcion,
                id_receta = recetaModelo.IdReceta,
                nombre = recetaModelo.Nombre,
            };

            if (recetaModelo.PrecioPersonalizado != 0)
            {
                entidad.precio_personalizado = recetaModelo.PrecioPersonalizado;
            }

            if (recetaModelo.Categoria != null)
            {
                entidad.id_categoria = recetaModelo.Categoria.Id;
            }

            if (recetaModelo.DetalleRecetas != null)
            {
                entidad.DETALLE_RECETAS = new List<DETALLE_RECETA>();
                foreach (var detalleReceta in recetaModelo.DetalleRecetas)
                {
                    entidad.DETALLE_RECETAS.Add(IngredienteDetalleRecetaMapper.ModeloAEntidad(detalleReceta));
                }
            }

            return entidad;
        }
    }
}
