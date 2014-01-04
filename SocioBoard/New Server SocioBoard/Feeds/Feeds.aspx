<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true"
    CodeBehind="Feeds.aspx.cs" Inherits="SocialSuitePro.Feeds.Feeds" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Contents/css/jquery.mCustomScrollbar.css" rel="stylesheet" type="text/css" />
    <script src="../Contents/js/jquery.mCustomScrollbar.concat.min.js" type="text/javascript"></script>
    <script src="../Contents/js/jquery.lazyscrollloading-src.js" type="text/javascript"></script>
    <script src="../Contents/js/jquery.easing.1.3.js" type="text/javascript"></script>
    <style type="text/css">
        p.commeent_box> .put_comments{display:none;}
        p.commeent_box.active > .put_comments{display:block;}
    </style>
    <!--script type="text/javascript">

        $(document).ready(function () {
            $('.comment').click(function () {
                $('p.commeent_box').removeClass('active');
                $(this).addClass('active');
            });
        });
    </script-->
    <div class="container reports" id="mainwrapper">
        <div class="feeds" id="sidebar">
            <div class="sidebar-inner">
                <div class="accordion" id="accordion2">
                    <div class="accordion-group">
                        <div class="accordion-heading">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseOne"
                                onclick="BindProfilesforNetwork('facebook');">
                                <img alt="" src="../Contents/img/admin/1.png" class="fesim">FACEBOOK <i class="icon-sort-down pull-right hidden">
                                </i></a>
                        </div>
                        <div id="collapseOne" class="accordion-body in collapse">
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
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseFour"
                                onclick="BindProfilesforNetwork('instagram');">
                                <img alt="" src="../Contents/img/admin/4.png" class="fesim">INSTAGRAM <i class="icon-sort-down pull-right hidden">
                                </i></a>
                        </div>
                        <div id="collapseFour" class="accordion-body collapse">
                            <div class="accordion-inner">
                                <ul id="instagramprofilesforfeed">
                                    <li><a href="#" class="active">
                                        <img src="../Contents/img/891.png" alt="" /></a> </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <%--         <div class="accordion-group">
                        <div class="accordion-heading">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseFive">
                                <img alt="" src="../Contents/img/admin/3.png" class="fesim">GOOGLE + <i class="icon-sort-down pull-right hidden">
                                </i></a>
                        </div>
                        <div id="collapseFive" class="accordion-body collapse">
                            <div class="accordion-inner">
                                <ul>
                                    <li><a href="#">Link 1</a> </li>
                                    <li><a href="#">Link 2</a> </li>
                                    <li><a href="#">Link 3</a> </li>
                                </ul>
                            </div>
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>
        <div id="contentcontainer-feeds">
            <div id="content">
                <div id="instag" class="row-fluid">
                    <div id="paneltab1" class="span4 rounder shadower whitebg feedwrap">
                        <div class="feedwraptitle rounder">
                            <img id="img_paneltab1" class="pull-left" alt="" src="../Contents/img/891.png" />
                            <div id="title_paneltab1" class="feedtitlename">
                                <h6>
                                  </h6>
                                
                            </div>
                            <div class="feedreficon">
                                <a href="#" onclick="">
                                    <img id="loader_tabpanel1" alt="" src="../Contents/img/891.png" /></a>
                            </div>
                        </div>
                        <ul class="mCustomScrollbar _mCS_1">
                            <div style="position: relative; height: 100%; overflow: hidden; max-width: 100%;"
                                id="mCSB_1" class="mCustomScrollBox mCS-light">
                                <div id="data_paneltab1" style="position: relative;" class="mCSB_container">
                                </div>
                                <div style="display: none;" class="mCSB_scrollTools">
                                    <div class="mCSB_draggerContainer">
                                        <div oncontextmenu="return false;" style="position: absolute; height: 388px; top: 49px;"
                                            class="mCSB_dragger">
                                            <div style="position: relative; line-height: 388px;" class="mCSB_dragger_bar">
                                            </div>
                                        </div>
                                        <div class="mCSB_draggerRail">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ul>
                    </div>
                    <div id="paneltab2" class="span4 rounder shadower whitebg feedwrap">
                        <div class="feedwraptitle rounder">
                            <img id="img_paneltab2" class="pull-left" alt="" src="../Contents/img/891.png">
                            <div id="title_paneltab2" class="feedtitlename">
                                <h6>
                                   </h6>
                             
                            </div>
                            <div class="feedreficon">
                                <a href="#" onclick="">
                                    <img id="loader_tabpanel2" alt="" src="../Contents/img/891.png"></a>
                            </div>
                        </div>
                        <ul class="mCustomScrollbar _mCS_1">
                            <div style="position: relative; height: 100%; overflow: hidden; max-width: 100%;"
                                id="Div1" class="mCustomScrollBox mCS-light">
                                <div id="data_paneltab2" style="position: relative;" class="mCSB_container">
                                </div>
                                <div style="display: none;" class="mCSB_scrollTools">
                                    <div class="mCSB_draggerContainer">
                                        <div oncontextmenu="return false;" style="position: absolute; height: 388px; top: 49px;"
                                            class="mCSB_dragger">
                                            <div style="position: relative; line-height: 388px;" class="mCSB_dragger_bar">
                                            </div>
                                        </div>
                                        <div class="mCSB_draggerRail">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ul>
                    </div>
                    <div id="paneltab3" class="span4 rounder shadower whitebg feedwrap">
                        <div class="feedwraptitle rounder">
                            <img id="img_paneltab3" class="pull-left" alt="" src="../Contents/img/891.png">
                            <div id="title_paneltab3" class="feedtitlename">
                                <h6>
                                   </h6>
                                
                            </div>
                            <div class="feedreficon">
                                <a href="#" onclick="">
                                    <img id="loader_tabpanel3" alt="" src="../Contents/img/891.png"></a>
                            </div>
                        </div>
                        <ul class="mCustomScrollbar _mCS_1">
                            <div style="position: relative; height: 100%; overflow: hidden; max-width: 100%;"
                                id="Div2" class="mCustomScrollBox mCS-light">
                                <div id="data_paneltab3" style="position: relative;" class="mCSB_container">
                                </div>
                                <div style="display: none;" class="mCSB_scrollTools">
                                    <div class="mCSB_draggerContainer">
                                        <div oncontextmenu="return false;" style="position: absolute; height: 388px; top: 49px;"
                                            class="mCSB_dragger">
                                            <div style="position: relative; line-height: 388px;" class="mCSB_dragger_bar">
                                            </div>
                                        </div>
                                        <div class="mCSB_draggerRail">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--popup for image--%>
    <div id="facebookImagePopup" style="background-color: #FFFFFF; border-radius: 10px 10px 10px 10px;
        box-shadow: 0 0 25px 5px #999999; color: #111111; display: none; min-width: 100px;
        padding: 25px; min-height: 100px;">
    <span class="button b-close" onclick="fbimageclose();"><span>X</span></span>
     
     <img id="popupimage" alt="" src="" style="min-height:43px;min-width:43px;" />
    </div>

    <script src="../Contents/js/jlitebox/js/jquery.lightbox-0.5.js" type="text/javascript"></script>
    <link href="../Contents/js/jlitebox/css/jquery.lightbox-0.5.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            $("#home").removeClass('active');
            $("#message").removeClass('active');
            $("#feeds").addClass('active');
            $("#discovery").removeClass('active');
            $("#publishing").removeClass('active');
            try {
                BindProfilesforNetwork('facebook');







            } catch (e) {

            }

         






            try {

                $('.accordion-toggle').click(function () {
                    $('.accordion-toggle i').addClass("hidden");
                    $(this).children("i").toggleClass("hidden");
                    //$(".accordion-toggle .collapsed").removeClass("hidden");
                });
            } catch (e) {

            }
            $(window).load(function () {
                try {
                    $(".feedwrap > ul").mCustomScrollbar({
                        scrollEasing: "easeOutCirc",
                        mouseWheel: "auto",
                        autoDraggerLength: true,
                        advanced: {
                            updateOnBrowserResize: true,
                            updateOnContentResize: true
                        }
                    });
                } catch (e) {

                }
            });






        });

    </script>
    <%--<script src="../Contents/js/Feeds.js" type="text/javascript"></script>--%>
</asp:Content>
