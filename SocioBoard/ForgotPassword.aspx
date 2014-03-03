<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="SocialSuitePro.ForgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Contents/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../Contents/js/jquery-2.0.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#btnresetpass").click(function () {
                var pass = $("#txtpass").val();
                var cpass = $("#txtconfirmpass").val();
                if (pass == "" || cpass == "") {
                    $("#lblerror").text("Please enter Password and Confirmpassword");
                    return false;
                }
                if ($.trim(pass) == "" || $.trim(cpass) == "") {
                    $("#lblerror").text("Please enter Password and Confirmpassword");
                    return false;
                }
                if (pass != cpass) {
                    $("#lblerror").text("Password mismatch");
                    return false;
                }
            });
        });
    </script>

</head>
<body class="login">
    <form id="form1" runat="server">
     <div class="forgot_pwd_header"><img src="../Contents/img/ssp/logo-txt.png" alt="" /></div>
    
    <div class="forgot_pwd_bg">
       <div class="signin" id="heading" runat="server"></div>
       <div style="float: left;margin-left: 90%;margin-top: 5px;position: relative;">
         <a href="Default.aspx">Login</a>
         </div>
       <div class="reg_temp_reg">
       
         <div class="left_reg_area">          
            <div class="inputbg" id="divtxtmail" runat="server"><asp:TextBox ID="txtEmail" runat="server" placeholder="Enter your email id."></asp:TextBox></div>
             <div class="inputbg" id="divtxtpass" runat="server"><asp:TextBox ID="txtpass" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox></div>
              <div class="inputbg" id="divtxtconfirmpass" runat="server"><asp:TextBox ID="txtconfirmpass" runat="server" placeholder="Confirm Password" TextMode="Password"></asp:TextBox></div>
            <div class="error">
                <asp:Label ID="lblerror" runat="server"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ErrorMessage="Please Enter Valid Email!" 
                    ControlToValidate="txtEmail" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Please Enter Valid Email!" ControlToValidate="txtEmail" CssClass="ws_err_star_enter" ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$"></asp:RegularExpressionValidator>
            </div>          
          
            <div class="create_your_account" id="btnforgetpass" runat="server">
                   <asp:ImageButton ID="btnForgotPwd" runat="server" 
                       ImageUrl="../Contents/img/submit_btn.png" onclick="btnForgotPwd_Click" />
             </div>
              <div class="create_your_account" id="btnresetpass" runat="server">
                   <asp:ImageButton ID="btnResetPwd" runat="server" 
                       ImageUrl="../Contents/img/submit_btn.png" onclick="btnResetPwd_Click"   />
             </div>

            
         </div>
    
       </div>
    </div>
    </form>
</body>
</html>
