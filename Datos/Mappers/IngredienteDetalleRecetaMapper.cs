using Datos.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    public class IngredienteDetalleRecetaMapper
    {
        internal static Dominio.Modelos.IngredienteDetalleRecetaModelo EntidadAModelo(DETALLERECETA entidad)
        {
            Dominio.Modelos.IngredienteDetalleRecetaModelo modelo = new Dominio.Modelos.IngredienteDetalleRecetaModelo
            {
                Cantidad = decimal.Round(entidad.cantidad, 2)
            };

            if (entidad.INGREDIENTE != null)
            {
                modelo.Ingrediente = IngredienteMapper.EntidadAModelo(entidad.INGREDIENTE);
            }

            return modelo;
        }

        internal static DETALLERECETA ModeloAEntidad(Dominio.Modelos.IngredienteDetalleRecetaModelo modelo)
        {
            return new DETALLERECETA
            {
                cantidad = modelo.Cantidad,
                id_ingrediente = modelo.Ingrediente.IdIngrediente,
                id_receta = modelo.IdReceta,
            };

        }
    }
}
