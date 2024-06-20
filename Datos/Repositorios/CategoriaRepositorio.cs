using Datos.EF;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class CategoriaRepositorio
    {
        public List<CategoriaModelo> Listar(string tipo = "")
        {
            Entities db = new Entities();
            List<CategoriaModelo> categorias = new List<CategoriaModelo>();
            List<CATEGORIA> categoriasdb = string.IsNullOrEmpty(tipo) ? db.CATEGORIAS.ToList() : db.CATEGORIAS.Where(c => c.tipo == tipo).ToList();
            try
            {
                foreach (CATEGORIA categoria in categoriasdb)
                {
                    categorias.Add(Mappers.CategoriaMapper.EntidadAModelo(categoria));
                }
                return categorias;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CategoriaModelo ObtenerPorId(Guid id)
        {
            Entities db = new Entities();
            try
            {
                CATEGORIA categoria = db.CATEGORIAS.Find(id);
                return Mappers.CategoriaMapper.EntidadAModelo(categoria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Agregar(CategoriaModelo categoria)
        {
            Entities db = new Entities();
            try
            {
                CATEGORIA entidad = Mappers.CategoriaMapper.ModeloAEntidad(categoria);
                db.CATEGORIAS.Add(entidad);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(CategoriaModelo categoria)
        {
            Entities db = new Entities();
            try
            {
                CATEGORIA entidad = Mappers.CategoriaMapper.ModeloAEntidad(categoria);
                CATEGORIA entidadDB = db.CATEGORIAS.Find(entidad.id_categoria);
                if (entidadDB == null)
                {
                    throw new Exception("Categoria no encontrada");
                }

                db.Entry(entidadDB).CurrentValues.SetValues(entidad);

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(int id)
        {
            Entities db = new Entities();
            try
            {
                CATEGORIA entidad = db.CATEGORIAS.Find(id);
                db.CATEGORIAS.Remove(entidad);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
