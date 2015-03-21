
document.write('<script type="text/javascript" language="javascript" src="/Themes/Socioboard/Views/Home/Index.cshtml"></script>')

function checkEmail(email) {
    try {

        debugger;
        if (email != '') {
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!filter.test(email)) {
                document.getElementById('txtEmail').focus;
                document.getElementById('signinemailMessages').innerHTML = "Invalid Email Address";
                return false;
            } else {
                document.getElementById('signinemailMessages').innerHTML = "";
                return true;
            }
        } else {
            document.getElementById('signinemailMessages').innerHTML = "Please Enter Email Address";
        }

    } catch (e) {

    }
}

function checkEmailDefault(email) {
    try {
        debugger;
        if (email != '') {
            document.getElementById('signinEmailError').style.display = 'none';
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!filter.test(email)) {
                document.getElementById('signinEmail').focus;

                document.getElementById('signinEmailError').innerHTML = "Invalid Email Address";
                document.getElementById('signinEmailError').style.display = 'block';
                return false;
            } else {
                document.getElementById('signinEmailError').innerHTML = "";
                document.getElementById('signinEmailError').style.display = 'none';
                return true;
            }
        } else {
            document.getElementById('signinEmailError').innerHTML = "Please Enter Email Address";
            document.getElementById('signinEmailError').style.display = 'block';
        }
    } catch (e) {

    }
}

function checkPasswordConfirmtion(confirmPassword) {
    try {
        debugger;
        var password = document.getElementById('signinpassword').value;

        if (password != '') {
            document.getElementById('signinpasswordError').style.display = 'none';
            document.getElementById('signinpasswordError').innerHTML = '';
            if (password != confirmPassword) {

                document.getElementById('signinpasswordConfirmationError').innerHTML = "Password and Confirm Password Must be Same";
                document.getElementById('signinpasswordConfirmationError').style.display = 'block';
            } else {
                document.getElementById('signinpasswordConfirmationError').style.display = 'none';
                document.getElementById('signinpasswordConfirmationError').innerHTML = '';
            }
        } else {
            document.getElementById('signinpasswordError').innerHTML = "Please Enter Password";
            document.getElementById('signinpasswordError').style.display = 'block';
        }
    } catch (e) {

    }
}


function signinFunction() {
    debugger;
    try {
        var password = document.getElementById('txtPassword').value;


        var username = document.getElementById('txtEmail').value;
        if (password != '' && password != undefined) {
            //password = CryptoJS.MD5(document.getElementById('txtPassword').value);
        }
        if (username != '' && password != '' && username != undefined && password != undefined) {

            //document.getElementById('btnlogin').src = "../../Contents/img/bx_loader.gif";
            $('#btnlogin').html("<img class='img-portfolio img-responsive' src='/Themes/Socioboard/Contents/img/bx_loader.gif'>");

            $.ajax({
                type: 'POST',
                //url: '../../Default/AjaxLogin.aspx?op=login&username=' + encodeURIComponent(username) + '&password=' + encodeURIComponent(password),
                url: '../Index/AjaxLogin?op=login&username=' + encodeURIComponent(username) + '&password=' + encodeURIComponent(password),
                success: function (msg) {
                    debugger;
                    if (msg != "Invalid Email or Password") {
                        if ($("#RememberMe").is(':checked')) {
                            checkCookie(username, password);
                        }
                    }
                    if (msg == "user") {
                        window.top.location = "../Home/Index";
                    }
                       // Edited by Antima[1/11/2014]
                    else if (msg == "notactivated") {
                        $.ajax({
                            type: 'POST',
                            url: '../Index/UserActivation',
                            success: function (msg) {
                                if (msg == 'Success') {
                                    window.location = '../Index/UserActivationByEmail?email=' + username;
                                }
                            }
                        });
                    }
                    else if (msg == "unpaid") {
                        window.location = '../PersonalSetting/Index';
                    }

                    //Modified by Hozefa
                    else if (msg == "SuperAdmin") {
                        window.top.location = "../AdminHome/Dashboard";
                    }
                    else if (msg=="User Not Exist!") {
                        document.getElementById('signinpasswordMessages').innerHTML = msg;
                        $('#btnlogin').html("<button class='btn btn-warning' type='button'>Login</button>");
                    }
                    else {
                        document.getElementById('signinpasswordMessages').innerHTML = msg;
                        $('#btnlogin').html("<button class='btn btn-warning' type='button'>Login</button>");
                    }
                }

            });
            //} else if (username == '' || username == undefined) {
            //    document.getElementById('signinemailMessages').innerHTML = "Please Enter Email Address";
            //} else if (password == '' || password == undefined) {
            //    document.getElementById('signinpasswordMessages').innerHTML = 'Please Enter Password';
        } else { alert("All fields are mandatory") }

    } catch (e) {

    }

}

