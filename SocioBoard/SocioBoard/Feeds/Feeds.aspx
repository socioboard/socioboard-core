<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true"
    CodeBehind="Feeds.aspx.cs" Inherits="SocialSuitePro.Feeds.Feeds" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Contents/css/jquery.mCustomScrollbar.css" rel="stylesheet" type="text/css" />
    <script src="../Contents/js/jquery.mCustomScrollbar.concat.min.js" type="text/javascript"></script>
    <script src="../Contents/js/jquery.lazyscrollloading-src.js" type="text/javascript"></script>
    <script src="../Contents/js/jquery.easing.1.3.js" type="text/javascript"></script>

    <script src="../Contents/js/bootstrap-dropdown.js" type="text/javascript"></script>
    <script src="../Contents/js/jquery.bpopup-0.9.3.min.js" type="text/javascript"></script>
    <script src="../Contents/js/bootstrap.min.js" type="text/javascript"></script>

    <style type="text/css">
        p.commeent_box > .put_comments
        {
            display: none;
        }
        p.commeent_box.active > .put_comments
        {
            display: block;
        }
        .video-containers
        {
            position: relative;
            padding-bottom: 4%;
            padding-top: 10px;
            height: 200px;
            overflow: hidden;
        }
        
        .video-containers iframe, .video-container object, .video-container embed
        {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
        }
        
        .top_select
        {
            position: static;
        }
        .yt_details
        {
            width: 1000px;
        }
        
        .yt_title
        {
            padding: 10px;
            background: none repeat scroll 0 0 rgb(241, 92, 57);
            white-space: nowrap;
            text-overflow: ellipsis;
            overflow: hidden;
            height:10px;
            color:#fff;
             border-radius: 3px;
             text-align:center;
        }
        .yt_description
        {
            padding: 10px;
            
            white-space: nowrap;
            text-overflow: ellipsis;
            overflow: hidden;
            height:20px;
            color:#000;
        }
        
        
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
                                onclick="BindProfilesforNetworkOnly('facebook');">
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
                    <div class="accordion-group">
                        <div class="accordion-heading">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseFive"
                                onclick="BindProfilesforNetwork('tumblr');">
                                <img alt="" src="../Contents/img/tumblr.png" class="fesim">TUMBLR<i class="icon-sort-down pull-right hidden">
                                </i></a>
                        </div>
                        <div id="collapseFive" class="accordion-body collapse">
                            <div class="accordion-inner">
                                <ul id="tumblrprofilesforfeed">
                                    <li><a href="#" class="active">
                                        <img src="../Contents/img/891.png" alt="" /></a> </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="accordion-group">
                        <div class="accordion-heading">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseSix"
                                onclick="BindProfilesforNetwork('youtube');">
                                <img alt="" src="../Contents/img/youtube.png" class="fesim">YOUTUBE<i class="icon-sort-down pull-right hidden">
                                </i></a>
                        </div>
                        <div id="collapseSix" class="accordion-body collapse">
                            <div class="accordion-inner">
                                <ul id="youtubeprofilesforfeed">
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
        <div id="contentcontainer-feeds" style="width: 980px;">
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
        <img id="popupimage" alt="" src="" style="min-height: 43px; min-width: 43px;" />
    </div>
    <div id="tumblrImagePopup" style="background-color: #FFFFFF; border-radius: 10px 10px 10px 10px;
        box-shadow: 0 0 25px 5px #999999; color: #111111; display: none; min-width: 100px;
        padding: 25px; min-height: 100px;">
        <span class="button b-close" onclick="tumblrimageclose();"><span>X</span></span>
        <img id="popupimages" alt="" src="" style="min-height: 43px; min-width: 43px; max-height: 500px;
            max-width: 700px;" />
    </div>
    <div id="youtubeImagePopup" style="background-color: #FFFFFF; border-radius: 10px 10px 10px 10px;
        box-shadow: 0 0 25px 5px #999999; color: #111111; display: none; min-width: 100px;
        padding: 25px; min-height: 100px;">
        <span class="button b-close" onclick="tumblrimageclose();"><span>X</span></span>
        <iframe id="popupimagesyt" width="420" height="345" src=""></iframe>
    </div>



  <div id="tumblrcontent" style="background-color: #FFFFFF; border-radius: 10px 10px 10px 10px;
        box-shadow: 0 0 25px 5px #999999; color: #111111; display: none; min-width: 100px;
        padding: 25px; min-height: 100px;">
        <span class="close_button b-close"><span id="Span4">X</span></span>
        <div class="search_title" style="color:#fb6947;font-size: 25px;
    font-weight: bold;">
            Tumblr Post</div>
       <div class="span10" id="text">
       <!-- Nav tabs -->
