<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true"
    CodeBehind="Billing.aspx.cs" Inherits="WooSuite.Settings.Billing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<link rel="stylesheet" type="text/css" href="Contents/css/index.css" media="all">--%>
    <div id="mainwrapper" class="container">
        <div id="sidebar">
            <div class="billing-left-container">
                <div class="left-header">
                    Team Member</div>
                <ul>
                    <li><a href="PersonalSettings.aspx">Personal Settings</a></li>
                    <li><a href="BusinessSetting.aspx">Business Settings</a></li>
                    <li><a href="UsersAndGroups.aspx">User &amp; Groups</a></li>
                    <li><a href="#" class="active">Billing</a></li>
                </ul>
            </div>
        </div>
        <div id="contentcontainer2-publishing">
            <div class="billing-right-container">
                <div class="payment-right-left">
                    <h1>
                        Payment Method</h1>
                    <div class="pay-options">
                        <%--<input name="" type="radio" value=""> Credit Card &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <input name="" type="radio" value=""> Debit Card --%>
                    </div>
                    <div class="pay-options">
                        <a runat="server" onserverclick="Payment">
                            <img src="../Contents/img/paypal-butn.jpg" alt="" /></a>
                    </div>
                    <h3>
                        Your Plan</h3>
                    <div class="right-info">
                        <div class="info-txt">
                            <div id="plantype" runat="server" class="info-label-big">
                                Deluxe Plan <a href="#">Change</a>
                            </div>
                            <div id="priceofplan" runat="server" class="info-price">
                                $59.00</div>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="info-txt">
                            <div class="info-label-big">
                                Additional users: 0 @   <div id="rateExtra" runat="server">$59.00</div> <a href="#" onclick="planspopup();">Change</a></div>
                            <div class="info-price">
                                $0.00</div>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="info-total">
                            <div class="info-label">
                                Monthly Total
                            </div>
                            <div id="monthly" runat="server" class="info-price">
                                $59.00</div>
                            <div class="clear">
                            </div>
                            <div class="info-label" style="width: 250px; padding-top: 5px;" id="divcreatedate" runat="server">
                                Subscription begins Jul 13, 2013
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="payment-right-right">
                    <%--<img src="Contents/img/p-m-logo.png" alt="" />--%>
                </div>
                <div class="clear">
                </div>
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
        function UpgradePlan(id) {


            $.ajax({

                url: '../Helper/AjaxHelper.aspx?op=upgradeplan&planid=' + id,
                dataType: "html",
                type: "GET",
                success: function (msg) {

                    $("#popupplans").bPopup().close();
                    alertify.success("Updated Successfully");
                    window.location.reload();
                }
            });


          

        }

        function planspopup() {

            $("#popupplans").bPopup();
        }

    </script>
    <%--package popup--%>
    <div id="popupplans" style="background-color: #FFFFFF; border-radius: 10px 10px 10px 10px;
        box-shadow: 0 0 25px 5px #999999; color: #111111; display: none; min-width: 450px;
        padding: 25px; min-height: 330px;">
        <span class="button b-close">x</span>
        <div class="pricing-container">
            <div class="pricing-inner">
                <h2>
                    Change Your Plan</h2>
                <h4>
                    Upgrade, Change or Cancel Anytime</h4>
                <div class="pricing-list-container">
                    <div class="price-shadow-1">
                        <div class="pricing-list">
                            <div class="price-head">
                                Standard</div>
                            <h3>
                                $39 Per User/Month</h3>
                            <p>
                                Every plan includes remarkable social tools. This one is great for small teams.</p>
                            <ul>
                                <li><strong>All-In-One Social Inbox</strong></li>
                                <li>Real-time Brand Monitoring</li>
                                <li>Advanced Publishing Features</li>
                                <li>Social CRM Tools</li>
                                <li>Comprehensive Reporting Tools</li>
                                <li>Complimentary Training & Support</li>
                                <li>Complimentary Training & Support</li>
                            </ul>
                            <a id="standard" onclick="UpgradePlan(this.id);" class="trial-butn">
                                <img src="Contents/img/trial-butn-1.png" alt="" /></a>
                        </div>
                    </div>
                    <div class="price-shadow-1">
                        <div class="pricing-list">
                            <div class="price-head">
                                Deluxe</div>
                            <h3 style="color: #3491da;">
                                $59 Per User/Month</h3>
                            <p>
                                Everything you need to effectively manage a growing social presence.</p>
                            <ul>
                                <li><strong>All Standard Plan Features</strong></li>
                                <li>Complete Publishing & Engagement</li>
                                <li>Helpdesk Integration</li>
                                <li>Deluxe Reporting Package</li>
                                <li>Google Analytics Integration</li>
                                <li>Complimentary Training & Support</li>
                                <li>Manage up to 20 profiles</li>
                            </ul>
                            <a id="deluxe" onclick="UpgradePlan(this.id);" class="trial-butn">
                                <img src="Contents/img/trial-butn-2.png" alt="" /></a>
                        </div>
                    </div>
                    <div class="price-shadow-2">
                        <div class="pricing-list">
                            <div class="price-head">
                                Premium</div>
                            <h3>
                                $99 Per User/Month</h3>
                            <p>
                                Includes advanced tools for more sophisticated objectives</p>
                            <ul>
                                <li><strong>All Standard & Deluxe Features</strong></li>
                                <li>Social Care Suite</li>
                                <li><strong>ViralPost</strong>&trade; Send Time Optimization</li>
                                <li>Premium Reporting Package</li>
                                <li>Custom Branded Interface</li>
                                <li>Complimentary Training & Support</li>
                                <li>Manage up to 50 profiles</li>
                            </ul>
                            <a id="premium" onclick="UpgradePlan(this.id);" class="trial-butn">
                                <img src="Contents/img/trial-butn-1.png" alt="" /></a>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
            </div>
        </div>
        <div class="clearfix">
        </div>
    </div>
</asp:Content>
