<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Publishing.aspx.cs" Inherits="WooSuite.Publishing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<link href="Contents/js/multidatepicker/css/mdp.css" rel="stylesheet" type="text/css" />
<link href="Contents/js/multidatepicker/css/prettify.css" rel="stylesheet" type="text/css" />
<link href="Contents/js/multidatepicker/css/pepper-ginder-custom.css" rel="stylesheet"
    type="text/css" />
<link href="Contents/css/Style_previous.css" rel="stylesheet" type="text/css" />
<link href="Contents/css/admin.css" rel="stylesheet" type="text/css" />
<link href="Contents/css/style.css" rel="stylesheet" type="text/css" />
<link href="Contents/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
<%--scripts--%>
<script src="Contents/js/jquery-1.7.2.js" type="text/javascript"></script>
<script src="Contents/js/jquery.session.js" type="text/javascript"></script>
<script src="Contents/js/jquery.bpopup-0.9.3.min.js" type="text/javascript"></script>
<script src="Contents/js/jquery.ui.core.js" type="text/javascript"></script>
<script src="Contents/js/jquery.ui.datepicker.js" type="text/javascript"></script>
<script src="Contents/js/multidatepicker/jquery-ui.multidatespicker.js" type="text/javascript"></script>
<link href="Contents/js/timepicer/include/ui-1.10.0/ui-lightness/jquery-ui-1.10.0.custom.min.css"
    rel="stylesheet" type="text/css" />
<link href="Contents/js/timepicer/jquery.ui.timepicker.css" rel="stylesheet" type="text/css" />
<%--timepicerscripts--%>
<script src="Contents/js/timepicer/include/ui-1.10.0/jquery.ui.widget.min.js" type="text/javascript"></script>
<script src="Contents/js/timepicer/include/ui-1.10.0/jquery.ui.core.min.js" type="text/javascript"></script>
<script src="Contents/js/timepicer/include/ui-1.10.0/jquery.ui.tabs.min.js" type="text/javascript"></script>
<script src="Contents/js/timepicer/include/ui-1.10.0/jquery.ui.position.min.js" type="text/javascript"></script>
<script src="Contents/js/timepicer/jquery.ui.timepicker.js" type="text/javascript"></script>
<%--this is for stylish alert box--%>
<script src="Contents/js/alertify.min.js" type="text/javascript"></script>
<link href="Contents/css/alertify.core.css" rel="stylesheet" type="text/css" />
<link href="Contents/css/alertify.default.css" rel="stylesheet" type="text/css" id="toggleCSS" />
<%--urlvalidate--%>
<%--<link rel="stylesheet" href="http://jquery.bassistance.de/validate/demo/site-demos.css">
<script type="text/javascript" src="http://jquery.bassistance.de/validate/jquery.validate.js"></script>
<script type="text/javascript" src="http://jquery.bassistance.de/validate/additional-methods.js"></script>--%>
<%--SocioboardScripts--%>
<script src="Contents/js/login.js" type="text/javascript"></script>
<script src="Contents/js/Feeds.js" type="text/javascript"></script>
<script src="Contents/js/Home.js" type="text/javascript"></script>
<script src="Contents/js/Message.js" type="text/javascript"></script>
<script src="Contents/js/Helper.js" type="text/javascript"></script>
<script src="Contents/js/publishing.js" type="text/javascript"></script>
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>MoopleSocial</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <!--[if IE 7]>
			<link href="css/font-awesome-ie7.min.css" rel="stylesheet">
		<![endif]-->
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
			<script src="js/html5shiv.js"></script>
		<![endif]-->
</head>
<script type="text/javascript">
    $(document).ready(function () {
        $('.cross').click(function () {
            debugger;
            $('.rederrormsg').fadeOut();
            $('.greenerrormsg').fadeOut();
        });
        $('#rsssetup').click(function () {

            $.ajax({
                url: '../Helper/AjaxHelper.aspx?op=rssusers',
                type: 'POST',
                data: '',
                dataType: 'html',
                success: function (msg) {
                    $("#rss_users").html(msg);
                }
            });

            $('.rssfeed').bPopup();
        });
    });
