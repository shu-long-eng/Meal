<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateDataPage.aspx.cs" Inherits="WebApplication1.CreateDataPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        #pic {
            margin-top: 10px;
            height: 100px;
            width: 100px;
            display:inline-block;
        }
        #Msg{
            color:red;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            團名:<asp:TextBox ID="GroupNameText" runat="server"></asp:TextBox>
            店名:<asp:DropDownList ID="ShopName" runat="server">
                <asp:ListItem Text="店一" Value="1">店一</asp:ListItem>
                <asp:ListItem Text="店二" Value="2">店二</asp:ListItem>
                <asp:ListItem Text="店三" Value="3">店三</asp:ListItem>
            </asp:DropDownList>
            <div id="body">
                <div id="pic">
                    <asp:Image ID="ShowImage" runat="server" Height="100"/></div>
                <div style="display:inline;">
                   <%-- <asp:DropDownList ID="ImageName" runat="server" AutoPostBack="true">
                        <asp:ListItem Value="Kiwi.png">奇異果</asp:ListItem>
                        <asp:ListItem Value="orange.png">橘子</asp:ListItem>
                    </asp:DropDownList>--%>
                    <asp:FileUpload ID="FileUpload1" runat="server"/>
                   
                </div>
            </div>
            <asp:Button ID="OKBtn" runat="server" Text="OK" OnClick="OKBtn_Click" OnClientClick="return confirm('確定要新增嗎？');"/>
            <asp:Button ID="cancelBtn" runat="server" Text="Cancel" OnClick="cancelBtn_Click"/>
            <asp:Button ID="ResetBtn" runat="server" Text="Reset" OnClick="ResetBtn_Click"/>
            <asp:Label ID="Msg" runat="server" Text="欄位不可空白"></asp:Label>
        </div>


        
    </form>
</body>
</html>
