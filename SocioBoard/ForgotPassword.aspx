<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/socialsuite.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="SocioBoard.ForgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
         $("#loginlink").click(function () {
             debugger;
             document.getElementById('signinemailMessages').innerHTML = "";
             document.getElementById('signinpasswordMessages').innerHTML = "";
             $('#logindiv').bPopup({
                 easing: 'easeOutBack',
                 positionStyle: 'fixed',
                 speed: 650,
                 transition: 'slideDown'
             });
         });
     });
    </script>
    <%--<link href="Contents/css/style.css" rel="stylesheet" type="text/css" />--%>
<%--<div class="forgot_pwd_header"><img src="../Contents/img/ssp/logo-txt.png" alt="" /></div>--%>
    <div class="forgotbg">
    <div class="forgot_pwd_bg">
       <div class="signin" id="heading" runat="server"></div>
       <div style="float: left;margin-left: 90%;margin-top: 5px;position: relative;">
         <a id="loginlink" href="#">Login</a>
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
                    <br />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Please Enter Valid Email!" ControlToValidate="txtEmail" CssClass="ws_err_star_enter" ValidationExpression="^[_A-Za-z0-9-]+(\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*(\.[A-Za-z]{2,3})$"></asp:RegularExpressionValidator>
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
    </div>
</asp:Content>
