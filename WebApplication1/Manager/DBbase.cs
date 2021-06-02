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
            string querystr = $@"select top ({pageSize}) a.ID,a.[Name] as GroupName,Account.[AccountName] as AccountName,Shop.[Name] as ShopName,MealCount.[Count],a.ImageUrl from
                                 (select GroupName.ID,GroupName.[Name],AccountID,ShopID,CountID,ImageUrl ,ROW_NUMBER() OVER (ORDER by id ) as rowid from GroupName)  a  
                                 left join Account on Account.ID = AccountID 
                                 left join Shop on Shop.ID = ShopID
                                 left join MealCount on MealCount.ID = CountID
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
            string querystr = $@"SELECT GroupName.ID,GroupName.Name as GroupName,ImageUrl,Account.Name as AccountName,Shop.Name as ShopName,MealCount.Count FROM GroupName
                                 left join Account on Account.ID = AccountID
                                 left join Shop on Shop.ID = ShopID
                                 left join MealCount on MealCount.ID = GroupName.CountID
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
                                 left join MealCount on MealCount.ID = GroupName.CountID
                                 WHERE GroupName.ID = @ID;";
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
    }
}