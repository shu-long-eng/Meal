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
        int ShopID;
        int totalcount =0;
        protected void Page_init(object sender, EventArgs e)
        {
            if (!LoginHelper.HasLogined())
            {
                Response.Redirect("~/LoginPage.aspx");
            }

            DataTable DetailDT = DBbase.GetGroup(_groupID);
            this.GroupRepeater.DataSource = DetailDT;
            this.GroupRepeater.DataBind();
             ShopID = (int)DetailDT.Rows[0]["ShopID"];
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
                    count = (Convert.ToInt32(count) + Convert.ToInt32(menulist[i].MealCount)).ToString();
                    menulist[i].MealCount = count;
                }else if (arr[0] != menulist[i].MealName && menulist.Count<menutypecount)
                {
                    menulist.Add(new MenuModel() { AccountID = model.ID, GroupID = _groupID, MeunID = arr[1], ShopID = ShopID, MealCount = count, MealName = arr[0] });
                    break;
                }
                
            }

            if (menulist.Count == 0) { 
            menulist.Add(new MenuModel() { AccountID = model.ID, GroupID = _groupID, MeunID = arr[1], ShopID = ShopID, MealCount = count, MealName = arr[0] });
            }

            for (var i = 0; i < menulist.Count; i++)
            {
                _menulist += menulist[i].MealName + "*" + menulist[i].MealCount;
            }
            this.Literal1.Text = _menulist;




            //for (var i = 0; i < menulist.Count; i++)
            //{
            //    if (arr[0] == menulist[i].MealName)
            //    {
            //        count = (Convert.ToInt32(count) + Convert.ToInt32(menulist[i].MealCount)).ToString();
            //        menulist[i].MealCount = count;

            //    }
            //}
            //if(menulist.Count == 0) { 
            //menulist.Add(new MenuModel() { AccountID = model.ID, GroupID = _groupID, MeunID = arr[1], ShopID = ShopID, MealCount = count, MealName = arr[0] });
            //}

            //for (var i=0; i < menulist.Count; i++) { 
            //_menulist += menulist[i].MealName + "*" + menulist[i].MealCount;
            //}





            //menulist.Add(new MenuModel() { AccountID = model.ID, GroupID = _groupID, MeunID = arr[1], ShopID = ShopID, MealCount = count, MealName = arr[0] });



            //for (var i = 0; i < menulist.Count; i++)
            //{

            //    _menulist += menulist[i].MealName + "*" + menulist[i].MealCount;

            //}
            //this.Literal1.Text = _menulist;
        }
        protected void MenuRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
            
        }

       
    }
}