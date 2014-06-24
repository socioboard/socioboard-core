<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/socialsuite.Master"
    AutoEventWireup="true" CodeBehind="SocialPricing.aspx.cs" Inherits="SocialSuitePro.SocialPricing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pricing-container">
        <div class="pricing-inner">
            <h2>
                All Plans Include a Free 30-Day Trial</h2>
            <h4>
                Upgrade, Change or Cancel Anytime</h4>
            <div class="pricing-list-container">
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
                        <a id="standardplan" runat="server" class="trial-butn">
                            <img src="../Contents/img/trial-butn-1.png" alt="" /></a>
                    </div>
                </div>
                <div class="price-shadow-1">
                    <div class="pricing-list">
                        <div class="price-head" id="heading2" runat="server">
                            SMALL BUSINESS</div>
                        <div id="price2" runat="server">
                            <%--<h3>
                            $7.90 Per User/Month
                        </h3>--%></div>
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
                        <a id="deluxeplan" runat="server" class="trial-butn">
                            <img src="../Contents/img/trial-butn-2.png" alt="" /></a>
                    </div>
                </div>
                <div class="price-shadow-2">
                    <div class="pricing-list">
                        <div class="price-head" id="heading3" runat="server">
                            CORPORATION</div>
                        <div id="price3" runat="server">
                            <%--<h3>
                            $7.90 Per User/Month
                        </h3>--%></div>
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
                        <a id="premiumplan" runat="server" class="trial-butn">
                            <img src="../Contents/img/trial-butn-1.png" alt="" /></a>
                    </div>
                </div>
                <div class="clearfix">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
