<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FeaturePage.aspx.cs" Inherits="SocioBoard.FeaturePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Features - Brandzter</title>
    <link href="Contents/Styles/Login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="topdividerbg">
    <a href="http://ranktracker.brandzter.com/" style="text-decoration: none;"><div class=" lsearchmarketing">Ranktracker</div></a>
    <a href="http://dashboard.brandzter.com/"  style="text-decoration: none;"><div class=" ranktracker">Social CRM</div></a>  
    <a href="http://local.brandzter.com/" style="text-decoration: none;"><div class="social_crm">Local Search Marketing</div></a>
</div>
    <div id="home_header_bg">
        <div class="main_headr">
            <div class="logo_menu_bg" style=" height: 109px;">
                <div class="tp_bg" style="width:1039px;">
                    <div class="h_logo">
                        <a href="#">
                            <img src="Contents/Images/home/home_logo.png" alt="" border="none" /></a></div>
                    <div class="menu_bg">
                        <ul>
                            <li><a href="FeaturePage.aspx">Features</a></li>
                            <li>|</li>
                            <li><a href="Pricing.aspx">Plans & Pricing</a></li>
                            <li>|</li>
                            <li><a href="http://blog.brandzter.com/">Blog</a></li>
                            <li>|</li>
                            <li><a href="Login.aspx">SignIn</a></li>
                        </ul>
                    </div>
                </div>
            </div>

             <!--features_video-->
                <div class="features_video">
                    <div class="features_row">
                       <iframe width="463px" height="335px" style="float: left; width: 463px; height: 335px; margin-top: 8px;" src="//www.youtube.com/embed/4Sq8XDcF9-c" frameborder="0" allowfullscreen></iframe>
                       
                        <div class="text-center">All Plans Boast the Following Powerful Features</div>
                        
                    </div>
                </div>
            <!--end features_video-->
        </div>
    </div>

        <!--features_main-->
        <div class="features_main">            
            <div class="feature_gallery_wrapper">
                <div class="feature_gallery">
                    <ul class="gallery">
                        <li class="engagement">
                            <h4 class="thin-sub">Enagagement</h4>
                            <p>
						        Single stream Inbox designed to never miss a message and 
						        tasking tools to ensure that no customer goes unanswered.
					        </p>
                            <a class="more">Find out more »</a>
                        </li>
                        <li class="publish">                        
                            <h4 class="thin-sub">Publishing</h4>
                            <p>
						        Seamlessly post and schedule your messages to Twitter,  						
                                Facebook and LinkedIn with Sprout's powerful tools. 					
					        </p>
                            <a class="more">Find out more »</a>
                        </li>
                        <li class="analytics">
                            <h4 class="thin-sub">Analytics</h4>
                            <p>
						        Unlimited reporting &amp; exporting across all of your accounts. 
						        Profile, group and roll-up reports for high or low 
						        level performance data.
					        </p>
                            <a class="more">Find out more »</a>
                        </li>
                        <li class="monitoring">
                            <h4 class="thin-sub">Monitoring</h4>
                            <p>
						        Discover what people on social media are saying about 
						        your brand through keyword monitoring.
					        </p>
                            <a class="more">Find out more »</a>                        
                        </li>
                    </ul>
                </div>
            </div>
            <div class="feature_blank"></div>
        </div>
        <!--end features_main-->

    <div id="home_footer">
        <div class="main_home_footer">
            <div class="menu_bg_login">
                <ul>
                    <li><a href="FeaturePage.aspx">Features</a></li>
                    <li>|</li>
                    <li><a href="Pricing.aspx">Plans & Pricing</a></li>
                    <li>|</li>
                    <li><a href="http://blog.brandzter.com/">Blog</a></li>
                </ul>
            </div>
        </div>
        <div class="copyrigh_footer">
            ©2012-13 Brandzter. All Rights Reserved.</div>
    </div>
    </form>
</body>
</html>
