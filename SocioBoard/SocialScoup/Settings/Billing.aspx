<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true"
    CodeBehind="Billing.aspx.cs" Inherits="SocialSuitePro.Settings.Billing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function MyJavascriptFunction() {
            alert('Your 30 Days trial period Over Please Upgrade your Package');
            //alertify('Your 30 Days trial period Over Please Upgrade your Package');
        };
    </script>
    <link rel="stylesheet" type="text/css" href="../Contents/css/socialsuite.css" />
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
            <div class="pricing-list-container" id="package" runat="server">
                <div class="price-shadow-1">
                    <div class="pricing-list">
                        <div class="price-head" id="heading1" runat="server">
                            INDIVIDUAL</div>
                        <div id="price1" runat="server">
                            <%--<h3>
                            $7.90 Per User/Month
                        </h3>--%></div>
                        <p>
                            This plan includes Powerful social tools. This is good for individual.</p>
                        <ul>
                            <li><strong>All-In-One Social Inbox</strong></li>
                            <li>Perfect Brand Monitoring</li>
                            <li>Advanced Publishing Features</li>
                            <li>Social CRM Tools</li>
                            <li>Performance Reporting Tools</li>
                            <li>Training &amp; Support</li>
                            <li>Manage up to 10 profiles</li>
                        </ul>
                        <a runat="server" class="trial-butn" id="ContentPlaceHolder1_standardplan" href="Billing.aspx?type=Standard">
                            <%-- <span class="trail_btn">Start your free trail !</span>--%>
                        </a>
                   </div>
                   </div>
                <div class="price-shadow-1">
                    <div class="pricing-list">
                        <div class="price-head" id="heading2" runat="server">
                            SMALL BUSINESS</div>
                        <div id="price2" runat="server">
                            <%--<h3 style="color: #F4594F;" >
                            $19.90 Per User/Month</h3--%>></div>
                        <p>
                            You can manage a growing business with this plan.</p>
                        <ul>
                            <li><strong>Everything Individual Plan Feature Plus</strong></li>
                            <li>Publishing &amp; Engagement Tool</li>
                            <li>Helpdesk Integration</li>
                            <li>Biult-in Advance Reporting tools</li>
                            <li>Google Analytics Integration</li>
                            <li>Training &amp; Support</li>
                            <li>Manage up to 25 profiles</li>
                        </ul>
                        <a runat="server" class="trial-butn" id="ContentPlaceHolder1_deluxeplan" href="Billing.aspx?type=Deluxe">
                            <%--<span class="trail_btn active">Start your free trail !</span>--%>
                        </a>
                    </div>
                </div>
                <div class="price-shadow-2">
                    <div class="pricing-list">
                        <div class="price-head" id="heading3" runat="server">
                            CORPORATION</div>
                        <div id="price3" runat="server">
                            <%--<h3>
                            $49.00 Per User/Month</h3>--%></div>
                        <p>
                            This plan best for Established Businesses
                        </p>
                        <ul>
                            <li><strong>Individual and Small businness Plan Features combine Plus</strong></li>
                            <li>Social Care Suite</li>
                            <li><strong>ViralPost</strong>&trade; Send Time Optimization</li>
                            <li>3D Performance Reporting</li>
                            <li>Dedicated Training &amp; Support</li>
                            <li>Manage up to 60 profiles</li>
                        </ul>
                        <a runat="server" class="trial-butn" id="ContentPlaceHolder1_premiumplan" href="Billing.aspx?type=Premium">
                            <%--<span class="trail_btn">Start your free trail !</span>--%>
                        </a>
                    </div>
                </div>
                <div class="clearfix">
                </div>
            </div>
        </div>
        <div class="payment-right-right">
            <%--	<img src="../Contents/img/p-m-logo.png" alt="" />--%>
        </div>
        <div class="clear">
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
