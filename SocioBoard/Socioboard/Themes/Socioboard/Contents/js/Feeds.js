var tumblrProfileid = '';
var feedajax = '';
var bindfeedsprofiles = '';
var updatingwallposts = '';
var feedsdataforfacebook = '';
var feedsdatafortwitter = '';
var feedprofilesforfacebook = '';
var feedprofilesfortwitter = '';
var instagramimagesforfeeds = '';
var tumblrimagesforfeeds = '';
var youtubechannelforfeeds = '';
//  woosuite variables

var facebookwallposts = '';
var facebookscheduler = '';
var facebookfeeds = '';

var twittertweets = '';
var twitterscheduler = '';
var twitterfeeds = '';

var linkedinwallposts = '';
var linkedinscheduler = '';
var linkedinfeeds = '';

var tabdata = '';
var instagramidforlazyload = '';
var tumblridforlazyload = '';
var youtubeidforlazyload = '';



function BindFeeds(network) {

    var checkbool = true;
    debugger;
    if (feedsdataforfacebook.indexOf('<div id=\"messagetaskable_') != -1 && network == "facebook") {
        checkbool = false;
    } else if (feedsdatafortwitter.indexOf('<div id=\"messagetaskable_') != -1 && network == "twitter") {
        checkbool = false;
    }

    if (checkbool) {
        feedajax = $.ajax
        ({
            type: "POST",
            url: "../Feeds/AjaxFeed.aspx?op=bindFeeds&network=" + network,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            cache: true,
            success: function (msg) {
                $("#inbox_msgs").html(msg);
                if (network == 'facebook') {
                    feedsdataforfacebook = msg;
                } else if (network == "twitter") {
                    feedsdatafortwitter = msg;
                }

                BindProfilesInFeedsTab(network);
                //     UpdatingWallPostfromFacebook();
            }
        });

    } else if (feedsdataforfacebook.indexOf('<div id=\"messagetaskable_') != -1 && network == "facebook") {
        $("#inbox_msgs").html(feedsdataforfacebook);
        BindProfilesInFeedsTab("facebook");
    } else if (feedsdatafortwitter.indexOf('<div id=\"messagetaskable_') != -1 && network == "twitter") {
        $("#inbox_msgs").html(feedsdatafortwitter);
        BindProfilesInFeedsTab("twitter");
    }
}


function BindProfilesInFeedsTab(network) {
    var boolcheck = true;

    debugger;
    bindfeedsprofiles.abort();
    if (feedprofilesforfacebook.indexOf('<li><a id=\"greencheck_') != -1 && network == "facebook") {
        boolcheck = false;
    } else if (feedprofilesfortwitter.indexOf('<li><a id=\"greencheck_') != -1 && network == "twitter") {
        boolcheck = false;
    }
    if (boolcheck) {
        bindfeedsprofiles =
    $.ajax
        ({
            type: "POST",
            url: "../Feeds/AjaxFeed.aspx?op=bindProfiles&network=" + network,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                $("#accordianprofiles").html(msg);
                if (network == "facebook") {
                    feedprofilesforfacebook = msg;
                } else if (network == "twitter") {
                    feedprofilesfortwitter = msg;
                }
            }
        });
    } else if (feedprofilesforfacebook.indexOf('<li><a id=\"greencheck_') != -1 && network == "facebook") {
        $("#accordianprofiles").html(feedprofilesforfacebook);
    } else if (feedprofilesfortwitter.indexOf('<li><a id=\"greencheck_') != -1 && network == "twitter") {
        $("#accordianprofiles").html(feedprofilesfortwitter);
    }

}

function UpdatingWallPostfromFacebook() {
    updatingwallposts = $.ajax({

        type: "POST",
        url: "../Feeds/AjaxFeed.aspx?op=updatewallposts",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            $("inbox_msgs").html(msg);

        }
    });
}

function showinsprof(id) {
    debugger;
    $("#instaAccounts").css('display', 'block');
    $(".b-modal").css('display', 'block');
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeed.aspx?op=IntagramProfiles&mediaId=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            //  alert(msg);
            $("#accountsins").html(msg);
            $("#instaAccounts").bPopup();
        }
    });
    // $.session('id', id);
}

function postLikeRequest(mediaid, IntagramId, accessToken) {
    debugger;
    // alert(mediaid);
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeed.aspx?op=postLike&mediaId=" + mediaid + "InstagramId" + IntagramId + "access=" + accessToken,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            //   alert(msg);
            $("#accountsins").html(msg);
            $("#instaAccounts").bPopup();
        }
    });
}


/******************************************ForWoosuite*******************************************************************/


function BindProfilesforNetwork(network) {


    try {
        $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=networkprofiles&network=" + network,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {

                if (network == "facebook") {
                    debugger;
                    var fblook = '';


                    fblook = '<li><div class="feedim pull-left"><img width="31" height="31" src="../Contents/img/blank_img.png" alt=""></div><div class="pull-left feedcontent"><a style=\"cursor:default\" class="feednm" href="#">No Records Found</a> <span></span><p></p><a href="#" class="retweets"></a><span></span></div></li>'


                    if (msg.indexOf('No Records Found') != -1) {
                        debugger;
                        $("#facebookusersforfeeds").html(msg);
                        try {
                            $("#img_paneltab1").attr('src', "../Contents/img/admin/1.png");
                            $("#loader_tabpanel1").attr('src', "../Contents/img/admin/9.png");
                            $("#data_paneltab1").html(fblook);
                        } catch (e) {
                        }
                        try {
                            $("#img_paneltab2").attr('src', "../Contents/img/admin/1.png");
                            $("#loader_tabpanel2").attr('src', "../Contents/img/admin/9.png");
                            $("#data_paneltab2").html(fblook);
                        } catch (e) {
                        }
                        try {
                            $("#img_paneltab3").attr('src', "../Contents/img/admin/1.png");
                            $("#loader_tabpanel3").attr('src', "../Contents/img/admin/9.png");
                            $("#data_paneltab3").html(fblook);
                        } catch (e) {
                        }
                        try {
                            $("#title_paneltab1").html("News Feeds");
                        } catch (e) {
                        }

                        try {
                            $("#title_paneltab2").html("Wall posts");
                        } catch (e) {

                        }

                        try {
                            $("#title_paneltab3").html("Scheduled Messages");
                        } catch (e) {

                        }




                    } else {
                        $("#facebookusersforfeeds").html(msg);
                        if (msg.indexOf('facebookdetails(\'') != -1) {
                            try {
                                var start = msg.indexOf('facebookdetails(\'');
                                var last = msg.indexOf('\')');
                                var sub = msg.substring(start, last + 2);
                                var startid = sub.indexOf('(\'');
                                var lastid = sub.indexOf('\')');
                                eval(sub + "()");
                            } catch (e) {

                            }

                        }
                    }

                    /// facebook details ko call karna hai;
                } else if (network == "twitter") {
                    $("#twitterprofilesoffeed").html(msg);
                } else if (network == "linkedin") {
                    $("#linkedinprofilesforfeed").html(msg);
                } else if (network == "instagram") {
                    $("#instagramprofilesforfeed").html(msg);
                }
                else if (network == "tumblr") {
                    $("#tumblrprofilesforfeed").html(msg);
                }
                else if (network == "youtube") {
                    $("#youtubeprofilesforfeed").html(msg);
                }
            }
        });
    } catch (e) {

    }
}



function BindTwitterInDiscovery() {
    try {
        debugger;
        $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=networkprofiles&network=twitter",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                debugger;
                var msgg = msg.replace(/twitterdetails/g, "getFollowers");
                $("#twitterprofilesoffeed").html(msgg);

            }
        });
    } catch (e) {

    }
}





function getFollowers(id) {

    try {
        debugger;
        $("#inbox_msgs").html('<img src="../Contents/img/328.gif" />');

        $.ajax
        ({
            type: "GET",
            url: "../Helper/AjaxHelper.aspx?op=getFollowers&id=" + id,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                debugger;
                //$("#inbox_msgs").html('asdasdasdas');
                // $("#inbox_msgs").html(msg);
                $("#content").html(msg);
                //  $(".smart_search").html(msg);

            }
        });






    } catch (e) {
        //  alert(e);
    }


}

/*bind facebook feeds on homepage*/

