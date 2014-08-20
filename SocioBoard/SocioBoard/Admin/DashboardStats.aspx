<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashboardStats.aspx.cs" Inherits="SocioBoard.Admin.DashboardStats" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
<script src="../Contents/js/highcharts.js" type="text/javascript"></script>
    <script src="../Contents/js/exporting.js" type="text/javascript"></script>
		<script type="text/javascript">
		    $(function () {
		        $('#container1').highcharts({
		            chart: {
		                type: 'column'
		            },
		            title: {
		                text: ''
		            },
		            
		            xAxis: {
		                categories: [<%=strPaidMonth %>]
		            },
		            yAxis: {
		                min: 0,
		                title: {
		                    text: 'No of Users'
		                }
		            },
		            tooltip: {
		                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
		                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y:.1f}</b></td></tr>',
		                footerFormat: '</table>',
		                shared: true,
		                useHTML: true
		            },
		            plotOptions: {
		                column: {
		                    pointPadding: 0.2,
		                    borderWidth: 0
		                }
		            },
		            series: [{
		                name: 'Paid User',
		                data: [<%= strPaidUser%>]

		            }]
		        });
		    });

              $(function () {
		        $('#container2').highcharts({
		            chart: {
		                type: 'column'
		            },
		            title: {
		                text: ''
		            },
		            
		            xAxis: {
		                categories: [<%=strUnPaidMonth %>]
		            },
		            yAxis: {
		                min: 0,
		                title: {
		                    text: 'No of Users'
		                }
		            },
		            tooltip: {
		                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
		                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y:.1f}</b></td></tr>',
		                footerFormat: '</table>',
		                shared: true,
		                useHTML: true
		            },
		            plotOptions: {
		                column: {
		                    pointPadding: 0.2,
		                    borderWidth: 0
		                }
		            },
		            series: [{
		                name: 'Unpaid User',
		                data: [<%= strUnPaidUser%>]

		            }]
		        });
		    });
		    
    

		</script>
 <style type="text/css">
    .asd{width:900px;}
    #container1{float:left;}
    #container2{float:right;}
 </style>
    
<div class="asd">
    <div id="container1" style="min-width: 310px; height: 400px;"></div>
    <div id="container2" style="min-width: 310px; height: 400px;"></div>
</div>
</body>
</html>
