<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true"
    CodeBehind="Group1.aspx.cs" Inherits="SocioBoard.Group.Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <div><%=groupname%></div>
   <%-- <div class="ws_container_page">
        <div class="ws_left">
            <div class="title">
                PROFILES</div>
            <div class="title_connect" id="MainContent_profiles">
                Connected To Praveen kumar</div>
            <div class="social_connectivity" id="manageprofiles">
            </div>
            <div class="social_connectivity_add">
                <div id="expanderHead" class="ws_conct_add">
                    <a>+</a>
                </div>
                <div style="display: none;" id="expanderContent">
                    <ul>
                        <li><a id="facebook_connect">
                            <img width="16" height="16" alt="" src="Contents/Images/fb_24X24.png">
                            <span>Facebook</span> </a></li>
                        <li><a href="javascript:__doPostBack('ctl00$MainContent$LinkedInLink','')" id="MainContent_LinkedInLink">
                            <img width="16" height="16" alt="" src="Contents/Images/linked_25X24.png">
                            <span>LinkedIn</span> </a></li>
                        <li><a href="javascript:__doPostBack('ctl00$MainContent$TwitterOAuth','')" id="MainContent_TwitterOAuth">
                            <img width="16" height="16" alt="" src="Contents/Images/twt_icon.png">
                            <span>Twitter</span> </a></li>
                        <li><a href="javascript:__doPostBack('ctl00$MainContent$InstagramConnect','')" id="MainContent_InstagramConnect">
                            <img width="16" height="16" alt="" src="Contents/Images/instagram_24X24.png">
                            <span>Instagram</span> </a></li>
                    </ul>
                </div>
            </div>
            <div class="title">
                TEAM MEMBERS</div>
            <div class="title_connect" id="MainContent_Team">
                Managing globus</div>
            <div class="social_connectivity" id="MainContent_team_member">
            </div>
            <div class="social_connectivity_add">
                <div class="ws_conct_add">
                    <a id="MainContent_TeamMemConnect">+</a>
                </div>
            </div>
        </div>
        <div class="ws_mid">
            <div class="title_connect">
                <div class="mid_title">
                    Audience Demographics</div>
                <div class="ws_mid_title_connect" id="MainContent_demograp">
                </div>
            </div>
            <div id="groupName" class="ws_graph" runat="server">
                <div class="title">
                    PROFILES</div>
            </div>
            <div class="ws_graph groupfeed">
                
            </div>
            <div class="title_connect">
                <div class="mid_title">
                    MY SOCIAL PROFILES</div>
                <div class="ws_mid_title_connect">
                    Snapshots of your connected accounts</div>
            </div>
            <div class="ws_mid_snapshots_div" id="midsnaps">
                <div class="col_three_linkedin" id="midsnap_rGyGZpCuw7">
                    <div class="col_three_link_my_accounts">
                        <div class="dt">
                            <a class="img">
                                <img width="48" height="48" alt="" src="http://m.c.lnkd.licdn.com/mpr/mprx/0_ncgMlQGpIBpKrINpN-0olLhjwt7AtDzpNrW6lLP3NBUhSWGyVASv05Ff5pffAoqrBqxbYCTXfHaP"></a><span
                                    class="icon"></span></div>
                        <div class="dd">
                            <h5>
                                mayankkrishna</h5>
                            <div class="friends_avg">
                                <div class="article_friends">
                                    <div class="facebook_blue">
                                        158</div>
                                    <div class="font-10">
                                        Friends</div>
                                </div>
                                <div class="article_avg">
                                    <div class="facebook_blue">
                                        0.91</div>
                                    <div class="font-10">
                                        Avg. Posts per Day</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="pillow_fade">
                        <div class="fb_notifications">
                            Recent Messages</div>
                        <div class="empty-state">
                            <strong>
                                <img src="http://m.c.lnkd.licdn.com/mpr/mprx/0_zR-8K5lnKAerGliYzJC0K6GRrlDrGlCYcM60KFPQftJhjzvOMjGuObFeOJSfTn_tq4rYx8DGw7XK">http://lnkd.in/dH-SZ...
                            </strong>
                            <br>
                            <strong>
                                <img src="http://m.c.lnkd.licdn.com/mpr/mprx/0_zR-8K5lnKAerGliYzJC0K6GRrlDrGlCYcM60KFPQftJhjzvOMjGuObFeOJSfTn_tq4rYx8DGw7XK">http://lnkd.in/dqMuc...
                            </strong>
                            <br>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="ws_right">
            <div class="ws_connect">
                <div class="connect_profile">
                    CONNECT A PROFILE</div>
                <div class="select_content">
                    Select a Profile below to connect to Brandzter</div>
                <div class="social_network_links">
                    <a href="javascript:__doPostBack('ctl00$MainContent$twt_cont','')" id="MainContent_twt_cont">
                        <img width="36" height="36" alt="" src="../Contents/Images/twt_icon.png"></a>
                    <a href="#" id="fb_cont">
                        <img width="36" height="36" alt="" src="../Contents/Images/fb_icon.png"></a>
                    <a href="javascript:__doPostBack('ctl00$MainContent$link_cont','')" id="MainContent_link_cont">
                        <img width="36" height="36" alt="" src="../Contents/Images/link_icon.png"></a>
                    <a href="javascript:__doPostBack('ctl00$MainContent$inst_cont','')" id="MainContent_inst_cont">
                        <img width="36" height="36" alt="" src="../Contents/Images/instagram_24X24.png"></a>
                </div>
                <div class="use_list" id="MainContent_usedAccount">
                    17 of 20</div>
            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {


            $("#home").addClass('active');

            $("#facebook_connect").click(function (e) {
                debugger;
                ShowFacebookDialog(false);
                e.preventDefault();
            });

            $("#fb_cont").click(function (e) {
                ShowFacebookDialog(false);
                e.preventDefault();
            });




            $("#expanderHead").click(function (event) {
                debugger;
                $("#expanderContent").slideToggle();
                //  event.stopPropagation();
            });
            $("#btnSubmit").click(function (e) {
                HideFacebookDialog();
                e.preventDefault();
            });

            BindSocialProfiles();
            BindMidSnaps("load");
            $(document).on('scroll', onScroll);
            //   bindProfilesComposeMessage();


        });
    </script>--%>
</asp:Content>
