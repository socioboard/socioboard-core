<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="Billing.aspx.cs" Inherits="SocioBoard.Settings.Billing" %>
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
    <div id="mainwrapper" class="billing_container">
        <div id="billing_sidebar">
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
        <div id="bilingcontentcontainer2-publishing">
            <div class="pricing-list-container" id="package" runat="server">
                <div runat="server" id="freediv" class="price-shadow-1" style="display:none">
                    <div class="pricing-list">
                        <div class="price-head" id="heading1" runat="server">
                            Basic</div>
                        <div id="price1" runat="server">
                            <%--<h3>
                            $7.90 Per User/Month
                        </h3>--%></div>
                        <p>
                            Every plan is a unique package. This one fits for individuals.</p>
                        <ul>
                            <li><strong>Comprehensive Dashboard</strong></li>
                            <li>Multiple Social Profiles Inbox</li>
                            <li>Social Media Updates</li>
                            <li>Scheduling Features</li>
                            <li>Social Media based CRM</li>
                            <li>Social Platform Reporting Tools</li>
                            <li>World Class 24*7 Training & Support</li>
                            <li>Manage up to 5 profiles</li>
                        </ul>
                        <a runat="server" class="trial-butn" id="ContentPlaceHolder1_freeplan">
                            <%-- <span class="trail_btn">Start your free trail !</span>--%>
                        </a>
                   </div>
                   </div>
                <div class="price-shadow-1">
                    <div class="pricing-list">
                        <div class="price-head" id="heading2" runat="server">
                            Standard</div>
                        <div id="price2" runat="server">
                            <%--<h3 style="color: #F4594F;" >
                            $19.90 Per User/Month</h3--%>></div>
                         <p>
                            Comprises of great tools suitable for small teams.</p>
                        <ul>
                            <li><strong>Smart Inbox</strong></li>
                            <li>Real Time Feeds from Social Media Platforms</li>
                            <li>Advanced Publishing Features</li>
                            <li>Sophisticated Tools for Social Media based CRM </li>
                            <li>In-Depth Reporting Tools</li>
                            <li>World Class 24*7 Training & Support</li>
                            <li>Manage up to 10 profiles</li>
                        </ul>
                        <a runat="server" class="trial-butn" id="ContentPlaceHolder1_standardplan" href="Billing.aspx?type=Standard">
                            <%--<span class="trail_btn active">Start your free trail !</span>--%>
                        </a>
                    </div>
                </div>
                <div class="price-shadow-2">
                    <div class="pricing-list">
                        <div class="price-head" id="heading3" runat="server">
                            Premium</div>
                        <div id="price3" runat="server">
                            <%--<h3>
                            $49.00 Per User/Month</h3>--%></div>
                        <p>
                            Package you need to efficiently manage an expanding social media network.</p>
                        <ul>
                            <li><strong>All Standard Plan Features</strong></li>
                            <li>Social Discovery </li>
                            <li>Helpdesk Integration</li>
                            <li>Powerful Statistics Engine</li>
                            <li>Custom Viewable Analytics</li>
                            <li>World Class 24*7 Training & Support</li>
                            <li>RSS Feeds</li>
                            <li>Manage up to 20 profiles</li>
                        </ul>
                        <a runat="server" class="trial-butn" id="ContentPlaceHolder1_premiumplan" href="Billing.aspx?type=Premium">
                            <%--<span class="trail_btn">Start your free trail !</span>--%>
                        </a>
                    </div>
                </div>


                 <div class="price-shadow-2">
                    <div class="pricing-list">
                        <div class="price-head" id="heading4" runat="server">
                            Deluxe</div>
                        <div id="price4" runat="server">
                            <%--<h3>
                            $49.00 Per User/Month</h3>--%></div>
                         <p>
                            Includes sophisticated tools for complex objectives</p>
                        <ul>
                            <li><strong>All Basic, Standard & Deluxe Features</strong></li>
                            <li>Prompt Feeds</li>
                            <li>Constant Real-time Monitoring</li>
                            <li>Team Collaboration Tools</li>
                            <li>Social CRM</li>
                            <li>World Class 24*7 Training & Support</li>
                            <li>Manage up to 50 profiles</li>
                        </ul>
                        <a runat="server" class="trial-butn" id="ContentPlaceHolder1_deluxeplan" href="Billing.aspx?type=Deluxe">
                            <%--<span class="trail_btn">Start your free trail !</span>--%>
                        </a>
                    </div>
                </div>
                
                
                
                
                
                     <div class="price-shadow-2">
                    <div class="pricing-list">
                        <div class="price-head" id="heading5" runat="server">
                            SocioBasic</div>
                        <div id="price5" runat="server">
                            <%--<h3>
                            $49.00 Per User/Month</h3>--%></div>
                         <p>
                            Includes sophisticated tools for complex objectives</p>
                        <ul>
                            <li><strong>All Basic, Standard & Deluxe Features</strong></li>
                            <li>Prompt Feeds</li>
                            <li>Constant Real-time Monitoring</li>
                            <li>Team Collaboration Tools</li>
                            <li>Social CRM</li>
                            <li>World Class 24*7 Training & Support</li>
                            <li>Manage up to 100 profiles</li>
                        </ul>
                        <a runat="server" class="trial-butn" id="ContentPlaceHolder1_SocioBasic" href="Billing.aspx?type=SocioBasic">
                            <%--<span class="trail_btn">Start your free trail !</span>--%>
                        </a>
                    </div>
                </div>
                     <div class="price-shadow-2" style="margin-top:20px;">
                    <div class="pricing-list">
                        <div class="price-head" id="heading6" runat="server">
                            SocioStandard</div>
                        <div id="price6" runat="server">
                            <%--<h3>
                            $49.00 Per User/Month</h3>--%></div>
                         <p>
                            Includes sophisticated tools for complex objectives</p>
                        <ul>
                            <li><strong>All Basic, Standard & Deluxe Features</strong></li>
                            <li>Prompt Feeds</li>
                            <li>Constant Real-time Monitoring</li>
                            <li>Team Collaboration Tools</li>
                            <li>Social CRM</li>
                            <li>World Class 24*7 Training & Support</li>
                            <li>Manage up to 200 profiles</li>
                        </ul>
                        <a runat="server" class="trial-butn" id="ContentPlaceHolder1_SocioStandard" href="Billing.aspx?type=SocioStandard">
                            <%--<span class="trail_btn">Start your free trail !</span>--%>
                        </a>
                    </div>
                </div>
                     <div class="price-shadow-2" style="margin-top:20px;">
                    <div class="pricing-list">
                        <div class="price-head" id="heading7" runat="server">
                            SocioPremium</div>
                        <div id="price7" runat="server">
                            <%--<h3>
                            $49.00 Per User/Month</h3>--%></div>
                         <p>
                            Includes sophisticated tools for complex objectives</p>
                        <ul>
                            <li><strong>All Basic, Standard & Deluxe Features</strong></li>
                            <li>Prompt Feeds</li>
                            <li>Constant Real-time Monitoring</li>
                            <li>Team Collaboration Tools</li>
                            <li>Social CRM</li>
                            <li>World Class 24*7 Training & Support</li>
                            <li>Manage up to 500 profiles</li>
                        </ul>
                        <a runat="server" class="trial-butn" id="ContentPlaceHolder1_SocioPremium" href="Billing.aspx?type=SocioPremium">
                        </a>
                    </div>
                </div>
                     <div class="price-shadow-2" style="margin-top:20px;">
                    <div class="pricing-list">
                        <div class="price-head" id="heading8" runat="server">
                            SocioDeluxe</div>
                        <div id="price8" runat="server">
                            <%--<h3>
                            $49.00 Per User/Month</h3>--%></div>
                         <p>
                            Includes sophisticated tools for complex objectives</p>
                        <ul>
                            <li><strong>All Basic, Standard & Deluxe Features</strong></li>
                            <li>Prompt Feeds</li>
                            <li>Constant Real-time Monitoring</li>
                            <li>Team Collaboration Tools</li>
                            <li>Social CRM</li>
                            <li>World Class 24*7 Training & Support</li>
                            <li>Manage up to 1000 profiles</li>
                        </ul>
                        <a runat="server" class="trial-butn" id="ContentPlaceHolder1_SocioDeluxe" href="Billing.aspx?type=SocioDeluxe">
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
