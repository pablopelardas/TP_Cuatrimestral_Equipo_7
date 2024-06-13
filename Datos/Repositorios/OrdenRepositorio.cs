using Datos.EF;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class OrdenRepositorio
    {
        public List<Dominio.Modelos.OrdenModelo> Listar()
        {
            List<Dominio.Modelos.OrdenModelo> ordenes = new List<Dominio.Modelos.OrdenModelo>();
            Entities db = new Entities();

            try
            {

                List<ORDEN> ordenesEntidad = db.ORDENES.ToList();
                foreach (var ordenEntidad in ordenesEntidad)
                {
                    OrdenModelo orden = Mappers.OrdenMapper.EntidadAModelo(ordenEntidad);
                    ordenes.Add(orden);
                }

                return ordenes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dominio.Modelos.OrdenModelo ObtenerPorId(Guid id)
        {
            Entities db = new Entities();
            try
            {

                ORDEN ordenEntidad = db.ORDENES.Find(id);

                if (ordenEntidad != null)
                {
                    return Mappers.OrdenMapper.EntidadAModelo(ordenEntidad);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarOrdenTx(OrdenModelo orden)
        {
            
            using (Entities db = new Entities())
            using (var scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    
                    
                    ORDEN ordenEntidad;
                    if (orden.IdOrden != Guid.Empty)
                    {
                        ordenEntidad = db.ORDENES.Find(orden.IdOrden);
                        
                        // Check if orden exists
                        if (ordenEntidad == null)
                        {
                            throw new Exception("La orden no existe");
                        }
                        // Check if direccion is different
                        if (ordenEntidad.id_direccion != orden.DireccionEntrega.IdDireccion)
                        {
                            // Check if direccion exists
                            DIRECCION direccionEntidad = db.DIRECCIONES.Find(orden.DireccionEntrega.IdDireccion);
                            if (direccionEntidad == null)
                            {
                                db.DIRECCIONES.Add(Mappers.DireccionMapper.ModeloAEntidad(orden.DireccionEntrega));
                                db.SaveChanges();
                            }
                            else
                            {
                                // Remove old direccion
                                db.DIRECCIONES.Remove(direccionEntidad);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            // Update direccion
                            DIRECCION direccionEntidad = db.DIRECCIONES.Find(orden.DireccionEntrega.IdDireccion);
                            Mappers.DireccionMapper.ActualizarEntidad(ref direccionEntidad, orden.DireccionEntrega);
                            db.SaveChanges();
                        }
                        Mappers.OrdenMapper.ActualizarEntidad(ref ordenEntidad, orden);
                    }
                    else
                    {
                        ordenEntidad = Mappers.OrdenMapper.ModeloAEntidad(orden);
                        if (orden.TipoEntrega == "Retiro")
                        {
                            ordenEntidad.id_direccion = null;
                        }
                    }
                    
                    db.ORDENES.AddOrUpdate(ordenEntidad);
                    db.SaveChanges();

                    // Agregar detalles

                    // Encontrar y eliminar detalles

                    List<DETALLEORDEN> detallesViejos = db.DETALLE_ORDENES.Where(x => x.id_orden == orden.IdOrden).ToList();
                    foreach (DETALLEORDEN detalle in detallesViejos)
                    {
                        db.DETALLE_ORDENES.Remove(detalle);
                    }

                    // Agregar detalles nuevos
                    foreach (ProductoDetalleOrdenModelo detalle in orden.DetalleProductos)
                    {
                        DETALLEORDEN detalleEntidad = Mappers.ProductoDetalleOrdenMapper.ModeloAEntidad(detalle);
                        detalleEntidad.id_orden = ordenEntidad.id_orden;
                        db.DETALLE_ORDENES.Add(detalleEntidad);
                    }



                    db.SaveChanges();


                    // Eliminar evento anterior
                    if (orden.IdOrden != Guid.Empty)
                    {
                        EVENTO eventoEntidad = db.EVENTOS.Find(ordenEntidad.id_evento);
                        if (eventoEntidad != null)
                        {
                            db.EVENTOS.Remove(eventoEntidad);
                        }
                    }

                    // Agregar evento
                    if (orden.Evento != null)
                    {
                        EVENTO evento = new EVENTO
                        {
                            id_cliente = orden.Cliente.Id,
                            fecha = orden.Evento.Fecha,
                            id_tipo_evento = orden.Evento.TipoEvento.IdTipoEvento
                        };
                        evento = db.EVENTOS.Add(evento);
                        db.SaveChanges();
                        
                        ordenEntidad.id_evento = evento.id_evento;

                        db.SaveChanges();
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }
        
        public OrdenModelo CambiarEstado(Guid idOrden, int idEstado)
        {
            Entities db = new Entities();
            try
            {
                ORDEN ordenEntidad = db.ORDENES.Find(idOrden);
                if (ordenEntidad != null)
                {
                    ordenEntidad.id_orden_estado = idEstado;
                    db.SaveChanges();
                }
                return Mappers.OrdenMapper.EntidadAModelo(ordenEntidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<OrdenEstadoModelo> ListarEstados()
        {
            List<OrdenEstadoModelo> estados = new List<OrdenEstadoModelo>();
            Entities db = new Entities();
            try
            {
                List<ORDENESTADO> estadosEntidad = db.ORDENES_ESTADOS.ToList();
                foreach (var estadoEntidad in estadosEntidad)
                {
                    OrdenEstadoModelo estado = Mappers.OrdenEstadoMapper.EntidadAModelo(estadoEntidad);
                    estados.Add(estado);
                }
                return estados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
