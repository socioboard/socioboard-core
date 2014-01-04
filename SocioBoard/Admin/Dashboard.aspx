<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/adminSite.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SocialSuitePro.Admin.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="userpermonth"> Users Per Month </div>

<div class="cvs_unpaid">
  <div class="cvs">
        <div class="chart_title">Users Register Per Month</div>
        <canvas id="cvs"  width="531" height="275" style="width: 531px; height: 275px;">[No canvas support]</canvas>
  </div>
  
  <div class="cvsUnpaid">
        <div class="chart_title">Users Register As Trail </div>
        <canvas id="cvsUnpaid"  width="531" height="275" style="width: 531px; height: 275px;">[No canvas support]</canvas>
  </div> 
   
</div>





<script src="../Contents/js/jquery.min.js" type="text/javascript"></script>
<script src="../Contents/js/Chart.js" type="text/javascript"></script>
<script type="text/javascript" src="../Contents/js/canvasjs.min.js"></script>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        debugger;
        var Userdata = {
            labels: [<%=strMonth %>],
            datasets: [
		{
		 	fillColor : "rgba(178,178,178,1)",
			strokeColor : "rgba(178,178,178,0)",
			data : [<%= strUser%>]
		   // data: [<%= strUser%>]

		}
	]
        }
      
        var myLine = new Chart(document.getElementById("cvs").getContext("2d")).Line(Userdata);

         var UserAccdata = {
            labels: [<%=strAccMonth %>],
            datasets: [
		        {
                fillColor : "rgba(150,150,150,0.9)",
					strokeColor : "rgba(150,150,150,1)",
					pointColor : "rgba(156,156,156,1)",
					pointStrokeColor : "#000",
			        data : [<%= strStandard%>]
		        },
                {
		 	       fillColor : "rgba(177,88,84,0.9)",
					strokeColor : "rgba(177,88,84,1)",
					pointColor : "rgba(182,85,116,1)",
                    pointStrokeColor : "#fff",
			        data : [<%= strDelux%>]
		        },
                {
		 	        fillColor : "rgba(190,190,190,1)",
			        strokeColor : "rgba(190,190,190,0)",
                    pointColor : "rgba(190,190,190,0)",
					pointStrokeColor : "#ccc",
			        data : [<%= strPremium%>]
		        }
	]
        }
      
        var myAccLine = new Chart(document.getElementById("cvsUnpaid").getContext("2d")).Line(UserAccdata);

    });
</script>
</asp:Content>
