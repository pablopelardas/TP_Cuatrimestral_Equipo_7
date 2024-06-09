using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class TipoEventoRepositorio
    {
        public List<Dominio.Modelos.TipoEventoModelo> Listar()
        {
            Entities db = new Entities();
            try
            {
                return db.TIPOS_EVENTOS.Select(x => new Dominio.Modelos.TipoEventoModelo
                {
                    IdTipoEvento = x.id_tipo_evento,
                    Nombre = x.nombre,
                }).ToList();

            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
