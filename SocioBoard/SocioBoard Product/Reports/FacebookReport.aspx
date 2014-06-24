<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="~/Reports/FacebookReport.aspx.cs" Inherits="SocialSuitePro.Reports.FacebookReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="mainwrapper" class="container reports">
			<div id="sidebar">            	
				<div class="sidebar-inner">
                	<a href="GroupStats.aspx" class="btn">GROUP REPORT <img src="../Contents/img/admin/boxes.png" alt="" class="pull-right" /></a>
                	<a href="#" class="btn actives" id="facebook_page">Facebook Pages <img src="../Contents/img/admin/fbicon2.png" alt="" class="pull-right" /></a>
					<a href="TwitterReport.aspx" class="btn">Twitter Reports <img src="../Contents/img/admin/twittericon2.png" alt="" class="pull-right" /></a>
					<a href="TeamReport.aspx" class="btn">Team Report <img src="../Contents/img/admin/peoples.png" alt="" class="pull-right" /></a>
                      <%--<a class="btn" href="GoogleAnalytics.aspx">Google Analytics Report <img class="pull-right" alt="" src="../Contents/img/admin/peoples.png"></a>	--%>
					<div id="facebookbox" style="display: none;">
                        <div class="drop_top">
                        </div>
                        <div  class="drop_mid loginbox">
                            <div class="teitter">
                                <ul runat="server" id="getAllGroupsOnHome">
                                   <%-- <li><a>No Records Found</a></li>
                                     <li><a>No Records Found</a></li>
                                     <li><a>No Records Found</a></li>    --%>           
                                </ul>
                            </div>
                        </div>
                    </div>			
				</div>
				
			</div>           
            	<div id="contentcontainer-report">
                	<div id="content" style="left:0;">
						<div class="alert alert-suite-grey" style="width:882px;">
							<div class="title">
								<h1>facebook page Report</h1>
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
                               
                               <%-- <button class="btn" style="padding:2px 5px;background:none repeat scroll 0 0 #CB786F;color:#FFFFFF;text-shadow:none">Export Data <b class="caret"></b></button>--%>
                            </div>
                            <div class="grey-caret"></div>
						</div>
                        
                        <div class="rounder shadower pull-left reportcontent" style="padding-right:20px; width:895px;">
                        	
                            <div class="fb-page-profilimg"><img src="../Contents/img/avathar-1.jpg" alt="" id="fbProfileImg" runat="server"></div>
                            
                          <div class="fb-graph-right">
                                <div class="fb-graph-right-header">
                                	<div class="fb-graph-right-head-left">
                                        <h2 id="divPageName" runat="server">"Please add atleast one Facebook Fan Page..."</h2><br>
                                        <div id="divPageLikes" runat="server"></div>
                                    </div>
                                    <div class="fb-graph-right-head-right">
                                    <%--	New Fans 2.3k <span>'Unliked' Your Page  421</span>--%>
                                    </div>
                                    <div class="clear"></div>
                                </div>
                                <div class="impress-age">
                                    <div class="impression-head" style="margin-left: -75px;"><h2>impressions by age & gender</h2></div>                                	    
                                  </div>
                                <div class="fb-graph">
                                <%--	<div id="fb_combo_chart" style="width: 810px; height: 150px;"></div>--%>
                                <canvas id="cvs" width="739px" height="100">[No canvas support]</canvas>
                                </div>
                          </div>
                            <div class="clear"></div>
                            
                            <div class="impression-graph-outer">
                           	  <div class="impression-head">
                                	<h2>page impressions</h2>
                                    <div class="impress-right">
                               	  		<%--Impressions  <strong>10.8m</strong> 
                                        <span>by  2.3m  Users</span>--%>
                                    </div>
                                    <div class="clear"></div>
                                </div>
                                <div class="impres-chart"><div id="impress_chart" style="width: 865px; height: 175px;"></div></div>
                                
                            </div>
                            <%--<div class="impression-break-container">
                            	<div class="impression-break-left">
                                	<div class="impression-break-head">impressions breakdown</div>
                                    <div class="impression-break_pie-left"><div id="impress_pie1" style="width:285px; height: 175px;"></div></div>
                                    <div class="impression-break_pie-left"><div id="impress_pie2" style="width:285px; height: 175px;"></div></div>
                                    <div class="clear"></div>
                                </div>
                                <div class="impression-break-right">
                                	<div class="impression-break-head">impressionsBy Day</div>
                                    <div id="impress_daily" style="width:285px; height: 175px;"></div>
                                </div>
                                <div class="clear"></div>
                            </div>--%>
                            
                            <%--<div class="impression-break-container">
                            	<div class="impress-age">
                                	<div class="impression-break-head" style="text-align:left;">impressions by age & gender</div>
                                    <div id="chartContainer" style="height:150px; width: 100%;"></div>
                                    <div class="male-female-outer" style="padding-left:40px;">
                                	<div class="male-left">
                                    	83%<span>MALE FOLLOWERS</span>
                                    </div>
                                    <div class="female-right">
                                    	17%<span>FEMALE FOLLOWERS</span>
                                    </div>
                                    <div class="clear"></div>
                                </div>
                                    
                                </div>
                                <div class="impress-location">
                                	<div class="impression-break-head" style="text-align:left;">impressions by Location</div>
                                    <div class="top-countries-outer">
                                    	<div class="top-countries-label">Top Countries</div>
                                        <div class="country-list"><img src="../Contents/img/flag-india.jpg" alt="" /><br />2.0 m</div>
                                        <div class="country-list"><img src="../Contents/img/flag-us.jpg" alt="" /><br />69.7k</div>
                                        <div class="country-list"><img src="../Contents/img/flag-uae.jpg" alt="" /><br />23.9k</div>
                                        <div class="country-list"><img src="../Contents/img/flag-uk.jpg" alt="" /><br />21.3k</div>
                                        <div class="country-list"><img src="../Contents/img/flag-eng.jpg" alt="" /><br />18.0k</div>
                                        <div class="clear"></div>
                                    </div>
                                    <div class="location-graph"><div id="location_graph" style="width:420px; height: 175px;"></div></div>
                                </div>
                                <div class="clear"></div>
                            </div>--%>
                            
                        </div>
                        
                        <div class="alert alert-suite-grey" style="width:882px;">
							<div class="title">
								<h1>sharing</h1>
								<span class="">How people are sharing your content.</span>
							</div>
                            
                            <div class="grey-caret"></div>
						</div>
                        
                        <div class="rounder shadower pull-left reportcontent" style="padding-right:20px; width:895px;">
                        	
                            
                            
                          <div class="stories-graph-right">
                                <div class="stories-graph-head">
                                	<h2>Stories</h2>
                                    <div class="stories-graph-right-head">
                                    	<img src="../Contents/img/graph-icon.jpg" alt="" align="absmiddle" />Stories Created <strong>254.6k </strong> <span><img src="../Contents/img/graph-icon2.jpg" alt="" align="absmiddle" /> by 127.5k Users</span>
                                    </div>
                                    <div class="clear"></div>
                                </div>

                                <div class="fb-graph">
                                	<%--<div id="stories_combo_chart" style="width: 100%; height: 150px;"></div>--%>
                                     <canvas id="stories_combo_chart" width="739px" height="100">[No canvas support]</canvas>
                                </div>
                          </div>
                            <div class="clear"></div>
                            
                            
                            <div class="impression-break-container">
                            	<div class="share-pie-left">
                                	<div class="impression-break-head">share type</div>
                                    <div class="share-pie-left"><div id="share_pie" style="width:410px; height: 175px;"></div></div>
                                    <div class="clear"></div>
                                </div>
                              <%--  <div class="share-daily-right">
                                	<div class="impression-break-head">sharing by day of week</div>
                                    <div id="share_daily" style="width:395px; height: 175px;"></div>
                                </div>--%>
                                <div class="clear"></div>
                            </div>
                            
                            <div class="impression-break-container">
                            	<%--<div class="impress-age">
                                	    <div class="impression-break-head" style="text-align:left;">impressions by age & gender</div>
                                        <div id="chartContainer" style="height:150px; width: 100%;"></div>
                                        <div class="male-female-outer" style="padding-left:40px;">
                                	        <div class="male-left">
                                    	        83%<span>MALE FOLLOWERS</span>
                                            </div>
                                            <div class="female-right">
                                    	        17%<span>FEMALE FOLLOWERS</span>
                                            </div>
                                            <div class="clear"></div>
                                        </div>
                                    
                                </div>--%>
                                <%--<div class="impress-location">
                                	<div class="impression-break-head" style="text-align:left;">impressions by Location</div>
                                    <div class="top-countries-outer">
                                    	<div class="top-countries-label">Top Countries</div>
                                        <div class="country-list"><img src="../Contents/img/flag-india.jpg" alt="" /><br />2.0 m</div>
                                        <div class="country-list"><img src="../Contents/img/flag-us.jpg" alt="" /><br />69.7k</div>
                                        <div class="country-list"><img src="../Contents/img/flag-uae.jpg" alt="" /><br />23.9k</div>
                                        <div class="country-list"><img src="../Contents/img/flag-uk.jpg" alt="" /><br />21.3k</div>
                                        <div class="country-list"><img src="../Contents/img/flag-eng.jpg" alt="" /><br />18.0k</div>
                                        <div class="clear"></div>
                                    </div>
                                    <div class="location-graph"><div id="location_graph2" style="width:420px; height: 175px;"></div></div>
                                </div>--%>
                                <div class="clear"></div>
                            </div>                            
                        </div>
                        
						<div class="alert alert-suite-grey" style="width:882px;">
							<div class="title">
								<h1>your contact</h1>
								<span class="">A breakdown of the content you post</span>
							</div>
                            
                            <div class="grey-caret"></div>
						</div>
                        
                        <div class="rounder shadower pull-left reportcontent" style="padding-right:20px; width:895px;">
                        	
                                                      
                            <div class="storetype-right">
                            	<div class="store-table">                                	
                                    <div class="store-table-content">
                                    	<div class="label1">Reach</div>
                                        <div class="label2"><span>0</span></div>
                                        <div class="label3"><span></span></div>
                                        <div class="clear"></div>
                                    </div>
                                    <div class="store-table-content">
                                    	<div class="label1">People Talking About This</div>
                                        <div class="label2"><span id="spanTalking" runat="server">0</span></div>
                                        <div class="label3"><span></span></div>
                                        <div class="clear"></div>
                                    </div>
                                    <div class="store-table-content">
                                    	<div class="label1">Engagement</div>
                                        <div class="label2">%</div>
                                        <div class="label3"><span>0%</span></div>
                                        <div class="clear"></div>
                                    </div>
                                    
                                </div>
                                
                            </div>
                            
                            <div class="clear"></div>
                            
                            <div class="foot-header">Content BreakDown <span>A breakdown of how your individual post performed.</span></div>
                            <div class="message-sent-details">
                            	<div class="message-sent-header">
                                	<div class="labe-1">Message Sent</div>
                                    <div class="labe-4">Talking</div>
                                    <div class="labe-5">Likes</div>
                                    <div class="labe-6">Comments</div>
                                    <div class="labe-5">Shares</div>
                                    <div class="clear"></div>
                                </div>
                                <div id="divpost"  runat="server"></div>
                            </div>                          
                            
                        </div>                        
					</div>                    
				</div>			
		</div>
        <script type="text/javascript" src="<%= Page.ResolveUrl("~/Contents/js/canvasjs.min.js")%>"></script>
         <script src="<%= Page.ResolveUrl("~/Contents/js/RGraph.common.core.js")%>" type="text/javascript"></script>
		<script src="<%= Page.ResolveUrl("~/Contents/js/RGraph.common.key.js")%>" type="text/javascript"></script>
        <script src="<%= Page.ResolveUrl("~/Contents/js/RGraph.bar.js")%>" type="text/javascript"></script>
             <script src="<%= Page.ResolveUrl("~/Contents/js/Chart.js")%>" type="text/javascript"></script>
        <style>
			canvas{
			}
		</style> 
	<script type="text/javascript">

	    $("#home").removeClass('active');
	    $("#message").removeClass('active');
	    $("#feeds").removeClass('active');
	    $("#discovery").removeClass('active');
	    $("#publishing").removeClass('active');
	    $("#reports").addClass('active');

	    var fbAgeArr;
	    var fbImpArr;
	    var fbLocArr;
        var fbstory;
	    function getProfilefbGraph(id, name, img, access) {
	        debugger;
	        $.ajax
                        ({
                            type: "GET",
                            url: "AjaxReport.aspx?op=facebook&id=" + id+ "&access=" + access,
                            data: '',
                            contentType: "application/text; charset=utf-8",
                            success: function (msg) {
                                debugger;
                                //  alert(msg);
                                var c=document.getElementById("cvs");
                                var ctx=c.getContext("2d");
                                ctx.clearRect(0,0,739,100);
                                var fbData = msg.split("_");
                                fbAgeArr =JSON.parse(fbData[0]);
                            
                                debugger;
                                fbImpArr = fbData[1].split("@");
                                fbLocArr = fbData[2].split("@");
                                getGraphData();
                                $("#<%=divPageName.ClientID %>").html(name);                              
                                $("#<%=divPageLikes.ClientID %>").html(fbData[3]);
                                 $("#<%=fbProfileImg.ClientID %>").attr('src',img);
                                // alert(twtArr);
                            }

                        });
	    }
	    $(document).ready(function () {
	        $('.togl').click(function () {
	            $(this).toggleClass("down");
	        });
	        debugger;
	        fbAgeArr = <%=strFbAgeArray %>;
	        fbImpArr = "<%=strPageImpression %>".split("@");
	        fbLocArr = "<%=strLocationArray %>".split("@");
            fbstory="<%=strstoriesArray %>";
	        getGraphData();
	    });
	    function  getGraphData()
        {
           
	       
	        getAgeDiff(fbAgeArr);
	        debugger;
	       
	        var dateArr = fbImpArr[0].split(',');
	        var ImpArr = fbImpArr[1].split(',');
	        var itemImpression = new Array(dateArr.length-1);
	        itemImpression[0] = new Array(2);
	        itemImpression[0][0] = "Date";
	        itemImpression[0][1] = "Page Impresion";
	        for (var i = 1; i <= dateArr.length-1; i++) {
	            itemImpression[i] = new Array(2);
	            if (dateArr[i - 1] != "") {
	                itemImpression[i][0] = dateArr[i];
	                itemImpression[i][1] = Number(ImpArr[i]);
	            }

	        }
	        getImpression(itemImpression);

	       
	        debugger;
	        var locArr = fbLocArr[0].split(',');
	        var countArr = fbLocArr[1].split(',');
	        var itemLoc = new Array(locArr.length-1);
	        itemLoc[0] = new Array(2);
	        itemLoc[0][0] = "Location";
	        itemLoc[0][1] = "Count";
	        for (var i = 1; i <= locArr.length-1; i++) {
	            itemLoc[i] = new Array(2);
	            if (locArr[i - 1] != "") {
	                itemLoc[i][0] = locArr[i];
	                itemLoc[i][1] = Number(countArr[i]);
	            }

	        }
	        getLocationCount(itemLoc);

	        getStories(<%=strstoriesArray %>);
        }
</script>
<script type="text/javascript" src="https://www.google.com/jsapi"></script>
<script type="text/javascript">
    google.load('visualization', '1', { packages: ['corechart'] });
    </script>
<script type="text/javascript" src="<%= Page.ResolveUrl("~/Contents/js/graph-admin-panel.js")%>"></script>
    <script language="javascript" type="text/javascript">

    
    </script>

</asp:Content>
