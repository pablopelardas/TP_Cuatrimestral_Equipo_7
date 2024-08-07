﻿using Datos.EF;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.Entity.SqlServer;
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

                List<ORDEN> ordenesEntidad = db.ORDENES.OrderByDescending(x => x.EVENTO.fecha).ToList();
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
        
        public int GetTotalCount()
        {
            Entities db = new Entities();
            return db.ORDENES.Count();
        }
        
        public int GetFilteredTotalCount(int semanas, int estado, ContactoModelo contacto = null)
        {
            Entities db = new Entities();
            IQueryable<ORDEN> query = db.ORDENES.AsQueryable();

            if (semanas != 0)
            {
                query = query.Where(x => x.EVENTO.fecha < DateTime.Now
                    ? SqlFunctions.DateDiff("week", x.EVENTO.fecha, DateTime.Now) <= semanas
                    : SqlFunctions.DateDiff("week", DateTime.Now, x.EVENTO.fecha) <= semanas);
            }

            if (estado != 0)
            {
                query = query.Where(x => x.id_orden_estado == estado);
            }
            
            if (contacto != null)
            {
                query = query.Where(x => x.id_cliente == contacto.Id);
            }

            return query.Count();
        }

        public List<Dominio.Modelos.OrdenModelo> GetFilteredPage(int pageNumber, int pageSize, int semanas, int estado, ContactoModelo contacto = null)
        {
            Entities db = new Entities();
            IQueryable<ORDEN> query = db.ORDENES.AsQueryable();

            if (semanas != 0)
            {
                query = query.Where(x => x.EVENTO.fecha < DateTime.Now
                    ? SqlFunctions.DateDiff("week", x.EVENTO.fecha, DateTime.Now) <= semanas
                    : SqlFunctions.DateDiff("week", DateTime.Now, x.EVENTO.fecha) <= semanas);
            }

            if (estado != 0)
            {
                query = query.Where(x => x.id_orden_estado == estado);
            }
            
            if (contacto != null)
            {
                query = query.Where(x => x.id_cliente == contacto.Id);
            }

            List<ORDEN> ordenes = query
                .OrderByDescending(x => x.EVENTO.fecha)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return ordenes.Select(x => Mappers.OrdenMapper.EntidadAModelo(x)).ToList();
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

        public OrdenModelo GuardarOrdenTx(OrdenModelo orden, bool guardarDireccionEnCliente = false)
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

                        // Check if contact is different
                        if (ordenEntidad.id_cliente != orden.Cliente.Id)
                        {
                            throw new Exception("No se puede cambiar el cliente de la orden");
                        }

                        Mappers.OrdenMapper.ActualizarEntidad(ref ordenEntidad, orden);
                    }
                    else
                    {
                        ordenEntidad = Mappers.OrdenMapper.ModeloAEntidad(orden);
                    }

                    db.ORDENES.AddOrUpdate(ordenEntidad);
                    db.SaveChanges();

                    if (orden.TipoEntrega == "Retiro")
                    {
                        ordenEntidad.ORDENDIRECCION = null;
                        // remove direccion
                        ORDENDIRECCION direccionEntidad = db.ORDENES_DIRECCIONES.Find(ordenEntidad.id_orden);
                        if (direccionEntidad != null)
                        {
                            db.ORDENES_DIRECCIONES.Remove(direccionEntidad);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        // Check if direccion is different
                        ORDENDIRECCION direccionEntidad = db.ORDENES_DIRECCIONES.Find(ordenEntidad.id_orden);

                        if (guardarDireccionEnCliente)
                        {
                            // check if direccion exists
                            DIRECCION direccionCliente = db.DIRECCIONES.Find(orden.DireccionEntrega.IdDireccion);
                            orden.DireccionEntrega.Cliente = orden.Cliente;
                            if (direccionCliente == null)
                            {
                                // add direccion
                                direccionCliente = Mappers.DireccionMapper.ModeloAEntidad(orden.DireccionEntrega);
                                db.DIRECCIONES.Add(direccionCliente);
                            }
                            else
                            {
                                Mappers.DireccionMapper.ActualizarEntidad(ref direccionCliente, orden.DireccionEntrega);
                            }

                            db.SaveChanges();
                        }

                        orden.DireccionEntrega.IdDireccion = ordenEntidad.id_orden;
                        // Check if direccion exists
                        if (direccionEntidad == null)
                        {
                            db.ORDENES_DIRECCIONES.Add(
                                Mappers.OrdenDireccionMapper.ModeloAEntidad(orden.DireccionEntrega));
                            db.SaveChanges();
                        }
                        else
                        {
                            // Update direccion
                            Mappers.OrdenDireccionMapper.ActualizarEntidad(ref direccionEntidad,
                                orden.DireccionEntrega);
                            db.SaveChanges();
                        }
                    }

                    // Eliminar detalles anteriores de la orden with linq
                    db.DETALLE_ORDENES.RemoveRange(db.DETALLE_ORDENES.Where(x => x.id_orden == ordenEntidad.id_orden));

                    // Agregar detalles nuevos linq
                    db.DETALLE_ORDENES.AddRange(orden.DetalleProductos.Select(x =>
                    {
                        DETALLEORDEN detalleEntidad = Mappers.ProductoDetalleOrdenMapper.ModeloAEntidad(x);
                        detalleEntidad.id_orden = ordenEntidad.id_orden;
                        return detalleEntidad;
                    }));

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

                    orden.IdOrden = ordenEntidad.id_orden;
                    scope.Complete();
                    return orden;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        
    public void AgregarPagoTx(OrdenModelo orden, PagoModelo pago)
    {
        using (Entities db = new Entities())
        using (var scope = new System.Transactions.TransactionScope())
        {
            try
            {
                PAGO pagoEntidad = Mappers.PagoMapper.ModeloAEntidad(pago);
                // buscar orden
                ORDEN ordenEntidad = db.ORDENES.Find(orden.IdOrden);
                if (ordenEntidad == null)
                {
                    throw new Exception("No se encontró la orden");
                }
                // verificar si el monto es 0
                if (pagoEntidad.monto <= 0)
                {
                    throw new Exception("El monto del pago debe ser mayor a 0");
                }
                // verificar si el pago es mayor al total de la orden
                if (pagoEntidad.monto > orden.Total)
                {
                    throw new Exception("El monto del pago es mayor al total de la orden");
                }
                // verificar si el pago + el total pagado es mayor al total de la orden
                if (pagoEntidad.monto + orden.TotalPagado > orden.Total)
                {
                    throw new Exception("El monto del pago + el total pagado es mayor al total de la orden");
                }
                
                // agregar pago
                pagoEntidad.id_orden = orden.IdOrden;
                db.PAGOS.Add(pagoEntidad);
                db.SaveChanges();
                
                //  actualizar estado de la orden
                ordenEntidad.id_orden_pago_estado = pagoEntidad.monto + orden.TotalPagado == orden.Total ? 3 : 2;
                
                db.SaveChanges();
                
                db.HISTORICO_ENTIDADES.Add(new HISTORICOENTIDAD
                {
                    id_entidad = orden.IdOrden,
                    justificacion = $"Pago de ${pagoEntidad.monto} realizado",
                    fecha = DateTime.Now
                });
                db.HISTORICO_ENTIDADES.Add(new HISTORICOENTIDAD
                {
                    id_entidad = orden.Cliente.Id,
                    justificacion = $"Pago de ${pagoEntidad.monto} realizado en orden {orden.ShortId}",
                    fecha = DateTime.Now
                });
                
                db.SaveChanges();
                
                scope.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
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
