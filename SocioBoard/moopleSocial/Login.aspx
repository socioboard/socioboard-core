<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WooSuite.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Login</title>
<link rel="stylesheet" type="text/css" href="../Contents/css/style.css" />
</head>

<body class="login">
  
    <form id="form1" runat="server">
  
    <div class="logo_login"><img src="../Contents/img/login.png" alt="" /></div>
    
    <div class="login_bg">
       <div class="signin">Sign in</div>
       <div class="signin_temp">
       
         <div class="left_area">
            <div class="inputbg">
                <asp:TextBox ID="txtEmail" runat="server" placeholder="Email"></asp:TextBox></div>
                <div class="error">  
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                    runat="server" ErrorMessage="Please Enter Email!"
                            ControlToValidate="txtEmail" 
                    CssClass="ws_err_star" ValidationGroup="btnGroup"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" Display="Dynamic" 
                    runat="server" ErrorMessage="Please Enter Valid Email!"
                            ControlToValidate="txtEmail" CssClass="ws_err_star_enter" 
                        ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$"></asp:RegularExpressionValidator></div>
            <div class="input_pswd_bg">
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox></div>
            <div class="error">  
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                    runat="server" ErrorMessage="Please Enter Password!" 
                    ControlToValidate="txtPassword"  CssClass="ws_err_star" 
                    ValidationGroup="btnGroup"></asp:RequiredFieldValidator>
                             <asp:Label ID="txterror" runat="server" CssClass="ws_err_star_enter"></asp:Label></div>
            <%--<div class="forgot_pswd"><input type="checkbox"  />Remember me</div>--%>
            <div class="forgot_pswd">Forgot password? <a href="ForgotPassword.aspx">Click here to restore</a></div>
            <div class="loginbtn">
                <asp:ImageButton ID="btnLogin" runat="server" 
                    ImageUrl="../Contents/img/login_btn.png" onclick="btnLogin_Click" 
                    ValidationGroup="btnGroup" />
             </div>
         </div>
         <div class="right_area">
             <div class="social_bg">
                <div class="you_cantext">You can also login with your social account</div>
                <div class="social_icon_bg">
                   <div class="fb_icon" ><a id="fb_account" href="#" runat="server" onserverclick="AuthenticateFacebook"></a></div>
                   <div class="google_icon"><a id="gp_account" href="#" runat="server" onserverclick="AuthenticateGooglePlus"></a></div>
                </div>
                
                <div class="register_text">Don`t registered? <a href="Plans.aspx">Register now!</a></div>
                
             </div>
         </div>
       
       </div>
    </div>

    </form>

</body>
</html>
