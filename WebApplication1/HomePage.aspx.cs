using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Manager;
using WebApplication1.PageManager;

namespace WebApplication1
{
    public partial class HomePage : System.Web.UI.Page
    {
        private int _pageSize = 2;
        internal class PagingLink
        {
            public string Name { get; set; }
            public string Link { get; set; }
            public string Title { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Helpers.LoginHelper.HasLogined()) //判斷有無登入
            {
                this.CreateBtn.Visible = true;
            }
            else
            {
                this.CreateBtn.Visible = false;
            }
            LoadGridView();
            this.PagePlaceHolder.Visible = true;
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CreateDataPage.aspx");//跳轉到創建頁
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            string name = this.SearchTextBox.Text;
            this.GroupRepeater.DataSource = DBbase.GetGroup(name);
            this.GroupRepeater.DataBind();
            this.PagePlaceHolder.Visible = false;
        }

        private void LoadGridView()
        {
            //----- Get Query string parameters -----
            string page = Request.QueryString["Page"];//取得當前頁數
            int pIndex = 0;
            if (string.IsNullOrEmpty(page))
                pIndex = 1;
            else
            {
                int.TryParse(page, out pIndex);

                if (pIndex <= 0)
                    pIndex = 1;
            }

           

            int totalSize = totalpage.totalsize();

            DataTable dt = DBbase.GetGroup(_pageSize, pIndex);//取得分頁的資料

            int pages = CalculatePages.myCalculatePages(totalSize, _pageSize);//取得分頁頁數

            List<PagingLink> pagingList = new List<PagingLink>();

            

            for (var i = 1; i <= pages; i++)
            {
                pagingList.Add(new PagingLink()
                {
                    Link = $"HomePage.aspx{this.GetQueryString(false, i)}",
                    Name = $"{i}",
                    Title = $"前往第 {i} 頁"
                });


            }

            this.repPaging.DataSource = pagingList;
            this.repPaging.DataBind();

            this.GroupRepeater.DataSource = dt;
            this.GroupRepeater.DataBind();
        }


        private string GetQueryString(bool includePage, int? pageIndex)
        {
            //----- Get Query string parameters -----
            string page = Request.QueryString["Page"];


            //----- Get Query string parameters -----


            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(page) && includePage)
                conditions.Add("Page=" + page);

            if (pageIndex.HasValue)
                conditions.Add("Page=" + pageIndex.Value);

            string retText =
                (conditions.Count > 0)
                    ? "?" + string.Join("&", conditions)
                    : string.Empty;

            return retText;
        }

        protected void GroupRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dr = e.Item.DataItem as DataRowView;
            int GroupID = Convert.ToInt32(dr["ID"]);
            Repeater repeater = e.Item.FindControl("Repeater1") as Repeater;
            repeater.DataSource = DBbase.GetMemberCount(GroupID);
            repeater.DataBind();
        }
    }
}