// Edited by Antima

function setCookie(cemail, cpwd, exdays) {
    debugger;
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = "logininfo" + cemail + "=" + cpwd + ";" + expires;
}
//Added By vikash
function GetAllCookies()
{
    var clist = [];
    var j = 0;
    debugger;
    var Cookies = document.cookie.split(';');
    for (var i = 0; i < Cookies.length; i++) {
        var currentcooki = Cookies[i];
        if (currentcooki.indexOf("logininfo") > -1)
        {
            clist[j] = currentcooki;
            j++;
        }
    }
    return clist[0].replace("logininfo","");
}
function getCookie(cemail) {
    debugger;
    var name = cemail + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) != -1) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function checkCookie(username, password) {
    debugger;
    var check = getCookie("logininfo" + username);
    if (check != "") {
        //  window.alert("Welcome again " + username);
    } else {
        if (username != "" && username != null && password != "" && password != null) {
            setCookie(username, password, 90);
        }
    }
}


function checkEmailForSignUp(email) {
    try {

        debugger;
        if (email != '') {
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!filter.test(email)) {
                document.getElementById('RequiredFieldValidator3').style.visibility = "hidden";
                document.getElementById('RegularExpressionValidator1').style.visibility = "visible";
                return false;
            } else {
                document.getElementById('RequiredFieldValidator3').style.visibility = "hidden";
                document.getElementById('RegularExpressionValidator1').style.visibility = "hidden";
                return true;
            }
        } else {
            document.getElementById('RegularExpressionValidator1').style.visibility = "hidden";
            document.getElementById('RequiredFieldValidator3').style.visibility = "visible";
        }

    } catch (e) {

    }

}


//commented  by vikash

//function register() {
//    debugger;
//    try {
//        var email = $('#txtrEmail').val();
//        var password = $('#txtrPassword').val();
//        var firstname = $('#txtrFirstName').val();
//        var lastname = $('#txtrLastName').val();
//        var confirmpassword = $('#txtrConfirmPassword').val();
//        var packag = $("#package :selected").val();
//        if (firstname != '') {

//            if (lastname != '') {

//                if (email != '') {

//                    if (password != '') {



//                        if (confirmpassword != '') {


//                            if (password == confirmpassword) {

//                                var totaldata = "{ 'email':'" + email + "', 'password': '" + password + "' , 'firstname':'" + firstname + "', 'lastname':'" + lastname + "', 'package':'" + packag + "' }";
//                                debugger;
//                                $.ajax({
//                                    type: "POST",
//                                    url: "../Index/Signup",
//                                    data: totaldata,
//                                    contentType: "application/json; charset=utf-8",

//                                    success: function (msg) {
//                                        alert("msg");
//                                        alert(msg);
//                                        window.location = "../Home/Index";
//                                    }

//                                });
//                            } else {

//                            }
//                        } else {

//                        }
//                    } else {

//                    }
//                } else {

//                }

//            } else {

//            }
//        } else {

//        }
//    } catch (e) {

//    }
//}


