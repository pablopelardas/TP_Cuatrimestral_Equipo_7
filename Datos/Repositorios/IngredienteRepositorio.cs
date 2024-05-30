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
        public static string GetSelectIngredientes(string prefix = "")
        {
            return $@"
INGREDIENTES.id_ingrediente as $'{prefix}id_ingrediente',
INGREDIENTES.nombre as $'{prefix}nombre',
INGREDIENTES.cantidad as '{prefix}cantidad',
INGREDIENTES.costo as '{prefix}costo',
INGREDIENTES.proveedor as '{prefix}proveedor',
{UnidadMedidaRepositorio.GetSelectUnidades(prefix + "UNIDEADES_MEDIDA.")}
";
        }

        public static string GetJoinIngredientes()
        {
            return $@"
INNER JOIN UNIDADES_MEDIDA ON INGREDIENTES.id_unidad = UNIDADES_MEDIDA.id_unidad
";
        }
        public static IngredienteEntidad GetEntidadFromReader(System.Data.SqlClient.SqlDataReader reader, string prefix = "")
        {
            IngredienteEntidad entidad = new IngredienteEntidad();

            entidad.id_ingrediente = (int)reader[$"{prefix}id_ingrediente"];
            entidad.nombre = (string)reader[$"{prefix}nombre"];
            entidad.cantidad = (float)reader[$"{prefix}cantidad"];
            entidad.costo = (decimal)reader[$"{prefix}costo"];
            entidad.proveedor = (string)reader[$"{prefix}proveedor"];

            entidad.unidad = UnidadMedidaRepositorio.GetEntidadFromReader(reader, prefix + "UNIDADES_MEDIDA");
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
{GetSelectIngredientes()}
FROM [dbo].[INGREDIENTES]
{GetJoinIngredientes()}
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

            try
            {
                datos.SetearConsulta("Select * from [dbo].[INGREDIENTES] where id_ingrediente = @id");
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
