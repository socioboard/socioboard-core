<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/socialsuite.Master" AutoEventWireup="true" CodeBehind="SocialPricing.aspx.cs" Inherits="SocialSuitePro.SocialPricing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="pricing-container">
                    	<div class="pricing-inner">
                        	<h2>All Plans Include a Free 30-Day Trial</h2>
                            <h4>Upgrade, Change or Cancel Anytime</h4>
                            
                            <div class="pricing-list-container">
                            	<div class="price-shadow-1">
                                    <div class="pricing-list">
                                        <div class="price-head">Standard</div>
                                        <h3>$29 Per User/Month</h3>
                                        <p>Every plan includes remarkable social tools. This one is great for small teams.</p>
                                      <ul>
                                            <li><strong>All-In-One Social Inbox</strong></li>
                                            <li>Real-time Brand Monitoring</li>
                                            <li>Advanced Publishing Features</li>
                                            <li>Social CRM Tools</li>
                                            <li>Comprehensive Reporting Tools</li>
                                            <li>Complimentary Training & Support</li>
                                            <li>Manage up to 10 profiles</li>
                                        </ul>
                                        <a id="standardplan"  runat="server" class="trial-butn"><img  src="../Contents/img/trial-butn-1.png" alt="" /></a>
                                        
                                    </div>
                                </div>
                                <div class="price-shadow-1">
                                    <div class="pricing-list">
                                        <div class="price-head">Deluxe</div>
                                        <h3 style="color:#F4594F;">$49 Per User/Month</h3>
                                        <p>Everything you need to effectively manage a growing social presence.</p>
                                      <ul>
                                            <li><strong>All Standard Plan Features</strong></li>
                                            <li>Complete Publishing & Engagement</li>
                                            <li>Helpdesk Integration</li>
                                            <li>Deluxe Reporting Package</li>
                                            <li>Google Analytics Integration</li>
                                            <li>Complimentary Training & Support</li>
                                            <li>Manage up to 20 profiles</li>
                                        </ul>
                                        <a  id="deluxeplan" runat="server" class="trial-butn"><img src="../Contents/img/trial-butn-2.png" alt="" /></a>
                                        
                                    </div>
                                </div>
                                
                                <div class="price-shadow-2">
                                    <div class="pricing-list">
                                        <div class="price-head">Premium</div>
                                        <h3>$89 Per User/Month</h3>
                                        <p>Includes advanced tools for more sophisticated objectives</p>
                                      <ul>
                                            <li><strong>All Standard & Deluxe Features</strong></li>
                                            <li>Social Care Suite</li>
                                            <li><strong>ViralPost</strong>&trade; Send Time Optimization</li>
                                            <li>Premium Reporting Package</li>
                                            <li>Custom Branded Interface</li>
                                            <li>Complimentary Training & Support</li>
                                            <li>Manage up to 50 profiles</li>
                                        </ul>
                                        <a id="premiumplan" runat="server" class="trial-butn"><img src="../Contents/img/trial-butn-1.png" alt="" /></a>
                                        
                                    </div>
                                </div>
                                
                                <div class="clearfix"></div>
                            </div>
                            
                        </div>
                    </div>
</asp:Content>