//----------------vikash---------//
var strength = "";
function register() {
    debugger;
    try {
        var email = $('#txtrEmail').val();
        var password = $('#txtrPassword').val();
        var firstname = $('#txtrFirstName').val();
        var lastname = $('#txtrLastName').val();
        var confirmpassword = $('#txtrConfirmPassword').val();
        var packag = $("#package :selected").val();
        var hint = getParameterByName("teamid");
        var Lower_FirstName = firstname.toLowerCase();
        var Lower_LastName = lastname.toLowerCase();
        if (packag != '') {

            if (firstname != '') {

                if (validateFName(firstname)) {

                    if (lastname != '') {

                        if (validateLName(lastname)) {

                            if (email != '') {

                                if (validateEmail(email)) {

                                    if (Lower_FirstName != Lower_LastName) {

                                        if (password != '') {

                                            if (confirmpassword != '') {

                                                if (password == confirmpassword) {

                                                    if (strength != "weak") {

                                                        var totaldata = "{ 'email':'" + email + "', 'password': '" + password + "' , 'firstname':'" + firstname + "', 'lastname':'" + lastname + "', 'package':'" + packag + "' }";
                                                        alertify.prompt("Are you sure, You want to register with ", function (e, str) {
                                                            debugger;
                                                            if (e) {
                                                                email = str;
                                                                $('#txtrEmail').val(str);
                                                                if (validateEmail(email)) {
                                                                    $.ajax({
                                                                        type: "POST",
                                                                        url: "../Index/Signup",
                                                                        data: totaldata,
                                                                        contentType: "application/json; charset=utf-8",
                                                                        success: function (msg_Signup) {
                                                                            debugger;
                                                                            if (msg_Signup == "Email Already Exists") {
                                                                                alertify.set({ delay: 5000 });
                                                                                alertify.error("Email Already Exists")
                                                                                return;
                                                                            }

                                                                           

                                                                            $.ajax({
                                                                                type: "GET",
                                                                                url: "../Index/SendRegistrationMail?emailId=" + email,
                                                                                data: '',
                                                                                success: function (msg) {
                                                                                    if (msg.indexOf("Success") != -1) {
                                                                                        alertify.success('Mail has been send Successfully!!');
                                                                                        if (msg.indexOf("Facebook Registration") != -1 && msg_Signup != null) {
                                                                                            //Dont alert to activate account
                                                                                        }
                                                                                        else {
                                                                                            alert('Please check your mail to activate your Account.');
                                                                                        }
                                                                                        $('#txtrEmail').val('');
                                                                                        $('#txtrPassword').val('');
                                                                                        $('#txtrFirstName').val('');
                                                                                        $('#txtrLastName').val('');
                                                                                        $('#txtrConfirmPassword').val('');
                                                                                    }
                                                                                    else {
                                                                                        alertify.error("failure");

                                                                                    }
                                                                                },
                                                                                error: function () {
                                                                                    alert("failure");
                                                                                }
                                                                            });

                                                                            //if (hint != "" && hint != null) {
                                                                            //    window.location = "../Home/Index?teamid='" + hint + "'";
                                                                            //}
                                                                            //else {
                                                                            //    window.location = "../Home/Index";
                                                                            //}


                                                                        }

                                                                    });
                                                                } else {
                                                                    alertify.error("Invalid Emal");
                                                                    return;
                                                                }
                                                            } else { }
                                                        }, email);
                                                    } else {
                                                        alertify.error("Password should contain atleast 6 characters and should be alphanumeric");
                                                        return;
                                                    }
                                                } else {
                                                    alertify.error("Password and Confirm Password is not Matched");
                                                    return;
                                                }
                                            } else {
                                                alertify.error("Enter Confirm Password");
                                                return;
                                            }
                                        } else {
                                            alertify.error("Enter Password");
                                            return;
                                        }
                                    } else {
                                        alertify.error("First Name and Last Name Can not be same!");
                                        return;
                                    }

                                } else {
                                    alertify.error("Invalid Emal");
                                    return;
                                }
                            } else {
                                alertify.error("Enter Email");
                                return;
                            }
                        } else {
                            alertify.error("Invalid Last Name");
                            return;
                        }

                    } else {
                        alertify.error("Enter Last Name");
                        return;
                    }

                } else {
                    alertify.error("Invalid First Name");
                    return;
                }
            } else {
                alertify.error("Enter First Name");
                return;
            }
        } else {
            alertify.error("Select Account Type");
            return;
        }
    } catch (e) {

    }
}

