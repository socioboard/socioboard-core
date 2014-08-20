<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/socialsuite.Master" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="SocialSuitePro.contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
  .left_contact{width:700px; width:530px\9; height:auto; float:left;}
  p:last-child{margin-top:18px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
       
        $('#submit').click(function () {
          
            var name = $('#name').val();
            var lname = $('#lname').val();
            var email = $('#email').val();
            var Subject = $('#Subject').val();
            var profile = $('#profile').val();
          
            if (name == "" || email == "" || profile == "") {
                alert('Please fill all the fields')
                return false;
            }
            {


                if (!validateFName(name)) {
                    alert('Please enter valid First Name');
                    return false;
                }
                else if (!validateLName(lname)) {
                    alert('Please enter valid Last Name');
                    return false;
                }
              
                if (!validateEmail(email)) {
                    alert('not valid email');
                    return false;
                }

                else {
                   
                    $.ajax({
                        url: "../Helper/Ajaxcontact.aspx",
                        type: "post",
                        data: 'name=' + name + '&lname=' + lname + '&email=' + email + '&Subject=' + Subject + '&profile=' + profile,
                        success: function (data) {
                            if (data == "success") {
                                alert('Mail has been sent successfully!');
                                $('#name').val('');
                                $('#lname').val('');
                                $('#email').val('');
                                $('#Subject').val('');
                                $('#profile').val('');
                            }
                            else {
                                alert("failure");
                                return false;
                            }
                        },
                        error: function () {
                            alert("failure");
                         
                        }
                    });

                }

            }




        });
    });
    function validateEmail($email) {
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        if (!emailReg.test($email)) {
            return false;
        } else {
            return true;
        }
    }


    function validateFName($name) {
        var fnameReg = /^[A-Z,a-z._]+$/;
        if (!fnameReg.test($name)) {
            return false;
        } else {
            return true;
        }
    }
    function validateLName($lname) {

        var lnameReg = /^[A-Z,a-z._]+$/;
        if (!lnameReg.test($lname)) {
            return false;
        } else {
            return true;
        }


    }








</script>

<hr />
 
 <div>
  <%--<iframe width="100%" height="350" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="https://maps.google.co.in/maps?f=q&amp;source=s_q&amp;hl=en&amp;geocode=&amp;q=Bhilai,+Chhattisgarh&amp;aq=0&amp;oq=bhilai&amp;sll=21.125498,81.914063&amp;sspn=21.895104,43.022461&amp;ie=UTF8&amp;hq=&amp;hnear=Bhilai,+Durg,+Chhattisgarh&amp;t=m&amp;z=11&amp;ll=21.208877,81.378063&amp;output=embed"></iframe><br /><small><a href="https://maps.google.co.in/maps?f=q&amp;source=embed&amp;hl=en&amp;geocode=&amp;q=Bhilai,+Chhattisgarh&amp;aq=0&amp;oq=bhilai&amp;sll=21.125498,81.914063&amp;sspn=21.895104,43.022461&amp;ie=UTF8&amp;hq=&amp;hnear=Bhilai,+Durg,+Chhattisgarh&amp;t=m&amp;z=11&amp;ll=21.208877,81.378063" style="color:#0000FF;text-align:left">View Larger Map</a></small>--%>
  <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d1944.2822179286532!2d77.6154475!3d12.9356925!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3bae144e41105dbd%3A0x6188a5bb947bfdf4!2sL+V+.+Complex%2C+1st+Main+Rd%2C+Koramangala+7th+Block%2C+Koramangala+Layout%2C+Bengaluru%2C+Karnataka+560030!5e0!3m2!1sen!2sin!4v1407845791034" width="100%" height="350" frameborder="0" scrolling="no" marginheight="0" marginwidth="0"></iframe>
 </div>
 
	<div class="contact_form">
        <h2 style="text-align:left;">Contact</h2>
        <div id="signup-inner" class="left_contact">
                <form id="send" action="">
            	     <p>
                        <label for="name">First Name *</label>
                        <input id="name" type="text" name="name" value=""  />
                    </p>
                    <p>
                        <label for="company">Last Name</label>
                        <input id="lname" type="text" name="company" value="" />
                    </p>
                    <p>
                        <label for="email">Email *</label>
                        <input id="email" type="text" name="email" value="" />
                    </p>
                    <p>
                        <label for="website">Your Subject</label>
                        <input id="Subject" type="text" name="Subject" value="" />
                    </p>
                    <p>
                        <label for="profile">Message *</label>
                        <textarea name="profile" id="profile" cols="30" rows="10" ></textarea>
                     </p>
                     <p>
                        <button id="submit" type="button">Submit</button>
                    </p>
                </form>
            </div>
                <div style="width:300px; float:right; height:auto;">
                  <h2 style=" text-align:left;">Let's Talk</h2>
                  <div style="width:300px; height:100px; border-top:1px solid #999; color:#787878; font-size:12px; font-family:nexa-light; padding-top:9px; font-size:16px;">
                    <img src="Contents/img/address.png" alt="" />
                  </div> 
           </div>      
    </div>
</asp:Content>
