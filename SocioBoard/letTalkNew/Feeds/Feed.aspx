<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true"
    CodeBehind="Feed.aspx.cs" Inherits="letTalkNew.Feeds.Feed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Contents/css/jquery.mCustomScrollbar.css" rel="stylesheet" type="text/css" />
    <script src="../Contents/js/jquery.mCustomScrollbar.concat.min.js" type="text/javascript"></script>
    <script src="../Contents/js/blocksit.min.js" type="text/javascript"></script>
    <script src="../Contents/js/jquery.easing.1.3.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            try {
                //BindFeeds("facebook");
                //alert('abhay');
                $("#fbsection").show();
                getFBUserForFeeds();

            }
            catch (e) {
                alert(e);
            }
        });
    </script>
    <div class="container_right">
        <!--graph-->
        <div class="container_right">
            <!--nav-->
            <nav>
                <ul id="tabs">
                    <li onclick="getFBUserForFeeds()"><img src="../Contents/img/facebook.png" alt="" /><a class="current" href="#" style="padding:4px 0px 0px 30px;">Facebook</a></li>
                <li onclick="getTwitterUsersForHome()"><img src="../Contents/img/twitter.png" alt="" /><a href="#" style="padding:4px 0px 0px 30px;">Twitter</a></li>
                <li onclick="getLinkedinUsersForHome()"><img src="../Contents/img/linked_25X24.png" alt="" /><a href="#" style="padding:4px 0px 0px 30px;">Linkedin</a></li>
                <li onclick="getGplusUsersForHome()"><img src="../Contents/img/google_plus.png" alt="" /><a href="#" style="padding:4px 0px 0px 30px;">google plus</a></li>
                </ul>
                <span id="indicator"></span> 
            </nav>
            <!--end nav-->
            <!--content-->
            <div id="content" style="width: 850px;">
                <!--common_width-->
                <div class="common_width">
                    <section id="fbsection" style="display: none;">
                	    <ul  id="fbUserMid" class="sub_name">
                        	<li><img style="width:50%;height:10px;" src="../Contents/img/292.gif" alt="" /><a href="#"></a></li>
                    	   <%-- <li><img src="../Contents/img/fbicon_new.png"><a href="#">Name-1</a></li>
                            <li><img src="../Contents/img/fbicon_new.png"><a href="#">Name-2</a></li>
                            <li><img src="../Contents/img/fbicon_new.png"><a href="#">Name-3</a></li>--%>
                        </ul>
                        <!--for fb div-->
                        <div class="row_fluid1 recpro rounder scroll">
                	<h2>Home</h2>
                  	<ul id="fbHome" class="news_feeds">
                    	<%--<li>
                        	<span><img src="../Contents/img/pic_1.jpg" alt="" />8/8/2013</span>
                            <h2>Richa Mallik</h2>
                            <p>Lorem Ipsum is simply dummy text</p>
                            <a href="#">5:30:15 PM</a>
                        </li>
                        
                        <li>
                        	<span><img src="../Contents/img/pic_1.jpg" alt="" />8/8/2013</span>
                            <h2>Richa Mallik</h2>
                            <p>Lorem Ipsum is simply dummy text</p>
                            <a href="#">5:30:15 PM</a>
                        </li>
                        
                        <li>
                        	<span><img src="../Contents/img/pic_1.jpg" alt="" />8/8/2013</span>
                            <h2>Richa Mallik</h2>
                            <p>Lorem Ipsum is simply dummy text</p>
                            <a href="#">5:30:15 PM</a>
                        </li>
                        <li>
                        	<span><img src="../Contents/img/pic_1.jpg" alt="" />8/8/2013</span>
                            <h2>Richa Mallik</h2>
                            <p>Lorem Ipsum is simply dummy text</p>
                            <a href="#">5:30:15 PM</a>
                        </li>--%>
                    </ul>
                </div>
                        <!--end for fb div--> 
                
                        <!--for Linkedin div-->
                        <div class="row_fluid1 recpro rounder scroll">
                	<h2>Scheduled Messages</h2>
                  	<ul id="scheduled">
                    	<%--<li><img src="../Contents/img/pic_1.jpg" alt="" />
                            	<a href="#">Hello</a>

                        </li>--%>
                    </ul>
                </div>
                        <!--end for linkedin div--> 
                
                        <!--for instagram div-->
                        <div class="row_fluid1 recpro rounder scroll">
                	<h2>Feeds</h2>
                  	<ul id="fbFeed" class="wallposts">
                    	<%--<li>
                        	<span><img src="../Contents/img/pic_1.jpg" alt="" /></span>
                            <h2>Richa Mallik</h2>
                            <b>8/8/2013</b>
                            <a href="#">5:30:15 PM</a>
                            <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy <img src="../Contents/img/img-11.jpg">text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries</p>
                        </li>--%>
                     </ul>
                </div>
                    </section>
                    <section id="twtsection" style="display: none;">
                        <ul id="twtUserMid" class="sub_name">
                    	   <li><img style="width:50%;height:10px;" src="../Contents/img/292.gif" alt="" /><a href="#"></a></li>
                        </ul>

                	    <div class="row_fluid1 recpro rounder scroll">
                	<h2>Home</h2>
                  	<ul id="twthome" class="news_feeds">
                    	
                    </ul>
                </div>
                       
                
                       
                        <div class="row_fluid1 recpro rounder scroll">
                	<h2>Scheduled Messages</h2>
                  	<ul id="twtschedule">
                    	<%--<li><img src="../Contents/img/pic_1.jpg" alt="" />
                            	<a href="#">Hello</a>

                        </li>--%>
                    </ul>
                </div>
                       
                
                       
                        <div class="row_fluid1 recpro rounder scroll">
                	<h2>Feeds</h2>
                  	<ul id="twtfeed" class="wallposts">
                    	<%--<li>
                        	<span><img src="../Contents/img/pic_1.jpg" alt="" /></span>
                            <h2>Richa Mallik</h2>
                            <b>8/8/2013</b>
                            <a href="#">5:30:15 PM</a>
                            <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy <img src="../Contents/img/img-11.jpg">text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries</p>
                        </li>--%>
                     </ul>
                </div>
                    </section>
                    <section id="linsection" style="display: none;">
                        <ul id="linUserMid" class="sub_name">
                    	       <li><img style="width:50%;height:10px;" src="../Contents/img/292.gif" alt="" /><a href="#"></a></li>
                        </ul>

                	        <div class="row_fluid1 recpro rounder scroll">
                	<h2>Home</h2>
                  	<ul id="linhome" class="news_feeds">
                    
                    </ul>
                </div>
                      
                
                       
                        <div class="row_fluid1 recpro rounder scroll">
                	<h2>Scheduled Messages</h2>
                  	<ul id="linscheduled">
                    	<%--<li><img src="../Contents/img/pic_1.jpg" alt="" />
                            	<a href="#">Hello</a>

                        </li>--%>
                    </ul>
                </div>
                       
                        <div class="row_fluid1 recpro rounder scroll">
                	<h2>Feeds</h2>
                  	<ul id="linfeed" class="wallposts">
                    	
                     </ul>
                </div>

                    </section>
                    <section id="gpsection" style="display: none;">
                        <ul id="gplusUserMid" class="sub_name">
                    	    <li><img style="width:50%;height:10px;" src="../Contents/img/292.gif" alt="" /><a href="#"></a></li>
                        </ul>
                	    <div class="row_fluid1 recpro rounder scroll">
                	<h2>Home</h2>
                  	<ul id="Ul7" class="news_feeds">
                    	
                    </ul>
                </div>
                        <!--end for fb div--> 
                
                        <!--for Linkedin div-->
                        <div class="row_fluid1 recpro rounder scroll">
                	<h2>Scheduled Messages</h2>
                  	<ul id="Ul8">
                    	<%--<li><img src="../Contents/img/pic_1.jpg" alt="" />
                            	<a href="#">Hello</a>

                        </li>--%>
                    </ul>
                </div>
                        <!--end for linkedin div--> 
                
                        <!--for instagram div-->
                        <div class="row_fluid1 recpro rounder scroll">
                	<h2>Feeds</h2>
                  	<ul id="Ul9" class="wallposts">
                    	
                     </ul>
                </div>
                    </section>
                </div>
                <!--end common_width-->
            </div>
            <!--end content-->
        </div>
        <!--end graph-->
    </div>
</asp:Content>
