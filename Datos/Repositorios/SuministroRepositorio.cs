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
    public class SuministroRepositorio
    {
        public List<SuministroModelo> Listar()
        {
            Entities db = new Entities();
            List<SuministroModelo> suministros = new List<SuministroModelo>();

            try
            {
                var query = from s in db.SUMINISTROS
                            select s;

                foreach (var item in query)
                {
                    suministros.Add(Mappers.SuministroMapper.EntidadAModelo(item));
                }

                return suministros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SuministroModelo ObtenerPorId(int id)
        {
            Entities db = new Entities();

            try
            {
                var query = from s in db.SUMINISTROS
                            where s.id_suministro == id
                            select s;

                return Mappers.SuministroMapper.EntidadAModelo(query.FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
