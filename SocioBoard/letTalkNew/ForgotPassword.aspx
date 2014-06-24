<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="letTalkNew.ForgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
  <head>
    <title>Home Page</title>
    <script>
        (function () { if (!/*@cc_on!@*/0) return; var e = "abbr,article,aside,audio,bb,canvas,datagrid,datalist,details,dialog,eventsource,figure,footer,header,hgroup,mark,menu,meter,nav,output,progress,section,time,video".split(','), i = e.length; while (i--) { document.createElement(e[i]) } })()
</script>
    <!--[if lt Ie 9]>
        <script src="js/html5.js" type="text/javascript"></script>
    <![endif]-->

    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <link href="css/grid.css" rel="stylesheet" type="text/css" />
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link href="css/admin.css" rel="stylesheet" type="text/css" />

    <!--[if Ie 7]>
        <style type="text/css">
            nav > .menu > ul { width:610px; margin:0 auto;}
            nav > .menu > ul li {float:left;}
            .cirlce_chart{position:relative;}
            .cirlce_chart > .value{margin-top:35px; margin-left:15px; width:80px; text-align:center;}
            li{float:left;}
            section > .main > .section_top > .container_right > .graph > .social_graph ul > li > .social_activity_links > ul > li > .tabs > .scl_activity_links{margin-left:0;}
        </style>
    <![endif]-->

    <!--[if Ie 8]>
        <style type="text/css">            
            .cirlce_chart{position:relative;}
            .cirlce_chart > .value{margin-top:35px; margin-left:15px; width:80px; text-align:center;}
        </style>
    <![endif]-->

    <script src="Contents/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('.menu ul li a').click(function () {
                $('.menu ul li a').removeClass('active');
                $(this).addClass('active');
            });


            $('.mailbox_bot ul li').click(function () {
                $('.mailbox_bot ul li').removeClass('active');
                $(this).addClass('active');
            });
        });

    </script>
      
    <link rel="stylesheet" type="text/css" href="Contents/css/common.css">
		<link rel="stylesheet" type="text/css" href="Contents/css/mopTip-2.2.css">
		<script type="text/javascript" src="Contents/js/mopTip-2.2.js"></script>
        <script type="text/javascript" src="Contents/js/jquery.pngFix-1.2.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $("#demo1Btn").mopTip({ 'w': 150, 'style': "overOut", 'get': "#demo1" });
                $("#demo2Btn").mopTip({ 'w': 150, 'style': "overClick", 'get': "#demo2" });
                $("#demo3Btn").mopTip({ 'w': 150, 'style': "overOut", 'get': "#demo3" });
                $("#demo4Btn").mopTip({ 'w': 150, 'style': "overClick", 'get': "#demo4" });
                $("#demo5Btn").mopTip({ 'w': 150, 'style': "overOut", 'get': "#demo5" });
            });
		</script>
    </head>
    <body id="login_width">
    <form method="post" action="Default.aspx" id="ctl01">
      <div class="aspNetHidden">
        <input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="/wEPDwUKMTY1NDU2MTA1MmRkm9sNplIkyfmUgZccZXeD4hUQ0nuTVu3ch1RGpTFnUbM=" />
      </div>
      <div class="page">
      <header>
        <div class="header">
          <div class="letstalk_logo"> <a href="index.html"><img src="Contents/img/letstalk_logo.png" alt="" /></a> </div>
          <nav>
            <div class="menu">&nbsp;</div>
          </nav>
          <div class="logout"><a><img src="Contents/img/logout.png" alt="" /></a></div>
        </div>
      </header>
 </div>
    </form>
    
    <div id="login_width">
    	<div class="login_sub_width">
        	<div class="login_common_width">
            	<fieldset>
                	<h1>Forgot Your Login <em></em></h1>
                    <p class="forgot_text">After you provide the information requested below, Trend Micro will send a message explaining how to reset your password. </p>
                	<span>
                    	<label>Enter Email Address <img src="Contents/img/login_arrow.png"></label>
                        <input type="text" placeholder="Ex:demo@demo.com">
                    </span>
                    
                    <span>
                    	<input type="submit" value="Submit">
                    </span>
                </fieldset>
            </div>
        </div>
    </div>
</body>
</html>
