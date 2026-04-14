using System;
using System.IO;
using MySql.Data.MySqlClient;
namespace Quality.Infrastructure
{
    public static class DatabaseInitializer
    {
        public static void EnsureDatabase()
        {
            EnsureDatabaseExists();
            ExecuteScript("Scripts\\init.sql");
            var count = Convert.ToInt32(MySqlDb.ExecuteScalar("SELECT COUNT(*) FROM users;"));
            if (count == 0) ExecuteScript("Scripts\\seed.sql");
        }
        private static void EnsureDatabaseExists()
        {
            using (var c = new MySqlConnection(DbConfig.MasterConnectionString))
            using (var cmd = c.CreateCommand())
            {
                c.Open();
                cmd.CommandText = $"CREATE DATABASE IF NOT EXISTS `{DbConfig.Database}` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;";
                cmd.ExecuteNonQuery();
            }
        }
        private static void ExecuteScript(string relativePath)
        {
            var full = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            if (!File.Exists(full)) return;
            var sql = File.ReadAllText(full);
            using (var c = MySqlDb.CreateConnection())
            {
                c.Open();
                foreach (var part in sql.Split(new[] { ";;" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var text = part.Trim();
                    if (string.IsNullOrWhiteSpace(text)) continue;
                    using (var cmd = new MySqlCommand(text, c)) cmd.ExecuteNonQuery();
                }
            }
        }
    }
}