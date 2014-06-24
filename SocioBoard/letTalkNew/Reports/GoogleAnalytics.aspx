<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true" CodeBehind="GoogleAnalytics.aspx.cs" Inherits="letTalkNew.Reports.GoogleAnalytics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


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
		
		<!-- MAP SCRIPT STARTS HETE -->
		<script type="text/javascript" src="../Contents/js/canvasjs.min.js"></script>
		<script type="text/javascript">
		    window.onload = function () {
		        var chart = new CanvasJS.Chart("chartContainer",
			{
			    theme: "theme2",
			    title: {
			        text: "Site Visits - per month"
			    },
			    axisX: {
			        valueFormatString: "MMM",
			        interval: 1,
			        intervalType: "month"

			    },
			    axisY: {
			        includeZero: false

			    },
			    data: [
			  {
			      type: "line",
			      //lineThickness: 3,        
			      dataPoints: <%=siteVisitGraphArr %>
//                  [
//				{ x: new Date(2012, 00, 1), y: 450 },
//				{ x: new Date(2012, 01, 1), y: 414 },
//				{ x: new Date(2012, 02, 1), y: 520 },
//				{ x: new Date(2012, 03, 1), y: 460 },
//				{ x: new Date(2012, 04, 1), y: 450 },
//				{ x: new Date(2012, 05, 1), y: 500 },
//				{ x: new Date(2012, 06, 1), y: 480 },
//				{ x: new Date(2012, 07, 1), y: 480 },
//				{ x: new Date(2012, 08, 1), y: 410 },
//				{ x: new Date(2012, 09, 1), y: 500 },
//				{ x: new Date(2012, 10, 1), y: 480 },
//				{ x: new Date(2012, 11, 1), y: 510 }

//				]
			  }


			  ]
			});

		        chart.render();
		    }
		</script>
		<!-- MAP SCRIPT ENDS HETE -->


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
            <li><a href="TeamReport.aspx">Team Report
                <img src="../Contents/img/peoples.png"></a></li>
            <li class="report_tap_width_active"><a href="GoogleAnalytics.aspx">Google Analytics Report
                <img src="../Contents/img/peoples.png"></a></li>
        </ul>
          <div id="contentcontainer-report">
            <div id="content">
            <div class="alert alert-suite-grey">
                <div class="title">
                  <h1>Google Analytics</h1>
                  <span class="">from 10/9/2013-10/24/2013</span> </div>
                <div class="graypaination">
                  <input type="submit" name="ctl00$ContentPlaceHolder1$btnfifteen" value="15" class="togl btn down" id="ContentPlaceHolder1_btnfifteen">
                  <input type="submit" name="ctl00$ContentPlaceHolder1$btnthirty" value="30" class="togl btn" id="ContentPlaceHolder1_btnthirty">
                  <input type="submit" name="ctl00$ContentPlaceHolder1$btnsixty" value="60" class="togl btn" id="ContentPlaceHolder1_btnsixty">
                  <input type="submit" name="ctl00$ContentPlaceHolder1$btnninty" value="90" class="togl btn" id="ContentPlaceHolder1_btnninty">
                </div>
              </div>
              
              <div class="rounder shadower pull-left reportcontent">
              	<div class="">
                 
					<!--  -->

						<div id="chartContainer" style="height: 300px; width: 100%;">

					<!--  -->
              </div>
              
            </div>
              
              </div>
              
              
              <div class="alert alert-suite-grey">
                
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

</asp:Content>
