<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Schedule.aspx.cs" Inherits="SocioBoard.Schedule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    
<html xmlns="http://www.w3.org/1999/xhtml">
<link href="Contents/Styles/Style.css" rel="stylesheet" type="text/css" />
<link href="Contents/Scripts/multidatepicker/css/mdp.css" rel="stylesheet" type="text/css" />
    <link href="Contents/Scripts/multidatepicker/css/prettify.css" rel="stylesheet" type="text/css" />
    <link href="Contents/Scripts/multidatepicker/css/pepper-ginder-custom.css" rel="stylesheet" type="text/css" />
   
   <%--scripts--%>
   

    <script src="Contents/Scripts/jquery-1.7.2.js" type="text/javascript"></script>
<script src="Contents/Scripts/jquery.session.js" type="text/javascript"></script>
    <script src="Contents/Scripts/jquery.bpopup-0.9.3.min.js" type="text/javascript"></script>
    <script src="Contents/Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="Contents/Scripts/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="../Contents/Scripts/multidatepicker/jquery-ui.multidatespicker.js" type="text/javascript"></script>

<link href="Contents/Scripts/timepicer/include/ui-1.10.0/ui-lightness/jquery-ui-1.10.0.custom.min.css"
    rel="stylesheet" type="text/css" />
   <link href="Contents/Scripts/timepicer/jquery.ui.timepicker.css" rel="stylesheet"
    type="text/css" />



    <%--timepicerscripts--%>

<script src="Contents/Scripts/timepicer/include/ui-1.10.0/jquery.ui.widget.min.js"
    type="text/javascript"></script>
<script src="Contents/Scripts/timepicer/include/ui-1.10.0/jquery.ui.core.min.js"
    type="text/javascript"></script>
<script src="Contents/Scripts/timepicer/include/ui-1.10.0/jquery.ui.tabs.min.js"
    type="text/javascript"></script>
<script src="Contents/Scripts/timepicer/include/ui-1.10.0/jquery.ui.position.min.js"
    type="text/javascript"></script>
<script src="Contents/Scripts/timepicer/jquery.ui.timepicker.js" type="text/javascript"></script>

    <%--SocioboardScripts--%>

    <script src="../Contents/Scripts/login.js" type="text/javascript"></script>
    <script src="../Contents/Scripts/Feeds.js" type="text/javascript"></script>
    <script src="../Contents/Scripts/Home.js" type="text/javascript"></script>
    <script src="../Contents/Scripts/Message.js" type="text/javascript"></script>
    <script src="../Contents/Scripts/Helper.js" type="text/javascript"></script>


<head runat="server">
    <title></title>
