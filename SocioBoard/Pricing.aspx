<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/socialsuite.Master"
    AutoEventWireup="true" CodeBehind="Pricing.aspx.cs" Inherits="SocialSuitePro.MasterPage.Pricing" %>

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
                        <div class="price-head">
                            Basic</div>
                        <h3>
                            FREE</h3>
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
                        <a id="freePlan" runat="server" class="trial-butn">
                            <img src="../Contents/img/trial-butn-1.png" alt="" /></a>
                    </div>
                </div>
                <div class="price-shadow-1">
                    <div class="pricing-list">
                        <div class="price-head">
                            Standard</div>
                        <h3>
                            $29 Per User/Month</h3>
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
                        <a id="standardplan" runat="server" class="trial-butn">
                            <img src="../Contents/img/trial-butn-1.png" alt="" /></a>
                    </div>
                </div>
                <div class="price-shadow-1">
                    <div class="pricing-listk">
                        <div class="price-head">
                            Deluxe</div>
                        <h3>
                            $49 Per User/Month</h3>
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
                        <a id="deluxeplan" runat="server" class="trial-butn">
                            <img src="../Contents/img/trial-butn-2.png" alt="" /></a>
                    </div>
                </div>
                <div class="price-shadow-2">
                    <div class="pricing-list">
                        <div class="price-head">
                            Premium</div>
                        <h3>
                            $89 Per User/Month</h3>
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
