using Datos.EF;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class RecetaRepositorio
    {

        public List<RecetaModelo> Listar(string categoria = "")
        {
            Entities db = new Entities();
            List<RecetaModelo> recetas = new List<RecetaModelo>();
            Guid idCategoria;
            idCategoria = Guid.TryParse(categoria, out idCategoria) ? idCategoria : Guid.Empty;
            try
            {
                List<RECETA> recetasEntidad = idCategoria == Guid.Empty ? db.RECETAS.ToList() : db.RECETAS.Where(c => c.id_categoria == idCategoria).ToList();
                recetasEntidad.ForEach(r => recetas.Add(Mappers.RecetaMapper.EntidadAModelo(r)));

                return recetas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RecetaModelo ObtenerPorId(Guid id)
        {
            Entities db = new Entities();
            try
            {
                RECETA recetaEntidad = db.RECETAS.Find(id);
                if (recetaEntidad != null)
                {
                    return Mappers.RecetaMapper.EntidadAModelo(recetaEntidad);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Agregar(RecetaModelo receta)
        {
            using (Entities db = new Entities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    RECETA recetaEntidad = Mappers.RecetaMapper.ModeloAEntidad(receta);
                    db.RECETAS.Add(recetaEntidad);
                    db.SaveChanges();
                    //foreach (var detalleReceta in receta.DetalleRecetas)
                    //{
                    //    detalleReceta.IdReceta = recetaEntidad.id_receta;
                    //    DETALLERECETA detalleRecetaEntidad = Mappers.IngredienteDetalleRecetaMapper.ModeloAEntidad(detalleReceta);
                    //    db.DETALLE_RECETAS.Add(detalleRecetaEntidad);
                    //}

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

        public void Modificar(RecetaModelo receta)
        {
            using (Entities db = new Entities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    RECETA recetaEntidad = Mappers.RecetaMapper.ModeloAEntidad(receta);
                    RECETA RecetaEntidadDB = db.RECETAS.Find(recetaEntidad.id_receta);

                    if (RecetaEntidadDB == null)
                        throw new Exception("Receta no encontrada");

                    db.Entry(RecetaEntidadDB).CurrentValues.SetValues(recetaEntidad);
                    db.SaveChanges();

                    List<DETALLERECETA> detallesDB = db.DETALLE_RECETAS.Where(x => x.id_receta == receta.IdReceta).ToList();
                    foreach (var detalle in detallesDB)
                    {
                        db.DETALLE_RECETAS.Remove(detalle);
                    }


                    foreach (var detalleReceta in receta.DetalleRecetas)
                    {
                        DETALLERECETA detalleRecetaEntidad = Mappers.IngredienteDetalleRecetaMapper.ModeloAEntidad(detalleReceta);
                        detalleRecetaEntidad.id_receta = receta.IdReceta;
                        db.DETALLE_RECETAS.Add(detalleRecetaEntidad);
                    }

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

        public void Eliminar(Guid id)
        {
            using (Entities db = new Entities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    RECETA recetaEntidad = db.RECETAS.Find(id);
                    db.RECETAS.Remove(recetaEntidad);
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex) { transaction.Rollback(); throw ex; }
            }
        }

    }
}
