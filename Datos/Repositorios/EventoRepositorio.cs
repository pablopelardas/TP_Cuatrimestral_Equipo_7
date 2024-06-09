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
        public List<Dominio.Modelos.EventoModelo> Listar()
        {
            Entities db = new Entities();
            List<Dominio.Modelos.EventoModelo> eventos = new List<Dominio.Modelos.EventoModelo>();
            try
            {
                var query = from e in db.EVENTOS
                            select e;
                foreach (var item in query)
                {
                    eventos.Add(Mappers.EventoMapper.EntidadAModelo(item));
                }
                return eventos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
