var bindmessageajax = '';
var bindprofilesajax = '';
//alert("Message");
function BindMessages() {
    debugger;
    bindmessageajax = $.ajax
        ({
            type: "POST",
            url: "../Message/AjaxMessage.aspx?op=bindMessages",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                $("#inbox_msgs").html(msg);
            }
        });


    }


    function BindProfilesInMessageTab() {
        bindprofilesajax = $.ajax
        ({
            type: "POST",
            url: "../Message/AjaxMessage.aspx?op=bindProfiles",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                $("#accordianprofiles").html(msg);

            }
        });
    }