<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs"
    Inherits="letTalkNew.ResetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Home Page</title>
    <script>
        (function () { if (!/*@cc_on!@*/0) return; var e = "abbr,article,aside,audio,bb,canvas,datagrid,datalist,details,dialog,eventsource,figure,footer,header,hgroup,mark,menu,meter,nav,output,progress,section,time,video".split(','), i = e.length; while (i--) { document.createElement(e[i]) } })()
</script>
    <!--[if lt Ie 9]>
        <script src="js/html5.js" type="text/javascript"></script>
    <![endif]-->
    <link type="text/css" rel="stylesheet" href="../Contents/css/Style.css">
    <link type="text/css" rel="stylesheet" href="../Contents/css/grid.css">
    <link type="text/css" rel="stylesheet" href="../Contents/css/reset.css">
    <link type="text/css" rel="stylesheet" href="../Contents/css/admin.css">
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
    <script type="text/javascript" src="../Contents/js/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#txtpass").val('');

            $("#btnresetpass").click(function () {
                var pass = $("#txtpass").val();
                var cpass = $("#txtconfirmpass").val();
                if (pass == "" || cpass == "") {
                    $("#lblerror").text("Please enter Password and Confirmpassword");
                    return false;
                }
                if (pass != cpass) {
                    $("#lblerror").text("Password mismatch");
                    return false;
                }
            });









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
    <link href="../Contents/css/common.css" type="text/css" rel="stylesheet">
    <link href="../Contents/css/mopTip-2.2.css" type="text/css" rel="stylesheet">
    <script src="../Contents/js/mopTip-2.2.js" type="text/javascript"></script>
    <script src="../Contents/js/jquery.pngFix-1.2.js" type="text/javascript"></script>
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
<body class="login_width">
   <form id="form1" runat="server">
   
        <header>
            <div class="header">
                  <div class="letstalk_logo"> <a href="index.html"><img alt="" src="../Contents/img/letstalk_logo.png"></a> </div>
                  <nav>
                    <div class="menu">&nbsp;</div>
                  </nav>
                  <div class="logout"><a><img alt="" src="../Contents/img/logout.png"></a></div>
            </div>
        </header>

   
    
    <div id="login_width">
        <div class="login_sub_width">
            <div class="login_common_width">
                <fieldset>
                    <h1>
                        Reset Your Password <em></em>
                    </h1>
                    <p class="forgot_text">
                        After you provide the information requested below, Trend Micro will send a message
                        explaining how to reset your password.
                    </p>
                    <span>
                        <label>
                            Password
                            <img src="../Contents/img/login_arrow.png"></label>
                        <%-- <input id="txtpass" type="text" placeholder="Password" runat="server">--%>
                        <asp:TextBox ID="txtpass" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                    </span><span>
                        <label>
                            Confirm Password<img src="../Contents/img/login_arrow.png"></label>
                        <input id="txtconfirmpass" type="Password" placeholder="Confirm Password" value="">
                    </span>
                    <span>
                    <asp:Button type="submit" ID="btnResetPwd" runat="server" Text="Submit" OnClick="btnResetPwd_Click" />

                    </span>
                    <asp:Label ID="lblerror" runat="server"></asp:Label>
                </fieldset>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
