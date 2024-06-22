using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Modelos;

namespace Negocio.Servicios
{
    public class HistoricoServicio
    {
        private Datos.Repositorios.HistoricoRepositorio historicoRepositorio;

        public HistoricoServicio()
        {
            historicoRepositorio = new Datos.Repositorios.HistoricoRepositorio();
        }

        public List<Dominio.Modelos.HistoricoModelo> ListarPorEntidad(Guid id)
        {
            return historicoRepositorio.ListarPorEntidad(id);
        }

        public HistoricoModelo GuardarHistorico(HistoricoModelo historico)
        {
            return historicoRepositorio.Agregar(historico);
        }
        
        private HistoricoModelo GenerarHistorico(Guid id, string justificacion)
        {
            return new HistoricoModelo
            {
                IdEntidad = id,
                Justificacion = justificacion,
                Fecha = DateTime.Now,
            };
        }
        
        public HistoricoModelo GeneraryGuardarHistorico(Guid id , string justificacion)
        {
           return GuardarHistorico(GenerarHistorico(id, justificacion));
        }
    }
}