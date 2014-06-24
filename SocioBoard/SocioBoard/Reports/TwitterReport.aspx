<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="TwitterReport.aspx.cs" Inherits="SocioBoard.Reports.TwitterReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    $("#reports").addClass('active');
</script>
    <div class="report">
        <img src="../Contents/Images/reports/engagementreport.PNG" />
    </div>

    <div class="report">
        <img src="../Contents/Images/reports/groupsstat.PNG" />
    </div>

    <div class="report">
        <img src="../Contents/Images/reports/twitter.PNG" />
    </div>
</asp:Content>