</script>
<body>
    <form id="form1" runat="server">
    <div id="header">
        <div id="top-nav">
            <div class="container">
                <div id="logo">
                    <a href="">
                        <img src="Contents/img/admin/logo.png" alt="WOOSUITE" /></a>
                </div>
                <div id="infocontainer">
                    <div id="msgcontainer">
                        <div id="msgcontent">
                            <a href="../Message/Messages.aspx">
                                <img alt="" src="Contents/img/admin/msgtiny.png" /><span runat="server" id="incom_messages">25</span></a></div>
                        <div id="tskcontent">
                            <a href="../Message/Task.aspx">
                                <img alt="" src="Contents/img/admin/remotetiny.png"><span runat="server" id="incom_tasks">0</span></a></div>
                    </div>
                    Information! Now You have <a href="~/Message/Messages.aspx" id="incomMessages" runat="server">
                        0</a> Incoming Message and <a runat="server" id="incomTasks" href="../Message/Task.aspx">
                            0</a> Task
                    <div id="errsuccess" class="greenerrormsg">
                        <span id="errsuccessmsg" class="msg">Successfull your work</span> <span class="cross">
                            X</span></div>
                </div>
                <div id="cmposecontainer">
                    <div id="composecontent">
                        <div id="logout">
                            <a href="../Default.aspx">
                                <img src="Contents/img/logout_woo.png" style="margin-right: 4px;" alt="" /><b>Logout</b></a></div>
                        <a href="#">
                            <img src="Contents/img/admin/111.png" alt="" />
                            Compose</a>
                    </div>
                    <div id="searchcontent">
                        <a href="#">
                            <img src="Contents/img/admin/061.png" alt="" /></a>
                    </div>
                </div>
            </div>
        </div>
        <div id="navi">
            <div class="container">
                <div class="row-fluid">
                    <div class="span3">
                        <div id="usercontainer">
                            <div id="userimg" runat="server">
                            </div>
                            <div id="usernm" runat="server">
                            </div>
                        </div>
                        <div id="topbtn">
                            <div class="navbar">
                                <div class="navbar-inner">
                                    <ul class="nav">
                                        <li class="dropdown">
                                            <%--<a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                    <img src="Contents/img/admin/inter.png" alt="">Inter<b class="caret" style="margin-left: 60px;
                                        margin-right: 10px"></b></a>
                                    <ul class="dropdown-menu">
                                        <li><a href="#">
                                            <img src="Contents/img/891.png" alt="" /></a></li>
                                    </ul>--%>
                                        </li>
                                        <li id="addico"><a>
                                            <img src="Contents/img/admin/addico.png" alt="" /></a>
                                            <div id="addicbox" style="display: none;">
                                                <div class="drop_top">
                                                </div>
                                                <div class="drop_mid loginbox">
                                                    <div class="teitter">
                                                        <ul>
                                                            <li><a id="master_facebook_connect" runat="server" onserverclick="AuthenticateFacebook">
                                                                <img width="18" border="none" style="float: left;" alt="" src="Contents/img/facebook.png" />
                                                                <span style="float: left; margin: 3px 0 0 5px;">Facebook</span> </a></li>
                                                        </ul>
                                                    </div>
                                                    <div class="teitter">
                                                        <ul>
                                                            <li><a id="master_TwitterOAuth" runat="server" onserverclick="AuthenticateTwitter">
                                                                <img width="18" border="none" style="float: left;" alt="" src="Contents/img/twitter.png" />
                                                                <span style="float: left; margin: 3px 0 0 5px;">Twitter</span> </a></li>
                                                        </ul>
                                                    </div>
                                                    <div class="teitter">
                                                        <ul>
                                                            <li><a id="master_LinkedInLink" runat="server" onserverclick="AuthenticateLinkedin">
                                                                <img width="18" border="none" style="float: left;" alt="" src="Contents/img/link.png" />
                                                                <span style="float: left; margin: 3px 0 0 5px;">LinkedIn</span> </a></li>
                                                        </ul>
                                                    </div>
                                                    <div class="teitter">
                                                        <ul>
                                                            <li><a id="master_InstagramConnect" runat="server" onserverclick="AuthenticateInstagram">
                                                                <img width="18" border="none" style="float: left;" alt="" src="Contents/img/instagram_24X24.png" />
                                                                <span style="float: left; margin: 3px 0 0 5px;">Instagram</span> </a></li>
                                                        </ul>
                                                    </div>
                                                    <div class="teitter">
                                                        <ul>
                                                            <li><a id="master_GooglePlusConnect" runat="server" onserverclick="AuthenticateGooglePlus">
                                                                <img width="18" border="none" style="float: left;" alt="" src="Contents/img/google_plus.png" />
                                                                <span style="float: left; margin: 3px 0 0 5px;">Google Plus</span> </a></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                        <li id="masterInvite"><a id="master_invite">
                                            <img src="Contents/img/admin/user2.png" alt="" /></a>
                                            <div id="inviteTeam" style="display: none;">
                                                <div class="drop_top">
                                                </div>
                                                <div class="drop_mid loginbox">
                                                    <div class="teitter">
                                                        <ul id="inviteRedirect" runat="server">
                                                            <li><a id="GroupNone">No Groups Found </a></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                        <li id="usersetting"><a>
                                            <img src="Contents/img/admin/usersetting.png" alt="" /></a>
                                            <div id="userset" style="display: none;">
                                                <div class="drop_top">
                                                </div>
                                                <div class="drop_mid loginbox">
                                                    <div class="teitter">
                                                        <ul>
                                                            <li><a href="../Settings/PersonalSettings.aspx">Personal Settings</a></li>
                                                        </ul>
                                                    </div>
                                                    <div class="teitter">
                                                        <ul>
                                                            <li><a href="../Settings/BusinessSetting.aspx">Business Settings</a></li>
                                                        </ul>
                                                    </div>
                                                    <div class="teitter">
                                                        <ul>
                                                            <li><a href="../Settings/UsersAndGroups.aspx">User and Groups</a></li>
                                                        </ul>
                                                    </div>
                                                    <div class="teitter">
                                                        <ul>
                                                            <li><a href="../Settings/Billing.aspx">Billing</a></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="span9">
                        <div class="navbar2">
                            <div class="navbar-inner">
                                <ul class="nav">
                                    <li id="home"><a href="../Home.aspx">
                                        <img src="Contents/img/admin/home.png" alt="" />Home</a> </li>
                                    <li id="message"><a href="../Message/Messages.aspx">
                                        <img src="Contents/img/admin/mail.png" alt="" />Message</a> </li>
                                    <li id="feeds"><a href="../Feeds/Feeds.aspx">
                                        <img src="Contents/img/admin/feeds.png" alt="" />Feeds</a> </li>
                                    <li id="publishing" class="active"><a href="../Publishing.aspx">
                                        <img src="Contents/img/admin/publishing.png" alt="" />Publishing</a> </li>
                                    <li id="discovery"><a href="../Discovery.aspx">
                                        <img src="Contents/img/admin/discovery.png" alt="" />Discovery</a> </li>
                                    <li><a href="WooReports/GroupStats.aspx">
                                        <img src="Contents/img/admin/reports.png" alt="" />Reports</a> </li>
                                    <li id="Li1"><a href="/Group/Group.aspx">
                                        <img alt="" src="/Contents/img/admin/reports.png" />Groups </a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container" id="mainwrapper">
        <div class="row-fluid">
            <div class="span3">
                <div id="sidebar">
                    <div class="sidebar-inner">
                        <a class="btn active" id="schedulemessage" onclick="publishcontent(this.id);" href="#">
                            Schedule Message</a><a class="btn" id="wooqueue" onclick="publishcontent(this.id);"
                                href="#">WooQueue</a> <a id="drafts" onclick="publishcontent(this.id);" class="btn"
                                    href="#">Drafts</a> <a id="rsspost" onclick="publishcontent(this.id);" class="btn"
                                        href="#">Post Via RSS</a>
                    </div>
                </div>
            </div>
            <div class="span9">
                <div id="contentcontainer2-publishing">
                    <div id="contentcontainer1-publishing">
                        <div id="content">
                            <%-- <div class="alert alert-suite">
                            <p>
                                Nothing scheduled in the Inter group</p>
                            <span class="pull-right">Schedule a new message</span>
                            <div class="red-caret">
                            </div>
                        </div>--%>
                            <div class="clearfix">
                                &nbsp;</div>
                            <div class="rounder" id="publishing-dropdown-details">
                                <%--  <button type="button" class="close pull-right" data-dismiss="alert">×</button>--%>
                                <h2 class="title">
                                    NEW MESSAGE</h2>
                                <div class="row-fluid">
                                    <div class="span6" id="leftspan">
                                        <div class="usermessage">
                                            <div id="userpiccontainer">
                                                <div class="userpictiny">
                                                    <img id="imageofuser_scheduler" width="48" height="48" alt="" src="Contents/img/blank_img.png" />
                                                    <a title="" class="userurlpic" href="#">
                                                        <img id="socialIcon_scheduler" alt="" src="Contents/img/admin/searchmini.png" style="height: 20px;
                                                            width: 20px;" /></a>
                                                </div>
                                                <div id="loginContainer_scheduler">
                                                    <div id="loginButton_scheduler">
                                                        <img src="Contents/img/drop_arrow.png" alt="" /></div>
                                                    <div style="clear: both">
                                                    </div>
                                                    <div id="loginBox_scheduler" style="position: absolute; z-index: 999; margin-left: -71px;">
                                                        <div class="drop_top">
                                                        </div>
                                                        <div class="drop_mid">
                                                            <a href="#">
                                                                <img src="Contents/img/891.png" alt="" /></a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="btnaddnewmessage">
                                                <div id="addContainer_scheduler">
                                                    <div id="abb_scheduler">
                                                        <a class="btn span6" style="height: 31px;">And +</a></div>
                                                    <div style="clear: both">
                                                    </div>
                                                    <div id="ab_scheduler" style="position: absolute; z-index: 999; margin-left: -20px;">
                                                        <div class="drop_top">
                                                        </div>
                                                        <div class="drop_mid">
                                                            <div class="twitte_text">
                                                                TWITTER</div>
                                                            <div class="teitter">
                                                                <ul>
                                                                    <li><a href="#">
                                                                        <img src="Contents/img/twitter.png" alt="" border="none" width="20" style="float: left;" />
                                                                        <span style="float: left; margin: 3px 0 0 5px;">Sumitghosh</span> </a></li>
                                                                    <li><a href="#">
                                                                        <img src="Contents/img/twitter.png" alt="" border="none" width="20" style="float: left;" />
                                                                        <span style="float: left; margin: 3px 0 0 5px;">Raj07</span> </a></li>
                                                                </ul>
                                                            </div>
                                                            <div class="twitte_text">
                                                                LINKEDIN</div>
                                                            <div class="teitter">
                                                                <ul>
                                                                    <li><a href="#">
                                                                        <img src="Contents/img/link.png" alt="" border="none" width="18" style="float: left;" />
                                                                        <span style="float: left; margin: 3px 0 0 5px;">Shyam SIngh</span> </a></li>
                                                                </ul>
                                                            </div>
                                                            <div class="twitte_text">
                                                                FACEBOOK</div>
                                                            <div class="teitter">
                                                                <ul>
                                                                    <li><a href="#">
                                                                        <img src="Contents/img/facebook.png" alt="" border="none" width="18" style="float: left;" />
                                                                        <span style="float: left; margin: 3px 0 0 5px;">Shyam SIngh</span> </a></li>
                                                                    <li><a href="#">
                                                                        <img src="Contents/img/facebook.png" alt="" border="none" width="18" style="float: left;" />
                                                                        <span style="float: left; margin: 3px 0 0 5px;">GLobussoft</span> </a></li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="divformultiusers_scheduler">
                                                    <%--<a class="btn span12" href="#">Syam Sharuk Kann <span class="close pull-right" data-dismiss="alert">×</span></a>--%></div>
                                            </div>
                                        </div>
                                        <div class="schedular_textarea">
                                            <span class="charremain" id="wordcount"><i id="messageCount_scheduler">140 CharactersRemaining</i></span>
                                            <%--<div onclick="saveDrafts();" class="savetodraft">Save To Draft</div>--%>
                                            <textarea id="textareavaluetosendmessage_scheduler" cols="5" rows="10" class="span12"
                                                placeholder="type your message here" style="height: 360px;"></textarea>
                                        </div>
                                    </div>
                                    <div class="span6" id="rightspan">
                                        <%-- <img alt="" src="Contents/img/admin/righ-details.png">--%>
                                        <div class="sub_tabs">
                                            <div onclick="saveDrafts();" class="savetodraft">
                                                Save To Draft</div>
                                            <div id="scheduleimg" style="float: right;">
                                                <img onclick="ScheduleMessage()" src="Contents/img/schedule.png" alt="" />
                                            </div>
                                        </div>
                                        <div class="datebg">
                                            <div id="multicaleder">
                                            </div>
                                            <div class="watchtimeshow">
                                                <input type="text" style="width: 65px;" value="" disabled="disabled" id="timepickerforScheduler" />
                                                <img id="imgtimepicker" src="Contents/img/clock.png" alt="" />
                                                <div id="chktimepicker">
                                                </div>
                                            </div>
                                            <div id="adddates_scheduler">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix">
                                &nbsp;</div>
                            <%--  <div class="shadower" id="calendar">
                        	    <img alt="" src="Contents/img/admin/calendar.png">
                            </div>--%>
                        </div>
                        <div id="content_wooqueue" style="display: none;">
                            <section class=" messages msg_view inbox_msgs">
                                    <div class="tasks-header">
                            	        <div class="task-owner"></div>
                                        <div class="task-activity">Send From</div>
                                        <div class="task-message">Message</div>
                                        <div class="task-status" style="width:55px;">Edit</div>
                                        <div class="task-status">Status</div>
                                        <div class="task-status">Network</div>
                                    </div>
                                    <div class="messages taskable" id="wooqueue_messages">
                                          <%--  <div class="js-task-cont read">
                                                <section class="task-owner">
                                                    <img width="32" height="32" border="0" src="Contents/img/user_img/The-best-top-desktop-hd-dark-black-wallpapers-dark-black-wallpaper-dark-background-dark-wallpaper-21.jpg" class="avatar">
                                                </section>
                                                <section class="task-activity third">
                                                    <p>Praveen Kumar</p>
                                                    <div>7/1/2013 5:49:02 PM</div>
                                                    <input type="hidden" value="#" id="hdntaskid_1">
                                                    <p>Assigned by Praveen Kumar</p>
                                              </section>
                                              <section class="task-message font-13 third"><a class="tip_left">again testing wiht woo.</a></section>
                                              <section class="task-status">
                                                <span class="ficon task_active" id="taskcomment">
                                                    <img width="14" height="17" alt="" src="Contents/img/task/task_pin.png" onclick="getmemberdata('7fd5773f-c5b0-4624-bba1-b8a6c0fbd56d');">
                                               </span>
                                               <div class="ui_light floating task_status_change">
                                                    <a href="#nogo" class="ui-sproutmenu">
                                                        <span class="ui-sproutmenu-status">True
                                                            <img title="Edit Status" onclick="PerformClick(this.id)" src="Contents/img/icon_edit.png" class="edit_button" id="img_7fd5773f-c5b0-4624-bba1-b8a6c0fbd56d_True"></span>
                                                   </a>
                                               </div>
                                           </section>
                                        </div>--%>
                                   </div>
                                </section>
                        </div>
                        <div id="content_drafts" style="display: none;">
                            <section class=" messages msg_view inbox_msgs">
                                    <div class="tasks-header" style="width:650px;">
                            	        <div class="task-owner"></div>
                                        <div class="task-activity">User</div>
                                        <div class="task-message">Message</div>
                                        <div class="task-status">Edit</div>
                                    </div>
                                    <div class="messages taskable" id="drafts_messages">
                                       
                                   </div>
                                </section>
                        </div>
                        <div id="content_rsspost" style="display: none;">
                            <div id="rdata" runat="server">
                                <div class="no_data">
                                    <h3>
                                        You don't have any RSS Feeds setup to automatically send messages for you</h3>
                                    <p>
                                        <a class="setupRssFeed" id="rsssetup">Setup an RSS Feed</a></p>
                                </div>
                            </div>
                            <section id="rss" runat="server" class="threefourth messages msg_view js-page-content"
                                style="display: none;">
                               
                        </section>
                        </div>
                        <div style="display: none;" id="pub-btncontainer">
                            <a class="btn btn-black">View "Recently Send"</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--div for compose message--%>
        <div id="composeBox" class="compose_box">
            <span class="close_button b-close"><span id="Span2" onclick="closeonCompose()">X</span></span>
            <div class="newmsd">
                NEW MESSAGE</div>
            <div class="pht_text_counter">
                <div class="pht_bg">
                    <div class="bgpht">
                        <img id="imageofuser" src="Contents/img/normal.jpg" alt="" style="height: 50px;" /></div>
                    <div class="twitter_icon">
                        <a href="#">
                            <img id="socialIcon" src="Contents/img/twitter.png" alt="" border="none" width="20" /></a></div>
                    <div id="loginContainer">
                        <div id="loginButton">
                            <img src="Contents/img/drop_arrow.png" alt="" /></div>
                        <div style="clear: both">
                        </div>
                        <div id="loginBox">
                            <div class="drop_top">
                            </div>
                            <div class="drop_mid">
                                <img src="Contents/img/small_loader.gif" alt="" style="margin-left: 90px;" /></div>
                            <%--          <div class="twitte_text">TWITTER</div>
                        <div class="teitter">
                           <ul>
                              <li><a href="#"><img src="Contents/img/twitter.png" alt="" border="none" width="20" style="float:left;" /> <span style="float:left;margin: 3px 0 0 5px;">Sumitghosh</span></a></li>
                              <li><a href="#"><img src="Contents/img/twitter.png" alt="" border="none" width="20" style="float:left;" /> <span  style="float:left;margin: 3px 0 0 5px;">Raj07</span></a></li>
                           </ul>
                        </div>
                        
                         <div class="twitte_text">LINKEDIN</div>
                        <div class="teitter">
                           <ul>
                              <li><a href="#"><img src="Contents/img/link.png" alt="" border="none" width="18" style="float:left;" /><span style="float:left;margin: 3px 0 0 5px;"> Shyam SIngh</span></a></li>
                           </ul>
                        </div>
                        
                        <div class="twitte_text">FACEBOOK</div>
                        <div class="teitter">
                           <ul>
                              <li><a href="#"><img src="Contents/img/facebook.png" alt="" border="none" width="18" style="float:left;" /><span style="float:left;margin: 3px 0 0 5px;"> Shyam SIngh</span></a></li>
                              <li><a href="#"><img src="Contents/img/facebook.png" alt="" border="none" width="18" style="float:left;" /><span style="float:left;margin: 3px 0 0 5px;"> GLobussoft</span></a></li>
                           </ul>
                        </div>
                        
                     </div>--%>
                        </div>
                    </div>
                </div>
                <div class="drop_textare">
                    <textarea id="textareavaluetosendmessage"></textarea></div>
                <div id="messageCount" class="counter_bg">
                    140</div>
            </div>
            <div class="pht_addbtn">
                <div id="addContainer">
                    <div id="addButton">
                        <img src="Contents/img/AddBtn.png" alt="" /></div>
                    <div id="divformultiusers">
                    </div>
                    <div style="clear: both">
                    </div>
                    <div id="addBox">
                        <div class="drop_top">
                        </div>
                        <div class="drop_mid">
                            <img src="Contents/img/small_loader.gif" style="margin-left: 90px;" alt="" />
                        </div>
                        <%--     <div class="twitte_text">TWITTER</div>
                        <div class="teitter">
                           <ul>
                              <li><a href="#"><img src="Contents/img/twitter.png" alt="" border="none" width="20" style="float:left;" /> <span style="float:left;margin: 3px 0 0 5px;">Sumitghosh</span></a></li>
                              <li><a href="#"><img src="Contents/img/twitter.png" alt="" border="none" width="20" style="float:left;" /> <span  style="float:left;margin: 3px 0 0 5px;">Raj07</span></a></li>
                           </ul>
                        </div>
                        
                         <div class="twitte_text">LINKEDIN</div>
                        <div class="teitter">
                           <ul>
                              <li><a href="#"><img src="Contents/img/link.png" alt="" border="none" width="18" style="float:left;" /><span style="float:left;margin: 3px 0 0 5px;"> Shyam SIngh</span></a></li>
                           </ul>
                        </div>
                        
                        <div class="twitte_text">FACEBOOK</div>
                        <div class="teitter">
                           <ul>
                              <li><a href="#"><img src="Contents/img/facebook.png" alt="" border="none" width="18" style="float:left;" /><span style="float:left;margin: 3px 0 0 5px;"> Shyam SIngh</span></a></li>
                              <li><a href="#"><img src="Contents/img/facebook.png" alt="" border="none" width="18" style="float:left;" /><span style="float:left;margin: 3px 0 0 5px;"> GLobussoft</span></a></li>
                           </ul>
                        </div>
                        
                     </div>--%>
                    </div>
                </div>
            </div>
            <div class="pht_addbtn">
                <%--         <div class="btn btn-success fileinput-button" style="background:url('Contents/img/attechphoto.png') transparent; height: 6px;width: 73px !important;margin: 12px 0 0 0; float:left; border:none;">
                   <input id="fileupload" type="file" name="files[]" multiple>
                </div>--%>
                <div class="send_btn">
                    <a id="sendMessageBtn" onclick="SendMessage()" href="#">
                        <img src="Contents/img/sendbtn.png" alt="" /></a></div>
                <%-- <div style="width:20px; height:20px;"><img src=</div>--%>
            </div>
        </div>
        <%--    div for contact search--%>
        <div id="contactsearch">
            <span class="close_button b-close"><span id="Span4">X</span></span>
            <div class="search_title">
                Contact Search</div>
            <div class="search_box">
                <input type="text" value="" id="contactvalue" />
                <a>
                    <img src="Contents/img/search_box.png" width="16" height="16" alt="" /></a>\
            </div>
            <div id="contactsearchresults" role="main">
                <section id="contactsection" class="threefourth messages msg_view" style="margin: 0;">
             
             </section>
            </div>
        </div>
        <!--Rss page -->
        <div class="rssfeed" style="display: none;">
            <span class="close_button b-close"><span id="Span1">X</span></span>
            <div class="add_rss">
                <span>Add an RSS Feed</span>
            </div>
            <div class="rssfeed_field">
                <div class="rss_row">
                    <div class="text_content">
                        Feed URL</div>
                    <div class="text_field">
                        <input id="rssfeedurl" type="text" class="left" onblur="val_url(this.value);" />
                        <div class="error_section">
                            Please enter a valid URL.</div>
                    </div>
                    <div id="rssfeedsurlerror" class="error_indication">
                        *</div>
                </div>
                <div class="rss_row">
                    <div class="text_content">
                        Prefix Text</div>
                    <div class="text_field">
                        <input id="rssmessage" type="text" placeholder="New Blog Post!" />
                        <%--<div class="error_section">Please enter a valid URL.</div>--%>
                    </div>
                    <%-- <div class="error_indication">*</div>--%>
                </div>
                <div class="rss_row">
                    <div class="text_content">
                        Check for New Posts</div>
                    <div class="text_field">
                        <select id="rssduration">
                            <option value="1">1 Hour</option>
                            <option value="2">2 Hours</option>
                            <option value="3">3 Hour</option>
                            <option value="4">4 Hour</option>
                        </select>
                    </div>
                </div>
                <div class="rss_row">
                    <div class="text_content">
                        Send From</div>
                    <div class="text_field">
                        <asp:DropDownList ID="ddlSendFrom" runat="server">
                        </asp:DropDownList>
                        <%-- <select id="rss_users">
                            
                        </select>--%>
                    </div>
                </div>
                <div class="rss_row">
                    <input id="saveRssFeeds" type="button" class="inactive" value="Save" />
                </div>
            </div>
        </div>
        <%--popup for edit wooqueue--%>
        <div id="woopopup" class="compose_box" style="">
            <span class="close_button b-close"><span id="Span3" onclick="closeonCompose()">X</span></span>
            <div class="newmsd">
                Edit WooQueue Message</div>
            <div class="pht_text_counter">
                <div class="pht_bg">
                    <div class="bgpht">
                        <img id="imageofuser_Woo" src="" alt="" style="height: 50px;" /></div>
                    <div class="twitter_icon">
                        <a href="#">
                            <img id="socialIcon_Woo" src="Contents/img/twitter.png" alt="" border="none" width="20" /></a></div>
                    <div id="loginContainer_Woo">
                        <div id="loginButton_Woo">
                            <img src="Contents/img/drop_arrow.png" alt="" /></div>
                        <div style="clear: both">
                        </div>
                        <div id="loginBox_Woo" style="margin-left: -72px;">
                            <div class="drop_top">
                            </div>
                            <div class="drop_mid">
                                <img src="Contents/img/small_loader.gif" alt="" style="margin-left: 90px;" /></div>
                        </div>
                    </div>
                </div>
                <div class="drop_textare">
                    <textarea id="textareavaluetosendmessage_Woo"></textarea></div>
                <%--   <div id="messageCount_Woo" class="counter_bg">
                    140</div>--%>
            </div>
            <div id="forid" style="display: none;">
            </div>
            <div id="profileidwithtype" style="display: none;">
            </div>
            <div id="profiletypeforwoo" style="display: none;">
            </div>
            <div class="pht_addbtn">
                <div class="send_btn">
                    <a id="sendMessageBtn_Woo" onclick="saveWooQueue();" href="#">
                        <img src="Contents/img/save.png" alt="" /></a></div>
                <%-- <div style="width:20px; height:20px;"><img src=</div>--%>
            </div>
        </div>
    </div>
    </form>
