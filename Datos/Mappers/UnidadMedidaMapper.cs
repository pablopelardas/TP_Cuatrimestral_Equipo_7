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
    public class UnidadMedidaMapper
    {
        public static UnidadMedidaModelo EntidadAModelo(UNIDAD_MEDIDA entidad)
        {
            return new UnidadMedidaModelo
            {
                Id = entidad.id_unidad,
                Nombre = entidad.nombre,
                Abreviatura = entidad.abreviatura
            };
        }

        public static UNIDAD_MEDIDA ModeloAEntidad(UnidadMedidaModelo unidad)
        {
            return new UNIDAD_MEDIDA
            {
                id_unidad = unidad.Id,
                nombre = unidad.Nombre,
                abreviatura = unidad.Abreviatura
            };
        }
    }
}