</head>
<body>
     <div class="main_page">
    <div class="header">
        <div class="header_wrapper">
                <div class="header_wrapper_top">
                    <!--a class="logout" href="../Login.aspx">Logout</a-->
                    <div class="settings">
                        <a class="settingclick">
                            <img id="commonmenuforAll" src="../Contents/Images/setting.png" width="16" height="18" alt=""  /></a>
                        <div id="commonmenuforAllClick" class="ss_actions_menu">
                            <div class="title">
                                Settings</div>
                            <div class="submenus">
                                <a href="../Setting/PersonalSettings.aspx">Personal Settings</a> <a href="../Setting/BusinessSetting.aspx">
                                    Business Settings</a> <a href="../Setting/UsersAndGroups.aspx">Users & Groups</a>
                            </div>
                            <a class="logout" href="../Login.aspx">Logout</a>
                        </div>
                    </div>
                     
                    <div id="addprofilesfromMaster"  class="team_member">
                    
                     <div id="expanderContentForMaster" style="display: none;">
             <ul>
                        <li><a id="facebook_connect_master">
                            <img src="../Contents/Images/fb_24X24.png" width="16" height="16" alt="" />
                            <span>Facebook</span> </a></li>
                        <li><a id="LinkedInLink_master"  runat="server">
                            <img src="../Contents/Images/linked_25X24.png" width="16" height="16" alt="" />
                            <span>LinkedIn</span> </a></li>
                      <li><a id="TwitterOAuth_master" runat="server" >
                            <img src="../Contents/Images/twt_icon.png" width="16" height="16" alt="" />
                            <span>Twitter</span> </a></li>
                       <%-- <li><a id="googleplus_connect" runat="server">
                            <img src="Contents/Images/google_plus.png" width="16" height="16" alt="" />
                            <span>Google Plus</span> </a></li>--%>
                        <li><a id="googleanalytics_connect_master" runat="server" href="#">
                            <img src="../Contents/Images/google_analytics.png" width="16" height="16" alt="" />
                            <span>Google Analytics</span> </a></li>
                        <li><a id="InstagramConnect_master"  runat="server">
                            <img src="../Contents/Images/instagram_24X24.png" width="16" height="16" alt="" />
                            <span>Instagram</span> </a></li>
                     

                    </ul>
                </div>
                   
                    </div>
                    <div class="profile">
                    </div>
                    <asp:Label ID="username" runat="server" Style="color: White; float: right; font-size: 12px;
                        margin: 5px 13px 0 0;"></asp:Label>
                </div>
                <!--header_wrapper_bottom-->
                <div class="header_wrapper_bottom">
                    <div  class="social_crowdlogo">
                        <a href="/Home.aspx">
                           <img src="../Contents/Images/Social-Crowd1.png"  alt=""/></a></div>
                   <ul class="menu">
                        <li><a id="home" href="../Home.aspx"  title="Go to dashboard"><span
                            class="icon">
                            <img src="../Contents/Images/home.png" width="24" height="24" alt="" /></span> <span
                                class="content">HOME</span> </a></li>
                        <li class="break"></li>
                        <li><a id="message" href="../Message/Message.aspx" class="" title="Go to messages">
                            <span class="icon">
                                <img src="../Contents/Images/msg.png" width="24" height="19" alt="" /></span>
                            <span class="content">MESSAGES</span> </a></li>
                        <li class="break"></li>
                        <li><a id="feeds" href="../Feeds/Feeds.aspx" title="Go to timeline"><span class="icon">
                            <img src="../Contents/Images/feeds.png" width="24" height="24" alt="" /></span>
                            <span class="content">FEEDS</span> </a></li>
                        <li class="break"></li>
                        <li><a id="publishing" href="../Schedule.aspx" class="active"><span class="icon">
                            <img src="../Contents/Images/publish.png" width="24" height="24" alt="" /></span>
                            <span class="content">PUBLISHING</span> </a></li>
                        <li class="break"></li>
                        <li><a id="discovery" href="../Discovery/Discovery.aspx" title="Monitor our brands ,track new competition, find new costumers ">
                            <span class="icon">
                                <img src="../Contents/Images/discovery.png" width="24" height="24" alt="" /></span>
                            <span class="content">DISCOVERY</span> </a></li>
                        <li class="break"></li>
                        <li><a id="reports" href="#" title="View reports"><span class="icon">
                            <img src="../Contents/Images/reports.png" width="24" height="24" alt="" /></span>
                            <span class="content">REPORTS</span> </a></li>
                        <li class="break"></li>
                    </ul>
                    <ul>
                        <li><a class="modal" href="#dialog" name="modal">
                            <img src="../Contents/Images/Search.png" width="16" height="16" /></a></li>
                           
                        <%--   <li><a class="compose_msgbtn" href="#other_dialog" name="compose_msgbtn">Compose Message</a></li>--%>
                    </ul>
                    
                        <a href="#"><div id="composeBtn" class="compose">Compose</div></a>
                    
                </div>
                <!--end header_wrapper_bottom-->
            </div>
        </div>


        <div class="ws_container_page">
 <aside id="actions" class="unselectable" role="complementary">
                        <ul class="msgs_nav" style="margin:83px 0 0;">
                         
                            <li class="accordion single selected">
                                <a href="#">
                                    <span class="nav_icon">
                                        <span class="msg_queue"></span>
                                    </span>
                                    <span class="text"><span class="label">Scheduled</span></span>
                                    
                                </a>
                            </li>
                            <li class="accordion  multi">
                                <a href="../Messages/SentMessages.aspx">
                                    <div class="nav_icon"><span class="msg_sent"></span>
                                    </div>
                                    <div class="text">Sent Message</div>
                                </a>
                            </li>
                        </ul>
    				</aside>




 <div class="shedule_no_data" style="margin-top:85px;">
 <div class="no_data">
                    <h3>
                        Nothing scheduled in the globus group.</h3>
                    
                    <a style="color:Blue;text-decoration:underline;cursor:pointer;"  id="MainContent_HyperLink1"><b>Schedule a new message</b></a>
                </div>
                
                
                
                
                
                
                
                
    <div id="composeBox_scheduler" class="compose_box">
      <span class="close_button b-close"><span id="Span1" onclick="closeonCompose()">X</span></span>
       <div class="newmsd">NEW MESSAGE</div>
       <div class="pht_text_counter">
         <div class="pht_bg">
            <div  class="bgpht"><img id="imageofuser_scheduler" src="../Contents/Images/blank_img.png" alt=""  style="height:50px;"/></div>
            <div class="twitter_icon"><a href="#"><img id="socialIcon_scheduler" src="../Contents/Images/twitter.png" alt="" border="none" width="20" /></a></div>
              <div id="loginContainer_scheduler">
                <div id="loginButton_scheduler"><img src="../Contents/Images/drop_arrow.png" alt="" style=" float:right;" /></div>
                <div style="clear:both"></div>
                <div id="loginBox_scheduler"  style="position: absolute; margin-left: -73px; z-index: 9999;">                
                   <div class="drop_top"></div>
                     <div class="drop_mid">   <img src="../Contents/Images/small_loader.gif" alt="" style="margin-left:90px;"/></div>
                <%--          <div class="twitte_text">TWITTER</div>
                        <div class="teitter">
                           <ul>
                              <li><a href="#"><img src="../Contents/Images/twitter.png" alt="" border="none" width="20" style="float:left;" /> <span style="float:left;margin: 3px 0 0 5px;">Sumitghosh</span></a></li>
                              <li><a href="#"><img src="../Contents/Images/twitter.png" alt="" border="none" width="20" style="float:left;" /> <span  style="float:left;margin: 3px 0 0 5px;">Raj07</span></a></li>
                           </ul>
                        </div>
                        
                         <div class="twitte_text">LINKEDIN</div>
                        <div class="teitter">
                           <ul>
                              <li><a href="#"><img src="../Contents/Images/link.png" alt="" border="none" width="18" style="float:left;" /><span style="float:left;margin: 3px 0 0 5px;"> Shyam SIngh</span></a></li>
                           </ul>
                        </div>
                        
                        <div class="twitte_text">FACEBOOK</div>
                        <div class="teitter">
                           <ul>
                              <li><a href="#"><img src="../Contents/Images/facebook.png" alt="" border="none" width="18" style="float:left;" /><span style="float:left;margin: 3px 0 0 5px;"> Shyam SIngh</span></a></li>
                              <li><a href="#"><img src="../Contents/Images/facebook.png" alt="" border="none" width="18" style="float:left;" /><span style="float:left;margin: 3px 0 0 5px;"> GLobussoft</span></a></li>
                           </ul>
                        </div>
                        
                     </div>--%>
                 
                </div>
            </div>
         </div>
         
         <div class="drop_textare"><textarea id="textareavaluetosendmessage_scheduler"></textarea></div>
         
         <div id="messageCount_scheduler" class="counter_bg">140</div>
         
    </div>
    
    
    <div class="pht_addbtn">
         <div id="addContainer_scheduler">
                <div id="abb_scheduler"><img src="../Contents/Images/AddBtn.png" alt="" /></div>
               <div id="divformultiusers_scheduler"></div>
                <div style="clear:both"></div>
                <div id="ab_scheduler" style="margin-left: -62px; z-index: 999999; position: absolute;">                
                    <div class="drop_top"></div>
                     <div class="drop_mid">  <img src="../Contents/Images/small_loader.gif" style="margin-left:90px;" alt=""/> </div>
                   <%--     <div class="twitte_text">TWITTER</div>
                        <div class="teitter">
                           <ul>
                              <li><a href="#"><img src="../Contents/Images/twitter.png" alt="" border="none" width="20" style="float:left;" /> <span style="float:left;margin: 3px 0 0 5px;">Sumitghosh</span></a></li>
                              <li><a href="#"><img src="../Contents/Images/twitter.png" alt="" border="none" width="20" style="float:left;" /> <span  style="float:left;margin: 3px 0 0 5px;">Raj07</span></a></li>
                           </ul>
                        </div>
                        
                         <div class="twitte_text">LINKEDIN</div>
                        <div class="teitter">
                           <ul>
                              <li><a href="#"><img src="../Contents/Images/link.png" alt="" border="none" width="18" style="float:left;" /><span style="float:left;margin: 3px 0 0 5px;"> Shyam SIngh</span></a></li>
                           </ul>
                        </div>
                        
                        <div class="twitte_text">FACEBOOK</div>
                        <div class="teitter">
                           <ul>
                              <li><a href="#"><img src="../Contents/Images/facebook.png" alt="" border="none" width="18" style="float:left;" /><span style="float:left;margin: 3px 0 0 5px;"> Shyam SIngh</span></a></li>
                              <li><a href="#"><img src="../Contents/Images/facebook.png" alt="" border="none" width="18" style="float:left;" /><span style="float:left;margin: 3px 0 0 5px;"> GLobussoft</span></a></li>
                           </ul>
                        </div>
                        
                     </div>--%>
                  
                </div>
            </div>
    </div>

   
        
    
            <div class="pht_addbtn">
      <%--         <div class="btn btn-success fileinput-button" style="background:url('../Contents/Images/attechphoto.png') transparent; height: 6px;width: 73px !important;margin: 12px 0 0 0; float:left; border:none;">
                   <input id="fileupload" type="file" name="files[]" multiple>
                </div>--%>

                <div class="send_btn"><a id="sendMessageBtn_scheduler" onclick="ScheduleMessage()" href="#"><img src="../Contents/Images/sendbtn.png" alt="" /></a></div>
               <%-- <div style="width:20px; height:20px;"><img src=</div>--%>
                
                 <div class="well">
       
            </div>
          
                
              </div>
             

              <div class="scheduler_bot"></div>
                 <div class="scheduler_calendr_bg">
                    <div id="multicaleder" class="left_vclendar"></div>
                    <div class="right_timedate">
                    <div  class="schedule_time">
                 
             <img id="imgtimepicker" src="Contents/Images/clock.png" alt=""/>
              <input type="text" value="" id="timepickerforScheduler" disabled="disabled" style="width:65px; height:25px; border:1px solid #999;" />
             <div id="chktimepicker" style="display:none;"></div>
                 
                 
                 
                 
                 
                 </div>
                                    <div class="times_half">
                                        <div class="time_schecule">
                                           <%-- <div class="time_content">
                                             
                                            <div class="time_show">
                                               <%-- <div class="woosuitime_entry">
                                                    <div id="sample2">
                                                        <p>
                                                            <input value="" name="s2Time2" id="txttime" class="hasPtTimeSelect">
                                                        </p>
                                                        <p style="display: none;" id="sample2-data">
                                                            </p><div style="font-weight: bold; display: none;">
                                                                Event Data:</div>
                                                            <p>
                                                            </p>
                                                            <p>
                                                            </p>
                                                            <p>
                                                            </p>
                                                            <p>
                                                            </p>
                                                            <p>
                                                            </p>
                                                            <p>
                                                            </p>
                                                            <p>
                                                            </p>
                                                            <p>
                                                            </p>
                                                            <p>
                                                            </p>
                                                            <p>
                                                            </p>
                                                            <p>
                                                            </p>
                                                            <p>
                                                            </p>
                                                            <p>
                                                            </p>
                                                            <p>
                                                            </p>
                                                            <p>
                                                            </p>
                                                            <p>
                                                            </p>
                                                            <p>
                                                            </p>
                                                            <p>
                                                            </p>
                                                        <p></p>
                                                    </div>
                                                </div>--%>
                                          <%--  </div>
                                        </div>--%>
                                        
                                        <div class="scheduled_address">
                                            <div id="adddates_scheduler">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                    
                    </div>
                 </div>
              
       
    </div>
                
                
                
                </div></div>

        </div>

        <div id="composeBox" class="compose_box">
      <span class="close_button b-close"><span id="Span2" onclick="closeonCompose()">X</span></span>
       <div class="newmsd">NEW MESSAGE</div>
       <div class="pht_text_counter">
         <div class="pht_bg">
            <div  class="bgpht"><img id="imageofuser" src="../Contents/Images/normal.jpg" alt=""  style="height:50px;"/></div>
            <div class="twitter_icon"><a href="#"><img id="socialIcon" src="../Contents/Images/twitter.png" alt="" border="none" width="20" /></a></div>
              <div id="loginContainer">
                <div id="loginButton"><img src="../Contents/Images/drop_arrow.png" alt="" /></div>
                <div style="clear:both"></div>
                <div id="loginBox">                
                   <div class="drop_top"></div>
                     <div class="drop_mid">   <img src="../Contents/Images/small_loader.gif" alt="" style="margin-left:90px;"/></div>
                <%--          <div class="twitte_text">TWITTER</div>
                        <div class="teitter">
                           <ul>
                              <li><a href="#"><img src="../Contents/Images/twitter.png" alt="" border="none" width="20" style="float:left;" /> <span style="float:left;margin: 3px 0 0 5px;">Sumitghosh</span></a></li>
                              <li><a href="#"><img src="../Contents/Images/twitter.png" alt="" border="none" width="20" style="float:left;" /> <span  style="float:left;margin: 3px 0 0 5px;">Raj07</span></a></li>
                           </ul>
                        </div>
                        
                         <div class="twitte_text">LINKEDIN</div>
                        <div class="teitter">
                           <ul>
                              <li><a href="#"><img src="../Contents/Images/link.png" alt="" border="none" width="18" style="float:left;" /><span style="float:left;margin: 3px 0 0 5px;"> Shyam SIngh</span></a></li>
                           </ul>
                        </div>
                        
                        <div class="twitte_text">FACEBOOK</div>
                        <div class="teitter">
                           <ul>
                              <li><a href="#"><img src="../Contents/Images/facebook.png" alt="" border="none" width="18" style="float:left;" /><span style="float:left;margin: 3px 0 0 5px;"> Shyam SIngh</span></a></li>
                              <li><a href="#"><img src="../Contents/Images/facebook.png" alt="" border="none" width="18" style="float:left;" /><span style="float:left;margin: 3px 0 0 5px;"> GLobussoft</span></a></li>
                           </ul>
                        </div>
                        
                     </div>--%>
                 
                </div>
            </div>
         </div>
         
         <div class="drop_textare"><textarea id="textareavaluetosendmessage"></textarea></div>
         
         <div id="messageCount" class="counter_bg">140</div>
         
    </div>
    
    
    <div class="pht_addbtn">
         <div id="addContainer">
                <div id="addButton"><img src="../Contents/Images/AddBtn.png" alt="" /></div>
               <div id="divformultiusers"></div>
                <div style="clear:both"></div>
                <div id="addBox">                
                    <div class="drop_top"></div>
                     <div class="drop_mid">  <img src="../Contents/Images/small_loader.gif" style="margin-left:90px;" alt=""/> </div>
                   <%--     <div class="twitte_text">TWITTER</div>
                        <div class="teitter">
                           <ul>
                              <li><a href="#"><img src="../Contents/Images/twitter.png" alt="" border="none" width="20" style="float:left;" /> <span style="float:left;margin: 3px 0 0 5px;">Sumitghosh</span></a></li>
                              <li><a href="#"><img src="../Contents/Images/twitter.png" alt="" border="none" width="20" style="float:left;" /> <span  style="float:left;margin: 3px 0 0 5px;">Raj07</span></a></li>
                           </ul>
                        </div>
                        
                         <div class="twitte_text">LINKEDIN</div>
                        <div class="teitter">
                           <ul>
                              <li><a href="#"><img src="../Contents/Images/link.png" alt="" border="none" width="18" style="float:left;" /><span style="float:left;margin: 3px 0 0 5px;"> Shyam SIngh</span></a></li>
                           </ul>
                        </div>
                        
                        <div class="twitte_text">FACEBOOK</div>
                        <div class="teitter">
                           <ul>
                              <li><a href="#"><img src="../Contents/Images/facebook.png" alt="" border="none" width="18" style="float:left;" /><span style="float:left;margin: 3px 0 0 5px;"> Shyam SIngh</span></a></li>
                              <li><a href="#"><img src="../Contents/Images/facebook.png" alt="" border="none" width="18" style="float:left;" /><span style="float:left;margin: 3px 0 0 5px;"> GLobussoft</span></a></li>
                           </ul>
                        </div>
                        
                     </div>--%>
                  
                </div>
            </div>
    </div>
        
    
            <div class="pht_addbtn">
      <%--         <div class="btn btn-success fileinput-button" style="background:url('../Contents/Images/attechphoto.png') transparent; height: 6px;width: 73px !important;margin: 12px 0 0 0; float:left; border:none;">
                   <input id="fileupload" type="file" name="files[]" multiple>
                </div>--%>

                <div class="send_btn"><a id="sendMessageBtn" onclick="SendMessage()" href="#"><img src="../Contents/Images/sendbtn.png" alt="" /></a></div>
               <%-- <div style="width:20px; height:20px;"><img src=</div>--%>
                
                 <div class="well">
       <%--     <div id="datetimepicker1" class="input-append date">
            <input data-format="dd/MM/yyyy hh:mm:ss" type="text"></input>
            <span class="add-on">
            <i data-time-icon="icon-time" data-date-icon="icon-calendar">
            </i>
            </span>
            </div>--%>
            </div>
            
                
              </div>

       
    </div>

