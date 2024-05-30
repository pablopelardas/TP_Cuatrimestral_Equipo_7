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
        public static IngredienteModelo EntidadAModelo(Entidades.IngredienteEntidad entidad)
        {
            IngredienteModelo ingrediente = new IngredienteModelo();
            UnidadMedidaRepositorio unidad = new UnidadMedidaRepositorio();

            ingrediente.IdIngrediente = entidad.id_ingrediente;
            ingrediente.Nombre = entidad.nombre;
            ingrediente.Cantidad = entidad.cantidad;
            ingrediente.Unidad= unidad.ObtenerPorId(entidad.unidad.id_unidad);
            ingrediente.Costo = entidad.costo;
            ingrediente.Proveedor = entidad.proveedor;

            return ingrediente;
        }

        public static Entidades.IngredienteEntidad ModeloAEntidad(IngredienteModelo ingrediente)
        {
            Entidades.IngredienteEntidad entidad = new Entidades.IngredienteEntidad();

            entidad.id_ingrediente = ingrediente.IdIngrediente;
            entidad.nombre = ingrediente.Nombre;
            entidad.cantidad = ingrediente.Cantidad;
            entidad.unidad = UnidadMedidaMapper.ModeloAEntidad(ingrediente.Unidad);
            entidad.costo = ingrediente.Costo;
            entidad.proveedor = ingrediente.Proveedor;

            return entidad;
        }
    }
}
