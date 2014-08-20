<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/socialsuite.Master" AutoEventWireup="true" CodeBehind="SpreadTheLove.aspx.cs" Inherits="SocioBoard.SpreadTheLove" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
.welcome-built h2{ text-align:center !important;}
.features-page-header{ text-align:center !important;}
.features-page {padding:45px 0 10px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="feature_body" id="p6">
            
            <div class="row-content">
                <div class="welcome-built" id="for-everyone">
                    <div class="features-page">
                        <h2>We know we are awesome!</h2>
                        <div class="features-page-header">
                        	Thanks for helping to Spread the word about Socioboard.
                            <br /><br />
                            
                            Spread the Love <br />

                            <div class="stl_social">
                            
                            <div class="fb-share-button" data-href="http://bit.ly/socioboard" data-type="button" class="faceshare freshbutton"></div>
                            <a href="https://twitter.com/share" class="twitter-share-button" data-url="http://bit.ly/socioboard" data-text="Hi, I love this new tool for generating sales on Social Media! Do give it a try!" data-count="none">Tweet</a>
                          
                            </div>
                        </div>
                    </div>
               </div>
            </div>
  </div>

  <div id="fb-root"></div>
<script>    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/all.js#xfbml=1";
        fjs.parentNode.insertBefore(js, fjs);
    } (document, 'script', 'facebook-jssdk'));</script>


    <script>        !function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + '://platform.twitter.com/widgets.js'; fjs.parentNode.insertBefore(js, fjs); } } (document, 'script', 'twitter-wjs');</script>
</asp:Content>
                                            