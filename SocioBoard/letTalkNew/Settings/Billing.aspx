<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true" CodeBehind="Billing.aspx.cs" Inherits="letTalkNew.Settings.Billing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style type="text/css">    
    #content section {display: block;}
    .container_right nav #indicator{left:440px;}
</style>
<nav>
        <ul>
            <li><a href="PersonalSetting.aspx">Personal Setting</a></li>
            <li><a href="BusinessSetting.aspx">Business Setting</a></li>
            <li><a href="UserGroups.aspx">User & Groups</a></li>
            <li><a class="current" href="Billing.aspx">Billing</a></li>
        </ul>
        <span id="indicator"></span> 
   </nav>

    <div id="content">
         <section>
            <div class="billing-right-container">
                  <div class="payment-right-left">
                <h1> Payment Method</h1>
                <div class="pay-options"> </div>
                <div class="pay-options"> <a href="javascript:__doPostBack('ctl00$ContentPlaceHolder1$ctl00','')"> <img alt="" src="../Contents/img/paypal-butn.jpg"> </a> </div>
                <h3> Your Plan</h3>
                <div class="right-info">
                      <div class="info-txt">
                  	<div id="plantype" runat="server" class="info-label-big">Deluxe Plan <a href="#">Change</a> </div>
                                <div id="priceofplan" runat="server" class="info-price">$59.00</div>
                    <div class="clear"> </div>
                  </div>
                      <div class="info-txt">
                    <div class="info-label-big"> Additional users: 0 @
                          <div id="ContentPlaceHolder1_rateExtra">$40</div>
                          <a onClick="planspopup();" href="#">Change</a> </div>
                    <div class="info-price"> $0.00</div>
                    <div class="clear"> </div>
                  </div>
                      <div class="info-total">
                    <div class="info-label"> Monthly Total </div>
                      <div id="monthly" runat="server" class="info-price">$59.00</div>
                    <div class="clear"> </div>
                    <div id="divcreatedate" runat="server" class="info-label" style="width: 250px; padding-top: 5px;">Subscription begins - 8/14/2013 4:56:21 AM</div>
                    <div class="clear"> </div>
                  </div>
                    </div>
              </div>
                  <div class="payment-right-right"> </div>
                  <div class="clear"> </div>
                </div>
          </section>
    </div>
</asp:Content>
