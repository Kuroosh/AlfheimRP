using AltV.Net.Async;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace Alfheim_Roleplay.Database
{
    public class Main
    {
        private static string connectionString;
        public static async void OnResourceStart()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Core.Debug.OutputDebugString("[DATABASE] : connecting to Database...");
                Console.ResetColor();
                string host = "127.0.0.1";
                string user = "Alfheim";
                string pass = "Yc82n?4fk5z#H08lk5z#H08l";
                string db = "Alfheim";
                connectionString = "SERVER=" + host + "; DATABASE=" + db + "; UID=" + user + "; PASSWORD=" + pass + "; SSLMODE=none;";
                await Task.Run(async () =>
                {
                    await AltAsync.Do(() =>
                    {

                    });
                });
                Console.ForegroundColor = ConsoleColor.Green;
                Core.Debug.OutputDebugString("[DATABASE] : Connected!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Core.Debug.CatchExceptions("Database-Exception", ex);
                Console.ForegroundColor = ConsoleColor.Red;
                Core.Debug.OutputDebugString("[DATABASE-ERROR] : Not Connected!");
                Console.ResetColor();
            }
        }

        public static bool LoginAccount(string username, string password)
        {
            try
            {
                bool login = false;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT UID FROM spieler WHERE SpielerName = @SpielerName AND Password = SHA2(@Password, '256') LIMIT 1";
                    command.Parameters.AddWithValue("@SpielerName", username);
                    command.Parameters.AddWithValue("@Password", password);
                    using MySqlDataReader reader = command.ExecuteReader();
                    login = reader.HasRows;
                }
                return login;
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("Login Account", ex); return false; }
        }

        public static void RegisterAccount(string username, string SpielerSocial, string password, string HardwareIdHash, string HardwareIdExHash)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO spieler (SpielerName, SpielerSocial, Password, HardwareIdHash, HardwareIdExHash) VALUES(@SpielerName, @SpielerSocial, SHA2(@Password, '256'), @HardwareIdHash, @HardwareIdExHash)";
                command.Parameters.AddWithValue("@SpielerName", username);
                command.Parameters.AddWithValue("@SpielerSocial", SpielerSocial);
                command.Parameters.AddWithValue("@HardwareIdHash", HardwareIdHash);
                command.Parameters.AddWithValue("@HardwareIdExHash", HardwareIdExHash);
                command.Parameters.AddWithValue("@Password", password);
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("RegisterAccount", ex); }
        }

        public static bool FindAccountByName(string name)
        {
            try
            {
                bool found = false;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT SpielerName FROM spieler WHERE SpielerName = @SpielerName LIMIT 1";
                    command.Parameters.AddWithValue("@SpielerName", name);

                    using MySqlDataReader reader = command.ExecuteReader();
                    found = reader.HasRows;
                }

                return found;
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("FindAccountByName", ex); return false; }
        }

        public static bool FindAccountByHwid(string HardwareIdHash)
        {
            try
            {
                bool found = false;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT HardwareIdHash FROM spieler WHERE HardwareIdHash = @HardwareIdHash LIMIT 1";
                    command.Parameters.AddWithValue("@HardwareIdHash", HardwareIdHash);

                    using MySqlDataReader reader = command.ExecuteReader();
                    found = reader.HasRows;
                }

                return found;
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("FindAccountByHwid", ex); return false; }
        }

        public static bool FindAccountByHardwareIdExHash(string HardwareIdExHash)
        {
            try
            {
                bool found = false;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT HardwareIdHash FROM spieler WHERE HardwareIdExHash = @HardwareIdExHash LIMIT 1";
                    command.Parameters.AddWithValue("@HardwareIdHash", HardwareIdExHash);

                    using MySqlDataReader reader = command.ExecuteReader();
                    found = reader.HasRows;
                }

                return found;
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("FindAccountByHwid", ex); return false; }
        }
        public static bool FindAccountBySocialID(string SpielerSocial)
        {
            try
            {
                bool found = false;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT SpielerSocial FROM spieler WHERE SpielerSocial = @SpielerSocial LIMIT 1";
                    command.Parameters.AddWithValue("@SpielerSocial", SpielerSocial);

                    using MySqlDataReader reader = command.ExecuteReader();
                    found = reader.HasRows;
                }

                return found;
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("FindAccountBySocialID", ex); return false; }
        }
    }
}
