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
            entidad.informacion_personal = (string)reader["informacion_personal"];
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
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }


    }
}
