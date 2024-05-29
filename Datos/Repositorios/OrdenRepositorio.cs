using Datos.Entidades;
using Dominio.Modelos;
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
                    entidad.descripcion = (string)datos.Lector["descripcion"];
                    entidad.descuento_porcentaje = datos.Lector["descuento_porcentaje"] == DBNull.Value ? 0 : (decimal)datos.Lector["descuento_porcentaje"];
                    entidad.incremento_porcentaje = datos.Lector["incremento_porcentaje"] == DBNull.Value ? 0 : (decimal)datos.Lector["incremento_porcentaje"];

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
        public decimal CalcularTotal(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            decimal total = 0;
            try
            {
                datos.SetearConsulta("SELECT SUM(producto_precio * cantidad) as Total FROM DETALLE_ORDENES WHERE id_orden = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                datos.Lector.Read();
                total = (decimal)datos.Lector[0];
                return total;
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
        public static List<ProductoModelo> ListarProductosPorOrden(int orden_id)
        {
            AccesoDatos datos = new AccesoDatos();
            List<ProductoModelo> listaProductos = new List<ProductoModelo>();
            try
            {
                datos.SetearConsulta("SELECT P.id_producto, p.nombre, p.descripcion, p.porciones, p.horas_trabajo, p.tipo_precio, p.valor_precio, p.id_categoria FROM PRODUCTOS AS P INNER JOIN DETALLE_ORDENES ON P.id_producto = DETALLE_ORDENES.id_producto WHERE DETALLE_ORDENES.id_orden = @orden_id");
                datos.SetearParametro("@orden_id", orden_id);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    ProductoEntidad entidad = new ProductoEntidad();
                    entidad.id_producto = (int)datos.Lector["id_producto"];
                    entidad.nombre = (string)datos.Lector["nombre"];
                    entidad.descripcion = (string)datos.Lector["descripcion"];
                    entidad.porciones = (int)datos.Lector["porciones"];
                    entidad.horas_trabajo = (decimal)datos.Lector["horas_trabajo"];
                    entidad.tipo_precio = (string)datos.Lector["tipo_precio"];
                    entidad.valor_precio = (decimal)datos.Lector["valor_precio"];
                    entidad.id_categoria = (int)datos.Lector["id_categoria"];

                    ProductoModelo producto = Mappers.ProductoMapper.EntidadAModelo(entidad);

                    producto.Categoria = new CategoriaModelo { Id = entidad.id_categoria, Nombre = "test", Tipo = "Receta" };

                    listaProductos.Add(producto);
                }
                return listaProductos;
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

        public void Agregar(OrdenModelo orden)
        {
            throw new NotImplementedException();
        }

        public void Modificar(OrdenModelo orden)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
