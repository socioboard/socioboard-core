<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SocioBoard.Home" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
    <script src="Contents/Scripts/Home.js" type="text/javascript"></script>
    

<div class="ws_container_page">
        <div class="ws_left">
            <div class="title">PROFILES</div>
            <div class="title_connect" id="profiles" runat="server">Connected to globus</div>
            <div id="manageprofiles" class="social_connectivity">
              
            </div>
                      <div class="social_connectivity_add">
                <div class="ws_conct_add" id="expanderHead">
                    <a>+</a>
                </div>    
                <div id="expanderContent" style="display: none;">
                    <ul>
                        <li><a id="facebook_connect">
                            <img src="Contents/Images/fb_24X24.png" width="16" height="16" alt="" />
                            <span>Facebook</span> </a></li>
                        <li><a id="LinkedInLink"  runat="server" onserverclick="AuthenticateLinkedin">
                            <img src="Contents/Images/linked_25X24.png" width="16" height="16" alt="" />
                            <span>LinkedIn</span> </a></li>
                      <li><a id="TwitterOAuth" runat="server" onserverclick="AuthenticateTwitter">
                            <img src="Contents/Images/twt_icon.png" width="16" height="16" alt="" />
                            <span>Twitter</span> </a></li>
                       <%-- <li><a id="googleplus_connect" runat="server">
                            <img src="Contents/Images/google_plus.png" width="16" height="16" alt="" />
                            <span>Google Plus</span> </a></li>--%>
                      <%--  <li><a id="googleanalytics_connect" runat="server" href="#">
                            <img src="Contents/Images/google_analytics.png" width="16" height="16" alt="" />
                            <span>Google Analytics</span> </a></li>--%>
                        <li><a id="InstagramConnect"  runat="server" onserverclick="AuthenticateInstagram">
                            <img src="Contents/Images/instagram_24X24.png" width="16" height="16" alt="" />
                            <span>Instagram</span> </a></li>
                     

                    </ul>
                </div>
            </div>
            <div class="title">
                TEAM MEMBERS</div>
            <div class="title_connect" id="Team" runat="server">
                Managing globus</div>
            <div id="team_member" runat="server" class="social_connectivity">
           
            <%--    <div class="ws_conct">
                    <span class="img">
                        <img src="../Contents/Images/blank_img.png" width="48" height="48" alt="" />
                        <i></i></span>
                </div>
                <div class="ws_conct">
                    <span class="img">
                        <img src="../Contents/Images/blank_img.png" width="48" height="48" alt="" />
                        <i></i></span>
                </div>--%>
           
           
            </div>
            <div class="social_connectivity_add">
                <div class="ws_conct_add">
                  <a id="TeamMemConnect"  runat="server">+</a>
                </div>
            </div>
        </div>
        <div class="ws_mid">
            <div class="title_connect">
                <div class="mid_title">
                   Audience Demographics</div>
                <div class="ws_mid_title_connect" id="demograp" runat="server">
                   </div>
            </div>
            <div class="ws_graph">
                <%--<div id="pageName" runat="server" class="graph_title">Page Title</div>--%>
              <div  id="chart_div"></div>
                <div id="chart_div1"></div>
                <div id="chart_div_twt"></div>
               <img src="Contents/Images/graph.png"  alt="" />
            </div>
            <div class="title_connect">
                <div class="mid_title">
                    MY SOCIAL PROFILES</div>
                <div class="ws_mid_title_connect">
                    Snapshots of your connected accounts</div>
            </div>
            
            <div id="midsnaps" class="ws_mid_snapshots_div">
        
            </div>
        </div>
        <div class="ws_right">
            <div class="ws_connect">
                <div class="connect_profile">CONNECT A PROFILE</div>
                <div class="select_content">Select a Profile below to connect to Brandzter</div>
                <div class="social_network_links">
                    <a id="twt_cont" runat="server" href="#" onserverclick="AuthenticateTwitter"><img src="../Contents/Images/twt_icon.png" width="36" height="36" alt="" /></a>
                    <a id="fb_cont" href="#"  ><img src="../Contents/Images/fb_icon.png" width="36" height="36" alt="" /></a>
                    <a id="link_cont" runat="server" href="#" onserverclick="AuthenticateLinkedin"><img src="../Contents/Images/link_icon.png" width="36" height="36" alt=""  /></a>
                   <%-- <a id="gp_cont" runat="server" href="#"><img src="../Contents/Images/google_plus.png" width="36" height="36" alt="" /></a>--%>
                    <%--<a id="ga_cont" runat="server" href="#"><img src="../Contents/Images/google_analytics.png" width="36" height="36" alt="" /></a>--%>
                    <a id="inst_cont" runat="server" href="#" onserverclick="AuthenticateInstagram"><img src="../Contents/Images/instagram_24X24.png" width="36" height="36" alt="" /></a>
                </div>
                <div id="usedAccount" runat="server" class="use_list">Using 3 of 50</div>
            </div>
        </div>
    </div>
    <div id="fbConnect" class="web_dialog" style="display:none;">
          <li>
            <a id="fb_account" runat="server" onserverclick="AuthenticateFacebook">
                <img src="Contents/Images/fb_24X24.png" width="16" height="16" alt="" />
                <span>Facebook Account</span> 
            </a>
          </li>
          <li>
            <a id="fb_pages" runat="server" >
              <img src="Contents/Images/fb_24X24.png" width="16" height="16" alt="" />
                <span>Fan Page</span> 
            </a>
         </li>
          <div id="fbpage" runat="server"></div>        
            <input id="btnSubmit" type="button" value="Cancel" />
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
</script>
</asp:Content>
