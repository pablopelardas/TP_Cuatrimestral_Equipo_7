using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class EventoMapper
    {
        public static Dominio.Modelos.EventoModelo EntidadAModelo(Datos.Entidades.EventoEntidad entidad)
        {
            return new Dominio.Modelos.EventoModelo
            {
                IdEvento = entidad.id_evento,
                Fecha = entidad.fecha,
                TipoEvento = TipoEventoMapper.EntidadAModelo(entidad.tipo_evento),
                Cliente = ContactoMapper.EntidadAModelo(entidad.cliente),
                Orden = OrdenMapper.EntidadAModelo(entidad.orden)

            };
        }

        public static Datos.Entidades.EventoEntidad ModeloAEntidad(Dominio.Modelos.EventoModelo modelo)
        {
            return new Datos.Entidades.EventoEntidad
            {
                id_evento = modelo.IdEvento,
                fecha = modelo.Fecha,
                tipo_evento = TipoEventoMapper.ModeloAEntidad(modelo.TipoEvento),
                cliente = ContactoMapper.ModeloAEntidad(modelo.Cliente),
                orden = OrdenMapper.ModeloAEntidad(modelo.Orden)
            };
        }
    }
}