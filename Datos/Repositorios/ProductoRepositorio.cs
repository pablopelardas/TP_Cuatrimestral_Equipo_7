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
        private Helpers.QueryHelper _QueryHelper = new Helpers.QueryHelper();
        private string CATEGORIA_PREFIX = "cat";

        private CategoriasRepositorio categoriasRepositorio = new CategoriasRepositorio();

        private string ProductoSelect(string prefixTable = "", string prefixColumn = "")
        {
            return $@"
{prefixTable}PRODUCTOS.id_producto as '{prefixColumn}id_producto',
{prefixTable}PRODUCTOS.nombre as '{prefixColumn}nombre',
{prefixTable}PRODUCTOS.descripcion as '{prefixColumn}descripcion',
{prefixTable}PRODUCTOS.porciones as '{prefixColumn}porciones',
{prefixTable}PRODUCTOS.horas_trabajo as '{prefixColumn}horas_trabajo',
{prefixTable}PRODUCTOS.tipo_precio as '{prefixColumn}tipo_precio',
{prefixTable}PRODUCTOS.valor_precio as '{prefixColumn}valor_precio',
{categoriasRepositorio.GetSelect(prefixColumn + CATEGORIA_PREFIX)}
";
        }

        private string ProductoJoin(string prefixTable = "")
        {
            string aliasCategorias = prefixTable + CATEGORIA_PREFIX + "_CATEGORIAS";
            return $@"
INNER JOIN CATEGORIAS as {aliasCategorias} ON {prefixTable}PRODUCTOS.ID_CATEGORIA = {aliasCategorias}.ID_CATEGORIA
";
        }

        private Entidades.ProductoEntidad ProductoReader(System.Data.SqlClient.SqlDataReader reader, string prefixColumn = "")
        {
            Entidades.ProductoEntidad entidad = new Entidades.ProductoEntidad();
            entidad.id_producto = (int)reader[$"{prefixColumn}id_producto"];
            entidad.nombre = (string)reader[$"{prefixColumn}nombre"];
            entidad.descripcion = (string)reader[$"{prefixColumn}descripcion"];
            entidad.porciones = (int)reader[$"{prefixColumn}porciones"];
            entidad.horas_trabajo = (decimal)reader[$"{prefixColumn}horas_trabajo"];
            entidad.tipo_precio = (string)reader[$"{prefixColumn}tipo_precio"];
            entidad.valor_precio = (decimal)reader[$"{prefixColumn}valor_precio"];
            // producto.categoria.
            entidad.categoria = categoriasRepositorio.GetEntity(reader, prefixColumn + CATEGORIA_PREFIX);
            return entidad;
        }
        public string GetSelect(string prefix = "")
        {
            return _QueryHelper.BuildSelect(prefix, ProductoSelect);
        }

        public string GetJoin(string prefix = "")
        {
            return _QueryHelper.BuildJoin(prefix, ProductoJoin);
        }

        public Entidades.ProductoEntidad GetEntity(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            return _QueryHelper.BuildEntityFromReader(reader, prefix, ProductoReader);
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
{GetSelect()}
FROM PRODUCTOS
{GetJoin()}
";
                datos.SetearConsulta(cmd);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Entidades.ProductoEntidad entidad = GetEntity(datos.Lector);
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
{GetSelect()}
FROM PRODUCTOS
{GetJoin()}
WHERE id_producto = @id
";
                datos.SetearParametro("@id", id);
                datos.SetearConsulta(cmd);
                datos.EjecutarLectura();
                datos.Lector.Read();
                Entidades.ProductoEntidad entidad = GetEntity(datos.Lector);

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
