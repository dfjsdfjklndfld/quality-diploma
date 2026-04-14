using MySql.Data.MySqlClient;
using Quality.Infrastructure;
using Quality.Models;
namespace Quality.Services
{
    public class AuthService
    {
        public User Login(string login, string password)
        {
            const string sql = @"SELECT u.id, u.login, u.full_name, u.role_name, u.department_id FROM users u WHERE u.login=@login AND u.password_hash=SHA2(@password,256) AND u.is_active=1 LIMIT 1;";
            using (var c = MySqlDb.CreateConnection())
            using (var cmd = new MySqlCommand(sql, c))
            {
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", password);
                c.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return null;
                    return new User
                    {
                        Id = r.GetInt32("id"),
                        Login = r.GetString("login"),
                        FullName = r.GetString("full_name"),
                        RoleName = r.GetString("role_name"),
                        DepartmentId = r.IsDBNull(r.GetOrdinal("department_id")) ? (int?)null : r.GetInt32("department_id")
                    };
                }
            }
        }
    }
}