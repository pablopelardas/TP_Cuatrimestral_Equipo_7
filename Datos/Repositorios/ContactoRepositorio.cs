using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class ContactoRepositorio
    {
        
        public List<Dominio.Modelos.ContactoModelo> Listar()
        {
            Entities db = new Entities();
            List<Dominio.Modelos.ContactoModelo> contactos = new List<Dominio.Modelos.ContactoModelo>();

            try
            {
                var query = from c in db.CONTACTOS
                            select new
                            {
                                Contacto = c
                            };

                foreach (var row in query)
                {
                    contactos.Add(Mappers.ContactoMapper.EntidadAModelo(row.Contacto));
                }

                return contactos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public Dominio.Modelos.ContactoModelo ObtenerPorId(int id)
        {
            Entities db = new Entities();
            try
            {
                var query = from c in db.CONTACTOS
                            where c.id_contacto == id
                            select new
                            {
                                Contacto = c
                            };

                var row = query.FirstOrDefault();
                if (row != null)
                {
                    return Mappers.ContactoMapper.EntidadAModelo(row.Contacto);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Agregar(Dominio.Modelos.ContactoModelo contacto)
        {
            Entities db = new Entities();
            try
            {
                CONTACTO c = Mappers.ContactoMapper.ModeloAEntidad(contacto);
                db.CONTACTOS.Add(c);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Actualizar(Dominio.Modelos.ContactoModelo contacto)
        {
            Entities db = new Entities();
            try
            {
                CONTACTO c = db.CONTACTOS.Find(contacto.Id);
                Mappers.ContactoMapper.ActualizarEntidad(ref c, contacto);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }


    }
}
