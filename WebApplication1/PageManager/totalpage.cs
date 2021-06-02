using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.PageManager
{
    public class totalpage
    {
        public static int totalsize()
        {
            string constr = Helpers.GetConnectionString.GetConnection();
            string querystr = $"select count(ID)as size from GroupName ;";
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand command = new SqlCommand(querystr, con);

                try
                {
                    con.Open();
                    var totalsize = command.ExecuteScalar();

                    return Convert.ToInt32(totalsize);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
    }
}