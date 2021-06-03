<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="WebApplication1.Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="Jquery/jquery.js"></script>
    <script src="bootstrap-4.6.0-dist/bootstrap-4.6.0-dist/js/bootstrap.js"></script>
    <link href="bootstrap-4.6.0-dist/bootstrap-4.6.0-dist/css/bootstrap.css" rel="stylesheet" />
    <title></title>
    <style>
        #MainPic {
            height: 150px;
            width: 150px;
            background-color: red;
        }

        #Image1, #Image2 {
            height: 80px;
            width: 80px;
            background-color: red;
        }

        .Menu {
            border: 2px solid black;
        }

        .MenuImg,#ImageAcc {
            height: 80px;
            width: 80px;
            background-color: red;
        }

        #order {
            height: 100px;
            border: 2px solid black;
        }
    </style>
</head>
<body class="container">
    <form id="form1" runat="server">















        <div>
            <asp:Repeater ID="GroupRepeater" runat="server">
                <ItemTemplate>
                    <div class="row">
                        <div>
                            <img id="MainPic" src="Image/<%# Eval("ImageUrl") %>" /><h1 id="groupname">
                                <p><%# Eval("GroupName") %></p>
                            </h1>
                            狀態:<asp:DropDownList ID="StatusList" runat="server">
                                <asp:ListItem>未結團</asp:ListItem>
                                <asp:ListItem>已結團</asp:ListItem>
                                <asp:ListItem>已送達</asp:ListItem>
                            </asp:DropDownList><br />
                            店名:<%# Eval("ShopName") %><br />
                            主揪:<%# Eval("AccountName") %><br />
                        </div>
                        <div>
                            
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <br />
            <br />
            菜單
            <div class="row">
                <asp:Repeater ID="MenuRepeater" runat="server" OnItemDataBound="MenuRepeater_ItemDataBound">
                    <ItemTemplate>
                        <div class="col-3 Menu">
                            <img class="MenuImg" alt="" src="Image/<%# Eval("ImageUrl") %>" />
                            $<%# Eval("Price") %>
                            <%# Eval("Menu") %>
                            <asp:DropDownList ID="CountList" AutoPostBack="true" runat="server"  ToolTip='<%# Eval("Menu") +","+Eval("MenuID")+","+Eval("Price")%>' OnSelectedIndexChanged="CountList_SelectedIndexChanged">  
				<asp:ListItem Value="0">0</asp:ListItem>
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                                <asp:ListItem Value="7">7</asp:ListItem>
                                <asp:ListItem Value="8">8</asp:ListItem>
                                <asp:ListItem Value="9">9</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <br />
            <br />
            成員
            <div class="row">
                <asp:Repeater ID="MemberRepeater" runat="server" OnItemDataBound="MemberRepeater_ItemDataBound" OnItemCommand="MemberRepeater_ItemCommand">
                    <ItemTemplate>
                        <div class="col-3 Menu">
                            <asp:Button CssClass="close" ID="Button1" runat="server" Text="&times;" CommandArgument='<%# Eval("AccountID") %>'/><img alt="" src="Image/<%# Eval("ImageUrl") %>" id="Image2" />
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <div>
                                    <%# Eval("MenuName") %> * <%# Eval("Count") %> <br />
                                        </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>


            <br />
            <br />

           <div class="row" id="order">
               <div>
                   <asp:Image ID="Image1" runat="server" />
                   <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                   <asp:Button ID="Button2" runat="server" Text="送出" OnClick="Button2_Click"/>
               </div>
           </div>

            <asp:Button ID="BackBtn" runat="server" Text="Back" OnClick="BackBtn_Click" />
        </div>
    </form>
</body>
</html>