<ul class="nav nav-tabs" role="tablist">
  <li class="active"><a href="#Text" role="tab" data-toggle="tab">Text</a></li>
  <li><a href="#Photo" role="tab" data-toggle="tab">Photo</a></li>
  <li><a href="#Quotes" role="tab" data-toggle="tab">Quote</a></li>
  <li><a href="#Link" role="tab" data-toggle="tab">Link</a></li>
    <li><a href="#Chat" role="tab" data-toggle="tab">Chat</a></li>
      <%--<li><a href="#Video" role="tab" data-toggle="tab">Video</a></li>--%>
    <li><a href="#audio" role="tab" data-toggle="tab">Audio</a></li>
    
</ul>

<!-- Tab panes -->
<div class="tab-content">
  <div class="tab-pane active" id="Text">
  <div class="span6" style="border: 1px solid rgb(204, 204, 204); padding: 10px;">
  <form action="#">
                        
                        <div class="form-group">
                        <label class="control-label">Blog :</label>
                         <select class="form-control" id="blog">
                          <option>Text</option>
                         </select>
                        </div>
                        <div class="form-group">
                           <label class="control-label">Title :</label>
                          <input type="text" class="form-control" placeholder="Enter Title Here" id="inputTextTitle">
                        </div>
                        <div class="form-group">
                         <label class="control-label">Body :</label>
                          <textarea class="form-control" placeholder="Write your message.." id="inputTextMessage"></textarea>
                        </div>
                      <%--  <div class="form-group">
                           <label class="control-label">Tags :</label>
                          <input type="text" class="form-control" placeholder="" id="inputTitle">
                        </div>--%>
                        <div class="form-group">
                          <button class="btn btn-success pull-right" id=textPost type="submit" onclick="postTextOnTumblr();">Post</button>
                        </div>
                      </form>
  </div>
  </div>
  <div class="tab-pane" id="Photo">
  <div class="span6" style="border: 1px solid rgb(204, 204, 204); padding: 10px;">
  <form action="#">
                       
                        <div class="form-group">
                        <label class="control-label">Blog :</label>
                         <select class="form-control" id="Select2">
                          <option>Photo</option>
                         </select>
                        </div>
                        <div class="form-group">
                           <label class="control-label">Caption :</label>
                          <input type="text" class="form-control" placeholder="Enter Caption Here" id="txtCaption">
                        </div>
                        <div class="form-group">
                         <label class="control-label">File to share :</label>
                          <input type="file"  id="txtUrl"/>
                        </div>
                      <%--  <div class="form-group">
                           <label class="control-label">Tags :</label>
                          <input type="text" class="form-control" placeholder="" id="Password2">
                        </div>--%>
                        <div class="form-group">
                          <button class="btn btn-success pull-right" type="submit" onclick="postPhotoOnTumblr();" >Post</button>
                        </div>
                      </form>
  </div></div>
  <div class="tab-pane" id="Video">
  <div class="span6" style="border: 1px solid rgb(204, 204, 204); padding: 10px;">
  <form action="#">
  <ul class="nav nav-tabs" role="tablist">
  <li class="active"><a href="#Search" role="tab" data-toggle="tab">Embed Code:</a></li>
  <li><a href="#Upload" role="tab" data-toggle="tab">File to share</a></li>

</ul>
<!-- Tab panes -->
<div class="tab-content">
  <div class="tab-pane active" id="Search"> <div class="form-group">
                           <label class="control-label">Embed Code:</label>

                                <textarea class="form-control" placeholder="Embed Code Here.." id="txtVideoUrl"></textarea>
                                <input type="text" class="form-control" placeholder="Enter Caption Here" id="txtVideoContent">

                        </div>
                        </div>
  <div class="tab-pane" id="Upload"> 
   <div class="form-group">
                         <label class="control-label">File to share :</label>
                          <input type="file"  id="fileVideo"/>
                        </div>
   </div>
   </div>
                      <%--  <div class="form-group">
                           <label class="control-label">Tags :</label>
                          <input type="text" class="form-control" placeholder="" id="Password2">
                        </div>--%>
                        <div class="form-group">
                          <button class="btn btn-success pull-right" type="submit" onclick="postVideoOnTumblr();" >Post</button>
                        </div>
                      </form>
  </div></div>
  <div class="tab-pane" id="audio">
  <div class="span6" style="border: 1px solid rgb(204, 204, 204); padding: 10px;">
  <form action="#">
                       <!-- Nav tabs -->
