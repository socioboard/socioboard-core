<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SocialSuitePro.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Contents/js/Home.js" type="text/javascript"></script>
    <script src="Contents/js/SimplePopup.js" type="text/javascript"></script> 
 <style type="text/css">
.web_dialog_overlay {
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            height: 100%;
            width: 100%;
            margin: 0;
            padding: 0;
            background: #000000;
            opacity: .15;
            filter: alpha(opacity=15);
            -moz-opacity: .15;
            z-index: 101;
            display: none;
        }
        
        .web_dialog
        {            
            width: 380px;
            z-index: 102;
            height:200px;
            overflow-x:hidden;
            overflow-y:auto;
        }
        
        .web_dialog_title
        {
            border-bottom: solid 2px #336699;
            background-color: #336699;
            padding: 4px;
            color: White;
            font-weight:bold;
        }
        
        #fbConnect,
        #fbConnect1
        {
            background-color: #FFFFFF;
            border: 3px solid #D49188;
            border-radius: 10px 10px 10px 10px;
            font-family: Verdana;
            font-size: 10pt;
            height: 200px;
            left: 50%;
            margin-left: -190px;
            margin-top: -100px;
            overflow-x: hidden;
            overflow-y: auto;
            padding: 20px;
            position: fixed;
            top: 50%;
            width: 380px;
            z-index: 102;
        }
        .web_dialog >li
        {
            border: 1px solid #9EC1F7;
            border-radius: 5px 5px 5px 5px;
            float: left;
            height: auto;
            margin-left: 30px;
            margin-top: 10px;
            padding: 10px 7px;
            width: 300px;
        }
        
        .web_dialog_title a
        {
            color: White;
            text-decoration: none;
        }
        .align_right
        {
            text-align: right;
        }
        .MainContent_fbpage 
        {
            border-top: 1px solid #D49188;
            float: left;
            margin-left: 27px;
            margin-top: 13px;
            padding-top: 13px;
            width: 325px !important;
        }
        #btnSubmit,
        #btnSubmit1
        {
            background: none repeat scroll 0 0 #D49188;
            border-radius: 5px 5px 5px 5px;
            clear: right;
            color: #FFFFFF;
            float: right;
            font-family: Arial;
            font-size: 13px;
            height: auto;
            margin-right: 32px;
            padding: 5px;
            width: 95px;
        }
        #MainContent_fbpage > div > a >span
        {
            border: 1px solid #95B5E2;
            color: #194385;
            float: left;
            font-family: Arial;
            font-size: 13px;
            height: auto;
            margin-bottom: 10px;
            padding: 9px;
            width: 300px;
        }
    </style> 
      <!--[if IE 7]>
			<link href="css/font-awesome-ie7.min.css" rel="stylesheet">
		<![endif]-->		
        
		<!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
		<!--[if lt IE 9]>
			<script src="../Contents/js/html5shiv.js"></script>
            <script type="text/javascript" src="../Contents/js/excanvas.js"></script>
		<![endif]-->
        <script src="../Contents/js/Chart.js" type="text/javascript"></script>
        <style type="text/javascript">
			canvas{
			}
		</style> 
        <script type="text/javascript">
            $(document).ready(function () {
                $(document).click(function (e) {
                    if ($(e.target).closest('#expanderHead').length > 0 || $(e.target).closest('#linkbox').length > 0) return;
                    $('#linkbox').hide();
                });

                $(document).click(function (e) {
                    if ($(e.target).closest('#invitefromHome').length > 0 || $(e.target).closest('#inviteAthome').length > 0) return;
                    $('#inviteAthome').hide();
                });
            });
        </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div id="mainwrapper" class="container">
			<div id="sidebar">
             <div class="sidebar-inner" style="width:255px;">
                	<div class="cols2">
                    	<h4>Incoming Messages</h4>
                        <span class="counter" id="spanIncoming" runat="server">0</span>
						<canvas id="cvs" width="90" height="50">[No canvas support]</canvas>
					</div>
                	<div class="cols2">
                    	<h4>Sent Messages</h4>
                        <span class="counter" id="spanSent" runat="server">0</span>
						<canvas id="cvsSent" width="90" height="50">[No canvas support]</canvas>
					</div>
                    <div class="clear"></div>
                	<div class="cols2" style="width:116px;">
                    	<h4>New Twitter Followers</h4>
                        <span class="counter" id="spanNewTweets" runat="server">0</span>
                      <%--    <div id="chart_div" style="width: 230px; height: 80px;"></div>--%>
						<canvas id="div_twt" height="50" width="90">
					</div>
                	<div class="cols2" style="width:131px">
                    	<h4>New Facebook Fans</h4>
                        <span class="counter" id="spanFbFans" runat="server">0</span>
                       <%-- <div id="div_twt" style="width: 230px; height: 80px; float:right;"></div>--%>
						<canvas id="chart_div" height="50" width="90">
					</div>
                    <div class="clear"></div>
				</div>

				<div class="sidebar-inner">
                	<a href="Message/Messages.aspx" class="btn">GO TO INBOX <img src="../Contents/img/admin/emailtiny.png" alt="" class="pull-right" /></a>
                	<a href="Message/Task.aspx" class="btn">VIEW TASK <img src="../Contents/img/admin/hammertiny.png" alt="" class="pull-right" /></a>
				<a href="Reports/GroupStats.aspx" class="btn">VIEW REPORT <img src="../Contents/img/admin/reportstiny.png" alt="" class="pull-right" /></a>
				</div>
				<div class="sidebar-inner">
                	<h1>Select Profile</h1>
                    <span>to connect </span>SocioBoard<ul id="profile-connect" style="width:270px;">
						<a id="A4" href="#" runat="server" onserverclick="AuthenticateFacebook"><li><img src="../Contents/img/admin/fbicon.png" alt="" /></li></a>
						<a id="A5" href="#" runat="server" onserverclick="AuthenticateTwitter"><li><img src="../Contents/img/admin/twittericon.png" alt="" /></li></a>
						<%--<li><a href="#" runat="server" onserverclick="AuthenticateTwitter"><img src="../Contents/img/admin/g+icon.png" alt=""/></a></li>--%>
						<a id="A6" href="#" runat="server" onserverclick="AuthenticateLinkedin"><li><img src="../Contents/img/admin/linkedinicon.png" alt=""/></li></a>
						<a id="A1" href="#" runat="server" onserverclick="AuthenticateInstagram"><li><img src="Contents/img/instagram_24X24_grey.png" alt=""/></li></a>
						<%--<li>
                        <a id="A2" href="#" onserverclick="AuthenticateGooglePlus" runat="server"><img src="../Contents/img/google_plus_grey.png" width="24" height="24" alt="" /></a>
                        <li><a id="A3" href="#" runat="server" onserverclick="AuthenticateGoogleAnalytics"><img src="Contents/img/google_analytics_grey.png" alt=""/></a></li>
                    </li>--%>
					</ul>
				</div>
				<div class="sidebar-inner" id="bindads" runat="server">
                    <%--<asp:Image ID="imgAds" runat="server" ImageUrl="../Contents/img/admin/ads.png"/>--%>
					<%--<a href="#"><button data-dismiss="alert" class="close pull-right" type="button">×</button><img src="../Contents/img/admin/ads.png" alt="" height="221" width="221"></a>--%>
				</div>
			</div>
            <div id="contentcontainer2">
            	<div id="contentcontainer1">
                	<div id="content">
						<div class="alert alert-suite">
							<button data-dismiss="alert" class="close" type="button" style="color:#FFFFFF">×</button>
							<p id="divNews" runat="server">Welcome to SocioBoard Your Social Business Partners</p>
                            <div class="red-caret"></div>
						</div>
						<div class="title">
							<h1>Audience Demographics</h1>
							<span runat="server" id="acrossProfile" class=""></span>
						</div>
						<div class="home-graph-outer">
                            <div class="graph-left">
                            	<div class="graph-title-tw"><img src="../Contents/img/admin/twitter-icon.png" alt="" align="absmiddle" /> Twitter Followers</div>
                                <div class="follow-percent-outer">
                                	<div class="tw-male-outer">
                                    	<%=twtmale%>%
                                        <span>Male<br>Followers</span>
                                    </div>
                                    <div class="tw-female-outer">
                                    	<%=twtfemale%>%
                                        <span>Female<br>Followers</span>
                                    </div>
                                    <div class="clear"></div>
                                </div>
                                <div class="follow-percent-outer">
                                	<div class="tw-male-graph">
                                    	<canvas id="tw_male" height="117" width="117"></canvas>
                                    </div>
                                    <div class="tw-male-graph">
                                    	<canvas id="tw_female" height="117" width="117"></canvas>
                                    </div>
                                    <div class="clear"></div>
                                </div>
                                
                            </div>
                            <div class="graph-right">
                            	<div class="graph-title-fb"><img src="../Contents/img/admin/fb-icon.png" alt="" align="absmiddle" /> Facebook Page Audience</div>
                                <div class="follow-percent-outer">
                                	<div class="fb-male-outer">
                                    	<%=male%>%
                                        <span>Male<br>Friends</span>
                                    </div>
                                    <div class="fb-female-outer">
                                    	<%=female %>%
                                        <span>Female<br>Friends</span>
                                    </div>
                                    <div class="clear"></div>
                                </div>
                                <div class="follow-percent-outer">
                                	<div class="tw-male-graph">
                                    	<canvas id="fb_male" height="117" width="117"></canvas>
                                    </div>
                                    <div class="tw-male-graph">
                                    	<canvas id="fb_female" height="117" width="117"></canvas>
                                    </div>
                                    <div class="clear"></div>
                                </div>
                                
                            </div>
                            <div class="clear"></div>
                            
                        </div>

						<%--<img src="../Contents/img/admin/graph.png" alt="" />--%>
						<div class="title">
							<h1>My recent profile</h1>
							<span class="">Snapshots of your connected accounts</span>
                            <div class="clearfix"></div>
                            <div id="midsnaps" >
						<%--	<div class="row-fluid" >
								<div class="span4 rounder recpro">
									<button data-dismiss="alert" class="close pull-right" type="button">×</button>
									<a href="#"><img src="../Contents/img/admin/ads2.png" alt="" style="width:246px;height:331px"></a>
								</div>
                            	
							</div>--%>
                            </div>
						</div>
					</div>
                 <div class="home_loader">
                    <img id="hm_loader" src="Contents/img/loading.gif" width="100" height="100"  alt=""/>
                 </div>
				</div>

                   <div id="right-sidebar_home">
                    	<div class="rsidebar-inner">
                        	<h3>Profile</h3>
                           <p class="stitlemini">connected</p>
                            <p id="usedAccount" runat="server" class="stitlemini">connected</p>
                            <div id="manageprofiles">
                            <ul class="rsidebar-profile">
                            	<%--<li>
									<div class="userpictiny">
										<img src="../Contents/img/admin/user.png" height="48" width="48" alt="" title="PRAB KUMAR" />
										<a href="#" class="userurlpic" title=""><img src="../Contents/img/admin/searchmini.png" alt=""></a>
									</div>
								</li>
                            	<li>
									<div class="userpictiny">
										<img src="../Contents/img/admin/user3.png" height="48" width="48" alt="" title="PRAB KUMAR" />
										<a href="#" class="userurlpic" title=""><img src="../Contents/img/admin/searchmini.png" alt=""></a>
									</div>
								</li>
                            	<li>
									<div class="userpictiny">
										<img src="../Contents/img/admin/user.png" height="48" width="48" alt="" title="PRAB KUMAR" />
										<a href="#" class="userurlpic" title=""><img src="../Contents/img/admin/searchmini.png" alt=""></a>
									</div>
								</li>
                            	<li>
									<div class="userpictiny">
										<img src="../Contents/img/admin/user3.png" height="48" width="48" alt="" title="PRAB KUMAR" />
										<a href="#" class="userurlpic" title=""><img src="../Contents/img/admin/searchmini.png" alt=""></a>
									</div>
								</li>
                            	<li>
									<div class="userpictiny">
										<img src="../Contents/img/admin/user.png" height="48" width="48" alt="" title="PRAB KUMAR" />
										<a href="#" class="userurlpic" title=""><img src="../Contents/img/admin/searchmini.png" alt=""></a>
									</div>
								</li>
                            	<li>
								
								</li>--%>
							</ul>
                            </div>
                            	<div id="expanderHead" class="userpictiny add">
										<a><img src="../Contents/img/admin/addprofile.png" height="48" width="48" alt="" /></a>
                                        <div id="linkbox" style="display: none;">
                                            <ul>
                                               <%-- <li><a id="facebook_connect">
                                                    <img src="../Contents/img/fb_24X24.png" width="16" height="16" alt="" />
                                                    <span>Facebook</span> </a></li>
                                                <li><a id="LinkedInLink"  runat="server" onserverclick="AuthenticateLinkedin">
                                                    <img src="../Contents/img/linked_25X24.png" width="16" height="16" alt="" />
                                                    <span>LinkedIn</span> </a></li>--%>
                                              <%--<li><a id="TwitterOAuth" runat="server" onserverclick="AuthenticateTwitter">
                                                    <img src="../Contents/img/twt_icon.png" width="16" height="16" alt="" />
                                                    <span>Twitter</span> </a></li>
                                                <li><a id="googleplus_connect" runat="server">
                                                    <img src="Contents/Images/google_plus.png" width="16" height="16" alt="" />
                                                    <span>Google Plus</span> </a></li>--%>
                                            <%--    <li><a id="googleanalytics_connect" runat="server" href="#">
                                                    <img src="../Contents/img/google_analytics.png" width="16" height="16" alt="" />
                                                    <span>Google Analytics</span> </a></li>--%>
                                               <%-- <li><a id="InstagramConnect"  runat="server" onserverclick="AuthenticateInstagram">
                                                    <img src="../Contents/img/instagram_24X24.png" width="16" height="16" alt="" />
                                                    <span>Instagram</span> </a></li>--%>

                                            </ul>
                                            <div class="drop_top"></div>
                                            <div class="drop_mid loginbox">
                                                <div class="teitter">
                                                    <ul>
                                                        <li>
                                                            <a id="facebook_connect">
                                                                <img width="18" border="none" style="float:left;" alt="" src="../Contents/img/facebook.png" />
                                                                <span style="float:left;margin: 3px 0 0 5px;">Facebook</span>
                                                           </a>
                                                        </li>                                                        
                                                    </ul> 
                                                 </div>
                                                <div class="teitter">
                                                    <ul>
                                                        <li>
                                                            <a id="TwitterOAuth" runat="server" onserverclick="AuthenticateTwitter">
                                                                <img width="18" border="none" style="float:left;" alt="" src="../Contents/img/twitter.png" />
                                                                <span style="float:left;margin: 3px 0 0 5px;">Twitter</span>
                                                            </a>
                                                        </li>
                                                    </ul> 
                                                  </div>
                                                <div class="teitter">
                                                    <ul>
                                                        <li>
                                                            <a id="LinkedInLink"  runat="server" onserverclick="AuthenticateLinkedin">
                                                                <img width="18" border="none" style="float:left;" alt="" src="../Contents/img/link.png" />
                                                                <span style="float:left;margin: 3px 0 0 5px;">LinkedIn</span>
                                                            </a>
                                                        </li>
                                                    </ul>       
                                                 </div>
                                                <div class="teitter">
                                                    <ul>
                                                        <li>
                                                            <a id="InstagramConnect"  runat="server" onserverclick="AuthenticateInstagram">
                                                                <img width="18" border="none" style="float:left;" alt=""  src="../Contents/img/instagram_24X24.png" />
                                                                <span style="float:left;margin: 3px 0 0 5px;">Instagram</span>
                                                            </a>
                                                        </li>
                                                    </ul> 
                                                 </div>
                                                   <%-- <div class="teitter">
                                                    <ul>
                                                        <li>
                                                            <a id="GooglePlusConnect"  runat="server" onserverclick="AuthenticateGooglePlus">
                                                                <img width="18" border="none" style="float:left;" alt=""  src="../Contents/img/google_plus.png" />
                                                                <span style="float:left;margin: 3px 0 0 5px;">Google Plus</span>
                                                            </a>
                                                        </li>
                                                    </ul> 
                                                 </div>
                                                   <div class="teitter">
                                                    <ul>
                                                        <li>
                                                            <a id="GoogleAnalyticsConnect"  runat="server" onserverclick="AuthenticateGoogleAnalytics">
                                                                <img width="18" border="none" style="float:left;" alt=""  src="../Contents/img/an_24X24.png" />
                                                                <span style="float:left;margin: 3px 0 0 5px;">Google Analytics</span>
                                                            </a>
                                                        </li>
                                                    </ul> 
                                                 </div>--%>
                                            </div>
                                        </div>
						        </div>
						</div>
                
                    	<div class="rsidebar-inner">
                        	<h3>Team Members</h3>
                            <p class="stitlemini" id="teamMem" runat="server"></p>
                                 <div class="team_member" id="team_member" runat="server">
                                <div class="userpictiny">
                                    <a href="#" target="_blank">
                                        <img width="48" height="48" src="http://graph.facebook.com/100004496770422/picture?type=small" alt="" title="Prab Kumar" />
                                     </a>

                                </div>

                                <div class="userpictiny">
                                    <a href="#" target="_blank">
                                        <img width="48" height="48" src="http://graph.facebook.com/100004496770422/picture?type=small" alt="" title="Prab Kumar" />
                                     </a>

                                </div>

                                <div class="userpictiny">
                                    <a href="#" target="_blank">
                                        <img width="48" height="48" src="http://graph.facebook.com/100004496770422/picture?type=small" alt="" title="Prab Kumar" />
                                     </a>

                                </div>
        
                            </div>
                            <div id="teammember" runat="server">
                            <ul>
                            	<%--<li><a href="#"><img src="../Contents/img/admin/img.png" height="48" width="48" alt="" /></a></li>--%>
                                 <li  id="invitefromHome"><a id="invitehome"><img src="../Contents/img/admin/invite.png" height="48" width="48" alt="" /></a>
                                <div  id="inviteAthome" style="display: none;">
                                    <div class="drop_top">
                                    </div>
                                    <div  class="drop_mid loginbox">
                                        <div class="teitter">
                                            <ul runat="server" id="getAllGroupsOnHome">
                                                <li><a>No Records Found</a></li>
                                                    
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                
                                
                                </li>
                            </ul>
                            </div>
						</div>                        
                    	<div class="rsidebar-inner">
                        	<h3>Recent Followers</h3>
                            <ul id="recentfollowers">
                            	
                            </ul>
						</div>                        
					</div>

			</div>
		</div>
        <script type="text/javascript" language="javascript">
            function getInsights(id, name) {
                debugger;
                $.ajax
                        ({
                            type: "GET",
                            url: "../AjaxHome.aspx?op=insight&id=" + id + "&name=" + name,
                            data: '',
                            contentType: "application/json; charset=utf-8",
                            success: function (msg) {
                                debugger;
                                HideDialogHome();
                                BindSocialProfiles();

                            }

                        });
            }
            $(document).ready(function () {
                //                $("#expanderHead").click(function (event) {
                //                    debugger;
                //                    $("#expanderContent").slideToggle();
                //                });

                BindSocialProfiles();
                BindMidSnaps("load");
                RecentFollowersOnHome();
                $("#facebook_connect").click(function (e) {
                    debugger;
                    ShowDialogHome(false);
                    e.preventDefault();
                });

                //                $("#fb_cont").click(function (e) {
                //                    ShowDialogHome(false);
                //                    e.preventDefault();
                //                });
                $("#btnSubmit").click(function (e) {
                    HideDialogHome();
                    e.preventDefault();
                    $("#<%=fbpage %>").html("");
                });
            });

            $("#invitehome").click(function () {
                debugger;
                var yes = $("#invitehome").attr('href');
                if (yes == "#") {
                    $("#errsuccess").removeClass('greenerrormsg');
                    $("#errsuccess").addClass('rederrormsg');
                    $("#errsuccessmsg").html('No Group is added to invite members');
                    $("#errsuccess").show();
                }
            });
        </script>
         <div id="overlay" class="web_dialog_overlay"></div>

     <div id="fbConnect1" style="display:none;">
        <div class="fbweb_dialog">
              <li>
                <a id="fb_account" runat="server" onserverclick="AuthenticateFacebook">
                    <img src="Contents/img/fb_24X24.png" width="16" height="16" alt="" />
                    <span>Facebook account</span> 
                </a>
              </li>
              <li>
                <a id="fb_pages" runat="server" onserverclick="fbPage_connect">
                  <img src="Contents/img/fb_24X24.png" width="16" height="16" alt="" />
                    <span>Fan Page</span> 
                </a>
             </li>
             <div id="fbpage" class="MainContent_fbpage" runat="server"></div>        
             <input id="btnSubmit" type="button" value="Cancel" />
        </div>
    </div>
     <script src="../Contents/js/RGraph.common.core.js" ></script>
		<script src="../Contents/js/RGraph.common.key.js" ></script>
        <script src="../Contents/js/RGraph.bar.js" ></script>
          <script type="text/javascript" src="https://www.google.com/jsapi"></script>
        <script>
          google.load("visualization", "1", {packages:["corechart"]});
         $(document).ready(function () {
            var twtm = <%= twtmale%>;
            var twtf=<%= twtfemale%>;
            var fbm=<%= male%>;
            var fbf=<%=female %>;
            
            
            debugger;
            var doughnutData = [
				{
				    value: twtm,
				    color: "#cae7ed"
				},
				{
				    value: 100-twtm,
				    color: "#4dc1d9"
				}
			];
            var myDoughnut = new Chart(document.getElementById("tw_male").getContext("2d")).Doughnut(doughnutData);
            var doughnutData = [
				{
				    value: twtf,
				    color: "#d9e2d0"
				},
				{
				    value: 100-twtf,
				    color: "#87ab66"
				}
			];
            var myDoughnut = new Chart(document.getElementById("tw_female").getContext("2d")).Doughnut(doughnutData);
            var doughnutData = [
				{
				    value: fbm,
				    color: "#cfd9e8"
				},
				{
				    value: 100-fbm,
				    color: "#6087c3"
				}
			];
            var myDoughnut = new Chart(document.getElementById("fb_male").getContext("2d")).Doughnut(doughnutData);
            var doughnutData = [
				{
				    value: fbf,
				    color: "#dce5e7"
				},
				{
				    value: 100-fbf,
				    color: "#94b9c0"
				}
			];
            var myDoughnut = new Chart(document.getElementById("fb_female").getContext("2d")).Doughnut(doughnutData);

             var fbArr=<%=strFBArray %>;
            var barChartData = {
               labels : ["1","2","3","5","6","7"],
                datasets: [
				{
				    fillColor: "rgba(167,167,167,1)",
				    strokeColor: "rgba(167,167,167,0)",
				    data: fbArr
				}
			]

            }
            debugger;
           
            var myLine = new Chart(document.getElementById("chart_div").getContext("2d")).Bar(barChartData);
            try
            {
             var twtArr=<%=strTwtArray %>;
             debugger;
           	var barChartData = {
			labels : ["1","2","3","5","6"],
			datasets : [
				{
					fillColor : "rgba(167,167,167,1)",
					strokeColor : "rgba(167,167,167,0)",
					data :twtArr
				}
			]
			
		}

	var myLine = new Chart(document.getElementById("div_twt").getContext("2d")).Bar(barChartData);
    }
    catch(e)
    {}
            debugger;
          //  var bar = new RGraph.Bar('cvs', [[5.33, 2.33, 3.32], [3.42, 2.23, 4.23], [4.23, 3.23, 4.99], [7.99, 2.98, 2.35], [3.42, 2.23, 4.23], [2.75, 1.02, 5.24], [3.42, 2.23, 4.23]])
          var arrData=<%=strArray %>;
          var bar = new RGraph.Bar('cvs',arrData)
                .Set('grouping', 'stacked')
            //.Set('labels', ['John','James','Fred','Luke','Luis'])
            //.Set('labels.above', true)
            //.Set('labels.above.decimals', 2)
                .Set('linewidth', 0)
            //.Set('strokestyle', 'white')
                .Set('colors', ['Gradient(#4572A7:#66f)', 'Gradient(#AA4643:white)', 'Gradient(#89A54E:white)'])
                .Set('shadow', true)
                .Set('shadow.offsetx', 1)
                .Set('shadow.offsety', 2)
                .Set('shadow.blue', 5)
            //.Set('hmargin', 5)
                .Set('gutter.left', 0)
                .Set('background.grid.vlines', false)
                .Set('background.grid.border', false)
				.Set('background.grid.hlines', false)
                .Set('axis.color', '#ccc')
                .Set('noyaxis', true)
            //.Set('key', ['Monday','Tuesday','Wednesday'])
                .Set('key.position', 'gutter');
            bar.Set('key.position.x', bar.canvas.width - 50)
                .Set('key.position.y', 10)
            //.Set('key.colors', ['blue','#c00','#0c0'])
            /*.ondraw = function (obj)
            {
            for (var i=0; i<obj.coords.length; ++i) {
            obj.context.fillStyle = 'white';
            RGraph.Text(obj.context, 'Verdana', 8, obj.coords[i][0] + (obj.coords[i][2] / 2), obj.coords[i][1] + (obj.coords[i][3] / 2),obj.data_arr[i].toString(),'center', 'center', null,null,null,true);
            }
            }*/

            bar.Draw();
       var arrSentData=<%=strSentArray %>;
       //  var barSent = new RGraph.Bar('cvsSent', [[5.33, 2.33, 3.32], [3.42, 2.23, 4.23], [4.23, 3.23, 4.99], [7.99, 2.98, 2.35], [3.42, 2.23, 4.23], [2.75, 1.02, 5.24], [3.42, 2.23, 4.23]])
       var barSent = new RGraph.Bar('cvsSent', arrSentData)
                .Set('grouping', 'stacked')
            //.Set('labels', ['John','James','Fred','Luke','Luis'])
            //.Set('labels.above', true)
            //.Set('labels.above.decimals', 2)
                .Set('linewidth', 0)
            //.Set('strokestyle', 'white')
                .Set('colors', ['Gradient(#4572A7:#66f)', 'Gradient(#AA4643:white)', 'Gradient(#89A54E:white)'])
                .Set('shadow', true)
                .Set('shadow.offsetx', 1)
                .Set('shadow.offsety', 2)
                .Set('shadow.blue', 5)
            //.Set('hmargin', 5)
                .Set('gutter.left', 0)
                .Set('background.grid.vlines', false)
                .Set('background.grid.border', false)
				.Set('background.grid.hlines', false)
                .Set('axis.color', '#ccc')
                .Set('noyaxis', true)
            //.Set('key', ['Monday','Tuesday','Wednesday'])
                .Set('key.position', 'gutter');
            barSent.Set('key.position.x', bar.canvas.width - 50)
                .Set('key.position.y', 10)
            //.Set('key.colors', ['blue','#c00','#0c0'])

            barSent.Draw();

           //  google.setOnLoadCallback(drawChart);             
          //  google.setOnLoadCallback(drawChartTwitter);
	});

     function drawChart() {
      var fbArr="<%=strFBArray %>".split(",");
          var items = new Array(fbArr.length);
          items[0] = new Array(2);
                items[0][0] = "Age";
                items[0][1] = "Visits";
      for (var i = 1; i < fbArr.length; i++) {
                    items[i] = new Array(2);
                    if (fbArr[i - 1] != "") {                       
                        items[i][0] = i;
                        items[i][1] = Number(fbArr[i]);
                    }
                   
                }
        var data = google.visualization.arrayToDataTable(items);

        var options = {
            backgroundColor:'#DEDEDE',
            vAxis: { textPosition: 'none',gridlines :{count:0}},
           hAxis: { textPosition: 'none',gridlines :{count:0}}
            //title: 'Company Performance',
            //hAxis: {title: 'Year', titleTextStyle: {color: 'red'}}
        };

        var chart = new google.visualization.ColumnChart(document.getElementById('div_twt'));
        chart.draw(data, options);
        }
           function drawChartTwitter() {
     debugger;
      var twtArr="<%=strTwtArray %>".split(",");
         var items = new Array(twtArr.length);
          items[0] = new Array(2);
                items[0][0] = "Age";
                items[0][1] = "Visits";
      for (var i = 1; i <= twtArr.length; i++) {
                    items[i] = new Array(2);
                    if (twtArr[i - 1] != "") {                       
                        items[i][0] = i;
                        items[i][1] = Number(twtArr[i]);
                    }
                   
                }
        var data = google.visualization.arrayToDataTable(items);

        var options = {
        backgroundColor:'#DEDEDE',
            vAxis: { textPosition: 'none',gridlines :{count:0}},
          hAxis: { textPosition: 'none',gridlines :{count:0}}
            //title: 'Company Performance',
            //hAxis: {title: 'Year', titleTextStyle: {color: 'red'}}
        };

        var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
        chart.draw(data, options);
        }
	</script>
     
  
</asp:Content>
