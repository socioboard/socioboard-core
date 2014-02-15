<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/adminSite.Master" AutoEventWireup="true" CodeBehind="AdminSettings.aspx.cs" Inherits="SocialSuitePro.Admin.AdminSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Contents/css/Style_previous.css" rel="stylesheet" type="text/css" />
   <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
<div class="ws_tm_mid">
            <div class="ws_tm_personal_setting">
                <!--personal_setting_left-->
                <div class="personal_setting_left">
                    <div class="invite_friends">
                      Admin Settings
                    </div>
                    <div class="first_name_last_name_div">
                      
                        <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name"></asp:TextBox>
                        <asp:TextBox ID="txtLastName" runat="server" placeholder="Last Name"></asp:TextBox>
                    </div>
                    <div class="first_name_last_name_div">
                        <asp:TextBox ID="txtUserName" runat="server" placeholder="UserName"></asp:TextBox>
                    </div>
                   <%-- <div class="email_div">
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                      
                    </div>--%>
                    <div class="first_name_last_name_div" id="change_password" style="display: none;">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Old Password"></asp:TextBox>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="New Password"></asp:TextBox>
                        <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="txtPassword"
                            ControlToValidate="txtConfirmPassword" ErrorMessage="*" ValidationGroup="setting"></asp:CompareValidator>
                      
                        <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" OnClick="changePassoword" />
                        <input id="btncancel" type="button" value="Cancel" />
                       
                    </div>
                    <div class="ws_tm_ps_button_div">
                        <input id="chngpwd" type="button" value="Change Password"  />
                    </div>
                    <div class="ws_ps_user_content">
                        <div class="user_photo">
                           
                            <asp:Image ID="custImg" runat="server" />
                        </div>
                        <div  class="personal_details">
                            Team members will see the avatar you have set for <strong><em id="email_personal_for_setting" runat="server"></em>
                            </strong>on Browse.
                            <br />
                            <asp:FileUpload ID="imgfileupload" runat="server" />
                            <asp:HiddenField ID="imghdn" runat="server" />
                          
                        </div>
                        <div style="width: 235px; height: 25px; margin-left: 57px;">
                            <div style="width: 140px; height: 22px; float: left;">
                                <div id="ListingImgMessage" clientidmode="Static" runat="server">
                                </div>
                                <div class="FPlstImgUploading">
                                 
                                    <img id="imgProgress" src="" alt="" />
                                </div>
                            </div>
                        </div>
                        <div class="img_crop">
                            <img id="listImg" src="" alt="" style="width: 214px; height: 300px; display: none;" />
                        </div>
                    </div>
                    <div class="ws_ps_user_content">
                        <div class="time_zone">
                            Time Zone</div>
                        <div class="ps_select">
                            <asp:DropDownList ID="ddlTimeZone" CssClass="select" runat="server" Height="21px"
                                Width="405px">
                            </asp:DropDownList>
                        
                        </div>
                    </div>
                </div>
             
            </div>
            <div class="ws_tm_button_div">
                <asp:Button ID="btnSave" runat="server" Text="Save"  ValidationGroup="setting"  onclick="btnSave_Click"
                    />
               
            </div>
        </div>
         <script type="text/javascript" language="javascript">

             $("#chngpwd").click(function () {
                 $("#change_password").css('display', 'block');
                 $("#chngpwd").hide();
             });


             $("#btncancel").click(function () {
                 $("#change_password").css('display', 'none');
                 $("#chngpwd").show();
             });
        </script>
</asp:Content>
