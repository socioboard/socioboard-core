<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SocialSuitePro.Admin.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!--[if lt IE 7]> <html class="ie lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
<!--[if IE 7]>    <html class="ie lt-ie9 lt-ie8"> <![endif]-->
<!--[if IE 8]>    <html class="ie lt-ie9"> <![endif]-->
<!--[if gt IE 8]> <html class="ie gt-ie8"> <![endif]-->
<!--[if !IE]><!--><html><!-- <![endif]-->
<head>
	<title>Socioboard - Admin</title>
	<link rel="shortcut icon" type="image/x-icon" href="../Contents/img/ssp/logo-bg.png"/>
	<!-- Meta -->
	<meta charset="UTF-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimum-scale=1.0, maximum-scale=1.0">
	<meta name="apple-mobile-web-app-capable" content="yes">
	<meta name="apple-mobile-web-app-status-bar-style" content="black">
	<meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=7; IE=EDGE" />
	
	<!-- Bootstrap -->
	<link href="../Contents/common/bootstrap/css/bootstrap.css" rel="stylesheet" />
	<link href="../Contents/common/bootstrap/css/responsive.css" rel="stylesheet" />
	
	<!-- Glyphicons Font Icons -->
	<link href="../Contents/common/theme/css/glyphicons.css" rel="stylesheet" />
	
	<!-- Uniform Pretty Checkboxes -->
	<link href="../Contents/common/theme/scripts/plugins/forms/pixelmatrix-uniform/css/uniform.default.css" rel="stylesheet" />
	
	<!-- Main Theme Stylesheet :: CSS -->
	<link href="../Contents/common/theme/css/style-light.css?1369753445" rel="stylesheet" />
	
	
	<!-- LESS.js Library -->
	<script src="../Contents/common/theme/scripts/plugins/system/less.min.js"></script>
    <script type="text/javascript">
        window.history.forward(-1);
    </script>
    <script type = "text/javascript" >
        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
</script>
</head>
<body class="login" onunload="disableBackButton()">
	
	<!-- Wrapper -->
<div id="login">

	<div class="container">
	
		<div class="wrapper">
		
			<h1 class="glyphicons lock">Socioboard Admin <i></i></h1>
		
			<!-- Box -->
			<div class="widget">
				
				<div class="widget-head">
					<h3 class="heading">Login area</h3>
					<%--<div class="pull-right">
						Don't have an account? 
						<a href="signup.html?lang=en&amp;layout_type=fluid&amp;menu_position=menu-right&amp;style=style-light" class="btn btn-inverse btn-mini">Sign up</a>
					</div>--%>
				</div>
				<div class="widget-body">
				
					<!-- Form -->
					<form id="Form1" method="post" runat="server">
						<label>Username</label>
                    <asp:TextBox ID="txtUserName" runat="server" class="input-block-level" placeholder="Your Username" autocomplete="off"></asp:TextBox>
					
						<label>Password <%--<a class="password" href="">forgot your password?</a>--%></label>
						
                         <asp:TextBox ID="txtPassword" runat="server" class="input-block-level" 
                            placeholder="Your Password" TextMode="Password" autocomplete="off"></asp:TextBox>
					
						<div class="separator bottom"></div> 
						<div class="row-fluid">
							<div class="span8">
								<%--<div class="uniformjs"><label class="checkbox"><input type="checkbox" value="remember-me">Remember me</label></div>--%>
							</div>
							<div class="span4 center">
                                <asp:Button ID="btnLogin" class="btn btn-block btn-primary" runat="server" 
                                    Text="Sign In" onclick="btnLogin_Click" />
								<%--<button class="btn btn-block btn-primary" type="submit">Sign in</button>--%>
							</div>
						</div>
					</form>
					<!-- // Form END -->
							
				</div>
				<div class="widget-footer">
					<p class="glyphicons restart"><i></i>Please enter your username and password ...</p>
				</div>
			</div>
			<!-- // Box END -->
			
		<%--	<div class="innerAll center">
				<p>Alternatively</p>
				<a href="index.html?lang=en&amp;layout_type=fluid&amp;menu_position=menu-right&amp;style=style-light" class="btn btn-icon-stacked btn-block btn-facebook glyphicons facebook"><i></i><span>Join using your</span><span class="strong">Facebook Account</span></a>
				<p>or</p>
				<a href="index.html?lang=en&amp;layout_type=fluid&amp;menu_position=menu-right&amp;style=style-light" class="btn btn-icon-stacked btn-block btn-google glyphicons google_plus"><i></i><span>Join using your</span><span class="strong">Google Account</span></a>
				<p>Having troubles? <a href="faq.html?lang=en&amp;layout_type=fluid&amp;menu_position=menu-right&amp;style=style-light">Get Help</a></p>
			</div>--%>
			
		</div>
		
	</div>
	
</div>
<!-- // Wrapper END -->	

<!-- Themer -->
<div id="themer" class="collapse">
	<div class="wrapper">
		<span class="close2">&times; close</span>
		<h4>Themer <span>color options</span></h4>
		<ul>
			<li>Theme: <select id="themer-theme" class="pull-right"></select><div class="clearfix"></div></li>
			<li>Primary Color: <input type="text" data-type="minicolors" data-default="#ffffff" data-slider="hue" data-textfield="false" data-position="left" id="themer-primary-cp" /><div class="clearfix"></div></li>
			<li>
				<span class="link" id="themer-custom-reset">reset theme</span>
				<span class="pull-right"><label>advanced <input type="checkbox" value="1" id="themer-advanced-toggle" /></label></span>
			</li>
		</ul>
		<div id="themer-getcode" class="hide">
			<hr class="separator" />
			<button class="btn btn-primary btn-small pull-right btn-icon glyphicons download" id="themer-getcode-less"><i></i>Get LESS</button>
			<button class="btn btn-inverse btn-small pull-right btn-icon glyphicons download" id="themer-getcode-css"><i></i>Get CSS</button>
			<div class="clearfix"></div>
		</div>
	</div>
</div>
<!-- // Themer END -->

	<!-- JQuery -->
	<script src="../Contents/common/theme/scripts/plugins/system/jquery.min.js"></script>
	
	
	<!-- Modernizr -->
	<script src="../Contents/common/theme/scripts/plugins/system/modernizr.js"></script>
	
	<!-- Bootstrap -->
	<script src="../Contents/common/bootstrap/js/bootstrap.min.js"></script>
	
	<!-- SlimScroll Plugin -->
	<script src="../Contents/common/theme/scripts/plugins/other/jquery-slimScroll/jquery.slimscroll.min.js"></script>
	
	<!-- Common Demo Script -->
	<script src="../Contents/common/theme/scripts/demo/common.js?1369753445"></script>
	
	<!-- Holder Plugin -->
	<script src="../Contents/common/theme/scripts/plugins/other/holder/holder.js"></script>
	
	<!-- Uniform Forms Plugin -->
	<script src="../Contents/common/theme/scripts/plugins/forms/pixelmatrix-uniform/jquery.uniform.min.js"></script>

	
	
</body>
</html>
