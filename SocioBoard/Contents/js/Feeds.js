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
                            $("#title_paneltab1").html("Wall Posts");
                        } catch (e) {
                        }

                        try {
                            $("#title_paneltab2").html("News Feeds");
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

function facebookdetails(id) {
    debugger;
    try {
        $("#img_paneltab1").attr('src', "../Contents/img/admin/1.png");
        $("#title_paneltab1").html("Wall Posts");
        $("#img_paneltab2").attr('src', "../Contents/img/admin/1.png");
        $("#title_paneltab2").html("News Feeds");
        $("#img_paneltab3").attr('src', "../Contents/img/admin/1.png");
        $("#title_paneltab4").html("Scheduled Messages");
        $("#instag").load("../Layouts/feedstab.htm");

        debugger;
        try {
            $('#data_paneltab1 a.feedspopup').lightBox();

            //            $('#data_paneltab1 a.feedspopup').on('', '');




        } catch (e) {
            debugger;
        }


    } catch (e) {

    }

    try {
        debugger;
        try {
            $("#loader_tabpanel1").attr('src', '../Contents/img/891.png');
        } catch (e) {

        }
        $("#data_paneltab1").html("");

        try {
            $("#loader_tabpanel2").attr('src', '../Contents/img/891.png');
        } catch (e) {

        }
        $("#data_paneltab2").html("");


        try {
            $("#loader_tabpanel3").attr('src', '../Contents/img/891.png');
        } catch (e) {

        }
        $("#data_paneltab3").html("");

        try {
            twitterfeeds.abort();
        } catch (e) {

        }
        try {
            twitterscheduler.abort();
        } catch (e) {

        }
        try {
            twittertweets.abort();
        } catch (e) {

        }

        try {
            linkedinfeeds.abort();
        } catch (e) {

        }
        try {
            linkedinscheduler.abort();
        } catch (e) {

        }
        try {
            linkedinwallposts.abort();
        } catch (e) {

        }



        try {
            facebookwallposts.abort();
        } catch (e) {

        }

        try {
            facebookfeeds.abort();
        } catch (e) {

        }
        try {
            facebookscheduler.abort();
        } catch (e) {

        }


        facebookwallposts = $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=facebookwallposts&load=first&profileid=" + id,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    $("#loader_tabpanel1").bind("click", function () {
                        // alert("refreshWallpostFacebook(id)");
                        $("#img_paneltab1").attr('src', "../Contents/img/admin/1.png");
                        $("#title_paneltab1").html("Wall Posts");
                        $("#img_paneltab2").attr('src', "../Contents/img/admin/1.png");
                        $("#title_paneltab2").html("News Feeds");
                        $("#img_paneltab3").attr('src', "../Contents/img/admin/1.png");
                        $("#title_paneltab4").html("Scheduled Messages");
                        refreshWallpostFacebook(id);

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
                //////////////////////////calling facebook Api for latestdata
                $.ajax({
                    type: "POST",
                    url: "../Feeds/AjaxFeeds.aspx?op=facebookapi&profileid=" + id,
                    data: '',
                    contentType: "application/json; charset=utf-8",
                    dataType: "html",
                    success: function (msg) {
                        debugger;


                        $.ajax({
                            type: "POST",
                            url: "../Feeds/AjaxFeeds.aspx?op=facebookwallposts&load=first&profileid=" + id,
                            data: '',
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (msg) {
                                debugger;
                                try {
                                    facebookfeeds.load();
                                } catch (e) {

                                }
                                try {
                                    $("#img_paneltab1").attr('src', "../Contents/img/admin/1.png");
                                } catch (e) {
                                }
                                try {
                                    $("#loader_tabpanel1").attr('src', "../Contents/img/admin/9.png");
                                } catch (e) {
                                }
                                try {
                                    $("#title_paneltab1").html("Wall Posts");
                                } catch (e) {
                                }
                                try {

                                    $("#data_paneltab1").html(msg);


                                    $("#data_paneltab1").on('scroll', facebookwallscrolldata);

                                } catch (e) {
                                }
                                try {
                                    $("#data_paneltab1").mCustomScrollbar("update");
                                } catch (e) {

                                }

                            }
                        });
                    }
                });

            }
        });

        //        try {
        //            var $container = $("#data_paneltab1");
        //            $("#data_paneltab1").lazyScrollLoading({
        //                onScrollToBottom: function (e, $lazyItems) {
        //                    debugger;
        //                    $.ajax({
        //                        type: "POST",
        //                        url: "../Feeds/AjaxFeeds.aspx?op=facebookwallposts&load=scroll",
        //                        data: '',
        //                        contentType: "application/json; charset=utf-8",
        //                        dataType: "html",
        //                        success: function (facemsg) {
        //                            debugger;
        //                            $("#data_paneltab1").append(facemsg);
        //                        }
        //                    });
        //                }
        //            });

        //        } catch (e) {

        //        }




        facebookfeeds = $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=facebookfeeds&profileid=" + id,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    $("#loader_tabpanel2").bind("click", function () {
                        // alert("  refreshFeedsFacebook");
                        refreshFeedsFacebook(id);

                    });
                    $("#img_paneltab1").attr('src', "../Contents/img/admin/1.png");
                } catch (e) {
                }
                try {
                    $("#img_paneltab2").attr('src', "../Contents/img/admin/1.png");
                } catch (e) {
                }
                try {
                    $("#loader_tabpanel2").attr('src', "../Contents/img/admin/9.png");
                } catch (e) {

                }
                try {
                    $("#title_paneltab2").html("News Feeds");
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









        facebookscheduler = $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=scheduler&network=facebook&profileid=" + id,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {

                try {
                    $("#loader_tabpanel3").bind("click", function () {
                        //alert("refreshSchedularMessageFacebook");
                        refreshSchedularMessageFacebook(id);

                    });
                    $("#img_paneltab1").attr('src', "../Contents/img/admin/1.png");
                } catch (e) {
                }

                try {
                    $("#img_paneltab3").attr('src', "../Contents/img/admin/1.png");
                } catch (e) {

                }


                try {
                    $("#loader_tabpanel3").attr('src', "../Contents/img/admin/9.png");
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
                try {
                    $("#data_paneltab3").mCustomScrollbar("update");
                } catch (e) {

                }

            }
        });

    } catch (e) {

    }
}


