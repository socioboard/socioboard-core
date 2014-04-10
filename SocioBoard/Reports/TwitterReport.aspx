<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="TwitterReport.aspx.cs" Inherits="SocialSuitePro.Reports.TwitterReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div id="mainwrapper" class="container reports">
			<div id="sidebar">            	
				<div class="sidebar-inner">
                	<a href="GroupStats.aspx" class="btn">GROUP REPORT <img src="<%= Page.ResolveUrl("~/Contents/img/admin/boxes.png")%>" alt="" class="pull-right" /></a>
                	<a href="FacebookReport.aspx" class="btn">Facebook Pages <img src="<%= Page.ResolveUrl("~/Contents/img/admin/fbicon2.png")%>" alt="" class="pull-right" /></a>
					<a href="TwitterReport.aspx" class="btn actives" id="twtrpt">Twitter Reports <img src="<%= Page.ResolveUrl("~/Contents/img/admin/twittericon2.png")%>" alt="" class="pull-right" /></a>
                    <div id="twtrptbox" style="display: none;">                                            
                                       <div class="drop_top"></div>
                                       <div id="divtwtUser" runat="server" class="drop_mid loginbox">
                                                <div  class="teitter">
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
                                                            <a id="TwitterOAuth" runat="server">
                                                                <img width="18" border="none" style="float:left;" alt="" src="../Contents/img/twitter.png" />
                                                                <span style="float:left;margin: 3px 0 0 5px;">Twitter</span>
                                                            </a>
                                                        </li>
                                                    </ul> 
                                                  </div>
                                                <div class="teitter">
                                                    <ul>
                                                        <li>
                                                            <a id="LinkedInLink"  runat="server">
                                                                <img width="18" border="none" style="float:left;" alt="" src="../Contents/img/link.png" />
                                                                <span style="float:left;margin: 3px 0 0 5px;">LinkedIn</span>
                                                            </a>
                                                        </li>
                                                    </ul>       
                                                 </div>
                                                <div class="teitter">
                                                    <ul>
                                                        <li>
                                                            <a id="InstagramConnect"  runat="server">
                                                                <img width="18" border="none" style="float:left;" alt=""  src="../Contents/img/instagram_24X24.png" />
                                                                <span style="float:left;margin: 3px 0 0 5px;">Instagram</span>
                                                            </a>
                                                        </li>
                                                    </ul> 
                                                 </div>
                                                <div class="teitter">
                                                    <ul>
                                                        <li>
                                                            <a id="GooglePlusConnect"  runat="server" >
                                                                <img width="18" border="none" style="float:left;" alt=""  src="../Contents/img/google_plus.png" />
                                                                <span style="float:left;margin: 3px 0 0 5px;">Google Plus</span>
                                                            </a>
                                                        </li>
                                                    </ul> 
                                                </div>
                                       </div>
                                </div>
					<a href="TeamReport.aspx" class="btn">Team Report <img src="../Contents/img/admin/peoples.png" alt="" class="pull-right" /></a>
					<%--<a class="btn" href="GoogleAnalytics.aspx">Google Analytics Report <img class="pull-right" alt="" src="../Contents/img/admin/peoples.png"></a>--%>
				</div>
				
			</div>           
            	<div id="contentcontainer-report">
                	<div id="content"  style="left:0">
						<div class="alert alert-suite-grey" style="width:882px;">
							<div class="title">
								<h1>Twitter account report</h1>
								<span class="" id="spandiv" runat="server">from Apr. 15, 2013 - Apr. 29, 2013</span>
							</div>
                            <div id="exportdt" class="pull-right">
                            	<img src="../Contents/img/admin/tt.png" class="help" alt="" title="This is tooltip"/>
                                 <asp:Button ID="btnfifteen" runat="server"  class="togl btn down" Text="15" onclick="btnfifteen_Click" />
                                <asp:Button ID="btnthirty" runat="server" Text="30" class="togl btn" 
                                    onclick="btnthirty_Click"/>
                                <asp:Button ID="btnsixty" runat="server" Text="60" class="togl btn" 
                                    onclick="btnsixty_Click"/>
                                <asp:Button ID="btnninty" runat="server" Text="90" class="togl btn" 
                                    onclick="btnninty_Click"/>    
                                <%--<button class="togl btn"><img src="../Contents/img/admin/add.png" alt="" style="margin-top:-5px"/></button>
                                <button class="btn" style="padding:2px 5px;background:none repeat scroll 0 0 #CB786F;color:#FFFFFF;text-shadow:none">Export Data <b class="caret"></b></button>--%>
                            </div>
                            <div class="grey-caret"></div>
						</div>
                        
                        <div class="rounder shadower pull-left reportcontent" style="padding-right:20px; width:895px;">
                        	
                            <div class="twitter-dashboard-left">
                            	<div class="user-details-outer">
                                	<div class="nam-photo">
                                    	<div class="avathar-pub">
                                            <asp:Image ID="profileImg" runat="server" /> <%--<img src="../Contents/img/avathar-1.jpg" alt="" />--%></div>
                                        <div class="twitter-avathar-name" id="divnameId" runat="server">Gaurav05</div>
                                    </div>
                                    <div class="follow-details">
                                    	<div class="follow-details-txt">Total Followers <span id="spanFollowers" runat="server">0</span></div>
                                        <div class="follow-details-txt"><span><%--0 connections--%></span></div>
                                        <div class="follow-details-txt">made in this time period</div>
                                    </div>
                                    <div class="clear"></div>
                                </div>
                                
                                <div class="followers-details-outer">
                                	<div class="follower-list">
                                    	<div class="follow-label">New Follower</div>
                                        <div class="follow-number" id="divnewFollower" runat="server">0</div>
                                        <div class="dash-graph"><div id="newfollower_graph" style="width:80px; height:35px;"></div></div>
                                        <div class="clear"></div>
                                    </div>
                                    <div class="follower-list">
                                    	<div class="follow-label">You Followed</div>
                                        <div class="follow-number" id="divFollowed" runat="server">0</div>
                                        <div class="dash-graph"><div id="newfollowed_graph" style="width:80px; height:35px;"></div></div>
                                        <div class="clear"></div>
                                    </div>
                                	<div class="follower-list">
                                    	<div class="follow-label">Direct Message<br/>(Received)</div>
                                        <div class="follow-number" id="divdmr" runat="server">0</div>
                                         <div class="dash-graph"><div id="msg_rec_graph" style="width:80px; height:35px;"></div></div>
                                        <div class="clear"></div>
                                    </div>
                                </div>
                                
                            </div>
                            
                            <div class="twitter-dashboard-right">
                                <div class="followers-details-outer">
                                	<div class="follower-list">
                                    	<div class="follow-label">@Mentions</div>
                                        <div class="follow-number" id="divMention" runat="server">0</div>
                                        <div class="dash-graph"><div id="mention_graph" style="width:80px; height:35px;"></div></div>
                                        <div class="clear"></div>
                                    </div>
                                    <div class="follower-list">
                                    	<div class="follow-label">Message Sent</div>
                                        <div class="follow-number" id="divMsgSent" runat="server">0</div>
                                        <div class="dash-graph"><div id="msg_sent_graph" style="width:80px; height:35px;"></div></div>
                                        <div class="clear"></div>
                                    </div>
                                	<div class="follower-list">
                                    	<div class="follow-label" >Message Received</div>
                                        <div class="follow-number" id="divMsgReceived" runat="server">0</div>
                                         <div class="dash-graph"><div id="msg_rece_graph" style="width:80px; height:35px;"></div></div>
                                        <div class="clear"></div>
                                    </div>
                                  <%--  <div class="follower-list">
                                    	<div class="follow-label">Clicks</div>
                                        <div class="follow-number">0</div>
                                        <div class="dash-graph"><div id="click_graph" style="width:80px; height:35px;"></div></div>
                                        <div class="clear"></div>
                                    </div>--%>
                                    <div class="follower-list">
                                    	<div class="follow-label">Retweets</div>
                                        <div class="follow-number" id="divretweetCnt" runat="server">0</div>
                                        <div class="dash-graph"><div id="retweet_graph" style="width:80px; height:35px;"></div></div>
                                        <div class="clear"></div>
                                    </div>
                                    <div class="follower-list">
                                    	<div class="follow-label">Direct Message<br/>(Sent)</div>
                                        <div class="follow-number" id="divdms" runat="server">0</div>
                                        <div class="dash-graph"><div id="dir_msg_sent_graph" style="width:80px; height:35px;"></div></div>
                                        <div class="clear"></div>
                                    </div>
                                </div>
                                
                            </div>
                            
                            
                            <div class="clear"></div>
                            
                        </div>
                        
                        <div class="alert alert-suite-grey" style="width:882px;">
							<div class="title">
								<h1>key indicators</h1>
								<span class="">Measure how you’re conversing with your audience.</span>
							</div>
                            
                            <div class="grey-caret"></div>
						</div>
                        
                        <div class="rounder shadower pull-left reportcontent" style="padding-right:20px; width:895px;">
                        	
                            <div class="key-left">
                            	<div class="key-header">
                                	<h2>my social scores</h2>
                                    <div class="graph-details">
                                    	<span id="spanEng" style="margin-right: 5px; color: rgb(109, 183, 49);">ENGAGEMENT 3% </span> <span id="spanInf">INFLUENCE 35%</span>
                                    </div>
                                    <div class="clear"></div>
                                </div>
                                <div id="social_graph" style="width:515px; height:135px;"></div>
                                
                                
                            </div>
                            
                            <div class="key-right">
                            	<div class="key-header">
                                	<h2></h2>
                                </div>
                                
                                
                            </div>
                            
                            <div class="clear"></div>
                        </div>
                        
						<div class="alert alert-suite-grey" style="width:882px;">
							<div class="title">
								<h1>FOLLOWER DEMOGRAPHICS</h1>
								<span class="">Learn more about your audience to shape your messaging & campaigns.</span>
							</div>
                            
                            <div class="grey-caret"></div>
						</div>
                        
                        <div class="rounder shadower pull-left reportcontent" style="padding-right:20px; width:895px;">
                        	
                            <div class="agerange-left">
                            	<div class="key-header">
                                	<h2>By AGE RANGE</h2>
                                    <div class="clear"></div>
                                </div>
                              <%--  <div id="chart_div" style="width: 380px; height:180px;"></div>--%>
                              	<canvas id="bar_5" height="150" width="350"></canvas>
                            </div>
                            
                            <div class="agerange-right">
                            	<div class="key-header">
                                	<h2>By GENDER</h2>
                                </div>
                                <div class="male-female-outer">
                                	<div class="male-left" id="divtwtMale" runat="server">
                                    	83%<span>MALE FOLLOWERS</span>
                                    </div>
                                    <div class="female-right" id="divtwtfeMale" runat="server">
                                    	83%<span>MALE FOLLOWERS</span>
                                    </div>
                                    <div class="clear"></div>
                                </div>
                           <%--     <div class="gender-info">
                                	Men between the ages of 18-20 appear to be the 
