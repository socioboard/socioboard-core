
var grpId = "";
var accToken = "";
var fuid = "";

function facebookgroupdetails(gid, fbUserId, fuid) {
    $(".gcontent").empty();
    //$(".gcontent").append('<img src=../Contents/img/43px_on_transparent1.gif/>');
    //alert("abhay");
    //alert(gid);
    //alert(fbUserId);




    debugger;


    grpId = gid;
    accToken = fbUserId;
    fuid = fuid;
    $.ajax({
        url: "../Group/AjaxGroup.aspx?op=getFBGroupFeeds",
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
                    var fbmsg = "";
                    if (val.picture === undefined) {
                        if (val.message === undefined) {

                        }
                        else {
                            fbmsg = val.message;
                        }

                        $(".gcontent").append('<div id="abhay" class="storyContent"><a class="actorPhoto">'
                              + '<img src="http://graph.facebook.com/' + val.from.id + '/picture?type=small"></a>'
                          + '<div class="storyInnerContent">'
                              + '<div class="actordescription">'
                                  + '<a class="passiveName">' + val.from.name + '</a>' + myDate(val.created_time) + ''
                             + '</div>'
                              + '<div class="messagebody">' + val.message + '</div>'
                          + '</div>'//below code is used for comment in group
                               + '<a href="#" class="retweets"></a>'
                               + '<p><span class="comment" onclick="commentText(\'' + val.id + '\');" id="commentfb_' + val.id + '">Comment</span></p>'
                               + '<p class="commeent_box"><input type="text" class="put_comments" id="textfb_' + val.id + '"></p>'
                               + '<p><span class="ok" id="okfb_' + val.id + '" onclick="commentFBGroup(\'' + val.id + '\',\'' + fuid + '\')">ok</span><span class="cancel" onclick="cancelFB(\'' + val.id + '\');" id="cancelfb_' + val.id + '"> cancel</span></p>'
                     + '</div>');
                    }
                    else {
                        var pic = val.picture;

                        if (pic.indexOf("?") != -1) {
                            pic = pic;
                        }
                        else {
                            if (pic.indexOf("_s.jpg") != -1) {
                                pic = pic.replace(/_s.jpg/g, '_n.jpg');
                            }
                        }

                        //                        if (pic.indexOf("_s.jpg") != -1) {
                        //                            pic = pic.replace(/_s.jpg/g, '_n.jpg');
                        //                        }
                        if (val.message === undefined) {

                        }
                        else {
                            fbmsg = val.message;
                        }



                       // $(".gcontent").append('<div class="storyContent"><a class="actorPhoto">'
                       //         + '<img src="http://graph.facebook.com/' + val.from.id + '/picture?type=small"></a>'
                       //     + '<div class="storyInnerContent">'
                       //         + '<div class="actordescription">'
                       //             + '<a href="http://facebook.com/' + val.from.id + '" target="_blank" class="passiveName">' + val.from.name + '</a> ' + myDate(val.created_time) + ''
                       //        + '</div>'
                       //         + '<div class="messagebody">' + fbmsg + '</div>'
                       //           + '<img src="' + pic + '">'
                       //     + '</div>'
                       //+ '</div>');



                        $(".gcontent").append('<div id="abhay" class="storyContent"><a class="actorPhoto">'
                               + '<img src="http://graph.facebook.com/' + val.from.id + '/picture?type=small"></a>'
                           + '<div class="storyInnerContent">'
                               + '<div class="actordescription">'
                                   + '<a class="passiveName">' + val.from.name + '</a>' + myDate(val.created_time) + ''
                              + '</div>'
                               + '<div class="messagebody">' + val.message + '</div>'
                           + '</div>'//below code is used for comment in group
                                + '<a href="#" class="retweets"></a>'
                                + '<p><span class="comment" onclick="commentText(\'' + val.id + '\');" id="commentfb_' + val.id + '">Comment</span></p>'
                                + '<p class="commeent_box"><input type="text" class="put_comments" id="textfb_' + val.id + '"></p>'
                                + '<p><span class="ok" id="okfb_' + val.id + '" onclick="commentFBGroup(\'' + val.id + '\',\'' + fuid + '\')">ok</span><span class="cancel" onclick="cancelFB(\'' + val.id + '\');" id="cancelfb_' + val.id + '"> cancel</span></p>'
                      + '</div>');




                    }


                    //                    $(".gcontent").append('<div class="storyContent"><a class="actorPhoto">'
                    //                                + '<img src="http://graph.facebook.com/' + val.from.id + '/picture?type=small"></a>'
                    //                            + '<div class="storyInnerContent">'
                    //                                + '<div class="actordescription">'
                    //                                    + '<a href="http://facebook.com/' + val.from.id + '" target="_blank" class="passiveName">' + val.from.name + '</a> updated the description.'
                    //                               + '</div>'
                    //                                + '<div class="messagebody">' + val.message + '</div>'
                    //                            + '</div>'
                    //                       + '</div>');





                    //var ob = jQuery.parseJSON(val)
                    //console.log(ob.id);
                });
            }



            else {
                alert('No comment found');

            }
        },
        error: function () {
            //alert("failure");
            //$("#result").html('There is error while submit');
        }
    });
}