function password() {
    debugger;
    var strongRegex = new RegExp("^(?=.{7,})(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*\\W).*$", "g");
    var mediumRegex = new RegExp("^(?=.{5,})(((?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]))|((?=.*[A-Z])(?=.*[0-9]))|((?=.*[a-z])(?=.*[0-9]))).*$", "g");
    if ($('#txtrPassword').val() != "") {
        $('#password_strength').css('display', 'block');
        $('#_password_strength').css('display', 'block');
        if ($('#txtrPassword').val().length <= 15) {
            if (strongRegex.test($('#txtrPassword').val())) {
                $('#stregth').html('<b>Strong!</b>');
                $('#weak').css("background-color", "#006400");
                $('#medium').css("background-color", "#006400");
                $('#strong').css("background-color", "#006400");
                strength = "";
            } else if (mediumRegex.test($('#txtrPassword').val())) {
                $('#stregth').html('<b>Medium!</b>');
                $('#weak').css("background-color", "#FFFF00");
                $('#medium').css("background-color", "#FFFF00");
                $('#strong').css("background-color", "rgb(204, 204, 204)");
                strength = "";
            } else {
                $('#stregth').html('<b>Weak!</b>');
                strength = "weak";
                $('#weak').css("background-color", "#FF0000");
                $('#medium').css("background-color", "rgb(204, 204, 204)");
                $('#strong').css("background-color", "rgb(204, 204, 204)");
            }
        } else {
            var pass = $('#txtrPassword').val();
            $('#txtrPassword').val(pass.substring(0, 15));
        }
    }
    else {
        $('#stregth').html('<b>Weak!</b>');
        $('#weak').css("background-color", "#FF0000");
        $('#medium').css("background-color", "rgb(204, 204, 204)");
        $('#strong').css("background-color", "rgb(204, 204, 204)");
        $('#password_strength').css('display', 'none');
        $('#_password_strength').css('display', 'none');
    }
    return true;
}

function validateEmail($email) {
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    if (!emailReg.test($email)) {
        return false;
    } else {
        return true;
    }
}

function validateFName($Fname) {
    //var fnameReg = /^[A-Z,a-z._]+$/;
    //regular expression for accept numbers and characters
    var fnameReg = /^[a-zA-Z0-9_]*$/;
    if (!fnameReg.test($Fname)) {
        return false;
    } else {
        return true;
    }
}

function validateLName($lname) {
    //var lnameReg = /^[A-Z,a-z._]+$/;
    var lnameReg = /^[a-zA-Z0-9_]*$/;
    if (!lnameReg.test($lname)) {
        return false;
    } else {
        return true;
    }
}

function validatePhone($Phoneno) {
    var phoneReg = /^\d{10}$/;
    if (!phoneReg.test($Phoneno)) {
        return false;
    } else {
        return true;
    }
}


function facebookLogin() {
    try {
        debugger;
        //$('#gp_account').css('display', 'none');
        $('#gp_account').attr('onclick', '');
        //$("#fb_account img").attr('src', '../../Contents/img/bx_loader.gif');

        $.ajax({
            type: "POST",
            url: "../FacebookManager/AuthenticateFacebook?op=fblogin",
            data: '',
            //contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                window.location = msg;
            }
        });
    } catch (e) {

    }
}


function googleplusLogin() {
    try {
        debugger;
        //$('#fb_account').css('display', 'none');
        $('#fb_account').attr('onclick', '');
        //$("#gp_account img").attr('src', '../Contents/img/bx_loader.gif');

        $.ajax({
            type: "POST",
            url: "../YoutubeManager/AuthenticateYoutube?op=googlepluslogin",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                debugger;
                window.location = msg;
            }
        });
    } catch (e) {

    }

}




