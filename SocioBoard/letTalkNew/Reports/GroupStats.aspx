<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true"
    CodeBehind="GroupStats.aspx.cs" Inherits="letTalkNew.Reports.GroupStats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- SCRIPT LIBRARY FILE FOR THE CHART BY BOOPATHY-->
    <script type="text/javascript" src="../Contents/js/canvasjs.min.js"></script>
    <!-- SCRIPT LIBRARY FILE FOR THE CHART BY BOOPATHY-->
    <script>
        (function () { if (!/*@cc_on!@*/0) return; var e = "abbr,article,aside,audio,bb,canvas,datagrid,datalist,details,dialog,eventsource,figure,footer,header,hgroup,mark,menu,meter,nav,output,progress,section,time,video".split(','), i = e.length; while (i--) { document.createElement(e[i]) } })()
    </script>
    <!--[if lt Ie 9]>
        <script src="js/html5.js" type="text/javascript"></script>
    <![endif]-->
    <link href="../Contents/css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Contents/css/grid.css" rel="stylesheet" type="text/css" />
    <link href="../Contents/css/admin.css" rel="stylesheet" type="text/css" />
    <link href="../Contents/css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../Contents/css/download.css" rel="stylesheet" type="text/css" />
    <!--[if Ie 7]>
        <style type="text/css">
            nav > .menu > ul { width:610px; margin:0 auto;}
            nav > .menu > ul li {float:left;}
            .cirlce_chart{position:relative;}
            .cirlce_chart > .value{margin-top:35px; margin-left:15px; width:80px; text-align:center;}
            li{float:left;}
            section > .main > .section_top > .container_right > .graph > .social_graph ul > li > .social_activity_links > ul > li > .tabs > .scl_activity_links{margin-left:0;}
        </style>
    <![endif]-->
    <!--[if Ie 8]>
        <style type="text/css">            
            .cirlce_chart{position:relative;}
            .cirlce_chart > .value{margin-top:35px; margin-left:15px; width:80px; text-align:center;}
        </style>
    <![endif]-->
    <script src="../Contents/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('.menu ul li a').click(function () {
                $('.menu ul li a').removeClass('active');
                $(this).addClass('active');
            });


            $('.mailbox_bot ul li').click(function () {
                $('.mailbox_bot ul li').removeClass('active');
                $(this).addClass('active');
            });
        });

    </script>
    <script type="text/javascript">

        $(function () {

            var indicator = $('#indicator'),
					indicatorHalfWidth = indicator.width() / 2,
					lis = $('#tabs').children('li');

            $("#tabs").tabs("#content section", {
                effect: 'fade',
                fadeOutSpeed: 0,
                fadeInSpeed: 400,
                onBeforeClick: function (event, index) {
                    var li = lis.eq(index),
					    newPos = li.position().left + (li.width() / 2) - indicatorHalfWidth;
                    indicator.stop(true).animate({ left: newPos }, 600, 'easeInOutExpo');
                }
            });

        });

    </script>
    <!-- stylesheets -->
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="../Contents/js/jquery-1.6.min.js"></script>
    <script type="text/javascript" src="../Contents/js/jquery.reveal.js"></script>
    <!-- javascript -->
    <script type="text/javascript" src="../Contents/js/jquery.min.js"></script>
    <script type="text/javascript" src="../Contents/js/jquery.tools.min.js"></script>
    <script type="text/javascript" src="../Contents/js/jquery.easing.1.3.js"></script>
    <!--[if lt IE9]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->
    <script type="text/ecmascript">
        $(document).ready(function () {
            $('.quarter').click(function () {
                $(this).parent().prev().children('span').css('width', '25%');
            });
            $('.half').click(function () {
                $(this).parent().prev().children('span').css('width', '50%');
            });
            $('.three-quarters').click(function () {
                $(this).parent().prev().children('span').css('width', '75%');
            });
            $('.full').click(function () {
                $(this).parent().prev().children('span').css('width', '100%');
            });
        });
    </script>
    <script src="../Contents/js/jquery.knob.js" type="text/javascript"></script>
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
                                            , 'stopper': false
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

        });
    </script>
    <link rel="stylesheet" type="text/css" href="../Contents/css/common.css">
    <link rel="stylesheet" type="text/css" href="../Contents/css/mopTip-2.2.css">
    <script type="text/javascript" src="../Contents/js/mopTip-2.2.js"></script>
    <script type="text/javascript" src="../Contents/js/jquery.pngFix-1.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#demo1Btn").mopTip({ 'w': 150, 'style': "overOut", 'get': "#demo1" });
            $("#demo2Btn").mopTip({ 'w': 150, 'style': "overClick", 'get': "#demo2" });
            $("#demo3Btn").mopTip({ 'w': 150, 'style': "overOut", 'get': "#demo3" });
            $("#demo4Btn").mopTip({ 'w': 150, 'style': "overClick", 'get': "#demo4" });
            $("#demo5Btn").mopTip({ 'w': 150, 'style': "overOut", 'get': "#demo5" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="report_width">
       <ul id="report_tap_width">
            <li class="report_tap_width_active"><a href="GroupStats.aspx">Group Report
                <img src="../Contents/img/boxes.png"></a></li>
            <li><a href="FacebookReport.aspx">Facebook Pages
                <img src="../Contents/img/f.png"></a></li>
            <li><a href="TwitterReport.aspx">Twitter Reports
                <img src="../Contents/img/twittericon2.png"></a></li>
            <li><a href="TeamReport.aspx">Team Report
                <img src="../Contents/img/peoples.png"></a></li>
            <li><a href="GoogleAnalytics.aspx">Google Analytics Report
                <img src="../Contents/img/peoples.png"></a></li>
        </ul>
        <div id="contentcontainer-report">
            <div id="content">
                <div class="alert alert-suite-grey">
                    <div class="title">
                        <h1>
                            Group Stats</h1>
                        <span class="">General stats across your Inter group</span>
                    </div>
                    <div class="graypaination">
                       <%-- <input id="ContentPlaceHolder1_btnfifteen" class="togl btn down" type="submit" value="15"
                            name="ctl00$ContentPlaceHolder1$btnfifteen">
                        <input id="ContentPlaceHolder1_btnthirty" class="togl btn" type="submit" value="30"
                            name="ctl00$ContentPlaceHolder1$btnthirty">
                        <input id="ContentPlaceHolder1_btnsixty" class="togl btn" type="submit" value="60"
                            name="ctl00$ContentPlaceHolder1$btnsixty">
                        <input id="ContentPlaceHolder1_btnninty" class="togl btn" type="submit" value="90"
                            name="ctl00$ContentPlaceHolder1$btnninty">--%>
                        <asp:Button ID="btnfifteen" runat="server" class="togl btn down" Text="15" OnClick="btnfifteen_Click" />
                        <asp:Button ID="btnthirty" runat="server" Text="30" class="togl btn" OnClick="btnthirty_Click" />
                        <asp:Button ID="btnsixty" runat="server" Text="60" class="togl btn" OnClick="btnsixty_Click" />
                        <asp:Button ID="btnninty" runat="server" Text="90" class="togl btn" OnClick="btnninty_Click" />
                    </div>
                </div>
                <div class="rounder shadower pull-left reportcontent">
                    <div id="rep_report">
                        <div id="group_re1_left">
                            <li>
                                <label>
                                    Incoming Messages
                                </label>
                                <span id="spanIncoming" runat="server">0</span>
                                <img src="../Contents/img/canvas.png">
                            </li>
                            <li>
                                <label>
                                    Sent Message
                                </label>
                                <span id="spanSent" runat="server">0</span>
                                <img src="../Contents/img/canvas.png">
                            </li>
                            <li>
                                <label>
                                    New Twitter Followers
                                </label>
                                <span id="spanTwtFollowers" runat="server">0</span>
                                <img src="../Contents/img/canvas.png">
                            </li>
                            <li>
                                <label>
                                    New Facebook Fans
                                </label>
                                <span id="spanFbFriends" runat="server">0</span>
                                <img src="../Contents/img/canvas.png">
                            </li>
                        </div>
                        <div id="gr_status">
                            <li><span>INTERACTIONS </span>
                                <div class="graph">
                                    <input style="width: 50px; height: 50px" class="knob" data-fgcolor="#f4ae40" data-thickness=".2"
                                        data-readonly="true" value="<%=pagelikes %>" /></div>
                            </li>
                            <li><span>PROFILES BY USERS </span>
                                <div class="graph">
                                    <input style="width: 50px; height: 50px" class="knob" data-fgcolor="#f4ae40" data-thickness=".2"
                                        data-readonly="true" value="<%=profileCount %>" /></div>
                            </li>
                            <li><span>IMPRESSIONS </span>
                                <div class="graph">
                                    <input style="width: 50px; height: 50px" class="knob" data-fgcolor="#f4ae40" data-thickness=".2"
                                        data-readonly="true" value="<%=talkingabtcount %>" /></div>
                            </li>
                        </div>
                    </div>
                </div>
                <div class="alert alert-suite-grey">
                    <div class="title">
                        <h1>
                            sharing</h1>
                        <span class="">How people are sharing your content.</span>
                    </div>
                </div>
                <div class="rounder shadower pull-left reportcontent">
                    <!-- div FOR THE PAGE IMPRESSION CHART HERE BY BOOPATHY -->
                    <div id="chartContainer2" style="float: left; height: 300px; padding: 0 151px 0 0;
                        width: 400px; position: relative;">
                    </div>
                    <!-- div FOR THE PAGE IMPRESSION CHART HERE BY BOOPATHY -->
                </div>
                <div class="alert alert-suite-grey">
                    <div class="title">
                        <h1>
                            Engagement Report</h1>
                        <span class="">These metrics are based on replies sent from profiles within the '' group</span>
                    </div>
                </div>
                <div class="rounder shadower pull-left reportcontent">
                    <div class="row-fluid">
                        <div class="span6">
                            <h5>
                                Follower Demographics</h5>
                            <div class="row-fluid">
                                <div style="width: 47.936%;" class="span6">
                                    <img alt="" src="../Contents/img/24.png" class="pull-left">
                                    <div style="margin-left: 10px" class="pull-left">
                                        <h2 id="divtwtMale" runat="server">
                                            33%</h2>
                                        <br>
                                        <span>Male
                                            <br>
                                            Followers</span>
                                    </div>
                                    <div style="width: 300px;">
                                        <div id="chartContainer1" style="height: 300px; width: 100%;">
                                        </div>
                                    </div>
                                </div>
                                <div style="width: 47.936%;" class="span6">
                                    <img alt="" src="../Contents/img/25.png" class="pull-left">
                                    <div style="margin-left: 10px" class="pull-left">
                                        <h2 id="divtwtfeMale" runat="server">
                                            67%</h2>
                                        <br>
                                        <span style="color: #C24642;">Female
                                            <br>
                                            Followers</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="span6">
                            <h5>
                                Twitter Stats</h5>
                            <img alt="" src="../Contents/img/23.png">
                            <h2 id="hTwtFollowers" runat="server">
                                3</h2>
                            <br>
                            Stats for 2 Twitter accounts in the Inter group
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span6">
                            <canvas width="350" height="150" id="bar_5" style="width: 350px; height: 150px;"></canvas>
                        </div>
                        <div class="span7" style="float: right">
                            <div class="row-fluid">
                                <div class="span4">
                                    <img alt="" src="../Contents/img/26.png">
                                    <h2 id="hmsgsent" runat="server">
                                        1</h2>
                                    <br>
                                    Message Sent
                                </div>
                                <div class="span4">
                                    <img alt="" src="../Contents/img/28.png">
                                    <h2 id="hmention" runat="server">
                                        0</h2>
                                    <br>
                                    Mentioned
                                </div>
                                <div class="span4">
                                    <img alt="" src="../Contents/img/27.png">
                                    <h2 id="hretweet" runat="server">
                                        0</h2>
                                    <br>
                                    Retweets
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span7">
                            <!-- <ul style="margin:0px;list-style:none">
                                    <li class="pull-left" style="margin-right:40px">April 17</li>
                                    <li class="pull-left" style="margin-right:40px">April 20</li>
                                    <li class="pull-left" style="margin-right:40px">April 24</li>
                                    <li class="pull-left" style="margin-right:40px">April 27</li>
                                    <li class="pull-left" style="margin-right:40px">Mei 03</li>
                                </ul>-->
                        </div>
                    </div>
                </div>
            </div>
            <!--end container_right-->
        </div>
        <!--end section_top-->
        <!-- graph chart script here-->
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
        </script>
    </div>
    <script type="text/javascript">
        var twtfemale=<%=twtfemale %>;
        var twtmale=<%=twtmale %>;
       var fbtwtmsgcount=<%=fbtwtmsgcount %>;
          debugger;
        var chart_new = new CanvasJS.Chart("chartContainer2",
	{

	    axisY: {
	        title: "Replies"
	    },
	    legend: {
	        verticalAlign: "bottom",
	        horizontalAlign: "center"
	    },
	    theme: "theme2",
	    data: [

		{
		    type: "column",
		    showInLegend: true,
		    legendMarkerColor: "grey",
		    legendText: "Posts",
		    dataPoints:fbtwtmsgcount
            // [
//		{ y: 297571, label: "10-20" },
//		{ y: 267017, label: "20-30" },
//		{ y: 175200, label: "30-40" },
//		{ y: 154580, label: "40-50" },
//		{ y: 116000, label: "50-60" },
//		{ y: 97800, label: "60-70" },
//		{ y: 20682, label: "80-90" },
//		{ y: 20350, label: "90 +" }
//		]
		}
		]
	});
        chart_new.render();
        
        var chart1 = new CanvasJS.Chart("chartContainer1", {
            data: [{
                type: "pie",
                dataPoints: [
				{ y: twtfemale, color: "#369EAD" },
				{ y: twtmale, color: "#C24642" },
			]
            }]
        });
        chart1.render();


    </script>
</asp:Content>
