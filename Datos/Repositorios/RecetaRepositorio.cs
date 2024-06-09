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
    public class RecetaRepositorio
    {

        public List<RecetaModelo> Listar()
        {
            Entities db = new Entities();
            List<RecetaModelo> recetas = new List<RecetaModelo>();
            try
            {
                var query = from r in db.RECETAS
                            select new
                            {
                                Receta = r
                            };

                foreach (var row in query)
                {
                    recetas.Add(Mappers.RecetaMapper.EntidadAModelo(row.Receta));
                }

                return recetas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public RecetaModelo ObtenerPorId(int id)
        {
            Entities db = new Entities();
            try
            {
                var query = from r in db.RECETAS
                            where r.id_receta == id
                            select new
                            {
                                Receta = r
                            };

                var row = query.FirstOrDefault();
                if (row != null)
                {
                    return Mappers.RecetaMapper.EntidadAModelo(row.Receta);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void Guardar(RecetaModelo receta)
        //{
        //    Entities db = new Entities();
        //    using (var transaction = db.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            RECETAS recetaEntidad = Mappers.RecetaMapper.ModeloAEntidad(receta);
        //            db.RECETAS.Add(recetaEntidad);
        //            db.SaveChanges();

        //            foreach (var detalleReceta in receta.DetalleRecetas)
        //            {
        //                detalleReceta.IdReceta = recetaEntidad.id_receta;
        //                DETALLE_RECETAS detalleRecetaEntidad = Mappers.IngredienteDetalleRecetaMapper.ModeloAEntidad(detalleReceta);
        //                db.DETALLE_RECETAS.Add(detalleRecetaEntidad);
        //            }

        //            db.SaveChanges();
        //            transaction.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            throw ex;
        //        }
        //    }
        //}

        //public void Actualizar(RecetaModelo receta)
        //{
        //    Entities db = new Entities();
        //    using (var transaction = db.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            RECETAS recetaEntidad = Mappers.RecetaMapper.ModeloAEntidad(receta);
        //            db.Entry(recetaEntidad).State = EntityState.Modified;
        //            db.SaveChanges();

        //            foreach (var detalleReceta in receta.DetalleRecetas)
        //            {
        //                DETALLE_RECETAS detalleRecetaEntidad = Mappers.IngredienteDetalleRecetaMapper.ModeloAEntidad(detalleReceta);
        //                db.Entry(detalleRecetaEntidad).State = EntityState.Modified;
        //            }

        //            db.SaveChanges();
        //            transaction.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            throw ex;
        //        }
        //    }
        //}

        //public void Eliminar(int id)
        //{
        //}

    }
}
