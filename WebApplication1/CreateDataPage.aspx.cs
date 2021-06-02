using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Helpers;

namespace WebApplication1
{
    public partial class CreateDataPage : System.Web.UI.Page
    {
        private const string _sessionKey = "IsLogined";
        private string path;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Helpers.LoginHelper.HasLogined())//判斷是否登入，如果未登入跳到HomePage
            {
                Response.Redirect("~/HomePage.aspx");
            }

            //string imageName = this.ImageName.SelectedValue; //取得預設圖片
            //this.ShowImage.ImageUrl = "~/Image/"+ imageName; 
            this.Msg.Visible = false; //隱藏錯誤訊息
        }

        protected void OKBtn_Click(object sender, EventArgs e)
        {
            //string imageName = this.ImageName.SelectedValue; //取得圖片名稱
            string imageUrl = GetPic();
            int ShopID = 0; 
            Int32.TryParse(this.ShopName.SelectedValue,out ShopID);//取得ShopID的Value
            LoginInfo AccountInfo = (LoginInfo)HttpContext.Current.Session[_sessionKey];//讀取session的帳號資訊
            int AccountID = AccountInfo.ID;
            string GroupName = this.GroupNameText.Text;//取得GroupName值
            if (!string.IsNullOrWhiteSpace(GroupName))
            {
                Manager.DBbase.InsertGroup(GroupName, AccountID, ShopID, imageUrl);
                Response.Write("<script>alert('新增完成');</script>");
                Response.Write("<script>window.location.href='HomePage.aspx';</script>");
            }
            else
            {
                this.Msg.Visible = true;//如果為空值顯示錯誤訊息
            }



        }

        protected void ResetBtn_Click(object sender, EventArgs e)
        {
            this.GroupNameText.Text = "";
            this.ShopName.SelectedIndex = 0;
            //this.ImageName.SelectedValue = "Kiwi.png";
            //this.ShowImage.ImageUrl = "~/Image/Kiwi.png";
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage.aspx");
        }


        private string GetPicName()
        {
            string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();//取得副檔名
            string _saveFolder = "~/Image";
            var file = FileUpload1.PostedFile;
            path = Server.MapPath(_saveFolder);
            string newFileName = Guid.NewGuid().ToString() + fileExtension;
            string fullPath = System.IO.Path.Combine(path, newFileName);
            file.SaveAs(fullPath);
            return newFileName;
        }
        private string GetPic()
        {
            bool fileOk = false;
            
            if (this.FileUpload1.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();//取得副檔名
                string[] allowExtension = { ".jpg",".png",".gif"};//允許的附檔名

                for(var i = 0; i < allowExtension.Length; i++)
                {
                    if(fileExtension == allowExtension[i])
                    {
                        fileOk = true;
                        break;
                    }
                }

                if (fileOk)//如果附檔名正確回傳路徑
                {
                    
                    return GetPicName();
                }
                else
                {
                    Response.Write("<script>alert('請確認檔案格式');</script>");
                    return null;
                }

            }
            else
            {
                Response.Write("<script>alert('請確認檔案是否上傳');</script>");
                return null;
            }
        }

        
    }
}