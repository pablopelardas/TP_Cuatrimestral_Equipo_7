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
    public class HistoricoRepositorio
    {
        public HistoricoModelo Agregar(HistoricoModelo historico)
        {
            Entities db = new Entities();
            try
            {
                db.HISTORICO_ENTIDADES.Add(Mappers.HistoricoMapper.ModeloAEntidad(historico));
                db.SaveChanges();
                return historico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoModelo> ListarPorEntidad(Guid entidadId)
        {
            Entities db = new Entities();
            try
            {
                // = db.ORDENES.OrderByDescending(x => x.EVENTO.fecha).ToList();
                List<HistoricoModelo> historicos = new List<HistoricoModelo>();
                List<HISTORICOENTIDAD> historicosEntidad = db.HISTORICO_ENTIDADES.OrderByDescending(x => x.fecha).Where(x => x.id_entidad == entidadId).ToList();
                foreach (var historicoEntidad in historicosEntidad)
                {
                    HistoricoModelo historico = Mappers.HistoricoMapper.EntidadAModelo(historicoEntidad);
                    historicos.Add(historico);
                }

                return historicos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}