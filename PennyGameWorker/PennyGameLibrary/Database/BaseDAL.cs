using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PennyGameLibrary.Utility;

namespace PennyGameLibrary.Database
{
    public class BaseDal
    {
        private const string ConfigPath = "config.json";
        private static MySqlConnection _connection;
        private static bool _initialized = false;

        //Initialize values
        private static void Initialize()
        {
            var credentials = ServerCredentials.Instance ??
                              Newtonsoft.Json.JsonConvert.DeserializeObject<ServerCredentials>(
                                  File.ReadAllText(ConfigPath));

            string connectionString = "SERVER=" + credentials.Server + ";" + "DATABASE=" +
                                      credentials.Database + ";" + "UID=" + credentials.Uid + ";" + "PASSWORD=" +
                                      credentials.Password + ";";

            _connection = new MySqlConnection(connectionString);

            _initialized = true;
        }

        private static bool OpenConnection()
        {
            if (!_initialized) Initialize();

            try
            {
                _connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Logger.WriteError("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Logger.WriteError("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        private static bool CloseConnection()
        {
            try
            {
                _connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Logger.WriteError(ex.Message);
                return false;
            }
        }

        private static MySqlCommand PrepareCommand(string commandText, Dictionary<string, object> _params = null)
        {
            var cmd = new MySqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = commandText;
            cmd.Prepare();

            if (_params != null)
                foreach (var param in _params)
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
            return cmd;
        }

        protected static bool ExecuteNonQuery(string commandText, Dictionary<string, object> _params = null)
        {
            var result = false;
            try
            {
                OpenConnection();
                var cmd = PrepareCommand(commandText, _params);
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (MySqlException ex)
            {
                Logger.WriteError(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return result;
        }

        protected static MySqlDataReader ExecuteQuery(string commandText, Dictionary<string, object> _params = null)
        {
            MySqlDataReader result = null;
            try
            {
                OpenConnection();
                var cmd = PrepareCommand(commandText, _params);
                result = cmd.ExecuteReader();
            }
            catch (MySqlException ex)
            {
                Logger.WriteError(ex.Message);
            }

            return result;
        }
    }
}