<ul class="nav nav-tabs" role="tablist">
  <li class="active"><a href="#Searchs" role="tab" data-toggle="tab">Search</a></li>
  <li><a href="#Uploads" role="tab" data-toggle="tab">Upload</a></li>
  <li><a href="#url" role="tab" data-toggle="tab">External URL</a></li>

</ul>

<!-- Tab panes -->
<div class="tab-content">
  <div class="tab-pane active" id="Searchs"><div class="form-group">
                         
                          <input type="text" class="form-control" placeholder="Enter Track Here" id="Text3">
                        </div>
                        </div>
  <div class="tab-pane" id="Uploads"> 
   <div class="form-group">
                         <label class="control-label">File to share :</label>
                          <input type="file"  id="fileAudio"/>
                        </div>
   </div>
  <div class="tab-pane" id="url">
  <div class="form-group">
                         
                          <input type="text" class="form-control" placeholder="http://" id="Text4">
                        </div>
  </div>

</div>
<div class="form-group">
                          <button class="btn btn-success pull-right" type="submit" onclick="postAudioOnTumblr();" >Post</button>
                        </div>

                      </form>
  </div>
  </div>
  <div class="tab-pane" id="Chat">
  <div class="span6" style="border: 1px solid rgb(204, 204, 204); padding: 10px;"
  <form action="#">
                        
                        <div class="form-group">
                           <label class="control-label">Title :</label>
                          <input type="text" class="form-control" placeholder="Enter Title Here" id="txtChatTitle">
                        </div>
                        <div class="form-group">
                         <label class="control-label">Body :</label>
                          <textarea class="form-control" placeholder="Write your message.." id="txtChatBody"></textarea>
                        </div>

                        <div class="form-group">
                           <label class="control-label">Tags :</label>
                          <input type="text" class="form-control" placeholder="" id="txtChatTag">
                        </div>
                        <div class="form-group">
                          <button class="btn btn-success pull-right" id=Button1 type="submit" onclick="postChatOnTumblr();">Post</button>
                        </div>
                      </form>
  </div>
  </div>
  <div class="tab-pane" id="Link">
  <div class="span6" style="border: 1px solid rgb(204, 204, 204); padding: 10px;">
  <form action="#">
                        
                        <div class="form-group">
                        <label class="control-label">Url :</label>
                          <input type="text" class="form-control" placeholder="" id="txtUrls">
                        </div>
                        <div class="form-group">
                           <label class="control-label">Title :</label>
                          <input type="text" class="form-control" placeholder="" id="txtTitle">
                        </div>
                        <div class="form-group">
                         <label class="control-label">Description :</label>
                          <textarea class="form-control" placeholder="Write your message.." id="txtDescription"></textarea>
                        </div>
                        
                        <div class="form-group">
                          <button class="btn btn-success pull-right" type="submit" onclick="postLinkOnTumblr();">Post</button>
                        </div>
                      </form>
  </div>
  </div>
  <div class="tab-pane" id="Quotes">
  <div class="span6" style="border: 1px solid rgb(204, 204, 204); padding: 10px;">
  <form action="#">
                        
                        <div class="form-group">
                        <label class="control-label">Blog :</label>
                         <select class="form-control" id="Select4">
                          <option>Text</option>
                         </select>
                        </div>
                        <div class="form-group">
                           <label class="control-label">Title :</label>
                          <input type="text" class="form-control" placeholder="" id="txtQuote">
                        </div>
                        <div class="form-group">
                         <label class="control-label">Message :</label>
                          <textarea class="form-control" id="txtQuotesource" placeholder="Write your message.."></textarea>
                        </div>
                        <div class="form-group">
                           <label class="control-label">Tags :</label>
                          <input type="text" class="form-control" placeholder="" id="Password8">
                        </div>
                        <div class="form-group">
                          <button class="btn btn-success pull-right" type="submit" onclick="postQuoteOnTumblr();">Post</button>
                        </div>
                      </form>
  </div>
  </div>
</div>
       </div>
    </div>








    <script src="../Contents/js/jlitebox/js/jquery.lightbox-0.5.js" type="text/javascript"></script>
    <link href="../Contents/js/jlitebox/css/jquery.lightbox-0.5.css" rel="stylesheet"
        type="text/css" />
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




            //            try {

            //                $('.accordion-toggle').click(function () {
            //                    $('.accordion-toggle i').addClass("hidden");
            //                    $(this).children("i").toggleClass("hidden");
            //                    //$(".accordion-toggle .collapsed").removeClass("hidden");
            //                });
            //            } catch (e) {

            //            }
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
