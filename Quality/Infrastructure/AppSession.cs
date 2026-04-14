using Quality.Models;
namespace Quality.Infrastructure
{
    public static class AppSession
    {
        public static User CurrentUser { get; set; }
    }
}