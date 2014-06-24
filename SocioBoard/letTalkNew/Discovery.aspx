<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true" CodeBehind="Discovery.aspx.cs" Inherits="letTalkNew.Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style type="text/css">
    #inbox_msgs {display: block; width:85%; margin:0 auto;}
    #inbox_msgs li{list-style:none;}
</style>
        <nav>
                <ul id="tabs">
                      <li><a class="current" href="#">Smart Search </a></li>
                      <li><a href="#">Suggestion</a></li>
                </ul>
            <span id="indicator"></span> 
        </nav>
          <div id="content">
                <section>
                    <div id="contentcontainer1-message">
                          <div id="content" class="disco-content">
                                <div class="smart_search">
                                      <div class="title"> Discovery allows you to find new customers by searching for keywords around your business. </div>
                                      <div class="input_text_select">
                                           <asp:TextBox ID="txtSearchText" runat="server"></asp:TextBox>
                                           <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" ></asp:Button>              
                                      </div>
                                      <div id="searchresults" class="messages" runat="server">
                                            <div id="DivAll"></div>
                                            <div id="TwtResults1" runat="server"></div>
                                            <div id="FbResults1" runat="server"></div>
                                      </div>
                                </div>
                          </div>
                   </div>
               </section>
                
               <section> 
                    <ul class="sub_name" id="twitterprofilesoffeed">
                    	<%--<li><img src="../../Contents/img//fbicon_new.png"><a href="#">Name-1</a></li>
                        <li><img src="../../Contents/img//fbicon_new.png"><a href="#">Name-2</a></li>
                        <li><img src="../../Contents/img//fbicon_new.png"><a href="#">Name-3</a></li>--%>
                    </ul>
          	        <div id="contentcontainer1-message">      
                        <div id="inbox_msgs" class="msg_view">
                            <li class="shadower">
                                <div class="disco-feeds">
                                    <div class="star-ribbon"></div>
                                    <div class="disco-feeds-img"><img alt="" src="http://a0.twimg.com/profile_images/3282829484/9ec23a02aac4edebde0f3638ff735962_normal.jpeg" style="height: 100px; width: 100px;" class="pull-left"></div>
                                    <div class="disco-feeds-content">
                                        <div class="disco-feeds-title">
                                            <h3 class="no-margin">Dr. S. S. Rathore</h3>
                                            <span>@DrRathore</span>
                                       </div>
                                       <p>What I want to do before I die?... I want to live.</p>
                                       <a href="#" onclick="detailsprofile('296255865')" class="btn">Full Profile <i class="icon-caret-right"></i> </a>
                                       <%--<div class="scl">
                                            <a href="#"><img alt="" src="../Contents/img/admin/usergrey.png" alt="" /></a>
                                            <a href="#"><img alt="" src="../Contents/img/admin/goto.png"></a>
                                            <a href="#"><img alt="" src="../Contents/img/admin/setting.png"></a>
                                       </div>--%>
                                    </div>
                                 </div>
                                <div class="disco-feeds-info">
                                    <ul class="no-margin">
                                        <li><a href="#"><img src="../Contents/img/admin/markerbtn2.png" alt="" />Hawaii</a></li>
                                        <li><a target="_blank" href="http://t.co/8f6JUdzsr0"><img src="../Contents/img/admin/url.png" alt="" />http://t.co/8f6JUdzsr0</a></li>
                                    </ul>
                                    <ul class="no-margin" style="margin-top:20px">
                                        <li><a href="#"><img src="../Contents/img/admin/twittericon-white.png" alt="">Followers <big><b>901</b></big></a></li>
                                        <li><a href="#"><img src="../Contents/img/admin/twitter-white.png" alt="">Following <big><b>1686</b></big></a></li>
                                   </ul>
                                </div>
                            </li>
                        </div>
                    </div>
               </section>
        </div>
        <script type="text/javascript" language="javascript" >
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
