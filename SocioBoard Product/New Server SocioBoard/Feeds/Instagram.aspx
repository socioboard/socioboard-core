<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="Instagram.aspx.cs" Inherits="SocialSuitePro.Feeds.Instagram" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <link href="../Contents/css/jquery.mCustomScrollbar.css" rel="stylesheet" type="text/css" />
    <script src="../Contents/js/jquery.mCustomScrollbar.concat.min.js" type="text/javascript"></script>
<div id="mainwrapper" class="container reports">
			<div id="sidebar" class="feeds">
				<div class="sidebar-inner">
<div id="accordion2" class="accordion">
               <div class="accordion-group">
                        <div class="accordion-heading">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseOne"
                                onclick="BindProfilesforNetwork('facebook');">
                                <img alt="" src="../Contents/img/admin/1.png" class="fesim">FACEBOOK <i class="icon-sort-down pull-right hidden">
                                </i></a>
                        </div>
                        <div id="collapseOne" class="accordion-body collapse">
                            <div class="accordion-inner">
                                <ul id="facebookusersforfeeds">
                                    <li><a href="#" class="active">
                                        <img src="../Contents/img/891.png" alt="" /></a> </li>
                                    <%--<li><a href="#">Link 2</a> </li>
                                    <li><a href="#">Link 3</a> </li>--%>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="accordion-group">
                        <div class="accordion-heading">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseTwo"
                                onclick="BindProfilesforNetwork('twitter');">
                                <img alt="" src="../Contents/img/admin/2.png" class="fesim">TWITTER <i class="icon-sort-down pull-right">
                                </i></a>
                        </div>
                        <div id="collapseTwo" class="accordion-body collapse">
                            <div class="accordion-inner">
                                <ul id="twitterprofilesoffeed">
                                    <li><a href="#" class="active">
                                        <img src="../Contents/img/891.png" alt="" /></a> </li>
                                    <%--  <li><a href="#">Profile Connected</a> </li>--%>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="accordion-group">
                        <div class="accordion-heading">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseThree"
                                onclick="BindProfilesforNetwork('linkedin');">
                                <img alt="" src="../Contents/img/admin/5.png" class="fesim" />LINKEDIN <i class="icon-sort-down pull-right hidden">
                                </i></a>
                        </div>
                        <div id="collapseThree" class="accordion-body collapse">
                            <div class="accordion-inner">
                                <ul id="linkedinprofilesforfeed">
                                    <li><a href="#" class="active">
                                        <img src="../Contents/img/891.png" alt="" /></a> </li>
                                    <%-- <li><a href="#">Link 2</a> </li>
                                    <li><a href="#">Link 3</a> </li>--%>
                                </ul>
                            </div>
                        </div>
                    </div>
                <div class="accordion-group">
                  <div class="accordion-heading">
                    <a href="#collapseFour" data-parent="#accordion2" data-toggle="collapse" class="accordion-toggle"  onclick="BindProfilesforNetwork('instagram');"><img class="fesim" src="../Contents/img/admin/4.png" alt="" />INSTAGRAM <i class="icon-sort-down pull-right"></i></a>
                  </div>
                  <div class="accordion-body collapse in" id="collapseFour">
                    <div  class="accordion-inner">
                    	<ul id="instagramProfilesforfeed">
                        	<li>
                            	<a href="#">Link 1</a>
                            </li>
                        	<li>
                            	<a href="#">Link 2</a>
                            </li>
                        	<li>
                            	<a href="#">Link 3</a>
                            </li>
                        </ul>
                    </div>
                  </div>
                </div>
          <%--      <div class="accordion-group">
                  <div class="accordion-heading">
                    <a href="#collapseFive" data-parent="#accordion2" data-toggle="collapse" class="accordion-toggle"><img class="fesim" src="img/admin/3.png" alt="" />GOOGLE + <i class="icon-sort-down pull-right hidden"></i></a>
                  </div>
                  <div class="accordion-body collapse" id="collapseFive">
                    <div class="accordion-inner">
                    	<ul>
                        	<li>
                            	<a href="#">Link 1</a>
                            </li>
                        	<li>
                            	<a href="#">Link 2</a>
                            </li>
                        	<li>
                            	<a href="#">Link 3</a>
                            </li>
                        </ul>
                    </div>
                  </div>
                </div>--%>
              </div>               
                </div>				
		  	</div>           
            	<div id="contentcontainer" runat="server" class="contentcontainer-feeds">
                            
				</div>			
		</div>
</asp:Content>
