<%@ Page Title="" Language="C#" MasterPageFile="~/MyPage.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="WebApplication1.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #header{
            text-align:center;
        }
        img{
            height:100px;
            width:100px;
        }
        
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="header">
        <asp:TextBox ID="SearchTextBox" runat="server"></asp:TextBox><asp:Button ID="SearchBtn" runat="server" Text="Button" OnClick="SearchBtn_Click"/>
        <asp:Button ID="CreateBtn" runat="server" Text="建立" OnClick="CreateBtn_Click"/>
    </div>
    <div>
        <asp:Repeater ID="GroupRepeater" runat="server">
            <ItemTemplate>
                <a href="../Detail.aspx?ID=<%# Eval("ID") %>">
                    <div class="jumbotron row">
                        <img src="Image/<%# Eval("ImageUrl") %>" /><h1 id="groupname"><p><%# Eval("GroupName") %></p></h1>
                        
                    </div>
                    主揪:<%# Eval("AccountName") %> 店名:<%# Eval("ShopName") %> 目前人數:<%# Eval("Count") %>
                </a>
            </ItemTemplate>
        </asp:Repeater>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:PlaceHolder ID="PagePlaceHolder" runat="server">
         <asp:Repeater runat="server" ID="repPaging">
        <ItemTemplate>
            <a href="<%# Eval("Link") %>" title="<%# Eval("Title") %>"><%# Eval("Name") %></a>
        </ItemTemplate>
    </asp:Repeater>
        </asp:PlaceHolder>
    </div>
</asp:Content>
