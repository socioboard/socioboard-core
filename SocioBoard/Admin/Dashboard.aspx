<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/adminSite.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SocialSuitePro.Admin.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <style type="text/css">
    iframe{width:1000px; height:425px; float:left; border:none;}
 </style>   

<div id="container" style="min-width: auto; height: auto; margin: 0 auto">
    <iframe src="DashboardStats.aspx"></iframe>
</div>

    <asp:Button ID="Button1" runat="server" Text="For Dev Only" 
        onclick="Button1_Click" />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>
