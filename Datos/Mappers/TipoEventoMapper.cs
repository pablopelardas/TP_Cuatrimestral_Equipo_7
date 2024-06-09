using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class TipoEventoMapper
    {
        public static Dominio.Modelos.TipoEventoModelo EntidadAModelo(TIPOS_EVENTOS entidad)
        {
            return new Dominio.Modelos.TipoEventoModelo
            {
                IdTipoEvento = entidad.id_tipo_evento,
                Nombre = entidad.nombre
            };
        }

        public static TIPOS_EVENTOS ModeloAEntidad(Dominio.Modelos.TipoEventoModelo modelo)
        {
            return new TIPOS_EVENTOS
            {
                id_tipo_evento = modelo.IdTipoEvento,
                nombre = modelo.Nombre
            };
        }
    }
}
