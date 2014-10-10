
$(function () {

    var button = $('#loginButton');
    var box = $('#loginBox');
    button.removeAttr('href');
    button.mouseup(function (login) {
        box.toggle();
        button.toggleClass('active');
    });

    $(this).mouseup(function (login) {
        if (!($(login.target).parent('#loginButton').length > 0)) {
            button.removeClass('active');
            box.hide();
        }
    });
});

$(function () {

    var button = $('#loginButton_Woo');
    var box = $('#loginBox_Woo');
    button.removeAttr('href');
    button.mouseup(function (login) {
        box.toggle();
        button.toggleClass('active');
    });

    $(this).mouseup(function (login) {
        if (!($(login.target).parent('#loginButton_Woo').length > 0)) {
            button.removeClass('active');
            box.hide();
        }
    });




});




$(function () {
    try {
        var button = $('#addButton');
        var box = $('#addBox');

        button.removeAttr('href');
        button.mouseup(function (add) {
            box.toggle();
            button.toggleClass('active');
        });

        $(this).mouseup(function (add) {
            if (!($(login.target).parent('#addButton').length > 0)) {
                button.removeClass('active');
                box.hide();
            }
        });
    } catch (e) {

    }
});





$(function () {
    try {
        var button = $('#abb_scheduler');
        var box = $('#ab_scheduler');

        button.removeAttr('href');
        button.mouseup(function (add) {
            box.toggle();
            button.toggleClass('active');
        });

        $(this).mouseup(function (add) {
            if (!($(login.target).parent('#abb_scheduler').length > 0)) {
                button.removeClass('active');
                box.hide();
            }
        });
    } catch (e) {

    }
});







$(function () {
    try {
        var button = $('#loginButton_scheduler');
        var box = $('#loginBox_scheduler');
        button.removeAttr('href');
        button.mouseup(function (add) {
            box.toggle();
            button.toggleClass('active');
        });

        $(this).mouseup(function (add) {
            if (!($(login.target).parent('#loginButton_scheduler').length > 0)) {
                button.removeClass('active');
                box.hide();
            }
        });
    } catch (e) {

    }
});



$(function () {

    var button = $('#expanderHead');
    var box = $('#linkbox');
    button.removeAttr('href');
    button.mouseup(function (login) {
        box.toggle();
        button.toggleClass('active');
    });

    $(this).mouseup(function (login) {
        if (!($(login.target).parent('#expanderHead').length > 0)) {
            button.removeClass('active');
            box.hide();
        }
    });
});

//-------------- this js for addico

$(function () {



    var button = $('#addico');
    var box = $('#addicbox');


    button.removeAttr('href');
    button.mouseup(function (login) {
        box.toggle();
        button.toggleClass('active');
    });

    $(this).mouseup(function (login) {
        if (!($(login.target).parent('#addico').length > 0)) {
            button.removeClass('active');
            box.hide();
        }
    });

    $('body').click(function (login) {
        if (!($(login.target).parent('#addico').length > 0)) {
            button.removeClass('active');
            box.hide();
        }
    });



});


// -------------- usersetting -------------

$(function () {

    var button = $('#usersetting');
    var box = $('#userset');
    button.removeAttr('href');
    button.mouseup(function (login) {
        box.toggle();
        button.toggleClass('active');
    });
    $('body').click(function (login) {
        if (!($(login.target).parent('#usersetting').length > 0)) {
            button.removeClass('active');
            box.hide();
        }
    });
    $(this).mouseup(function (login) {
        if (!($(login.target).parent('#usersetting').length > 0)) {
            button.removeClass('active');
            box.hide();
        }
    });
});

/************************invite team members*************************/

$(function () {

    var button = $('#masterInvite');
    var box = $('#inviteTeam');
    button.removeAttr('href');
    button.mouseup(function (login) {
        box.toggle();
        button.toggleClass('active');
    });


    $(this).mouseup(function (login) {
        if (!($(login.target).parent('#inviteTeam').length > 0)) {
            button.removeClass('active');
            box.hide();
        }
    });

    $('body').click(function (login) {
        if (!($(login.target).parent('#master_invite').length > 0)) {
            button.removeClass('active');
            box.hide();
        }
    });


});

/**************************invite from home **********************************/

$(function () {


    var button = $('#invitefromHome');
    var box = $('#inviteAthome');

    button.removeAttr('href');
    button.mouseup(function (login) {
        box.toggle();
        button.toggleClass('active');
    });

    $(this).mouseup(function (login) {
        if (!($(login.target).parent('#inviteAthome').length > 0)) {
            button.removeClass('active');
            box.hide();
        }
    });
});


/* =============== twitter reports =========================== */
$(function () {

    var button = $('#twtrpt');
    var box = $('#twtrptbox');
    button.removeAttr('href');
    button.mouseup(function (login) {
        box.toggle();
        button.toggleClass('active');
    });

    $(this).mouseup(function (login) {
        if (!($(login.target).parent('#twtrptbox').length > 0)) {
            button.removeClass('active');
            box.hide();
        }
    });
});

/* =============== Facebook reports =========================== */
$(function () {

    var button = $('#facebook_page');
    var box = $('#facebookbox');
    button.removeAttr('href');
    button.mouseup(function (login) {
        box.toggle();
        button.toggleClass('active');
    });

    $(this).mouseup(function (login) {
        if (!($(login.target).parent('#facebookbox').length > 0)) {
            button.removeClass('active');
            box.hide();
        }
    });
});