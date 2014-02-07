<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true"
    CodeBehind="Group.aspx.cs" Inherits="SocialScoup.Group.Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="../Contents/js/group.js"></script>
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
                            Groups <a class="post" gid="" onclick="userdetails();">Post</a></div>
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
    <div id="popupchk">
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
                                <textarea id="txtcmt"  placeholder="Your comment (viewable only to team members)"></textarea>
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
    <script type="text/javascript">
        $(document).ready(function () {

            $("#home").removeClass('active');
            $("#message").removeClass('active');
            $("#feeds").removeClass('active');
            $("#discovery").removeClass('active');
            $("#publishing").removeClass('active');
            $("#Li1").addClass('active');


//            $('.accordion-toggle').click(function () {
//                $('.accordion-toggle i').addClass("hidden");
//                $(this).children("i").toggleClass("hidden");
//            });
//            $(".feedwrap > ul").mCustomScrollbar({
//                scrollInertia: 150
//            });





//            $(".fbgroup").click(function () {
//                alert("abhay");
            //            });




        });

        function userdetails() {

            var gid = $('.post').attr("gid");
            debugger;
            if (gid === "" || gid === null) {
                alert('Please Select one group for reply.');
            }
            else {
                $('#popupchk').bPopup();
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
