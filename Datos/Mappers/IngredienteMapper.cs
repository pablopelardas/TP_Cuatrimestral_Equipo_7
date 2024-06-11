using Datos.EF;
using Datos.Repositorios;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    public class IngredienteMapper
    {
        public static IngredienteModelo EntidadAModelo(INGREDIENTE entidad)
        {
            IngredienteModelo ingrediente = new IngredienteModelo
            {
                IdIngrediente = entidad.id_ingrediente,
                Nombre = entidad.nombre,
                Cantidad = entidad.cantidad,
                Costo = entidad.costo,
                Proveedor = entidad.proveedor
            };
            if (entidad.UNIDAD_MEDIDA != null)
            {
                ingrediente.Unidad = UnidadMedidaMapper.EntidadAModelo(entidad.UNIDAD_MEDIDA);
            }
            return ingrediente;
        }

        public static List<IngredienteModelo> EntidadesAModelos(List<INGREDIENTE> entidades)
        {
            List<IngredienteModelo> ingredientes = new List<IngredienteModelo>();
            foreach (var entidad in entidades)
            {
                ingredientes.Add(EntidadAModelo(entidad));
            }
            return ingredientes;
        }

        public static INGREDIENTE ModeloAEntidad(IngredienteModelo ingrediente)
        {
            INGREDIENTE entidad = new INGREDIENTE
            {
                id_ingrediente = ingrediente.IdIngrediente,
                nombre = ingrediente.Nombre,
                cantidad = ingrediente.Cantidad,
                costo = ingrediente.Costo,
                proveedor = ingrediente.Proveedor
            };

            if (ingrediente.Unidad != null)
            {
                entidad.id_unidad = ingrediente.Unidad.Id;
            }

            return entidad;
        }
    }
}
