using MySql.Data.MySqlClient;
using Quality.Infrastructure;

namespace Quality.Services
{
    public class AuditService
    {
        public void Log(string actionName, string entityName, int? entityId, string details)
        {
            using (var c = MySqlDb.CreateConnection())
            using (var cmd = c.CreateCommand())
            {
                c.Open();
                cmd.CommandText = @"INSERT INTO audit_log(user_id, action_name, entity_name, entity_id, details) VALUES(@user_id, @action_name, @entity_name, @entity_id, @details);";
                cmd.Parameters.AddWithValue("@user_id", AppSession.CurrentUser == null ? (object)System.DBNull.Value : AppSession.CurrentUser.Id);
                cmd.Parameters.AddWithValue("@action_name", actionName);
                cmd.Parameters.AddWithValue("@entity_name", entityName);
                cmd.Parameters.AddWithValue("@entity_id", entityId.HasValue ? (object)entityId.Value : System.DBNull.Value);
                cmd.Parameters.AddWithValue("@details", string.IsNullOrWhiteSpace(details) ? (object)System.DBNull.Value : details);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
