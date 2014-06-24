<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="letTalkNew.Registration" %>

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
    </head>
    <body class="login_width">
    <form method="post" id="ctl01" runat="server">
      <div class="aspNetHidden">
     <%--   <input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="/wEPDwUKMTY1NDU2MTA1MmRkm9sNplIkyfmUgZccZXeD4hUQ0nuTVu3ch1RGpTFnUbM=" />--%>
      </div>
      <div class="page">
      <header>
        <div class="header">
          <div class="letstalk_logo"> <a href="index.html"><img src="Contents/img/letstalk_logo.png" alt="" /></a> </div>
          <nav>
            <div class="menu">&nbsp;</div>
          </nav>
          <div class="logout"><a><%--<img src="Contents/img/logout.png" alt="" />--%></a></div>
        </div>
      </header>
 </div>
    
    
    <div class="login_width">
    	<div class="login_sub_width">
        	<div class="login_common_width">
            	<fieldset>
                	<h1>Sign Up <%--<em>Join us now for free</em>--%></h1>
                	<span>
                    	<label>First Name<img src="Contents/img/login_arrow.png"></label>
                        <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name"></asp:TextBox>
                          <div class="error"><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter First Name!"
                                ControlToValidate="txtFirstName" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                                
                          <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="ws_err_star" runat="server" 
    ControlToValidate="txtFirstName" ErrorMessage="Please Enter Valid Name" 
    ValidationExpression="[a-zA-Z]+"></asp:RegularExpressionValidator>      
                                
                                
                                
                                
                                </div>
                    </span>
                    <span>
                    	<label>Last Name<img src="Contents/img/login_arrow.png"></label>
                        <asp:TextBox ID="txtLastName" runat="server" placeholder="Last Name"></asp:TextBox>
                         <div class="error"><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Enter Last Name!"
                                ControlToValidate="txtLastName" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
    ControlToValidate="txtLastName" ErrorMessage="Please Enter Valid Name"  CssClass="ws_err_star"
    ValidationExpression="[a-zA-Z]+"></asp:RegularExpressionValidator>      
                                
                                
                                </div>
                       <%-- <input type="text" placeholder="Last Name">--%>



                    </span>
                    <span>
                    	<label>Email <img src="Contents/img/login_arrow.png"></label>
                        <asp:TextBox ID="txtEmail" runat="server" placeholder="Email"></asp:TextBox>
                         <div class="error"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                    runat="server" ErrorMessage="Please Enter Email!"
                                ControlToValidate="txtEmail" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Please Enter Valid Email!"
                                ControlToValidate="txtEmail" CssClass="ws_err_star_enter" 
                    ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$"></asp:RegularExpressionValidator></div>
                       <%-- <input type="text" placeholder="Email">--%>
                    </span>
                    <span>
                    	<label>Password <img src="Contents/img/login_arrow.png"></label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
                         <div class="error"> <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                    runat="server" ErrorMessage="Please Enter Password!"
                                ControlToValidate="txtPassword" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                    runat="server" ErrorMessage="Please Enter Valid Password!"
                                ControlToValidate="txtPassword" ValidationExpression="^\S+$"
                                CssClass="ws_err_star_enter" ></asp:RegularExpressionValidator></div>
                       <%-- <input type="password" placeholder="Password">--%>
                    </span>
                    <span>
                    	<label>Password Confirmation<img src="Contents/img/login_arrow.png"></label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm Password"></asp:TextBox>
                         <div class="error">    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                    runat="server" ErrorMessage="Please Enter Confirm Password!"
                                ControlToValidate="txtConfirmPassword" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Password & Confirm Password should be same"
                                ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"
                                CssClass="ws_err_star_enter"></asp:CompareValidator></div>
                     <%--   <input type="password" placeholder="Password Confirmation">--%>
                    </span>
                    <%--<span>
                    	<input type="checkbox"> <em>Remember Me</em>
                        <a href="#">Forgot Your Login</a>
                    </span>--%>
                    <span>
                     <asp:ImageButton ID="btnRegister" runat="server" ImageUrl="../Contents/img/sign_up.png" onclick="btnRegister_Click" Text="Submit"/>
                    	<%--<input type="submit" value="Sign up for free" >--%>
                        <div class="error">
                        <asp:Label ID="lblerror" runat="server"></asp:Label>
                        </div>
                    </span>
                </fieldset>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
