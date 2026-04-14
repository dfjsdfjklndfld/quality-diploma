using System.Configuration;
namespace Quality.Infrastructure
{
    public static class DbConfig
    {
        public static string Server => ConfigurationManager.AppSettings["Server"] ?? "127.0.0.1";
        public static string Port => ConfigurationManager.AppSettings["Port"] ?? "3306";
        public static string Database => ConfigurationManager.AppSettings["Database"] ?? "quality_db";
        public static string User => ConfigurationManager.AppSettings["User"] ?? "root";
        public static string Password => ConfigurationManager.AppSettings["Password"] ?? "";
        public static string MasterConnectionString => $"Server={Server};Port={Port};Uid={User};Pwd={Password};SslMode=Disabled;AllowUserVariables=True;";
        public static string ConnectionString => $"Server={Server};Port={Port};Database={Database};Uid={User};Pwd={Password};SslMode=Disabled;AllowUserVariables=True;Convert Zero Datetime=True;";
    }
}