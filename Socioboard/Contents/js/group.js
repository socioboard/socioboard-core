
var grpId = "";
var accToken = "";
function facebookgroupdetails(gid, fbUserId) {
    $(".gcontent").empty();
    //alert("abhay");
    //alert(gid);
    //alert(fbUserId);

    grpId = gid;
    accToken = fbUserId;

    $.ajax({
        url: "../Group/AjaxGroup.aspx",
        type: "post",
        data: "{'gid':'" + gid + "','ack':'" + fbUserId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // data: 'blog_id=' + blog_id + '&type=C_GetAllComment',
        success: function (message) {

            $('div .alert_suite_title > a').attr("gid", gid);
            debugger;
            if (message != "") {
                //  console.log(datain["data"]);
                $.each(message["data"], function (i, val) {
                    console.log(val.from.name);
                    console.log(val.message);



                    $(".gcontent").append('<div class="storyContent"><a class="actorPhoto">'
                                + '<img src="http://graph.facebook.com/' + val.from.id + '/picture?type=small"></a>'
                            + '<div class="storyInnerContent">'
                                + '<div class="actordescription">'
                                    + '<a href="http://facebook.com/' + val.from.id + '" target="_blank" class="passiveName">' + val.from.name + '</a> updated the description.'
                               + '</div>'
                                + '<div class="messagebody">' + val.message + '</div>'
                            + '</div>'
                       + '</div>');





                    //var ob = jQuery.parseJSON(val)
                    //console.log(ob.id);
                });
            }



            else {
                alert('No comment found');

            }
        },
        error: function () {
            alert("failure");
            //$("#result").html('There is error while submit');
        }
    });
}

function postFBGroupFeeds() {
    var gid = grpId;
    var fbUserId = accToken;
    var msg = $('#txtcmt').val();

    //alert(msg);


    $.ajax({
        url: "../Group/AjaxGroup.aspx?op=postFBGroupFeeds",
        type: "post",
        data: "{'gid':'" + gid + "','ack':'" + fbUserId + "','msg':'" + msg + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //alert(msg);
            if (msg != "") {
                alert("Success !!");
                facebookgroupdetails(gid, fbUserId);
                //to close a popupbox begin
                $('#close').live('click', function (e) {
                    $('#popupchk').bPopup().close();
                });
                //to close a popupbox end

                $('#close').click();
            }
            else {
                alert("failure");
            }

        },
        error: function () {
            alert("failure");
        }

    });

}












function abc() {
    $(".gcontent").append('<div id="abhay" class="storyContent"><a class="actorPhoto">'
                                + '<img src="https://fbcdn-profile-a.akamaihd.net/hprofile-ak-prn2/211863_569212492_1347003669_q.jpg" alt=""></a>'
                            + '<div class="storyInnerContent">'
                                + '<div class="actordescription">'
                                    + '<a class="passiveName">Tommy Skaue </a>updated the description.'
                               + '</div>'
                                + '<div class="messagebody">'
                                    + 'Facebook group for all you Asp.Net developers out there who find MVC (Model View'
                                   + 'Controller) interesting. My intention for this group is to share interesting links'
                                    + 'to blogs, articles or demonstration-code related to Asp.Net MVC. Users might offer'
                                    + 'answers and hints if you have certain questions or issues, but you may want to consider'
                                    + 'visiting the forum found here <a href="#">http://www.asp.net/mvc</a>'
                                + '</div>'
                            + '</div>'
                       + '</div>');
}