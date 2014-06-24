<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="letTalkNew.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Contents/css/smartpaginator.css" rel="stylesheet" type="text/css" />
    <script src="Contents/js/smartpaginator.js" type="text/javascript"></script>
    <script src="Contents/js/jquery.knob.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $(".knob").knob();

            // Example of infinite knob, iPod click wheel
            var val, up = 0, down = 0, i = 0
                    , $idir = $("div.idir")
                    , $ival = $("div.ival")
                    , incr = function () { i++; $idir.show().html("+").fadeOut(); $ival.html(i); }
                    , decr = function () { i--; $idir.show().html("-").fadeOut(); $ival.html(i); };
            $("input.infinite").knob(
                                    {
                                        'min': 0
                                    , 'max': 20
                                    , 'stopper': true
                                    , 'change': function (v) {
                                        if (val > v) {
                                            if (up) {
                                                decr();
                                                up = 0;
                                            } else { up = 1; down = 0; }
                                        } else {
                                            if (down) {
                                                incr();
                                                down = 0;
                                            } else { down = 1; up = 0; }
                                        }
                                        val = v;
                                    }
                                    }
                                    );


            $('.home').addClass('active');
            $('.message').removeClass('active');
            $('.download').removeClass('active');
            $('.group').removeClass('active');
            $('.reports').removeClass('active');

        });
    


    </script>
    <!--graph-->
    <div class="graph">
        <!--social_graph facebook-->
        <div class="social_graph">
            <ul>
                <li class="social_conection fb_page_frnd_active">
                    <div class="active_title">
                        <span class="face">
                            <img src="../Contents/img/fbicon_new.png">Facebook Page</span> <span class="friends">
                                Friend Active</span>
                    </div>
                </li>
                <li>
                    <div class="f_left_width">
                        <span>
                            <abbr>
                                <h2>
                                    Male Friends</h2>
                                <img src="../Contents/img/male_img.png">
                            </abbr>
                            <aside>
                                	<b><%= male %>%</b>
                                    <div class="progress-bar orange shine">
                                        <span style="width:<%= male %>%"></span>
                                    </div>
                                </aside>
                        </span><span>
                            <abbr>
                                <h2>
                                    Female Friends</h2>
                                <img src="../Contents/img/female_img.png">
                            </abbr>
                            <aside>
                                	<b><%=female%>%</b>
                                     <div class="progress-bar orange shine">
                                        <span style="width:<%=female%>%"></span>
                                    </div>
                                </aside>
                        </span>
                    </div>
                    <div class="f_right_width">
                        <h2>
                            Total Friends</h2>
                        <div style="float: left; width: 110px; background-color: #fff; color: #FFF; padding: 20px">
                            <input class="knob home_chart" data-width="100" data-displayprevious="true" data-fgcolor="#3b5998"
                                data-skin="tron" data-thickness=".2" value="<%=totfbfriends %>">
                        </div>
                    </div>
                </li>
                <li>
                    <div class="social_activity_links">
                        <ul>
                            <li>
                                <a class="tabs" href="Reports/GroupStats.aspx">
                                    <span class="scl_activity_links chart"></span>
                                    <span class="tabs_name">CHART</span>
                                </a>
                            </li>
                            <li>
                                <a class="tabs" href="Publishing.aspx">
                                    <span class="scl_activity_links tasks"></span>
                                    <span class="tabs_name">TASKS</span>
                                </a>
                            </li>
                            <li>
                                <a class="tabs" href="Event/Events.aspx">
                                    <span class="scl_activity_links events"></span>
                                    <span class="tabs_name">EVENTS</span>
                                </a>
                            </li>
                            <li>
                                <a class="tabs" href="Update-Events.aspx?type=Updates">
                                    <span class="scl_activity_links updates"></span>
                                    <span class="tabs_name">UPDATES</span>
                                </a>
                            </li>
                            <li>
                                <a class="tabs">
                                    <span class="scl_activity_links contacts"></span>
                                    <span class="tabs_name">CONTACTS</span>
                                </a>
                            </li>
                        </ul>
                    </div>
                </li>
            </ul>
        </div>
        <!--end social_graph facebook-->
        <!--social_graph twitter-->
        <div class="social_graph">
            <ul>
                <li class="social_conection fb_page_frnd_active">
                    <div class="active_title">
                        <span class="twitter">
                            <img src="../Contents/img/twittericon_new.png">Twitter Page</span> <span class="friends">
                                Friend Active</span>
                    </div>
                </li>
                <li>
                    <div class="f_left_width">
                        <span>
                            <abbr>
                                <h2>
                                    Male Friends</h2>
                                <img src="../Contents/img/male_img.png">
                            </abbr>
                            <aside>
                                	<b><%=twtmale%>%</b>
                                    <div class="progress-bar orange shine">
                                        <span style="width:<%=twtmale%>%"></span>
                                    </div>
                                </aside>
                        </span><span>
                            <abbr>
                                <h2>
                                    Female Friends</h2>
                                <img src="../Contents/img/female_img.png">
                            </abbr>
                            <aside>
                                	<b><%=twtfemale%>%</b>
                                    <div class="progress-bar orange shine">
                                        <span style="width:<%=twtfemale%>%"></span>
                                    </div>
                                </aside>
                        </span>
                    </div>
                    <div class="f_right_width">
                        <h2>Total Friends</h2>
                        <div style="float: left; width: 110px; background-color: #fff; color: #FFF; padding: 20px">
                            <input class="knob home_chart" data-width="100" data-displayprevious="true" data-fgcolor="#3b5998" data-skin="tron" data-thickness=".2" value="<%=tottwtfriends %>">
                        </div>
                    </div>
                </li>
                <li>
                    <div class="social_activity_links">
                        <ul>
                            <li>
                                <a class="tabs" href="Reports/GroupStats.aspx">
                                    <span class="scl_activity_links chart"></span>
                                    <span class="tabs_name">CHART</span>
                                </a>
                            </li>
                            <li>
                                <a class="tabs" href="Publishing.aspx">
                                    <span class="scl_activity_links tasks"></span>
                                    <span class="tabs_name">TASKS</span>
                                </a>
                            </li>
                            <li>
                                <a class="tabs" href="Event/Events.aspx">
                                    <span class="scl_activity_links events"></span>
                                    <span class="tabs_name">EVENTS</span>
                                </a>
                            </li>
                            <li>
                                <a class="tabs" href="Update-Events.aspx?type=Updates">
                                    <span class="scl_activity_links updates"></span>
                                    <span class="tabs_name">UPDATES</span>
                                </a>
                            </li>
                            <li>
                                <a class="tabs">
                                    <span class="scl_activity_links contacts"></span>
                                    <span class="tabs_name">CONTACTS</span>
                                </a>
                            </li>
                        </ul>
                    </div>
                </li>
            </ul>
        </div>
        <!--end social_graph twitter-->
    </div>
    <!--end graph-->
    <!--end graph-->
    <!--profile_div-->
    <!--end profile_div-->
    <nav>
              <ul id="tabs">
                <li onclick="getFacebookUsersForHome()"><img src="../Contents/img/facebook.png" alt="" /><a class="current" href="#" style="padding:4px 0px 0px 30px;">Facebook</a></li>
                <li onclick="getTwitterUsersForHome()"><img src="../Contents/img/twitter.png" alt="" /><a href="#" style="padding:4px 0px 0px 30px;">Twitter</a></li>
                <li onclick="getLinkedinUsersForHome()"><img src="../Contents/img/linked_25X24.png" alt="" /><a href="#" style="padding:4px 0px 0px 30px;">Linkedin</a></li>
                <li onclick="getGplusUsersForHome()"><img src="../Contents/img/google_plus.png" alt="" /><a href="#" style="padding:4px 0px 0px 30px;">google plus</a></li>
              </ul>
              <span id="indicator"></span> </nav>
    <div id="content" style="width: 100%;">
        <section>
                    <ul id="fbUserMid" class="sub_name">
                    	<li><img style="width:50%;height:10px;" src="../Contents/img/292.gif" alt="" /><a href="#"></a></li><%--
                        <li><img src="../Contents/img/fbicon_new.png" alt="" /><a href="#">Name-2</a></li>
                        <li><img src="../Contents/img/fbicon_new.png" alt="" /><a href="#">Name-3</a></li>--%>
                    </ul>
            		<div class="home_social_width">
                    	<span><img id="userimg_fb" src="../Contents/img/blankLarge.png"></span>
                        <ul>
                        	<li><b id="fans_fb">2</b>Fans</li>
                            <li><b id="avgposts_fb">0.54</b>Avg. Post Per Day</li>
                        </ul>
                    </div>
                    <div class="home_social_width_1">
                    	<div id="recentmsgs_fb">
                        	<h3>Recent messages</h3>
                      
                       <div class="loader"><img alt="" src="../Contents/img/482.gif"></div>
                           
                        </div>
                         
                    </div>
                    <ul id="fb_pagination">
                            	<%--<li><a href="#">prv</a></li>
                            	<li><a href="#" class="active">1</a></li>
                                <li><a href="#">2</a></li>
                                <li><a href="#">3</a></li>
                                <li><a href="#">4</a></li>
                                <li><a href="#">5</a></li>
                                <li><a href="#">Next</a></li>--%>
                            </ul>
          	  </section>
        <section>
                    <ul id="twtUserMid" class="sub_name">
                    <li><img style="width:50%;height:10px;" src="../Contents/img/292.gif" alt="" /><a href="#"></a></li>
                    <%--	<li><img src="../Contents/img/twittericon_new.png"><a href="#">Name-1</a></li>
                        <li><img src="../Contents/img/twittericon_new.png"><a href="#">Name-2</a></li>
                        <li><img src="../Contents/img/twittericon_new.png"><a href="#">Name-3</a></li>--%>
                    </ul>
            		<div class="home_social_width">
                    	<span><img id="userimg_twt" src="../Contents/img/blankLarge.png"></span>
                        <ul>
                        	<li><b id="fans_twt">2</b>Fans</li>
                            <li><b id="avgposts_twt">0.54</b>Avg. Post Per Day</li>
                        </ul>
                    </div>
                    <div class="home_social_width_1">
                    	<div id="recentmsgs_twt">
                        	<h3>Recent messages</h3>
                           <div class="loader"><img alt="" src="../Contents/img/482.gif"></div>
                        </div>
                       <%-- <ul id="pagenation">
                            	<li><a href="#">prv</a></li>
                            	<li><a href="#" class="active">1</a></li>
                                <li><a href="#">2</a></li>
                                <li><a href="#">3</a></li>
                                <li><a href="#">4</a></li>
                                <li><a href="#">5</a></li>
                                <li><a href="#">Next</a></li>
                            </ul>--%>
                    </div>
          	  </section>
        <section>
                    <ul id="linUserMid" class="sub_name">
