


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
            password = CryptoJS.MD5(document.getElementById('txtPassword').value);
        }
        if (username != '' && password != '' && username != undefined && password != undefined) {

            document.getElementById('btnLogin').src = "../Contents/img/bx_loader.gif";

            $.ajax({
                type: 'POST',
                url: '../AjaxLogin.aspx?op=login&username=' + username + '&password=' + password,
                success: function (msg) {
                    debugger;
                    if (msg == "user") {
                        window.location = "../Home.aspx";
                        //window.location = "../Referrals.aspx"; 
                    }
                    else {
                        document.getElementById('signinpasswordMessages').innerHTML = msg;
                        document.getElementById('btnLogin').src = "../Contents/img/login_btn.png";
                    }
                }

            });
        } else if (username == '' || username == undefined) {
            document.getElementById('signinemailMessages').innerHTML = "Please Enter Email Address";
        } else if (password == '' || password == undefined) {
            document.getElementById('signinpasswordMessages').innerHTML = 'Please Enter Password';
        }

    } catch (e) {

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


function registerFunction() {
    debugger;
    try {
        var email = document.getElementById('EmailTxt').value;
        var password = document.getElementById('PasswordTxt').value;
        var firstname = document.getElementById('txtFirstName').value;
        var lastname = document.getElementById('txtLastName').value;
        var confirmpassword = document.getElementById('txtConfirmPassword').value;
        if (firstname != '') {
            document.getElementById('RequiredFieldValidator5').style.visibility = 'hidden';

            if (lastname != '') {
                document.getElementById('RequiredFieldValidator6').style.visibility = 'hidden';

                if (email != '') {
                    document.getElementById('RequiredFieldValidator3').style.visibility = 'hidden';
                    document.getElementById('RegularExpressionValidator1').style.visibility = "hidden";
                    if (password != '') {

                        document.getElementById('RequiredFieldValidator4').style.visibility = 'hidden';
                        if (confirmpassword != '') {

                            document.getElementById('RequiredFieldValidator7').style.visibility = 'hidden';

                            if (password == confirmpassword) {
                                var cryptpassword = CryptoJS.MD5(password);
                                document.getElementById('btnRegister').src = "../Contents/img/bx_loader.gif";
                                document.getElementById('lblerror').innerHTML = "";
                                var totaldata = "{ 'email':'" + email + "', 'password': '" + cryptpassword + "' , 'firstname':'" + firstname + "', 'lastname':'" + lastname + "' }";
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
                                            window.location = "../Plans.aspx";
                                        } else {
                                            document.getElementById('lblerror').innerHTML = msg;
                                            document.getElementById('btnRegister').src = "../Contents/img/create_account.png";
                                        }
                                    }

                                });
                            } else {
                                document.getElementById('CompareValidator1').style.visibility = 'visible';
                            }
                        } else {
                            document.getElementById('RequiredFieldValidator7').style.visibility = 'visible';
                        }
                    } else {
                        document.getElementById('RequiredFieldValidator4').style.visibility = 'visible';
                    }
                } else {
                    document.getElementById('RequiredFieldValidator3').style.visibility = 'visible';
                }

            } else {
                document.getElementById('RequiredFieldValidator6').style.visibility = 'visible';
            }
        } else {
            document.getElementById('RequiredFieldValidator5').style.visibility = 'visible';

        }
    } catch (e) {

    }
}


function facebookLogin() {
    try {
        debugger;

      $("#fb_account img").attr('src', '../../Contents/img/bx_loader.gif');

        $.ajax({
            type: "POST",
            url: "../AjaxLogin.aspx?op=facebooklogin",
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


function googleplusLogin() {
    try {
        debugger;
        $("#gp_account img").attr('src', '../Contents/img/bx_loader.gif');

        $.ajax({
            type: "POST",
            url: "../AjaxLogin.aspx?op=googlepluslogin",
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
                                var totaldata = "{ 'email':'" + email + "', 'password': '" + cryptpassword + "' , 'firstname':'" + firstname + "', 'lastname':'" + lastname + "','plantype':'"+plantype+"' }";
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