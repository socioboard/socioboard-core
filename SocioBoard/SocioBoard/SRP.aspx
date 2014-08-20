<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/socialsuite.Master" AutoEventWireup="true" CodeBehind="SocialRegisterPage.aspx.cs" Inherits="SocialSuitePro.SocialRegisterPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="ssp_registration_page">
      <div class="login_reg_bg">
       <div class="signin">Create Your Account</div>
       <div class="reg_temp_reg">       
         <div class="left_reg_area">
            <div class="inputbg"><asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name"></asp:TextBox></div>
            <div class="error"><asp:RequiredFieldValidator ID="RequiredFieldValidator5" 
                    runat="server" ErrorMessage="Please Enter First Name!"
                                ControlToValidate="txtFirstName" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ErrorMessage="Please Enter Only Alphabets" ControlToValidate="txtFirstName" 
                    ValidationExpression="[a-zA-Z][a-zA-Z ]+"></asp:RegularExpressionValidator>
                                </div>
            <div class="inputbg"><asp:TextBox ID="txtLastName" runat="server" placeholder="Last Name"></asp:TextBox></div>
            <div class="error"><asp:RequiredFieldValidator ID="RequiredFieldValidator6" 
                    runat="server" ErrorMessage="Please Enter Last Name!"
                                ControlToValidate="txtLastName" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                    ErrorMessage="Please Enter Only Alphabets" ControlToValidate="txtLastName" 
                    ValidationExpression="[a-zA-Z][a-zA-Z ]+"></asp:RegularExpressionValidator>
                                </div>
            <div class="inputbg"><asp:TextBox ID="txtEmail" runat="server" placeholder="Email"></asp:TextBox></div>
            <div class="error"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                    runat="server" ErrorMessage="Please Enter Email!"
                                ControlToValidate="txtEmail" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Please Enter Valid Email!"
                                ControlToValidate="txtEmail" CssClass="ws_err_star_enter" 
                    ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$"></asp:RegularExpressionValidator>
</div>
            <div class="input_pswd_bg"><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" AUTOCOMPLETE="off" placeholder="Password"></asp:TextBox></div>
            <div class="error"> <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                    runat="server" ErrorMessage="Please Enter Password!"
                                ControlToValidate="txtPassword" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                    runat="server" ErrorMessage="Please Enter Valid Password!"
                                ControlToValidate="txtPassword" ValidationExpression="^\S+$"
                                CssClass="ws_err_star_enter" ></asp:RegularExpressionValidator></div>

            <div class="input_pswd_bg"> <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm Password"></asp:TextBox></div>
            <div class="error">    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                    runat="server" ErrorMessage="Please Enter Confirm Password!"
                                ControlToValidate="txtConfirmPassword" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Password & Confirm Password should be same"
                                ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"
                                CssClass="ws_err_star_enter"></asp:CompareValidator></div>
 <asp:Label ID="lblerror" runat="server" CssClass="ws_err_star" style="text-decoration:none;"></asp:Label>
               <div class="create_your_account">
                   <asp:ImageButton ID="btnRegister" runat="server" 
                       ImageUrl="../Contents/img/create_account.png" onclick="btnRegister_Click" 
                        />
             </div>
            
         </div>
         <!--<div class="right_area_reg">
             <div class="social_bg">
                <div class="you_cantext">Sign In With Twitter <br />
                 Your 30 day trial lasts until midnight on  <br /><b> June 27, 2013</b></div>
                 <div class="create_your_account"><a href="#"><img src="../Contents/img/create_account.png" alt="" boeder="none" /></a></div>
                               
                <div class="register_regtext">By clicking 'Create Account' you agree to the <br /> <a>Terms of Service</a> and <a>Privacy policies</a>. </div>
                
             </div>
         </div>-->       
       </div>
    </div>
</div>
</asp:Content>
