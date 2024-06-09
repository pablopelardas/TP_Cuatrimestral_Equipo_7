using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
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
                // add eventos
                var query = from o in db.ORDENES
                            select new
                            {
                                Orden = o
                            };

                foreach (var row in query)
                {
                    OrdenModelo orden = Mappers.OrdenMapper.EntidadAModelo(row.Orden);
                    ordenes.Add(orden);
                }

                return ordenes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dominio.Modelos.OrdenModelo ObtenerPorId(int id)
        {
            Entities db = new Entities();
            try
            {
                var query = from o in db.ORDENES
                            where o.id_orden == id
                            select new
                            {
                                Orden = o,
                            };

                var row = query.FirstOrDefault();
                if (row != null)
                {
                    return Mappers.OrdenMapper.EntidadAModelo(row.Orden);
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
                    if (orden.IdOrden != 0)
                    {
                        ORDENES ordenEntidad = db.ORDENES.Find(orden.IdOrden);
                        Mappers.OrdenMapper.ActualizarEntidad(ref ordenEntidad, orden);
                    }
                    else
                    {
                        ORDENES ordenEntidad = Mappers.OrdenMapper.ModeloAEntidad(orden);
                        db.ORDENES.Add(ordenEntidad);
                    }

                    db.SaveChanges();

                    // Agregar detalles

                    // Encontrar y eliminar detalles

                    List<DETALLE_ORDENES> detallesViejos = db.DETALLE_ORDENES.Where(x => x.id_orden == orden.IdOrden).ToList();
                    foreach (DETALLE_ORDENES detalle in detallesViejos)
                    {
                        db.DETALLE_ORDENES.Remove(detalle);
                    }

                    // Agregar detalles nuevos
                    foreach (ProductoDetalleOrdenModelo detalle in orden.DetalleProductos)
                    {
                        DETALLE_ORDENES detalleEntidad = Mappers.ProductoDetalleOrdenMapper.ModeloAEntidad(detalle);
                        db.DETALLE_ORDENES.Add(detalleEntidad);
                    }



                    db.SaveChanges();


                    // Eliminar evento anterior
                    if (orden.IdOrden != 0)
                    {
                        EVENTOS eventoEntidad = db.EVENTOS.Where(x => x.id_orden == orden.IdOrden).FirstOrDefault();
                        if (eventoEntidad != null)
                        {
                            db.EVENTOS.Remove(eventoEntidad);
                        }
                    }

                    // Agregar evento
                    if (orden.Evento != null)
                    {
                        EVENTOS evento = new EVENTOS
                        {
                            id_cliente = orden.Cliente.Id,
                            id_orden = orden.IdOrden,
                            fecha = orden.Evento.Fecha,
                            id_tipo_evento = orden.Evento.TipoEvento.IdTipoEvento
                        };
                        db.EVENTOS.Add(evento);

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

    }
}
