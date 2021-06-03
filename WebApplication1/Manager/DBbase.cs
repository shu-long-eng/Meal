using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1.Manager
{
    public class DBbase
    {
        public static void InsertGroup(string Name, int AccountID, int ShopID, string ImageUrl)
        {
            string connstr = Helpers.GetConnectionString.GetConnection();
            string querystr = "INSERT INTO GroupName (Name, AccountID, ShopID, ImageUrl) VALUES (@Name, @AccountID, @ShopID, @ImageUrl);";
            using (SqlConnection con = new SqlConnection(connstr))
            {
                SqlCommand command = new SqlCommand(querystr, con);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@AccountID", AccountID);
                command.Parameters.AddWithValue("@ShopID", ShopID);
                command.Parameters.AddWithValue("@ImageUrl", ImageUrl);
                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception e)
                {
                    HttpContext.Current.Response.Write(e);
                }
            }
        }
        public static DataTable GetGroup(int pageSize, int currentPage)
        {
            string connstr = Helpers.GetConnectionString.GetConnection();
            string querystr = $@"select top ({pageSize}) a.ID,a.[Name] as GroupName,Account.[AccountName] as AccountName,Shop.[Name] as ShopName,a.ImageUrl,ShopID from
                                 (select GroupName.ID,GroupName.[Name],AccountID,ShopID,ImageUrl ,ROW_NUMBER() OVER (ORDER by id ) as rowid from GroupName)  a  
                                 left join Account on Account.ID = AccountID 
                                 left join Shop on Shop.ID = ShopID
                                 where  rowid > {pageSize}*({currentPage}-1);";
            using (SqlConnection con = new SqlConnection(connstr))
            {
                SqlCommand command = new SqlCommand(querystr, con);
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();
                    con.Close();
                    return dt;
                }
                catch (Exception e)
                {
                    HttpContext.Current.Response.Write(e);
                    return null;
                }
            }
        }
        public static DataTable GetGroup(string name)
        {
            string connstr = Helpers.GetConnectionString.GetConnection();
            string querystr = $@"SELECT GroupName.ID,GroupName.[Name] as GroupName,account.ImageUrl,AccountName as AccountName
                                 ,Shop.Name as ShopName,ShopID FROM GroupName
                                 left join Account on Account.ID = AccountID
                                 left join Shop on Shop.ID = ShopID
                                 WHERE GroupName.Name like @Name;";
            using (SqlConnection con = new SqlConnection(connstr))
            {
                SqlCommand command = new SqlCommand(querystr, con);
                command.Parameters.AddWithValue("@Name", '%' + name + '%');
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();
                    con.Close();
                    return dt;
                }
                catch (Exception e)
                {
                    HttpContext.Current.Response.Write(e);
                    return null;
                }
            }
        }
        public static DataTable GetGroup(int ID)
        {
            string connstr = Helpers.GetConnectionString.GetConnection();
            string querystr = $@"SELECT GroupName.ID,GroupName.Name as GroupName,GroupName.ImageUrl,Account.AccountName as AccountName,Shop.Name as ShopName,Shop.ID as ShopID,MealCount.Count FROM GroupName
                                 left join Account on Account.ID = AccountID
                                 left join Shop on Shop.ID = ShopID
                                 
                                 WHERE GroupName.ID = {ID};";
            using (SqlConnection con = new SqlConnection(connstr))
            {
                SqlCommand command = new SqlCommand(querystr, con);
                
                
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();
                    con.Close();
                    return dt;
                }
                catch (Exception e)
                {
                    HttpContext.Current.Response.Write(e);
                    return null;
                }
            }
        }
        public static DataTable GetMenu(int ID)
        {
            string connstr = Helpers.GetConnectionString.GetConnection();
            string querystr = @"select *,ID as MenuID from Menu where ShopID = @ID;";
            using (SqlConnection con = new SqlConnection(connstr))
            {
                SqlCommand command = new SqlCommand(querystr, con);
                command.Parameters.AddWithValue("@ID", ID);
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();
                    con.Close();
                    return dt;
                }
                catch (Exception e)
                {
                    HttpContext.Current.Response.Write(e);
                    return null;
                }
            }
        }
        public static int GetMenuCount(int ID)
        {
            string connstr = Helpers.GetConnectionString.GetConnection();
            string querystr = @"select count('ID') from Menu where ShopID = @ID;";
            using (SqlConnection con = new SqlConnection(connstr))
            {
                SqlCommand command = new SqlCommand(querystr, con);
                command.Parameters.AddWithValue("@ID", ID);
                try
                {
                    con.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count;
                }
                catch (Exception e)
                {
                    HttpContext.Current.Response.Write(e);
                    return 0;
                }
            }
        }
        public static void InsertOrder(int AccountID, int MeunID, int GroupID, int ShopID,string MenuName, int MenuCount,int TotalPrice)
        {
            string connstr = Helpers.GetConnectionString.GetConnection();
            string query = @"Insert into [Order] (AccountID, MeunID, GroupID, ShopID,MenuName,MenuCount,TotalPrice,Isdelete) values (@AccountID,@MeunID, @GroupID, @ShopID,@MenuName,@MenuCount,@TotalPrice,'False');";
            using(SqlConnection con = new SqlConnection(connstr))
            {
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@AccountID", AccountID);
                command.Parameters.AddWithValue("@MeunID", MeunID);
                command.Parameters.AddWithValue("@GroupID", GroupID);
                command.Parameters.AddWithValue("@ShopID", ShopID);
                command.Parameters.AddWithValue("@MenuName", MenuName);
                command.Parameters.AddWithValue("@MenuCount", MenuCount);
                command.Parameters.AddWithValue("@TotalPrice", TotalPrice);
                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public static DataTable GetOrderAccount(int ID)
        {
            string connstr = Helpers.GetConnectionString.GetConnection();
            string querystr = @"select [Order].Isdelete,AccountID,Account.ImageUrl,GroupID from [Order]  left join Account on Account.ID = AccountID   where Isdelete = 'False' and GroupID = @ID  group by AccountID,Account.ImageUrl,[Order].Isdelete,GroupID;";
            using(SqlConnection con = new SqlConnection(connstr))
            {
                SqlCommand command = new SqlCommand(querystr, con);
                command.Parameters.AddWithValue("@ID", ID);
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();
                    con.Close();
                    return dt;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public static DataTable GetMemberOrder(int AccountID,int GroupID)
        {
            string connstr = Helpers.GetConnectionString.GetConnection();
            string querystr = $@"select Sum(MenuCount) as [count],SUM(TotalPrice) as Price, Account.ID as accountID,MenuName
                                 from [Order] left join Account on Account.ID = AccountID where AccountID = @AccountID and GroupID = @GroupID  and Isdelete = 'false' group by 
                                 MeunID,Account.ID,[Order].Price,[Order].MenuName;";
            using(SqlConnection con = new SqlConnection(connstr))
            {
                SqlCommand command = new SqlCommand(querystr, con);
                command.Parameters.AddWithValue("@AccountID", AccountID);
                command.Parameters.AddWithValue("@GroupID", GroupID);
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    con.Close();
                    return dt;
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
        }
        public static void DeleteOrder(int AccountID, int GroupID)
        {
            string connstr = Helpers.GetConnectionString.GetConnection();
            string queystr = $@"UPDATE [Order] SET Isdelete = 'true' 
            where AccountID = {AccountID} and GroupID = {GroupID}";
            using(SqlConnection con = new SqlConnection(connstr))
            {
                SqlCommand command = new SqlCommand(queystr, con);
                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}