</body>

<script type="text/javascript" language="javascript">
    var datearr = new Array();

    $(document).ready(function () {
        SchedulerCompose();

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


    $("#MainContent_HyperLink1").click(function () {
        var today = new Date();
        debugger;
        $('#multicaleder').multiDatesPicker({
            dateFormat: "yy-mm-dd",
            onSelect: function (dates) {
                debugger;
                var index = $.inArray(dates, datearr);
                if (index < 0) {
                    $('#adddates_scheduler').append('<div id="' + dates + '" class="date_timesqbg"  onclick="dateDivDelete(this.id);"><b>' + dates + '</b> </ div> <div class="closewicon"></div>');
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
        $("#composeBox_scheduler").bPopup({

            fadeSpeed: 'slow', //can be a string ('slow'/'fast') or int
            followSpeed: 1500, //can be a string ('slow'/'fast') or int
            modalColor: 'black',
            modalClose: false,
            opacity: 0.6,
            positionStyle: 'fixed'
        });

    });
    debugger;

    function SchedulerCompose() {
        try {
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
            $("#sendMessageBtn_scheduler").html('<img src="../Contents/Images/325.gif" alt="" />');
            var timeforsch =   $("#timepickerforScheduler").val();

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

    $(function () {
        'use strict';
        // Change this to the location of your server-side upload handler:
        var url = (window.location.hostname === 'blueimp.github.com' ||
                        window.location.hostname === 'blueimp.github.io') ?
                        '//jquery-file-upload.appspot.com/' : 'server/php/';
        $('#fileupload').fileupload({
            url: url,
            dataType: 'json',
            done: function (e, data) {
                $.each(data.result.files, function (index, file) {
                    $('<p/>').text(file.name).appendTo('#files');
                });
            },
            progressall: function (e, data) {
                var progress = parseInt(data.loaded / data.total * 100, 10);
                $('#progress .bar').css(
                        'width',
                        progress + '%'
                    );
            }
        });
    });

    $("#composeBtn").click(function () {

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



</script>
</html>