function facebookdetails(id, li_id) {
    $("#first-profile-load_Id_" + li_id).parent().parent().children().children().removeClass("active");
    $("#first-profile-load_Id_" + li_id).addClass("active");
    debugger;
    var local = getlocatdatetime();
    try {
        loadfeedpartialpage = $.ajax({
            type: "POST",
            url: "../Feeds/LoadFeedPartialPage?netword=facebook&id=" + id,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    $("#page-wrapper").html(msg);
                    $('#refreshpanel1').attr('nwt', 'fb');
                    $('#refreshpanel1').attr('nwtid', id);
                    $('#refreshpanel2').attr('nwt', 'fb');
                    $('#refreshpanel2').attr('nwtid', id);
                    $('#refreshpanel3').attr('nwt', 'fb');
                    $('#refreshpanel3').attr('nwtid', id);
                    $('#data_paneltab1').attr('network', 'facebook');
                    $("#img_paneltab1").attr('src', "/Themes/Socioboard/Contents/img/admin/1.png");
                    //$("#title_paneltab1").html("News Feeds");
                    $("#title_paneltab1").html("News Feeds");
                    $('#data_paneltab2').attr('network', 'facebook');
                    $("#img_paneltab2").attr('src', "/Themes/Socioboard/Contents/img/admin/1.png");
                    //$("#title_paneltab2").html("Wall Posts");
                    $("#title_paneltab2").html("Wall posts");
                    $("#img_paneltab3").attr('src', "/Themes/Socioboard/Contents/img/admin/1.png");
                    $("#title_paneltab3").html("User Feeds");
                    $('#fbfeedsfilter').css("display", "block");
                } catch (e) {
                }
            },
            async: false
        });


        facebookwallposts = $.ajax({
            type: "POST",
            url: "../Feeds/wallposts?op=facebookwallposts&load=first&profileid=" + id,
            data: { "localtime": local },
            //contentType: "application/json; charset=utf-8",
            //dataType: "html",
            success: function (msg) {
                try {
                    $("#loader_tabpanel1").bind("click", function () {
                        // alert("refreshWallpostFacebook(id)");
                        //$("#img_paneltab1").attr('src', "../Contents/img/admin/1.png");
                        //$("#title_paneltab1").html("Wall Posts");
                        //$("#img_paneltab2").attr('src', "../Contents/img/admin/1.png");
                        //$("#title_paneltab2").html("News Feeds");
                        //$("#img_paneltab3").attr('src', "../Contents/img/admin/1.png");
                        //$("#title_paneltab4").html("Scheduled Messages");
                        //refreshWallpostFacebook(id);

                    });
                    // $("#img_paneltab1").attr('src', "../Contents/img/admin/1.png");
                } catch (e) {
                }

                try {
                    // $("#title_paneltab1").html("Wall Posts");
                } catch (e) {
                }
                try {
                    $("#data_paneltab1").html(msg);
                } catch (e) {
                }
                try {
                    $("#data_paneltab1").mCustomScrollbar("update");
                } catch (e) {

                }

            }
        });
        ////To Load feeds data every 15 seconds
        //try {
        //    FacebookLoadNewUserHome();
        //} catch (e) {

        //}

        //facebookfeeds = $.ajax({
        //    type: "POST",
        //    url: "../Feeds/AjaxFeeds?op=facebookfeeds&profileid=" + id,
        //    data: '',
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "html",
        //    success: function (msg) {
        //        try {
        //            $("#loader_tabpanel2").bind("click", function () {
        //                // alert("  refreshFeedsFacebook");
        //                //refreshFeedsFacebook(id);

        //            });
        //            //$("#img_paneltab1").attr('src', "../Contents/img/admin/1.png");
        //        } catch (e) {
        //        }
        //        try {
        //            //  $("#img_paneltab2").attr('src', "../Contents/img/admin/1.png");
        //        } catch (e) {
        //        }
        //        try {
        //            // $("#loader_tabpanel2").attr('src', "../Contents/img/admin/9.png");
        //        } catch (e) {

        //        }
        //        try {
        //            // $("#title_paneltab2").html("News Feeds");
        //        } catch (e) {

        //        }
        //        try {
        //            $("#data_paneltab2").html(msg);
        //        } catch (e) {

        //        }

        //        try {
        //            //  $("#data_paneltab2").mCustomScrollbar("update");
        //        } catch (e) {

        //        }
        //    }
        //});

        // Edited by Antima

        facebookfeeds = $.ajax({

            type: "POST",
            url: "../Feeds/AjaxFeeds?op=facebookfeeds&load=first&profileid=" + id,
            data: { "localtime": local },
            //contentType: "application/json; charset=utf-8",
            //dataType: "html",
            success: function (msg) {
                try {
                    debugger;
                    $("#loader_tabpanel2").bind("click", function () {
                        // alert("  refreshFeedsFacebook");
                        //refreshFeedsFacebook(id);

                    });
                    //$("#img_paneltab1").attr('src', "../Contents/img/admin/1.png");
                } catch (e) {
                }
                try {
                    //  $("#img_paneltab2").attr('src', "../Contents/img/admin/1.png");
                } catch (e) {
                }
                try {
                    // $("#loader_tabpanel2").attr('src', "../Contents/img/admin/9.png");
                } catch (e) {

                }
                try {
                    // $("#title_paneltab2").html("News Feeds");
                } catch (e) {

                }
                try {
                    $("#data_paneltab2").html(msg);
                } catch (e) {

                }

                try {
                    //  $("#data_paneltab2").mCustomScrollbar("update");
                } catch (e) {

                }
            }


        });

        try {
            //FacebookUserFeeds(id);
        } catch (e) {

        }

        ////To Load feeds data every 15 seconds
        //try {
        //    debugger;
        //    FacebookLoadNewNewsFeeds();
        //} catch (e) {

        //}

        //facebookscheduler = $.ajax({
        //    type: "POST",
        //    url: "../Feeds/scheduler?network=facebook&profileid=" + id,
        //    data: '',
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "html",
        //    success: function (msg) {

        //        try {
        //            $("#loader_tabpanel3").bind("click", function () {
        //                //alert("refreshSchedularMessageFacebook");
        //                //refreshSchedularMessageFacebook(id);

        //            });
        //            //$("#img_paneltab1").attr('src', "../Contents/img/admin/1.png");
        //        } catch (e) {
        //        }

        //        try {
        //            // $("#img_paneltab3").attr('src', "../Contents/img/admin/1.png");
        //        } catch (e) {

        //        }


        //        try {
        //            // $("#loader_tabpanel3").attr('src', "../Contents/img/admin/9.png");
        //        } catch (e) {

        //        }
        //        try {
        //            // $("#title_paneltab3").html("Scheduled Messages");
        //        } catch (e) {

        //        }
        //        try {
        //            $("#data_paneltab3").html(msg);
        //        } catch (e) {

        //        }
        //        try {
        //            $("#data_paneltab3").mCustomScrollbar("update");
        //        } catch (e) {

        //        }

        //    }
        //});

    } catch (e) {

    }
    return true;

}


function facebookwallscrolldata() {
    debugger;
    var local = getlocatdatetime();
    $("#img-loader-feed-panel1").css("display", "block");
    try {
        var $container = $("#data_paneltab1");
        $.ajax({
            type: "POST",
            url: "../Feeds/wallposts?op=facebookwallposts&load=scroll",
            data: { "localtime": local },
            //contentType: "application/json; charset=utf-8",
            //dataType: "html",
            success: function (facemsg) {
                debugger;
                $("#img-loader-feed-panel1").css("display", "none");
                $("#data_paneltab1").append(facemsg);
            }
        });

    } catch (e) {

    }


}
// Edited by Antima

function facebookfeedscrolldata() {
    debugger;
    $("#img-loader-feed-panel2").css("display", "block");
    var local = getlocatdatetime();
    try {
        var $container = $("#data_paneltab2");
        $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds?load=scroll",
            data: { "localtime": local },
            //contentType: "application/json; charset=utf-8",
            //dataType: "html",
            success: function (facemsg) {
                debugger;
                $("#img-loader-feed-panel2").css("display", "none");
                $("#data_paneltab2").append(facemsg);
            }
        });
    } catch (e) {
    }
    return true;

}

function FacebookUserFeeds(id) {
    debugger;
    //alert("antima");
    $("#title_paneltab3").html("User Feeds");
    //var nwtid = $('#refreshpanel2').attr('nwtid');
    $.ajax({
        type: "GET",
        url: "/Feeds/FacebookUserFeeds",
        data: { "profileid": id },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        beforeSend: function () {
            console.log("test");
        },
        success: function (msg) {
            console.log("I")

            $("#data_paneltab3").html(msg);
            console.log("testend");

        },
        error: function (e) {
            console.log("testerr");

            console.log(e);
            alert("Somthing went wrong");
        }
    });

}

//function FacebookStatus(id) {

//        $("#title_paneltab3").html("Status");

//        FacebookStatus = $.ajax({
//            type: "GET",
//            url: "/Feeds/FacebookStatus",
//            data: { "profileid": id },
//            contentType: "application/json; charset=utf-8",
//            dataType: "html",
//            beforeSend: function () {
//                console.log("test");
//            },
//            success: function (msg) {

//                    $("#data_paneltab3").html(msg);
//                    console.log("testend");




//            },
//            error: function (e) {
//                console.log("testerr");

//                console.log(e);
//                alert("gfgh");
//            }
//        });

//}


//function FacebookTag(id) {
//    console.log("FacebookTag begin")

//        $("#title_paneltab3").html("Tags");

//        FacebookTag = $.ajax({
//            type: "GET",
//            url: "/Feeds/FacebookTag",
//            data: {profileid: id},
//            contentType: "application/json; charset=utf-8",
//            dataType: "html",
//            beforeSend: function () {
//                console.log("test");
//            },
//            success: function (msg) {

//                    $("#data_paneltab3").html(msg);
//                    console.log("testend");


//            },
//            error: function (e) {
//                console.log(e);
//                console.log("testerr");
//                alert("gfgh");
//            }
//        });

//}


/*************Twitter****************/


