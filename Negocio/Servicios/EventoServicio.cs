using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servicios
{
    public class EventoServicio
    {
        Datos.Repositorios.EventoRepositorio eventoRepositorio = new Datos.Repositorios.EventoRepositorio();
        Datos.Repositorios.TipoEventoRepositorio tipoEventoRepositorio = new Datos.Repositorios.TipoEventoRepositorio();

        public List<TipoEventoModelo> ListarTipoDeEventos()
        {
            return tipoEventoRepositorio.Listar();
        }

        public List<EventoModelo> ListarEventos(bool soloActivos = true)
        {
            return eventoRepositorio.Listar(soloActivos);
        }

        public List<EventoModelo> ListarEventosPorCliente(Guid idCliente)
        {
            return eventoRepositorio.ListarPorCliente(idCliente);
        }

    }
}
