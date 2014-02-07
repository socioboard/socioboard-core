<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true"
    CodeBehind="UsersAndGroups.aspx.cs" Inherits="SocialSuitePro.Settings.UsersAndGroups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>SocioCrowd-TeamMember-UserAndGroups</title>
   
    <link rel="stylesheet" type="text/css" href="../Contents/css/Style_previous.css" />
    <script src="../Contents/js/dialog_box.js" type="text/javascript"></script>
      <script src="../Contents/js/UsersAndGroups.js" type="text/javascript"></script>
     

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="HiddenFieldGroupName" runat="server" />
    <asp:HiddenField ID="HiddenFieldGroupNameInDDl" runat="server" />
   <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HiddenField2" runat="server" />
    <div id="mainwrapper" class="container">

        <div class="ws_tm_left">
            <div class="team_member_div">
                <div class="tm_title">
                    Team Member</div>
                <h3>
                    <asp:Label id="memberName" runat="server"></asp:Label></h3>
                <ul>
                    <li><a href="PersonalSettings.aspx">Personal Setting</a> </li>
                    <li><a href="BusinessSetting.aspx">Business Setting</a> </li>
                    <li><a class="active">Users & Groups</a> </li>
                    <li><a href="Billing.aspx">Billing</a> </li>
                  <%--  <li><a>Helpdesk Integration</a> </li>
                    <li><a>Sprout Queue</a> </li>
                    <li><a>Billing</a> </li>
                    <li><a>Utilities & Goodies</a> </li>--%>
                </ul>
            </div>
        </div>
        <div class="ws_tm_mid">
            <div class="ws_tm_personal_setting">
                <!--personal_setting_left-->
                <div class="userandgroups">
                    <div class="invite_friends">
                        Groups
                        <input id="Button1" type="button" value=" Add a group" />
                    </div>
                    <div class="first_name_last_name_div">
                        Groups are used to categorize social profiles together to help manage and report
                        on your social media efforts efficiently. Learn more about Groups
                    </div>
                    <div id="Insertgroupname" class="email_div" style="display: none;">
                        <div class="inst_title">
                            Create a Group
                            <div id="close" style="width: auto; float: right; margin-right: 10px; color: #fff;
                                cursor: pointer;">
                                close</div>
                        </div>
                        <div class="instagram_title">
                            Groups help you organize your social profiles and team members. Name your group
                            and you can begin connecting social profiles and adding team members to it. Twitter
                            or Facebook required to create a group.
                        </div>
                        <div class="instagram_txt_cont">
                            <div class="instagram_txt">
                                Group Name :
                            </div>
                            <input id="txtGroupName" name="txtGroupName" type="text" />
                        </div>
                        <ul>
                            <li><a id="TwitterOAuth" runat="server" onserverclick="TwitterOAuthRedirect" >
                                <img src="../Contents/img/twt_icon.png" width="16" height="16" alt="" />
                                <span>Twitter</span> </a></li>
                            <li><a id="facebook_connect" runat="server" onserverclick="FacebookRedirect" >
                                <img src="../Contents/img/fb_24X24.png" width="16" height="16" alt="" />
                                <span>Facebook</span> </a></li>
                        </ul>
                    </div>
                    <div id="Div1" class="email_div">
                        <div id="AllGroups" runat="server">


                        </div>
                    </div>
                </div>

                <div class="userandgroups">
                    <div class="invite_friends">
                        Profile Linked
                        <asp:Label ID="lblSelectedGroup" runat="server" Text=""></asp:Label>
                        <input id="Button2" type="button" value="Connect a Profile " />
                    </div>
                    <div id="insertFbPopup" class="email_div" style="display: none;">
                        <div class="conct_group">
                            <div class="cont_title">
                                CONNECT A PROFILE</div>
                            <asp:DropDownList ID="ddlGroup" runat="server">
                            </asp:DropDownList>
                        </div>
                        <ul>
                            <li><a id="TwitOuthForProfile" runat="server" onclick="GetGroupName('twitter');" onserverclick="TwitterRedirect">
                                <img src="../Contents/img/twt_icon.png" width="16" height="16" alt="" />
                                <span>Twitter</span> </a></li>
                            <li><a id="FbOuthForProfile" runat="server" onclick="GetGroupName('facebook')" onserverclick="FacebookOAuthRedirect">
                                <img src="../Contents/img/fb_24X24.png" width="16" height="16" alt="" />
                                <span>Facebook</span> </a></li>
                            <li><a id="LinkedInLink" runat="server" onclick="GetGroupName('linkedin')" onserverclick="LinkedInRedirect">
                                <img src="../Contents/img/linked_25X24.png" width="16" height="16" alt="" />
                                <span>LinkedIn</span> </a></li>
                           <%-- <li><a id="gp_cont" runat="server" onclick="GetGroupName('googleplus')" onserverclick="GooglePlusRedirect">
                                <img src="../Contents/img/google_plus.png" width="16" height="16" alt="" />
                                <span>Google Plus</span> </a></li>--%>
                           <%-- <li><a id="ga_cont" runat="server" href="#">
                                <img src="../Contents/img/google_analytics.png" width="16" height="16" alt="" />
                                <span>Google Analytics</span> </a></li>--%>
                            <li><a id="InstagramConnect" runat="server" onclick="GetGroupName('instagram')" onserverclick="InstagramRedirect">
                                <img src="../Contents/img/instagram_24X24.png" width="16" height="16" alt="" />
                                <span>Instagram</span> </a></li>
                        </ul>
                        <div class="cancel_proceed_div">
                            <input type="button" id="bclose" value="cancel" />
                        </div>
                    </div>
                    <div class="first_name_last_name_div">
                    </div>
                    <div class="email_div">
                        <div id="SelectedGroupProfiles" runat="server">
                        </div>
                    </div>
                    <div class="invite_friends">
                        Profiles available for connection
                        <div class="email_div">
                            <div id="AllGroupProfiles" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="userandgroups">
                    <div class="invite_friends">
                        Team Members <a runat="server" id="inviteteamfromUserAndGroups" href="#">
                            <input id="Button3" onclick="gettingSessionForGroup();" type="button" value="Invite a New Team Member" />
                        </a>
                    </div>
                    <div class="first_name_last_name_div">
                    </div>
                    <div class="email_div">
                        <div id="inviTeamMem" runat="server">
                        </div>
                    </div>
                </div>
                <div class="ws_tm_button_div">
               
    
        
    </div>
            </div>
         </div>
 
    </div>

 
