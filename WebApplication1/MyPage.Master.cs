using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Helpers;

namespace WebApplication1
{
    public partial class MyPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            

            if(!Helpers.LoginHelper.HasLogined())
            {
                this.LoginBtn.Visible = true;
                this.LogoutBtn.Visible = false;
            }
            else
            {
                this.LoginBtn.Visible = false;
                this.LogoutBtn.Visible = true;

            }
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            string targetUrl =
                    "~/LoginPage.aspx";

            Response.Redirect(targetUrl); 
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            Helpers.LoginHelper.Logout();

            string targetUrl =
                   "~/HomePage.aspx";

            Response.Redirect(targetUrl);

        }
    }
}