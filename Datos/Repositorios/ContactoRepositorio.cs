using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class ContactoRepositorio
    {

        private Entidades.ContactoEntidad getEntidadFromReader(System.Data.SqlClient.SqlDataReader reader)
        {
            Entidades.ContactoEntidad entidad = new Entidades.ContactoEntidad();
            entidad.id_contacto = (int)reader["id_contacto"];
            entidad.nombre_apellido = (string)reader["nombre_apellido"];
            entidad.tipo = (string)reader["tipo"];
            entidad.telefono = (string)reader["telefono"];
            entidad.correo = (string)reader["correo"];
            entidad.direccion = (string)reader["direccion"];
            entidad.fuente = (string)reader["fuente"];
            entidad.producto_que_provee = reader["producto_que_provee"] == DBNull.Value ? null : (string)reader["producto_que_provee"];
            entidad.desea_recibir_correos = (bool)reader["desea_recibir_correos"];
            entidad.desea_recibir_whatsapp = (bool)reader["desea_recibir_whatsapp"];
            entidad.informacion_personal = reader["informacion_personal"] == DBNull.Value ? null : (string)reader["informacion_personal"];
            return entidad;
        }
        public List<Dominio.Modelos.ContactoModelo> Listar()
        {
            List<Dominio.Modelos.ContactoModelo > contactos = new List<Dominio.Modelos.ContactoModelo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT * FROM [dbo].[CONTACTOS]");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Entidades.ContactoEntidad entidad = getEntidadFromReader(datos.Lector);
                    contactos.Add(Mappers.ContactoMapper.EntidadAModelo(entidad));
                }
                return contactos;
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

        public Dominio.Modelos.ContactoModelo ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearParametro("@id", id);
                datos.SetearConsulta(datos.Comando.CommandText = "SELECT * FROM [dbo].[CONTACTOS] WHERE id_contacto = @id");
                datos.EjecutarLectura();
                datos.Lector.Read();
                Entidades.ContactoEntidad entidad = getEntidadFromReader(datos.Lector);

                return Mappers.ContactoMapper.EntidadAModelo(entidad);
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

        public void Agregar(Dominio.Modelos.ContactoModelo contacto)
        {
            throw new NotImplementedException();
        }

        public void Modificar(Dominio.Modelos.ContactoModelo contacto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                Entidades.ContactoEntidad entidad = Mappers.ContactoMapper.ModeloAEntidad(contacto);
                datos.SetearConsulta("UPDATE [dbo].[CONTACTOS] SET nombre_apellido = @nombre_apellido, tipo = @tipo, telefono = @telefono, correo = @correo, direccion = @direccion, fuente = @fuente, producto_que_provee = @producto_que_provee, desea_recibir_correos = @desea_recibir_correos, desea_recibir_whatsapp = @desea_recibir_whatsapp, informacion_personal = @informacion_personal WHERE id_contacto = @id_contacto");
                datos.SetearParametro("@id_contacto", entidad.id_contacto);
                datos.SetearParametro("@nombre_apellido", entidad.nombre_apellido);
                datos.SetearParametro("@tipo", entidad.tipo);
                datos.SetearParametro("@telefono", entidad.telefono);
                datos.SetearParametro("@correo", entidad.correo);
                datos.SetearParametro("@direccion", entidad.direccion);
                datos.SetearParametro("@fuente", entidad.fuente);
                // validar si es nulo el producto que provee
                if (entidad.producto_que_provee == null)
                {
                    datos.SetearParametro("@producto_que_provee", DBNull.Value);
                }
                else
                {
                    datos.SetearParametro("@producto_que_provee", entidad.producto_que_provee);
                }
                datos.SetearParametro("@desea_recibir_correos", entidad.desea_recibir_correos);
                datos.SetearParametro("@desea_recibir_whatsapp", entidad.desea_recibir_whatsapp);
                // validar informacion personal
            if (entidad.informacion_personal == null)
                {
                    datos.SetearParametro("@informacion_personal", DBNull.Value);
                }
                else
                {
                    datos.SetearParametro("@informacion_personal", entidad.informacion_personal);
                }

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
            throw new NotImplementedException();
        }


    }
}
