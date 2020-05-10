using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCoreTutorial
{
    public static class Common
    {
        public static string tbladminlogin = "SiteUser";
    }
    public class SqlHelper
    {

        private string ConnectionString { get; set; }

        public SqlHelper(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void CloseConnection(System.Data.SqlClient.SqlConnection connection)
        {
            if(connection != null)
            {
                connection.Close();
            }
            
        }

        public SqlParameter CreateParameter(string name, object value, DbType dbType)
        {
            return CreateParameter(name, 0, value, dbType, ParameterDirection.Input);
        }

        internal IDataReader GetDataReader(object p, CommandType text, SqlParameter[] sqlParameter, out SqlConnection conn)
        {
            throw new NotImplementedException();
        }

        public SqlParameter CreateParameter(string name, int size, object value, DbType dbType)
        {
            return CreateParameter(name, size, value, dbType, ParameterDirection.Input);
        }

        public SqlParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection direction)
        {
            return new SqlParameter
            {
                DbType = dbType,
                ParameterName = name,
                Size = size,
                Direction = direction,
                Value = value
            };
        }

        public DataTable GetDataTable(string commandText, CommandType commandType, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            if (parameter.Value == null)
                            {
                                //command.Parameters.Add(parameter);
                                command.Parameters.AddWithValue(parameter.ParameterName, DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.Add(parameter);
                            }
                        }
                    }

                    var dataset = new DataSet();
                    var dataAdaper = new SqlDataAdapter(command);
                    dataAdaper.Fill(dataset);

                    return dataset.Tables[0];
                }
            }
        }

        public DataSet GetDataSet(string commandText, CommandType commandType, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            if (parameter.Value == null)
                            {
                                //command.Parameters.Add(parameter);
                                command.Parameters.AddWithValue(parameter.ParameterName, DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.Add(parameter);
                            }
                        }
                    }

                    var dataset = new DataSet();
                    var dataAdaper = new SqlDataAdapter(command);
                    dataAdaper.Fill(dataset);

                    return dataset;
                }
            }
        }

        public IDataReader GetDataReader(string commandText, CommandType commandType, SqlParameter[] parameters, out SqlConnection connection)
        {
            IDataReader reader = null;
            connection = new SqlConnection(ConnectionString);
            connection.Open();

            var command = new SqlCommand(commandText, connection);
            command.CommandType = commandType;
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    if (parameter.Value == null)
                    {
                        //command.Parameters.Add(parameter);
                        command.Parameters.AddWithValue(parameter.ParameterName, DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.Add(parameter);
                    }
                }
            }

            reader = command.ExecuteReader();

            return reader;
        }

        public void Delete(string commandText, CommandType commandType, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Insert(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public int Insert(string commandText, CommandType commandType, SqlParameter[] parameters, out int lastId)
        {
            lastId = 0;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    object newId = command.ExecuteScalar();
                    lastId = Convert.ToInt32(newId);
                }
            }

            return lastId;
        }

        public long Insert(string commandText, CommandType commandType, SqlParameter[] parameters, out long lastId)
        {
            lastId = 0;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    object newId = command.ExecuteScalar();
                    lastId = Convert.ToInt64(newId);
                }
            }

            return lastId;
        }

        public void InsertWithTransaction(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            SqlTransaction transactionScope = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                transactionScope = connection.BeginTransaction();

                using (var command = new SqlCommand(commandText, connection, transactionScope))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    try
                    {
                        command.ExecuteNonQuery();
                        transactionScope.Commit();
                    }
                    catch (Exception ex)
                    {
                        transactionScope.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public void InsertWithTransaction(string commandText, CommandType commandType, IsolationLevel isolationLevel, SqlParameter[] parameters)
        {
            SqlTransaction transactionScope = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                transactionScope = connection.BeginTransaction(isolationLevel);

                using (var command = new SqlCommand(commandText, connection, transactionScope))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    try
                    {
                        command.ExecuteNonQuery();
                        transactionScope.Commit();
                    }
                    catch (Exception)
                    {
                        transactionScope.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public void Update(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateWithTransaction(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            SqlTransaction transactionScope = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                transactionScope = connection.BeginTransaction();

                using (var command = new SqlCommand(commandText, connection, transactionScope))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    try
                    {
                        command.ExecuteNonQuery();
                        transactionScope.Commit();
                    }
                    catch (Exception ex)
                    {
                        transactionScope.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public void UpdateWithTransaction(string commandText, CommandType commandType, IsolationLevel isolationLevel, SqlParameter[] parameters)
        {
            SqlTransaction transactionScope = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                transactionScope = connection.BeginTransaction(isolationLevel);

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    try
                    {
                        command.ExecuteNonQuery();
                        transactionScope.Commit();
                    }
                    catch (Exception)
                    {
                        transactionScope.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public object GetScalarValue(string commandText, CommandType commandType, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    return command.ExecuteScalar();
                }
            }
        }


        public List<SqlParameter> GetSPParameterList(string SpName)
        {
            List<SqlParameter> inputParamList = new List<SqlParameter>();
            List<SqlParameter> outputParamList = new List<SqlParameter>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlCommandBuilder.DeriveParameters(command);
                    foreach (SqlParameter param in command.Parameters)
                    {
                        if (param.Direction == ParameterDirection.Input || param.Direction == ParameterDirection.InputOutput)
                        {
                            inputParamList.Add(param);
                        }
                        else
                        {
                            outputParamList.Add(param);
                        }
                    }
                }
            }

            return inputParamList;


        }


    }
}
