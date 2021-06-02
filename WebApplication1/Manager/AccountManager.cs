using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using WebApplication1.Models;
using WebApplication1.Helpers;

namespace WebApplication1.Manager
{
    public class AccountManager
    {
        public AccountModel GetAccount(string Account)
        {
            string connectionstr = GetConnectionString.GetConnection(); //GetConnectionString為取得連線字串方法
            string querystr = "select * from Account where Account = @Account;";
            using(SqlConnection con = new SqlConnection(connectionstr))
            {
                SqlCommand command = new SqlCommand(querystr, con);
                command.Parameters.AddWithValue("@Account", Account);
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    AccountModel model = null;

                    while (reader.Read()) //取得SQL資料存入Model
                    {
                        model = new AccountModel();
                        model.ID = (int)reader["ID"];
                        model.Pwd = (string)reader["Pwd"];
                        model.ImageUrl = (string)reader["ImageUrl"];
                        model.Account = (string)reader["Account"];
                        model.AccountName = (string)reader["AccountName"];
                    }
                    reader.Close();
                    con.Close();
                    return model;
                }
                catch (Exception e)
                {
                    HttpContext.Current.Response.Write(e);
                    return null;                    
                }
            }
        }
        
    }

}