using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class OrdenRepositorio
    {
        public List<Dominio.Modelos.OrdenModelo> Listar()
        {
            List<Dominio.Modelos.OrdenModelo> ordenes = new List<Dominio.Modelos.OrdenModelo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT * FROM [dbo].[Ordenes]");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Entidades.OrdenEntidad entidad = new Entidades.OrdenEntidad();
                    entidad.id_orden = (int)datos.Lector["id_orden"];
                    entidad.id_cliente = (int)datos.Lector["id_cliente"];
                    entidad.fecha = (DateTime)datos.Lector["fecha"];
                    entidad.tipo_evento = (string)datos.Lector["tipo_evento"];
                    entidad.tipo_entrega = (string)datos.Lector["tipo_entrega"];
                    entidad.total = (decimal)datos.Lector["total"];
                    entidad.descuento = (decimal)datos.Lector["descuentos"];
                    entidad.incremento = (decimal)datos.Lector["incrementos"];
                    entidad.descripcion = (string)datos.Lector["descripcion"];

                    ordenes.Add(Mappers.OrdenMapper.EntidadAModelo(entidad));
                }
                return ordenes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Dominio.Modelos.OrdenModelo ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearParametro("@id", id);
                datos.SetearConsulta(datos.Comando.CommandText = "SELECT * FROM [dbo].[Ordenes] WHERE id_orden = @id");
                datos.EjecutarLectura();
                datos.Lector.Read();
                Entidades.OrdenEntidad entidad = new Entidades.OrdenEntidad();
                entidad.id_orden = (int)datos.Lector["id_orden"];
                entidad.id_cliente = (int)datos.Lector["id_cliente"];
                entidad.fecha = (DateTime)datos.Lector["fecha"];
                entidad.tipo_evento = (string)datos.Lector["tipo_evento"];
                entidad.tipo_entrega = (string)datos.Lector["tipo_entrega"];
                entidad.total = (decimal)datos.Lector["total"];
                entidad.descuento = (decimal)datos.Lector["descuentos"];
                entidad.incremento = (decimal)datos.Lector["incrementos"];
                entidad.descripcion = (string)datos.Lector["descripcion"];

                return Mappers.OrdenMapper.EntidadAModelo(entidad);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Agregar(Dominio.Modelos.OrdenModelo orden)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(datos.Comando.CommandText = "INSERT INTO Ordenes (id_cliente, fecha, tipo_evento, tipo_entrega, total, descuento, incremento, descripcion) values (@id_cliente, @fecha, @tipo_evento, @tipo_entrega, @total, @descuento, @incremento, @descripcion) SELECT CAST(scope_identity() AS int)");
                datos.SetearParametro("@id_cliente", orden.IdCliente);
                datos.SetearParametro("@fecha", orden.Fecha);
                datos.SetearParametro("@tipo_evento", orden.TipoEvento);
                datos.SetearParametro("@tipo_entrega", orden.TipoEntrega);
                datos.SetearParametro("@total", orden.Total);
                datos.SetearParametro("@descuento", orden.Descuento);
                datos.SetearParametro("@incremento", orden.Incremento);
                datos.SetearParametro("@descripcion", orden.Descripcion);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Modificar(Dominio.Modelos.OrdenModelo orden)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(datos.Comando.CommandText = "UPDATE Ordenes set id_cliente = @id_cliente, fecha = @fecha, tipo_evento = @tipo_evento, tipo_entrega = @tipo_entrega, total = @total, descuento = @descuento, incremento = @incremento, descripcion = @descripcion");
                datos.SetearParametro("@id_cliente", orden.IdCliente);
                datos.SetearParametro("@fecha", orden.Fecha);
                datos.SetearParametro("@tipo_evento", orden.TipoEvento);
                datos.SetearParametro("@tipo_entrega", orden.TipoEntrega);
                datos.SetearParametro("@total", orden.Total);
                datos.SetearParametro("@descuento", orden.Descuento);
                datos.SetearParametro("@incremento", orden.Incremento);
                datos.SetearParametro("@descripcion", orden.Descripcion);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(datos.Comando.CommandText = "DELETE FROM Ordenes where id_orden = @id");
                datos.SetearParametro("@id", id);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public static List<Entidades.DetalleEntidad> BuscarDetallePorIdOrden(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Entidades.DetalleEntidad> listaDetalle = new List<Entidades.DetalleEntidad>();
            try
            {
                datos.SetearConsulta(datos.Comando.CommandText = "Select * FROM Detalles where id_orden = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarAccion();
                ListarDetalle(listaDetalle, datos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                datos.CerrarConexion();
            }
            return listaDetalle;
        }

        public static List<Entidades.DetalleEntidad> ListarDetalle(List<Entidades.DetalleEntidad> listaDetalle, AccesoDatos datos)
        {
            while (datos.Lector.Read())
            {
                Entidades.DetalleEntidad detalle = new Entidades.DetalleEntidad();

                detalle.id_orden = (int)datos.Lector["id_orden"];
                detalle.id_producto = (int)datos.Lector["id_producto"];
                detalle.cantidad = (int)datos.Lector["cantidad"];
                detalle.precio = (int)datos.Lector["precio"];

                listaDetalle.Add(detalle);
            }
            return listaDetalle;
        }

        public static List<Entidades.ProductoEntidad> BuscarProductosPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Entidades.ProductoEntidad> listaProductos = new List<Entidades.ProductoEntidad>();
            try
            {
                datos.SetearConsulta(datos.Comando.CommandText = "Select * FROM Productos where id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarAccion();
                ListarProductos(listaProductos, datos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                datos.CerrarConexion();
            }
            return listaProductos;
        }
        public static List<Entidades.ProductoEntidad> ListarProductos(List<Entidades.ProductoEntidad> listaProductos, AccesoDatos datos)
        {
            while (datos.Lector.Read())
            {
                Entidades.ProductoEntidad producto = new Entidades.ProductoEntidad();

                producto.codigo = (int)datos.Lector["codigo"];
                producto.nombre = (string)datos.Lector["nombre"];
                producto.descripcion = (string)datos.Lector["descripcion"];
                producto.porciones = (int)datos.Lector["porciones"];
                producto.horas = (int)datos.Lector["horas"];
                producto.recetas = (string)datos.Lector["recetas"];
                producto.suministros = (string)datos.Lector["suministros"];
                producto.costo = (decimal)datos.Lector["costo"];
                producto.costo_porcion = (decimal)datos.Lector["costo_porcion"];
                producto.precio_venta = (decimal)datos.Lector["precio_venta"];
                producto.tarifa_impuesto = (decimal)datos.Lector["tarifa_impuesto"];
                producto.ganancia_neta = (decimal)datos.Lector["ganancia_neta"];
                //producto.Imagenes = ListarImagenes(codigo);


                listaProductos.Add(producto);
            }
            return listaProductos;
        }

    }
}
