using Datos.EF;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class EventoRepositorio
    {
        public List<Dominio.Modelos.EventoModelo> Listar(bool soloActivos = true)
        {
            Entities db = new Entities();
            List<Dominio.Modelos.EventoModelo> eventos = new List<Dominio.Modelos.EventoModelo>();
            try
            {
                var query = db.EVENTOS.ToList();
                foreach (var item in query)
                {
                    EventoModelo evento = Mappers.EventoMapper.EntidadAModelo(item);
                    if (evento.Orden == null)
                    {
                        evento.Fecha = new DateTime(DateTime.Now.Year, evento.Fecha.Month, evento.Fecha.Day);
                    }
                    eventos.Add(evento);
                }
                // filter cancelled events
                if (soloActivos)
                    return eventos.Where(x => x.Orden == null || x.Orden.Estado.IdOrdenEstado != 5).ToList();
                // if event has no order, make event year current year

                return eventos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Dominio.Modelos.EventoModelo> ListarPorCliente(Guid idCliente, bool sinOrden = false)
        {
            Entities db = new Entities();
            List<Dominio.Modelos.EventoModelo> eventos = new List<Dominio.Modelos.EventoModelo>();
            try
            {
                var query = db.EVENTOS.Where(x => x.id_cliente == idCliente).ToList();
                foreach (var item in query)
                {
                    EventoModelo evento = Mappers.EventoMapper.EntidadAModelo(item);
                    if (evento.Orden == null)
                    {
                        evento.Fecha = new DateTime(DateTime.Now.Year, evento.Fecha.Month, evento.Fecha.Day);
                    }
                    eventos.Add(evento);
                }
                // filter cancelled events
                if (sinOrden)
                    return eventos.Where(x => x.Orden == null).ToList();
                // if event has no order, make event year current year

                return eventos;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
