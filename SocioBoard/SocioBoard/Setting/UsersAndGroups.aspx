<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true"
    CodeBehind="UsersAndGroups.aspx.cs" Inherits="SocioBoard.Setting.UsersAndGroups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>SocioCrowd-TeamMember-UserAndGroups</title>
   
   
    
  

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="HiddenFieldGroupName" runat="server" />
    <asp:HiddenField ID="HiddenFieldGroupNameInDDl" runat="server" />
    <div class="ws_container_page">
        <div class="ws_tm_left">
            <div class="team_member_div">
                <div class="tm_title">
                    Team Member</div>
                <h3>
                    <asp:Label ID="lblLoginName" runat="server" Text=""></asp:Label></h3>
                <ul>
                    <li><a href="PersonalSettings.aspx">Personal Setting</a> </li>
                    <li><a href="BusinessSetting.aspx">Business Setting</a> </li>
                    <li><a class="active">Users & Groups</a> </li>
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
                                <img src="../Contents/Images/twt_icon.png" width="16" height="16" alt="" />
                                <span>Twitter</span> </a></li>
                            <li><a id="facebook_connect" runat="server" onserverclick="FacebookRedirect" >
                                <img src="../Contents/Images/fb_24X24.png" width="16" height="16" alt="" />
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
                            <li><a id="TwitOuthForProfile" runat="server" onclick="GetGroupName('twitter');" onserverclick="TwitterOAuthRedirect">
                                <img src="../Contents/Images/twt_icon.png" width="16" height="16" alt="" />
                                <span>Twitter</span> </a></li>
                            <li><a id="FbOuthForProfile" runat="server" onclick="GetGroupName('facebook')" onserverclick="FacebookRedirect">
                                <img src="../Contents/Images/fb_24X24.png" width="16" height="16" alt="" />
                                <span>Facebook</span> </a></li>
                            <li><a id="LinkedInLink" runat="server">
                                <img src="../Contents/Images/linked_25X24.png" width="16" height="16" alt="" />
                                <span>LinkedIn</span> </a></li>
                            <li><a id="gp_cont" runat="server">
                                <img src="../Contents/Images/google_plus.png" width="16" height="16" alt="" />
                                <span>Google Plus</span> </a></li>
                            <li><a id="ga_cont" runat="server" href="#">
                                <img src="../Contents/Images/google_analytics.png" width="16" height="16" alt="" />
                                <span>Google Analytics</span> </a></li>
                            <li><a id="InstagramConnect" runat="server">
                                <img src="../Contents/Images/instagram_24X24.png" width="16" height="16" alt="" />
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
                        Team Members <a href="InviteMember.aspx">
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
               
        <script type="text/javascript" language="javascript">


            $(document).ready(function () {
                var totalgroups = $("#totalgroups").html();
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
                        url: "../Setting/AjaxInsertGroup.aspx?op=bindGroupProfiles&groupId=" + actualgroupid,
                        data: '',
                        contentType: "application/json; charset=utf-8",
                        dataType: "html",
                        success: function (msg) {
                            document.getElementById("<%=lblSelectedGroup.ClientID %>").innerHTML = 'To ' + groupname;
                            document.getElementById("<%=SelectedGroupProfiles.ClientID %>").innerHTML = msg;

                            var chkeckinngdata = document.getElementById("<%=SelectedGroupProfiles.ClientID %>");
                            var countdiv = chkeckinngdata.getElementsByTagName('div');
                            debugger;
                            for (var i = 0; i < countdiv.length; i++) {
                                var stringidchk = countdiv[i].id.split('_');

                                $("#usergroups_" + stringidchk[1]).hide();

                            }

                        }
                    });



                }

                if (totalgroups > 0) {
                    debugger;
                    changeClassandProfilesOfGroup('group_0')
                }

                $("#Button1").click(function () {
                    $("#Insertgroupname").css('display', 'block');

                });

                $("#close").click(function () {
                    $("#Insertgroupname").css('display', 'none');

                });

                $("#Button2").click(function () {
                    $("#insertFbPopup").css('display', 'block');

                });
                $("#bclose").click(function () {
                    $("#insertFbPopup").css('display', 'none');
                });

                function DeleteGroup(groupid, i) {

                    $("#group_" + i).hide();

                    $.ajax
                        ({
                            type: "post",
                            url: "../setting/ajaxinsertgroup.aspx?op=deleteGroupName&groupId=" + groupid,
                            data: '',
                            contenttype: "application/json; charset=utf-8",
                            datatype: "html",
                            success: function (msg) {
                                window.location.reload();
                            }
                        });

                }

                function gettingSessionForGroup() { 
                



                }


            });
            
        </script>
        
    </div>
</asp:Content>
