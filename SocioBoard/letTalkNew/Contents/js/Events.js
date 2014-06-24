$(document).ready(function () {
    $("#eventtabs").hide();

    $(function () {
        var date = $("#ContentPlaceHolder1_txtDate").datepicker({ dateFormat: 'yy/mm/dd' }).val();
    });
});

var fbUserId = "";
var fbNetwork = "";

function getFacebookProfileId(fbProfilrid, network) {
//    alert(fbProfilrid);
    //    alert(network);
    $("#eventtabs").show();
    fbUserId = fbProfilrid;
    fbNetwork = network;
}

function createEvnt() {
   
    if (fbNetwork == 'fb') {
        //alert('fb')

        var name = $("#ContentPlaceHolder1_txtName").val();
        var details = $("#ContentPlaceHolder1_txtDetails").val();
        var where = $("#ContentPlaceHolder1_txtWhere").val();
        var when = $("#ContentPlaceHolder1_txtDate").val();


        $.ajax({
            type: "POST",
            url: "../Event/AjaxEvents.aspx?op=createEvent",
            data: "{'fbUserId':'" + fbUserId + "','name':'" + name + "','details':'" + details + "','where':'" + where + "','when':'" + when + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                try {
                    if (msg["type"].toString().contains("logout")) {
                       // alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  
                console.log(msg);
                //alert(msg);
                if (msg != null && msg !="") {
                    alert("Success !");
                }
            },
            error: function (errMsg) {
                alert("Failure !");
            }
        });
    }
}

function getEvntDetails() {
    //alert('asd');
    //return false;
//    alert(fbUserId);
//    alert(fbNetwork);
    if (fbNetwork == 'fb') {
        alert('fb')
        $.ajax({
            type: "POST",
            url: "../Event/AjaxEvents.aspx?op=getEventDetails",
            data: "{'fbUserId':'" + fbUserId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                try
                {
                if (msg["type"].toString().contains("logout")) {
                    //alert("logout");
                    window.location = "../Default.aspx?hint=logout";
                    return false;
                }
                }
                catch(e)
                {
                }  

                //console.log(msg);
                console.log(msg['data']);



                if (msg != null && msg != "") {
                    $.each(msg['data'], function (i, val) {

                        //                        alert(val.name);
                        //                        alert(val.start_time);
                        //                        alert(val.timezone);
                        //                        alert(val.location);

                        $("#addEvntDetails").append('<li>'
                        + '<img src="http://graph.facebook.com/' + fbUserId + '/picture?type=small">'
                        + '<span>' + val.name + '</span>'// + '<span>' + val.id + '</span>' // Event Id
                        + '<p>' + val.timezone + '</p>'
                        + '<p><span class="date-eve">' + val.start_time + '</span><span class="place-eve">' + val.location + '</span></p>'
                     + '</li>');
                    });
                }
            },
            error: function (errMsg) {
                alert("Failure !");
            }
        });
    }
}

