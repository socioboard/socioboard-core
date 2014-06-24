<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true"
    CodeBehind="YourFollowers.aspx.cs" Inherits="WooSuite.Discovery.YourFollowers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js" type="text/javascript"></script>
    <script src="../Contents/Scripts/jquery.bpopup-0.9.3.min.js" type="text/javascript"></script>
    <script src="../Contents/Scripts/Message.js" type="text/javascript"></script>
    <script type="text/javascript">


        $(document).ready(function () {
            $("#home").removeClass('active');
            $("#message").removeClass('active');
            $("#feeds").removeClass('active');
            $("#discovery").addClass('active');
            var divid = $("#hdndivid").val()

            //            if (divid != "")
            //                alert("divid:" + divid);
            //            $(divid).attr('checked', true);
        });


        function PerformClick(id) {
           
            debugger;
            $("#<%=hdndivid.ClientID %>").val(id);
            var twtid = "";
            var twtaccountid = "";
            alert("id :" + id);
            debugger;
            twtaccountid = id.split('_')[1];
            twtid = id.split('_')[0];
            var i = id.split('_')[2];

            $("#<%=hdntwtaccountid.ClientID %>").val(twtaccountid);
            $("#<%=hdntwtid.ClientID %>").val(twtid);
            if (twtaccountid != "" && twtid != "") {
                $.ajax({
                    type: "POST",
                    url: "../Discovery/ajaxFollowers.aspx?op=followers&twtaccountid=" + twtaccountid + "&twtid=" + twtid,
                    data: '',
                    contentType: "application/json; charset=utf-8",
                    dataType: "html",
                    success: function (msg) {
                        $('#<%=divsuggestion.ClientID%>').html(msg);
                    }

                });

            }


            //            if (i == 1)
            //                document.getElementById('<%=btnchangestatus.ClientID %>').click();
        }


        function followersdivhide(divid) {
            debugger;
            try {
                $("#divfollowers_" + divid).css('display', 'none');
                $("#divfollowers_" + divid).hide();
            } catch (e) {
                alert(e);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .task_user_assign > ul > li
        {
            width: 100%;
        }
    </style>
    <div class="container_wrapper">
        <div class="ws_container_page">
            <aside role="complementary" class="unselectable" id="actions">
                        <ul class="msgs_nav">
                            <li class="accordion single">
                                <a href="YourFollowers.aspx">
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
                                	People who follow you You are not following these people
                                </div>
                               
                     </div>
                   <div class="smart_search" id="divsuggestion" runat="server">
                        <%-- <div class="messages">
                            <section>
                                <aside> 
                                    <section class="js-avatar_tip" data-sstip_class="twt_avatar_tip">
                                        <a class="avatar_link view_profile"> 
                                            <img width="54" height="54" border="0"  id="330238994946347008&gt;&lt;article class=" class="avatar" src="http://a0.twimg.com/profile_images/2877131555/d7589ee9ab915f6fc04d82eaaa4815b2_normal.jpeg">
                                            <span class="twitter_bm"><img width="16" height="16" src="../Contents/Images/twticon.png"></span>
                                        </a>
                                    </section>
                                    <ul></ul>
                                 </aside>
                                 <div class="yf_article">
                                    <div class="yf_div">
                                        <div class="dd">
                                            <ul class="top_actions band"> 
                                                <li><span class="add_usr_msg">👍</span></li>
                                                <li><span class="reply">↰</span></li> 
                                                <li><span class="gear_small">⚙</span></li>
                                           </ul>
                                           <h3 title="Amanda Goldstein" class="fn js-view_profile">
                                                <span>Amanda Goldstein</span> 
                                                <span class="screenname prof_meta"> @c6u1c66n040</span>
                                           </h3>
                                           <p class="note">Nothing seems to bring on an emergency as quickly as putting money aside in case of one.</p>
                                           <ul class="prof_meta"> 
                                                <li class="dim"><span class="loc_pointer_sm">📍</span>Hollins, United States</li>  
                                                <li class="dim"><span class="loc_pointer_sm">🌐</span>No URL</li> 
                                           </ul>

                                           <section class="profile_sub_wrap">
                                                <ul class="follow">
                                                    <li><span class="followers"><span>Followers</span> <span class="count">7 </span></span></li>
                                                    <li><span class="following"><span>Following</span> <span class="count">45 </span></span></li>
                                                </ul>
                                                <ul class="band_actions">
                                                    <li class="half"><a id="divhide" class="yf_button subtle js-hide_suggestion" href="javascript:void(0)">Hide</a></li>
                                                    <li class="half"><a class="yf_button subtle js-view_profile" href="javascript:void(0)">Full Profile»</a></li>
                                                </ul>
                                          </section>

                                        </div>
                                    </div>
                                  </div>
                                </section>
                        </div>--%>
                    </div>
               </section>
            </div>
            <div class="ws_msg_right">
                <div class="feeds_right">
                    <h3 class="section_sub_ttl">
                        Ideas for Searching</h3>
                    <div class="pillow_fade">
                        <div class="task_user_assign" id="divtwtprofiles" runat="server">
                            <ul style="width: 96%; background: none; border: none;">
                                <li>
                                    <%--<input type="radio" checked="checked" value="rdbtnteamtask" name="task"
                                        id="rdbtnteamtask" onclick="PerformClick(this.id)"/>
                                    <label for="rdbtnteamtask">
                                        Team task</label>--%>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdntwtaccountid" runat="server" />
    <asp:HiddenField ID="hdntwtid" runat="server" />
    <asp:Button ID="btnchangestatus" runat="server" Style="display: none;" OnClick="btn_Click" />
    <input id="hdndivid" type="hidden" runat="server" />
    <%-- <asp:Button ID="checkradio" runat="server" OnClick="checkradio_Click" />--%>
    <div id="userdetails" style="background-color: #FFFFFF; border-radius: 10px 10px 10px 10px;
        box-shadow: 0 0 25px 5px #999999; color: #111111; min-width: 1045px; padding: 25px;
        display: none;">
        <span class="close_button b-close"><span id="Span1">X</span></span>
        <div id="checkdetails" role="main">
            <section id="inbox1" class="threefourth messages msg_view" style="margin: 0;">
                <div id="details" class="messages taskable">
                </div>
             </section>
        </div>
    </div>
</asp:Content>
