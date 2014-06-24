<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true"
    CodeBehind="TeamReport.aspx.cs" Inherits="letTalkNew.Reports.TeamReport" %>

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
    <link href="../Contents/css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../Contents/css/admin.css" rel="stylesheet" type="text/css" />
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="report_width">
        <ul id="report_tap_width">
            <li><a href="GroupStats.aspx">Group Report
                <img src="../Contents/img/boxes.png"></a></li>
            <li><a href="FacebookReport.aspx">Facebook Pages
                <img src="../Contents/img/f.png"></a></li>
            <li><a href="TwitterReport.aspx">Twitter Reports
                <img src="../Contents/img/twittericon2.png"></a></li>
            <li class="report_tap_width_active"><a href="TeamReport.aspx">Team Report
                <img src="../Contents/img/peoples.png"></a></li>
            <li><a href="GoogleAnalytics.aspx">Google Analytics Report
                <img src="../Contents/img/peoples.png"></a></li>
        </ul>
        <div id="contentcontainer-report">
            <div id="content">
                <div class="alert alert-suite-grey">
                    <div class="title">
                        <h1>
                            Team Report</h1>
                        <span class="" id="spanTopDate" runat="server">from 10/6/2013-10/21/2013 </span>
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
                            <img src="../Contents/img/admin/tt.png" class="help" alt="" title="This is tooltip"/>
                                <asp:Button ID="btnfifteen" runat="server"  class="togl btn down" Text="15" onclick="btnfifteen_Click" />
                                <asp:Button ID="btnthirty" runat="server" Text="30" class="togl btn" 
                                    onclick="btnthirty_Click"/>
                                <asp:Button ID="btnsixty" runat="server" Text="60" class="togl btn" 
                                    onclick="btnsixty_Click"/>
                                <asp:Button ID="btnninty" runat="server" Text="90" class="togl btn" 
                                    onclick="btnninty_Click"/> 
                    </div>
                </div>
                <div class="rounder shadower pull-left reportcontent">
                    <div id="rep_report">
                        <h2>
                            PUBLISHING</h2>
                        <div id="re_publishing">
                            <img src="../Contents/img/blank_img.png">
                            <h2 id="divName" runat="server">
                                Praveen</h2>
                            <span>0.00 Daily avg</span>
                            <div id="repliescount" runat="server">
                                0 REPLIES <strong>/ 0TOTAL POSTS </strong>
                            </div>
                            <div>
                                <!-- DIV FOR THE PAGE IMPRESSION CHART HERE BY BOOPATHY -->
                                <div class="rounder shadower pull-left reportcontent">
                                    <div id="chartContainer" style="height: 200px; width: 100%;">
                                    </div>
                                </div>
                                <!-- DIV FOR THE PAGE IMPRESSION CHART HERE BY BOOPATHY -->
                            </div>
                        </div>
                    </div>
                </div>
                <div class="rounder shadower pull-left reportcontent">
                 <div class="task-list-outer" id="taskdiv" runat="server">
                    <div id="rep_report">
                        <h2>
                            TASKS <span id="spanDate" runat="server">
                                <%--<img src="../Contents/img/blank_img.png">--%> from 10/6/2013-10/21/2013</span></h2>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="task1">
                            <tr>
                                <td>
                                    Task Owner
                                </td>
                                <td>
                                    Assigned
                                </td>
                                <td>
                                    TaskMessage
                                </td>
                                <td>
                                    Assign Date
                                </td>
                                <td>
                                    Completion Date
                                </td>
                                <td>
                                    Status
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
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
    <script>

        var chart = new CanvasJS.Chart("chartContainer",
	{
	    zoomEnabled: false,

	    axisY2: {
	        valueFormatString: "0.0 %",

	        maximum: 1.0,
	        interval: .2,
	        interlacedColor: "#D6D6D6",
	        gridColor: "#CBD5F8",
	        tickColor: "#C7DDF8"
	    },
	    theme: "theme2",
	    legend: {
	        verticalAlign: "bottom",
	        horizontalAlign: "center",
	        fontSize: 15,
	        fontFamily: "Lucida Sans Unicode"

	    },
	    data: [
		{
		    type: "line",
		    lineThickness: 3,
		    axisYType: "secondary",
		    showInLegend: true,         
		    name: "Posts",
		    dataPoints: <%=postGraphArr %>
//            [
//			{ x: new Date(2001, 0), y: 0 },
//			{ x: new Date(2002, 0), y: 0.001 },
//			{ x: new Date(2003, 0), y: 0.01 },
//			{ x: new Date(2004, 0), y: 0.05 },
//			{ x: new Date(2005, 0), y: 0.1 },
//			{ x: new Date(2006, 0), y: 0.98 },
//			{ x: new Date(2007, 0), y: 0.22 },
//			{ x: new Date(2008, 0), y: 0.38 },
//			{ x: new Date(2009, 0), y: 0.56 },
//			{ x: new Date(2010, 0), y: 0.98 },
//			{ x: new Date(2011, 0), y: 0.91 },
//			{ x: new Date(2012, 0), y: 0.94 }


//			]
		},
		{
		    type: "line",
		    lineThickness: 3,
		    showInLegend: true,
		    name: "Replies",
		    axisYType: "secondary",
		    dataPoints: <%=replyGraphArr %>
//            [
//			{ x: new Date(2001, 00), y: 0.18 },
//			{ x: new Date(2002, 00), y: 0.2 },
//			{ x: new Date(2003, 0), y: 0.25 },
//			{ x: new Date(2004, 0), y: 0.35 },
//			{ x: new Date(2005, 0), y: 0.42 },
//			{ x: new Date(2006, 0), y: 0.2 },
//			{ x: new Date(2007, 0), y: 0.58 },
//			{ x: new Date(2008, 0), y: 0.67 },
//			{ x: new Date(2009, 0), y: 0.78 },
//			{ x: new Date(2010, 0), y: 0.88 },
//			{ x: new Date(2011, 0), y: 0.98 },
//			{ x: new Date(2012, 0), y: 0.04 },
//			{ x: new Date(2012, 0), y: 0.30 }



//			]
		},
		]
	});

        chart.render();
    </script>
</asp:Content>
