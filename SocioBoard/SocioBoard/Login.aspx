<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SocioBoard.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" >
    <title>Brandzter Login</title>
        <link href="Contents/Styles/Login.css" rel="stylesheet" type="text/css" />
    <script src="Contents/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Contents/Scripts/JSlogin.js" type="text/javascript"></script>
</head>
<body class="login_body_img" style="background: #fff;">
    <form id="form1" runat="server" >

    <div class="topdividerbg">
    <a href="http://ranktracker.brandzter.com/" style="text-decoration: none;"><div class="lsearchmarketing">Ranktracker</div></a>
    <a href="http://dashboard.brandzter.com/"  style="text-decoration: none;"><div class="ranktracker">Social CRM</div></a>  
    <a href="http://local.brandzter.com/" style="text-decoration: none;"><div class="social_crm">Local Search Marketing</div></a>
</div>

    <div id="home_header_bg">
             <div class="main_headr">
                 <div class="logo_menu_bg">
                   <div class="tp_bg">                       
                     <div class="h_logo"><a href="#"><img src="Contents/Images/home/home_logo.png" alt="" border="none" /></a></div> 
                     <div class="menu_bg">
                          <ul>
                              <li><a href="FeaturePage.aspx">Features</a></li>
                              <li>|</li>
                              <li><a href="Pricing.aspx">Plans & Pricing</a></li>
                              <li>|</li>
                              <li><a href="http://blog.brandzter.com/">Blog</a></li>                              
                          </ul>
                     </div>
                   </div> 
                                       
                     <div class="social_media_text">Social Media Management</div>
                     <div class="theleading_text">The Leading social media dashboard to manage and measure your social network</div>

                     <div class="gloves_text_bh">
                     
                       <div class="gloves_img"><img src="Contents/Images/home/gloves.png" alt="" /></div>

                       <div class="view_plantext">
                       
                           <ul>
                              <li>Manage Multiple Social profiles</li>
                              <li>Schedule messages and tweets</li>
                              <li>track brand mentions</li>
                              <li>Analyze social media traffic</li>
                              <li>4 million + satisfied users</li>
                              <li>Analyze social media traffic</li>
                              <li>4 million + satisfied users</li>
                           </ul>

                           <div class="view_plan_img"><a href="Pricing.aspx"><img src="Contents/Images/home/view_plave_pricing_btn.png" alt="" border="none" /></a></div>
                         
                       </div>
                     
                     </div>
                                    
                 </div>
                 <div class="right_signup_bg">
                      <div class="sign_btn_bg">
                          <div class="sign_btn">                           
                            
                          </div>

                          <div id="SignUpForm" class="sign_btn">
                            
                             <div id="loginContainer">
                                <a href="#" id="loginButton"><span>Login</span><em></em></a>
                                <div style="clear:both"></div>
                                <div id="loginBox">                
                                    <div id="loginForm">
                                        <fieldset id="body">
                                            <fieldset>
                                                <label for="email">Email Address</label>
                                                <%--<asp:TextBox ID="txtLoginId" watermark="Email"  ></asp:TextBox>--%>
                                                <asp:TextBox ID="txtLoginId" runat="server" placeholder="Email"   />

                                                <div class="home_login_error">
                                                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2"  ErrorMessage="Please Enter Email!"
                            ControlToValidate="txtLoginId" ValidationGroup="reg" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="revEmail" Display="Dynamic"  ErrorMessage="Please Enter Valid Email!"
                            ControlToValidate="txtLoginId" CssClass="ws_err_star_enter" ValidationGroup="reg" ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$"></asp:RegularExpressionValidator>
                                                </div>
                                            </fieldset>
                                            <fieldset>
                                                <label for="password">Password</label>
                                                <%--<asp:TextBox ID="txtPassword"  watermark="Password"  TextMode="Password"></asp:TextBox>--%>
                                                <asp:TextBox runat="server" ID="txtPassword" placeholder="Password" TextMode="Password"/>

                                                <div class="home_login_error">
                                                    <%-- <asp:Label ID="lblpassword" ></asp:Label>--%>
                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1"  ErrorMessage="Please Enter Password!" ControlToValidate="txtPassword" ValidationGroup="reg"  CssClass="ws_err_star"></asp:RequiredFieldValidator>
                             <asp:Label runat="server" ID="txterror"  CssClass="ws_err_star_enter"></asp:Label>
                                                </div>
                                                <div class="home_login_error">
                                                    <span id="errorMessage" style="float:left;"></span>
                                                </div>
                                            </fieldset>
                                            
                                            <input runat="server" type="submit" ID="btnLogin" class="ws_login_btn"  onserverclick="btnLogin_Click"  ValidationGroup="reg" value="Sign in" />
                                            <label for="checkbox"><input type="checkbox" id="checkbox" />Remember me</label>
                                        </fieldset>
                                        <span><a href="ForgetPassword.aspx">Forgot your password?</a></span>
                                    </div>
                                </div>
                            </div>


                         </div>
                         </div>

                         <div class="signup_top_bg"></div>
                         <div class="signup_mid_bg">
                         
                            <div class="signtesxt"></div>
                            <%--<div class="input">
                                
                                <asp:TextBox name="txtLoginName" runat="server" ID="txtUserName" />
                                <input type="hidden" name="weUserName_ClientState" id="weUserName_ClientState" />
                            </div>--%>
                          <%--  <div class="inputerror">
                                <span id="rfvUserName" style="color:Red;display:none;">User Name Required!</span>
                            </div>--%>

                            <div class="input">
                                <input  runat="server" id="txtEmail" placeholder="Email" onblur="validationSignup()"  type="text"/>
                                <input type="hidden" name="weEmail_ClientState" id="weEmail_ClientState" />
                            </div>
                            <div class="inputerror">
                                <span id="rfvEmail" style="color:Red;display:none;">Email Required!</span>
                                <span id="revdEmail" style="color:Red;display:none;">Enter Valid Email!</span>
                            </div>
                            <div class="input">
                                <input name="txtFirstName" type="text" id="txtFirstName" runat="server" placeholder="FirstName"  onblur="validationSignup()"/>
                                <input type="hidden" name="weFirstName_ClientState" id="weFirstName_ClientState" />
                            </div>
                            <div class="inputerror">
                                <span id="rfvFirstName" style="color:Red;display:none;">First Name Required!</span>    
                            </div>
                            <div class="input">
                                
                                <input name="txtLastName" type="text" id="txtLastName"  placeholder="LastName" runat="server" onblur="validationSignup()"/>
                                <input type="hidden" name="weLastName_ClientState" id="weLastName_ClientState" />
                            </div>
                            <div class="inputerror">
                                <span id="rfvLastName" style="color:Red;display:none;">Last Name Required!</span>
                            </div>
                            <div class="input">
                                
                                <asp:TextBox ID="txtPasswordSignUp" runat="server" TextMode="Password"  placeholder="PassWord" onblur="validationSignup()"/>
                                <input type="hidden" name="wePassword_ClientState" id="wePassword_ClientState" />
                            </div>
                            <div class="inputerror">
                                <span id="rfvPassword" style="color:Red;display:none;">Password Required!!</span>
                            </div>
                            <div class="input">
                                
                                <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" placeholder="ConfirmPassword"  onblur="validationSignup()"/>
                                <input type="hidden" name="weConfPass_ClientState" id="weConfPass_ClientState" />
                            </div>
                            <div class="inputerror">
                                <span id="cvConfirmPassword" style="color:Red;display:none;">Password Mis Matched!!</span>
                                <span id="lblError" runat="server" style="color:Red;"></span>
                            </div>
                            <div>
                                <asp:Label ID="lblregerror" runat="server" Text="Label" Visible="false"></asp:Label>
                            </div>
                            <div class="ihave_text">
                                <input id="chkTerms" type="checkbox" name="chkTerms" checked="checked" />I have read and agree to the <a href="javascript:void(0);">Term of Use</a> <br />
                                <input id="chkNews" type="checkbox" name="chkNews" /> I’d like to receive Brandzter newsletters
                            </div>

                            <a href="#"><div class="sign_up_btn">
                                <input type="image" name="btnSignUp" id="btnSignUp" src="Contents/Images/home/home_sinup_btn.png"  runat="server" onclick=" return validationSignup()" onserverclick="btnSignup_Click" style="border-width:0px;" />
                            </div></a>


                         </div>
                         <div class="signup_bot_bg"></div>

                      </div>
                 </div>
             </div>
        <!--home_header_bg ends-->

        <div id="home_container">
             <div class="allcustemet">Who’s Using Brandzter</div>
             <div><img src="Contents/Images/user_client.png" alt="" border="none" /></div>

             <div class="product_bg">
                <a href="#"><div class="product_chatter"></div></a>
                <a href="#"><div class="product_pricing"></div></a>
             </div>

             <div class="product_bg">
                <a href="#"><div class="product_rivew"></div></a>
                <a href="#"><div class="product_about"></div></a>
             </div>

             <div class="view_planpricinbg">
                 <div class="takentour"><a href="#"><img src="Contents/Images/home/take_a_quick_tour.png" alt="" border="none" /></a></div>
                 <div class="plnpricing"><a href="Pricing.aspx"><img src="Contents/Images/home/view_planpricingbtn.png" alt="" border="none" /></a></div>
             </div>

        </div>

        <!--container ends-->

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
        <!--footer ends-->
    </form>
</body>
</html>
