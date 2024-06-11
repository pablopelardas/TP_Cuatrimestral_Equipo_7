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
    public class UnidadMedidaRepositorio
    {

        public List<UnidadMedidaModelo> Listar()
        {
            Entities db = new Entities();
            List<UnidadMedidaModelo> unidades = new List<UnidadMedidaModelo>();
            try
            {
                foreach (UNIDAD_MEDIDA unidad in db.UNIDADES_MEDIDA)
                {
                    unidades.Add(Mappers.UnidadMedidaMapper.EntidadAModelo(unidad));
                }
                return unidades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UnidadMedidaModelo ObtenerPorId(int id)
        {
            Entities db = new Entities();
            try
            {
                UNIDAD_MEDIDA unidad = db.UNIDADES_MEDIDA.Find(id);
                return Mappers.UnidadMedidaMapper.EntidadAModelo(unidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Agregar(UnidadMedidaModelo unidad)
        {
            Entities db = new Entities();
            try
            {
                UNIDAD_MEDIDA entidad = Mappers.UnidadMedidaMapper.ModeloAEntidad(unidad);
                db.UNIDADES_MEDIDA.Add(entidad);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(UnidadMedidaModelo unidad)
        {
            Entities db = new Entities();
            try
            {
                UNIDAD_MEDIDA entidad = Mappers.UnidadMedidaMapper.ModeloAEntidad(unidad);
                UNIDAD_MEDIDA entidadDB = db.UNIDADES_MEDIDA.Find(entidad.id_unidad);
                if (entidadDB == null)
                {
                    throw new Exception("Unidad de medida no encontrada");
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
                UNIDAD_MEDIDA entidad = db.UNIDADES_MEDIDA.Find(id);
                db.UNIDADES_MEDIDA.Remove(entidad);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
