<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="Feeds.aspx.cs" Inherits="SocioBoard.Feeds.Feeds" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" src="../Contents/Scripts/SpryAccordion.js"></script>
   
<div class="ws_container_page">
        <aside role="complementary" class="unselectable" id="actions">
                        <ul class="msgs_nav">

                            <li id="facebookLiSideTab" class="accordion single selected">
                                <a href="#">
                                    <span class="nav_icon">
                                        <span data-tip="" class="responseRate proxima ss_tip tip_left">
                                              <span class="heart dark active">
                                                   <img src="../Contents/Images/heart-dark-mask-outline02.png" class="mask" alt="" />
                                                   <span><span style="height: 0%;" class="fill"></span></span>
                                                   <img src="../Contents/Images/heart-dark-fill.png" class="heart-fill" alt="" />
                                              </span>
                                            <span class="heart dark broken" style="display: none;"><img src="../Contents/Images/heart-broken-small.png" alt="" /></span>
                                        </span>
                                    </span>
                                    <span class="text">
                                        <span class="label">Facebook</span>  
                                        <span class="numeric" id="smartinbox_count" style="display:none"><span>0</span></span>
                                    </span>
                                </a>
                            </li>

                            <li id="twitterLiSideTab" class="accordion single">
                                <a href="#">
                                    <span class="nav_icon">
                                        <span class="msg_queue"></span>
                                    </span>
                                    <span class="text">
                                        <span class="label">Twitter</span>  
                                        <span class="numeric" id="tasks_count"><span>1</span></span>
                                    </span>
                                </a>
                            </li>

                            <li id="linkedinLiSideTab" class="accordion single">
                                <a href="#">
                                    <span class="nav_icon">
                                        <span class="msg_sent"></span>
                                    </span>
                                    <span class="text">LinkedIn</span>
                                </a>
                      
                            </li>
                              <li id="InstagramLiSideTab" class="accordion single">
                                <a href="#">
                                    <span class="nav_icon">
                                        <span class="msg_sent"></span>
                                    </span>
                                    <span class="text">Instagram</span>
                                </a>
                      
                            </li>


                        </ul>
    	</aside>
        <div id="content" role="main">
            <section id="inbox_msgs" class="threefourth messages msg_view">
                      <div class="loader_div" style="display:block;">
                    <img src="../Contents/Images/328.gif" width="90" height="90" alt="" />
                </div>
            </section>
        </div>
        <div class="ws_msg_right">
            <div id="Accordion1" class="Accordion" tabindex="0">
                <div class="AccordionPanel">
                    <div class="AccordionPanelTab">
                        <h3>
                            PROFILE</h3>
                    </div>
                    <div id="accordianprofiles" class="AccordionPanelContent">
                              <%--  <ul class="options_list">
                            <li><a><span class="network_icon">
                                <img src="../Contents/Images/msg/network_twt.png" width="17" height="16" alt="" /></span>
                                <span class="user_name">Yash</span> <span class="checkbx_green">
                                    <img src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" /></span>
                            </a></li>
                            <li><a><span class="network_icon">
                                <img src="../Contents/Images/msg/network_twt.png" width="17" height="16" alt="" /></span>
                                <span class="user_name">Yash</span> <span class="checkbx_green">
                                    <img src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" /></span>
                            </a></li>
                            <li><a><span class="network_icon">
                                <img src="../Contents/Images/msg/network_twt.png" width="17" height="16" alt="" /></span>
                                <span class="user_name">Yash</span> <span class="checkbx_green">
                                    <img src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" /></span>
                            </a></li>
                            <li><a><span class="network_icon">
                                <img src="../Contents/Images/msg/network_twt.png" width="17" height="16" alt="" /></span>
                                <span class="user_name">Yash</span> <span class="checkbx_green">
                                    <img src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" /></span>
                            </a></li>
                        </ul>--%>
                    </div>
                </div>
                <div class="AccordionPanel">
                    <div class="AccordionPanelTab">
                        <h3>MESSAGE TYPES</h3>
                    </div>
                    <div class="AccordionPanelContent">
                        <ul class="options_list">
                            <li>
                                <a>
                                    <span class="network_icon"><img src="../Contents/Images/msg/network_twt.png" width="17" height="16" alt="" /></span>
                                    <span class="user_name">Mentions</span> 
                                    <span class="checkbx_green">
                                        <img id="message_mentions" src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" />
                                    </span>
                                 </a>
                            </li>

                            <li>
                                <a>
                                    <span class="network_icon"><img src="../Contents/Images/msg/network_twt.png" width="17" height="16" alt="" /></span>
                                    <span class="user_name">Direct Messages</span> 
                                    <span class="checkbx_green"><img id="message_directmessages" src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" /></span> 
                                </a>
                           </li>

                            <li>
                                <a>
                                    <span class="network_icon"><img src="../Contents/Images/msg/network_twt.png" width="17" height="16" alt="" /></span>
                                    <span class="user_name">Retweets</span> 
                                    <span class="checkbx_green"><img id="message_retweets" src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" /></span> 
                                </a>
                            </li>

                            <li>
                                <a>
                                    <span class="network_icon"><img src="../Contents/Images/msg/network_twt.png" width="17" height="16" alt="" /></span>
                                    <span class="user_name">New Followers</span> 
                                    <span class="checkbx_green"><img id="message_newfollowers" src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" /></span> 
                                </a>
                            </li>

                            <li>
                                <a>
                                    <span class="network_icon"><img src="../Contents/Images/msg/network_fb_icon.png" width="17" height="16" alt="" /></span>
                                    <span class="user_name">User Wall Posts</span> 
                                    <span class="checkbx_green"><img id="message_userwallposts" src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" /></span> 
                               </a>
                           </li>

                           <li>
                               <a>
                                    <span class="network_icon"><img src="../Contents/Images/msg/network_fb_icon.png" width="17" height="16" alt="" /></span>
                                    <span class="user_name">User Comments</span> 
                                    <span class="checkbx_green"><img id="message_usercomments"  src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" /></span> 
                               </a>
                          </li>

                        </ul>
                    </div>
                </div>
                <div class="AccordionPanel">
                    <div class="AccordionPanelTab">
                        <h3>BRAND KEYWORDS</h3>
                    </div>
                    <div class="AccordionPanelContent">
                        <ul class="options_list">
                            <li>
                                <a>
                                    <span class="network_icon"><img src="../Contents/Images/msg/Network_search.png" width="17" height="16" alt="" /></span>
                                    <span class="user_name">Globus</span> 
                                    <span class="checkbx_green"><img src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" /></span>
                                </a>
                            </li>

                            <li>
                                <a>
                                    <span class="network_icon"><img src="../Contents/Images/msg/network_plus.png" width="17" height="16" alt="" /></span>
                                    <span class="user_name">Add Brand Keyword</span> 
                                    <span class="checkbx_green"></span>
                                </a>
                           </li>

                        </ul>
                    </div>
                </div>
            </div>
        </div>
 </div>
   <div id ="instaAccounts">
    <span class="close_button b-close"><span id="Span4">X</span></span>
    <div class="usertweets">Instagram Accounts</div>
    <div id="accountsins">
        <img src="../Contents/Images/00031502_normal.jpg" />
        <img src="../Contents/Images/00031502_normal.jpg" />
        <img src="../Contents/Images/00031502_normal.jpg" />
        <img src="../Contents/Images/00031502_normal.jpg" />
    </div>
    </div>
 <script type="text/javascript">
     var Accordion1 = new Spry.Widget.Accordion("Accordion1");

     $(document).ready(function () {
         $("#home").removeClass('active');
         $("#message").removeClass('active');
         $("#feeds").addClass('active');
         $("#discovery").removeClass('active');
         $("#publishing").removeClass('active');
         BindFeeds("facebook");
         

     });

     $("#facebookLiSideTab").click(function () {
         $("#twitterLiSideTab").removeClass('selected');
         $("#facebookLiSideTab").addClass('selected');
         $("#linkedinLiSideTab").removeClass('selected');
         $("#InstagramLiSideTab").removeClass('selected');
         $("#inbox_msgs").html('<div class="loader_div" style="display:block;"><img src="../Contents/Images/328.gif" width="90" height="90" alt="" /></div>');
         BindFeeds("facebook");
     });

     $("#twitterLiSideTab").click(function () {
         $("#twitterLiSideTab").addClass('selected');
         $("#facebookLiSideTab").removeClass('selected');
         $("#linkedinLiSideTab").removeClass('selected');
         $("#InstagramLiSideTab").removeClass('selected');
         $("#inbox_msgs").html('<div class="loader_div" style="display:block;"><img src="../Contents/Images/328.gif" width="90" height="90" alt="" /></div>');
         BindFeeds("twitter");
     });
     $("#linkedinLiSideTab").click(function () {
         $("#twitterLiSideTab").removeClass('selected');
         $("#facebookLiSideTab").removeClass('selected');
         $("#linkedinLiSideTab").addClass('selected');
         $("#InstagramLiSideTab").removeClass('selected');
         $("#inbox_msgs").html('<div class="loader_div" style="display:block;"><img src="../Contents/Images/328.gif" width="90" height="90" alt="" /></div>');
         BindFeeds("linkedin");
     });
     $("#InstagramLiSideTab").click(function () {
         $("#twitterLiSideTab").removeClass('selected');
         $("#facebookLiSideTab").removeClass('selected');
         $("#linkedinLiSideTab").removeClass('selected');
         $("#InstagramLiSideTab").addClass('selected');
         $("#inbox_msgs").html('<div class="loader_div" style="display:block;"><img src="../Contents/Images/328.gif" width="90" height="90" alt="" /></div>');
         BindFeeds("instagram");
     });
    </script>
</asp:Content>