leading force among your followers.
                                </div>--%>
                                
                            </div>
                            
                            <div class="clear"></div>
                        </div>
                    <%--    
                       <div class="alert alert-suite-grey" style="width:882px;">
                        <div class="title">
                            <h1>publishing</h1>
                            <span class="">Measure the performance of your outbound content</span>
                        </div>
                        
                        <div class="grey-caret"></div>
                    </div>
                    --%>
                <%--    <div class="rounder shadower pull-left reportcontent" style="padding-right:20px; width:895px;">
                        
                        <div class="publish-left">
                            <div class="key-header">
                                <h2>daily engagement</h2>
                                <div class="clear"></div>
                            </div>
                            <div id="daily_enguagement" style="height: 200px; width: 520px;"></div>
                        </div>
                        
                        <div class="publish-right">
                            <div class="key-header">
                                <h2>outbound tweet content</h2>
                            </div>
                            <div class="clear"></div>
                            <div class="tweet-count-outer">
                            	<div class="tweet-count-plain">
                                	30 <span>Plain Text</span>
                                </div>
                                <div class="tweet-count-link">
                                	0 <span>Links to Pages</span>
                                </div>
                                <div class="tweet-count-photo">
                                	0 <span>Photo Links</span>
                                </div>
                            </div>
                            
                        </div>
                        
                        <div class="clear"></div>
                    </div>  --%>
						
                        
                        
						
                        
					</div>                    
				</div>			
		</div>
        
         <script src="<%= Page.ResolveUrl("~/Contents/js/RGraph.common.core.js")%>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Contents/js/RGraph.common.key.js")%>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Contents/js/RGraph.bar.js")%>" type="text/javascript"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/Contents/js/bootstrap.min.js")%>"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/Contents/js/scripts.js")%>"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/Contents/js/canvasjs.min.js")%>"></script>
    <script src="<%= Page.ResolveUrl("~/Contents/js/Chart.js")%>" type="text/javascript"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/Contents/js/graph-scripts.js")%>"></script>
    <script src="<%= Page.ResolveUrl("~/Contents/js/reloadtwtgraph.js")%>" type="text/javascript"></script>
        <script type="text/javascript">

            $("#home").removeClass('active');
            $("#message").removeClass('active');
            $("#feeds").removeClass('active');
            $("#discovery").removeClass('active');
            $("#publishing").removeClass('active');
            $("#reports").addClass('active');


            var twtArr = "<%=strTwtArray %>".split(",");
            var twtFollowArr = "<%=strTwtFollowing %>".split(",");;
            var twtInMsgArr = "<%=strIncomingMsg %>".split(",");
            var twtSentMsgArr  = "<%=strSentMsg %>".split(",");
            var twtDMRecArr = "<%=strDmRecieve %>".split(",");
            var twtDMArrSent = "<%=strDMSent %>".split(",");
            var reTwt = "<%=strRetweet %>".split(",");
            var ageDiff = "<%=strAgeDiff %>".split(",");
            function getProfileGraph(id,name,img,follower) {
                debugger;
                $.ajax
                        ({
                            type: "GET",
                            url: "AjaxReport.aspx?op=twitter&id=" + id,
                            data: '',
                            contentType: "application/text; charset=utf-8",
                            success: function (msg) {
                                debugger;
                                $("#newfollower_graph").empty();
                                $("#newfollowed_graph").empty();
                                $("#msg_rec_graph").empty();
                                $("#msg_sent_graph").empty();
                                $("#msg_rece_graph").empty();
                                $("#dir_msg_sent_graph").empty();
                                $("#retweet_graph").empty();

                                var twtData = msg.split("@");

                                ///////////////////////
                                debugger;
                                var twtlen = twtData[0].split(',');
                                $("#<%=divnewFollower.ClientID %>").html(twtlen[twtlen.length - 1]);
                                twtlen = twtData[1].split(',');
                                $("#<%=divFollowed.ClientID %>").html(twtlen[twtlen.length - 1]);
                                twtlen = twtData[2].split(',');
                                $("#<%=divMsgReceived.ClientID %>").html(twtlen[twtlen.length - 1]);
                                twtlen = twtData[5].split(',');
                                $("#<%=divMsgSent.ClientID %>").html(twtlen[twtlen.length - 1]);
                                twtlen = twtData[3].split(',');
                                $("#<%=divdmr.ClientID %>").html(twtlen[twtlen.length - 1]);
                                twtlen = twtData[4].split(',');
                                $("#<%=divdms.ClientID %>").html(twtlen[twtlen.length - 1]);
                                twtlen = twtData[6].split(',');
                                $("#<%=divretweetCnt.ClientID %>").html(twtlen[twtlen.length - 1]);
                                reloadGraph(twtData);
                                /////////////////////////////////
                                var twtindex = twtArr[0];
                                //  $("#<%=divnewFollower.ClientID %>").html(twtArr.substring(twtindex));
                                $("#<%=divnameId.ClientID %>").html(name);
                                $("#<%=spanFollowers.ClientID %>").html(follower);
                                $("#<%=profileImg.ClientID %>").attr('src', img);
                            }
                        });
            }
            $(document).ready(function () {
                $('.togl').click(function () {
                    $(this).toggleClass("down");
                });
                getGraphData();
            });

            function getGraphData() {
                debugger;
                try {
                   
                    var items = new Array(twtArr.length);
                    items[0] = new Array(2);
                    items[0][0] = "Age";
                    items[0][1] = "Visits";
                    for (var i = 0; i < twtArr.length; i++) {
                        items[i+1] = new Array(2);
                        if (twtArr[i] != "") {
                            items[i+1][0] = i;
                            items[i+1][1] = Number(twtArr[i]);
                            $("#<%=divnewFollower.ClientID %>").html(twtArr[i]);
                        }

                    }
                   
//                    $("#newfollower_graph").sparkline([<%=strTwtArray %>], {
//                        type: 'line'
//                    });
                    getTwitterNewFollower(items);
                }
                catch (e) {
                    console.log(e);
                }
                try {
                   
                    var itemstwtFollow = new Array(twtFollowArr.length);
                    itemstwtFollow[0] = new Array(2);
                    itemstwtFollow[0][0] = "Days";
                    itemstwtFollow[0][1] = "Following";
                    for (var i = 1; i <= twtFollowArr.length; i++) {
                        itemstwtFollow[i] = new Array(2);
                        if (twtFollowArr[i] != "") {
                            itemstwtFollow[i][0] = i;
                            itemstwtFollow[i][1] = Number(twtFollowArr[i]);
                            $("#<%=divFollowed.ClientID %>").html(twtFollowArr[i]);

                        }

                    }
                    getTwitterFollowing(items);
                }
                catch (e) {
                    console.log(e);
                }
                //                getTwitterAgeWise("<%=strTwtAge %>");



                debugger;
                try{
                var itemstwtInMsg = new Array(twtInMsgArr.length);
                itemstwtInMsg[0] = new Array(2);
                itemstwtInMsg[0][0] = "Days";
                itemstwtInMsg[0][1] = "Following";
                for (var i = 1; i <= twtInMsgArr.length; i++) {
                    itemstwtInMsg[i] = new Array(2);
                    if (twtInMsgArr[i] != "") {
                        itemstwtInMsg[i][0] = i;
                        itemstwtInMsg[i][1] = Number(twtInMsgArr[i]);
                        $("#<%=divMsgReceived.ClientID %>").html(twtInMsgArr[i]);
                    }

                }
                getIncomingMsg(itemstwtInMsg);
            }
            catch (e) {
                console.log(e);
            }
               
                debugger;
                var itemstwtSentMsg = new Array(twtSentMsgArr.length);
                itemstwtSentMsg[0] = new Array(2);
                itemstwtSentMsg[0][0] = "Days";
                itemstwtSentMsg[0][1] = "Following";
                for (var i = 1; i <= twtSentMsgArr.length; i++) {
                    itemstwtSentMsg[i] = new Array(2);
                    if (twtSentMsgArr[i] != "") {
                        itemstwtSentMsg[i][0] = i;
                        itemstwtSentMsg[i][1] = Number(twtSentMsgArr[i]);
                        $("#<%=divMsgSent.ClientID %>").html(twtSentMsgArr[i]);
                    }

                }
                getSentMsg(itemstwtSentMsg);

              
                debugger;
                var itemstwtDMRec = new Array(twtDMRecArr.length);
                var dmr = 0;
                itemstwtDMRec[0] = new Array(2);
                itemstwtDMRec[0][0] = "Days";
                itemstwtDMRec[0][1] = "Following";
                for (var i = 1; i <= twtDMRecArr.length; i++) {
                    itemstwtDMRec[i] = new Array(2);
                    if (twtDMRecArr[i] != "") {
                        itemstwtDMRec[i][0] = i;
                        itemstwtDMRec[i][1] = Number(twtDMRecArr[i]);
                        dmr = Number(dmr) + Number(twtDMRecArr[i]);
                        $("#<%=divdmr.ClientID %>").html(twtDMRecArr[i]);
                    }

                }
               // $("#dmr").val(dmr);
                getDirectMessageReceive(itemstwtDMRec);


             
                debugger;
                var itemstwtDMSent = new Array(twtDMArrSent.length);
                var dmSent = 0;
                itemstwtDMSent[0] = new Array(2);
                itemstwtDMSent[0][0] = "Days";
                itemstwtDMSent[0][1] = "Following";
                for (var i = 1; i <= twtDMArrSent.length; i++) {
                    itemstwtDMSent[i] = new Array(2);
                    if (twtDMArrSent[i - 1] != "") {
                        itemstwtDMSent[i][0] = i;
                        itemstwtDMSent[i][1] = Number(twtDMArrSent[i]);
                        dmSent = Number(dmSent) + Number(twtDMArrSent[i]);
                        $("#<%=divdms.ClientID %>").html(twtDMRecArr[i]);
                    }
                }
              //  $("#dms").val(dmSent);
                getDirectMessageSent(itemstwtDMSent);


               
                var itemsretwt = new Array(reTwt.length);
                var retwt = 0;
                itemsretwt[0] = new Array(2);
                itemsretwt[0][0] = "Days";
                itemsretwt[0][1] = "Following";
                for (var i = 1; i <= reTwt.length; i++) {
                    itemsretwt[i] = new Array(2);
                    if (reTwt[i - 1] != "") {
                        itemsretwt[i][0] = i;
                        itemsretwt[i][1] = Number(reTwt[i]);
                        retwt = Number(retwt) + Number(reTwt[i]);
                        $("#<%=divretweetCnt.ClientID %>").html(reTwt[i]);
                    }
                }
                debugger;
              //  $("#retweetCnt").val(retwt);
                getRetweet(itemsretwt);

                var calTwt = "<%=strEngInf %>".split("@");
                var engTwt = calTwt[0].split(",");
                var infTwt = calTwt[1].split(",");
                var itemstwt = new Array(engTwt.length);
                var eng = 0,inf=0;
                itemstwt[0] = new Array(3);
                itemstwt[0][0] = "Days";
                itemstwt[0][1] = "Engagement";
                itemstwt[0][2] = "Influence";

                for (var i = 1; i < engTwt.length; i++) {
                    itemstwt[i] = new Array(3);
                    if (engTwt[i] != "") {
                        itemstwt[i][0] = i;
                        itemstwt[i][1] = Number(engTwt[i]);
                        itemstwt[i][2] = Number(infTwt[i]);
                        eng=eng+Number(engTwt[i]);
                        inf=inf+ Number(infTwt[i]);
                        // retwt = Number(retwt) + Number(reTwt[i])
                    }
                }
                var engPer = Math.round((eng / engTwt.length) * 100);
                var infPer = Math.round((inf / infTwt.length) * 100);
                $("#spanEng").html("ENGAGEMENT: " + engPer + " %");
                $("#spanInf").html("INFLUENCE: " + infPer + " %");
                debugger;
                //    $("#retweetCnt").val(retwt);
                getEngaegmentInfluence(itemstwt);

                var itemsage = new Array(ageDiff.length);
                for (var i = 0; i < ageDiff.length; i++) {
                    if (ageDiff[i] != "") {
                        itemsage[i] = Number(ageDiff[i]);
                    }
                }
                getTwitterAgeWise(itemsage);
            }	
