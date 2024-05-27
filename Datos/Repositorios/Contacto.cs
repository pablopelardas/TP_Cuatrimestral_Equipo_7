using Datos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class Contacto
    {
        public List<Entidades.Contacto> Listar()
        {
            List<Entidades.Contacto> contactoEntidades = new List<Entidades.Contacto>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT * FROM [dbo].[CONTACTOS]");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Entidades.Contacto entidad = new Entidades.Contacto();
                    entidad.Id = (int)datos.Lector["Id"];
                    entidad.NombreApellido = (string)datos.Lector["NombreApellido"];
                    entidad.Tipo = (string)datos.Lector["Tipo"];
                    entidad.Telefono = (string)datos.Lector["Telefono"];
                    entidad.Correo = (string)datos.Lector["Correo"];
                    entidad.Direccion = (string)datos.Lector["Direccion"];
                    entidad.Fuente = (string)datos.Lector["Fuente"];
                    entidad.ProductoQueProvee = (string)datos.Lector["ProductoQueProvee"];
                    entidad.DeseaRecibirCorreos = (bool)datos.Lector["DeseaRecibirCorreos"];
                    entidad.DeseaRecibirWhatsapp = (bool)datos.Lector["DeseaRecibirWhatsapp"];

                    contactoEntidades.Add(entidad);
                }
                return contactoEntidades;
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

        public void Agregar(Datos.Entidades.Contacto contactoEntidad)
        {
            throw new NotImplementedException();
        }

        public void Modificar(Datos.Entidades.Contacto contactoEntidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Datos.Entidades.Contacto contactoEntidad)
        {
            throw new NotImplementedException();
        }


    }
}
