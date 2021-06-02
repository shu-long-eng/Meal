using System.Web;
using WebApplication1.Manager;

namespace WebApplication1.Helpers
{
    public class LoginHelper
    {
        private const string _sessionKey = "IsLogined";
        public static bool HasLogined()//判斷是否有登入
        {
            var val = HttpContext.Current.Session[_sessionKey] as LoginInfo;
            if (val == null)
                return false; 
            else
                return true;      
        }
        public static bool TryLogin(string Account,string Pwd)
        {
            if (LoginHelper.HasLogined())
            {
                return true;
            }

            //讀取DB以及檢察帳號密碼是否正確/存在
            AccountManager manager = new AccountManager();
            var model = manager.GetAccount(Account);
            if (model == null ||
                string.Compare(Pwd.Trim(), model.Pwd.Trim(), false) != 0)
                return false;

            HttpContext.Current.Session[_sessionKey] = new LoginInfo()
            {
                ID = model.ID,
                GroupID = model.GroupID,
                ImageUrl = model.ImageUrl,
                AccountName = model.AccountName
                
            };

            return true;

        }
        /// <summary> 登出目前使用者，如果還沒登入就不執行 </summary>
        public static void Logout()
        {
            if (!LoginHelper.HasLogined())
                return;

            HttpContext.Current.Session.Remove(_sessionKey);
        }

        /// <summary> 取得已登入者的資訊，如果還沒登入回傳 NULL </summary>
        /// <returns></returns>
        public static LoginInfo GetCurrentUserInfo()
        {
            if (!LoginHelper.HasLogined())
                return null;

            return HttpContext.Current.Session[_sessionKey] as LoginInfo;
        }
    }
}