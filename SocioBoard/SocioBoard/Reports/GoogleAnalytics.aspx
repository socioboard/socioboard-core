<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="GoogleAnalytics.aspx.cs" Inherits="SocialSuitePro.Reports.GoogleAnalytics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style type="text/css">
.entryPost
{
    width:955px;
    height:auto;
    float:left;
    background:#fff;
    padding:10px;
}
.twitter_drop_down_and_tweet_box
{
    float: left;
    height: auto;
    margin-left: 55px;
    width: 905px;
}
.twitter_drop_down_and_tweet_box .fbinsigh_drop_down,
.twitter_drop_down_and_tweet_box .fbinsigh_drp_down
{
    width:225px;
    height:auto;
    float:left;
}

.twitter_drop_down_and_tweet_box .fbinsigh_drop_down select,
.twitter_drop_down_and_tweet_box .fbinsigh_drp_down select
{
    width:220px;
    float:left;
    margin-right:5px;
}

.twitter_drop_down_and_tweet_box .fbinsight_drop_down_inputbx
{
    width:210px;
    height:auto;
    float:left;
}
.twitter_drop_down_and_tweet_box .fbinsight_drop_down_inputbx input[type="text"]
{
    width:190px;
    float:left;
}
.postContent 
{
    width:100%;
    height:auto;
    float:left;
}
.postContent > .metric,
.postContent > .metric > table,
.postContent > .visit,
.postContent > .visualiztion_div
{
    width:100%;
    height:auto;
    float:left;
}
.postContent > h3,
.postContent > .visit > h3,
.postContent > .metric > h3,
.postContent > .visualiztion_div > h3{
    color: #7A92B8;
    float: left;
    font-family: Arial,Helvetica,sans-serif;
    font-size: 19px;
    height: auto;
    text-align: justify;
    width: 100%;
    margin-left:55px;
}
.insight_btn {
    float: left;
    margin-left: 5px;
    width: 125px;
}
div > svg > g > g > g > text {
    font-size: 13px !important;
}
.chart_bar_div h3
{
    margin-left:55px;
}


#chart_div_year::-webkit-scrollbar {
	    width: 8px;
	}
#chart_div_year::-webkit-scrollbar-button {
	width: 8px;
	height:5px;
}
#chart_div_year::-webkit-scrollbar-track {
    background:#eee;
	border: thin solid lightgray;
	box-shadow: 0px 0px 3px #dfdfdf inset;
	border-radius:10px;
}
#chart_div_year::-webkit-scrollbar-thumb {
	background:#999;
	border: thin solid gray;
	border-radius:10px;
}
#chart_div_year::-webkit-scrollbar-thumb:hover {
	background:#7d7d7d;
}
</style>
<div id="mainwrapper" class="container reports">

<div id="sidebar">            	
	<div class="sidebar-inner">
        <a class="btn" href="#">GROUP REPORT <img class="pull-right" alt="" src="<%= Page.ResolveUrl("~/Contents/img/admin/boxes.png")%>" /></a>
        <a class="btn" href="FacebookReport.aspx">Facebook Pages <img class="pull-right" alt="" src="<%= Page.ResolveUrl("~/Contents/img/admin/fbicon2.png")%>" /></a>
		<a class="btn" href="TwitterReport.aspx">Twitter Reports <img class="pull-right" alt="" src="<%= Page.ResolveUrl("~/Contents/img/admin/twittericon2.png")%>" /></a>
		<a class="btn" href="TeamReport.aspx">Team Report <img class="pull-right" alt="" src="<%= Page.ResolveUrl("~/Contents/img/admin/peoples.png")%>" /></a>
        <a class="btn" href="GoogleAnalytics.aspx">Google Analytics Report <img class="pull-right" alt="" src="<%= Page.ResolveUrl("~/Contents/img/admin/peoples.png")%>"></a>				
	</div>				
