using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class AccesoDatos : IDisposable
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector; }
        }
        public SqlCommand Comando
        {
            get { return comando; }
        }

        public AccesoDatos()
        {
            // CONN PABLO M

            conexion = new SqlConnection("server = .\\SQLEXPRESS; database = tp-cuatrimestral-grupo-7; integrated security = true");
            // CONN PABLO P
            //conexion = new SqlConnection("Data Source=localhost,15000;Initial Catalog=tp-cuatrimestral-grupo-7;User Id=sa;Password=Pablo2846!;TrustServerCertificate=True");
            comando = new SqlCommand();
        }

        public void SetearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void SetearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void EjecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();

            }
            catch (Exception ex)
            {
                CerrarConexion();
                throw ex;
            }
        }

        public void EjecutarAccion()
        {
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CerrarConexion();
                throw ex;
            }
            finally
            {
                CerrarConexion();
            }
        }
        public int EjecutarScalar()
        {
            comando.Connection = conexion;

            try
            {
                if (conexion.State != System.Data.ConnectionState.Open)
                    conexion.Open();
                return (int)comando.ExecuteScalar();
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw ex;
            }
        }

        public void CerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }

        public void Dispose()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
            conexion = null;
            comando = null;
            lector = null;
        }
    }
}
