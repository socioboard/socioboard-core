<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="Billing.aspx.cs" Inherits="SocialSuitePro.Settings.Billing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div id="mainwrapper" class="container">
			<div id="sidebar">
				<div class="billing-left-container">
                	<div class="left-header">Team Member</div>
                    <ul>
                    	<li><a href="PersonalSettings.aspx">Personal Settings</a></li>
                        <li><a href="BusinessSetting.aspx">Business Settings</a></li>
                        <li><a href="UsersAndGroups.aspx">User &amp; Groups</a></li>
                        <li><a href="#" class="active" >Billing</a></li>
                    </ul>
                    
                </div>
                				
		  	</div>
            <div id="contentcontainer2-publishing">
            	<div class="billing-right-container">
                	<div class="payment-right-left">
                    	<h1>Payment Method</h1>
                        <div class="pay-options">
                        	<%--<input name="" type="radio" value=""> Credit Card &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <input name="" type="radio" value=""> Debit Card --%>
                        </div>
                        <div class="pay-options">
                        <a runat="server"  onserverclick="Payment"><img src="../Contents/img/paypal-butn.jpg" alt="" /></a>
                        </div>
                        <h3>Your Plan</h3>
                      	<div class="right-info">
                        	<div class="info-txt">
                            	<div id="plantype" runat="server" class="info-label-big">Deluxe Plan <a href="#">Change</a> </div>
                                <div id="priceofplan" runat="server" class="info-price">$59.00</div>
                                <div class="clear"></div>
                            </div>
                            <div class="info-txt">
                            	<%--<div class="info-label-big">Additional users: 0 @ <div id="rateExtra" runat="server">$59.00</div> <a href="#">Change</a></div>
                                <div class="info-price">$0.00</div>--%>
                                <div class="clear"></div>
                            </div>
                            <div class="info-total">
                            	<div class="info-label">Monthly Total </div>
                               <div id="monthly" runat="server" class="info-price">$59.00</div>
                                <div class="clear"></div>
                                <div class="info-label" style="width:250px; padding-top:5px;"  id="divcreatedate" runat="server">Subscription begins Jul 13, 2013 </div>
                                <div class="clear"></div>
                            </div>
                        </div>
                      	
                    </div>
                    <div class="payment-right-right">
                    <%--	<img src="../Contents/img/p-m-logo.png" alt="" />--%>
                        
                    </div>
                    
                    <div class="clear"></div>
                </div>
                
			</div>
		</div>
     <script type="text/javascript" language="javascript">
         $(document).ready(function () {

             $("#home").removeClass('active');
             $("#message").removeClass('active');
             $("#feeds").removeClass('active');
             $("#discovery").removeClass('active');
             $("#publishing").removeClass('active');
         });
     
     </script>
</asp:Content>
