<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true"
    CodeBehind="PersonalSettings.aspx.cs" Inherits="SocialSuitePro.Settings.PersonalSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>SocialSuitePro-TeamMember-PersonalSetting</title>
    <link rel="stylesheet" type="text/css" href="../Contents/css/Style_previous.css" />
    <link href="../Contents/js/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Contents/js/jquery.uploadify-3.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".cngpwd").click(function () {
                var pwd = $(".pwd").val();
                var cpwd = $(".cpwd").val();
                if (pwd != cpwd) {
                    //alert("Password Missmatch");
                    return false;
                }
                if (pwd == "" || cpwd == "") {
                    // alert("Password must not be blank");
                    $(".lblerror").text("Please enter Password");
                    return false;
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="mainwrapper" class="container">
        <div class="ws_tm_left">
            <div class="team_member_div">
                <div class="tm_title">
                    Team Member</div>
                <h3 id="memberName" runat="server">
                    Member Name</h3>
                <ul>
                    <li><a class="active">Personal Setting</a> </li>
                    <li><a href="BusinessSetting.aspx">Business Setting</a> </li>
                    <li><a href="UsersAndGroups.aspx">Users & Groups</a> </li>
                    <li><a href="Billing.aspx">Billing</a><li>
                        <%--<li><a>Helpdesk Integration</a> </li>
                    <li><a>Sprout Queue</a> </li>
                    <li><a>Billing</a> </li>
                    <li><a>Utilities & Goodies</a> </li>--%>
                </ul>
            </div>
        </div>
        <div class="ws_tm_mid">
            <div class="ws_tm_personal_setting">
                <!--personal_setting_left-->
                <div class="personal_setting_left">
                    <div class="invite_friends">
                        Personal Settings
                    </div>
                    <div class="first_name_last_name_div">
                        <%--<input type="text" id="txtFirstName" placeholder="First Name" runat="server" />--%>
                        <%--<input type="text" id="Text1" placeholder="Last Name" runat="server" />--%>
                        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                    </div>
                    <div class="email_div">
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        <%-- <input type="text" id="txtEmail" placeholder="Email" runat="server" />--%>
                    </div>
                    <div class="first_name_last_name_div" id="change_password" style="display: none;">
                       <label>Password</label> 
                        <asp:TextBox class="pwd" ID="txtPassword" runat="server" TextMode="Password" 
                            AutoCompleteType="Disabled"></asp:TextBox>
                        <label>Confirm Password</label> 
                        <asp:TextBox class="cpwd" ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                         <asp:Label class="lblerror" ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                        <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="txtPassword"
                            ControlToValidate="txtConfirmPassword" ErrorMessage="Password Missmatch" 
                            ValidationGroup="setting" ForeColor="Red" SetFocusOnError="True"></asp:CompareValidator>
                       
                        <%-- <div class="ws_tm_ps_button_div">--%>
                        <asp:Button class="cngpwd" ID="btnChangePassword" runat="server" Text="Change Password"
                            OnClick="changePassoword" />
                        <input id="btncancel" type="button" value="Cancel" />
                        <%-- </div>--%>
                    </div>
                    <div class="ws_tm_ps_button_div">
                        <input id="chngpwd" type="button" value="Change Password" />
                    </div>
                    <div class="ws_ps_user_content">
                        <div class="user_photo">
                            <%-- <img id="custImg" alt="" runat="server" />--%>
                            <asp:Image ID="custImg" runat="server" />
                        </div>
                        <div class="personal_details">
                            Team members will see the avatar you have set for <strong><em id="email_personal_for_setting"
                                runat="server"></em>&nbsp;</strong>on Browse.
                            <br />
                            <asp:FileUpload ID="imgfileupload" runat="server" />
                            <asp:HiddenField ID="imghdn" runat="server" />
                            <%-- <input type="file" value="browse" />--%>
                        </div>
                        <div style="width: 235px; height: 25px; margin-left: 57px;">
                            <div style="width: 140px; height: 22px; float: left;">
                                <div id="ListingImgMessage" clientidmode="Static" runat="server">
                                </div>
                                <div class="FPlstImgUploading">
                                    <%--Uploading...--%>
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
                            <%--<select class="select" title="Select one">
                                <option>Blue</option>
                                <option selected="selected">Red</option>
                                <option>Green</option>
                                <option>Yellow</option>
                                <option>Brown</option>
                            </select>--%>
                        </div>
                    </div>
                </div>
                <!--end personal_setting_left-->
                <!--personal_setting_right-->
                <%--       <div class="personal_setting_right">
                    <div class="invite_friends">
                        Personal Profiles</div>
                    <p>
                        These profiles are only visible to you.</p>
                    <div class="addsocial_network">
                        <div class="addNetworkIcon">
                            <a>+ <span class="social_icon">
                                <img src="../Contents/img/twiter_24X24.png" width="16" height="15" alt="" /></span></a>
                        </div>
                        <div class="add_to_netword">
                            <a>Connect your personal <strong>Twitter account</strong></a>
                        </div>
                    </div>
                    <div class="addsocial_network">
                        <div class="addNetworkIcon">
                            <a id="facebookanchor" runat="server">+ <span class="social_icon">
                                <img src="../Contents/img/fb_24X24.png" width="16" height="15" alt="" /></span></a>
                        </div>
                        <div class="add_to_netword">
                            <a>Connect your personal<strong>Facebook user</strong></a> <span class="network_subtext">
                                (Personal account only, Business Pages not supported in Personal Mode) </span>
                        </div>
                    </div>
                    <div class="addsocial_network">
                        <div class="addNetworkIcon">
                            <a id="linkinanchor" runat="server">+ <span class="social_icon">
                                <img src="../Contents/img/linked_25X24.png" width="16" height="15" alt="" /></span></a>
                        </div>
                        <div class="add_to_netword">
                            <a>Connect your personal <strong>LinkedIn account</strong></a>
                        </div>
                    </div>
                    <div class="addsocial_network">
                        <div class="addNetworkIcon">
                            <a id="GoogleAnalyticsanchor" runat="server">+ <span class="social_icon">
                                <img src="../Contents/img/an_24X24.png" width="16" height="15" alt="" /></span></a>
                        </div>
                        <div class="add_to_netword">
                            <a>Connect your personal <strong>Google Analytics account</strong></a>
                        </div>
                    </div>
                    <div class="addsocial_network">
                        <div class="addNetworkIcon">
                            <a id="googleplusanchor" runat="server">+ <span class="social_icon">
                                <img src="../Contents/img/google_plus.png" width="16" height="15" alt="" /></span></a>
                        </div>
                        <div class="add_to_netword">
                            <a>Connect your personal <strong>Google Plus account</strong></a>
                        </div>
                    </div>
                    <div class="addsocial_network">
                        <div class="addNetworkIcon">
                            <a>+ <span class="social_icon">
                                <img src="../Contents/img/instagram_24X24.png" width="16" height="15" alt="" /></span></a>
                        </div>
                        <div class="add_to_netword">
                            <a>Connect your personal <strong>Instagram account</strong></a>
                        </div>
                    </div>
                </div>--%>
                <!--personal_setting_right-->
            </div>
            <div class="ws_tm_button_div">
                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="setting" OnClick="btnSave_Click" />
                <%--<input type="button" value="SAVE" id="btnSave" runat="server" onclick="btnSave_onclick" />--%>
            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript">

        $("#chngpwd").click(function () {
            $("#chngpwd").css('display', 'none');
            $("#change_password").css('display', 'block');
        });





        $("#btncancel").click(function () {
            $("#chngpwd").css('display', 'block');
            $("#change_password").css('display', 'none');
            $(".lblerror").text("");
        });

        $("#home").removeClass('active');
        $("#message").removeClass('active');
        $("#feeds").removeClass('active');
        $("#discovery").removeClass('active');
        $("#publishing").removeClass('active');
    
 


    </script>
</asp:Content>
