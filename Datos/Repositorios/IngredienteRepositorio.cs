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
        private IngredienteEntidad getEntidadFromReader(System.Data.SqlClient.SqlDataReader reader)
        {
            IngredienteEntidad entidad = new IngredienteEntidad();
            entidad.id_ingrediente = (int)reader["id_ingrediente"];
            entidad.nombre = (string)reader["nombre"];
            entidad.cantidad = (float)reader["cantidad"];
            entidad.id_unidad = (int)reader["id_unidad"];
            entidad.costo = (decimal)reader["costo"];
            entidad.proveedor = (string)reader["proveedor"];
            return entidad;
        }

        private void ParametrizarEntidad(IngredienteEntidad entidad, AccesoDatos datos)
        {
            datos.SetearParametro("@id_ingrediente", entidad.id_ingrediente);
            datos.SetearParametro("@nombre", entidad.nombre);
            datos.SetearParametro("@cantidad", entidad.cantidad);
            datos.SetearParametro("@id_unidad", entidad.id_unidad);
            datos.SetearParametro("@costo", entidad.costo);
            datos.SetearParametro("@proveedor", entidad.proveedor);
        }

            public List<IngredienteModelo> Listar()
        {
            List<IngredienteModelo> ingredientes = new List<IngredienteModelo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("Select * from [dbo].[INGREDIENTES]");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    ingredientes.Add(Mappers.IngredienteMapper.EntidadAModelo(getEntidadFromReader(datos.Lector)));
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
            IngredienteModelo ingrediente = new IngredienteModelo();

            try
            {
                datos.SetearConsulta("Select * from [dbo].[INGREDIENTES] where id_ingrediente = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                datos.Lector.Read();
                ingrediente = Mappers.IngredienteMapper.EntidadAModelo(getEntidadFromReader(datos.Lector));
                return ingrediente;
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

            try
            {
                IngredienteEntidad entidad = Mappers.IngredienteMapper.ModeloAEntidad(ingrediente);
                datos.SetearConsulta("INSERT INTO[dbo].[Ingredientes](id_ingrediente, nombre, cantidad, id_unidad, costo, proveedor) VALUES(@id_ingrediente, @nombre, @cantidad, @id_unidad, @costo, @proveedor)");
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

            try
            {
                IngredienteEntidad entidad = Mappers.IngredienteMapper.ModeloAEntidad(ingrediente);
                datos.SetearConsulta("UPDATE [dbo].[Ingredientes] SET id_ingrediente = @id_ingrediente, nombre = @nombre, cantidad = @cantidad, id_unidad = @id_unidad, costo = @costo, proveedor = @proveedor WHERE id_ingrediente = @id_ingrediente");
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

            try
            {
                datos.SetearConsulta("DELETE FROM [dbo].[Ingredientes] WHERE id_ingrediente = @id_ingrediente");
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
