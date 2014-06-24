<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="WooSuite.ForgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Contents/css/style.css" rel="stylesheet" type="text/css" />
</head>
<body class="login">
    <form id="form1" runat="server">
     <div class="logo_login"><img src="../Contents/img/login.png" alt="" /></div>
    
    <div class="login_reg_bg">
       <div class="signin">Forgot Password</div>
       <div class="reg_temp_reg">
       
         <div class="left_reg_area">          
            
            <div class="inputbg"><asp:TextBox ID="txtEmail" runat="server" placeholder="Email"></asp:TextBox></div>
            <div class="error">
                <asp:Label ID="lblerror" runat="server" Text="Label"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Email!"  ControlToValidate="txtEmail" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Please Enter Valid Email!" ControlToValidate="txtEmail" CssClass="ws_err_star_enter" ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$"></asp:RegularExpressionValidator>
            </div>          
          
            <div class="create_your_account">
                   <asp:ImageButton ID="btnForgotPwd" runat="server" 
                       ImageUrl="../Contents/img/submit_btn.png" onclick="btnForgotPwd_Click" />
             </div>
            
         </div>
         
       
       </div>
    </div>
    </form>
</body>
</html>
