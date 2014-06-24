<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true"
    CodeBehind="Message.aspx.cs" Inherits="SocioBoard.Message.Message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
<script src="../Contents/Scripts/SpryAccordion.js" type="text/javascript"></script> 
   <asp:HiddenField ID="hdnMemberId" runat="server" />
    <div class="ws_container_page">
        <aside role="complementary" class="unselectable" id="actions">
            <ul class="msgs_nav">
                <li id="inboxli" class="accordion single selected" onclick ="CallInbox();">
                    <a  href="#" onclick="" >
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
                                <span class="label">Smart Inbox</span>  
                                <span class="numeric" id="smartinbox_count" style="display:none"><span>0</span></span>
                        </span>
                    </a>
                </li>
                    <li id="tasksli" class="accordion single">
                    <a href="Task.aspx">
                        <span class="nav_icon">
                            <span class="msg_queue"></span>
                        </span>
                        <span class="text">
                            <span class="label">My Tasks</span>
                        </span>
                    </a>
                </li>
                    <li id="sentli" class="accordion  single">
                    <a  href="#">
                        <span class="nav_icon">
                            <span class="msg_sent"></span>
                        </span>
                        <span class="text">Sent Messages</span>
                    </a>
                
                </li>
                    <li id="instagramli" class="accordion  single">
                    <a  href="#" onclick="instagramcall();">
                        <span class="nav_icon">
                            <span class="msg_sent"></span>
                        </span>
                        <span class="text">Instagram</span>
                    </a>
      
                </li>

                <li class="accordion  single">
                    <a  href="#" >
                        <span class="nav_icon">
                            <span class="msg_sent"></span>
                        </span>
                        <span class="text">Google Plus</span>
                    </a>
          
                </li>
                         
                    <li id="archivemsg" class="accordion  single">
                    <a  href="#">
                        <span class="nav_icon">
                            <span class="msg_archive"></span>
                        </span>
                        <span class="text">Archive</span>
                    </a>
                </li>

            </ul>
        </aside>

        <div id="content" role="main">
            <div id="another-load" class="another-loader" style="display:none;">
                <img src="../Contents/Images/360.gif" />
            </div>
            <section id="inbox_msgs" class="threefourth messages msg_view">
                <div class="loader_div" style="display:block;">
                    <img src="../Contents/Images/328.gif" width="90" height="90" alt="" />
                </div>
                <%--  <div class="messages taskable">
                            <section>
                                <aside>
                                    <section class="js-avatar_tip" data-sstip_class="twt_avatar_tip">
                                        <a class="avatar_link view_profile" href="javascript:void(0)">
                                            <img width="54" height="54" border="0" class="avatar" src="../Contents/images/00031502_normal.jpg">
                                            <article class="message-type-icon">
                                                <span class="twitter_bm"> </span>
                                            </article>
                                        </a>
                                    </section>
                                </aside>

                                <article>
                                    <div class="">
                                        <a class="language" href=""></a>
                                    </div>
                                    <div class="message_actions">
                                        <a class="gear_small" href="#">
                                            <span title="Options" class="ficon">⚙</span>
                                        </a>
                                    </div>
                                    <div class="message-text font-14">Утро -13 весна продолжает удивлять!</div>
                                    <section class="bubble-meta">
                                        <article class="threefourth text-overflow">
                                            <section class="floatleft">
                                                <a data-tip="View Yaroslav Lukashev's Profile" class="js-avatar_tip view_profile profile_link" href="#" data-sstip_class="twt_avatar_tip">
                                                    <span>globus_net</span>
                                                </a>
                                                <a data-msg-time="1363926699000" class="time" target="_blank" title="View message on Twitter" href="#">30 minutes ago</a>, 
                                                    in<span class="location" title="Moscow">&nbsp;Moscow</span>
                                            </section>
                                        </article>
                                        <ul class="message-buttons quarter clearfix">
                                            <li><a href="#"><img src="../Contents/Images/replay.png" alt="" width="17" height="24" border="none" /></a></li>
                                            <li><a href="#"><img src="../Contents/Images/pin.png" alt="" width="14"  height="17" border="none" /></a></li>                                            
                                            <li><a href="#"><img src="../Contents/Images/archive.png" alt="" width="21" height="16" border="none" /></a></li>
                                        </ul>
                                    </section>
                                </article>
                            </section>
                        </div>--%>
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
                     <%--   <ul class="options_list">
                            <li>
                                <a>
                                    <span class="network_icon"><img src="../Contents/Images/msg/network_twt.png" width="17" height="16" alt="" /></span>
                                    <span class="user_name">Yash</span> 
                                    <span class="checkbx_green"><img src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" /></span>
                                </a>
                            </li>

                            <li>
                                <a>
                                    <span class="network_icon"><img src="../Contents/Images/msg/network_twt.png" width="17" height="16" alt="" /></span>
                                    <span class="user_name">Yash</span> 
                                    <span class="checkbx_green"><img src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" /></span>
                                </a>
                            </li>

                            <li>
                                <a>
                                    <span class="network_icon"><img src="../Contents/Images/msg/network_twt.png" width="17" height="16" alt="" /></span>
                                    <span class="user_name">Yash</span> 
                                    <span class="checkbx_green"><img src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" /></span>
                                </a>
                            </li>

                            <li>
                                <a>
                                    <span class="network_icon"><img src="../Contents/Images/msg/network_twt.png" width="17" height="16" alt="" /></span>
                                    <span class="user_name">Yash</span> 
                                    <span class="checkbx_green"><img src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" /></span>
                                </a>
                           </li>

                        </ul>--%>
                    </div>
                </div>
                <div class="AccordionPanel">
                    <div class="AccordionPanelTab">
                        <h3>
                            MESSAGE TYPES</h3>
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
                                    <span class="checkbx_green">
                                        <img id="message_directmessages"  src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" />
                                    </span> 
                                </a>
                            </li>

                            <li>
                                <a>
                                    <span class="network_icon"><img src="../Contents/Images/msg/network_twt.png" width="17" height="16" alt="" /></span>
                                    <span class="user_name">Retweets</span> 
                                    <span class="checkbx_green">
                                        <img id="message_retweets"  src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" />
                                    </span> 
                                </a>
                            </li>

                            <li>
                                <a>
                                    <span class="network_icon"><img src="../Contents/Images/msg/network_twt.png" width="17" height="16" alt="" /></span>
                                    <span class="user_name">New Followers</span> 
                                    <span class="checkbx_green">
                                        <img id="message_newfollowers" src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" />
                                   </span> 
                                </a>
                            </li>

                            <li>
                                <a>
                                    <span class="network_icon"> <img src="../Contents/Images/msg/network_fb_icon.png" width="17" height="16" alt="" /></span>
                                    <span class="user_name">User Wall Posts</span> 
                                    <span class="checkbx_green">
                                        <img id="message_userwallposts" onclick="checkmessageTypes(this.id);" src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" />
                                    </span> 
                                </a>
                            </li>

                            <li>
                                <a>
                                    <span class="network_icon">
                                        <img src="../Contents/Images/msg/network_fb_icon.png" width="17" height="16" alt="" />
                                    </span>
                                    <span class="user_name">User Comments</span> 
                                    <span class="checkbx_green">
                                        <img id="message_usercomments"  src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" />
                                   </span> 
                                </a>
                           </li>
                        </ul>
                    </div>
                </div>
                <div class="AccordionPanel">
                    <div class="AccordionPanelTab">
                        <h3>
                            BRAND KEYWORDS</h3>
                    </div>
                    <div class="AccordionPanelContent">
                        <ul id="ulgroupnames" class="options_list">
                            <li>
                                <a>
                                    <span class="network_icon">
                                        <img src="../Contents/Images/msg/Network_search.png" width="17" height="16" alt="" />
                                    </span>
                                    <span class="user_name">Globus</span> <span class="checkbx_green">
                                        <img src="../Contents/Images/msg/network_click.png" width="17" height="17" alt="" />
                                    </span>
                                </a>
                            </li>
                            <li>
                                <a>
                                    <span class="network_icon">
                                        <img src="../Contents/Images/msg/network_plus.png" width="17" height="16" alt="" />
                                    </span>
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
     
    <script type="text/javascript">
        var Accordion1 = new Spry.Widget.Accordion("Accordion1");
        $(document).ready(function () {


            $("#home").removeClass('active');
            $("#message").addClass('active');
            $("#feeds").removeClass('active');
            $("#discovery").removeClass('active');
            $("#publishing").removeClass('active');

            $("#aad").data();

            BindMessages();
            BindProfilesInMessageTab();

        });
    
    </script>
</asp:Content>
