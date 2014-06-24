<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true"
    CodeBehind="FacebookReport.aspx.cs" Inherits="letTalkNew.Reports.FacebookReport" %>

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
            $('#fbpage').click(function () {
                $('#facebookbox').slideToggle();
            });
        });

    </script>
    <link rel="stylesheet" type="text/css" href="css/common.css">
    <link rel="stylesheet" type="text/css" href="css/mopTip-2.2.css">
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
    <!--[if IE]>
    <script type="text/javascript" src="../excanvas.js"></script>
    <![endif]-->
    <!--
    For production (minified) code, use:
    <script type="text/javascript" src="dygraph-combined.js"></script>
    -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="report_width">
        <ul id="report_tap_width">
            <li><a href="GroupStats.aspx">Group Report
                <img src="../Contents/img/boxes.png"></a></li>
            <li class="report_tap_width_active">
                <a id="fbpage">Facebook Pages<img src="../Contents/img/f.png" alt="" /></a>
                <div id="facebookbox">
                        <div class="drop_top">
                        </div>
                        <div  class="drop_mid loginbox">
                            <div class="teitter">
                                <ul runat="server" id="getAllGroupsOnHome">
                                    <%--<li><a>No Records Found</a></li>
                                     <li><a>No Records Found</a></li>
                                     <li><a>No Records Found</a></li>--%>               
                                </ul>
                            </div>
                        </div>
                    </div>		
                </li>
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
                            facebook page Report</h1>
                        <span class="" id="spandiv" runat="server">from 10/9/2013-10/24/2013</span>
                    </div>
                    <div><%-- class="graypaination">
                        <input id="ContentPlaceHolder1_btnfifteen" class="togl btn down" type="submit" value="15"
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
                        <div id="face_re">
                            <img src="../Contents/img/blank_img.png" id="fbProfileImg" runat="server">
                            <span>
                               <h2 id="divPageName" runat="server">"Please add atleast one Facebook Fan Page..."</h2>
                                <p id="divPageLikes" runat="server">
                                    </p>
                            </span>
                            <li>
                                <h2>
                                    Impressions by Age & Gender</h2>
                                <div style="height: 60px; float: left;">
                                    &nbsp;
                                </div>
                            </li>
                            <!-- LI FOR THE PAGE IMPRESSION CHART HERE BY BOOPATHY -->
                            <li>
                                <div id="chartContainer2" style="float: left; height: 300px; padding: 0 151px 0 0;
                                    width: 400px; position: relative;">
                                </div>
                            </li>
                            <!-- LI FOR THE PAGE IMPRESSION CHART HERE BY BOOPATHY -->
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
                <!-- DIV FOR THE PAGE IMPRESSION CHART HERE BY BOOPATHY -->
                <div class="rounder shadower pull-left reportcontent">
                    <div id="chartContainer" style="height: 200px; width: 100%;">
                    </div>
                </div>
                <!-- DIV FOR THE PAGE IMPRESSION CHART HERE BY BOOPATHY -->
                <div class="alert alert-suite-grey">
                    <div class="title">
                        <h1>
                            your contact</h1>
                        <span class="">A breakdown of the content you post</span>
                    </div>
                </div>
                <div class="rounder shadower pull-left reportcontent">
                    <div id="face_perc">
                        <li>
                            <label>
                                Reach</label><span>0</span></li>
                        <li>
                            <label>
                                People Talking About This</label><span id="spanTalking" runat="server">0</span></li>
                        <li>
                            <label>
                                Engagement</label><h2>
                                    %</h2>
                            <span>0%</span> </li>
                    </div>
                    <div class="title">
                        <h1>
                            Content BreakDown
                        </h1>
                        <span class="">A breakdown of how your individual post performed.</span>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr id="contentidta">
                                <td>
                                    Message Sent
                                </td>
                                <td>
                                    Talking
                                </td>
                                <td>
                                    Likes
                                </td>
                                <td>
                                    Comments
                                </td>
                                <td>
                                    Shares
                                </td>
                            </tr>
                            
                        </table>

                    </div>
                     <div id="divpost"  runat="server"></div>
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
           
           var shareGraphArray=<%=shareGraphArray%>

           //alert(shareGraphArray);

		var chart = new CanvasJS.Chart("chartContainer",
		{
			data: [
			{        
			type: "splineArea",
			color: "#000",
			dataPoints: shareGraphArray
//            [
//			{ label: "jan", y: 168 } ,
//			{ label: "feb", y: 118 } ,
//			{ label: "mar", y: 38 } ,
//			{ label: "apr", y: 28 } ,
//			{ label: "may", y: 148 } ,
//			{ label: "jun", y: 38 } ,
//			{ label: "jul", y: 178 } ,
//			{ label: "aug", y: 0 } ,
//			{ label: "sep", y: 98 } ,
//			{ label: "oct", y: 68 } ,
//			{ label: "nov", y: 18 } ,
//			{ label: "dec", y: 50 } 

//			]
			}             

			]
		});

		chart.render();
        var strgraph=<%=strgraph%>;
		var chart_new = new CanvasJS.Chart("chartContainer2",
		{
			title:{
			text: "Page Impressions",    
			},
			axisY: {
			title: "page impressions"
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
			legendText: "Age",
			dataPoints: strgraph
//            [      
//			{y: 297571, label: "10-20"},
//			{y: 267017,  label: "20-30" },
//			{y: 175200,  label: "30-40"},
//			{y: 154580,  label: "40-50"},
//			{y: 116000,  label: "50-60"},
//			{y: 97800, label: "60-70"},
//			{y: 20682,  label: "80-90"},        
//			{y: 20350,  label: "90 +"}        
//			]
			}   
			]
		});

			

		chart_new.render();

    </script>
</asp:Content>