</div>
<div id="contentcontainer-report">
<div id="content" style="margin-left:-175px; width:855px;">
   <div class="entryPost">
            <div class="postContent">
                 <h3>Google Analytics : Site Usage<asp:Label ID="lblMessage" runat="server" 
                         ForeColor="#990000"></asp:Label>
                 </h3>
                <div class="twitter_drop_down_and_tweet_box">
                    <div class="styled-select">
                        <asp:DropDownList ID="ddlAccounts" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlAccounts_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </div>
                    <div class="styled-select">
                        <asp:DropDownList ID="ddlProfile" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="month-select">
                        <asp:DropDownList ID="ddlPeriod" runat="server" AutoPostBack="True" ><%--
                            <asp:ListItem Value="0">Select Period</asp:ListItem>--%>
                            <asp:ListItem Value="1">Day</asp:ListItem>
                            <asp:ListItem Value="2">Month</asp:ListItem>
                            <asp:ListItem Value="3">Year</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <%--<div class="fbinsight_drop_down_inputbx">
                        <asp:TextBox ID="txtDateSince" runat="server" onchange="setMaxDate()" />
                    </div>
                    <div class="fbinsight_drop_down_inputbx">
                        <asp:TextBox ID="txtDateUntill" runat="server" />
                    </div>--%>
                    <div class="insight_btn">
                        <%--<asp:Button ID="btnAnalytics" runat="server" onclick="btnAnalytics_Click" Text="Get Analytics" />--%>
                        <asp:ImageButton ID="btnGa" runat="server" Height="35" ImageUrl="~/Contents/img/btn_analytics.png" OnClick="btnAnalytics_Click" Width="104" />
                    </div>
                    
                    <div style="width:200px; height:auto; float:left;">
                        <asp:Label ID="Label7" runat="server" Text="Invalid Dates" ForeColor="#FF3300" Visible="False"></asp:Label>
                    </div>
                </div>
               
            </div>
            <!-- chart code here -->
            <div class="postContent">
             <!--visit-->
            <div class="visit">
                <h3> Visits</h3>
                <div id="chart_div"></div>

                <div id="chart_div_month"></div>

                <div id="chart_div_year"></div>
            </div>
            </div>
            <!--end visit-->

            <div class="postContent">
            <!--metric-->
                
                <div class="metric">
                  <%--  <h3> Metric Values</h3>--%>
                <%--<table>
                    <thead>
                        <tr>
                            <th scope="col">
                                Metric
                            </th>
                            <th scope="col">
                                Value
                            </th>
                        </tr>
                    </thead>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Total Visits: " 
                                ToolTip="Visits is the number of visits to your site."></asp:Label>
                        </td>
                        <td>
                            <asp:Literal ID="ltrVisits" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;<asp:Label 
                                ID="Label2" runat="server" Text="Pageviews:" 
                                ToolTip="Pageviews is the total number of pages viewed. Repeated views of a single page are counted."></asp:Label>
                        </td>
                        <td>
                            <asp:Literal ID="ltrPageviews" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Pages/Visit: " 
                                ToolTip="Pages/Visit (Average Page Depth) is the average number of pages viewed during a visit to your site. Repeated views of a single page are counted."></asp:Label>
                        </td>
                        <td>
                            <asp:Literal ID="ltrPagevisits" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Bounce Rate: " 
                                ToolTip="Bounce Rate is the percentage of single-page visits (i.e. visits in which the person left your site from the entrance page)."></asp:Label>
                        </td>
                        <td>
                            <asp:Literal ID="ltrBounceRate" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Average Time on Site: " 
                                ToolTip="The average time on site."></asp:Label>
                        </td>
                        <td>
                            <asp:Literal ID="ltrAvgTimeOnSite" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Total New Visits: " 
                                ToolTip="visits that were first-time visits (from people who had never visited your site before)."></asp:Label>
                        </td>
                        <td>
                            <asp:Literal ID="ltrNewVisits" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>--%>
                </div>
            <!--end metric-->
           

            <!--chart-pie-->
               <div class="chart_pie_div">
             <%--   <h3> Visits per Region</h3>
                   <div id="chart_pie"> </div>--%>
               </div>
            <!--end chartpie-->
            </div>
            <div class="postContent">
            <!--visualization-->
            <div class="visualiztion_div">
              <%--  <h3> visualization</h3>
                            <div id="visualization"></div>--%>
            </div>
            <!--end visualization-->

            <!--chart_bar-->
            <div class="chart_bar_div">
                <h3> Country</h3>
                <div id="chart_bar"></div>
            </div>
             <!--end chart_bar-->
            </div>
        <!--end chart code here -->
        </div>
        </div>
        </div>
</div>

    <script type="text/javascript" src="../Contents/js/jquery.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>

        <script type="text/javascript" language="javascript">
            google.load("visualization", "1", { packages: ["corechart"] });
    $(document).ready(function () {
        var cntryVal="<%=strCntryVal %>".split("@");
        debugger;
        var cntry=cntryVal[0].split(",");
        var cval=cntryVal[1].split(",");
        var arrData = new Array(cntry.length);
        arrData[0] = new Array(2)
        arrData[0][0] = "Country";
        arrData[0][1] = "Visits";
        i++;
       for (var i = 1; i <= cval.length; i++) {
                    arrData[i] = new Array(2);
                    if (cntry[i - 1] != "") {
                        arrData[i][0] = cntry[i-1];
                        arrData[i][1] = Number(cval[i-1]);
                    }

                }

        var data = google.visualization.arrayToDataTable(arrData);

        var options = {
            title: 'Visit on site from Refrral'
        };
        new google.visualization.ColumnChart(document.getElementById('chart_bar')).
      draw(data,
           {
               width: 1500, height: 400,
               vAxis: { title: "Country" },
               hAxis: { title: "visits" },
               colors: ['#3E5B85'],
               fontSize: 14
           });

       
           var yearVal = "<%=strYearVal %>".split("@");
           debugger;
           var year = yearVal[0].split(",");
           var yval = yearVal[1].split(",");
           var arrYearData = new Array(yval.length);
           arrYearData[0] = new Array(2)
           arrYearData[0][0] = "Year";
           arrYearData[0][1] = "Visits";
           i++;
           for (var i = 1; i <= year.length; i++) {
               arrYearData[i] = new Array(2);
               if (year[i - 1] != "") {
                   arrYearData[i][0] = year[i - 1];
                   arrYearData[i][1] = Number(yval[i - 1]);
               }

           }

           var data = google.visualization.arrayToDataTable(arrYearData);

           var options = {
               title: 'Visit on site from Refrral'
           };
           new google.visualization.ColumnChart(document.getElementById('chart_div_year')).
      draw(data,
           {
              width:1500, height: 400,
               vAxis: { title: "<%=strdurationVal %>" },
               hAxis: { title: "visits" },
               colors: ['#3E5B85']
           });
    });
</script>
</asp:Content>
