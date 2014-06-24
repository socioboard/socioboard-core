<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true"
    CodeBehind="Messages.aspx.cs" Inherits="letTalkNew.Messages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script src="../Contents/js/jquery.session.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('.home').removeClass('active');
            $('.message').addClass('active');
            $('.download').removeClass('active');
            $('.group').removeClass('active');
            $('.reports').removeClass('active');

            debugger;
            //        $('#msg_task').click(function () {
            //            $('#msg_task_open').bPopup();
            //        });

        });
    </script>
    <!--container_right-->
    <div class="container_right" id="inbox_msgs">
        <!--graph-->
        <nav>
            <ul id="tabs">
                    <li><a class="current" onclick="BindProfilesInMessageTab()">Smart Inbox</a></li>
                    <li><a onclick="BindTasks()">My Task</a></li>
                    <li><a onclick="BindSentMessages()">Sent Message</a></li>
                    <li><a onclick="BindArchieveMessage()">Archive Message</a></li>
            </ul>
            <span id="indicator"></span> 
        </nav>
        <div id="content">
            <section  >
                <ul id="messageProfiles" class="sub_name">
                  <li><img style="width:50%;height:10px;" src="../Contents/img/292.gif" alt="" /><a href="#"></a></li>
                    <%--<li><img src="../Contents/img/fbicon_new.png"><a href="#">Name-1</a></li>
                    <li><img src="../Contents/img/fbicon_new.png"><a href="#">Name-2</a></li>
                    <li><img src="../Contents/img/fbicon_new.png"><a href="#">Name-3</a></li>--%>
                </ul>
    	        <div id="message">
                	      <%--  <div id="message_list">
                    	        <span id="message_list_img">
                        	        <img src="../Contents/img/pic_1.jpg">
                                    <a href="#" id="message_list_img_soci"><img src="../Contents/img/twitter.jpg" ></a>
                                </span>
                                <ul id="message_list_right">
                        	        <span>5:10:36 AM  8/9/2013</span>
                                    <a href="#">Lorem Ipsum is simply dummy text</a>
                                    <h2>Dani</h2>
                                    <div id="message_list_right_comment">
                            	        <a href="#"><img src="../Contents/img/list_icon1.png"></a>
                                        <a href="downloads.html"><img src="../Contents/img/list_icon2.png"></a>
                                        <a href="#"><img src="../Contents/img/list_icon3.png"></a>                            
                                    </div>                            
                                </ul>
                            </div>--%>
                <div class="loader"><img alt="" src="../Contents/img/482.gif"/></div>
                           <%-- <div id="message_list">
                    	        <span id="message_list_img">
                        	        <img src="../Contents/img/pic_1.jpg">
                                    <a href="#" id="message_list_img_soci"><img src="../Contents/img/twitter.jpg" ></a>
                                </span>
                                <ul id="message_list_right">
                        	        <span>5:10:36 AM  8/9/2013</span>
                                    <a href="#">Lorem Ipsum is simply dummy text</a>
                                    <h2>Dani</h2>
                                    <div id="message_list_right_comment">
                            	        <a href="#"><img src="../Contents/img/list_icon1.png"></a>
                                        <a href="downloads.html"><img src="../Contents/img/list_icon2.png"></a>
                                        <a href="#"><img src="../Contents/img/list_icon3.png"></a>                            
                                    </div>
                            
                                </ul>
                            </div>
                    
                            <div id="message_list">
                    	        <span id="message_list_img">
                        	        <img src="../Contents/images/pic_1.jpg">
                                    <a href="#" id="message_list_img_soci"><img src="../Contents/img/twitter.jpg" ></a>
                                </span>
                                <ul id="message_list_right">
                        	        <span>5:10:36 AM  8/9/2013</span>
                                    <a href="#">Lorem Ipsum is simply dummy text</a>
                                    <h2>Dani</h2>
                                    <div id="message_list_right_comment">
                            	        <a href="#"><img src="../Contents/img/list_icon1.png"></a>
                                        <a href="downloads.html"><img src="../Contents/img/list_icon2.png"></a>
                                        <a href="#"><img src="../Contents/img/list_icon3.png"></a>                            
                                    </div>
                            
                                </ul>
                            </div>
                    
                            <div id="message_list">
                    	        <span id="message_list_img">
                        	        <img src="../Contents/images/pic_1.jpg">
                                    <a href="#" id="message_list_img_soci"><img src="../Contents/img/twitter.jpg" ></a>
                                </span>
                                <ul id="message_list_right">
                        	        <span>5:10:36 AM  8/9/2013</span>
                                    <a href="#">Lorem Ipsum is simply dummy text</a>
                                    <h2>Dani</h2>
                                    <div id="message_list_right_comment">
                            	        <a href="#"><img src="../Contents/img/list_icon1.png"></a>
                                        <a href="downloads.html"><img src="../Contents/img/list_icon2.png"></a>
                                        <a href="#"><img src="../Contents/img/list_icon3.png"></a>                            
                                    </div>
                            
                                </ul>
                            </div>
                    
                            <div id="message_list">
                    	        <span id="message_list_img">
                        	        <img src="../Contents/img/pic_1.jpg">
                                    <a href="#" id="message_list_img_soci"><img src="../Contents/img/twitter.jpg" ></a>
                                </span>
                                <ul id="message_list_right">
                        	        <span>5:10:36 AM  8/9/2013</span>
                                    <a href="#">Lorem Ipsum is simply dummy text</a>
                                    <h2>Dani</h2>
                                    <div id="message_list_right_comment">
                            	        <a href="#"><img src="../Contents/img/list_icon1.png"></a>
                                        <a href="downloads.html"><img src="../Contents/img/list_icon2.png"></a>
                                        <a href="#"><img src="../Contents/img/list_icon3.png"></a>                            
                                    </div>
                            
                                </ul>
                            </div>
                    
                            <div id="message_list">
                    	        <span id="message_list_img">
                        	        <img src="../Contents/images/pic_1.jpg">
                                    <a href="#" id="message_list_img_soci"><img src="../Contents/img/twitter.jpg" ></a>
                                </span>
                                <ul id="message_list_right">
                        	        <span>5:10:36 AM  8/9/2013</span>
                                    <a href="#">Lorem Ipsum is simply dummy text</a>
                                    <h2>Dani</h2>
                                    <div id="message_list_right_comment">
                            	        <a href="#"><img src="../Contents/img/list_icon1.png"></a>
                                        <a href="downloads.html"><img src="../Contents/img/list_icon2.png"></a>
                                        <a href="#"><img src="../Contents/img/list_icon3.png"></a>                            
                                    </div>
                            
                                </ul>
                            </div>--%>
                        </div>
            </section>
            <section  id="mytask">
    	     <div id="bindtasks">
              <div class="loader"><img alt="" src="../Contents/img/482.gif"/></div>
             </div>
            </section>
            <section  id="sendmessage">
    	        <div id="bindsent"> <div class="loader"><img alt="" src="../Contents/img/482.gif"/></div></div>
            </section>
            <section id="archive">
    	        <div id="bindarchive"> <div class="loader"><img alt="" src="../Contents/img/482.gif"/></div></div>
            </section>
        </div>
    </div>
        <!--graph-->
    <!--end container_right-->
    <!-- graph chart script here-->
    <!--msg_task_popup-->
    <div id="msg_task_open" style="left: 143px; position: absolute; top: 20px; z-index: 9999;
        opacity: 1;">
        <span class="close_button b-close"><span id="close">X</span></span>
        <div id="content_msg" role="main" style="padding: 0;">
            <section id="inbox" class="threefourth messages msg_view" style="margin: 0;">
                                  <div id="inbox_messages" class="messages_list taskable">
                                <div style="width:60px;height:60px;float:left">
                                    <img id="formprofileurl_0" onclick="detailsprofile(this.alt);" src="https://lh3.googleusercontent.com/-XdUIqdMkCWA/AAAAAAAAAAI/AAAAAAAAAAA/4252rscbv5M/photo.jpg?sz=50" alt="Praveen Kumar" title="Praveen Kumar" height="48" width="48"  />
                                    <a href="#" class="userurlpic" title=""><img src="../Contents/img/google_plus.png" alt="" height="16" width="16" /></a>
                                 </div>
                                 <div id="msgdescription_0" style="width:500px;height:60px;float:left">
                                    <p>hello to test..</p>
                                    <div class="message-list-info">
                                        <span>
                                            <a href="#" id="rowname_0" onclick="getGooglePlusProfiles('110013478146689373710');">Praveen Kumar</a>
                                            <div id="createdtime_0">6/11/2013 4:52:49 AM</div>
                                         </span>
                                         <div class="scl">
                                            <a id="createtasktwt_0" href="#" onclick="createtask(this.id);"><img src="" alt="" border="none" height="17" width="14"></a>
                                            <a href="#"><img src="../Contents/img/admin/goto.png" alt="" height="12" width="12" alt="" /></a>
                                            <a id="savearchive_0" href="#" onclick="savearchivemsg(0,'googleplus','z12fdhz40z3auxphz04cercqmyigtl0qfs40k','110013478146689373710');">
                                                <img src="../Contents/img/archive.png" alt="" border="none" height="17" width="14" alt="" />
                                             </a>
                                          </div>
                                       </div>
                                    </div>
                            </div>
                                  <div class="task_leave_comment">
	                            <div class="sub_small">Leave a Comment</div>
                            </div>
                                  <div class="assign_task_to">
                            	<img src="../Contents/img/blank_img.png" alt="">
                                <textarea id="txttaskcomment" rows="" cols="" name="" placeholder="Your comment (viewable only to team members)"></textarea>
                            </div>
                                  <div class="task_ws_tm_button_div">
                            	<input id="save_task" name="" value="SAVE" onclick="savetask();" type="button">
                              
                            </div>
                              </section>
        </div>
        <div class="ws_msg_right" style="margin-right: 25px;">
            <div class="quarter">
                <div class="sub_small">
                    Assign Task To</div>
            </div>
            <div id="tasksteam" class="task_user_assign">
                <ul>
                    <li><a>
                        <img src="../Contents/img/blank_img.png" alt="" />
                        <span class="name">Praveen Kumar</span> <span>
                            <input id="customerid_fd83ca17-41f5-472a-8efe-7a975d83ed9b" name="team_members" value="customerid_fd83ca17-41f5-472a-8efe-7a975d83ed9b"
                                type="radio"></span> </a></li>
                </ul>
            </div>
        </div>
    </div>
    <!--end msg_task_popup-->
    <!--reply-->
    <div id="replysection" class="box_popup">
        <span class="close_button b-close"><span id="Span3">X</span></span>
        <section style="margin: 0;" class="threefourth messages msg_view" id="inbox">
                            <div class="messages taskable" id="replyMessages">
                                <div style="width:500px;height:60px;float:left" id="Div1">
                                    <p>"asdf" on The Times of India's photo.</p>
                                    <div class="message-list-info">
                                        <span>
                                            <a onclick="getFacebookProfiles(100004496770422);" id="A1" href="#">Prab Kumar</a>
                                            <div id="Div2">9/19/2013 3:07:55 AM</div>
                                        </span>
                                        <div class="scl">
                                            <a onclick="createtask(this.id);" href="#" id="A2"><img width="14" height="17" border="none" alt="" src="../Contents/img/pin.png" /></a>
                                            <a href="#"><img width="12" height="12" alt="" src="../Contents/img/admin/goto.png" /></a>
                                            <a href="#" id="A3"><img width="14" height="17" border="none" alt="" src="../Contents/img/archive.png"></a>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                            <div class="task_leave_comment">
	                            <div class="sub_small">Leave a Comment</div>
                            </div>
                            <div class="assign_task_to">
                            	<img alt="" src="../Contents/img/blank_img.png">
                                <textarea placeholder="Your comment (viewable only to team members)" name="" cols="" rows="" id="Textarea1"></textarea>
                            </div>
                            <div class="task_ws_tm_button_div">
                            	<input type="button" onclick="twittercomments();" value="SAVE" name="" id="Button1" />                              
                            </div>
                     </section>
    </div>
    
    <!--end reply-->



    <script src="../Contents/js/jquery.easy-pie-chart.js" type="text/javascript"></script>
     
    <script type="text/javascript">
        $(function () {
            // fb graph chart for male and female
            $('.femalefbchart').easyPieChart({
                //your configuration goes here
                barColor: '#ff569a'
            });

            $('.malefbchart').easyPieChart({
                //your configuration goes here
                barColor: '#44619d'
            });

            // end fb graph chart for male and female

            //twitter graph chart for male and female

            $('.twitermalechart').easyPieChart({
                //your configuration goes here
                barColor: '#14b9d6'
            });

            $('.twitterfemalechart').easyPieChart({
                //your configuration goes here
                barColor: '#ff569a'
            });

            // end twitter graph chart for male and female

        });

        $(document).ready(function () {
            debugger;
           //    BindMessages();
            BindProfilesInMessageTab();
        });
    </script>
    <%--     </div>--%>
    <!--end message_section-->
</asp:Content>
