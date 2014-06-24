<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pricing.aspx.cs" Inherits="SocioBoard.Pricing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Brandzter Pricing</title>
    <link href="Contents/Styles/Login.css" rel="stylesheet" type="text/css" />
    <script src="Contents/js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Contents/js/login.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="topdividerbg">
    <a href="http://ranktracker.brandzter.com/" style="text-decoration: none;"><div class=" lsearchmarketing">Ranktracker</div></a>
    <a href="http://dashboard.brandzter.com/"  style="text-decoration: none;"><div class=" ranktracker">Social CRM</div></a>  
    <a href="http://local.brandzter.com/" style="text-decoration: none;"><div class="social_crm">Local Search Marketing</div></a>
</div>
            <div id="home_header_bg" style="height:109px;">
                 <div class="main_headr" style="height:109px;">
                     <div class="logo_menu_bg" style="height:109px;">
                          <div class="tp_bg">                       
                             <div class="h_logo"><a href="#"><img src="Contents/Images/home/home-logo.png" alt="" border="none" /></a></div> 
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
                     
                  </div>
             </div>
        <!--home_header_bg ends-->

        <div id="home_container">
             <div class="allcustemet">All Plans Include a Free 30-Day Trial </div>
             <div class="allcustemet"><span class="upgrade">Upgrade, Change or Cancel Anytime</span> </div>
        </div>

        <!--container ends-->


        <!--Pricing -->
        <!--about_chatm-->
        <div class="about_chatm">
            <div class="main_about_content">
                <!--basib_bg-->
                <div class="basib_bg">     
                     <!--basic_img-->
                     <div class="basic_img">
                         <div class="basic_img_top">
                            <div class="tbasic_text">STANDARD</div>               
                            <div class="basic_doller_tex">
                                <div class="basic_doller_tex"><img border="none" alt="" src="../Contents/Images/pricing/standard.png" /></div>
                            </div>
                         </div>

                         <div class="basic_img_mid">
                               <div class="plus_text_bg">
                                  <div class="plus_icon"></div>
                                  <div class="plus_with_text">All-In-One Social Inbox</div>
                              </div>
              
                              <div class="plus_text_bgs">
                                  <div class="plus_icon"></div>
                                  <div class="plus_with_text">Advanced Publishing Features</div>
                              </div>
              
                              <div class="plus_text_bgs">
                                  <div class="plus_icon"></div>
                                  <div class="plus_with_text">Social CRM Tools</div>
                              </div>

                              <div class="plus_text_bgs">
                                  <div class="plus_icon"></div>
                                  <div class="plus_with_text">Comprehensive Reporting Tools</div>
                              </div>

                              <div class="plus_text_bgs">
                                  <div class="plus_icon"></div>
                                  <div class="plus_with_text">Complimentary Training & Support</div>
                              </div>

                              <div class="plus_text_bgs">
                                  <div class="plus_icon"></div>
                                  <div class="plus_with_text">Manage up to 10 profiles</div>
                              </div>
                        </div>

                         <div class="basic_img_bot">
                            <a href="Login.aspx?type=standard"><span class="signup_now_btn"></span></a>
                        </div> 
                     </div>             
                    <!--end basic_img-->

                    <!--standard_img-->
                     <div class="standard_img">
                            <div class="standard_img_top">
                                <div class="sbasic_text">Deluxe</div>               
                                <div class="standard_doller_tex"><img border="none" alt="" src="../Contents/Images/pricing/deluxe.png" /></div>               
                                <div class="standard_space"></div>
                            </div>

                            <div class="standard_img_mid">
                                <div class="plus_text_bgs">
                                  <div class="plus_icon"></div>
                                  <div class="plus_with_text">All Standard Plan Features</div>
                                </div>   
                                
                                <div class="plus_text_bgs">
                                  <div class="plus_icon"></div>
                                  <div class="plus_with_text">Complete Publishing & Engagement</div>
                                </div>
                                
                                <div class="plus_text_bgs">
                                  <div class="plus_icon"></div>
                                  <div class="plus_with_text">Helpdesk Integration</div>
                                </div>
                                
                                <div class="plus_text_bgs">
                                  <div class="plus_icon"></div>
                                  <div class="plus_with_text">Deluxe Reporting Package</div>
                                </div>
                                
                                <div class="plus_text_bgs">
                                  <div class="plus_icon"></div>
                                  <div class="plus_with_text">Google Analytics Integration</div>
                                </div>  
                                
                                <div class="plus_text_bgs">
                                  <div class="plus_icon"></div>
                                  <div class="plus_with_text">Complimentary Training & Support</div>
                                </div> 
                                
                                <div class="plus_text_bgs">
                                  <div class="plus_icon"></div>
                                  <div class="plus_with_text">Manage up to 20 profiles</div>
                                </div>  
                                       
                            </div>

                            <div class="standard_img_bot">
                                <a href="Login.aspx?type=deluxe"><span class="signup_grnnow_btn"></span></a>
                            </div>
             
                     </div>             
                    <!--end standard_img-->

                     <div class="premium_img">
                        <div class="basic_img_top">
                                <div class="tbasic_text">PREMIUM</div>             
                                <div class="basic_doller_tex">
                                    <div class="basic_doller_tex"><img border="none" alt="" src="../Contents/images/pricing/premium_text.png" /></div>
                                </div>
                        </div>
               
                        <div class="basic_img_mid">
                                <div class="plus_text_bg">
                                    <div class="plus_icon"></div>
                                    <div class="plus_with_text">All Standard & Deluxe Features</div>
                                </div>
              
                                <div class="plus_text_bgs">
                                    <div class="plus_icon"></div>
                                    <div class="plus_with_text">Social Care Suite</div>
                                </div>
              
                                <div class="plus_text_bgs">
                                    <div class="plus_icon"></div>
                                    <div class="plus_with_text">ViralPost™ Send Time Optimization</div>
                                </div>

                                <div class="plus_text_bgs">
                                    <div class="plus_icon"></div>
                                    <div class="plus_with_text">Premium Reporting Package</div>
                                </div>

                                <div class="plus_text_bgs">
                                    <div class="plus_icon"></div>
                                    <div class="plus_with_text">Custom Branded Interface</div>
                                </div>                                

                                <div class="plus_text_bgs">
                                    <div class="plus_icon"></div>
                                    <div class="plus_with_text">Manage up to 50 profiles</div>
                                </div>
                        </div>

                        <div class="basic_img_bot">
                            <a href="Login.aspx?type=premium"><div class="signup_prm_btn"></div></a>
                        </div>

                     </div>
          
                </div>
                <!--end basib_bg-->
           </div>
        </div>

      <!--end about_chatm-->

        <div class="home_breakupline"></div>

        <!-- blog area ends-->
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

          <div class="copyrigh_footer">©2012-13 Brandzter. All Rights Reserved.</div>

        </div>

        <%--<div id="home_footer">
            <div id="home_footer_wrapper">
                  <div class="main_home_footer">
                       <div class="fwise">
                          <h1>About Us</h1>
                           <ul>
                               <li><a href="#">Company</a></li>
                               <li><a href="#">Leadership</a></li>
                               <li><a href="#">Careers</a></li>
                               <li><a href="#">Contact Us</a></li>
                           </ul>
                       </div>

                       <div class="fwise">
                  <h1>Product</h1>
                   <ul>
                       <li><a href="#">Features</a></li>
                       <li><a href="#">Mobile</a></li>
                       <li><a href="#">App Directory</a></li>
                       <li><a href="#">Downloads</a></li>
                       <li><a href="#">Pricing</a></li>
                   </ul>
               </div>

                       <div class="fwise">
                  <h1>Services</h1>
                   <ul>
                       <li><a href="#">Help Desk</a></li>
                       <li><a href="#">Feedback</a></li>
                       <li><a href="#">FAQ</a></li>
                       <li><a href="#">Translation Project</a></li>
                   </ul>
               </div>

                       <div class="fwise">
                  <h1>Media</h1>
                   <ul>
                       <li><a href="#">Media Kit</a></li>
                       <li><a href="#">Press Releases</a></li>
                       <li><a href="#">Events</a></li>
                   </ul>
               </div>

                       <div class="fwise">
                  <h1>Partner</h1>
                   <ul>
                       <li><a href="#">Becoming a Partner</a></li>
                       <li><a href="#">Solution Partners</a></li>
                       <li><a href="#">Integration Partners</a></li>
                       <li><a href="#">Affiliates</a></li>
                       <li><a href="#">API</a></li>
                   </ul>
               </div>

                       <div class="fwise">
                  <h1>Legal</h1>
                   <ul>
                       <li><a href="#">Terms & Conditions</a></li>
                       <li><a href="#">Privacy Policy</a></li>
                   </ul>
               </div>
                  </div>
                  <div class="copyrigh_footer">©2013 Brandzter. All Rights Reserved.</div>
            </div>
        </div>--%>
        <!--footer ends-->
    </form>
</body>
</html>
