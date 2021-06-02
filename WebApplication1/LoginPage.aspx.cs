using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Helpers;

namespace WebApplication1
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Msg.Visible = false;
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            string Account = this.Account.Text;
            string Pwd = this.Pwd.Text;
            if (LoginHelper.TryLogin(Account, Pwd))
            {
                Response.Redirect("~/HomePage.aspx");
            }
            else
            {
                this.Msg.Visible = true;
            }
        }
    }
}