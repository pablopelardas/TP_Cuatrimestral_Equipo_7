using System;
using System.Collections.Generic;
using System.Data;
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
        private string _connectionString = "Data Source=localhost,15000;Initial Catalog=tp-cuatrimestral-grupo-7;User Id=sa;Password=Pablo2846!;TrustServerCertificate=True";
        //private string _connectionString = "server = .\\SQLEXPRESS; database = tp-cuatrimestral-grupo-7; integrated security = true";

        public AccesoDatos(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public void ExecuteNonQuery(string query)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ExecuteNonQuery(SqlCommand command)
        {
            using (SqlConnection connection = GetConnection())
            {
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int ExecuteScalar(string query)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                {
                    connection.Open();
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public int ExecuteScalar(SqlCommand command)
        {
            using (SqlConnection connection = GetConnection())
            {
                command.Connection = connection;
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        public DataTable ExecuteQuery (string query)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
        }
        public DataTable ExecuteQuery(SqlCommand command)
        {
            using (SqlConnection connection = GetConnection())
            {
                command.Connection = connection;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
        public DataTable Transaction (List<string> queries)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    try
                    {
                        foreach (string query in queries)
                        {
                            command.CommandText = query;
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            return ExecuteQuery("SELECT @@IDENTITY");
        }

        public DataTable Transaction(List<SqlCommand> cmds)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    try
                    {
                        foreach (SqlCommand cmd in cmds)
                        {
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;
                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            return ExecuteQuery("SELECT @@IDENTITY");
        }

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

            //conexion = new SqlConnection("server = .\\SQLEXPRESS; database = tp-cuatrimestral-grupo-7; integrated security = true");
            // CONN PABLO P
            conexion = new SqlConnection("Data Source=localhost,15000;Initial Catalog=tp-cuatrimestral-grupo-7;User Id=sa;Password=Pablo2846!;TrustServerCertificate=True");
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
