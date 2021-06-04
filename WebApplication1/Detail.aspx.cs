using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Helpers;
using WebApplication1.Manager;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class Detail : System.Web.UI.Page
    {
        private int _groupID = Convert.ToInt32(HttpContext.Current.Request.QueryString["ID"]);//取得GroupID
        static List<MenuModel> menulist = new List<MenuModel>();
        private string _menulist="";
        int ShopID = Convert.ToInt32(HttpContext.Current.Request.QueryString["ShopID"]);
        protected void Page_init(object sender, EventArgs e)
        {
            if (!LoginHelper.HasLogined())
            {
                Response.Redirect("~/LoginPage.aspx");
            }
            //從DB取得畫面資料
            DataTable DetailDT = DBbase.GetGroup(_groupID);
            this.GroupRepeater.DataSource = DetailDT;
            this.GroupRepeater.DataBind();
            
            this.MenuRepeater.DataSource = DBbase.GetMenu(ShopID);
            this.MenuRepeater.DataBind();
            var lll = this.MenuRepeater.FindControl("CountList") as DropDownList;

            var model = Helpers.LoginHelper.GetCurrentUserInfo();

            List<AccountModel> acclist = new List<AccountModel>(); ;
            if (model == null)
            {
                acclist.Add(new AccountModel { AccountName = "未登入", ImageUrl = "NoAcc.png" });
            }
            else
            {
                acclist.Add(new AccountModel { AccountName = model.AccountName, ImageUrl = model.ImageUrl });
            }


            this.Image1.ImageUrl = "~/Image/" + acclist[0].ImageUrl;
            this.MemberRepeater.DataSource = DBbase.GetOrderAccount(_groupID);
            this.MemberRepeater.DataBind();

            
            this.SubTotalRepeater.DataSource = DBbase.GetSubTotal(_groupID);
            this.SubTotalRepeater.DataBind();
            this.SubTotal.Text = DBbase.GetSubTotalMoney(_groupID).ToString();

            if(DetailDT.Rows[0]["StatusID"].ToString() != "0")
            {
                this.Button2.Enabled = false;
            }

            
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage.aspx");
            
        }

        protected void CountList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            var model = Helpers.LoginHelper.GetCurrentUserInfo();
            var droplist = sender as DropDownList;
            string ss = droplist.ToolTip;
            string[] arr = ss.Split(',');
            string count = droplist.SelectedValue;
            int menutypecount = DBbase.GetMenuCount(ShopID);

            for (var i = 0; i < menulist.Count; i++)
            {
                if (arr[0] == menulist[i].MealName)
                {
                    
                    menulist[i].MealCount = count;
                    menulist[i].total = Convert.ToInt32(count) * Convert.ToInt32(arr[2]);
                }
                else if (arr[0] != menulist[i].MealName && menulist.Count < menutypecount)
                {
                    menulist.Add(new MenuModel() { AccountID = model.ID, GroupID = _groupID, MeunID = arr[1], ShopID = ShopID, MealCount = count, MealName = arr[0],total = Convert.ToInt32(count) * Convert.ToInt32(arr[2]) });
                    break;
                }

            }
            if (menulist.Count == 0)
            {
                menulist.Add(new MenuModel() { AccountID = model.ID, GroupID = _groupID, MeunID = arr[1], ShopID = ShopID, MealCount = count, MealName = arr[0],total = Convert.ToInt32(count)* Convert.ToInt32(arr[2]) });
            }
            int total = 0;
            for (var i = 0; i < menulist.Count; i++)
            {
                _menulist += menulist[i].MealName + "*" + menulist[i].MealCount;
                total += menulist[i].total;
            }
            this.Literal1.Text = _menulist + $"總額:{total}";

        }
        protected void MenuRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < menulist.Count; i++)
            {
                DBbase.InsertOrder(menulist[i].AccountID,Convert.ToInt32(menulist[i].MeunID), 
                    menulist[i].GroupID, menulist[i].ShopID,menulist[i].MealName
                    ,Convert.ToInt32(menulist[i].MealCount),menulist[i].total);
            }
            menulist.Clear();
            string targetUrl = $"~/Detail.aspx?ID={_groupID}&ShopID={ShopID}";
            Response.Redirect(targetUrl);
        }

        protected void MemberRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dr = e.Item.DataItem as DataRowView;
            int AccountID = Convert.ToInt32(dr["AccountID"]);
            Repeater repeater = e.Item.FindControl("Repeater1") as Repeater;
            repeater.DataSource = DBbase.GetMemberOrder(AccountID, _groupID);
            repeater.DataBind();
        }

        protected void MemberRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            
            int AccountID = Convert.ToInt32(e.CommandArgument);
            DBbase.DeleteOrder(AccountID, _groupID);
            string targetUrl = $"~/Detail.aspx?ID={_groupID}&ShopID={ShopID}";
            Response.Redirect(targetUrl);
        }

        protected void GroupRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            DropDownList list = e.Item.FindControl("StatusList") as DropDownList;
            int status = Convert.ToInt32(list.SelectedValue);
            DBbase.SetGroupStatus(status, _groupID);

            Response.Redirect(Request.Url.ToString());
        }

        protected void GroupRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DropDownList downList = e.Item.FindControl("StatusList") as DropDownList;
            DataTable DetailDT = DBbase.GetGroup(_groupID);
            string StatusID = "0";
            if(DetailDT.Rows.Count == 0)
            {
                StatusID = "0";
            }
            else
            {
                StatusID = DetailDT.Rows[0]["StatusID"].ToString();
            }
            
            downList.SelectedValue = StatusID;

            
        }
    }
}