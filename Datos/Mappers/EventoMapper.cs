using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class EventoMapper
    {
        public static Dominio.Modelos.EventoModelo EntidadAModelo(EVENTOS entidad)
        {
            Dominio.Modelos.EventoModelo modelo = new Dominio.Modelos.EventoModelo
            {
                IdEvento = entidad.id_evento,
                Fecha = entidad.fecha,
                //TipoEvento = TipoEventoMapper.EntidadAModelo(entidad.TIPOS_EVENTOS),
                //TipoEvento = TipoEventoMapper.EntidadAModelo(),
                //Cliente = ContactoMapper.EntidadAModelo(entidad.cliente),
                //Orden = OrdenMapper.EntidadAModelo(entidad.orden)

            };
            if (entidad.TIPOS_EVENTOS != null)
            {
                modelo.TipoEvento = TipoEventoMapper.EntidadAModelo(entidad.TIPOS_EVENTOS);
            }
            if (entidad.CONTACTOS != null)
            {
                modelo.Cliente = ContactoMapper.EntidadAModelo(entidad.CONTACTOS);
            }
            if (entidad.ORDENES != null)
            {
                //modelo.Orden = OrdenMapper.EntidadAModelo(entidad.ORDENES);
            }

            return modelo;
        }

        public static EVENTOS ModeloAEntidad(Dominio.Modelos.EventoModelo modelo)
        {
            EVENTOS entidad = new EVENTOS
            {
                id_evento = modelo.IdEvento,
                fecha = modelo.Fecha,
                //tipo_evento = TipoEventoMapper.ModeloAEntidad(modelo.TipoEvento),
                //cliente = ContactoMapper.ModeloAEntidad(modelo.Cliente),
                //orden = OrdenMapper.ModeloAEntidad(modelo.Orden)
            };

            if (modelo.TipoEvento != null)
            {
                entidad.TIPOS_EVENTOS = Mappers.TipoEventoMapper.ModeloAEntidad(modelo.TipoEvento);
            }

            if (modelo.Cliente != null)
            {
                entidad.CONTACTOS = Mappers.ContactoMapper.ModeloAEntidad(modelo.Cliente);
            }

            if (modelo.Orden != null)
            {
                entidad.ORDENES = Mappers.OrdenMapper.ModeloAEntidad(modelo.Orden);
            }

            return entidad;
        }
    }
}