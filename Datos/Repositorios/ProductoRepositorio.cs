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
        public static string GetSelectProductos(string prefix = "")
        {
            string prefixTable = prefix.Length > 0 ? prefix.Replace(".", "_") + '_' : "";
            prefix = prefix.Length > 0 ? prefix + "." : "";
            return $@"
{prefixTable}PRODUCTOS.id_producto as '{prefix}id_producto',
{prefixTable}PRODUCTOS.nombre as '{prefix}nombre',
{prefixTable}PRODUCTOS.descripcion as '{prefix}descripcion',
{prefixTable}PRODUCTOS.porciones as '{prefix}porciones',
{prefixTable}PRODUCTOS.horas_trabajo as '{prefix}horas_trabajo',
{prefixTable}PRODUCTOS.tipo_precio as '{prefix}tipo_precio',
{prefixTable}PRODUCTOS.valor_precio as '{prefix}valor_precio',
{CategoriasRepositorio.GetSelectCategorias(prefix + "categoria")}
";
        }

        public static string GetJoinProductos(string prefix = "")
        {
            prefix = prefix.Length > 0 ? prefix.Replace(".", "_") + '_' : "";
            string aliasCategorias = prefix + "categoria_" + "CATEGORIAS";
            return $@"
INNER JOIN CATEGORIAS as {aliasCategorias} ON {prefix}PRODUCTOS.ID_CATEGORIA = {aliasCategorias}.ID_CATEGORIA
";
        }

        public static Entidades.ProductoEntidad GetEntidadFromReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            prefix = prefix.Length > 0 ? prefix + "." : "";
            Entidades.ProductoEntidad entidad = new Entidades.ProductoEntidad();
            entidad.id_producto = (int)reader[$"{prefix}id_producto"];
            entidad.nombre = (string)reader[$"{prefix}nombre"];
            entidad.descripcion = (string)reader[$"{prefix}descripcion"];
            entidad.porciones = (int)reader[$"{prefix}porciones"];
            entidad.horas_trabajo = (decimal)reader[$"{prefix}horas_trabajo"];
            entidad.tipo_precio = (string)reader[$"{prefix}tipo_precio"];
            entidad.valor_precio = (decimal)reader[$"{prefix}valor_precio"];
            // producto.categoria.
            entidad.categoria = CategoriasRepositorio.GetEntidadFromReader(reader, prefix + "categoria");
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
            datos.SetearParametro("@id_categoria", entidad.categoria.id_categoria);
        }

        public List<Dominio.Modelos.ProductoModelo> Listar()
        {
            List<Dominio.Modelos.ProductoModelo> productos = new List<Dominio.Modelos.ProductoModelo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string cmd = $@"
SELECT
{GetSelectProductos()}
FROM PRODUCTOS
{GetJoinProductos()}
";
                datos.SetearConsulta(cmd);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Entidades.ProductoEntidad entidad = GetEntidadFromReader(datos.Lector);
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
                string cmd = $@"
SELECT
{GetSelectProductos()}
FROM PRODUCTOS
{GetJoinProductos()}
WHERE id_producto = @id
";
                datos.SetearParametro("@id", id);
                datos.SetearConsulta(cmd);
                datos.EjecutarLectura();
                datos.Lector.Read();
                Entidades.ProductoEntidad entidad = GetEntidadFromReader(datos.Lector);

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