<script type="text/javascript">

    $("#home").removeClass('active');
    $("#message").removeClass('active');
    $("#feeds").removeClass('active');
    $("#discovery").removeClass('active');
    $("#publishing").removeClass('active');



    function changeClassandProfilesOfGroup(id) {
        debugger;
        var groupid = id.split('_');
        var totalgroups = document.getElementById("totalgroups").innerHTML;
        var actualgroupid = document.getElementById("itemid_" + groupid[1]).innerHTML;
        for (var j = 0; j < totalgroups; j++) {
            $("#group_" + j).removeClass("selected puff");
        }
        $("#group_" + groupid[1]).addClass("selected puff");
        var itemid = document.getElementById('itemid_' + groupid[1]).innerHTML;
        var groupname = document.getElementById('groupname_' + groupid[1]).value;
        $.ajax({
            type: "POST",
            url: "../Settings/AjaxInsertGroup.aspx?op=bindGroupProfiles&groupId=" + actualgroupid,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                document.getElementById("ContentPlaceHolder1_lblSelectedGroup").innerHTML = 'To ' + groupname;
                document.getElementById("ContentPlaceHolder1_SelectedGroupProfiles").innerHTML = msg;

                var chkeckinngdata = document.getElementById("ContentPlaceHolder1_SelectedGroupProfiles");
                var countdiv = chkeckinngdata.getElementsByTagName('div');
                debugger;
                for (var i = 0; i < countdiv.length; i++) {
                    var stringidchk = countdiv[i].id.split('_');

                    $("#usergroups_" + stringidchk[1]).hide();

                }

            }
        });



    }



    function GetGroupName(profilename) {

    }







    function showMessageErr(str, suc) {
        debugger;
        $("#errsuccessmsg").html(str);
        if (suc == "green") {
            $("#errsuccess").addClass('greenerrormsg');
            $("#errsuccess").removeClass('rederrormsg');
        } else if (suc == "red") {
            $("#errsuccess").removeClass('greenerrormsg');
            $("#errsuccess").addClass('rederrormsg');
        }


    }


    $("#<%=inviteteamfromUserAndGroups.ClientID %>").click(function (e) {
        debugger;
        var href = $("#<%=inviteteamfromUserAndGroups.ClientID %>").attr('href');
        if (href == "#") {
            alertify.alert("Create a Group to Invite team Members");

        } else {
            window.location = "../InviteTeamMember.aspx";
        }

    });






</script>
   
</asp:Content>