function twitterdetails(id, li_id) {
    $("#first-profile-load_Id_" + li_id).parent().parent().children().children().removeClass("active");
    $("#first-profile-load_Id_" + li_id).addClass("active");
    var local = getlocatdatetime();
    try {
        loadfeedpartialpage = $.ajax({
            type: "POST",
            url: "../Feeds/LoadFeedPartialPage?network=twitter&id=" + id,
            data: '',
            //contentType: "application/json; charset=utf-8",
            //dataType: "html",
            success: function (msg) {
                try {
                    $("#page-wrapper").html(msg);
                    $('#refreshpanel1').attr('nwt', 'twt');
                    $('#refreshpanel1').attr('nwtid', id);
                    $('#refreshpanel2').attr('nwt', 'twt');
                    $('#refreshpanel2').attr('nwtid', id);
                    $('#refreshpanel3').attr('nwt', 'twt');
                    $('#refreshpanel3').attr('nwtid', id);
                    $('#data_paneltab1').attr('network', 'twitter');
                    $("#img_paneltab1").attr('src', "/Themes/Socioboard/Contents/img/admin/2.png");
                    $("#title_paneltab1").html("Feeds");
                    $('#data_paneltab2').attr('network', 'twitter');
                    $("#img_paneltab2").attr('src', "/Themes/Socioboard/Contents/img/admin/2.png");
                    $("#title_paneltab2").html("Tweets");
                    $("#img_paneltab3").attr('src', "/Themes/Socioboard/Contents/img/admin/2.png");
                    $("#title_paneltab3").html("User Tweet");
                    $('#twtfeedsfilter').css("display", "block");
                } catch (e) {
                }
            },
            async: false
        });




        //$.ajax({
        //    type: "POST",
        //    url: "../Feeds/TwitterNetworkDetails?profileid=" + id,
        //    data: '',
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "html",
        //    success: function (msg) {
        //        try {
        //            $("#loader_tabpanel1").bind("click", function () {
        //                //alert("refreshWallpostTwitter");
        //                //refreshWallpostTwitter(id);

        //            });
        //        } catch (e) {

        //        }

        //        try {
        //            $("#data_paneltab1").html(msg);
        //        } catch (e) {

        //        }
        //        try {
        //            $("#data_paneltab1").mCustomScrollbar("update");
        //        } catch (e) {

        //        }
        //    }
        //});

        $.ajax({
            type: "POST",
            url: "../Feeds/TwitterNetworkDetails?&load=first&profileid=" + id,
            data: { "localtime": local },
            //contentType: "application/json; charset=utf-8",
            //dataType: "html",
            success: function (msg) {
                try {
                    $("#loader_tabpanel1").bind("click", function () {
                        //alert("refreshWallpostTwitter");
                        //refreshWallpostTwitter(id);
                    });
                } catch (e) {
                }
                try {
                    $("#data_paneltab1").html(msg);
                } catch (e) {

                }
                try {
                    $("#data_paneltab1").mCustomScrollbar("update");
                } catch (e) {

                }
            }
        });








        //twitterfeeds = $.ajax({
        //    type: "POST",
        //    url: "../Feeds/TwitterFeeds?profileid=" + id,
        //    data: '',
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "html",
        //    success: function (msg) {
        //        try {
        //            $("#loader_tabpanel2").bind("click", function () {
        //                //alert(" refreshSchedularMessageTwitter");
        //                //refreshSchedularMessageTwitter(id);

        //            });
        //        } catch (e) {

        //        }

        //        try {
        //            $("#data_paneltab2").html(msg);
        //        } catch (e) {

        //        }
        //        try {
        //            $("#data_paneltab2").mCustomScrollbar("update");
        //        } catch (e) {

        //        }


        //    }
        //});

        twitterfeeds = $.ajax({
            type: "POST",
            url: "../Feeds/TwitterFeeds?&load=first&profileid=" + id,
            data: { "localtime": local },
            //contentType: "application/json; charset=utf-8",
            //dataType: "html",
            success: function (msg) {
                try {
                    $("#loader_tabpanel2").bind("click", function () {
                        //alert(" refreshSchedularMessageTwitter");
                        //refreshSchedularMessageTwitter(id);
                    });
                } catch (e) {

                }

                try {
                    $("#data_paneltab2").html(msg);
                } catch (e) {

                }
                try {
                    $("#data_paneltab2").mCustomScrollbar("update");
                } catch (e) {

                }
            }
        });

        //twitterscheduler = $.ajax({
        //    type: "POST",
        //    url: "../Feeds/scheduler?network=twitter&profileid=" + id,
        //    data: '',
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "html",
        //    success: function (msg) {

        //        try {
        //            $("#loader_tabpanel3").bind("click", function () {
        //                //alert("refreshFeedsTwitter");
        //                refreshFeedsTwitter(id);

        //            });
        //        } catch (e) {

        //        }
        //        try {
        //            $("#data_paneltab3").html(msg);
        //        } catch (e) {

        //        }
        //        try {
        //            $("#data_paneltab3").mCustomScrollbar("update");
        //        } catch (e) {

        //        }
        //    }
        //});

        //TwitterUserTweet(id);

    } catch (e) {

    }

}

function Twitterscrolldata() {
    var local = getlocatdatetime();
    $("#img-loader-feed-panel1").css("display", "block");
    debugger;
    try {

        var $container = $("#data_paneltab1");

        $.ajax({
            type: "POST",
            url: "../Feeds/TwitterNetworkDetails?load=scroll",
            data: { "localtime": local },
            //contentType: "application/json; charset=utf-8",
            //dataType: "html",
            success: function (twtmsg) {
                debugger;
                $("#img-loader-feed-panel1").css("display", "none");
                $("#data_paneltab1").append(twtmsg);
            }
        });
        // }
        //  });

    } catch (e) {
    }
}

function Twitterfeedscrolldata() {
    debugger;
    $("#img-loader-feed-panel2").css("display", "block");
    var local = getlocatdatetime();
    try {

        var $container = $("#data_paneltab2");

        $.ajax({
            type: "POST",
            url: "../Feeds/TwitterFeeds?load=scroll",
            data: { "localtime": local },
            //contentType: "application/json; charset=utf-8",
            //dataType: "html",
            success: function (twtmsg) {
                debugger;
                $("#img-loader-feed-panel2").css("display", "none");
                $("#data_paneltab2").append(twtmsg);
            }
        });
        // }
        //  });

    } catch (e) {
    }
}

function TwitterUserTweet(id) {
    try {
        debugger;
        $("#title_paneltab3").html("User Tweet");

        $.ajax({
            type: "POST",
            url: "/Feeds/TwitterUserTweet?profileid=" + id,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    debugger;
                    $("#data_paneltab3").html(msg);
                } catch (e) {

                }
            }
        });
    } catch (e) {

    }
}

function TwitterRetweets(id) {
    try {
        debugger;
        $("#title_paneltab3").html("Retweets");
        $.ajax({
            type: "POST",
            url: "../Feeds/TwitterRetweets?profileid=" + id,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    debugger;
                    $("#data_paneltab3").html(msg);
                } catch (e) {

                }
            }
        });
    } catch (e) {

    }
}

function TwitterMentions(id) {
    try {
        debugger;
        $("#title_paneltab3").html("Mentions");
        $.ajax({
            type: "POST",
            url: "../Feeds/TwitterMentions?profileid=" + id,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    debugger;
                    $("#data_paneltab3").html(msg);
                } catch (e) {

                }
            }
        });
    } catch (e) {

    }
}

function linkedindetails(id, li_id) {
    $("#first-profile-load_Id_" + li_id).parent().parent().children().children().removeClass("active");
    $("#first-profile-load_Id_" + li_id).addClass("active");
    var local = getlocatdatetime();
    try {

        debugger;

        loadfeedpartialpage = $.ajax({
            type: "POST",
            url: "../Feeds/LoadFeedPartialPage?network=linkedin",
            data: '',
            //contentType: "application/json; charset=utf-8",
            //dataType: "html",
            success: function (msg) {
                try {
                    $("#page-wrapper").html(msg);
                    $("#instag").load("../Layouts/feedstab.htm");
                    $('#refreshpanel1').attr('nwt', 'linkedin');
                    $('#refreshpanel1').attr('nwtid', id);
                    $('#refreshpanel2').attr('nwt', 'linkedin');
                    $('#refreshpanel2').attr('nwtid', id);
                    $('#refreshpanel3').attr('nwt', 'linkedin');
                    $('#refreshpanel3').attr('nwtid', id);
                    $('#data_paneltab1').attr('network', 'linkedin');
                    $("#img_paneltab1").attr('src', "/Themes/Socioboard/Contents/img/admin/5.png");
                    $("#title_paneltab1").html("Network Updates");
                    $("#img_paneltab2").attr('src', "/Themes/Socioboard/Contents/img/admin/5.png");
                    $("#title_paneltab2").html("User Updates");
                    $("#img_paneltab3").attr('src', "/Themes/Socioboard/Contents/img/admin/5.png");
                    $("#title_paneltab3").html("Scheduled Messages");
                    $("#data_paneltab2").attr('network', 'linkedin');
                } catch (e) {
                }
            },
            async: false
        });




        //linkedinwallposts = $.ajax({
        //    type: "POST",
        //    url: "../Feeds/linkedinwallposts?profileid=" + id,
        //    data: '',
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "html",
        //    success: function (msg) {
        //        try {
        //            $("#loader_tabpanel1").bind("click", function () {
        //                // alert("refreshWallpostLinkedin");
        //                //refreshWallpostLinkedin(id);

        //            });
        //            //$("#img_paneltab1").attr('src', "../Contents/img/admin/5.png");
        //        } catch (e) {
        //        }

        //        try {
        //            $("#data_paneltab1").html(msg);
        //        } catch (e) {

        //        }
        //    }
        //});

        linkedinwallposts = $.ajax({
            type: "POST",
            //  url: "../Feeds/TwitterNetworkDetails?&load=first&profileid=" + id,
            url: "../Feeds/linkedinwallposts?&load=first&profileid=" + id,
            data: { "localtime": local },
            //contentType: "application/json; charset=utf-8",
            //dataType: "html",
            success: function (msg) {
                try {
                    $("#loader_tabpanel1").bind("click", function () {
                        // alert("refreshWallpostLinkedin");
                        //refreshWallpostLinkedin(id);

                    });
                    //$("#img_paneltab1").attr('src', "../Contents/img/admin/5.png");
                } catch (e) {
                }

                try {
                    $("#data_paneltab1").html(msg);
                } catch (e) {

                }
            }
        });

        linkedinfeeds = $.ajax({
            type: "POST",
            //url: "../Feeds/LinkedinFeeds?profileid=" + id,
            url: "../Feeds/LinkedinFeeds?&load=first&profileid=" + id,
            data: { "localtime": local },
            //contentType: "application/json; charset=utf-8",
            //dataType: "html",
            success: function (msg) {

                try {
                    $("#loader_tabpanel2").bind("click", function () {
                        //alert("refreshFeedsLinkedin");
                        refreshFeedsLinkedin(id);

                    });
                    //$("#img_paneltab1").attr('src', "../Contents/img/admin/5.png");
                } catch (e) {
                }


                try {
                    //$("#img_paneltab2").attr('src', "../Contents/img/admin/5.png");
                } catch (e) {

                }


                try {
                    //$("#loader_tabpanel2").attr('src', "../Contents/img/admin/9.png");
                } catch (e) {

                }
                try {
                    //$("#title_paneltab2").html("User Updates");
                } catch (e) {

                }
                try {
                    $("#data_paneltab2").html(msg);
                } catch (e) {

                }

            }
        });





        //linkedinscheduler = $.ajax({
        //    type: "POST",
        //    url: "../Feeds/scheduler?network=linkedin&profileid=" + id,
        //    data: '',
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "html",
        //    success: function (msg) {


        //        try {
        //            $("#loader_tabpanel3").bind("click", function () {
        //                // alert("refreshSchedularMessageLinkedin");
        //                refreshSchedularMessageLinkedin(id);

        //            });
        //            //$("#img_paneltab1").attr('src', "../Contents/img/admin/5.png");
        //        } catch (e) {
        //        }



        //        try {
        //            //$("#img_paneltab3").attr('src', "../Contents/img/admin/5.png");
        //        } catch (e) {
        //        }
        //        try {
        //            // $("#loader_tabpanel3").attr('src', "../Contents/img/admin/9.png");
        //        } catch (e) {

        //        }
        //        try {
        //            //$("#title_paneltab3").html("Scheduled Messages");
        //        } catch (e) {

        //        }
        //        try {
        //            $("#data_paneltab3").html(msg);
        //        } catch (e) {

        //        }


        //    }
        //});

    } catch (e) {

    }
}