</body>
<script type="text/javascript" language="javascript">

    var datearr = new Array();

    $(document).ready(function () {
        SchedulerCompose();
        $("#ab_scheduler").hide();
        $("#chktimepicker").hide();
    });


    var totalmessagewords_scheduler = 140;
    $('#textareavaluetosendmessage_scheduler').bind('keyup', function () {
        debugger;
        $("#messageCount_scheduler").css('color', '#CDD3D3');
        var charactersUsed_scheduler = $(this).val().length;

        if (charactersUsed_scheduler > totalmessagewords_scheduler) {
            charactersUsed_scheduler = totalmessagewords_scheduler;
            $(this).val($(this).val().substr(0, totalmessagewords_scheduler));
            $(this).scrollTop($(this)[0].scrollHeight);
        }

        var charactersRemaining = totalmessagewords_scheduler - charactersUsed_scheduler;

        $('#messageCount_scheduler').html(charactersRemaining + ' Characters Remaining');

    });


    $("#imgtimepicker").click(function () {
        $("#chktimepicker").toggle();
    });

    $('#chktimepicker').timepicker({
        showPeriod: true,
        showLeadingZero: true,
        onSelect: function (time) {
            $("#timepickerforScheduler").val(time);
            $("#chktimepicker").hide();
            $("#timepickerforScheduler").attr('disabled', 'disabled');
        }
    });

    //    $("#timepickerforscheduler").timepicker();

    var today = new Date();

    $('#multicaleder').multiDatesPicker({
        dateFormat: "yy-mm-dd",
        onSelect: function (dates) {
            debugger;
            var index = $.inArray(dates, datearr);
            if (index < 0) {
                $('#adddates_scheduler').append('<div id="' + dates + '"  class="btn span12" style="width:47%;height:31px;"  onclick="dateDivDelete(this.id);"><b>' + dates + '</b> </ div><span data-dismiss="alert" class="close pull-right">×</span> </div>');
                datearr.push(dates);
            }
            else {
                $("#" + dates).remove();
                datearr.splice(index, 1);
            }
        },
        minDate: today

    });
    closeonCompose();
    //        $("#composeBox_scheduler").bPopup({

    //            fadeSpeed: 'slow', //can be a string ('slow'/'fast') or int
    //            followSpeed: 1500, //can be a string ('slow'/'fast') or int
    //            modalColor: 'black',
    //            modalClose: false,
    //            opacity: 0.6,
    //            positionStyle: 'fixed'
    //        });

    debugger;

    function SchedulerCompose() {
        try {
            $.ajax
               ({
                   type: "POST",
                   url: "../../AjaxHome.aspx?op=MasterCompose",
                   data: '',
                   contentType: "application/json; charset=utf-8",
                   dataType: "html",
                   success: function (msg) {
                       debugger;

                       var addmsg = msg.replace(/composemessage/g, "addAnotherProfileforMessage");
                       $("#ab_scheduler").html(addmsg);
                       $("#loginBox_scheduler").html(msg);

                       var countinguserids_scheduler = document.getElementById('loginBox_scheduler');
                       var countdivofloginbox_scheduler = countinguserids_scheduler.getElementsByTagName('li');
                       var firstid_scheduler = '';
                       for (var i = 0; i < countdivofloginbox_scheduler.length; i++) {
                           firstid_scheduler = countdivofloginbox_scheduler[i].id;
                           break;
                       }

                       composemessage(firstid_scheduler, 'fb');

                   }
               });
        } catch (e) {

        }
    }



    function dateDivDelete(id) {
        debugger;

        try {
            var s = datearr.pop(id);
            $("#" + id).remove();
            var bindingofdata = document.getElementsByTagName('a');


            for (var i = 0; i < bindingofdata.length; i++) {
                try {
                    var sss = bindingofdata[i].attr("firstChild");
                } catch (e) {

                } try {

                    //                    var sssss = bindingofdata[i];
                    //                    var dd = sssss["firstChild"];
                } catch (e) {

                }
            }


        } catch (e) {
            alert(e);
        }

    }


    function ScheduleMessage() {
        try {


            debugger;
            var curdate = new Date();
            var now = (curdate.getMonth() + 1) + "/" + curdate.getDate() + "/" + curdate.getFullYear() + " " + curdate.getHours() + ":" + curdate.getMinutes() + ":" + curdate.getSeconds();

            var message_scheduler = $("#textareavaluetosendmessage_scheduler").val();
            $("#sendMessageBtn_scheduler").html('<img src="Contents/img/325.gif" alt="" />');
            var timeforsch = $("#timepickerforScheduler").val();

            var bindingofdata_scheduler = document.getElementById('divformultiusers_scheduler');
            var countdiv_scheduler = bindingofdata_scheduler.getElementsByTagName('div');

            for (var i = 0; i < countdiv_scheduler.length; i++) {
                chkidforusertest.push(countdiv_scheduler[i].id);
            }

            if (singleprofileIdforscheduler != '') {
                if (chkidforusertest.indexOf(singleprofileIdforscheduler) == -1) {
                    chkidforusertest.push(singleprofileIdforscheduler);
                }
            }
            if (datearr.length == 0) {
                var today = new Date();
                var dd = today.getDate();
                var mm = today.getMonth() + 1; //January is 0!
                var yyyy = today.getFullYear();
                var date = mm + '/' + dd + '/' + yyyy;
                datearr.push(date);
            }

            if (message_scheduler != '') {
                $.ajax
               ({
                   type: "POST",
                   url: "AjaxHome.aspx?op=schedulemessage&datearr[]=" + datearr + "&users[]=" + chkidforusertest + "&message=" + message_scheduler + "&time=" + timeforsch + "&clittime=" + now,
                   data: '',
                   contentType: "application/json; charset=utf-8",
                   dataType: "html",
                   success: function (msg) {
                       debugger;
                       closeonCompose();
                   }
               });
            }
        } catch (e) {

        }
    }


    /*************************MasterScript*****************************/



    $("#addprofilesfromMaster").click(function (e) {
        debugger;
        $("#expanderContentForMaster").slideToggle();
    });
    $("#facebook_connect_master").click(function (e) {
        debugger;
        ShowFacebookDialog(false);
        e.preventDefault();
    });

    //          $(function () {
    //              'use strict';
    //              // Change this to the location of your server-side upload handler:
    //              var url = (window.location.hostname === 'blueimp.github.com' ||
    //                        window.location.hostname === 'blueimp.github.io') ?
    //                        '//jquery-file-upload.appspot.com/' : 'server/php/';
    //              $('#fileupload').fileupload({
    //                  url: url,
    //                  dataType: 'json',
    //                  done: function (e, data) {
    //                      $.each(data.result.files, function (index, file) {
    //                          $('<p/>').text(file.name).appendTo('#files');
    //                      });
    //                  },
    //                  progressall: function (e, data) {
    //                      var progress = parseInt(data.loaded / data.total * 100, 10);
    //                      $('#progress .bar').css(
    //                        'width',
    //                        progress + '%'
    //                    );
    //                  }
    //              });
    //          });

    $("#composecontent").click(function () {
        debugger;
        closeonCompose();
        //  ("#composeBox").bPopup();
        $('#composeBox').bPopup({
            fadeSpeed: 'slow', //can be a string ('slow'/'fast') or int
            followSpeed: 1500, //can be a string ('slow'/'fast') or int
            modalColor: 'black',
            modalClose: false,
            opacity: 0.6,
            positionStyle: 'fixed'
        });

        var totalmessagewords = 140;
        bindProfilesComposeMessage();

        $('#textareavaluetosendmessage').bind('keyup', function () {

            var charactersUsed = $(this).val().length;

            if (charactersUsed > totalmessagewords) {
                charactersUsed = totalmessagewords;
                $(this).val($(this).val().substr(0, totalmessagewords));
                $(this).scrollTop($(this)[0].scrollHeight);
            }

            var charactersRemaining = totalmessagewords - charactersUsed;

            $('#messageCount').html(charactersRemaining);

        });
    });

    $('#commonmenuforAll').click(function () {
        $('#commonmenuforAllClick').toggle();
    });

    $("#searchcontent").click(function () {

        $("#contactsearch").bPopup();

        $.ajax
        ({
            type: "POST",
            url: "../../Helper/AjaxHelper.aspx?op=usersearchresults",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {

            }
        });

    });
    var searchajax = '';

    $("#contactvalue").keyup(function () {

        try {
            searchajax.abort();
        } catch (e) {

        }
        searchajax = $.ajax
        ({
            type: "POST",
            url: "../../Helper/AjaxHelper.aspx?op=searchingresults&txtvalue=" + document.getElementById('contactvalue').value,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                debugger;
                $("#contactsection").html('');
                $("#contactsection").html(msg);
            }
        });

    });
   



</script>
</html>
