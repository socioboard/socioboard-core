<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="TeamReport.aspx.cs" Inherits="SocialSuitePro.Reports.TeamReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript" src="../../Contents/js/progressbar.js"></script>
<style text="text/css">
#ContentPlaceHolder1_taskdiv {
    clear: both;
    margin: 20px 0 10px;
    width: 99%;
}
.task-text-3 {font-size: 13px;}
.task-text-4,.task-text-5 {font-size: 13px;}
</style>
<div id="mainwrapper" class="container reports">
			<div id="sidebar">            	
				<div class="sidebar-inner">
                	<a href="GroupStats.aspx" class="btn">GROUP REPORT <img src="<%= Page.ResolveUrl("~/Contents/img/admin/boxes.png")%>" alt="" class="pull-right" /></a>
                	<a href="FacebookReport.aspx" class="btn">Facebook Pages <img src="<%= Page.ResolveUrl("~/Contents/img/admin/fbicon2.png")%>" alt="" class="pull-right" /></a>
					<a href="TwitterReport.aspx" class="btn">Twitter Reports <img src="<%= Page.ResolveUrl("~/Contents/img/admin/twittericon2.png")%>" alt="" class="pull-right" /></a>
					<a href="#" class="btn actives">Team Report <img src="<%= Page.ResolveUrl("~/Contents/img/admin/peoples.png")%>" alt="" class="pull-right" /></a>
                     <%-- <a class="btn" href="GoogleAnalytics.aspx">Google Analytics Report <img class="pull-right" alt="" src="<%= Page.ResolveUrl("~/Contents/img/admin/peoples.png")%>"></a>	--%>
				<%--	<a href="#" class="btn">Twitter Comparison <img src="../Contents/img/admin/loopback.png" alt="" class="pull-right" /></a>
					<a href="#" class="btn">Sent Message <img src="../Contents/img/admin/bar-chart.png" alt="" class="pull-right" /></a>	--%>			
				</div>
			</div>           
            	<div id="contentcontainer-report">
                	<div id="content" style="left:0;">
						<div class="alert alert-suite-grey" style="width:882px;">
							<div class="title">
								<h1>Inter report</h1>
								<span class="" id="spanTopDate" runat="server">from Apr. 15, 2013 - Apr. 29, 2013</span>
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
                             <%--   <button class="togl btn"><img src="../Contents/img/admin/add.png" alt="" style="margin-top:-5px"/></button>
                                <button class="btn" style="padding:2px 5px;background:none repeat scroll 0 0 #CB786F;color:#FFFFFF;text-shadow:none">Export Data <b class="caret"></b></button>--%>
                            </div>
                            <div class="grey-caret"></div>
						</div>
                        
                        <div class="rounder shadower pull-left reportcontent" style="padding-right:20px; width:895px;">
                        	<div class="header-outer">
                            	<h1>publishing</h1>
                                <div class="date-head"><%--Apr. 15, 2013 - Apr. 29, 2013--%></div>
                                <div class="clear"></div>
                            </div>
                            <div class="publish-list">
                            	<div class="avathar-pub"><img src="../Contents/img/avathar-1.jpg" alt="" /></div>
                                <div class="publish-list-right">
                                	<div class="publish-name" id="divName" runat="server">Shobhit</div>
                                    <div class="daily-ave">0.00 Daily avg</div>
                                    <div  class="publish-reply" id="repliescount" runat="server"><span>0 REPLIES</span> / 0 TOTAL POSTS</div>
                                    <div class="clear"></div>
                                </div>

                                <div id="progressBar" class="jquery-ui-like"><div></div></div>
                                <div class="clear"></div>
                            </div>
                            
                        </div>
                        
                        <div class="rounder shadower pull-left reportcontent" style="padding-right:20px; width:895px;">
                        	<div class="header-outer">
                            	<h1>TASKS</h1>
                                <div class="date-head"><img src="../Contents/img/help-icon.png" alt="" align="absmiddle" /><span id="spanDate" runat="server"> Apr. 15, 2013 - Apr. 29, 2013</span></div>
                                <div class="clear"></div>
                            </div>
                            <div class="task-list-outer" id="taskdiv" runat="server">
                            	<div class="task-labels">
                                	<div class="task-labe-1">TASK OWNER</div>
                                    <div class="task-labe-2">ASSIGNED</div>
                                    <div class="task-labe-3">TASK MESSAGE</div>
                                    <div class="task-labe-4">ASSIGN DATE</div>
                                    <div class="task-labe-5">COMPLETE DATE</div>
                                    <div class="task-labe-6">STATUS</div>
                                    <div class="task-labe-7">COMPLETION TIME</div>
                                    <div class="clear"></div>
                                </div>
                                <div class="task-header">
                                	<div class="task-header-owner">
                                    	<div class="avathar-pub"><img src="../Contents/img/avathar-1.jpg" alt="" /></div>
                                        <div class="task-header-owner-name">SHOBHIT</div>
                                        <div class="clear"></div>
                                    </div>
                                    <div class="assigned-lable">-----</div>
                                    <div class="task-text-3">-----</div>
                                    <div class="task-text-4">-----</div>
                                    <div class="task-text-5">-----</div>
                                    <div class="task-text-6">-----</div>
                                    <div class="task-text-7">-----</div>
                                    <div class="clear"></div>
                                </div>
                                <div class="task-lists">
                                	<div class="task-header-owner">
                                    	<div class="avathar-pub"><img src="../Contents/img/avathar-1.jpg" alt="" /></div>
                                        <div class="task-header-owner-name">SURYA T.</div>
                                        <div class="clear"></div>
                                    </div>
                                    <div class="assigned-lable">23</div>
                                    <div class="task-text-3">21</div>
                                    <div class="task-text-4">30min</div>
                                    <div class="task-text-5">48min</div>
                                    <div class="task-text-6">50min</div>
                                    <div class="task-text-7">91%<div id="percent_complete-1" style="width:60px; height:25px; float:right;"></div></div>
                                    <div class="clear"></div>
                                </div>
                                <div class="task-lists">
                                	<div class="task-header-owner">
                                    	<div class="avathar-pub"><img src="../Contents/img/avathar-1.jpg" alt="" /></div>
                                        <div class="task-header-owner-name">Anubhav Sinha.</div>
                                        <div class="clear"></div>
                                    </div>
                                    <div class="assigned-lable">25</div>
                                    <div class="task-text-3">14</div>
                                    <div class="task-text-4">25min</div>
                                    <div class="task-text-5">1 hrs. 5min</div>
                                    <div class="task-text-6">30min</div>
                                    <div class="task-text-7">85%<div id="percent_complete-2" style="width:60px; height:25px; float:right;"></div></div>
                                    <div class="clear"></div>
                                </div>
                                <div class="clear"></div>
                            </div>
                        </div>
					</div>                    
				</div>			
		</div>
        <script type="text/javascript" src="https://www.google.com/jsapi"></script>
<script type="text/javascript" src="<%= Page.ResolveUrl("~/Contents/js/graph-scripts.js")%>"></script>
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
<script type="text/javascript">
    progressBar(Number(<%=taskPer %>), $('#progressBar'));
	</script>
</asp:Content>
