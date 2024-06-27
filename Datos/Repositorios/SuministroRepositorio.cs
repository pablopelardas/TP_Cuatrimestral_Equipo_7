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

        public SuministroModelo ObtenerPorId(Guid id)
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

        public void Agregar(SuministroModelo suministro)
        {
            using (Entities db = new Entities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    SUMINISTRO suministroEntidad = Mappers.SuministroMapper.ModeloAEntidad(suministro);
                    db.SUMINISTROS.Add(suministroEntidad);

                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Modificar(SuministroModelo suministro)
        {
            Entities db = new Entities();
            try
            {
                SUMINISTRO entidad = Mappers.SuministroMapper.ModeloAEntidad(suministro);
                SUMINISTRO entidadDB = db.SUMINISTROS.Find(entidad.id_suministro);
                if (entidadDB == null)
                {
                    throw new Exception("Suministro no encontrado");
                }

                db.Entry(entidadDB).CurrentValues.SetValues(entidad);

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(Guid id)
        {
            Entities db = new Entities();
            try
            {
                SUMINISTRO entidad = db.SUMINISTROS.Find(id);
                db.SUMINISTROS.Remove(entidad);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}