function LinkedInscrolldata() {
    var local = getlocatdatetime();
    $("#img-loader-feed-panel1").css("display", "block");
    debugger;
    try {
        var $container = $("#data_paneltab1");

        $.ajax({
            type: "POST",
            url: "../Feeds/linkedinwallposts?load=scroll",
            data: { "localtime": local },
            //contentType: "application/json; charset=utf-8",
            //dataType: "html",
            success: function (linkedinmsg) {
                debugger;
                $("#img-loader-feed-panel1").css("display", "none");
                $("#data_paneltab1").append(linkedinmsg);
            }
        });
        // }
        //  });

    } catch (e) {
    }
}
function LinkedInfeedscrolldata() {
    debugger;
    $("#img-loader-feed-panel2").css("display", "block");
    var local = getlocatdatetime();
    try {

        var $container = $("#data_paneltab2");

        $.ajax({
            type: "POST",
            url: "../Feeds/LinkedinFeeds?load=scroll",
            data: { "localtime": local },
            //contentType: "application/json; charset=utf-8",
            //dataType: "html",
            success: function (twtmsg) {
                debugger;
                $("#img-loader-feed-panel2").css("display", "none");
                $("#data_paneltab2").append(twtmsg);
            }
        });
        // }
        //  });

    } catch (e) {
    }
}

//******************* Linkedin Page******************



function linkedinpagedetails(id) {
    debugger;
    $.ajax({
        type: "POST",
        url: "../Feeds/linkedinPageWallPost?profileid=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#page-wrapper").html(msg);

            } catch (e) {
            }
        }
    });

}

function viewallcomentlink(id, cnt) {
    debugger;

    var text = document.getElementById("cmntlink_" + id).innerHTML;
    if (text == "View all Comments") {
        debugger;
        $("#allcommnetbox_" + id).css("display", "block");
        document.getElementById("cmntlink_" + id).innerHTML = "Hide all Comment";
    }
    else {
        $("#allcommnetbox_" + id).css("display", "none");
        document.getElementById("cmntlink_" + id).innerHTML = "View all Comments";
    }

}

function licomntonpagepost(pageid, updatekey) {
    debugger;
    var coment = $("#lipgcmntbx_" + updatekey).val();
    if (coment == "") {
        alertify.alert("Please write something to comment on a Post!");
        return false;
    }
    $("#linkedinpage-feeds").css("display", "block");

    $.ajax({
        type: "POST",
        url: "../Feeds/linkedinpagecomentonpost?pageid=" + pageid + "&updatekey=" + updatekey + "&comment=" + coment,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            debugger;
            linkedinpagedetails(pageid);

        }
    });




}

function LikePagePosts(Pageid, Updatekey, isLike) {
    debugger;

    $.ajax({


        type: "POST",
        url: "../Feeds/LikeCompanyPagePost?pageid=" + Pageid + "&updatekey=" + Updatekey + "&isLike=" + isLike,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (message) {


            linkedinpagedetails(Pageid);

        }
    });

}


function SendPostOnLiCompanyPage(id) {
    debugger;
    //fileimage = document.getElementById('fileuploadforpost').files[0];

    //var lpd = new FormData();
    //var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
    //if (fileimage != null) {
    //    if (hasExtension('fileuploadforpost', fileExtension)) {
    //        lpd.append('fileimg', fileimage);
    //    }
    //    else {
    //        alert("File Extention is not current. Please upload any image file");
    //        return;
    //    }
    //}
    var pageid = id;
    var post = $("#txtPostofPage").val();

    //if ((post == "" || post == null) && (fileimage == "" || fileimage == null)) {
    if ((post == "" || post == null)) {
        alert("Please write something or Attach to Post..!");
        return false;
    }
    //lpd.append("pageid", pageid);
    //lpd.append('postmessage', post);


    $.ajax({
        type: "post",
        url: "../Feeds/CreatePostOnPage?Pageid=" + pageid + "&Post=" + post,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (message) {

            $("#txtPostofPage").val('');
            linkedinpagedetails(pageid);

        }
    });

}






//***************tumblr data**********


function tumblrdetails(id, li_id) {
    $("#first-profile-load_Id_" + li_id).parent().parent().children().children().removeClass("active");
    $("#first-profile-load_Id_" + li_id).addClass("active");


    tumblridforlazyload = id;

    tumblrProfileid = id;

    loadfeedpartialpage = $.ajax({
        type: "POST",
        url: "../Feeds/LoadFeedPartialPage?network=tumblr",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#page-wrapper").html(msg);
                $("#feedimages").attr("network", "tumblr");
            } catch (e) {
            }
        }
    });

    $.ajax({
        type: "POST",
        //url: "../Feeds/TumblrImages?profileid=" + id,
        url: "../Feeds/TumblrImages?&load=first&profileid=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            $("#feedimages").html(msg);


            //$(document).on('scroll', tumblrimages);


        }
    });

}


function Tumblrscrolldata() {
    debugger;
    $.ajax({
        type: "POST",
        url: "../Feeds/TumblrImages?&load=scroll",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            $("#feedimages").append(msg);
        }
    });
}




function LikePic(profileid, id, accesstoken, accesstokensecret, liked, notes) {
    // alert(notes);
    debugger;
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds.aspx?op=LikeUnlikeTumblrImage&profileid=" + profileid + "&id=" + id + "&accesstoken=" + accesstoken + "&accesstokensecret=" + accesstokensecret + "&likes=" + liked + "&notes=" + notes,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            $.ajax({
                type: "POST",
                url: "../Feeds/AjaxFeeds.aspx?op=tumblrimages&profileid=" + profileid,
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (msg) {
                    $("#instag").html(msg);

                    $(document).on('scroll', tumblrimages);


                }
            });
        }
    });

}

function UnfollowBlog(profileid, id, accesstoken, accesstokensecret, blogname) {
    // alert(notes);
    debugger;
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds.aspx?op=UnfollowTumblrBlog&profileid=" + profileid + "&id=" + id + "&accesstoken=" + accesstoken + "&accesstokensecret=" + accesstokensecret + "&blogname=" + blogname,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            $.ajax({
                type: "POST",
                url: "../Feeds/AjaxFeeds.aspx?op=tumblrimages&profileid=" + profileid,
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (msg) {
                    $("#instag").html(msg);

                    // $(document).on('scroll', tumblrimages);


                }
            });
        }
    });

}










function tumblrimage(src) {
    debugger;

    if (src.indexOf('_s.png') != -1) {
        var newsrc = src.replace('_s.png', '_n.png');
        $("#popupimages").attr('src', newsrc);
    } else if (src.indexOf('_s.jpg') != -1) {
        var newsrcP = src.replace('_s.jpg', '_n.jpg');
        $("#popupimages").attr('src', newsrcP);
    } else {
        $("#popupimages").attr('src', src);
    }
    $("#tumblrImagePopup").bPopup({

        positionStyle: 'fixed'

    });

}
function tumblrimageclose() {

    $("#popupimages").attr('src', "../Contents/img/43px_on_transparent.gif");

}


function youtubevideo(src) {
    debugger;

    if (src.indexOf('_s.png') != -1) {
        var newsrc = src.replace('_s.png', '_n.png');
        $("#popupimagesyt").attr('src', newsrc);
    } else if (src.indexOf('_s.jpg') != -1) {
        var newsrcP = src.replace('_s.jpg', '_n.jpg');
        $("#popupimagesyt").attr('src', newsrcP);
    } else {
        //src = "https://www.youtube.com/watch?v=" + src;
        src = "http://www.youtube.com/embed/" + src;
        $("#popupimagesyt").attr('src', src);
    }
    $("#youtubeImagePopup").bPopup({

        positionStyle: 'fixed'

    });

}
function tumblrimageclose() {

    $("#popupimages").attr('src', "../Contents/img/43px_on_transparent.gif");

}


