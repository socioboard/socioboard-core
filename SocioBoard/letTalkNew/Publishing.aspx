<%@ Page Title="Publishing" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true" CodeBehind="~/Publishing1.aspx.cs" Inherits="letTalkNew.Publishing" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">


</asp:Content>

<asp:Content ID="ContentPlaceHolder1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%--scripts--%>
<script src="../Contents/js/jquery.session.js" type="text/javascript"></script>
<script src="../Contents/js/jquery.bpopup-0.9.3.min.js" type="text/javascript"></script>
<script src="../Contents/js/jquery.ui.core.js" type="text/javascript"></script>
<script src="../Contents/js/jquery.ui.datepicker.js" type="text/javascript"></script>
<script src="../Contents/js/multidatepicker/jquery-ui.multidatespicker.js" type="text/javascript"></script>
<%--<link href="../Contents/js/timepicer/include/ui-1.10.0/ui-lightness/jquery-ui-1.10.0.custom.min.css"
    rel="stylesheet" type="text/css" />
<link href="../Contents/js/timepicer/jquery.ui.timepicker.css" rel="stylesheet" type="text/css" />--%>
<%--timepicerscripts--%>
<%--<script src="../Contents/js/timepicer/include/ui-1.10.0/jquery.ui.widget.min.js"  type="text/javascript"></script>
<script src="../Contents/js/timepicer/include/ui-1.10.0/jquery.ui.core.min.js" type="text/javascript"></script>
<script src="../Contents/js/timepicer/include/ui-1.10.0/jquery.ui.position.min.js" type="text/javascript"></script>
<script src="../Contents/js/timepicer/jquery.ui.timepicker.js" type="text/javascript"></script>--%>
 <link rel="stylesheet" href="../Contents/css/jquery-ui.css" type="text/css"/>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js" type="text/javascript"></script>
    <script src="http://multidatespickr.sourceforge.net/jquery-ui.multidatespicker.js" type="text/javascript"></script>

   <%--  <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js"></script>--%>
    <link rel="stylesheet" type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.22/themes/redmond/jquery-ui.css" />
    <link href="Contents/css/jquery.ptTimeSelect.css" rel="stylesheet" type="text/css" />
    <script src="Contents/js/jquery.ptTimeSelect.js" type="text/javascript"></script>
  <%--  <link rel="stylesheet" type="text/css" href="jquery.ptTimeSelect.css" />
    <script type="text/javascript" src="jquery.ptTimeSelect.js"></script>--%>



