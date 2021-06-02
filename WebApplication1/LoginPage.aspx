<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="WebApplication1.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        #Msg{
           color:red!important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            帳號:<asp:TextBox ID="Account" runat="server"></asp:TextBox>
            密碼:<asp:TextBox ID="Pwd" runat="server"></asp:TextBox>
            <asp:Button ID="Login" runat="server" Text="Login" OnClick="Login_Click"/>
            <asp:Label ID="Msg" runat="server" Text="帳號或密碼錯誤"></asp:Label>
        </div>
    </form>
</body>
</html>
