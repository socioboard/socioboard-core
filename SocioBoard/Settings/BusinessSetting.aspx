<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="BusinessSetting.aspx.cs" Inherits="SocialSuitePro.Settings.BusinessSetting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>SocialSuitePro-TeamMember-PersonalSetting</title>
    <link rel="stylesheet" type="text/css" href="../Contents/css/Style_previous.css" />
    <script src="../Contents/Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Contents/Scripts/jquery.bpopup-0.9.3.min.js" type="text/javascript"></script>
    <script src="../Contents/Scripts/jquery.bpopup.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="mainwrapper" class="container">
        <div class="ws_tm_left">
            <div class="team_member_div">
                <div class="tm_title">
                    Team Member</div>
                <h3><asp:Label id="memberName" runat="server"></asp:Label></h3>
                <ul>
                    <li><a href="PersonalSettings.aspx" >Personal Setting</a> </li>
                    <li><a class="active">Business Setting</a> </li>
                    <li><a href="UsersAndGroups.aspx">Users & Groups</a> </li>
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
                           <div class="personal_setting_left">
                           		<div class="invite_friends">Business Name </div>
                                <div class="email_div">
                                 <asp:TextBox ID="txtBusnName" runat="server"></asp:TextBox>

                                </div>
                                
                                <div class="invite_friends">Assigning Tasks </div>
                                <div class="ws_ps_user_content">                                	
                                    <div class="personal_details">
                                    	This setting will enable your users to assign tasks across groups and will make other group members visible. 
                                        If you do not want users to assign tasks across groups, leave this setting "disabled".
                                    </div>
                                    <div class="radio_content">
                                         <asp:RadioButton ID="rbDisableAssignTask" runat="server" Text="Disabled" 
                                             GroupName="A" />
                                       
                                             <asp:RadioButton ID="rbEnableAssignTask" runat="server" 
                                             Text="Enable cross-group assignments" GroupName="A" />
                                        
                                    </div>
                                         
                                    
                                </div>
                                
                                <div class="invite_friends">Task Notification Emails</div>
                                <div class="ws_ps_user_content">                                	
                                    <div class="personal_details">
                                    	Socioboard alerts you & your team members via email on various activities.
                                    </div>
                                    <div class="radio_content">
                                        <asp:RadioButton ID="rbDisableTaskNoti" runat="server" Text="Disabled" 
                                            GroupName="B" />
                                        
                                            <asp:RadioButton ID="rbEnableTaskNoti" runat="server" 
                                            Text="Enable Task Notifications" GroupName="B" />
                                                                            
                                    </div>
                                </div> 
                                
                                <div class="invite_friends">Facebook Audience Confirmation</div>
                                <div class="ws_ps_user_content">                                	
                                    <div class="personal_details">
                                    	Displays a confirmation prompt to a user that they are publishing a Facebook wall post to their entire audience 
                                        if they click Send without selecting a Location or Language.
                                    </div>
                                    <div class="radio_content">
                                         <asp:RadioButton ID="rbDoNotShowPromt" runat="server" Text="Do not show prompt" 
                                             GroupName="C" />
                                            
                                        <asp:RadioButton ID="rbShowPromt" runat="server" Text="Show prompt" 
                                             GroupName="C" />
                                    </div>
                                </div>
                                
                                <div class="invite_friends">Facebook Photo Upload</div>
                                <div class="ws_ps_user_content">                                	
                                    <div class="personal_details">
                                    	Select an album to store photos uploaded to Facebook. We can use the default Facebook Timeline Photos album or a 
                                        dedicated album titled Socioboard Photos. You are able to rename the Socioboard album within Facebook.
                                    </div>
                                    <div class="radio_content">
                                            <asp:RadioButton ID="rbFbTimeLine" runat="server" Text="Facebook Timeline" 
                                                GroupName="D" />                                             
                                            <asp:RadioButton ID="rbFbWooSuitePhotos" runat="server" Text="Socioboard Photos" 
                                                GroupName="D" />  
                                </div>
                                              
                           </div>
                           <!--end personal_setting_left-->
                        </div>
                        
                        
                        <div class="ws_tm_button_div">
                           <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                                onclick="btnSubmit_Click" />
                        </div>
                    </div>   
    </div>

    <script type="text/javascript" language="javascript">
        $("#home").removeClass('active');
        $("#message").removeClass('active');
        $("#feeds").removeClass('active');
        $("#discovery").removeClass('active');
        $("#publishing").removeClass('active');
    
    </script>
</asp:Content>