<style type="text/css">
    .twitte_text 
    {
        padding: 5px 0 0 5px;
    }
    #datePick {border: 1px solid #333333; height: 24px; width: 136px;}
    #content section {display: block;}
    .timedate{float: right; height: auto; width: 110px;}
    .time_text{width:40px; height:auto; float:left;}
    .time_text > .time_set { float: left; font-family: Arial; font-size: 13px; height: 30px; margin-bottom: 3px; margin-top: 5px;  width: 100%;}
    
    
    .msg_view.threefourth {
    border: 1px solid #CCCCCC;
    border-radius: 5px 5px 5px 5px;
    max-width: 607px;
    padding: 10px;
    width: 100%;
}
.msg_view.threefourth > h2.league {
    clear: both;
    color: #B0B0B0;
    float: left;
    font-family: "league-gothic",sans-serif;
    font-size: 1.8em;
    font-weight: 400;
    margin: 0;
    text-shadow: 1px 1px 0 rgba(255, 255, 255, 0.5);
    text-transform: uppercase;
    width: 100%;
}
.msg_view.threefourth > h2.rss_header span {
    font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif;
    font-size: 12px !important;
    font-weight: 400;
    left: 3px;
    position: relative;
    text-shadow: none;
    text-transform: lowercase;
}
#rss > section:nth-child(2) {
    border-top: 1px solid #BBBEBF !important;
    box-shadow: 0 1px 0 rgba(255, 255, 255, 0.5) inset;
}
#rss > section:last-child {
    border-bottom: 0 none;
}
#rss > section.publishing {
    border-bottom: 1px solid #BBBEBF;
    border-top: 1px solid #F0F0F0;
    clear: both;
    overflow: hidden;
    padding: 10px;
}
.messages > section.publishing {
    display: block;
    min-height: 62px;
    padding: 10px 0;
}
#rss > section.publishing > section.twothird {
    float: left;
    width: 66%;
}
#rss section > section.third {
    float: right;
    margin-top: 24px;
    text-align: right;
}
#rss > section.publishing > section.twothird > .quarter {
    float: left;
    width: 20%;
}
#rss > section.publishing > section.twothird > .quarter > .avatar_link img.sm {
    float: left;
    height: 36px;
    width: 36px;
}
.rss_ava_icon {
    float: left;
    left: -9px;
    position: relative;
    top: 18px;
}
.icon {
    background-image: url("../Contents/img/sprite-main-162.png");
    display: inline-block;
    overflow: hidden;
    text-indent: 100%;
    vertical-align: text-bottom;
    white-space: nowrap;
}
.twitter_16 {
    background-position: 0 -250px;
    height: 16px;
    margin-top: 5px;
    width: 16px;
}
#rss > section.publishing > section.twothird > .threefourth {
    float: left;
    margin: 0;
    padding: 0;
    width: 74.5%;
}
#rss > section.publishing > section.twothird > .threefourth > ul {
    float: left;
    list-style: none outside none;
    margin-top: 0;
    padding: 0;
}
#rss > section.publishing > section.twothird > .threefourth > ul > li {
    clear: left;
    color: #545453;
    float: left;
    font-family: arial;
    font-size: 0.8em;
    margin-right: 10px;
}
#rss > section.publishing > section.twothird > .threefourth > ul > li.freq {
    color: #8F8F8D;
}
#rss > section.publishing > section.twothird > .threefourth > ul > li:last-child {
    margin: 0;
}
.small_pause {
    background-position: -153px -1001px;
    height: 12px;
    width: 11px;
}
.small_remove {
    background-position: -100px -1000px;
    height: 15px;
    position: relative;
    top: 2px;
    width: 15px;
}
#rss section.publishing > section.third > ul {
    float: left;
    list-style: none outside none;
    width: auto;
}
#rss section.publishing > section.third > ul > li {
    float: left;
    margin-right: 10px;
}
#rss > section.publishing > section.third > ul > li.show-on-hover {
    display: none;
    margin-top: 2px;
}
#rss > section.publishing:hover {
    background: none repeat scroll 0 0 #F1F1F1;
}
#rss > section.publishing:hover > section.third > ul > li.show-on-hover {
    display: block;
}
#contentcontainer1-publishing #content, #contentcontainer1-publishing #content_drafts, #contentcontainer1-publishing #content_wooqueue, #contentcontainer1-publishing #content_rsspost {
    left: 256px;
    width: 648px;
}
#content > section#inbox_msgs > .tasks-header, section.messages.msg_view.inbox_msgs > .tasks-header, #content_drafts > section.messages.msg_view.inbox_msgs > .tasks-header, #content_rsspost > section.messages.msg_view.inbox_msgs > .tasks-header {
    width: 648px;
}
#content > section#inbox_msgs > .messages, section.messages.msg_view.inbox_msgs > .messages, #content_drafts > section.messages.msg_view.inbox_msgs > .messages, #content_rsspost > section.messages.msg_view.inbox_msgs > .messages {
    width: 648px;
}
#content > section#inbox_msgs > .messages > section.section, section.messages.msg_view.inbox_msgs > .messages > section.section, #content_drafts > section.messages.msg_view.inbox_msgs > .messages > section.section, #content_rsspost > section.messages.msg_view.inbox_msgs > .messages > section.section {
    width: 648px;
}
#contentcontainer1-publishing > .publishing_div {
    float: left;
    height: auto;
    width: 694px;
}
#content, #content_drafts, #content_wooqueue, #content_rsspost {
    color: #A0A0A2;
    float: left;
    left: 173px;
    margin: 20px 10px;
    position: relative;
    width: 777px;
}
.dd {
    background-color: #FFFFFF;
    float: left;
    font-family: Arial,Helvetica,sans-serif;
    font-size: 12px;
    text-align: left;
}
.dd .ddTitle {
    background: none repeat scroll 0 0 #F2F2F2;
    border: 1px solid #C3C3C3;
    cursor: default;
    height: 16px;
    overflow: hidden;
    padding: 3px;
    text-indent: 0;
}
.dd .ddTitle span.arrow {
    background: url("dd_arrow.gif") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
    cursor: pointer;
    display: inline-block;
    float: right;
    height: 16px;
    width: 16px;
}
.dd .ddTitle span.ddTitleText {
    line-height: 16px;
    overflow: hidden;
    text-indent: 1px;
}
.dd .ddTitle span.ddTitleText img {
    padding: 0 2px 0 0;
    text-align: left;
}
.dd .ddTitle img.selected {
    padding: 0 3px 0 0;
    vertical-align: top;
}
.dd .ddChild {
    -moz-border-bottom-colors: none;
    -moz-border-left-colors: none;
    -moz-border-right-colors: none;
    -moz-border-top-colors: none;
    background-color: #FFFFFF;
    border-color: -moz-use-text-color #C3C3C3 #C3C3C3;
    border-image: none;
    border-right: 1px solid #C3C3C3;
    border-style: none solid solid;
    border-width: medium 1px 1px;
    display: none;
    margin: 0;
    overflow-x: hidden !important;
    overflow-y: auto;
    position: absolute;
    width: auto;
}
.dd .ddChild .opta a, .dd .ddChild .opta a:visited {
    padding-left: 10px;
}
.dd .ddChild a {
    color: #000000;
    cursor: pointer;
    display: block;
    overflow: hidden;
    padding: 2px 0 2px 3px;
    text-decoration: none;
    white-space: nowrap;
}
.dd .ddChild a:hover {
    background-color: #66CCFF;
}
.dd .ddChild a img {
    border: 0 none;
    padding: 0 2px 0 0;
    vertical-align: middle;
}
.dd .ddChild a.selected {
    background-color: #66CCFF;
}
.hidden {
    display: none;
}
.dd2 {
    background-color: #FFFFFF;
    float: left;
    font-family: Arial,Helvetica,sans-serif;
    font-size: 12px;
    text-align: left;
}
.dd2 .ddTitle {
    background: url("../images/msDropDown.gif") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
    cursor: default;
    height: 36px;
    overflow: hidden;
    padding: 0 3px;
    text-indent: 0;
}
.dd2 .ddTitle span.arrow {
    background: url("../images/icon-arrow.gif") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
    cursor: pointer;
    display: inline-block;
    float: right;
    height: 27px;
    position: relative;
    right: 2px;
    top: 5px;
    width: 27px;
}
.dd2 .ddTitle span.ddTitleText {
    color: #FFFFFF;
    font-family: Georgia,"Times New Roman",Times,serif;
    font-size: 16px;
    font-weight: bold;
    line-height: 33px;
    overflow: hidden;
    text-indent: 1px;
}
.dd2 .ddTitle span.ddTitleText img {
    padding: 0 2px 0 0;
    text-align: left;
}
.dd2 .ddTitle img.selected {
    padding: 0 2px 0 0;
    vertical-align: top;
}
.dd2 .ddChild {
    -moz-border-bottom-colors: none;
    -moz-border-left-colors: none;
    -moz-border-right-colors: none;
    -moz-border-top-colors: none;
    background-color: #FFFFFF;
    border-color: -moz-use-text-color #C3C3C3 #C3C3C3;
    border-image: none;
    border-right: 1px solid #C3C3C3;
    border-style: none solid solid;
    border-width: medium 1px 1px;
    display: none;
    font-size: 14px;
    margin: 0;
    overflow-x: hidden !important;
    overflow-y: auto;
    position: absolute;
    width: auto;
}
.dd2 .ddChild .opta a, .dd2 .ddChild .opta a:visited {
    padding-left: 10px;
}
.dd2 .ddChild a {
    color: #000000;
    cursor: pointer;
    display: block;
    overflow: hidden;
    padding: 3px 0 3px 3px;
    text-decoration: none;
    white-space: nowrap;
}
.dd2 .ddChild a:hover {
    background-color: #66CCFF;
}
.dd2 .ddChild a img {
    border: 0 none;
    padding: 0 2px 0 0;
    vertical-align: middle;
}
.dd2 .ddChild a.selected {
    background-color: #66CCFF;
}
.dd .ddChild a.sprite, .dd .ddChild a.sprite:visited {
    background-image: url("../icons/sprite.gif");
    background-repeat: no-repeat;
    padding-left: 24px;
}
.dd .ddChild a.calendar, .dd .ddChild a.calendar:visited {
    background-position: 0 -404px;
}
.dd .ddChild a.shoppingcart, .dd .ddChild a.shoppingcart:visited {
    background-position: 0 -330px;
}
.dd .ddChild a.cd, .dd .ddChild a.cd:visited {
    background-position: 0 -439px;
}
.dd .ddChild a.email, .dd .ddChild a.email:visited {
    background-position: 0 -256px;
}
.dd .ddChild a.faq, .dd .ddChild a.faq:visited {
    background-position: 0 -183px;
}
.dd .ddChild a.games, .dd .ddChild a.games:visited {
    background-position: 0 -365px;
}
.dd .ddChild a.music, .dd .ddChild a.music:visited {
    background-position: 0 -146px;
}
.dd .ddChild a.phone, .dd .ddChild a.phone:visited {
    background-position: 0 -109px;
}
.dd .ddChild a.graph, .dd .ddChild a.graph:visited {
    background-position: 0 -73px;
}
.dd .ddChild a.secured, .dd .ddChild a.secured:visited {
    background-position: 0 -37px;
}
.dd .ddChild a.video, .dd .ddChild a.video:visited {
    background-position: 0 0;
}
#content > section#inbox_msgs > .tasks-header > .task-activity, section.messages.msg_view.inbox_msgs > .tasks-header > .task-activity, #content_drafts > section.messages.msg_view.inbox_msgs > .tasks-header > .task-activity, #content_rsspost > section.messages.msg_view.inbox_msgs > .tasks-header > .task-activity {
    width: 18%;
}
#content > section#inbox_msgs > .messages > section > .read > .task-activity, section.messages.msg_view.inbox_msgs > .messages > section > .read > .task-activity, #content_drafts > section.messages.msg_view.inbox_msgs > .messages > section > .read > .task-activity, #content_rsspost > section.messages.msg_view.inbox_msgs > .messages > section > .read > .task-activity {
    width: 19.6%;
}
.edit-icon {
    float: left;
    height: auto;
    width: 16px;
}
.edit-icon > a > img {
    float: left;
    height: auto;
    margin-left: 5px;
    margin-top: 12px;
    width: 16px;
}
.section > .small_close_icon {
    display: none;
    float: right;
    margin-right: 16px;
    margin-top: -51px;
}
.section:hover > .small_close_icon {
    display: block;
}
</style>
	<script type="text/javascript">
	    $(document).ready(function () {

//	        $('.userpictiny').click(function () {
//	            //alert('asd');
//	            $('#loginBox_scheduler').slideToggle();
//	        });

	        $('#abb_scheduler').click(function () {
	            $('#ab_scheduler').slideToggle();
	        });

	        $('#datePick').multiDatesPicker();

	        $('input[name="time"]').ptTimeSelect();
	        SchedulerCompose();
	        //	        var indicator = $('#indicator'),
	        //					indicatorHalfWidth = indicator.width() / 2,
	        //					lis = $('#tabs').children('li');

	        //	        $("#tabs").tabs("#content section", {
	        //	            effect: 'fade',
	        //	            fadeOutSpeed: 0,
	        //	            fadeInSpeed: 400,
	        //	            onBeforeClick: function (event, index) {
	        //	                var li = lis.eq(index),
	        //					    newPos = li.position().left + (li.width() / 2) - indicatorHalfWidth;
	        //	                indicator.stop(true).animate({ left: newPos }, 600, 'easeInOutExpo');
	        //	            }
	        //	        });
	        $('document').click(function () {
	            alert('hello');
	            $('#loginBox_scheduler').hide();
	            $('#ab_scheduler').hide();
	        });
	    });
    </script>

    <style type="text/css">
        .ui-state-default, .ui-widget-content .ui-state-default, .ui-widget-header .ui-state-default,.ui-widget-header,.ui-datepicker th {font-size: 11px;}
        .ui-datepicker{width:12em !important;}
        #ptTimeSelectCntr{font-size:11px; width:19em;}        
        #adddates_scheduler > input { border: 1px solid #333333; height: 24px; margin-top: 11px;}
    </style>
      <nav>
          <ul>
              <li><a class="current" href="Publishing.aspx">Schedule Message </a></li>
              <li><a href="Queue.aspx">Queue</a></li>
              <li><a href="Draft.aspx">Drafts</a></li>
             <%-- <li><a href="#"> Post Via RSS</a></li>--%>
        </ul>
        <span id="indicator"></span> 
    </nav>
      <div id="content">
    <section>
    <div id="contentcontainer1-publishing">
          <div id="content">
        <div class="clearfix"> </div>
        <div id="publishing-dropdown-details" class="rounder">
              <div class="schedule_time"> 
              	<span>NEW MESSAGE</span>
              		<ul>
                    <%--	<li><a href="#">Select</a>
                        	<ul>
                            	<li><a href="#">facebook</a></li>
                                <li><a href="#">twitter</a></li>
                                <li><a href="#">Linked id</a></li>
                            </ul>
                        </li>--%>
                    </ul>
              </div>
              <div class="row-fluid">
            <div id="leftspan" class="span6">
                  <div class="usermessage">
                <div id="userpiccontainer">
                      <div class="userpictiny"> 
                      <img id="imageofuser_scheduler" width="48" height="48" src="../Contents/img/blank_img.png" alt="" /> 
                       
                      </div>
                      <div id="loginContainer_scheduler">
                    <div style="clear: both"> </div>
                    <div id="loginBox_scheduler" style="margin-top: 87px;position: absolute; z-index: 999; margin-left: -71px; display: none;">
                          <div class="drop_top"></div>
                          <div class="drop_mid">
                          <div class="twitte_text">FACEBOOK</div>
                          <div class="teitter">
                                        <ul>
                                    <li>No Records Found</li>
                                    </ul>
                                </div>
                        <div class="twitte_text">TWITTER</div>
                        <div class="teitter">
                              <ul>
                            <li>No Records Found</li>
                          </ul>
                            </div>
                        <div class="twitte_text">LINKEDIN</div>
                        <div class="teitter">
                              <ul>
                            <li>No Records Found</li>
                          </ul>
                            </div>
                      </div>
                        </div>
                  </div>
                    </div>
                <div id="btnaddnewmessage">
                      <div id="addContainer_scheduler">
                            <div id="abb_scheduler"> <a class="btn span6" style="height: 31px;">And +</a> </div>
                            <div style="clear: both"> </div>
                            <div  id="ab_scheduler" style="position: absolute; z-index: 999; margin-left: -59px; top: 106px; display:none;">
                                  <div class="drop_top"></div>
                                  <div class="drop_mid" id="connectedprof">
                                <div class="twitte_text">FACEBOOK</div>
                                <div class="teitter">
                                      <ul>
                                    <li>No Records Found</li>
                                  </ul>
                                    </div>
                                <div class="twitte_text">TWITTER</div>
                                <div class="teitter">
                                      <ul>
                                    <li>No Records Found</li>
                                  </ul>
                                    </div>
                                <div class="twitte_text">LINKEDIN</div>
                                <div class="teitter">
                                      <ul>
                                    <li>No Records Found</li>
                                  </ul>
                                    </div>
                              </div>
                            </div>

                            <div class="add_profile_div">                           
                              <%--  <span class="btn add_prof span_add">
                                    <img width="15" alt="" src="../Contents/img/facebook.png" alt="" />Prab Kumar
                                    <span class="close pull-right" data-dismiss="alert">×</span>
                                </span>
                                
                                <span class="btn add_prof span_add">
                                    <img width="15" alt="" src="../Contents/img/facebook.png" alt="" />Prab Kumar
                                    <span class="close pull-right" data-dismiss="alert">×</span>
                                </span>--%>
                          </div>

                      </div>

                      <div id="divformultiusers_scheduler"></div>
                    </div>
              </div>
                  <div class="schedular_textarea"> <span id="wordcount" class="charremain"> <i id="messageCount_scheduler">140 Characters Remaining</i> </span>
                <textarea id="textareavaluetosendmessage_scheduler" class="span12" style="height: 360px;" placeholder="type your message here" rows="10" cols="5"></textarea>
              </div>
                </div>
                <div id="rightspan" class="span6">
                          <div class="sub_tabs">
                                <div class="savetodraft" onClick="saveDrafts();">Save To Draft</div>
                                <div id="sendMessageBtn_scheduler" class="savetodraft" onclick="ScheduleMessage();">Schedule</div>
                          </div>
                        <div class="datebg">
                            <div class="time_text">
                                <div class="time_set">Date</div>
                                <div class="time_set">Time</div>
                            </div>
                            <div class="timedate" >
                            <input type="text" id="datePick" />
                     <%--       <div class="watchtimeshow">
                                <input type="text" style="width: 65px;" value="" disabled="disabled" id="timepickerforScheduler" />
                                <img id="imgtimepicker" src="Contents/img/clock.png" alt="" />
                                <div id="chktimepicker">
                                </div>
                            </div>--%>
                            <div id="adddates_scheduler">
                                   <%--         <div class="example">
                                <script>
                                    $(function () {
                                        $('#onselectExample').timepicker();
                                        $('#onselectExample').on('changeTime', function () {
                                            $('#onselectTarget').text($(this).val());
                                        });
                                    });
                                </script>
            
                                <p>
                                  <input id="onselectExample" type="text" class="time" />
                                  <span id="onselectTarget" style="margin-left: 30px;"></span>
                                </p>
                            </div>	--%>
                            <input name="time" id="timepickerforScheduler" value="" />

                                        </div>
                                        </div>
                        </div>
               </div>
               </div>
        </div>
        </div>
        </div>
        </section>
        
        

        <section> 
       		<div class="no_data">
                <h3>You don't have any RSS Feeds setup to automatically send messages for you</h3>
                    <p>
                    <a href="#" id="rsssetup" class="setupRssFeed">Setup an RSS Feed</a>
                    </p>
            </div> 
       </section>

      </div>
      <script type="text/javascript" language="javascript">
          var datearr = new Array();
          $(function () {
              $("#datepicker").datepicker();
          });

          function ScheduleMessage() {
              try {


                  debugger;
                  var curdate = new Date();
                  var now = (curdate.getMonth() + 1) + "/" + curdate.getDate() + "/" + curdate.getFullYear() + " " + curdate.getHours() + ":" + curdate.getMinutes() + ":" + curdate.getSeconds();

                  var message_scheduler = $("#textareavaluetosendmessage_scheduler").val();
                  $("#sendMessageBtn_scheduler").html('<img src="../Contents/img/325.gif" alt="" />');
                  var timeforsch = $("#timepickerforScheduler").val();

                  var bindingofdata_scheduler = document.getElementById('divformultiusers_scheduler');
                  var countdiv_scheduler = bindingofdata_scheduler.getElementsByTagName('div');
                //  alert(countdiv_scheduler.length);
                  for (var i = 0; i < countdiv_scheduler.length; i++) {
                      //alert(i);
                      //alert(countdiv_scheduler[i].id);
                      chkidforusertest.push(countdiv_scheduler[i].id);
                  }
               //   alert('singleprofileIdforscheduler' + singleprofileIdforscheduler);
                  if (singleprofileIdforscheduler != '') {
                      if (chkidforusertest.indexOf(singleprofileIdforscheduler) == -1) {
                          chkidforusertest.push(singleprofileIdforscheduler);
                      }
                  }
            //      alert("after singleprofileIdforscheduler");
                  try {
                      if (datearr.length == 0) {
                          var today = new Date();
                          var dd = today.getDate();
                          var mm = today.getMonth() + 1; //January is 0!
                          var yyyy = today.getFullYear();
                          var date = mm + '/' + dd + '/' + yyyy;
                          datearr.push(date);
                      }
                  }
                  catch (e) {
                      alert(e);
                  }


                  if ($("#textareavaluetosendmessage_scheduler").val() == "" || $("#datePick").val() == "" || $("#timepickerforScheduler").val()=="") {
                      alert('Plesae filed all the fields');
                      $("#sendMessageBtn_scheduler").empty();
                      $("#sendMessageBtn_scheduler").append('Schedule');
                      return false;
                  }
                  

                //  alert("message_scheduler" + message_scheduler);
                  if (message_scheduler != '') {
                      $.ajax
               ({
                   type: "POST",
                   url: "../AjaxHome.aspx?op=schedulemessage&datearr[]=" + datearr + "&users[]=" + chkidforusertest + "&message=" + message_scheduler + "&time=" + timeforsch + "&clittime=" + now,
                   data: '',
                   contentType: "application/json; charset=utf-8",
                   dataType: "html",
                   success: function (msg) {
                       debugger;
                       alert('Message Scheduled');
                       $("#sendMessageBtn_scheduler").empty();
                       $("#sendMessageBtn_scheduler").append('Schedule');
                       $("#timepickerforScheduler").val('');
                       $("#adddates_scheduler").val('');
                       $("#datePick").val('');

                       closeonCompose();
                   }
               });
                  }
              } catch (e) {

              }
   }

   function SchedulerCompose() {
       try {
           debugger;
           $.ajax
               ({
                   type: "POST",
                   url: "../AjaxHome.aspx?op=MasterCompose",
                   data: '',
                   contentType: "application/json; charset=utf-8",
                   dataType: "html",
                   success: function (msg) {
                       debugger;

                       var addmsg = msg.replace(/composemessage/g, "addAnotherProfileforMessage");
                       $("#connectedprof").html(addmsg);
                       $("#loginBox_scheduler").html(addmsg);

                       var countinguserids_scheduler = document.getElementById('loginBox_scheduler');
                       var countdivofloginbox_scheduler = countinguserids_scheduler.getElementsByTagName('li');
                       var firstid_scheduler = '';
                       for (var i = 0; i < countdivofloginbox_scheduler.length; i++) {
                           firstid_scheduler = countdivofloginbox_scheduler[i].id;
                           break;
                       }

                       //composemessage(firstid_scheduler, 'fb');

                   },
                   error: function (err) {
                       alert(err);
                   }
               });
       } catch (e) {
       alert(e);
       }
   }
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

</script>
</asp:Content>