function Bpopup() {

    $('#tumblrcontent').bPopup();

}

function postTextOnTumblr() {
    try {

        var profileid = tumblrProfileid;
        var title = $('#inputTextTitle').val();
        var msg = $('#inputTextMessage').val();

        if (title == "" || title == null) {
            alert("Please enter title");
            return false;
        }
        if (msg == "" || msg == null) {
            alert("Please enter in Comment Box");
            return false;
        }
        $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=tumblrTextPost&profileid=" + profileid + "&msg=" + msg + "&title=" + title,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {

            }
        });
    } catch (e) {

    }
}



function postPhotoOnTumblr() {

    try {
        var fd = new FormData();

        var filesimage = document.getElementById('txtUrl').files[0];
        //  alert(filesimage);
        fd.append('file', filesimage);


        var profileid = tumblrProfileid;
        var caption = $('#txtCaption').val();


        $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=tumblrImagePost&profileid=" + profileid + "&caption=" + caption,
            data: fd,
            processData: false,
            contentType: false,
            dataType: "html",
            success: function (msg) {


            }
        });



    } catch (e) {
        alert(e);
    }


}

function postQuoteOnTumblr() {

    try {

        var profileid = tumblrProfileid;
        var quote = $('#txtQuote').val();
        var source = $('#txtQuotesource').val();


        if (quote == "" || quote == null) {
            alert("Please select quote!");
            return false;
        }
        if (source == "" || source == null) {
            alert("Please select source!");
            return false;
        }
        $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=tumblrQuotePost&profileid=" + profileid + "&source=" + source + "&quote=" + quote,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {


            }
        });



    } catch (e) {

    }


}
function postLinkOnTumblr() {

    try {

        var profileid = tumblrProfileid;
        var linkurl = $('#txtUrls').val();
        var title = $('#txtTitle').val();
        var description = $('#txtDescription').val();

        if (linkurl == "" || linkurl == null) {
            alert("Please select quote!");
            return false;
        }
        if (title == "" || title == null) {
            alert("Please select source!");
            return false;
        }
        $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=tumblrLinkPost&profileid=" + profileid + "&linkurl=" + linkurl + "&title=" + title + "&description=" + description,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {


            }
        });



    } catch (e) {

    }


}




function postChatOnTumblr() {

    try {

        var profileid = tumblrProfileid;
        var title = $('#txtChatTitle').val();
        var body = $('#txtChatBody').val();
        var tag = $('#txtChatTag').val();

        if (body == "" || body == null) {
            alert("Please Enter!");
            return false;
        }

        $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=tumblrChatPost&profileid=" + profileid + "&title=" + title + "&body=" + body + "&tag=" + tag,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {


            }
        });



    } catch (e) {

    }


}

function postAudioOnTumblr() {

    try {
        var fd = new FormData();

        var filesaudio = document.getElementById('fileAudio').files[0];

        fd.append('file', filesaudio);


        var profileid = tumblrProfileid;
        // var caption = $('#txtCaption').val();


        $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=tumblrAudioPost&profileid=" + profileid,
            data: fd,
            processData: false,
            contentType: false,
            dataType: "html",
            success: function (msg) {


            }
        });



    } catch (e) {
        alert(e);
    }


}

function postVideoOnTumblr() {
    debugger;
    try {
        var fd = new FormData();

        var filesvideo = document.getElementById('fileVideo').files[0];

        fd.append('file', filesvideo);

        var VideoUrl = $('#txtVideoUrl').val();
        var VideoContent = $('#txtVideoContent').val();
        var profileid = tumblrProfileid;


        $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=tumblrVideoPost&profileid=" + profileid + "&VideoUrl=" + VideoUrl + "&VideoContent=" + VideoContent,
            data: fd,
            processData: false,
            contentType: false,
            dataType: "html",
            success: function (msg) {

            }
        });



    } catch (e) {
        alert(e);
    }


}



//***************youtube data**********


function youtubedetails(id, li_id) {
    $("#first-profile-load_Id_" + li_id).parent().parent().children().children().removeClass("active");
    $("#first-profile-load_Id_" + li_id).addClass("active");


    loadfeedpartialpage = $.ajax({
        type: "POST",
        url: "../Feeds/LoadFeedPartialPage?network=youtube",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#page-wrapper").html(msg);
            } catch (e) {
            }
        }
    });



    youtubeidforlazyload = id;
    $.ajax({
        type: "POST",
        url: "../Feeds/YoutubeChannelVideos?profileid=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            //alert(msg);
            $("#feedimages").html(msg);

        }
    });

}












/******************************************instagram********************/
function likesupdate(imgid, access, userid, chk) {
    debugger;

    try {

        if (chk == 'like') {
            $.ajax({
                type: "POST",
                url: "../Feeds/AjaxFeeds.aspx?mediaid=" + imgid + "&op=instagramlike&access=" + access + "&userid=" + userid,
                data: "",
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (msg) {
                    debugger;
                    $("#instaAccounts").css('display', 'none');
                    $(".b-modal").css('display', 'none');
                }
            });
        } else if (chk == 'unlike') {
            $.ajax({
                type: "POST",
                url: "../Feeds/AjaxFeeds.aspx?mediaid=" + imgid + "&op=instagramunlike&access=" + access + "&userid=" + userid,
                data: "",
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (msg) {
                    debugger;
                    $("#instaAccounts").css('display', 'none');
                    $(".b-modal").css('display', 'none');
                }
            });

        }
    } catch (e) {
        //  //alert(e);
    }

}


function Instagramdetails(id) {


    loadfeedpartialpage = $.ajax({
        type: "POST",
        url: "../Feeds/LoadFeedPartialPage?network=instagram",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#page-wrapper").html(msg);
            } catch (e) {
            }
        },
        async:false
    });
    //instagramidforlazyload = id;
    $.ajax({
        type: "POST",
        url: "../Feeds/InstagramImages?profileid=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            $("#feedimages").html(msg);
            //$(document).on('scroll', instagramimages);
        }
    });

}


function instagramimages() {

    debugger;
    $(document).off('scroll', instagramimages);
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds.aspx?op=instagramimages&loadtime=nofirst&profileid=" + instagramidforlazyload,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            $("#instag").append(msg);
            $(document).on('scroll', instagramimages);
        }
    });
}



function fbimage(src) {
    debugger;

    if (src.indexOf('_s.png') != -1) {
        var newsrc = src.replace('_s.png', '_n.png');
        $("#popupimage").attr('src', newsrc);
    } else if (src.indexOf('_s.jpg') != -1) {
        var newsrcP = src.replace('_s.jpg', '_n.jpg');
        $("#popupimage").attr('src', newsrcP);
    } else {
        $("#popupimage").attr('src', src);
    }
    $("#facebookImagePopup").bPopup({

        positionStyle: 'fixed'

    });

}

function fbimageclose() {

    $("#popupimage").attr('src', "../Contents/img/43px_on_transparent.gif");

}


//var throttled = throttle(facebookwallscrolldata, 20000);


function refreshWallpostFacebook(id) {
    debugger;
    try {
        $("#refreshpanel1").attr('src', '../Themes/Socioboard/Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/wallposts?op=facebookwallposts&load=first&profileid=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {

            debugger;
            try {
                //facebookfeeds.load();
            } catch (e) {

            }
            try {
                $("#img_paneltab1").attr('src', "/Themes/Socioboard/Contents/img/admin/1.png");
            } catch (e) {
            }
            try {
                $("#refreshpanel1").attr('src', "../Themes/Socioboard/Contents/img/admin/9.png");
            } catch (e) {
            }
            try {
                $("#title_paneltab1").html("News Feeds");
            } catch (e) {
            }
            try {

                $("#data_paneltab1").html(msg);


                //$("#data_paneltab1").on('scroll', facebookwallscrolldata);
                //$("#data_paneltab1").scroll(throttled);
                //$("#data_paneltab1").scroll(facebookwallscrolldata);
            } catch (e) {
            }
            try {
                $("#data_paneltab1").mCustomScrollbar("update");
            } catch (e) {

            }

        }
    });
}

function refreshFeedsFacebook(id) {
    debugger;
    try {
        $("#refreshpanel2").attr('src', '../Themes/Socioboard/Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds?op=facebookfeeds&profileid=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {

            debugger;
            try {
                //facebookfeeds.load();
            } catch (e) {

            }
            try {
                $("#img_paneltab2").attr('src', "/Themes/Socioboard/Contents/img/admin/1.png");
            } catch (e) {
            }
            try {
                $("#refreshpanel2").attr('src', "../Themes/Socioboard/Contents/img/admin/9.png");
            } catch (e) { }
            try {
                $("#title_paneltab2").html("Wall posts");
            } catch (e) {
            }
            try {
                if (msg.indexOf('<div') > 0 && msg != null) {
                    $("#data_paneltab2").html(msg);
                }

                //$("#data_paneltab2").on('scroll', facebookwallscrolldata);

                //$("#data_paneltab2").on('scroll', facebookfeedscrolldata);
                //$("#data_paneltab2").on('scroll', facebookfeed);
            } catch (e) {
            }
            try {
                $("#data_paneltab2").mCustomScrollbar("update");
            } catch (e) {

            }

        }
    });
}