<li><img style="width:50%;height:10px;" src="../Contents/img/292.gif" alt="" /><a href="#"></a></li>
</ul>
            		<div class="home_social_width">
                    	<span><img src="../Contents/img/blankLarge.png"></span>
                        <ul>
                        	<li><b>2</b>Fans</li>
                            <li><b>0.54</b>Avg. Post Per Day</li>
                        </ul>
                    </div>
                    <div class="home_social_width_1">
                    	<div id="recentmsgs_lin">
                        	<h3>Recent messages</h3>
                            <%--<span>
                            	<abbr>2 minutes</abbr>
                            	<i><img src="../Contents/img/photo.png"></i>
                                <h4>Rohit Sane <b>@Rohit Sane</b></h4>
                                <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                <a href="#">View Conversation</a>
                            </span>
                            
                            <span>
                            	<abbr>5 minutes</abbr>
                            	<i><img src="../Contents/img/photo.png"></i>
                                <h4>Rohit Sane <b>@Rohit Sane</b></h4>
                                <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                <a href="#">View Conversation</a>
                            </span>
                            
                            <span>
                            	<abbr>10 minutes</abbr>
                            	<i><img src="../Contents/img/photo.png"></i>
                                <h4>Rohit Sane <b>@Rohit Sane</b></h4>
                                <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                <a href="#">View Conversation</a>
                            </span>
                            
                            <span>
                            	<abbr>15 minutes</abbr>
                            	<i><img src="../Contents/img/photo.png"></i>
                                <h4>Rohit Sane <b>@Rohit Sane</b></h4>
                                <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                <a href="#">View Conversation</a>
                            </span>
                            
                            
                            <span>
                            	<abbr>30 minutes</abbr>
                            	<i><img src="../Contents/img/photo.png"></i>
                                <h4>Rohit Sane <b>@Rohit Sane</b></h4>
                                <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                <a href="#">View Conversation</a>
                            </span>
                            
                            <span>
                            	<abbr>1 day</abbr>
                            	<i><img src="../Contents/img/photo.png"></i>
                                <h4>Rohit Sane <b>@Rohit Sane</b></h4>
                                <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                <a href="#">View Conversation</a>
                            </span>
                            <ul id="pagenation">
                            	<li><a href="#">prv</a></li>
                            	<li><a href="#" class="active">1</a></li>
                                <li><a href="#">2</a></li>
                                <li><a href="#">3</a></li>
                                <li><a href="#">4</a></li>
                                <li><a href="#">5</a></li>
                                <li><a href="#">Next</a></li>
                            </ul>--%>
                           <div class="loader"><img alt="" src="../Contents/img/482.gif"></div>
                        </div>
                    </div>
          	  </section>
        <section>
                    <ul id="gplusUserMid" class="sub_name">
                    	<li><img style="width:50%;height:10px;" src="../Contents/img/292.gif" alt="" /><a href="#"></a></li>
                    </ul>
            		<div class="home_social_width">
                    	<span><img src="../Contents/img/blankLarge.png"></span>
                        <ul>
                        	<li><b>2</b>Fans</li>
                            <li><b>0.54</b>Avg. Post Per Day</li>
                        </ul>
                    </div>
                    <div class="home_social_width_1">
                    	<div id="recentmsgs_gp">
                        	<h3>Recent messages</h3>
                          <%--  <span>
                            	<abbr>2 minutes</abbr>
                            	<i><img src="../Contents/img/photo.png"></i>
                                <h4>Rohit Sane <b>@Rohit Sane</b></h4>
                                <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                <a href="#">View Conversation</a>
                            </span>
                            
                            <span>
                            	<abbr>5 minutes</abbr>
                            	<i><img src="../Contents/img/photo.png"></i>
                                <h4>Rohit Sane <b>@Rohit Sane</b></h4>
                                <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                <a href="#">View Conversation</a>
                            </span>
                            
                            <span>
                            	<abbr>10 minutes</abbr>
                            	<i><img src="../Contents/img/photo.png"></i>
                                <h4>Rohit Sane <b>@Rohit Sane</b></h4>
                                <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                <a href="#">View Conversation</a>
                            </span>
                            
                            <span>
                            	<abbr>15 minutes</abbr>
                            	<i><img src="../Contents/img/photo.png"></i>
                                <h4>Rohit Sane <b>@Rohit Sane</b></h4>
                                <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                <a href="#">View Conversation</a>
                            </span>
                            
                            
                            <span>
                            	<abbr>30 minutes</abbr>
                            	<i><img src="../Contents/img/photo.png"></i>
                                <h4>Rohit Sane <b>@Rohit Sane</b></h4>
                                <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                <a href="#">View Conversation</a>
                            </span>
                            
                            <span>
                            	<abbr>1 day</abbr>
                            	<i><img src="../Contents/img/photo.png"></i>
                                <h4>Rohit Sane <b>@Rohit Sane</b></h4>
                                <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                                <a href="#">View Conversation</a>
                            </span>
                            <ul id="pagenation">
                            	<li><a href="#">prv</a></li>
                            	<li><a href="#" class="active">1</a></li>
                                <li><a href="#">2</a></li>
                                <li><a href="#">3</a></li>
                                <li><a href="#">4</a></li>
                                <li><a href="#">5</a></li>
                                <li><a href="#">Next</a></li>
                            </ul>--%>
                            <div class="loader"><img alt="" src="../Contents/img/482.gif"></div>
                        </div>
                    </div>
          	  </section>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {

            getFacebookUsersForHome();
           
           

        });
     
    </script>
</asp:Content>
