using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    public class IngredienteDetalleRecetaMapper
    {
        internal static Dominio.Modelos.IngredienteDetalleRecetaModelo EntidadAModelo(Datos.Entidades.IngredienteDetalleRecetaEntidad entidad)
        {
            return new Dominio.Modelos.IngredienteDetalleRecetaModelo
            {
                Ingrediente = IngredienteMapper.EntidadAModelo(entidad.ingrediente),
                Cantidad = entidad.cantidad
            };
        }

        internal static Datos.Entidades.IngredienteDetalleRecetaEntidad ModeloAEntidad(Dominio.Modelos.IngredienteDetalleRecetaModelo modelo)
        {
            return new Datos.Entidades.IngredienteDetalleRecetaEntidad
            {
                ingrediente = IngredienteMapper.ModeloAEntidad(modelo.Ingrediente),
                cantidad = modelo.Cantidad
            };
        }
    }
}
