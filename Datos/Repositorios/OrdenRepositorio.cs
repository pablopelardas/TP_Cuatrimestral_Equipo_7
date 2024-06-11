using Datos.EF;
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
                        Mappers.OrdenMapper.ActualizarEntidad(ref ordenEntidad, orden);
                    }
                    else
                    {
                        ordenEntidad = Mappers.OrdenMapper.ModeloAEntidad(orden);
                        db.ORDENES.Add(ordenEntidad);
                    }

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
                        db.EVENTOS.Add(evento);
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

    }
}
