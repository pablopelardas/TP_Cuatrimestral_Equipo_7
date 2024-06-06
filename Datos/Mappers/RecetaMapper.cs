using Datos;
using Datos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class RecetaMapper
    {

        internal static Dominio.Modelos.RecetaModelo EntidadAModelo(Entidades.RecetaEntidad recetaEntidad, bool incluyeDetalle = false)
        {
            Repositorios.IngredienteDetalleRecetaRepositorio ingredienteDetalleRecetaRepositorio = new Repositorios.IngredienteDetalleRecetaRepositorio();
            Dominio.Modelos.RecetaModelo receta = new Dominio.Modelos.RecetaModelo();

            receta.IdReceta = recetaEntidad.id_receta;
            receta.Nombre = recetaEntidad.nombre;
            receta.Descripcion = recetaEntidad.descripcion;
            receta.PrecioPersonalizado = recetaEntidad.precio_personalizado;
            receta.Categoria = CategoriaMapper.EntidadAModelo(recetaEntidad.categoria);


            if(incluyeDetalle) receta.DetalleRecetas = ingredienteDetalleRecetaRepositorio.ObtenerDetallePorReceta(recetaEntidad.id_receta);

            return receta;
        }

        internal static Entidades.RecetaEntidad ModeloAEntidad(Dominio.Modelos.RecetaModelo recetaModelo)
        {
            return new Entidades.RecetaEntidad
            {
                id_receta = recetaModelo.IdReceta,
                nombre = recetaModelo.Nombre,
                descripcion = recetaModelo.Descripcion,
                precio_personalizado = recetaModelo.PrecioPersonalizado,
                categoria = CategoriaMapper.ModeloAEntidad(recetaModelo.Categoria)
            };
        }
    }
}