//function bindgrpdata(nxtpage) {
//$('#Groupsid').attr("/feed?limit=25&until=1401654656&__paging_token=enc_Aez3xAqqEyaMLpIglwNhfd-b-6y-mZ4yikny4_dyK1YMsuRFV0TIyn2IisCs5lCWj9tHloeYAbRKaAABcEVlwsAL", nxtpage);
//BindMidSnaps(loadtype,nxtpage)
//}

//function BindMidSnaps(loadtype) {
//debugger;
//    $("#hm_loader").css('display', 'block');
//    $("#hm_loader").attr('src', '../Contents/img/43px_on_transparent1.gif');

//    $.ajax
//        ({
//            type: "POST",
//            url: "../Group/AjaxGroup.aspx?op=scroll",
//            contentType: "application/json; charset=utf-8",
//            dataType: "html",
//            success: function (msg) {

//                debugger;
//                $(document).on('scroll', onScroll);
//                $(".gcontent").append(msg);
//                $("#hm_loader").attr("src", "").css('display', 'none');
//            }
//        });
//}
//function onScroll(event) {
//    var closeToBottom = ($(window).scrollTop() + $(window).height() > $(document).height() - 100);
//    if (closeToBottom) {
//        $(document).off('scroll', onScroll);
//        BindMidSnaps("scroll");
//    }
//}


//$(function () {
//    $(window).scroll(function () {
//        var aTop = $('.gcontent').height();
//        if ($(this).scrollTop() >= aTop) {
//            alert('header just passed.');
//            // instead of alert you can use to show your ad
//            // something like $('#footAd').slideup();
//        }
//    });
//});

//$(document).scroll(function () {
//    debugger;
//    var y_scroll_pos = ($(window).height() > $(document).height() - 20);
//    var scroll_pos_test = 150;
//    debugger;
//});

$(window).scroll(function () {
    debugger;
    //if ($(window).scrollTop() + $(window).height() > $(document).height() - 100) {
        

    //} 
    GetMessagesOnScroll();
});