function refreshSchedularMessageFacebook(id) {
    debugger;
    try {
        $("#refreshpanel3").attr('src', '../Themes/Socioboard/Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/scheduler?network=facebook&profileid=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {

            debugger;
            try {
                //facebookfeeds.load();
            } catch (e) {

            }
            try {
                $("#img_paneltab3").attr('src', "/Themes/Socioboard/Contents/img/admin/1.png");
            } catch (e) {
            }
            try {
                $("#refreshpanel3").attr('src', "../Themes/Socioboard/Contents/img/admin/9.png");
            } catch (e) {
            }
            try {
                // $("#title_paneltab3").html("Scheduled Messages");
            } catch (e) {
            }
            try {

                $("#data_paneltab3").html(msg);


                $("#data_paneltab3").on('scroll', facebookwallscrolldata);

            } catch (e) {
            }
            try {
                $("#data_paneltab3").mCustomScrollbar("update");
            } catch (e) {

            }

        }
    });
}



function refreshWallpostTwitter(id) {
    debugger;
    var local = getlocatdatetime();
    try {
        $("#refreshpanel1").attr('src', '../Themes/Socioboard/Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/TwitterNetworkDetails?load=first&profileid=" + id,
        data: { "localtime": local },
        //contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#img_paneltab1").attr('src', "../Themes/Socioboard/Contents/img/admin/2.png");
            } catch (e) {
            }
            try {
                $("#refreshpanel1").attr('src', "../Themes/Socioboard/Contents/img/admin/9.png");
            } catch (e) {

            }
            try {
                $("#title_paneltab1").html("Feeds");
            } catch (e) {

            }
            try {
                $("#data_paneltab1").html(msg);
            } catch (e) {

            }
            try {
                $("#data_paneltab1").mCustomScrollbar("update");
            } catch (e) {

            }
        }
    });
}

function refreshFeedsTwitter(id) {
    debugger;
    var local = getlocatdatetime();
    try {
        $("#refreshpanel2").attr('src', '../Themes/Socioboard/Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/TwitterFeeds?load=first&profileid=" + id,
        data: { "localtime": local },
        //contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#img_paneltab2").attr('src', "../Themes/Socioboard/Contents/img/admin/2.png");
            } catch (e) {
            }
            try {
                $("#refreshpanel2").attr('src', "../Themes/Socioboard/Contents/img/admin/9.png");
            } catch (e) {

            }
            try {
                $("#title_paneltab2").html("Tweets");
            } catch (e) {

            }
            try {
                $("#data_paneltab2").html(msg);
            } catch (e) {

            }
            try {
                $("#data_paneltab2").mCustomScrollbar("update");
            } catch (e) {

            }
        }
    });
}

function refreshSchedularMessageTwitter(id) {
    debugger;
    try {
        $("#refreshpanel3").attr('src', '../Themes/Socioboard/Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/scheduler?network=twitter&profileid=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#img_paneltab3").attr('src', "../Themes/Socioboard/Contents/img/admin/2.png");
            } catch (e) {
            }
            try {
                $("#refreshpanel3").attr('src', "../Themes/Socioboard/Contents/img/admin/9.png");
            } catch (e) {

            }
            try {
                // $("#title_paneltab3").html("Scheduler");
            } catch (e) {

            }
            try {
                $("#data_paneltab3").html(msg);
            } catch (e) {

            }
            try {
                $("#data_paneltab3").mCustomScrollbar("update");
            } catch (e) {

            }
        }
    });
}




function refreshWallpostLinkedin(id) {
    debugger;
    var local = getlocatdatetime();
    try {
        $("#refreshpanel1").attr('src', '../Themes/Socioboard/Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/linkedinwallposts?load=first&profileid=" + id,
        data: { "localtime": local },
        //contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#img_paneltab1").attr('src', "/Themes/Socioboard/Contents/img/admin/5.png");
            } catch (e) {
            }
            try {
                $("#refreshpanel1").attr('src', "../Themes/Socioboard/Contents/img/admin/9.png");
            } catch (e) {

            }
            try {
                $("#title_paneltab1").html("Network Updates");
            } catch (e) {

            }
            try {
                $("#data_paneltab1").html(msg);
            } catch (e) {

            }


        }
    });
}

function refreshFeedsLinkedin(id) {
    debugger;
    var local = getlocatdatetime();
    try {
        $("#refreshpanel2").attr('src', '../Themes/Socioboard/Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/LinkedinFeeds?load=first&profileid=" + id,
        data: { "localtime": local },
        //contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#img_paneltab2").attr('src', "/Themes/Socioboard/Contents/img/admin/5.png");
            } catch (e) {
            }
            try {
                $("#refreshpanel2").attr('src', "../Themes/Socioboard/Contents/img/admin/9.png");
            } catch (e) {

            }
            try {
                $("#title_paneltab2").html("User Updates");
            } catch (e) {

            }
            try {
                $("#data_paneltab2").html(msg);
            } catch (e) {

            }


        }
    });
}

function refreshSchedularMessageLinkedin(id) {
    debugger;
    try {
        $("#refreshpanel3").attr('src', '../Themes/Socioboard/Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/scheduler?network=linkedin&profileid=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#img_paneltab3").attr('src', "/Themes/Socioboard/Contents/img/admin/5.png");
            } catch (e) {
            }
            try {
                $("#refreshpanel3").attr('src', "../Themes/Socioboard/Contents/img/admin/9.png");
            } catch (e) {

            }
            try {
                $("#title_paneltab3").html("Scheduled Messages");
            } catch (e) {

            }
            try {
                $("#data_paneltab3").html(msg);
            } catch (e) {

            }


        }
    });
}








function facebookLike(msgid, profileid, fbid) {
    debugger;
    try {

        var status = $("#likefb_" + fbid).attr('status');

        $.ajax({
            type: "POST",
            url: "../Feeds/FacebookLike?fbid=" + fbid + "&profileid=" + profileid + "&msgid=" + msgid,
            data: '',
            contentType: "application/json; charset=utf-8",
            success: function (e) {
                if (status == "likes") {
                    $("#likefb_" + fbid).attr('status', 'unlike');
                    $("#likefb_" + fbid).html('<i class="fa fa-thumbs-o-down"></i>');

                }
                else {
                    $("#likefb_" + fbid).attr('status', 'likes');
                    $("#likefb_" + fbid).html('<i class="fa fa-thumbs-o-up"></i>');
                }
                if (status == "unlike") {
                    alertify.success("Unliked Successfully");
                }
                else {
                    alertify.success("Liked Successfully");
                }
            }
        });





    } catch (e) {

    }
}


function commentText(Mid) {
    debugger;
    try {
        $("#textfb_" + Mid).removeClass('put_comments').addClass('put_comments_display');
        $("#okfb_" + Mid).removeClass('ok').addClass('ok_display');
        $("#cancelfb_" + Mid).removeClass('cancel').addClass('cancel_display');

    } catch (e) {

    }

}


function cancelFB(Mid) {
    debugger;
    try {
        $("#textfb_" + Mid).val('');
        $("#textfb_" + Mid).addClass('put_comments').removeClass('put_comments_display');
        $("#okfb_" + Mid).addClass('ok').removeClass('ok_display');
        $("#cancelfb_" + Mid).addClass('cancel').removeClass('cancel_display');
    } catch (e) {

    }

}

function commentFB(fbid, profileid) {

    var message = $("#textfb_" + fbid).val();
    if (message == "") {
        //alert("Please write something!")
        alertify.alert("Please write something to Post!");
        return false;
    }
    $.ajax({
        type: "POST",
        url: "../Feeds/FacebookComment?fbcomment&fbcommentid=" + fbid + "&profileid=" + profileid + "&message=" + message,
        data: '',
        contentType: "application/json; charset=utf-8",
        success: function (e) {
            debugger;
            $("#okfb_" + fbid).css('display', '');
            alertify.success("Commented Successfully");

            cancelFB(fbid);

        }
    });



}



function dropDownChange(sval, id, token) {
    var SFVal = $(sval).html();
    if (SFVal.indexOf("VIDEO") > -1) {
        debugger;
        youtubedetails(id, token);
    }
    else if (SFVal.indexOf("SUBSCRIBTIONS") > -1) {
        debugger;
        //youtubesubscribe
        YoutubePostReq("youtubesubscribe", id, token);
    }
    else if (SFVal.indexOf("ACTIVITES") > -1) {
        debugger;
        YoutubePostReq("youtubeactivity", id, token);
    }
    else {
        alert("Please Select any Featur.");
    }

    debugger;
}

function YoutubePostReq(option, profileid, token) {
    debugger;
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds.aspx?op=" + option + "&profileid=" + profileid + "&accesstoken=" + token,
        data: '',
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            debugger;
            //            $(".yt_details").empty();
            //            $(".yt_details").html(msg);
            $(".yt_details_container").empty();
            $(".yt_details_container").html(msg);
            //alertify.success("Commented Successfully");
        }
    });


}

function TwtFolloUser(event, id) {
    debugger;
    var cls = $(event).attr("class");
    var Userid = $(event).attr("userid");
    //var Userid = $(event).attr("followerid");
    var screenname = $(event).attr("screenname");
    var accesstoken = $(event).attr("token");

    if (cls.indexOf("btn_follow") > -1) {
        debugger;

        $.ajax({
            type: "POST",
            url: "../Helper/AjaxHelper.aspx?op=TwtFolloUser&id=\"" + id + "\"&Userid=" + Userid + "&screen_name=" + screenname + "&accesstoken=" + accesstoken,
            data: '',
            //contentType: "application/json; charset=utf-8",
            success: function (msg) {
                debugger;
                $(event).removeClass("btn_follow");
                $(event).addClass("btn_unfollow");
                $(event).html();
                $(event).html('Unfollow');
                alertify.success("Follow Successfully.");
            },
            error: function (err) {
                alert(err);
                debugger;
            }
        });
    }
    else if (cls.indexOf("btn_unfollow") > -1) {
        debugger;

        $.ajax({
            type: "POST",
            url: "../Helper/AjaxHelper.aspx?op=TwtUnfolloUser&id=\"" + id + "\"&Userid=" + Userid + "&screen_name=" + screenname + "&accesstoken=" + accesstoken,
            data: '',
            //contentType: "application/json; charset=utf-8",
            success: function (msg) {
                debugger;
                $(event).removeClass("btn_unfollow");
                $(event).addClass("btn_follow");
                $(event).html();
                $(event).html('Follow');
                alertify.success("Unfollow Successfully");
            },
            error: function (err) {
                alert(err);
                debugger;
            }
        });
    }
}


