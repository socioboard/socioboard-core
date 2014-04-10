<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="GroupStats.aspx.cs" Inherits="SocialSuitePro.Reports.GroupStats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <link href="<%= Page.ResolveUrl("~/Contents/css/bootstrap.min.css")%>" rel="stylesheet">
    <link href="<%= Page.ResolveUrl("~/Contents/css/admin.css")%>" rel="stylesheet">
    <!--[if IE 7]>
			<link href="../Contents/css/font-awesome-ie7.min.css" rel="stylesheet">
		<![endif]-->
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
			<script src="../Contents/js/html5shiv.js"></script>
		<![endif]-->
    <script src="<%= Page.ResolveUrl("~/Contents/js/Chart.js")%>" type="text/javascript"></script>
    <style type="text/css">
        .graypaination
        {
            width: auto;
            float: right;
            margin-top: 17px;
        }
        .knob
        {
            font-size: 20px !important;
            font-weight: bold;
            height: 50px;
            margin-left: -51px !important;
            margin-top: -39px !important;
            position: inherit !important;
            text-align: center;
            top: 17px;
            width: 50px !important;
        }
        .graph > div > canvas
        {
            width: 50px !important;
            height: 50px;
        }
    </style>
    <div id="mainwrapper" class="container reports">
        <div id="sidebar">
            <div class="sidebar-inner">
                <a href="#" class="btn actives">GROUP REPORT
                    <img src="../Contents/img/admin/boxes.png" alt="" class="pull-right" /></a>
                <a href="FacebookReport.aspx" class="btn">Facebook Pages
                    <img src="../Contents/img/admin/fbicon2.png" alt="" class="pull-right" /></a>
                <a href="TwitterReport.aspx" class="btn">Twitter Reports
                    <img src="../Contents/img/admin/twittericon2.png" alt="" class="pull-right" /></a>
                <a href="TeamReport.aspx" class="btn">Team Report
                    <img src="../Contents/img/admin/peoples.png" alt="" class="pull-right" /></a>
                     <%-- <a class="btn" href="GoogleAnalytics.aspx">Google Analytics Report <img class="pull-right" alt="" src="../Contents/img/admin/peoples.png"></a>	--%>
                <%--	<a href="#" class="btn">Twitter Comparison <img src="../Contents/img/admin/loopback.png" alt="" class="pull-right" /></a>
					<a href="#" class="btn">Sent Message <img src="../Contents/img/admin/bar-chart.png" alt="" class="pull-right" /></a>				--%>
            </div>
        </div>
        <div id="contentcontainer-report">
            <div id="content" style="margin-left: -115px; width: 855px;">
                <%--<div class="alert alert-suite-grey">
							<div class="title">
								<h1>Inter</h1>
								<span class="">from Apr. 15, 2013 - Apr. 29, 2013</span>
							</div>
                            <div id="exportdt" class="pull-right">
                            	<img src="../Contents/img/admin/tt.png" class="help" alt="" title="This is tooltip"/>
                                <button class="togl btn down">15</button>
                                <button class="togl btn">30</button>
                                <button class="togl btn">60</button>
                                <button class="togl btn">90</button>
                                <button class="togl btn"><img src="../Contents/img/admin/add.png" alt="" style="margin-top:-5px"/></button>
                                <button class="btn" style="padding:2px 5px;background:none repeat scroll 0 0 #CB786F;color:#FFFFFF;text-shadow:none">Export Data <b class="caret"></b></button>
                            </div>
                            <div class="grey-caret"></div>
						</div>--%>
                <%--<div class="rounder shadower pull-left reportcontent">
                           
                        <form name="" action="#">
                        	<label class="pull-left"><input type="checkbox"> All Accounts</label>
							<label class="pull-left"><input type="checkbox"> Customize Report</label>
                        </form>
                        </div>--%>
                <div class="alert alert-suite-grey">
                    <div class="title">
                        <h1>
                            Group Stats</h1>
                        <span class="">General stats across your Inter group</span>
                    </div>
                    <div class="graypaination">
                        <asp:Button ID="btnfifteen" runat="server" class="togl btn down" Text="15" OnClick="btnfifteen_Click" />
                        <asp:Button ID="btnthirty" runat="server" Text="30" class="togl btn" OnClick="btnthirty_Click" />
                        <asp:Button ID="btnsixty" runat="server" Text="60" class="togl btn" OnClick="btnsixty_Click" />
                        <asp:Button ID="btnninty" runat="server" Text="90" class="togl btn" OnClick="btnninty_Click" />
                    </div>
                </div>
                <div class="rounder shadower pull-left reportcontent">
                    <div class="row-fluid">
                        <div class="span7">
                            <ul class="groupstat">
                                <li>Incoming Messages
                                    <div class="pull-right small-graph-out">
                                        <div class="counterbg">
                                            <span id="spanIncoming" runat="server">10</span></div>
                                        <canvas id="bar_1" height="50" width="90" style="float: right;">[No canvas support]</canvas>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </li>
                                <li>Sent Message
                                    <div class="pull-right small-graph-out">
                                        <div class="counterbg">
                                            <span id="spanSent" runat="server">10</span></div>
                                        <canvas id="bar_2" height="50" width="90"></canvas>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </li>
                                <li>New Twitter Followers
                                    <div class="pull-right small-graph-out">
                                        <div class="counterbg">
                                            <span id="spanTwtFollowers" runat="server">10</span></div>
                                        <canvas id="div_twt" height="50" width="90"></canvas>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </li>
                                <li>New Facebook Fans
                                    <div class="pull-right small-graph-out">
                                        <div class="counterbg">
                                            <span id="spanFbFriends" runat="server">10</span></div>
                                        <canvas id="chart_div" height="50" width="90"></canvas>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </li>
                            </ul>
                        </div>
                        <div class="span5">
                            <div class="pull-right">
                                <%--<img src="../Contents/img/admin/groupstats.png" alt=""/>--%>
                                <ul class="graphdiv">
                                    <li class="maingraph">
                                        <div class="contents">
                                            <span class="value">
                                                <%--0--%></span> <span class="valinstraction">INTERACTIONS</span>
                                        </div>
                                        <div class="graph">
                                            <input style="width: 50px; height: 50px" class="knob" data-fgcolor="#f4ae40" data-thickness=".2"
                                                data-readonly="true" value="<%=pagelikes %>" /></div>
                                    </li>
                                    <li class="maingraph">
                                        <div class="contents">
                                            <span class="valinstraction"></span> <span class="value">
                                                <%--0--%></span> <span class="valinstraction">PROFILES BY USERS</span>
                                        </div>
                                        <div class="graph">
                                            <input class="knob" data-fgcolor="#f4ae40" data-thickness=".2" data-readonly="true"
                                                value="<%=profileCount %>" /></div>
                                    </li>
                                    <li class="maingraph">
                                        <div class="contents">
                                            <span class="value">
                                                <%--144--%></span> <span class="valinstraction">IMPRESSIONS</span>
                                        </div>
                                        <div class="graph">
                                            <input class="knob" data-fgcolor="#f4ae40" data-thickness=".2" data-readonly="true"
                                                value="<%=talkingabtcount %>" />
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="ttr">
                        <a href="#">
                            <img src="../Contents/img/admin/ttred.png" alt="" /></a></div>
                </div>
                <div class="alert alert-suite-grey">
                    <div class="title">
                        <h1>
                            Engagement Report</h1>
                        <span class="">These metrics are based on replies sent from profiles within the '' group</span>
                    </div>
                    <div class="grey-caret">
                    </div>
                </div>
                <div class="rounder shadower pull-left reportcontent">
                    <div class="row-fluid">
                        <div class="span8">
                            <div id="chartContainer" style="height: 272px; width: 531px;">
                            </div>
                        </div>
                        <div class="span4" style="text-align: center">
                            <%--  <h5>YOUR RANK IN THE</h5>
                                	<img src="../Contents/img/admin/19.png" alt=""/>
	                                <h5>FOR RESPONSE RATE</h5>  --%>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span8">
                            <canvas id="canvas" height="275" width="531"></canvas>
                        </div>
                        <div class="span4" style="text-align: center">
                            <%--	<img src="../Contents/img/admin/estimateinbound.png" alt=""/>       --%>
                        </div>
                    </div>
                    <div class="ttr">
                        <a href="#">
                            <img src="../Contents/img/admin/ttred.png" alt="" /></a></div>
                </div>
                <div class="alert alert-suite-grey">
                    <div class="title">
                        <h1>
                            Twitter Stats</h1>
                        <span class="">Stats for 2 Twitter accounts in the Inter group</span>
                    </div>
                    <div class="grey-caret">
                    </div>
                </div>
                <div class="rounder shadower pull-left reportcontent">
                    <div class="row-fluid">
                        <div class="span6">
                            <h5>
                                Follower Demographics</h5>
                            <div class="row-fluid">
                                <div class="span6" style="width: 47.936%;">
                                    <img class="pull-left" src="../Contents/img/admin/24.png" alt="" />
                                    <div class="pull-left" style="margin-left: 10px">
                                        <h2 id="divtwtMale" runat="server">
                                            75%</h2>
                                        <br />
                                        <span>Male
                                            <br />
                                            Followers</span>
                                    </div>
                                </div>
                                <div class="span6" style="width: 47.936%;">
                                    <img class="pull-left" src="../Contents/img/admin/25.png" alt="" />
                                    <div class="pull-left" style="margin-left: 10px">
                                        <h2 id="divtwtfeMale" runat="server">
                                            25%</h2>
                                        <br />
                                        <span>Female
                                            <br />
                                            Followers</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="span6">
                            <h5>
                                Twitter Stats</h5>
                            <img src="../Contents/img/admin/23.png" alt="">
                            <h2 id="hTwtFollowers" runat="server">
                                3</h2>
                            <br />
                            New followers in this time period.
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span6">
                            <canvas id="bar_5" height="150" width="350"></canvas>
                        </div>
                        <div class="span6">
                            <div class="row-fluid">
                                <div class="span4">
                                    <img src="../Contents/img/admin/26.png" alt="" />
                                    <h2 id="hmsgsent" runat="server">
                                        0</h2>
                                    <br />
                                    Message Sent
                                </div>
                                <div class="span4">
                                    <img src="../Contents/img/admin/28.png" alt="" />
                                    <h2 id="hmention" runat="server">
                                        0</h2>
                                    <br />
                                    Mentioned
                                </div>
                                <div class="span4">
                                    <img src="../Contents/img/admin/27.png" alt="" />
                                    <h2 id="hretweet" runat="server">
                                        0</h2>
                                    <br />
                                    Retweets
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span7">
                           <%-- <h5>
                                Follower Demographics</h5>
                            <canvas id="line_2" height="50" width="400"></canvas>--%>
                            <!-- <ul style="margin:0px;list-style:none">
                                    <li class="pull-left" style="margin-right:40px">April 17</li>
                                    <li class="pull-left" style="margin-right:40px">April 20</li>
                                    <li class="pull-left" style="margin-right:40px">April 24</li>
                                    <li class="pull-left" style="margin-right:40px">April 27</li>
                                    <li class="pull-left" style="margin-right:40px">Mei 03</li>
                                </ul>-->
                        </div>
                        <%--           <div class="span5">
                                	<h5>Outbound Tweet Content</h5>
                                    <ul class="groupstat">
                                    	<li><img src="../Contents/img/admin/29.png" alt=""/><h2 style="margin-right:10px;color:#cb786f;width:50px;display:inline-block">20</h2><span>Plain Text</span></li>
                                    	<li><img src="../Contents/img/admin/30.png" alt=""/><h2 style="margin-right:10px;color:#cb786f;width:50px;display:inline-block">0</h2>Links to Pages</li>
                                    	<li><img src="../Contents/img/admin/31.png" alt=""/><h2 style="margin-right:10px;color:#cb786f;width:50px;display:inline-block">0</h2>Photo Links</li>                                        
                                    </ul>
                                </div>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="<%= Page.ResolveUrl("~/Contents/js/RGraph.common.core.js")%>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Contents/js/RGraph.common.key.js")%>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Contents/js/RGraph.bar.js")%>" type="text/javascript"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <%--<script type="text/javascript" src="<%= Page.ResolveUrl("~/Contents/js/bootstrap.min.js")%>"></script>--%>
    <script type="text/javascript" src="../Contents/js/scripts.js"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/Contents/js/canvasjs.min.js")%>"></script>
    <script type="text/javascript">

        $("#home").removeClass('active');
        $("#message").removeClass('active');
        $("#feeds").removeClass('active');
        $("#discovery").removeClass('active');
        $("#publishing").removeClass('active');
        $("#reports").addClass('active');
        $(document).ready(function () {
            $('.togl').click(function () {
                $(this).toggleClass("down");
            });
        });			
    </script>
    <script>
 google.load("visualization", "1", {packages:["corechart"]});
 $(document).ready(function () {
  debugger;
            var FollowerMonth=<%=strFollowerMonth %>;
            var FollowingMonth=<%=strFollowingMonth %>;
            var mon=<%=strMonth %>;
		var lineChartData = {
			labels : mon,
			datasets : [
				{
					fillColor : "rgba(177,88,84,0.9)",
					strokeColor : "rgba(177,88,84,1)",
					pointColor : "rgba(182,85,116,1)",
					pointStrokeColor : "#fff",
					data : FollowerMonth
				},
				{
					fillColor : "rgba(150,150,150,0.9)",
					strokeColor : "rgba(150,150,150,1)",
					pointColor : "rgba(156,156,156,1)",
					pointStrokeColor : "#fff",
					data : FollowingMonth
				}
			]
		}
	var myLine = new Chart(document.getElementById("canvas").getContext("2d")).Line(lineChartData);

	  var twtArr=<%=strTwtArray %>;
      debugger;
	var barChartTwtData = {
			labels : ["1","2","3","4","5"],
			datasets : [
				{
					fillColor : "rgba(167,167,167,1)",
					strokeColor : "rgba(167,167,167,0)",
					data : twtArr
				}
			]
		}
	var myLine = new Chart(document.getElementById("div_twt").getContext("2d")).Bar(barChartTwtData);

//    	var lineChartdemoData = {
//			labels : ["January","February","March","April","May"],
//			datasets : [
//				{
//					fillColor : "rgba(115,181,186,0.9)",
//					strokeColor : "rgba(115,181,186,1)",
//					pointColor : "rgba(115,181,186,1)",
//					pointStrokeColor : "#fff",
//					data : [15,25,15,25,15]
//				}
//			]
//		}
//	var myLineDemo = new Chart(document.getElementById("line_2").getContext("2d")).Line(lineChartdemoData);



     var fbArr=<%=strFBArray %>;
      debugger;
	var barChartfbData = {
			labels : ["1","2","3","4","5"],
			datasets : [
				{
					fillColor : "rgba(167,167,167,1)",
					strokeColor : "rgba(167,167,167,0)",
					data : fbArr
				}
			]
		}
	var myLine = new Chart(document.getElementById("chart_div").getContext("2d")).Bar(barChartfbData);


	  var arrData=<%=strArray %>;
          var bar = new RGraph.Bar('bar_1',arrData)
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
       var barSent = new RGraph.Bar('bar_2', arrSentData)
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

//	var barChartData = {
//			labels : ["Jan","Feb","Mar","Apr","May","June","July","Aug", "Sep", "Oct", "Nov", "Dec"],
//			datasets : [
//				{
//					fillColor : "rgba(178,178,178,1)",
//					strokeColor : "rgba(178,178,178,0)",
//					data : [25,19,60,81,56,55,70,30,51,56,35,95 ]
//				}
//			]
//			
//		}

//	var myLine = new Chart(document.getElementById("bar_4").getContext("2d")).Bar(barChartData);
//	
debugger;
var twtAgeArray=<%=strTwtAgeArray %>;
	var barChartData = {
			labels : ["18-20","21-24","25-34","35-44","45-54","55-64","65+"],
			datasets : [
				{
					fillColor : "rgba(178,178,178,1)",
					strokeColor : "rgba(178,178,178,0)",
					data : twtAgeArray
				}
			]
			
		}

	var myLine = new Chart(document.getElementById("bar_5").getContext("2d")).Bars(barChartData);
	
	var eng=<%=strEng %>;
	window.onload = function () {
    debugger;
    var chart = new CanvasJS.Chart("chartContainer",
    {
      theme: "theme2",
      title:{
        text: "Engagemnet"
      },
      axisX: {
        valueFormatString: "MMM",
        interval:1,
        intervalType: "day"
        
      },
      axisY:{
        includeZero: true
        
      },
      data: [
      {        
        type: "line",
        //lineThickness: 3,        
        dataPoints: [eng]
      },
      
      
      ]
    });

chart.render();
}
	});   
    </script>
    <script src="<%= Page.ResolveUrl("~/Contents/js/jquery.knob.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //$(".knob").knob();
            $(".knob").knob(
                    {
                        'change': function (e) {
                            console.log(e);
                        }
                    }
                )
            //.val(79)
                ;
        });
    </script>
</asp:Content>
