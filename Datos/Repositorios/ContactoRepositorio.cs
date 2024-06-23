using Datos.EF;
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
        
        public List<Dominio.Modelos.ContactoModelo> GetFilteredPage(int pageNumber, int pageSize, string tipo, string filtro)
        {
            Entities db = new Entities();
            
            if (pageNumber < 1)
            {
                return new List<Dominio.Modelos.ContactoModelo>();
            }
            return db.CONTACTOS
                .Where(c => string.IsNullOrEmpty(tipo) || c.tipo == tipo)
                .Where(c => string.IsNullOrEmpty(filtro) || c.nombre_apellido.Contains(filtro) || c.correo.Contains(filtro) || c.telefono.Contains(filtro))
                .OrderBy(c => c.nombre_apellido)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(c => Mappers.ContactoMapper.EntidadAModelo(c))
                .ToList();
        }
        
        public int GetFilteredTotalCount(string tipo, string filtro)
        {
            Entities db = new Entities();
            return db.CONTACTOS
                .Where(c => string.IsNullOrEmpty(tipo) || c.tipo == tipo)
                .Where(c => string.IsNullOrEmpty(filtro) || c.nombre_apellido.Contains(filtro) || c.correo.Contains(filtro) || c.telefono.Contains(filtro))
                .Count();
        }
        
        
        public List<Dominio.Modelos.ContactoModelo> ListarPorTipo(string tipo)
        {
            try
            {
                using (Entities db = new Entities())
                {
                    return Mappers.ContactoMapper.EntidadesAModelos(db.CONTACTOS.Where(c => c.tipo == tipo).ToList());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Dominio.Modelos.ContactoModelo ObtenerPorId(Guid id)
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
