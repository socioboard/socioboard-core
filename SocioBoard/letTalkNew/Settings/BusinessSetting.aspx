<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true" CodeBehind="BusinessSetting.aspx.cs" Inherits="letTalkNew.Settings.BusinessSetting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style type="text/css">
    #content section {display: block;}
    .container_right nav #indicator{left:195px;}
</style>
<nav>
        <ul>
            <li><a href="PersonalSetting.aspx">Personal Setting</a></li>
            <li><a class="current" href="BusinessSetting.aspx">Business Setting</a></li>
            <li><a href="UserGroups.aspx">User & Groups</a></li>
            <li><a href="Billing.aspx">Billing</a></li>
        </ul>
        <span id="indicator"></span> 
   </nav>
    <div id="content">
         <section>
            <div class="ws_tm_mid">
                  <div class="ws_tm_personal_setting">
                <div class="personal_setting_left">
                      <div class="invite_friends">Business Name </div>
                      <div class="email_div">
                            <asp:TextBox ID="txtBusnName" runat="server"></asp:TextBox>
                  </div>
                      <div class="invite_friends">Assigning Tasks </div>
                      <div class="ws_ps_user_content">
                    <div class="personal_details"> This setting will enable your users to assign tasks across groups and will make other group members visible. If you do not want users to assign tasks across groups, leave this setting "disabled". </div>
                    <div class="radio_content"> <span>
                       <asp:RadioButton ID="rbDisableAssignTask" runat="server" Text="Disabled" 
                                             GroupName="A" />
                                       
                                         
                      <label for="r7">
                      <p></p>
                      </label>
                      <em>Enabled</em>
                        <asp:RadioButton ID="rbEnableAssignTask" runat="server" 
                                             Text="" GroupName="A" />
                      <label for="r8">
                     <%-- <p></p>--%>
                      </label>
                     <%-- <em>Enable cross-group assignments</em> --%></span> </div>
                  </div>
                      <div class="invite_friends">Task Notification Emails</div>
                      <div class="ws_ps_user_content">
                    <div class="personal_details"> lets talk alerts you & your team members via email on various activities. </div>
                    <div class="radio_content"> <span>
                         <asp:RadioButton ID="rbDisableTaskNoti" runat="server" Text="Disabled" 
                                            GroupName="B" />
                                        
                                        
                      <label for="r9">
                     <%-- <p></p>--%>
                      </label>
                      <em>Enabled</em>
                         <asp:RadioButton ID="rbEnableTaskNoti" runat="server" 
                                            Text="" GroupName="B" />
                      <label for="r10">
                      <%--<p></p>--%>
                      </label>
                     <%-- <em>Enable cross-group assignments</em>--%> </span> </div>
                  </div>
                      <div class="invite_friends">Facebook Audience Confirmation</div>
                      <div class="ws_ps_user_content">
                    <div class="personal_details"> Displays a confirmation prompt to a user that they are publishing a Facebook wall post to their entire audience if they click Send without selecting a Location or Language. </div>
                    <div class="radio_content"> <span>
                      <asp:RadioButton ID="rbDoNotShowPromt" runat="server" Text="Disabled" 
                                             GroupName="C" />
                                            
                                   
                      <label for="r11">
                     <%-- <p></p>--%>
                      </label>
                      <em></em>
                         <asp:RadioButton ID="rbShowPromt" runat="server" Text="Enabled" 
                                             GroupName="C" />
                      <label for="r12">
                   <%--   <p></p>--%>
                      </label>
                     <%-- <em>Enable cross-group assignments</em>--%> </span> </div>
                  </div>
                      <div class="invite_friends">Facebook Photo Upload</div>
                      <div class="ws_ps_user_content">
                    <div class="personal_details"> Select an album to store photos uploaded to Facebook. We can use the default Facebook Timeline Photos album or a dedicated album titled lets talk Photos. You are able to rename the lets talk album within Facebook. </div>
                    <div class="radio_content"> <span>
                          <asp:RadioButton ID="rbFbTimeLine" runat="server" Text="Enabled" 
                                                GroupName="D" />                                             
                                         
                      <label for="r13">
                   <%--   <p></p>--%>
                      </label>
                      <em>Disabled</em>
                        <asp:RadioButton ID="rbFbPhotos" runat="server" Text="" 
                                                GroupName="D" />  
                      <label for="r14">
                    <%--  <p></p>--%>
                      </label>
                     <%-- <em>Enable cross-group assignments</em>--%> </span> </div>
                  </div>
                    </div>
                <div class="ws_tm_button_div">
                 <asp:Label ID="lblBusinessSettingSubmitStatus" runat="server"></asp:Label>
                      <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                                onclick="btnSubmit_Click" />
                    </div>
              </div>
                </div>
          </section>
    </div>
</asp:Content>
