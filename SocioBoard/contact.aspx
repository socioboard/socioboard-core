<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/socialsuite.Master" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="SocialSuitePro.contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        //alert("asdads");
        $('#submit').click(function () {
            //alert('asdasd');
            var name = $('#name').val();
            var lname = $('#lname').val();
            var email = $('#email').val();
            var Subject = $('#Subject').val();
            var profile = $('#profile').val();
            if (name == "" || lname == "" || email == "" || Subject == "" || profile == "") {
                alert('Please fill all the fields')
                return false;
            }
            else {
                if (!validateEmail(email)) {
                    alert('not valid email');
                    return false;
                }

                else {
                    //alert('come on ajax');
                    //ajax start

                    $.ajax({
                        url: "../Helper/Ajaxcontact.aspx",
                        type: "post",
                        data: 'name=' + name + '&lname=' + lname + '&email=' + email + '&Subject=' + Subject + '&profile=' + profile,
                        success: function (data) {
                            if (data == "success") {
                                alert('Mail Send');
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
                            //$("#result").html('There is error while submit');
                        }
                    });


                    //ajax end
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
</script>

<hr />
 
 <div>
   <iframe width="100%" height="350" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="https://maps.google.co.in/maps?f=q&amp;source=s_q&amp;hl=en&amp;geocode=&amp;q=Globussoft,+Smriti+Nagar,+Bhilai+Nagar,+Chhattisgarh&amp;aq=0&amp;oq=globussoft&amp;sll=21.205521,81.487619&amp;sspn=0.360404,0.676346&amp;ie=UTF8&amp;hq=Globussoft,&amp;hnear=Smriti+Nagar,+Bhilai,+Durg,+Chhattisgarh&amp;t=m&amp;ll=21.219529,81.318624&amp;spn=0.006295,0.019671&amp;output=embed"></iframe><br /><small><a href="https://maps.google.co.in/maps?f=q&amp;source=embed&amp;hl=en&amp;geocode=&amp;q=Globussoft,+Smriti+Nagar,+Bhilai+Nagar,+Chhattisgarh&amp;aq=0&amp;oq=globussoft&amp;sll=21.205521,81.487619&amp;sspn=0.360404,0.676346&amp;ie=UTF8&amp;hq=Globussoft,&amp;hnear=Smriti+Nagar,+Bhilai,+Durg,+Chhattisgarh&amp;t=m&amp;ll=21.219529,81.318624&amp;spn=0.006295,0.019671" style="color:#0000FF;text-align:left">View Larger Map</a></small>
 
 </div>
 
	<div class="contact_form">
    <h2 style="text-align:left;">Contact</h2>
    <div id="signup-inner" style="width:700px; height:auto; float:left;">
          
            <form id="send" action="">
            	
                <p>
                <label for="name">First Name *</label>
                <input id="name" type="text" name="name" value="" />
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
                <textarea name="profile" id="profile" cols="30" rows="10"></textarea>

                </p>
                
                <p>

                <button id="submit" type="button">Submit</button>
                </p>
                
            </form>
            
          </div>
    <div style="width:300px; float:right; height:auto;">
      <h2 style=" text-align:left;">Let's Talk</h2>
      <div style="width:300px; height:100px; border-top:1px solid #999; color:#787878; font-size:12px; font-family:nexa-light; padding-top:9px; font-size:16px;">
         <!--Office: CHPL DreamHomes Apartments, Apollo Hospital Road Junwani, Bhilai-490020, Chhattisgarh, India. <br /><br />
          Call us: 0788-4083007, 4083008, 4083009<br /><br />
         Write to us: <img src="contents/img/support.png" alt="" />-->
         <img src="Contents/img/address.png" alt="" />
      </div> 
        
    </div>      
    </div>


</asp:Content>
