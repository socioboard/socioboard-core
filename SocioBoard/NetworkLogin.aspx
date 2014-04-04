<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NetworkLogin.aspx.cs" Inherits="SocialSuitePro.NetworkLogin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title></title>
    <script src="Contents/js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //alert("asdad");
            $("#check").click(function () {
                var couponcode = $("#TextBox1").val();
                if (couponcode == "") {
                    alert("Please enter coupon Code")
                    return false;
                }
                $.ajax({
                    url: '../Ajaxcoupon.aspx',
                    type: 'POST',
                    data: 'code=' + couponcode,
                    dataType: 'html',
                    success: function (msg) {
                        //$("#rss_users").html(msg);
                        //$("#check").html(msg);
                        alert(msg);
                    }
                });


            });
        });
</script>
<link href="Contents/css/style.css" rel="stylesheet" type="text/css" />
</head>

<body class="login">
    <form id="form1" runat="server">
        <div class="logo_login"><img src="../Contents/img/under_sspro_logo.png" alt="" /></div>
    
    <div class="login_reg_bg">
       <div class="signin">Account Profile</div>
       <div class="reg_temp_reg">
       
         <div class="left_reg_area">
             <div class="inputbg"><asp:DropDownList class="dropdwn" ID="DropDownList1" runat="server">
                  <asp:ListItem Value="Select">Select Account Type</asp:ListItem>
                      <asp:ListItem Value="Free">Basic(Free)</asp:ListItem>
                      <asp:ListItem>Standard</asp:ListItem>
                      <asp:ListItem>Deluxe</asp:ListItem>
                      <asp:ListItem>Premium</asp:ListItem>
                      </asp:DropDownList></div>
            <div class="inputbg"><asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name"></asp:TextBox></div>
            <div class="error">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter First Name!" ControlToValidate="txtFirstName" CssClass="ws_err_star"></asp:RequiredFieldValidator>
            </div>

            <div class="inputbg"><asp:TextBox ID="txtLastName" runat="server" placeholder="Last Name"></asp:TextBox></div>
            <div class="error">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Enter Last Name!" ControlToValidate="txtLastName" CssClass="ws_err_star"></asp:RequiredFieldValidator>
            </div>

            <div class="inputbg"><asp:TextBox ID="txtEmail" runat="server" placeholder="Email"></asp:TextBox></div>
            <div class="error">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Email!" ControlToValidate="txtEmail" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Please Enter Valid Email!" ControlToValidate="txtEmail" CssClass="ws_err_star_enter" ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$"></asp:RegularExpressionValidator>
            </div>

            <div class="input_pswd_bg"><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox></div>
            <div class="error"> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Password!" ControlToValidate="txtPassword" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please Enter Valid Password!" ControlToValidate="txtPassword" ValidationExpression="^\S+$" CssClass="ws_err_star_enter" ></asp:RegularExpressionValidator>
            </div>

            <div class="input_pswd_bg"> <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm Password"></asp:TextBox></div>
            <div class="error">    
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Confirm Password!" ControlToValidate="txtConfirmPassword" CssClass="ws_err_star"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Password & Confirm Password should be same" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" CssClass="ws_err_star_enter"></asp:CompareValidator>
            </div>
            <div class="input_pswd_bg" style="width:120%"> <asp:TextBox style="width:218px;" ID="TextBox1" runat="server" placeholder="Coupon Code(Optional)"></asp:TextBox><a id="check" style="margin-left: 29px;" href="#">Click to verify</a></div>
            <asp:Label ID="lblerror" runat="server" CssClass="ws_err_star" style="text-decoration:none;"></asp:Label>

             <div class="create_your_account">
                   <asp:ImageButton ID="btnRegister" runat="server" 
                       ImageUrl="../Contents/img/submit_btn.png" onclick="btnRegister_Click" />
             </div>
            
         </div>

       </div>
    </div>
    </form>
</body>
</html>
