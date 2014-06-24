<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="SocioBoard.Registration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="login_wrapper">
            <div class="ws_registration" style="margin-top: 80px;">
                <div class="registration_title">
                    Create An Account</div>
                <div class="registration_mid">
                    <div class="registration_form">
                        <!--<div class="ws_lg_text">Username</div>-->
                        <div class="blank_div">
                        </div>
                        <div class="ws_input_text_star">
                            <div class="ws_txt_cont">
                                Name :</div>
                            <div class="ws_input_text">
                                <div class="ws_lg_input">
                                    <%--<input type="text" name="" placeholder="Enter User Name" />--%>
                                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                                </div>
                                <div class="icon">
                                    <img src="Contents/Images/Login/user_icon.png" width="22" height="24" alt="" />
                                </div>
                            </div>
                            <div class="ws_err_star">
                            </div>
                        </div>
                        <div class="ws_input_text_error">
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="Please Enter UserName!"
                                ControlToValidate="txtUserName" ValidationGroup="reg" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtUserName" runat="server" ErrorMessage="Please Enter valid User Name"
                                ValidationExpression="^[a-zA-Z]+? ?[a-zA-Z]+$" ValidationGroup="reg" ControlToValidate="txtUserName"
                                CssClass="ws_err_star_enter"></asp:RegularExpressionValidator>
                        </div>
                        <div class="ws_input_text_star">
                            <div class="ws_txt_cont">
                                Email :</div>
                            <div class="ws_input_text">
                                <div class="ws_lg_input">
                                    <%--<input type="text" name="" placeholder="Enter Email" runat="server" />--%>
                                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                </div>
                                <!--div class="icon"><img src="Images/Login/user_icon.png" width="22" height="24" alt="" /></div-->
                            </div>
                            <div class="ws_err_star">
                            </div>
                        </div>
                        <div class="ws_input_text_error">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Email!"
                                ControlToValidate="txtEmail" ValidationGroup="reg" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Please Enter Valid Email!"
                                ControlToValidate="txtEmail" ValidationGroup="reg" CssClass="ws_err_star_enter" ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$"></asp:RegularExpressionValidator>
                        </div>
                        <!-- <div class="ws_lg_text">Password</div>-->
                        <div class="ws_input_text_star">
                            <div class="ws_txt_cont">
                                Password :
                            </div>
                            <div class="ws_input_text">
                                <div class="ws_lg_input">
                                    <%--<input type="password" name="" placeholder="Enter Password"  runat="server" />--%>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="icon">
                                    <img src="Contents/Images/Login/user_pwd.png" width="18" height="23" alt="" /></div>
                            </div>
                        </div>
                        <div class="ws_input_text_error">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Password!"
                                ControlToValidate="txtPassword" ValidationGroup="reg" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please Enter Valid Password!"
                                ControlToValidate="txtPassword" ValidationGroup="reg" ValidationExpression="^\S+$"
                                CssClass="ws_err_star_enter" ></asp:RegularExpressionValidator>
                        </div>
                        <div class="ws_input_text_star">
                            <div class="ws_txt_cont">Confirm Password :</div>
                            <div class="ws_input_text">
                                <div class="ws_lg_input">
                                    <%-- <input type="password" name="" placeholder="Enter Confirm Password" />--%>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="icon"><img src="Contents/Images/Login/user_pwd.png" width="18" height="23" alt="" /></div>
                            </div>
                            <%--<div class="ws_err_star">
                              
                            </div>--%>
                        </div>
                        <div class="ws_input_text_error">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Confirm Password!"
                                ControlToValidate="txtConfirmPassword" ValidationGroup="reg" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Password & Confirm Password should be same"
                                ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ValidationGroup="reg"
                                CssClass="ws_err_star_enter"></asp:CompareValidator>
                        </div>
                        <div class="ws_signup_btn_div">
                            <%--<input type="button" class="ws_signup_btn" />--%>
                            <asp:ImageButton ID="btnSignup" runat="server" class="ws_signup_btn" 
                                ImageUrl="~/Contents/Images/reg/sign_up_btn.png" ValidationGroup="reg" 
                                onclick="btnSignup_Click" />
                        </div>
                        <div class="ws_input_text_error">
                            <asp:Label ID="lblerror" runat="server" CssClass="ws_err_star" style="text-decoration:none;"></asp:Label>
                            <%--Registration is Successfully--%>
                            
                        </div>
                    </div>
                </div>
                <div class="registration_bot">
                </div>
            </div>
        </div>
</asp:Content>
