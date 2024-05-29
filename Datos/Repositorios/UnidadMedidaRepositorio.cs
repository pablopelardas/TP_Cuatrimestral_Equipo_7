using Datos.Entidades;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class UnidadMedidaRepositorio
    {
        private UnidadMedidaEntidad getEntidadFromReader(System.Data.SqlClient.SqlDataReader reader)
        {
            UnidadMedidaEntidad entidad = new UnidadMedidaEntidad();
            entidad.id_unidad = (int)reader["id_unidad"];
            entidad.nombre = (string)reader["nombre"];
            entidad.abreviatura = (string)reader["abreviatura"];
            return entidad;
        }

        private void ParametrizarEntidad(UnidadMedidaEntidad entidad, AccesoDatos datos)
        {
            datos.SetearParametro("@id_ingrediente", entidad.id_unidad);
            datos.SetearParametro("@nombre", entidad.nombre);
            datos.SetearParametro("@cantidad", entidad.abreviatura);
        }

        public List<UnidadMedidaModelo> Listar()
        {
            List<UnidadMedidaModelo> unidades = new List<UnidadMedidaModelo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("Select * from [dbo].[UNIDADES_MEDIDA]");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    unidades.Add(Mappers.UnidadMedidaMapper.EntidadAModelo(getEntidadFromReader(datos.Lector)));
                }
                return unidades;
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

        public UnidadMedidaModelo ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            UnidadMedidaModelo unidad = new UnidadMedidaModelo();

            try
            {
                datos.SetearConsulta("Select * from [dbo].[INGREDIENTES] where id_ingrediente = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                datos.Lector.Read();
                unidad = Mappers.UnidadMedidaMapper.EntidadAModelo(getEntidadFromReader(datos.Lector));
                return unidad;
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

        public void Agregar(UnidadMedidaModelo unidad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                UnidadMedidaEntidad entidad = Mappers.UnidadMedidaMapper.ModeloAEntidad(unidad);
                datos.SetearConsulta("INSERT INTO[dbo].[Unidades_Medida](id_unidad, nombre, abreviatura) VALUES(@id_unidad, @nombre, @abreviatura)");
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
        public void Modificar(UnidadMedidaModelo unidad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                UnidadMedidaEntidad entidad = Mappers.UnidadMedidaMapper.ModeloAEntidad(unidad);
                datos.SetearConsulta("UPDATE [dbo].[Unidades_Medida] SET id_unidad = @id_unidad, nombre = @nombre, abreviatura = @abreviatura WHERE id_unidad = @id_unidad");
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
                datos.SetearConsulta("DELETE FROM [dbo].[Unidades_Medida] WHERE id_unidad = @id_unidad");
                datos.SetearParametro("@id_unidad", id);
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
