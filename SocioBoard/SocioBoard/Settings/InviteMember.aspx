<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="InviteMember.aspx.cs" Inherits="SocialSuitePro.Settings.InviteMember" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
   <script src="../Contents/js/Home.js" type="text/javascript"></script>
    <script src="../Contents/js/SimplePopup.js" type="text/javascript"></script>
    <script src="../Contents/js/jquery.bpopup-0.9.3.min.js" type="text/javascript"></script> 
  </asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--<script src="../Contents/js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Contents/js/jquery-1.4.1.js" type="text/javascript"></script>--%>
    
 <link rel="stylesheet" type="text/css" href="../Contents/css/Style_previous.css" />
  
    <div class="ws_container_page">
                 <asp:HiddenField ID="HiddenfieldProfID" runat="server" />
                    	<div class="ws_tm_left">
                    	<div class="team_member_div">
                        	<div class="tm_title">Team Member</div>
                            <h3><asp:Label id="memberName" runat="server"></asp:Label></h3>
                            <ul>
                            	<li><a href="PersonalSettings.aspx">Personal Setting</a></li>
                                <li><a href="BusinessSetting.aspx" >Business Setting</a></li>
                                <li><a href="UsersAndGroups.aspx">Users & Groups</a></li>
                               
                                <li><a href="Billing.aspx">Billing</a><li>
                              
                            </ul>
                        </div>
                    </div>
                    
                    <div class="ws_tm_mid">
                    	<div class="invite_friends">Invite a Team Member <a href="../Home.aspx">← Back to Accounts</a></div>
                        <div id="settings"><h5>Your billing will be adjusted to reflect additional users  at <strong>$99.00/mo</strong></h5></div>
                        <div class="first_name_last_name_div">
                        	
                            <div class="first_error">
                                <asp:TextBox ID="txtFirstName" runat="server" placeholder="FirstName"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="star-ribbon" runat="server" ErrorMessage="" ControlToValidate="txtFirstName" ForeColor="Red"></asp:RequiredFieldValidator>
                           </div>
                            <div class="first_error">                                
                                <asp:TextBox ID="txtLastName" runat="server"  placeholder="LastName"></asp:TextBox>                             
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="" ControlToValidate="txtLastName" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            <div class="first_error">     
                            </div>
                        </div>
                       
                        <div class="email_div">
                        	
                             <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter Email Address"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="" ControlToValidate="txtEmail" ForeColor="Red"></asp:RequiredFieldValidator>
                              <span class="error-keyup-3"></span>
                        </div>
                      
                        
                        <div class="ws_tm_network">
                        
                        <div class="twitter">
                            	<div class="twitter_list">
                                	<div class="twitter_icon"></div>
                                    <div class="desc">Twitter Accounts</div>
                                    <div class="filter">
                                    <input type="checkbox" id="selectallTwt"/>
                                    </div>
                                </div>
                                <div id="TwitterAc" runat="server" >
                                No Accounts For this Group
                                </div>
                            </div>
                        
                          <div class="facebook">
                            	<div class="facebook_list">
                                	<div class="facebook_icon"></div>
                                    <div class="desc">Facebook Profile</div>
                                    <div class="filter">
                                    <input type="checkbox" id="selectallFb"/>
                                    
                                    </div>
                                </div>
                                 <div id="FacebookAc" runat="server">
                                </div>
                               </div>

                               <div class="linkedin">
                            	<div class="linkedin_list">
                                	<div class="linkedin_icon"></div>
                                    <div class="desc">LinkedIn Profile</div>
                                    <div class="filter">
                                     <input type="checkbox" id="selectallLd"/>
                                    </div>
                                </div>
                                <div id="LinkedInAc" runat="server" class="ws_tm_network_one">
                                 No Accounts For this Group
                                </div>
                               </div>

                            <%--   <div class="ganalytics">
                            	<div class="ganalytics_list">
                                	<div class="ganalytics_icon"></div>
                                    <div class="desc">Google Analytics Profile</div>
                                    <div class="filter">
                                   <input type="checkbox" id="selectallGa"/>
                                    </div>
                                </div>
                                <div id="GAAc" runat="server" class="ws_tm_network_one">
                                </div>
                               </div>

                               <div class="ganalytics">
                            	<div class="ganalytics_list">
                                	<div class="ganalytics_icon"></div>
                                    <div class="desc">Google Plus</div>
                                    <div class="filter">
                                    <input type="checkbox" id="selectallGp"/>
                                 </div>
                                </div>
                                <div id="GplusAc" runat="server" class="ws_tm_network_one">
                                </div>
                               </div>--%>

                               <div class="ganalytics">
                            	<div class="ganalytics_list">
                                	<div class="ganalytics_icon"></div>
                                    <div class="desc">Instagram Profile</div>
                                    <div class="filter">
                                    <input type="checkbox" id="selectallIns"/>
                                    </div>
                                </div>
                                <div id="InstagramAc" runat="server" class="ws_tm_network_one">
                                 No Accounts For this Group
                                </div>
                               </div>




                               <div class="ganalytics">
                            	<div class="ganalytics_list">
                                	<div class="ganalytics_icon"></div>
                                    <div class="desc">Tumblr Profile</div>
                                    <div class="filter">
                                    <input type="checkbox" id="selectallTumblr"/>
                                    </div>
                                </div>
                                <div id="TumblrAc" runat="server" class="ws_tm_network_one">
                                 No Accounts For this Group
                                </div>
                               </div>







                               <div id="totalaccountscheck" style="display:none;" runat="server"></div>
                               </div>
                               
                               

                        <div class="ws_tm_button_div">
                            	<%--<input type="button" value="SEND INVITES" />
                                <input type="button" value="CANCEL" />--%>
                                  <div>
                    <asp:Label ID="Label1" runat="server" ForeColor="#999999"></asp:Label>
                    </div>
                                <asp:Button ID="btnSendInvite" runat="server" Text="SEND INVITES" onclick="btnSendInvite_Click"/>
                                <asp:Button ID="btnCancel" runat="server" Text="CANCEL" 
                                    onclick="btnCancel_Click"/>
                        </div>
                    
                    
                    
                </div>
    </div>
    <div id="allcheckcounts" style="display:none;" runat="server"></div>
     <script src="../Contents/js/InviteTeamMember.js" type="text/javascript"></script>
  
     <script type="text/javascript" language="javascript">
         $("#home").removeClass('active');
         $("#message").removeClass('active');
         $("#feeds").removeClass('active');
         $("#discovery").removeClass('active');
         $("#publishing").removeClass('active');


         function disp_confirm() {
             try {
                 alertify.alert("No Profiles Are Selected to Send Invitation");

             } catch (e) {

             }
         
         }

     </script>
</asp:Content>