function facebookwallscrolldata() {
    debugger;
    try {
        $("#data_paneltab1").off('scroll', facebookwallscrolldata);

        var $container = $("#data_paneltab1");
        //                    $("#data_paneltab1").lazyScrollLoading({
        //                        onScrollToBottom: function (e, $lazyItems) {
        //                            debugger;
        $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=facebookwallposts&load=scroll",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (facemsg) {
                debugger;
                $("#data_paneltab1").on('scroll', facebookwallscrolldata);
                $("#data_paneltab1").append(facemsg);
            }
        });
        //                        }
        //                    });

    } catch (e) {

    }


}


/*************Twitter****************/


function twitterdetails(id) {
    try {
        debugger;
        $("#instag").load("../Layouts/feedstab.htm");
    } catch (e) {

    }
    try {

        try {
            $("#loader_tabpanel1").attr('src', '../Contents/img/891.png');
        } catch (e) {

        }
        $("#data_paneltab1").html("");
        try {
            $("#loader_tabpanel3").attr('src', '../Contents/img/891.png');
        } catch (e) {

        }
        $("#data_paneltab3").html("");

        try {
            $("#loader_tabpanel2").attr('src', '../Contents/img/891.png');
        } catch (e) {

        }
        $("#data_paneltab2").html("");


        try {
            twitterfeeds.abort();
        } catch (e) {

        }
        try {
            twitterscheduler.abort();
        } catch (e) {

        }
        try {
            twittertweets.abort();
        } catch (e) {

        }


        try {
            linkedinfeeds.abort();
        } catch (e) {

        }
        try {
            linkedinscheduler.abort();
        } catch (e) {

        }
        try {
            linkedinwallposts.abort();
        } catch (e) {

        }



        try {
            facebookwallposts.abort();
        } catch (e) {

        }

        try {
            facebookfeeds.abort();
        } catch (e) {

        }
        try {
            facebookscheduler.abort();
        } catch (e) {

        }

        twittertweets = $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=twitternetworkdetails&profileid=" + id,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    $("#img_paneltab1").attr('src', "../Contents/img/admin/2.png");
                } catch (e) {

                }


                try {
                    $("#loader_tabpanel1").attr('src', "../Contents/img/admin/9.png");
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

                $.ajax({
                    type: "POST",
                    url: "../Feeds/AjaxFeeds.aspx?op=twitterapi&profileid=" + id,
                    data: '',
                    contentType: "application/json; charset=utf-8",
                    dataType: "html",
                    success: function (msg) {
                        $.ajax({
                            type: "POST",
                            url: "../Feeds/AjaxFeeds.aspx?op=twitternetworkdetails&profileid=" + id,
                            data: '',
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (msg) {
                                try {
                                    $("#loader_tabpanel1").bind("click", function () {
                                        //alert("refreshWallpostTwitter");
                                        refreshWallpostTwitter(id);

                                    });
                                    $("#img_paneltab1").attr('src', "../Contents/img/admin/2.png");
                                } catch (e) {

                                }


                                try {
                                    $("#img_paneltab1").attr('src', "../Contents/img/admin/2.png");
                                } catch (e) {



                                }


                                try {
                                    $("#loader_tabpanel1").attr('src', "../Contents/img/admin/9.png");
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
                });

            }
        });



        twitterfeeds = $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=scheduler&network=twitter&profileid=" + id,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    $("#loader_tabpanel3").bind("click", function () {
                        //alert(" refreshSchedularMessageTwitter");
                        refreshSchedularMessageTwitter(id);

                    });
                    $("#img_paneltab3").attr('src', "../Contents/img/admin/2.png");
                } catch (e) {

                }
                try {
                    $("#img_paneltab3").attr('src', "../Contents/img/admin/2.png");
                } catch (e) {
                }
                try {
                    $("#loader_tabpanel3").attr('src', "../Contents/img/admin/9.png");
                } catch (e) {

                }
                try {
                    $("#title_paneltab3").html("Scheduler");
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




        twitterscheduler = $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=twitterfeeds&profileid=" + id,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {

                try {
                    $("#loader_tabpanel2").bind("click", function () {
                        //alert("refreshFeedsTwitter");
                        refreshFeedsTwitter(id);

                    });
                    $("#img_paneltab2").attr('src', "../Contents/img/admin/2.png");
                } catch (e) {

                }
                try {
                    $("#img_paneltab2").attr('src', "../Contents/img/admin/2.png");
                } catch (e) {
                }
                try {
                    $("#loader_tabpanel2").attr('src', "../Contents/img/admin/9.png");
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



    } catch (e) {

    }

}




function linkedindetails(id) {
    debugger;
    try {
        $("#instag").load("../Layouts/feedstab.htm");

    } catch (e) {

    }

    try {
        debugger;
        try {
            $("#loader_tabpanel1").attr('src', '../Contents/img/891.png');
        } catch (e) {

        }
        $("#data_paneltab1").html("");

        try {
            $("#loader_tabpanel2").attr('src', '../Contents/img/891.png');
        } catch (e) {

        }
        $("#data_paneltab2").html("");


        try {
            $("#loader_tabpanel3").attr('src', '../Contents/img/891.png');
        } catch (e) {

        }
        $("#data_paneltab3").html("");

        try {
            linkedinscheduler.abort();
        } catch (e) {

        }
        try {
            linkedinwallposts.abort();
        } catch (e) {

        }
        try {
            linkedinscheduler.abort();
        } catch (e) {

        }
        try {
            twitterfeeds.abort();
        } catch (e) {

        }
        try {
            twitterscheduler.abort();
        } catch (e) {

        }
        try {
            twittertweets.abort();
        } catch (e) {

        }
        try {
            facebookwallposts.abort();
        } catch (e) {

        }

        try {
            facebookfeeds.abort();
        } catch (e) {

        }
        try {
            facebookscheduler.abort();
        } catch (e) {

        }

        linkedinwallposts = $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=linkedinwallposts&profileid=" + id,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                try {
                    $("#loader_tabpanel1").bind("click", function () {
                        // alert("refreshWallpostLinkedin");
                        refreshWallpostLinkedin(id);

                    });
                    $("#img_paneltab1").attr('src', "../Contents/img/admin/5.png");
                } catch (e) {
                }


                try {
                    $("#img_paneltab1").attr('src', "../Contents/img/admin/5.png");
                } catch (e) {
                }


                try {
                    $("#loader_tabpanel1").attr('src', "../Contents/img/admin/9.png");
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

        linkedinfeeds = $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=linkedinfeeds&profileid=" + id,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {

                try {
                    $("#loader_tabpanel2").bind("click", function () {
                        //alert("refreshFeedsLinkedin");
                        refreshFeedsLinkedin(id);

                    });
                    $("#img_paneltab1").attr('src', "../Contents/img/admin/5.png");
                } catch (e) {
                }


                try {
                    $("#img_paneltab2").attr('src', "../Contents/img/admin/5.png");
                } catch (e) {

                }


                try {
                    $("#loader_tabpanel2").attr('src', "../Contents/img/admin/9.png");
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





        linkedinscheduler = $.ajax({
            type: "POST",
            url: "../Feeds/AjaxFeeds.aspx?op=scheduler&network=linkedin&profileid=" + id,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {


                try {
                    $("#loader_tabpanel3").bind("click", function () {
                        // alert("refreshSchedularMessageLinkedin");
                        refreshSchedularMessageLinkedin(id);

                    });
                    $("#img_paneltab1").attr('src', "../Contents/img/admin/5.png");
                } catch (e) {
                }



                try {
                    $("#img_paneltab3").attr('src', "../Contents/img/admin/5.png");
                } catch (e) {
                }
                try {
                    $("#loader_tabpanel3").attr('src', "../Contents/img/admin/9.png");
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

    } catch (e) {

    }
}


//***************tumblr data**********


function tumblrdetails(id) {
    debugger;
    try {

    } catch (e) {

    }

    try {
        $("#instag").html("");
    } catch (e) {

    }
    try {
        linkedinscheduler.abort();
    } catch (e) {

    }
    try {
        linkedinwallposts.abort();
    } catch (e) {

    }
    try {
        linkedinscheduler.abort();
    } catch (e) {

    }
    try {
        twitterfeeds.abort();
    } catch (e) {

    }
    try {
        twitterscheduler.abort();
    } catch (e) {

    }
    try {
        twittertweets.abort();
    } catch (e) {

    }
    try {
        facebookwallposts.abort();
    } catch (e) {

    }

    try {
        facebookfeeds.abort();
    } catch (e) {

    }
    try {
        facebookscheduler.abort();
    } catch (e) {

    }

    try {
        instagramimagesforfeeds.abort();
    } catch (e) {

    }

    try {
        tumblrimagesforfeeds.abort();
    } catch (e) {

    }


    tumblridforlazyload = id;

    tumblrProfileid = id;


    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds.aspx?op=tumblrimages&profileid=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            $("#instag").html(msg);


            $(document).on('scroll', tumblrimages);


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


function youtubedetails(id, token) {
    debugger;
    try {

    } catch (e) {

    }

    try {
        $("#instag").html("");
    } catch (e) {

    }
    try {
        linkedinscheduler.abort();
    } catch (e) {

    }
    try {
        linkedinwallposts.abort();
    } catch (e) {

    }
    try {
        linkedinscheduler.abort();
    } catch (e) {

    }
    try {
        twitterfeeds.abort();
    } catch (e) {

    }
    try {
        twitterscheduler.abort();
    } catch (e) {

    }
    try {
        twittertweets.abort();
    } catch (e) {

    }
    try {
        facebookwallposts.abort();
    } catch (e) {

    }

    try {
        facebookfeeds.abort();
    } catch (e) {

    }
    try {
        facebookscheduler.abort();
    } catch (e) {

    }

    try {
        instagramimagesforfeeds.abort();
    } catch (e) {

    }

    try {
        tumblrimagesforfeeds.abort();
    } catch (e) {

    }

    try {
        youtubechannelforfeeds.abort();
    } catch (e) {

    }

    youtubeidforlazyload = id;
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds.aspx?op=youtubechannel&profileid=" + id + "&accesstoken=" + token,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            //alert(msg);
            $("#instag").html(msg);

            //            $.ajax({
            //                type: "POST",
            //                url: "../Feeds/AjaxFeeds.aspx?op=instagramApi&profileid=" + id,
            //                data: '',
            //                contentType: "application/json; charset=utf-8",
            //                dataType: "html",
            //                success: function (data) {
            //                }
            //            });
            // $(document).on('scroll',youtubeimages);


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
    debugger;
    try {

    } catch (e) {

    }

    try {
        $("#instag").html("");
    } catch (e) {

    }
    try {
        linkedinscheduler.abort();
    } catch (e) {

    }
    try {
        linkedinwallposts.abort();
    } catch (e) {

    }
    try {
        linkedinscheduler.abort();
    } catch (e) {

    }
    try {
        twitterfeeds.abort();
    } catch (e) {

    }
    try {
        twitterscheduler.abort();
    } catch (e) {

    }
    try {
        twittertweets.abort();
    } catch (e) {

    }
    try {
        facebookwallposts.abort();
    } catch (e) {

    }

    try {
        facebookfeeds.abort();
    } catch (e) {

    }
    try {
        facebookscheduler.abort();
    } catch (e) {

    }

    try {
        instagramimagesforfeeds.abort();
    } catch (e) {

    }
    instagramidforlazyload = id;
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds.aspx?op=instagramimages&loadtime=first&profileid=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            $("#instag").html(msg);

            $.ajax({
                type: "POST",
                url: "../Feeds/AjaxFeeds.aspx?op=instagramApi&profileid=" + id,
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (data) {
                }
            });
            $(document).on('scroll', instagramimages);


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


function refreshWallpostFacebook(id) {
    debugger;
    try {
        $("#loader_tabpanel1").attr('src', '../Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds.aspx?op=facebookwallposts&load=first&profileid=" + id,
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
                $("#img_paneltab1").attr('src', "../Contents/img/admin/1.png");
            } catch (e) {
            }
            try {
                $("#loader_tabpanel1").attr('src', "../Contents/img/admin/9.png");
            } catch (e) {
            }
            try {
                $("#title_paneltab1").html("Wall Posts");
            } catch (e) {
            }
            try {

                $("#data_paneltab1").html(msg);


                $("#data_paneltab1").on('scroll', facebookwallscrolldata);

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
        $("#loader_tabpanel2").attr('src', '../Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds.aspx?op=facebookfeeds&profileid=" + id,
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
                $("#img_paneltab2").attr('src', "../Contents/img/admin/1.png");
            } catch (e) {
            }
            try {
                $("#loader_tabpanel2").attr('src', "../Contents/img/admin/9.png");
            } catch (e) {
            }
            try {
                $("#title_paneltab2").html("News Feeds");
            } catch (e) {
            }
            try {

                $("#data_paneltab2").html(msg);


                $("#data_paneltab2").on('scroll', facebookwallscrolldata);

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
        $("#loader_tabpanel3").attr('src', '../Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds.aspx?op=scheduler&network=facebook&profileid=" + id,
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
                $("#img_paneltab3").attr('src', "../Contents/img/admin/1.png");
            } catch (e) {
            }
            try {
                $("#loader_tabpanel3").attr('src', "../Contents/img/admin/9.png");
            } catch (e) {
            }
            try {
                $("#title_paneltab3").html("Scheduled Messages");
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
    try {
        $("#loader_tabpanel1").attr('src', '../Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds.aspx?op=twitternetworkdetails&profileid=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#img_paneltab1").attr('src', "../Contents/img/admin/2.png");
            } catch (e) {
            }
            try {
                $("#loader_tabpanel1").attr('src', "../Contents/img/admin/9.png");
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
    try {
        $("#loader_tabpanel2").attr('src', '../Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds.aspx?op=twitterfeeds&profileid=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#img_paneltab2").attr('src', "../Contents/img/admin/2.png");
            } catch (e) {
            }
            try {
                $("#loader_tabpanel2").attr('src', "../Contents/img/admin/9.png");
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
        $("#loader_tabpanel3").attr('src', '../Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds.aspx?op=scheduler&network=twitter&profileid=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#img_paneltab3").attr('src', "../Contents/img/admin/2.png");
            } catch (e) {
            }
            try {
                $("#loader_tabpanel3").attr('src', "../Contents/img/admin/9.png");
            } catch (e) {

            }
            try {
                $("#title_paneltab3").html("Scheduler");
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
    try {
        $("#loader_tabpanel1").attr('src', '../Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds.aspx?op=linkedinwallposts&profileid=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#img_paneltab1").attr('src', "../Contents/img/admin/5.png");
            } catch (e) {
            }
            try {
                $("#loader_tabpanel1").attr('src', "../Contents/img/admin/9.png");
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
    try {
        $("#loader_tabpanel2").attr('src', '../Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds.aspx?op=linkedinfeeds&profileid=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#img_paneltab2").attr('src', "../Contents/img/admin/5.png");
            } catch (e) {
            }
            try {
                $("#loader_tabpanel2").attr('src', "../Contents/img/admin/9.png");
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
        $("#loader_tabpanel3").attr('src', '../Contents/img/891.png');
    } catch (e) {

    }
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds.aspx?op=scheduler&network=linkedin&profileid=" + id,
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (msg) {
            try {
                $("#img_paneltab3").attr('src', "../Contents/img/admin/5.png");
            } catch (e) {
            }
            try {
                $("#loader_tabpanel3").attr('src', "../Contents/img/admin/9.png");
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







function facebookLike(id, profileid, fbid) {
    debugger;

    try {

        var lik = $("#likefb_" + fbid).html();

        if (lik == "Like") {
            $("#likefb_" + fbid).html("Unlike");

            $.ajax({
                type: "POST",
                url: "../Feeds/AjaxFeeds.aspx?op=fblike&fbid=" + fbid + "&profileid=" + profileid,
                data: '',
                contentType: "application/json; charset=utf-8",
                success: function (e) {
                    debugger;
                    alertify.success("Liked Successfully");
                }
            });


        }
        else if (lik == "Unlike") {
            $("#likefb_" + fbid).html("Like");
        }


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
    debugger;
    $("#okfb_" + fbid).hide();

    var message = $("#textfb_" + fbid).val();
    if (message == "") {
        //alert("Please write something!")
        alertify.alert("Please write something to Post!");
        return false;
    }
    $.ajax({
        type: "POST",
        url: "../Feeds/AjaxFeeds.aspx?op=fbcomment&fbid=" + fbid + "&profileid=" + profileid + "&message=" + message,
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