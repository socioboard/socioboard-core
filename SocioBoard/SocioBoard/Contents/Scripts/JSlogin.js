// Login Form

$(function () {
    try {
        var button = $('#loginButton');
        var box = $('#loginBox');
        var form = $('#loginForm');
        button.removeAttr('href');
        button.mouseup(function (login) {
            box.toggle();
            button.toggleClass('active');
        });
        form.mouseup(function () {
            return false;
        });
        $(this).mouseup(function (login) {
            if (!($(login.target).parent('#loginButton').length > 0)) {
                button.removeClass('active');
                box.hide();
            }
        });
    } catch (e) {

    }
});


$(function () {
    try {
        var button = $('#FbloginButton');
        var box = $('#FbloginBox');
        var form = $('#FbloginForm');
        button.removeAttr('href');
        button.mouseup(function (login) {
            box.toggle();
            button.toggleClass('active');
        });
        form.mouseup(function () {
            return false;
        });
    } catch (e) {

    }
});


function validationSignup() {
    debugger;
    if (document.getElementById('txtEmail').value == '') {
        document.getElementById('rfvEmail').style.display = "block";
        document.getElementById('rfvFirstName').style.display = "none";
        document.getElementById('rfvLastName').style.display = "none";
        document.getElementById('rfvPassword').style.display = "none";
        document.getElementById('lblError').innerHTML = "";
        document.getElementById('cvConfirmPassword').style.display = "none";
        return false;
    }
    else if (document.getElementById('txtFirstName').value == '') {
        document.getElementById('rfvEmail').style.display = "none";
        document.getElementById('rfvFirstName').style.display = "block";
        document.getElementById('rfvLastName').style.display = "none";
        document.getElementById('rfvPassword').style.display = "none";
        document.getElementById('lblError').innerHTML = "";
        document.getElementById('cvConfirmPassword').style.display = "none";
        return false;
    }
    else if (document.getElementById('txtLastName').value == '') {
        document.getElementById('rfvEmail').style.display = "none";
        document.getElementById('rfvFirstName').style.display = "none";
        document.getElementById('rfvLastName').style.display = "block";
        document.getElementById('rfvPassword').style.display = "none";
        document.getElementById('lblError').innerHTML = "";
        document.getElementById('cvConfirmPassword').style.display = "none";
        return false;
    }
    else if (document.getElementById('txtPasswordSignUp').value == '') {
        document.getElementById('rfvEmail').style.display = "none";
        document.getElementById('rfvFirstName').style.display = "none";
        document.getElementById('rfvLastName').style.display = "none";
        document.getElementById('rfvPassword').style.display = "block";
        document.getElementById('lblError').innerHTML = "";
        document.getElementById('cvConfirmPassword').style.display = "none";
        return false;
    }
    else if (document.getElementById('txtConfirmPassword').value == '') {
        document.getElementById('lblError').innerHTML = "Confirmed Password Required";
        document.getElementById('rfvEmail').style.display = "none";
        document.getElementById('rfvFirstName').style.display = "none";
        document.getElementById('rfvLastName').style.display = "none";
        document.getElementById('rfvPassword').style.display = "none";
        document.getElementById('cvConfirmPassword').style.display = "none";
        return false;
    }
    else if (document.getElementById('txtPasswordSignUp').value != document.getElementById('txtConfirmPassword').value) {
        document.getElementById('lblError').innerHTML = "";
        document.getElementById('rfvEmail').style.display = "none";
        document.getElementById('rfvFirstName').style.display = "none";
        document.getElementById('rfvLastName').style.display = "none";
        document.getElementById('rfvPassword').style.display = "none";
        document.getElementById('cvConfirmPassword').style.display = "block";
        return false;
    } else {

        if (!document.getElementById('chkTerms').checked) {
            document.getElementById('lblError').innerHTML = "Please Check Terms of Use";
            return false;
    } else {
        document.getElementById('lblError').innerHTML = "";
        document.getElementById('rfvEmail').style.display = "none";
        document.getElementById('rfvFirstName').style.display = "none";
        document.getElementById('rfvLastName').style.display = "none";
        document.getElementById('rfvPassword').style.display = "none";
        document.getElementById('cvConfirmPassword').style.display = "none";
        var chk = checkEmail(document.getElementById('txtEmail').value);
        return chk
    }
    }
}



function checkEmail(email) {

    debugger;
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!filter.test(email)) {
        document.getElementById('txtEmail').focus;
        document.getElementById('revdEmail').style.display = "block";
        return false;
    } else {
        document.getElementById('revdEmail').style.display = "none";
        return true;
    }
}