function RegisterDefault(plantype) {
    debugger;
    document.getElementById('completeError').style.display = 'none';
    try {
        var firstname = document.getElementById('signinFirstName').value;
        var lastname = document.getElementById('signinLastName').value;
        var email = document.getElementById('signinEmail').value;
        var password = document.getElementById('signinpassword').value;
        var confirmpassword = document.getElementById('signinpasswordConfirmation').value;

        if (firstname != '') {
            document.getElementById('signinFirstNameError').style.display = 'none';
            if (lastname != '') {
                document.getElementById('signinLastNameError').style.display = 'none';
                if (email != '') {
                    document.getElementById('signinEmailError').style.display = 'none';
                    if (password != '') {
                        document.getElementById('signinpasswordError').style.display = 'none';

                        if (confirmpassword != '') {
                            document.getElementById('signinpasswordConfirmationError').style.display = "none";
                            if (password == confirmpassword) {

                                document.getElementById('signupbtn').innerHTML = "<img src='../Contents/img/bx_loader.gif' alt='' />"

                                document.getElementById('signinpasswordConfirmationError').style.display = "none";
                                var cryptpassword = CryptoJS.MD5(password);
                                var totaldata = "{ 'email':'" + email + "', 'password': '" + cryptpassword + "' , 'firstname':'" + firstname + "', 'lastname':'" + lastname + "','plantype':'" + plantype + "' }";
                                debugger;
                                $.ajax({
                                    type: "POST",
                                    url: "../AjaxLogin.aspx?op=register",
                                    data: totaldata,
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "html",
                                    success: function (msg) {
                                        debugger;
                                        if (msg == "user") {
                                            window.location = "../Home.aspx";
                                        } else {
                                            document.getElementById('completeError').innerHTML = msg;
                                            document.getElementById('completeError').style.display = 'block';
                                            document.getElementById('signupbtn').innerHTML = '<input class="btn" name="commit" value="Sign up for free" type="button">';
                                        }
                                    }

                                });

                            } else {
                                document.getElementById('signinpasswordConfirmationError').innerHTML = "Password And Confirm password Must Be Same";
                                document.getElementById('signinpasswordConfirmationError').style.display = "block";
                            }

                        } else {
                            document.getElementById('signinpasswordConfirmationError').innerHTML = "Please Enter Confirm password";
                            document.getElementById('signinpasswordConfirmationError').style.display = "block";
                        }

                    } else {
                        document.getElementById('signinpasswordErrorError').innerHTML = "Please Enter Password";
                        document.getElementById('signinpasswordErrorError').style.display = 'block';
                    }
                } else {
                    document.getElementById('signinEmailError').innerHTML = "Please Enter Password";
                    document.getElementById('signinEmailError').style.display = 'block';
                }

            } else {
                document.getElementById('signinLastNameError').style.display = 'block';
                document.getElementById('signinLastNameError').innerHTML = "Please Enter LastName";
            }

        } else {
            document.getElementById('signinFirstNameError').innerHTML = 'Please Enter FirstName';
            document.getElementById('signinFirstNameError').style.display = 'block';
        }



    } catch (e) {

    }

}

function getParameterByName(name) {
    debugger;
    var match = RegExp('[?&]' + name + '=([^&]*)').exec(window.location.search);
    return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
}

function ResentActivationMail(email) {
    $.ajax({
        type: "GET",
        url: "../Index/SendRegistrationMail?emailId=" + email,
        data: '',
        success: function (msg) {
            if (msg == "Success") {
                alertify.success('Mail has been send Successfully!!');
                alert('Please check your mail to activate your Account.');
            }
            else {
                alertify.error("failure");

            }
        },
        error: function () {
            alert("failure");
        }
    });

}

$(document).ready(function () {
    $('input').keypress(function (e) {
        var is_shift_pressed = false;
        debugger;
        if (e.shiftKey) {
            is_shift_pressed = e.shiftKey;
        }

        if (((e.which >= 65 && e.which <= 90) && !is_shift_pressed) || ((e.which >= 97 && e.which <= 122) && is_shift_pressed)) {
            $("#signinpasswordMessages").html("Caps Lock Is On");
        }
        else {
            $("#signinpasswordMessages").html("");
        }
    });
});