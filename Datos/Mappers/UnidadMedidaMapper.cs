using Datos.Repositorios;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    public class UnidadMedidaMapper
    {
        public static UnidadMedidaModelo EntidadAModelo(Entidades.UnidadMedidaEntidad entidad)
        {
            UnidadMedidaModelo unidad = new UnidadMedidaModelo();

            unidad.Id = entidad.id_unidad;
            unidad.Nombre = entidad.nombre;
            unidad.Abreviatura = entidad.abreviatura;

            return unidad;
        }

        public static Entidades.UnidadMedidaEntidad ModeloAEntidad(UnidadMedidaModelo unidad)
        {
            Entidades.UnidadMedidaEntidad entidad = new Entidades.UnidadMedidaEntidad();
            AccesoDatos datos = new AccesoDatos();

            entidad.id_unidad = unidad.Id;
            entidad.nombre = unidad.Nombre;
            entidad.abreviatura = unidad.Abreviatura;
            
            return entidad;
        }
    }
}
