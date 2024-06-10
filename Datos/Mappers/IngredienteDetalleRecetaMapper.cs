using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    public class IngredienteDetalleRecetaMapper
    {
        internal static Dominio.Modelos.IngredienteDetalleRecetaModelo EntidadAModelo(DETALLE_RECETA entidad)
        {
            Dominio.Modelos.IngredienteDetalleRecetaModelo modelo = new Dominio.Modelos.IngredienteDetalleRecetaModelo
            {
                Cantidad = entidad.cantidad != 0 ? entidad.cantidad : 1
            };

            if (entidad.INGREDIENTE != null)
            {
                modelo.Ingrediente = IngredienteMapper.EntidadAModelo(entidad.INGREDIENTE);
            }

            return modelo;
        }

        internal static DETALLE_RECETA ModeloAEntidad(Dominio.Modelos.IngredienteDetalleRecetaModelo modelo)
        {
            DETALLE_RECETA entidad = new DETALLE_RECETA
            {
                cantidad = modelo.Cantidad != 0 ? modelo.Cantidad : 1
            };

            if (modelo.Ingrediente != null)
            {
                entidad.id_ingrediente = modelo.Ingrediente.IdIngrediente;
            }

            return entidad;
        }
    }
}
