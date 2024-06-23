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
                    eventos.Add(Mappers.EventoMapper.EntidadAModelo(item));
                }
                // filter cancelled events
                if (soloActivos)
                    return eventos.Where(x => x.Orden.Estado.IdOrdenEstado != 5).ToList();
                return eventos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