///-------------------------vikash------------------------////

function Show_twt_menu(i) {
    debugger;
    var id = "dropdownmenu_" + i;
    var menu = document.getElementById(id);

    if (menu.style.display == 'block') {
        menu.style.display = 'none';
    } else {
        menu.style.display = 'block';
    }
}

function RetweetPopup(ScreenName, ProfileId, MessageId) {
    debugger;
    var message = 'Retweet this message from @' + ScreenName + '?';
    $(".hidden_menu").hide();
    alertify.confirm(message, function (e) {
        if (e) {
            debugger;
            $.ajax({
                type: "POST",
                url: "../Feeds/retweetmessage?MessageId=" + MessageId + "&ProfileId=" + ProfileId,
                dataType: "html",
                success: function (msg) {
                    debugger;
                    if (msg == "succeess") {
                        alertify.success("Message has been RETWEETED");
                    }
                    else {
                        alertify.log("Message has been already RETWEETED");
                    }
                },
                error: function (err) {
                    alertify.error("Somthing went Wrong!");
                }
            });
        }
        else {
            debugger;
        }
    });
}

function FavoritePopup(ScreenName, ProfileId, MessageId) {
    debugger;
    $(".hidden_menu").hide();
    var message = 'Favorite this message from @' + ScreenName + '?';
    alertify.confirm(message, function (e) {
        if (e) {
            $.ajax({
                type: "POSt",
                url: "../Feeds/favoritemessage?MessageId=" + MessageId + "&ProfileId=" + ProfileId,
                dataType: "html",
                success: function (msg) {
                    if (msg == "succeess") {
                        alertify.success("Message has been FAVOTITED");
                    }
                    else {
                        alertify.log("Message has been already FAVOTITED");
                    }
                },
                error: function (err) {
                    alertify.error("Somthing went Wrong!");
                }
            });
        }
        else {
            debugger;
        }
    });
}

function SpamUserPopup(FromScreenName, ProfileId) {
    debugger;
    $(".hidden_menu").hide();
    var msg = "Are you sure you want to report @" + FromScreenName + " as a spammer?"
    alertify.confirm(msg, function (e) {
        if (e) {
            debugger;
            $.ajax({
                type: "POST",
                url: "../Feeds/spamuser?SpammerScreanName=" + FromScreenName + "&UserProfileId=" + ProfileId,
                dataType: "html",
                success: function (msg) {
                    var str = "@" + FromScreenName + " has been reported as SPAM";
                    alertify.success(str);
                },
                error: function (err) {
                    alertify.error("Somthing went Wrong!");
                }
            });
        }
        else { }
    });

}

function QuoteMessagePopup(ProfileId, Feed) {
    debugger;
    $(".hidden_menu").hide();
    var buttonhtm = "<button type=\"button\" class=\"btn btn-default\" onclick=\"SendQuoteCompose('" + ProfileId + "')\">Post</button>";
    buttonhtm += "<button data-dismiss=\"modal\" class=\"btn btn-default\" type=\"button\">Close</button>";
    $("#leaveQuotecompose").html(buttonhtm);
    var img = $("#user_avtar").attr('src');
    $("#quotemessageimg").attr('src', img);
    $("#Quote_text").val(Feed);
    var len = Feed.length;
    $('#compose_count').text(140 - len);
    //$("#QuoteCompose").modal('show');

}
function SendQuoteCompose(profileid) {
    debugger;
    var curdate = new Date();
    var curdaatetimetime = (curdate.getMonth() + 1) + "/" + curdate.getDate() + "/" + curdate.getFullYear() + " " + curdate.getHours() + ":" + curdate.getMinutes() + ":" + curdate.getSeconds();
    var user = profileid + "~twitter";

    var fd = new FormData();
    var filesimage = document.getElementById('uploadImage').files[0];

    var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
    if (filesimage != null) {
        if (hasExtension('uploadImage', fileExtension)) {
            fd.append('file', filesimage);
        }
        else {
            alert("File Extention is not current. Please upload any image file");
            return;
        }
    }

    var message = $("#Quote_text").val();

    if (message != '' || filesimage != null) {
        $.ajax({
            type: 'POST',
            url: '../Home/ComposeMessageSend?message=' + encodeURIComponent(message) + '&allprofiles=' + user + '&curdaatetimetime=' + curdaatetimetime,
            data: fd,
            processData: false,
            contentType: false,
            success: function (data) {
                $('#closequotepopup').click();
                alertify.success("Message Sent Successfully");
                $('#uploadImage').val('');
                $('#showRemove').css('display', 'none');
            }
        });
    }
    else {

    }

}


function countChar() {
    debugger;
    var val = document.getElementById('Quote_text').value;
    var len = val.length;
    if (len > 140) {
        document.getElementById('Quote_text').value = val.substring(0, 140);
    } else {
        $('#compose_count').text(140 - len);

    }
}

function Addimage() {
    debugger;
    var filesinput = $('#uploadImage');
    if (filesinput != undefined && filesinput[0].files[0] != null) {
        $('#showRemove').css('display', 'block');
    }
}

function ImageDelete() {

    try {
        var filesinput = $('#uploadImage');

        debugger;

        if (filesinput !== 'undefined') {
            $('#showRemove').css('display', 'none');
            document.getElementById('uploadImage').value = "";

        }
    } catch (e) {

    }
}

function MailPopUpTwt(feedid) {
    debugger;
    $(".hidden_menu").hide();
    $.ajax({
        type: 'POST',
        url: '../Feeds/ShowTwtMailPopUp?Id=' + feedid,
        dataType: "html",
        processData: false,
        contentType: false,
        success: function (data) {
            $('#twtmailpopup').html(data);
            //$("#twtmailpopup").modal('show');
        }
    });

}


function WordpressDetails(siteid, WPUserId) {
    debugger;
    $.ajax({
        type: "POST",
        url: "../Feeds/LoadFeedPartialPageNew?network=wordpress",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#page-wrapper").html(msg);
            } catch (e) {
            }
        }
    });


    $.ajax({
        type: "POST",
        url: "../Feeds/WordpressBlogPost?SiteId=" + siteid + "&WPUserId=" + WPUserId,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#blogfeeds").html(msg);
            } catch (e) {
            }
        }
    });


}

function loadblogpostpartial(SiteId) {
    $.ajax({
        type: "POST",
        url: "../Feeds/LoadBlogPostPartial?SiteId=" + SiteId,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#page-wrapper").html(msg);
            } catch (e) {
            }
        }
    });
}

function PostBlogOnWordpress(siteid, wpuserid) {
    debugger;
    var title = $('#text-input-tittle').val();
    var message = $('#text-publish-wordpress').val();
    var tags = "";
    $.ajax({
        type: "POST",
        url: "../Home/PostMessageInWordpress?siteid=" + siteid + "&WPUserId=" + wpuserid + "&Message=" + message + "&Title=" + title + "&Tags=" + tags,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            alert(msg);
        }
    });
}

//6 functions added
//modified by sumit gupta [13-02-2015]
function refreshWallpostFacebook_FeedsSearch(keyword) {
    debugger;
    var local = getlocatdatetime();
    var id = $('#refreshpanel2').attr('nwtid');
    try {

        //$("#refreshpanel1").attr('src', '../Themes/Socioboard/Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/wallposts_FeedsSearch?op=facebookwallposts&load=first&profileid=" + id + "&keyword=" + keyword,
        data: { "localtime": local },
        //contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {

            debugger;
            try {
                //facebookfeeds.load();
            } catch (e) {

            }
            try {
                $("#img_paneltab1").attr('src', "/Themes/Socioboard/Contents/img/admin/1.png");
            } catch (e) {
            }
            try {
                $("#refreshpanel1").attr('src', "../Themes/Socioboard/Contents/img/admin/9.png");
            } catch (e) {
            }
            try {
                $("#title_paneltab1").html("News Feeds");
            } catch (e) {
            }
            try {

                $("#data_paneltab1").html(msg);


                //$("#data_paneltab1").on('scroll', facebookwallscrolldata);
                //$("#data_paneltab1").scroll(throttled);
                $("#data_paneltab1").scroll(facebookwallscrolldata);
            } catch (e) {
            }
            try {
                $("#data_paneltab1").mCustomScrollbar("update");
            } catch (e) {

            }

        }
    });
}


function refreshFeedsFacebook_FeedsSearch(keyword) {
    debugger;
    var local = getlocatdatetime();
    var id = $('#refreshpanel2').attr('nwtid');
    try {
        //$("#refreshpanel2").attr('src', '../Themes/Socioboard/Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds_FeedsSearch?op=facebookfeeds&profileid=" + id + "&keyword=" + keyword,
        data: { "localtime": local },
        //contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {

            debugger;
            try {
                //facebookfeeds.load();
            } catch (e) {

            }
            try {
                $("#img_paneltab2").attr('src', "/Themes/Socioboard/Contents/img/admin/1.png");
            } catch (e) {
            }
            try {
                $("#refreshpanel2").attr('src', "../Themes/Socioboard/Contents/img/admin/9.png");
            } catch (e) { }
            try {
                $("#title_paneltab2").html("Wall posts");
            } catch (e) {
            }
            try {
                //if (msg.indexOf('<div') > 0 && msg != null) {
                $("#data_paneltab2").html(msg);
                //}

                //$("#data_paneltab2").on('scroll', facebookwallscrolldata);

                //$("#data_paneltab2").on('scroll', facebookfeedscrolldata);
                //$("#data_paneltab2").on('scroll', facebookfeed);
            } catch (e) {
            }
            try {
                $("#data_paneltab2").mCustomScrollbar("update");
            } catch (e) {

            }

        }
    });
}

