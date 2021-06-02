

namespace WebApplication1.Helpers
{
    public class GetConnectionString
    {
        public static string GetConnection() //從Web.config 中取得連線字串
        {
            var manage = System.Configuration.ConfigurationManager.ConnectionStrings["ConnDB"];

            if (manage == null)
                return string.Empty;
            else
                return manage.ConnectionString;
        }
    }
}