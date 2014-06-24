<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="WooSuite.ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
    <link href="Contents/css/style.css" rel="stylesheet" type="text/css" />
</head>
<body  class="login">
    <form id="form1" runat="server">
            <div class="logo_login"><img src="../Contents/img/login.png" alt="" /></div>    
            <div class="login_reg_bg">
               <div class="signin">Change Password</div>
               <div class="reg_temp_reg">       
                 <div class="left_reg_area">           
                    <div class="input_pswd_bg"><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Old Password"></asp:TextBox></div>
                    <div class="error"> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Old Password!"  ControlToValidate="txtPassword" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please Enter Valid Password!" ControlToValidate="txtPassword" ValidationExpression="^\S+$" CssClass="ws_err_star_enter" ></asp:RegularExpressionValidator>
                    </div>

                    <div class="input_pswd_bg"><asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="New Password"></asp:TextBox></div>
                    <div class="error">    
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Confirm Password!" ControlToValidate="txtConfirmPassword" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Password & Confirm Password should be same" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" CssClass="ws_err_star_enter"></asp:CompareValidator>
                    </div>
                    <asp:Label ID="lblerror" runat="server" CssClass="ws_err_star" style="text-decoration:none;"></asp:Label>
                    <div class="create_your_account">
                           <asp:ImageButton ID="btnRegister" runat="server"  ImageUrl="../Contents/img/chng_pwd.png" />
                    </div>
            
                 </div>
               </div>
            </div>
    </form>
</body>
</html>