function GetMessagesOnScroll() {
    debugger;


    $.ajax({
        url: "../Group/AjaxGroup.aspx?op=scroll",
        type: "post",
        data: "{'gid':'" + grpId + "','ack':'" + accToken + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // data: 'blog_id=' + blog_id + '&type=C_GetAllComment',
        success: function (message) {

            $('div .alert_suite_title > a').attr("gid", grpId);
            debugger;
            if (message != "") {
                //  console.log(datain["data"]);
                $.each(message["data"], function (i, val) {
                    console.log(val.from.name);
                    console.log(val.message);
                    var fbmsg = "";
                    if (val.picture === undefined) {
                        if (val.message === undefined) {

                        }
                        else {
                            fbmsg = val.message;
                        }

                        $(".gcontent").append('<div id="abhay" class="storyContent"><a class="actorPhoto">'
                               + '<img src="http://graph.facebook.com/' + val.from.id + '/picture?type=small"></a>'
                           + '<div class="storyInnerContent">'
                               + '<div class="actordescription">'
                                   + '<a class="passiveName">' + val.from.name + '</a>' + myDate(val.created_time) + ''
                              + '</div>'
                               + '<div class="messagebody">' + val.message + '</div>'
                           + '</div>'//below code is used for comment in group
                                + '<a href="#" class="retweets"></a>'
                                + '<p><span class="comment" onclick="commentText(\'' + val.id + '\');" id="commentfb_' + val.id + '">Comment</span></p>'
                                + '<p class="commeent_box"><input type="text" class="put_comments" id="textfb_' + val.id + '"></p>'
                                + '<p><span class="ok" id="okfb_' + val.id + '" onclick="commentFBGroup(\'' + val.id + '\',\'' + fuid + '\')">ok</span><span class="cancel" onclick="cancelFB(\'' + val.id + '\');" id="cancelfb_' + val.id + '"> cancel</span></p>'
                      + '</div>');
                    }
                    else {
                        var pic = val.picture;

                        if (pic.indexOf("?") != -1) {
                            pic = pic;
                        }
                        else {
                            if (pic.indexOf("_s.jpg") != -1) {
                                pic = pic.replace(/_s.jpg/g, '_n.jpg');
                            }
                        }

                        //                        if (pic.indexOf("_s.jpg") != -1) {
                        //                            pic = pic.replace(/_s.jpg/g, '_n.jpg');
                        //                        }
                        if (val.message === undefined) {

                        }
                        else {
                            fbmsg = val.message;
                        }


                        $(".gcontent").append('<div id="abhay" class="storyContent"><a class="actorPhoto">'
                                                      + '<img src="http://graph.facebook.com/' + val.from.id + '/picture?type=small"></a>'
                                                  + '<div class="storyInnerContent">'
                                                      + '<div class="actordescription">'
                                                          + '<a class="passiveName">' + val.from.name + '</a>' + myDate(val.created_time) + ''
                                                     + '</div>'
                                                      + '<div class="messagebody">' + val.message + '</div>'
                                                  + '</div>'//below code is used for comment in group
                                                       + '<a href="#" class="retweets"></a>'
                                                       + '<p><span class="comment" onclick="commentText(\'' + val.id + '\');" id="commentfb_' + val.id + '">Comment</span></p>'
                                                       + '<p class="commeent_box"><input type="text" class="put_comments" id="textfb_' + val.id + '"></p>'
                                                       + '<p><span class="ok" id="okfb_' + val.id + '" onclick="commentFBGroup(\'' + val.id + '\',\'' + fuid + '\')">ok</span><span class="cancel" onclick="cancelFB(\'' + val.id + '\');" id="cancelfb_' + val.id + '"> cancel</span></p>'
                                             + '</div>');


                    }


                    //                    $(".gcontent").append('<div class="storyContent"><a class="actorPhoto">'
                    //                                + '<img src="http://graph.facebook.com/' + val.from.id + '/picture?type=small"></a>'
                    //                            + '<div class="storyInnerContent">'
                    //                                + '<div class="actordescription">'
                    //                                    + '<a href="http://facebook.com/' + val.from.id + '" target="_blank" class="passiveName">' + val.from.name + '</a> updated the description.'
                    //                               + '</div>'
                    //                                + '<div class="messagebody">' + val.message + '</div>'
                    //                            + '</div>'
                    //                       + '</div>');





                    //var ob = jQuery.parseJSON(val)
                    //console.log(ob.id);
                });
            }



            else {
                alert('No comment found');

            }
        },
        error: function () {
           // alert("failure");
            //$("#result").html('There is error while submit');
        }
    });

}




function myDate(dt) {
    var msgarr = dt.split('T');
    var arrmsg1 = msgarr[0].split('-');

    var month = new Array();
    month[1] = "January";
    month[2] = "February";
    month[3] = "March";
    month[4] = "April";
    month[5] = "May";
    month[6] = "June";
    month[7] = "July";
    month[8] = "August";
    month[9] = "September";
    month[10] = "October";
    month[11] = "November";
    month[12] = "December";
    var n = month[parseInt(arrmsg1[1])];
    return arrmsg1[2] + " " + n;
}


function postFBGroupFeeds() {
    var gid = grpId;
    var fbUserId = accToken;
    var msg = $('#txtcmt').val();
    if (msg == "" || msg == null) {
        alert("Please enter in Comment Box");
        return false;
    }
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
                document.getElementById('txtcmt').value = "";
                // $('#close').live('click', function (e) {
                $('#close').click('click', function (e) {
                    $('#popupchk12').bPopup().close();
                });
                //to close a popupbox end

                $('#close').click();
            }
            else {
                // alert("failure");
                //alert("Please enter in Comment Box");
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