</script>
<script type="text/javascript" src="https://www.google.com/jsapi"></script>

    <script type="text/javascript">
	  window.onload = function () {
    var chart = new CanvasJS.Chart("daily_enguagement",
    {
      theme: "theme2",
      title:{
        text: "",
      },
      axisX: {
        valueFormatString: "MMM",
        interval:1,
        intervalType: "month",
        
      },
      axisY:{
        includeZero: false
        
      },
      data: [
      {        
        type: "line",
        //lineThickness: 3,        
        dataPoints: [
        { x: new Date(2012, 00, 1), y: 450 },
        { x: new Date(2012, 01, 1), y: 414 },
        { x: new Date(2012, 02, 1), y: 520 },
        { x: new Date(2012, 03, 1), y: 460 },
        { x: new Date(2012, 04, 1), y: 450 },
        { x: new Date(2012, 05, 1), y: 500 },
        { x: new Date(2012, 06, 1), y: 480 },
        { x: new Date(2012, 07, 1), y: 480 },
//        { x: new Date(2012, 08, 1), y: 410 },
//        { x: new Date(2012, 09, 1), y: 500 },
        { x: new Date(2012, 10, 1), y: 480 },
        { x: new Date(2012, 11, 1), y: 510 },        
        ]
      },      
      ]
    });

chart.render();
}

    </script>

</asp:Content>
