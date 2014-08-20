<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true"
    CodeBehind="Group.aspx.cs" Inherits="SocialScoup.Group.Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="../Contents/js/group.js"></script>


<style type="text/css">
 p.commeent_box > .put_comments
        {
            display: none;
        }
        p.commeent_box > .put_commentss
        {
            display: block;
        }
        
      .ok{ display: none;}
      .cancel{display: none;}
      .ok_display
       {
            display: block;
        }
      .cancel_display
       {
            display: block;
        }
</style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="mainwrapper-message" class="container">
        <div class="sidebar feeds">
            <div class="sidebar-inner">
                <!--Here left side data bind start-->
                <div id="accordion2" class="accordion" runat="server">
                    <%-- <div class="accordion-group">
                        <div class="accordion-heading">
                            <a href="#collapseOne" data-parent="#accordion2" data-toggle="collapse" class="accordion-toggle">
                                <img class="fesim" src="img/admin/1.png" alt="" />FACEBOOK <i class="icon-sort-down pull-right hidden">
                                </i></a>
                        </div>
                        <div class="accordion-body collapse" id="collapseOne">
                            <div class="accordion-inner">
                                <ul>
                                    <li><a href="#">Link 1</a> </li>
                                </ul>
                            </div>
                        </div>
                    </div>--%>
                    <%--<div class="accordion-group">
                        <div class="accordion-heading">
                            <a href="#collapseTwo" data-parent="#accordion2" data-toggle="collapse" class="accordion-toggle">
                                <img class="fesim" src="img/admin/2.png" alt="" />TWITTER <i class="icon-sort-down pull-right">
                                </i></a>
                        </div>
                        <div class="accordion-body collapse in" id="collapseTwo">
                            <div class="accordion-inner">
                                <ul>
                                    <li><a href="#">Select User</a> </li>
                                    <li><a href="#">Profile Connected</a> </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="accordion-group">
                        <div class="accordion-heading">
                            <a href="#collapseThree" data-parent="#accordion2" data-toggle="collapse" class="accordion-toggle">
                                <img class="fesim" src="img/admin/5.png" alt="" />LINKEDIN <i class="icon-sort-down pull-right hidden">
                                </i></a>
                        </div>
                        <div class="accordion-body collapse" id="collapseThree">
                            <div class="accordion-inner">
                                <ul>
                                    <li><a href="#">Link 1</a> </li>
                                    <li><a href="#">Link 2</a> </li>
                                    <li><a href="#">Link 3</a> </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="accordion-group">
                        <div class="accordion-heading">
                            <a href="#collapseFour" data-parent="#accordion2" data-toggle="collapse" class="accordion-toggle">
                                <img class="fesim" src="img/admin/4.png" alt="" />INSTAGRAM <i class="icon-sort-down pull-right hidden">
                                </i></a>
                        </div>
                        <div class="accordion-body collapse" id="collapseFour">
                            <div class="accordion-inner">
                                <ul>
                                    <li><a href="#">Link 1</a> </li>
                                    <li><a href="#">Link 2</a> </li>
                                    <li><a href="#">Link 3</a> </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="accordion-group">
                        <div class="accordion-heading">
                            <a href="#collapseFive" data-parent="#accordion2" data-toggle="collapse" class="accordion-toggle">
                                <img class="fesim" src="img/admin/3.png" alt="" />GOOGLE + <i class="icon-sort-down pull-right hidden">
                                </i></a>
                        </div>
                        <div class="accordion-body collapse" id="collapseFive">
                            <div class="accordion-inner">
                                <ul>
                                    <li><a href="#">Link 1</a> </li>
                                    <li><a href="#">Link 2</a> </li>
                                    <li><a href="#">Link 3</a> </li>
                                </ul>
                            </div>
                        </div>
                    </div>--%>
                </div>
                <!--Here left side data bind End-->
            </div>
        </div>
        <div class="contentcontainer-feeds">
            <div class="group_content">
                <div class="row-fluid">
                    <div class="span11">
                        <div class="alert_suite_title">
                            <p style="float: left;">Groups</p>
                            <a style="color:#FFFFFF;" onclick="postmessage();"> Post On Selected Group</a><a class="post" gid="" onclick="userdetails12();">Post</a>
                        </div>
                        <!--repeated div-->
                        <div id="gcontent" class="gcontent" runat="server">
                       <%-- <div class="storyContent">
                            <a class="actorPhoto">
                                <img alt="" src="https://fbcdn-profile-a.akamaihd.net/hprofile-ak-prn2/211863_569212492_1347003669_q.jpg" />
                            </a>
                            <div class="storyInnerContent">
                                <div class="actordescription">
                                    <a class="passiveName">Tommy Skaue </a>updated the description.
                                </div>
                                <div class="messagebody">
                                    Facebook group for all you Asp.Net developers out there who find MVC (Model View
                                    Controller) interesting. My intention for this group is to share interesting links
                                    to blogs, articles or demonstration-code related to Asp.Net MVC. Users might offer
                                    answers and hints if you have certain questions or issues, but you may want to consider
                                    visiting the forum found here <a href="#">http://www.asp.net/mvc</a>
                                </div>
                            </div>
                        </div>
                        <div class="storyContent">
                            <a class="actorPhoto">
                                <img alt="" src="https://fbcdn-profile-a.akamaihd.net/hprofile-ak-prn2/211863_569212492_1347003669_q.jpg" />
                            </a>
                            <div class="storyInnerContent">
                                <div class="actordescription">
                                    <a class="passiveName">Tommy Skaue </a>updated the description.
                                </div>
                                <div class="messagebody">
                                    Facebook group for all you Asp.Net developers out there who find MVC (Model View
                                    Controller) interesting. My intention for this group is to share interesting links
                                    to blogs, articles or demonstration-code related to Asp.Net MVC. Users might offer
                                    answers and hints if you have certain questions or issues, but you may want to consider
                                    visiting the forum found here <a href="#">http://www.asp.net/mvc</a>
                                </div>
                            </div>
                        </div>--%>
                        </div>
                        <!--end repeated div-->
                    </div>
                </div>
            </div>
        </div>

    </div>


    <div id="postmessagepopup" class="compose_box" style="left: 285.5px; position: absolute; top: 20px; z-index: 9999; opacity: 1; display: none;">
        <span class="close_button b-close"><span id="Span3">X</span></span>
        <div style="padding: 0;" role="main" id="Div2">
            <section style="margin: 0;" class="threefourth messages" id="Section1">
                            
                            <div class="group_leave_comment">
                             <div class="sub_small">Post On Selected Group</div>
                            </div>
                            <%--<div class="assign_taskpop">
                             <img alt="" src="../Contents/img/blank_img.png">
                                <textarea style="width:482px;"placeholder="Write your message here" name="" cols="" rows="" id="txtmessage"></textarea>
                            </div>--%>
                            <div class="assign_task_to">
                             <img src="../Contents/img/blank_img.png" alt="" />
                               <div id="Div1">
                                <textarea  id="txtTitle"   placeholder="Your enter Title here"></textarea></div>
                               <div id="Div3"><textarea  id="txtmessage"  placeholder="Your comment (viewable only to team members)"></textarea></div>
                            </div>
                
                 <div class="form-group">
                     

             <label class="control-label span2">Date:</label><input class="form-control span4" id="datepicker" placeholder="Date" name="date" type="text">
         </div>
         <div class="form-group">
          <label class="control-label span2">Time:</label><input class="form-control span4" placeholder="Time" name="time" type="text" id="timepicker">
          <img id="imgtimepicker" alt="" src="/Contents/img/clock.png" />
         </div>
                 <div class="form-group">
          <label class="control-label span2">Interval Time:</label><select style="width:90px;" class="form-control span4" id="ddlIntervalTime">
              <option>1</option>
              <option>2</option>
              <option>5</option>
              <option>10</option>
                                                                   </select>
         </div>

                            <div class="task_ws_tm_button_div" style="width:200px;">
                             <input type="button" onclick="Sendgroupmessage();" value="Post" name="" class="btn btnspan btnclr" id="Button1" />
                              
                            </div>
                                     <div class="filebutton">
                    <input id="fileuploadImage1" type="file" name="" onchange="showimage1()" accept="image/*" />
                </div>
                 <div id="showBlock" class="fileupload_data" style="background-color: #222222; border: 1px solid #000000;
                border-radius: 5px; color: #FFFFFF; display: block; margin-top:3px; padding: 5px;" onclick="deleteimage1()">
                     <span style="float:right; margin-left:5px" ></span>
                <label style="float:right;font-size:small">Remove Image x </label></div>
                        </section>
        </div>
    </div>





    <div id="popupchk12">
        <span class="close_button b-close"><span id="close">X</span></span>
        <div id="content_msg" role="main" style="padding: 0;">
            <section id="inbox" class="threefourth messages msg_view" style="margin: 0;">
                            <div id="inbox_messages" class="messages taskable">
                            
                            </div>
                            <div class="task_leave_comment">
                             <div class="sub_small">Leave a post in this Group</div>
                            </div>
                            <div class="assign_task_to">
                             <img src="../Contents/img/blank_img.png" alt="" />
                               <div id="asd">
                                <textarea  id="txttitle"   placeholder="Your enter Title here"></textarea></div>
                               <div id="sdf"><textarea  id="txtcmt"  placeholder="Your comment (viewable only to team members)"></textarea></div>
                            </div>
                            <div class="task_ws_tm_button_div">
                             <input id="save_task" type="button" name="" id="close" value="Post"  onclick="postFBGroupFeeds();"/>
                              
                            </div>
                        </section>
        </div>
        <div class="ws_msg_right" style="margin-right: 25px;">
            <div class="quarter">
                <%--<div class="sub_small">
                    Assign Task To</div>--%>
            </div>
            <div id="tasksteam" class="task_user_assign">
            </div>
        </div>
    </div>




     <!-- Time Picker Js and Css-->
    <link href="../Contents/js/jquery-timepicker.css" rel="stylesheet" />
    <script  src="../Contents/js/jquery-timepicker.js"></script>
    
    <!-- End Section-->

    <!--DatePicker-->
    <script src="../Contents/js/bootstrap-datepicker.js"></script>
    <link href="../Contents/js/bootstrap-datepicker.css" rel="stylesheet" />
    <!---->




    <script type="text/javascript">
        $(document).ready(function () {

            $('.grpli').click(function () {

                $(".grpli").each(function () {
                    $(this).removeClass('grpliselected');
                });
                 $(this).addClass('grpliselected');
            });




            $("#home").removeClass('active');
            $("#message").removeClass('active');
            $("#feeds").removeClass('active');
            $("#discovery").removeClass('active');
            $("#publishing").removeClass('active');

            $("#groups").addClass('active');
            $("#Li1").addClass('active');


            try {

                $('.accordion-toggle').click(function () {
                    $('.accordion-toggle i').addClass("hidden");
                    $(this).children("i").toggleClass("hidden");
                    //$(".accordion-toggle .collapsed").removeClass("hidden");
                });
            } catch (e) {

            }





            //            $(".fbgroup").click(function () {
            //                alert("abhay");
            //            });


            //**********Time Picker ***********
            $("#timepicker").timepicker();
            $("#imgtimepicker").on('click', function () {
                debugger;
                $("#timepicker").timepicker("show");
            });

            //**********************************

            //************Date Picker **********
            $('#datepicker').datepicker({
                'format': 'yyyy-m-d',
                'autoclose': true
            });

        });

        function userdetails12() {

            var gid = $('.post').attr("gid");
            debugger;
            if (gid === "" || gid === null) {
                alert('Please add group first');
            }
            else {

                if (gid.indexOf("lin_") == -1) {
                    $('.assign_task_to > #asd').css({display:'none'});
                    $('#popupchk12').bPopup();

                }
                else {

                    $('#popupchk12').bPopup();
                    $('.assign_task_to > #asd').css({ display: 'block' });
                   


                }
            }
        }
    </script>
    <%-- <script type="text/javascript>
		    $(document).ready(function () {
		        $('.accordion-toggle').click(function () {
		            $('.accordion-toggle i').addClass("hidden");
		            $(this).children("i").toggleClass("hidden");
		        });
		        $(".feedwrap > ul").mCustomScrollbar({
		            scrollInertia: 150
		        });
		    });			
</script>--%>
</asp:Content>
