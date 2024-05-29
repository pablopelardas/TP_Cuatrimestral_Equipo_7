using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class ProductoRepositorio
    {
        private Entidades.ProductoEntidad getEntidadFromReader(System.Data.SqlClient.SqlDataReader reader)
        {
            Entidades.ProductoEntidad entidad = new Entidades.ProductoEntidad();
            entidad.id_producto = (int)reader["id_producto"];
            entidad.nombre = (string)reader["nombre"];
            entidad.descripcion = (string)reader["Description"];
            entidad.porciones = (int)reader["porciones"];
            entidad.horas_trabajo = (int)reader["horas_trabajo"];
            entidad.tipo_precio = (string)reader["tipo_precio"];
            entidad.valor_precio = (int)reader["valor_precio"];
            entidad.id_categoria = (int)reader["id_categoria"];
            return entidad;
        }

        private void ParametrizarEntidad(Entidades.ProductoEntidad entidad, AccesoDatos datos)
        {
            datos.SetearParametro("@id_producto", entidad.id_producto);
            datos.SetearParametro("@nombre", entidad.nombre);
            datos.SetearParametro("@descripcion", entidad.descripcion);
            datos.SetearParametro("@porciones", entidad .porciones);
            datos.SetearParametro("@horas_trabajo", entidad.horas_trabajo);
            datos.SetearParametro("@tipo_precio", entidad.tipo_precio);
            datos.SetearParametro("@valor_precio", entidad.valor_precio);
            datos.SetearParametro("@id_categoria", entidad.id_categoria);
        }

        public List<Dominio.Modelos.ProductoModelo> Listar()
        {
            List<Dominio.Modelos.ProductoModelo> productos = new List<Dominio.Modelos.ProductoModelo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT FROM [dbo].[PRODUCTOS]");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Entidades.ProductoEntidad entidad = getEntidadFromReader(datos.Lector);
                    productos.Add(Mappers.ProductoMapper.EntidadAModelo(entidad));
                }
                return productos;
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

        public Dominio.Modelos.ProductoModelo ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearParametro("@id", id);
                datos.SetearConsulta(datos.Comando.CommandText = "SELECT * FROM [dbo].[PRODUCTOS] WHERE id_producto = @id");
                datos.EjecutarLectura();
                datos.Lector.Read();
                Entidades.ProductoEntidad entidad = getEntidadFromReader(datos.Lector);

                return Mappers.ProductoMapper.EntidadAModelo(entidad);

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

        public void Agregar(Dominio.Modelos.ProductoModelo producto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                Entidades.ProductoEntidad entidad = Mappers.ProductoMapper.ModeloAEntidad(producto);
                datos.SetearConsulta("INSERT INTO [dbo].[PRODUCTOS] (id_producto, nombre, descripcion, prociones, horas_trabajo, tipo_precio, valor_precio, id_categoria) VALUES (@id_producto, @nombre, @descripcion, @porciones, @horas_trabajo, @tipo_precio, @valor_precio, @id_categoria)");
                ParametrizarEntidad(entidad, datos);
                datos.EjecutarAccion();
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

        public void Modificar(ProductoModelo orden)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
