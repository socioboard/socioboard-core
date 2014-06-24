<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="Discovery.aspx.cs" Inherits="SocioBoard.Discovery.Discovery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
    <script type="text/javascript">
        $(document).ready(function () {
            $("#home").removeClass('active');
            $("#message").removeClass('active');
            $("#feeds").removeClass('active');
            $("#discovery").addClass('active');

        });

        function Validate() {
            var message = "";
            if (document.getElementById("txtSearchText").value == "") {
                message += ".";
            }

            if (message != '') {
                    message = "Please fill out this field " + message;
                
                return false;
            }
            return true;
        }

    </script>

  <div class="container_wrapper">
            	<div class="ws_container_page"> 
                    <aside role="complementary" class="unselectable" id="actions">
                        <ul class="msgs_nav">
                            <li class="accordion single">
                                <a href="#">
                                    <div class="nav_icon dicovery_icon"></div>
                                    <div class="text">
                                    	<span class="label">Suggestions</span>  
                                        <span class="numeric" id="smartinbox_count" style="display:none">
                                        	<span>0</span>
                                        </span>
                                    </div>
                                </a>
                            </li>
                            <li class="accordion single">
                                <a href="#">
                                    <div class="nav_icon dicovery_icon"></div>
                                    <div class="text">
                                    	<span class="label">Cleanup</span>  
                                        <span class="numeric" id="smartinbox_count" style="display:none">
                                        	<span>0</span>
                                        </span>
                                    </div>
                                </a>
                            </li>
                            <li class="accordion single selected">
                                <a href="/messages/queue/">
                                    <div class="nav_icon dicovery_icon"></div>
                                    <div class="text">
                                    	<span class="label">Smart Search</span>  
                                    </div>
                                </a>
                            </li>
                           
                        </ul>
    				</aside>      
                    <div id="content" role="main">
                        <section id="inbox_msgs" class="threefourth messages msg_view">
                            <div class="smart_search">
                            	<div class="title">
                                	Discovery allows you to find new customers by searching for keywords around your business. 
                                </div>
                                <div class="input_text_select">
                                	<asp:TextBox ID="txtSearchText" runat="server"></asp:TextBox><%--<input id = "txtSearchText" name="txtSearchText" type="text" runat="server" placeholder="Enter Keyword and Search" /> <input type="button" value="Search" onclick="BindTwitterSearchInDiscovery(); " />--%><%-- <asp:TextBox ID="txtSearchText" ></asp:TextBox>--%><%--OnClientClick="BindTwitterSearchInDiscovery();"--%>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" 
                                        OnClientClick="return Validate()" onclick="btnSearch_Click"  ></asp:Button>

                                </div>

                                <div id="searchresults"  runat="server" class="messages">
                                <div id ="DivAll"></div>
                                <div id ="TwtResults" runat="server"></div>
                                <div id ="FbResults" runat="server"></div>
                                </div>
                            </div>
                        </section>
                    </div>      
                                  
                    <div class="ws_msg_right">     
                       <div class="ws_msg_right"> 
                       <!--twiter-->                            
                        <div class="feeds_right">
                        	<h3 class="section_sub_ttl">Ideas for Searching</h3>
                            <div class="pillow_fade">
                            	<div class="discovery_twitterAccounts_rightbar_radio_list">
                                	<div class="twt_user_photo">
                                    	<img src="../Contents/Images/00031502_normal.jpg" alt="" />
                                    </div>
                                    <div class="center">
                                    	<h4> Search for keywords or phrases that your customers would be talking about.</h4>
                                        <span class="subheader">@example: 'I want a latte'</span>
                                    </div>
                                    <span class="radio_button selected"></span>
                                </div>
                                
                                <div class="discovery_twitterAccounts_rightbar_radio_list">
                                	<div class="twt_user_photo">
                                    	<img src="../Contents/Images/00031502_normal.jpg" alt="" />
                                    </div>
                                    <div class="center">
                                    	<h4> Search for keywords that may be found in the profile of a potential customer.</h4>
                                        <span class="subheader">example: 'latte lover'</span>
                                    </div>
                                    <span class="radio_button selected"></span>
                                </div>
                                
                                
                            </div>
                        </div>
                      
                      <div class="feeds_right">
                        	<h3 class="section_sub_ttl">Recent Searches</h3>
                            <div class="pillow_fade">
                                <div id="RecentKey"  runat="server" class="messages"></div>
                                                   
                            </div>
                        </div>
                           
                    </div>
                   </div>                    
                </div>
            </div>


       

         
</asp:Content>
