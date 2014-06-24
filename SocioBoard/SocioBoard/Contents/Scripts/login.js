// Login Form

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