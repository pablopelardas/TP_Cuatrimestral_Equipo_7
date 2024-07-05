using Datos.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    internal class EventoMapper
    {
        public static Dominio.Modelos.EventoModelo EntidadAModelo(EVENTO entidad, bool ListarOrdenes = true, bool ListarContactos = false)
        {
            Dominio.Modelos.EventoModelo modelo = new Dominio.Modelos.EventoModelo
            {
                IdEvento = entidad.id_evento,
                Fecha = entidad.fecha,
                Descripcion = entidad.descripcion

            };
            if (entidad.TIPO_EVENTO != null)
            {
                modelo.TipoEvento = TipoEventoMapper.EntidadAModelo(entidad.TIPO_EVENTO);
            }
            if (entidad.CLIENTE != null && ListarContactos)
            {
                modelo.Cliente = ContactoMapper.EntidadAModelo(entidad.CLIENTE);
            }
            if (entidad.ORDENES != null && entidad.ORDENES.Count() > 0 && ListarOrdenes)
            {
                modelo.Orden = OrdenMapper.EntidadesAModelos(entidad.ORDENES.ToList()).First();
            }

            return modelo;
        }

        public static EVENTO ModeloAEntidad(Dominio.Modelos.EventoModelo modelo)
        {
            EVENTO entidad = new EVENTO
            {
                id_evento = modelo.IdEvento,
                fecha = modelo.Fecha,
                id_cliente = modelo.Cliente.Id,
                id_tipo_evento = modelo.TipoEvento.IdTipoEvento,
                descripcion = modelo.Descripcion
            };

            return entidad;
        }

        internal static void ActualizarEntidad(ref EVENTO evento, Dominio.Modelos.EventoModelo modelo)
        {
            evento.id_evento = modelo.IdEvento;
            evento.fecha = modelo.Fecha;
            evento.id_cliente = modelo.Cliente.Id;
            evento.id_tipo_evento = modelo.TipoEvento.IdTipoEvento;
            evento.descripcion = modelo.Descripcion;
        }
    }
}