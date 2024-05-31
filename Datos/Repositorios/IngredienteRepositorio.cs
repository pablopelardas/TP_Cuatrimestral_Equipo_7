using Datos.Entidades;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class IngredienteRepositorio
    {
        private static string UNIDAD_PREFIX = "uni";
        public static string GetSelect(string prefix = "")
        {
            string prefixTable = prefix.Length > 0 ? prefix.Replace(".", "_") + "_" : "";
            prefix = prefix.Length > 0 ? prefix + "." : "";

            return $@"
{prefixTable}INGREDIENTES.id_ingrediente as '{prefix}id_ingrediente',
{prefixTable}INGREDIENTES.nombre as '{prefix}nombre',
{prefixTable}INGREDIENTES.cantidad as '{prefix}cantidad',
{prefixTable}INGREDIENTES.costo as '{prefix}costo',
{prefixTable}INGREDIENTES.proveedor as '{prefix}proveedor',
{UnidadMedidaRepositorio.GetSelect(prefix + UNIDAD_PREFIX)}
";
        }

        public static string GetJoin(string prefix = "")
        {
            prefix = prefix.Length > 0 ? prefix.Replace(".", "_") + '_' : "";
            string aliasUnidades = prefix + UNIDAD_PREFIX + "_UNIDADES_MEDIDA";
            return $@"
INNER JOIN UNIDADES_MEDIDA as {aliasUnidades} ON {prefix}INGREDIENTES.id_unidad = {aliasUnidades}.id_unidad
";
        }
        public static IngredienteEntidad GetEntidadFromReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            IngredienteEntidad entidad = new IngredienteEntidad();

            entidad.id_ingrediente = (int)reader[$"{prefix}id_ingrediente"];
            entidad.nombre = (string)reader[$"{prefix}nombre"];
            entidad.cantidad = (double)reader[$"{prefix}cantidad"];
            entidad.costo = (decimal)reader[$"{prefix}costo"];
            entidad.proveedor = (string)reader[$"{prefix}proveedor"];

            entidad.unidad = UnidadMedidaRepositorio.GetEntidadFromReader(reader, prefix + UNIDAD_PREFIX);
            return entidad;
        }

        private void ParametrizarEntidad(IngredienteEntidad entidad, AccesoDatos datos)
        {
            datos.SetearParametro("@id_ingrediente", entidad.id_ingrediente);
            datos.SetearParametro("@nombre", entidad.nombre);
            datos.SetearParametro("@cantidad", entidad.cantidad);
            datos.SetearParametro("@id_unidad", entidad.unidad.id_unidad);
            datos.SetearParametro("@costo", entidad.costo);
            datos.SetearParametro("@proveedor", entidad.proveedor);
        }

            public List<IngredienteModelo> Listar()
        {
            List<IngredienteModelo> ingredientes = new List<IngredienteModelo>();
            AccesoDatos datos = new AccesoDatos();
            string cmd = $@"
SELECT
{GetSelect()}
FROM dbo.[INGREDIENTES]
{GetJoin()}
";
            try
            {
                datos.SetearConsulta(cmd);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    ingredientes.Add(Mappers.IngredienteMapper.EntidadAModelo(GetEntidadFromReader(datos.Lector)));
                }
                return ingredientes;
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

        public IngredienteModelo ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            string cmd = $@"
Select
{GetSelect()}
FROM [dbo].[INGREDIENTES]
{GetJoin()}
WHERE id_ingrediente = @id
";
            try
            {
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                datos.Lector.Read();
                return Mappers.IngredienteMapper.EntidadAModelo(GetEntidadFromReader(datos.Lector));
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

        public void Agregar(IngredienteModelo ingrediente)
        {
            AccesoDatos datos = new AccesoDatos();
            string cmd = $@"
INSERT INTO [dbo].[INGREDIENTES]
(id_ingrediente, nombre, cantidad, id_unidad, costo, proveedor)
VALUES
(@id_ingrediente, @nombre, @cantidad, @id_unidad, @costo, @proveedor)
";
            try
            {
                IngredienteEntidad entidad = Mappers.IngredienteMapper.ModeloAEntidad(ingrediente);
                datos.SetearConsulta(cmd);
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
        public void Modificar(IngredienteModelo ingrediente)
        {
            AccesoDatos datos = new AccesoDatos();
            string cmd = $@"
UPDATE [dbo].[INGREDIENTES]
SET
id_ingrediente = @id_ingrediente,
nombre = @nombre,
cantidad = @cantidad,
id_unidad = @id_unidad,
costo = @costo,
proveedor = @proveedor
WHERE id_ingrediente = @id_ingrediente
";
            try
            {
                IngredienteEntidad entidad = Mappers.IngredienteMapper.ModeloAEntidad(ingrediente);
                datos.SetearConsulta(cmd);
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
        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            string cmd = $@"
DELETE FROM [dbo].[INGREDIENTES]
WHERE id_ingrediente = @id_ingrediente";
            try
            {
                datos.SetearConsulta(cmd);
                datos.SetearParametro("@id_ingrediente", id);
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
    }
}
