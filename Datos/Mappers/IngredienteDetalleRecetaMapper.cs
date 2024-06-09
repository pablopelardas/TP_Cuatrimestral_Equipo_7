using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    public class IngredienteDetalleRecetaMapper
    {
        internal static Dominio.Modelos.IngredienteDetalleRecetaModelo EntidadAModelo(DETALLE_RECETAS entidad)
        {
            Dominio.Modelos.IngredienteDetalleRecetaModelo modelo = new Dominio.Modelos.IngredienteDetalleRecetaModelo
            {
                Cantidad = entidad.cantidad != 0 ? entidad.cantidad : 1
            };

            if (entidad.INGREDIENTES != null)
            {
                modelo.Ingrediente = IngredienteMapper.EntidadAModelo(entidad.INGREDIENTES);
            }

            return modelo;
        }

        internal static DETALLE_RECETAS ModeloAEntidad(Dominio.Modelos.IngredienteDetalleRecetaModelo modelo)
        {
            DETALLE_RECETAS entidad = new DETALLE_RECETAS
            {
                cantidad = modelo.Cantidad != 0 ? modelo.Cantidad : 1
            };

            if (modelo.Ingrediente != null)
            {
                entidad.INGREDIENTES = IngredienteMapper.ModeloAEntidad(modelo.Ingrediente);
            }

            return entidad;
        }
    }
}
