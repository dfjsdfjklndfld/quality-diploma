using System.Data;
using MySql.Data.MySqlClient;
namespace Quality.Infrastructure
{
    public static class MySqlDb
    {
        public static MySqlConnection CreateConnection() => new MySqlConnection(DbConfig.ConnectionString);
        public static object ExecuteScalar(string sql)
        {
            using (var c = CreateConnection()) using (var cmd = new MySqlCommand(sql, c)) { c.Open(); return cmd.ExecuteScalar(); }
        }
        public static DataTable GetTable(string sql)
        {
            using (var c = CreateConnection()) using (var cmd = new MySqlCommand(sql, c)) using (var ad = new MySqlDataAdapter(cmd))
            { var t = new DataTable(); c.Open(); ad.Fill(t); return t; }
        }
        public static int ExecuteNonQuery(string sql)
        {
            using (var c = CreateConnection()) using (var cmd = new MySqlCommand(sql, c)) { c.Open(); return cmd.ExecuteNonQuery(); }
        }
    }
}