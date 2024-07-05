using Datos.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Modelos;

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
        
        public ContactoModelo GuardarContactoTx(Dominio.Modelos.ContactoModelo contacto, List<Guid> direccionesEliminadas, List<Guid> eventosEliminados)
        {
            using (Entities db = new Entities())
            using (var scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    CONTACTO contactoEntidad;
                    if (contacto.Id != Guid.Empty)
                    {
                        contactoEntidad = db.CONTACTOS.Find(contacto.Id);
                        if (contactoEntidad == null)
                        {
                            throw new Exception("No se encontró el contacto");
                        }
                        Mappers.ContactoMapper.ActualizarEntidad(ref contactoEntidad, contacto);
                    }
                    else
                    {
                        contactoEntidad = Mappers.ContactoMapper.ModeloAEntidad(contacto);
                    }
                    
                    db.CONTACTOS.AddOrUpdate(contactoEntidad);
                    db.SaveChanges();
                    
                    // Add or update addresses
                    if (contacto.Direcciones != null)
                    {
                        foreach (var direccion in contacto.Direcciones)
                        {
                            DIRECCION direccionEntidad;
                            direccion.Cliente = contacto;
                            if (direccion.IdDireccion != Guid.Empty)
                            {
                                direccionEntidad = db.DIRECCIONES.Find(direccion.IdDireccion);
                                if (direccionEntidad == null)
                                {
                                    throw new Exception("No se encontró la dirección");
                                }
                                Mappers.DireccionMapper.ActualizarEntidad(ref direccionEntidad, direccion);
                            }
                            else
                            {
                                direccionEntidad = Mappers.DireccionMapper.ModeloAEntidad(direccion);
                            }
                            direccionEntidad.id_cliente = contactoEntidad.id_contacto;
                            db.DIRECCIONES.AddOrUpdate(direccionEntidad);
                            db.SaveChanges();
                            direccion.IdDireccion = direccionEntidad.id_direccion;
                        }
                    }

                    
                    db.DIRECCIONES.RemoveRange(db.DIRECCIONES.Where(d => direccionesEliminadas.Contains(d.id_direccion)));
                    db.SaveChanges();
                    
                    // Add or update events
                    if (contacto.Eventos != null)
                    {
                        foreach (var evento in contacto.Eventos)
                        {
                            EVENTO eventoEntidad;
                            evento.Cliente = contacto;
                            if (evento.IdEvento != Guid.Empty)
                            {
                                eventoEntidad = db.EVENTOS.Find(evento.IdEvento);
                                if (eventoEntidad == null)
                                {
                                    throw new Exception("No se encontró el evento");
                                }
                                Mappers.EventoMapper.ActualizarEntidad(ref eventoEntidad, evento);
                            }
                            else
                            {
                                eventoEntidad = Mappers.EventoMapper.ModeloAEntidad(evento);
                            }
                            eventoEntidad.id_cliente = contactoEntidad.id_contacto;
                            db.EVENTOS.AddOrUpdate(eventoEntidad);
                            db.SaveChanges();
                            evento.IdEvento = eventoEntidad.id_evento;
                        }
                    }
                    
                    db.EVENTOS.RemoveRange(db.EVENTOS.Where(e => eventosEliminados.Contains(e.id_evento)));
                    db.SaveChanges();
                    
                    contacto.Id = contactoEntidad.id_contacto;
                    scope.Complete();
                    return contacto;


                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
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