function FacebookUserFeeds_FeedsSearch(keyword) {
    debugger;
    var local = getlocatdatetime();
    var id = $('#refreshpanel2').attr('nwtid');
    //alert("antima");
    $("#title_paneltab3").html("User Feeds");
    //var nwtid = $('#refreshpanel2').attr('nwtid');
    $.ajax({
        type: "GET",
        url: "/Feeds/FacebookUserFeeds_FeedsSearch",
        data: { "profileid": id, "keyword": keyword, "localtime": local },
        //contentType: "application/json; charset=utf-8",
        dataType: "html",
        beforeSend: function () {
            console.log("test");
        },
        success: function (msg) {
            console.log("I")

            $("#data_paneltab3").html(msg);
            console.log("testend");

        },
        error: function (e) {
            console.log("testerr");

            console.log(e);
            alert("Somthing went wrong");
        }
    });

}

function TwitterUserTweet_FeedsSearch(keyword) {
    try {
        debugger;
        var local = getlocatdatetime();
        var id = $('#refreshpanel2').attr('nwtid');
        $("#title_paneltab3").html("User Tweet");

        $.ajax({
            type: "POST",
            url: "/Feeds/TwitterUserTweet_FeedsSearch?profileid=" + id + "&keyword=" + keyword,
            data: { "localtime": local },
            //contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    debugger;
                    $("#data_paneltab3").html(msg);
                } catch (e) {

                }
            }
        });
    } catch (e) {

    }
}

function TwitterFeeds_FeedsSearch(keyword) {
    var local = getlocatdatetime();
    var id = $('#refreshpanel2').attr('nwtid');
    twitterfeeds = $.ajax({
        type: "POST",
        url: "../Feeds/TwitterFeeds_FeedsSearch?&load=first&profileid=" + id + "&keyword=" + keyword,
        data: { "localtime": local },
        //contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#loader_tabpanel2").bind("click", function () {
                    //alert(" refreshSchedularMessageTwitter");
                    //refreshSchedularMessageTwitter(id);
                });
            } catch (e) {

            }

            try {
                $("#data_paneltab2").html(msg);
            } catch (e) {

            }
            try {
                $("#data_paneltab2").mCustomScrollbar("update");
            } catch (e) {

            }
        }
    });
}

function TwitterNetworkDetails_FeedsSearch(keyword) {
    var local = getlocatdatetime();
    var id = $('#refreshpanel2').attr('nwtid');
    $.ajax({
        type: "POST",
        url: "../Feeds/TwitterNetworkDetails_FeedsSearch?&load=first&profileid=" + id + "&keyword=" + keyword,
        data: { "localtime": local },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#loader_tabpanel1").bind("click", function () {
                    //alert("refreshWallpostTwitter");
                    //refreshWallpostTwitter(id);
                });
            } catch (e) {
            }
            try {
                $("#data_paneltab1").html(msg);
            } catch (e) {

            }
            try {
                $("#data_paneltab1").mCustomScrollbar("update");
            } catch (e) {

            }
        }
    });
}

//6 functions added
//modified by sumit gupta [15-02-2015]
function FacebookLoadNewNewsFeeds() {
    debugger;
    var local = getlocatdatetime();
    var id = $('#refreshpanel2').attr('nwtid');
    try {

        //$("#refreshpanel1").attr('src', '../Themes/Socioboard/Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/AddLoadNewFacebookNewsFeeds?profileid=" + id,
        data: { "localtime": local },
        //contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {

            debugger;
            try {
                //facebookfeeds.load();
            } catch (e) {

            }
            //try {
            //    $("#img_paneltab2").attr('src', "/Themes/Socioboard/Contents/img/admin/1.png");
            //} catch (e) {
            //}
            //try {
            //    $("#refreshpanel2").attr('src', "../Themes/Socioboard/Contents/img/admin/9.png");
            //} catch (e) {
            //}
            //try {
            //    $("#title_paneltab2").html("Wall Posts");
            //} catch (e) {
            //}
            try {

                if (msg.indexOf("<div") != -1) {
                    //$("#data_paneltab1").html(msg);
                    var existingHtml = $("#data_paneltab2").html();
                    $("#data_paneltab2").html('');
                    $("#data_paneltab2").html(msg + existingHtml);


                }

                //does function of thread.sleep
                setTimeout(function () {
                    FacebookLoadNewNewsFeeds();
                }, 120000);

                // $("#data_paneltab2").scroll(facebookwallscrolldata);
            } catch (e) {
            }
            try {
                // $("#data_paneltab1").mCustomScrollbar("update");
            } catch (e) {

            }

        },
        error: function (failmsg) {
            //does function of thread.sleep
            setTimeout(function () {
                FacebookLoadNewNewsFeeds();
            }, 120000);
        }
    });
}

function FacebookLoadNewUserHome() {
    debugger;
    var local = getlocatdatetime();
    var id = $('#refreshpanel1').attr('nwtid');
    try {

        //$("#refreshpanel1").attr('src', '../Themes/Socioboard/Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/AddLoadNewFacebookWallPosts?profileid=" + id,
        data: { "localtime": local },
        //contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {

            debugger;
            try {
                //facebookfeeds.load();
            } catch (e) {

            }
            //try {
            //    $("#img_paneltab2").attr('src', "/Themes/Socioboard/Contents/img/admin/1.png");
            //} catch (e) {
            //}
            //try {
            //    $("#refreshpanel2").attr('src', "../Themes/Socioboard/Contents/img/admin/9.png");
            //} catch (e) {
            //}
            //try {
            //    $("#title_paneltab2").html("Wall Posts");
            //} catch (e) {
            //}
            try {

                if (msg.indexOf("img-circle profilePic") != -1) {
                    //$("#data_paneltab1").html(msg);
                    $('#data_paneltab1 img').remove('.clsImageLoader');
                    var existingHtml = $("#data_paneltab1").html();
                    $("#data_paneltab1").html('');
                    $("#data_paneltab1").html(msg + existingHtml);
                    $('#data_paneltab1').append('<img class="clsImageLoader" src="/Themes/Socioboard/Contents/img/loader.gif" />');

                }

                //does function of thread.sleep
                setTimeout(function () {
                    FacebookLoadNewUserHome();
                }, 60000);

                // $("#data_paneltab2").scroll(facebookwallscrolldata);
            } catch (e) {
            }
            try {
                // $("#data_paneltab1").mCustomScrollbar("update");
            } catch (e) {

            }

        },
        error: function (failmsg) {
            //does function of thread.sleep
            //does function of thread.sleep
            setTimeout(function () {
                FacebookLoadNewUserHome();
            }, 60000);
        }
    });
}

function FacebookLoadNewUserFeeds() {
    debugger;
    var local = getlocatdatetime();
    var id = $('#refreshpanel1').attr('nwtid');
    try {

        //$("#refreshpanel1").attr('src', '../Themes/Socioboard/Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        //url: "../Feeds/AddLoadNewFacebookWallPosts?profileid=" + id+"&type=userfeeds",
        url: "../Feeds/AddLoadNewFacebookWallPosts?profileid=" + id + "&type=",
        data: { "localtime": local },
        //contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {

            debugger;
            try {
                //facebookfeeds.load();
            } catch (e) {

            }
            //try {
            //    $("#img_paneltab2").attr('src', "/Themes/Socioboard/Contents/img/admin/1.png");
            //} catch (e) {
            //}
            //try {
            //    $("#refreshpanel2").attr('src', "../Themes/Socioboard/Contents/img/admin/9.png");
            //} catch (e) {
            //}
            //try {
            //    $("#title_paneltab2").html("Wall Posts");
            //} catch (e) {
            //}
            try {

                if (msg.indexOf("img-circle profilePic") != -1) {
                    //$("#data_paneltab1").html(msg);
                    var existingHtml = $("#data_paneltab3").html();
                    $("#data_paneltab3").html('');
                    $("#data_paneltab3").html(msg + existingHtml);


                }

                //does function of thread.sleep
                setTimeout(function () {
                    FacebookLoadNewUserFeeds();
                }, 60000);

                // $("#data_paneltab2").scroll(facebookwallscrolldata);
            } catch (e) {
            }
            try {
                // $("#data_paneltab1").mCustomScrollbar("update");
            } catch (e) {

            }

        },
        error: function (failmsg) {
            //does function of thread.sleep
            setTimeout(function () {
                FacebookLoadNewUserFeeds();
            }, 60000);
        }
    });
}

function gplusdetails(gplusrid) {
    debugger;
    $.ajax({
        type: "POST",
        url: "../Feeds/LoadFeedPartialPage?network=gplus",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#page-wrapper").html(msg);
            } catch (e) {
            }
        },
        async:false
    });


    $.ajax({
        type: "POST",
        url: "../Feeds/GooglePlusFeeds?Id=" + gplusrid,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                if (msg == "no_data") {
                    $("#glusfeed").html("<div><center><h3>No Messages Found.</h3></center></div>");
                }
                else {
                    $("#glusfeed").html(msg);
                }
            } catch (e) {
            }
        }
    });
}