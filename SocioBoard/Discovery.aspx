<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true"
    CodeFile="Discovery.aspx.cs" Inherits="SocialSuitePro.Discovery" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div id="mainwrapper-message" class="container">
        <div id="sidebar" class="discovery">
            <div class="sidebar-inner">
                <div id="accordion2" class="accordion">
                    <div class="accordion-group">
                        <div class="accordion-heading">
                            <a href="#collapseOne"  data-parent="#accordion2" data-toggle="collapse" class="accordion-toggle">
                                <i class="icon-caret-right" style="margin-right: 10px"></i>Suggestion </a>
                        </div>
                        <div class="accordion-body collapse" id="collapseOne">
                            <div class="accordion-inner">
                                <ul>
                                    <li><a onclick="BindProfilesforNetwork('twitter')"><i  class="show icon-caret-right" style="visibility: visible; margin-right: 5px"></i>Your Followers</a>
                                         <ul id="twitterprofilesoffeed"></ul>
                                    </li>
                                <%--    <li><a onclick="GetTwitterSuggestions();" ><i  class="show icon-caret-right" style="visibility: visible; margin-right: 5px"></i>WooSuite Suggestions</a>--%>
                                    
                                    </li>
                                   <%-- <li><a href="#"><i class="hidden icon-caret-right" style="visibility: hidden; margin-right: 5px">
                                    </i>Conversed With</a> </li>
                                    <li><a href="#"><i class="hidden icon-caret-right" style="visibility: hidden; margin-right: 5px">
                                    </i>Have Mentioned You</a> </li>--%>
                                </ul>
                            </div>
                        </div>
                    </div>
                <%--    <div class="accordion-group">
                        <div class="accordion-heading">
                            <a href="#collapseTwo" data-parent="#accordion2" data-toggle="collapse" class="accordion-toggle">
                                <i class="icon-caret-right" style="margin-right: 10px"></i>Clean Up </a>
                        </div>
                        <div class="accordion-body collapse" id="collapseTwo">
                            <div class="accordion-inner">
                                <ul>
                                    <li><a href="#"><i class="show icon-caret-right" style="visibility: visible; margin-right: 5px">
                                    </i>Link 1</a> </li>
                                    <li><a href="#"><i class="hidden icon-caret-right" style="visibility: hidden; margin-right: 5px">
                                    </i>Link 2</a> </li>
                                    <li><a href="#"><i class="hidden icon-caret-right" style="visibility: hidden; margin-right: 5px">
                                    </i>Link 3</a> </li>
                                </ul>
                            </div>
                        </div>
                    </div>--%>
                    <div class="accordion-group">
                        <div class="accordion-heading">
                        <%-- smartsearch href was set = "#collapseThree" and changed by praveen on 12/07/2013 to discovery.aspx--%>

                            <a id="smartsearch" href="#collapseThree" data-parent="#accordion2" data-toggle="collapse" class="accordion-toggle">
                                <i class="icon-caret-right" style="margin-right: 10px"></i>Smart Search </a>
                        </div>
                        <div class="accordion-body collapse in" id="collapseThree">
                            <div class="accordion-inner">
                                <ul id="searchkeyword">
                                   <%-- <li><a href="#"><i class="show icon-caret-right" style="visibility: visible; margin-right: 5px">
                                    </i>Link 1</a> </li>
                                    <li><a href="#"><i class="hidden icon-caret-right" style="visibility: hidden; margin-right: 5px">
                                    </i>Link 2</a> </li>
                                    <li><a href="#"><i class="hidden icon-caret-right" style="visibility: hidden; margin-right: 5px">
                                    </i>Link 3</a> </li>--%>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="contentcontainer2">
            <div id="contentcontainer1-message">
                <div id="content" class="disco-content">
                  	<%--<div class="title">
							<h1>People who follow you</h1>
							<span class="">You are not following these people</span>
						</div>--%>



                    <%--    <ul id="disco-feeds-container" class="no-margin">
                        	<li class="shadower">
                            	<div class="disco-feeds">
                                	<div class="star-ribbon"></div>
                                    <div class="disco-feeds-img">
	                                    <img class="pull-left" src="img/admin/user5.png" alt="" />
                                    </div>
                                    <div class="disco-feeds-content">
                                        <div class="disco-feeds-title">
                                            <h3 class="no-margin">John Da Kid</h3>
                                            <span>@John_Da_Kid</span>
                                        </div>
                                        <p>Just Another Broke Atlanta Party Promoter</p>
                                        <a href="#" class="btn">Hide</a>
                                        <a href="#" class="btn">Full Profile <i class="icon-caret-right"></i> </a>                                        <div class="scl">
                                    	<a href="#"><img alt="" src="img/admin/usergrey.png"></a>
                                    	<a href="#"><img alt="" src="img/admin/goto.png"></a>
                                    	<a href="#"><img alt="" src="img/admin/setting.png"></a>
                                    </div>
                                    </div>
                                </div>
                                <div class="disco-feeds-info">
                                	<ul class="no-margin">
                                    	<li><a href="#"><img src="img/admin/markerbtn2.png" alt="">Atlanta, GA</a></li>
                                    	<li><a href="#"><img src="img/admin/url.png" alt="">http://t.co/NWAinfj51L</a></li>                                    </ul>
                                	<ul class="no-margin" style="margin-top:20px">
                                    	<li><a href="#"><img src="img/admin/twittericon-white.png" alt="">Followers <big><b>137,451</b></big></a></li>
                                    	<li><a href="#"><img src="img/admin/twitter-white.png" alt="">Following <big><b>114,078</b></big></a></li>
                                        </ul>
                                </div>
                            </li>
                        	<li class="shadower">
                            	<div class="disco-feeds">
                                	<div class="star-ribbon"></div>
                                    <div class="disco-feeds-img">
	                                    <img class="pull-left" src="img/admin/user6.png" alt="" />
                                    </div>
                                    <div class="disco-feeds-content">
                                        <div class="disco-feeds-title">
                                            <h3 class="no-margin">keshav999</h3>
                                            <span>@keshav999</span>
                                        </div>
                                        <p>India's permier auction Portal ....Win 100% FREE Product</p>
                                        <a href="#" class="btn">Hide</a>
                                        <a href="#" class="btn">Full Profile <i class="icon-caret-right"></i> </a>                                        <div class="scl">
                                    	<a href="#"><img alt="" src="img/admin/usergrey.png"></a>
                                    	<a href="#"><img alt="" src="img/admin/goto.png"></a>
                                    	<a href="#"><img alt="" src="img/admin/setting.png"></a>
                                    </div>
                                    </div>
                                </div>
                                <div class="disco-feeds-info">
                                	<ul class="no-margin">
                                    	<li><a href="#"><img src="img/admin/markerbtn2.png" alt="">Not Specific</a></li>
                                    	<li><a href="#"><img src="img/admin/url.png" alt="">No URL</a></li>                                    
                                    </ul>
                                	<ul class="no-margin" style="margin-top:20px">
                                    	<li><a href="#"><img src="img/admin/twittericon-white.png" alt="">Followers <big><b>137,451</b></big></a></li>
                                    	<li><a href="#"><img src="img/admin/twitter-white.png" alt="">Following <big><b>114,078</b></big></a></li>
                                        </ul>
                                </div>
                            </li>
                        	<li class="shadower">
                            	<div class="disco-feeds">
                                	<div class="star-ribbon"></div>
                                    <div class="disco-feeds-img">
	                                    <img class="pull-left" src="img/admin/user5.png" alt="" />
                                    </div>
                                    <div class="disco-feeds-content">
                                        <div class="disco-feeds-title">
                                            <h3 class="no-margin">John Da Kid</h3>
                                            <span>@John_Da_Kid</span>
                                        </div>
                                        <p>Just Another Broke Atlanta Party Promoter</p>
                                        <a href="#" class="btn">Hide</a>
                                        <a href="#" class="btn">Full Profile <i class="icon-caret-right"></i> </a>                                        <div class="scl">
                                    	<a href="#"><img alt="" src="img/admin/usergrey.png"></a>
                                    	<a href="#"><img alt="" src="img/admin/goto.png"></a>
                                    	<a href="#"><img alt="" src="img/admin/setting.png"></a>
                                    </div>
                                    </div>
                                </div>
                                <div class="disco-feeds-info">
                                	<ul class="no-margin">
                                    	<li><a href="#"><img src="img/admin/markerbtn2.png" alt="">Atlanta, GA</a></li>
                                    	<li><a href="#"><img src="img/admin/url.png" alt="">http://t.co/NWAinfj51L</a></li>                                    </ul>
                                	<ul class="no-margin" style="margin-top:20px">
                                    	<li><a href="#"><img src="img/admin/twittericon-white.png" alt="">Followers <big><b>137,451</b></big></a></li>
                                    	<li><a href="#"><img src="img/admin/twitter-white.png" alt="">Following <big><b>114,078</b></big></a></li>
                                        </ul>
                                </div>
                            </li>
                        	<li class="shadower">
                            	<div class="disco-feeds">
                                	<div class="star-ribbon"></div>
                                    <div class="disco-feeds-img">
	                                    <img class="pull-left" src="img/admin/user6.png" alt="" />
                                    </div>
                                    <div class="disco-feeds-content">
                                        <div class="disco-feeds-title">
                                            <h3 class="no-margin">keshav999</h3>
                                            <span>@keshav999</span>
                                        </div>
                                        <p>India's permier auction Portal ....Win 100% FREE Product</p>
                                        <a href="#" class="btn">Hide</a>
                                        <a href="#" class="btn">Full Profile <i class="icon-caret-right"></i> </a>                                        <div class="scl">
                                    	<a href="#"><img alt="" src="img/admin/usergrey.png"></a>
                                    	<a href="#"><img alt="" src="img/admin/goto.png"></a>
                                    	<a href="#"><img alt="" src="img/admin/setting.png"></a>
                                    </div>
                                    </div>
                                </div>
                                <div class="disco-feeds-info">
                                	<ul class="no-margin">
                                    	<li><a href="#"><img src="img/admin/markerbtn2.png" alt="">Not Specific</a></li>
                                    	<li><a href="#"><img src="img/admin/url.png" alt="">No URL</a></li>                                    
                                    </ul>
                                	<ul class="no-margin" style="margin-top:20px">
                                    	<li><a href="#"><img src="img/admin/twittericon-white.png" alt="">Followers <big><b>137,451</b></big></a></li>
                                    	<li><a href="#"><img src="img/admin/twitter-white.png" alt="">Following <big><b>114,078</b></big></a></li>
                                        </ul>
                                </div>
                            </li>
                        </ul>--%>
                        <%--class was included before  and class name is : threefourth, removed on 13/07/2013 by praveen --%>
              
                     <section id="inbox_msgs" style="text-align:center;" class=" messages msg_view">
                            <div class="smart_search">
                            	<div class="title">
                                	Discovery allows you to find new customers by searching for keywords around your business. 
                                </div>
                                <div class="input_text_select">
                                	<asp:TextBox ID="txtSearchText" runat="server"></asp:TextBox>
                                     <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click"
                                        ></asp:Button>

                                </div>

                                <div id="searchresults"  runat="server" class="messages">
                                <div id ="DivAll"></div>
                                <div id ="TwtResults" runat="server"></div>
                                <div id ="FbResults" runat="server"></div>
                                </div>
                            </div>
                        </section>
                </div>
             <%--   <div id="right-sidebar">
                    <h3>
                        Suggesstions</h3>
                    <p>
                        Find new people to follow or cleanup your twitter account using WooSuite's
                        Suggestions.</p>
                    <span>Filter By</span>
                    <form name="" action="#">
                    <select id="searchfilteration" name="" style="background: none repeat scroll 0 0 #CB7870; border: 0 none;
                        color: #FFFFFF; height: 40px; padding: 10px;">
                        <option value="1">All</option>
                        <option value="2">Facebook</option>
                        <option value="3">Twitter</option>
                    </select>
                    </form>
                </div>--%>
            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript">
        debugger;
        $(".accordion-toggle").click(function () {
            $(".accordion-toggle").each(function (index) {
                $(this).addClass('collapsed');
            });

            $(this).removeClass('collapsed');
        })

        debugger;
        $("#home").removeClass('active');
        $("#message").removeClass('active');
        $("#feeds").removeClass('active');
        $("#discovery").addClass('active');
        $("#publishing").removeClass('active');
        debugger;

        debugger;

        $(document).ready(function () {
            BindTwitterInDiscovery();
            GetSearchedKeyword();
            $("#smartsearch").click(function () {
                debugger;
                $("#content").load("../Layouts/discovery.htm");

            });


        });
    
    



    </script>
</asp:Content>
