<%@ Page Language="C#" AutoEventWireup="true" Debug="true" CodeBehind="~/Default.aspx.cs" Inherits="letTalkNew.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
    <head>
    <title>Home Page</title>
    <script>
        (function () { if (!/*@cc_on!@*/0) return; var e = "abbr,article,aside,audio,bb,canvas,datagrid,datalist,details,dialog,eventsource,figure,footer,header,hgroup,mark,menu,meter,nav,output,progress,section,time,video".split(','), i = e.length; while (i--) { document.createElement(e[i]) } })()
</script>
    <!--[if lt Ie 9]>
        <script src="js/html5.js" type="text/javascript"></script>
    <![endif]-->

    <link href="../Contents/css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Contents/css/grid.css" rel="stylesheet" type="text/css" />
    <link href="../Contents/css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../Contents/css/admin.css" rel="stylesheet" type="text/css" />

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

    <script src="../Contents/js/jquery.min.js" type="text/javascript"></script>
        <script src="Contents/js/JSlogin.js" type="text/javascript"></script>
        <script src="Contents/js/md5.js" type="text/javascript"></script>
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

    <style>
    .login_common_width fieldset span {
    float: left;
    margin: 10px 0;
    padding: 0;
    width: 100%;
}
    </style>
    </head>
    <body class="login_width">
    <form  runat="server">
      <div class="aspNetHidden">
      <%--  <input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="/wEPDwUKMTY1NDU2MTA1MmRkm9sNplIkyfmUgZccZXeD4hUQ0nuTVu3ch1RGpTFnUbM=" />--%>
      </div>
    <div class="page">
      <header>
        <div class="header">
          <div class="letstalk_logo"> <a href="Default.aspx"><img src="../Contents/img/letstalk_logo.png" alt="" /></a> </div>
          <nav>
            <div class="menu">&nbsp;</div>
          </nav>
      <%--    <div class="logout"><a><img src="Contents/img/logout.png" alt="" /></a></div>--%>
        </div>
      </header>
 </div>
  
    <div class="login_width">
    	<div class="login_sub_width">
        	<div class="login_common_width">
            	<fieldset>
                	<h1>Login <em><a href="Registration.aspx">Join us now for free</a></em></h1>
                	<span>
                    	<label>EmailId<img src="Contents/img/login_arrow.png"></label>
                     <%--   <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>--%>
                      <input type="text" id="txtEmail" placeholder="Name"  onblur="checkEmail(this.value);" runat="server">
                      <div id="signinemailMessages"></div>
                    <%--<div style="display:none;" id="signinEmailError" class="input"></div>--%>
                    </span>
                    <span>
                    	<label>Password <img src="Contents/img/login_arrow.png"></label>
                    <%--    <asp:TextBox ID="txtPassword" runat="server" ></asp:TextBox>--%>
                    <input type="password" id="txtPassword" placeholder="****" runat="server">
                    <div id="signinpasswordMessages"></div>
                    </span>
                    <span>
                    	<input type="checkbox"> <em>Remember Me</em>
                        <a href="forgot.aspx">Forgot Your Login</a>
                    </span>
                    <span class="loader">
                      <%--  <asp:Button ID="btnSubmit" runat="server" Text="Submit" onclick="btnSubmit_Click" />--%>
                    	<input type="button" value="Submit" runat="server" onclick="signinFunction();" />
                    </span>
                </fieldset>
            </div>
        </div>
         <a href="#"onclick="facebookLogin();" id="facebook_login"><img src="Contents/img/facebookicon_login.png"></a>
    </div>
    </form>
</body>

<script type="text/javascript">
    $("#txtPassword").bind("keydown", function (event) {
        var keycode = (event.keyCode ? event.keyCode : (event.which ? event.which : event.charCode));
        if (keycode == 13) {
            signinFunction();
            return false;
        } else {
            return true;
        }
    });

</script>

</html>
