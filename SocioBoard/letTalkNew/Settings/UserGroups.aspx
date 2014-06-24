<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true" CodeBehind="UserGroups.aspx.cs" Inherits="letTalkNew.Settings.UserGroups" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Contents/css/Style_previous.css" rel="stylesheet" type="text/css" />
    <script src="../Contents/js/dialog_box.js" type="text/javascript"></script>
      <script src="../Contents/js/UsersAndGroups.js" type="text/javascript"></script>
<style type="text/css">
    #content section {display: block;}
    .container_right nav #indicator{left:330px;}
</style>
<nav>
        <ul>
            <li><a href="PersonalSetting.aspx">Personal Setting</a></li>
            <li><a href="BusinessSetting.aspx">Business Setting</a></li>
            <li><a class="current" href="UserGroups.aspx">User & Groups</a></li>
            <li><a href="Billing.aspx">Billing</a></li>
        </ul>
        <span id="indicator"></span> 
   </nav>
     <asp:HiddenField ID="HiddenFieldGroupName" runat="server" />
    <asp:HiddenField ID="HiddenFieldGroupNameInDDl" runat="server" />
    <div id="content">
         <section>
            <div class="ws_tm_mid">
                  <div class="ws_tm_personal_setting">
                <div class="userandgroups">
                      <div class="invite_friends"> Groups 
                        <input id="Button1" type="button" value=" Add a group" />
                       
                  <%--  <div id="myModal" class="reveal-modal">--%>
                          <div id="Insertgroupname" class="email_div" style="display: none;">
                        <div class="inst_title"> Create a Group  <span id="close" style="width: auto; float: right; margin-right: 10px; color: #fff;cursor: pointer;">close</span></div>
                         
                        <div class="instagram_title"> Groups help you organize your social profiles and team members. Name your group and you can begin connecting social profiles and adding team members to it. Twitter or Facebook required to create a group. </div>
                        <div class="instagram_txt_cont">
                              <div class="instagram_txt"> Group Name : </div>
                              <input id="txtGroupName" type="text" name="txtGroupName">
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
                          <a class="close-reveal-modal">&#215;</a> <%--</div>--%>
                  </div>
                      <div class="first_name_last_name_div"> Groups are used to categorize social profiles together to help manage and report on your social media efforts efficiently. Learn more about Groups </div>
                      <div id="Div1" class="email_div">

                  <div id="AllGroups" runat="server">


                        </div>
                  </div>
                    </div>
                <div class="userandgroups">
                      <div class="invite_friends"> Profile Linked  <asp:Label ID="lblSelectedGroup" runat="server" Text=""></asp:Label>
                      <a class="big-link" data-reveal-id="myModa2" data-animation="fade">
                        <input id="Button2" type="button" value="Connect a Profile ">
                        </a>
                   
                          <div id="insertFbPopup" class="email_div" style="display: none;">

                        <div class="conct_group">
                              <div class="cont_title"> CONNECT A PROFILE</div>
                             <asp:DropDownList ID="ddlGroup" runat="server">
                            </asp:DropDownList>
                          
                            </div>
                        <ul>
                              <li><a id="TwitOuthForProfile" runat="server" onclick="GetGroupName('twitter');" onserverclick="TwitterRedirect">
                              <img width="16" height="16" alt="" src="../Contents/img/twt_icon.png"> <span>Twitter</span> </a> 
                              </li>
                              <li><a id="FbOuthForProfile" runat="server" onclick="GetGroupName('facebook')" onserverclick="FacebookOAuthRedirect">
                              <img width="16" height="16" alt="" src="../Contents/img/fb_24X24.png"> <span>Facebook</span> </a> 
                              </li>
                              <li><a id="LinkedInLink" runat="server" onclick="GetGroupName('linkedin')" onserverclick="LinkedInRedirect">
                              <img width="16" height="16" alt="" src="../Contents/img/linked_25X24.png"> <span>LinkedIn</span> </a> 
                              </li>
                              <li> <a id="gp_cont" runat="server" onclick="GetGroupName('googleplus')" onserverclick="GooglePlusRedirect">
                              <img width="16" height="16" alt="" src="../Contents/img/google_plus.png"> <span>Google Plus</span> </a> 
                              </li>
                              <li> <a id="InstagramConnect" runat="server" onclick="GetGroupName('instagram')" onserverclick="InstagramRedirect"> 
                              <img width="16" height="16" alt="" src="../Contents/img/instagram_24X24.png"> <span>Instagram</span> </a> 
                              </li>
                            </ul>
                            <div class="cancel_proceed_div" style="width:634px !important;">
                            <input type="button" value="cancel" id="bclose">
                        </div>
                      </div>
                          <a class="close-reveal-modal">&#215;</a>
                  </div>
                      <div class="first_name_last_name_div"> </div>
                      <div class="email_div">
                   <div id="SelectedGroupProfiles" runat="server">
                        </div>
                  </div>
                      <div class="invite_friends"> Profiles available for connection
                    <div class="email_div">
                          <div id="AllGroupProfiles" runat="server">
                            </div>
                  </div>
                    </div>
                
              </div><div class="userandgroups">
                      <div class="invite_friends"> Team Members <a runat="server" id="inviteteamfromUserAndGroups" href="#" onclick="popup()">
                         <input id="Button3" onclick="gettingSessionForGroup();" type="button" value="Invite a New Team Member" />
                        </a>
                    <div id="myModa3" class="reveal-modal"> Create a Group to Invite team Members
                          <nav class="alertify-buttons"> <a id="alertify-ok" class="alertify-button alertify-button-ok" href="#">OK</a> </nav>
                          <a class="close-reveal-modal">&#215;</a> </div>
                  </div>
                      <div class="first_name_last_name_div"> </div>
                      <div class="email_div">
                    <div id="ContentPlaceHolder1_inviTeamMem"> </div>
                  </div>
                    </div>
                <div class="ws_tm_button_div"> </div>
                </div>
          </section>
    </div>

    <script type="text/javascript">

//    $("#home").removeClass('active');
//    $("#message").removeClass('active');
//    $("#feeds").removeClass('active');
//    $("#discovery").removeClass('active');
//    $("#publishing").removeClass('active');



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
        } else if(suc == "red") {
            $("#errsuccess").removeClass('greenerrormsg');
            $("#errsuccess").addClass('rederrormsg');
        }
    
    
    }
    </script>
</asp:Content>
