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
                datos.SetearConsulta(datos.Comando.CommandText = "UPDATE Ordenes set id_cliente = @id_cliente, fecha = @fecha, tipo_evento = @tipo_evento, tipo_entrega = @tipo_entrega, total = @total, descuento");
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

        public void Eliminar(Dominio.Modelos.OrdenModelo orden)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(datos.Comando.CommandText = "DELETE FROM Ordenes where id_orden = @IdOrden");
                datos.SetearParametro("@IdOrden", orden.IdOrden);

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
    }
}
