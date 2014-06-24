
var profilescountingtosend = 0;
var singleprofileid = '';
var profilesofcomposeinsession = '';
var singleprofileIdforscheduler = '';

/*Show Facebook pages dialog to redirect 
on facebook to add facebook accounts or fan pages*/

function ShowFacebookDialog(modal) {
    debugger;
    $("#overlay").show();
    $("#fbConnect").fadeIn(300);

    if (modal) {
        $("#overlay").unbind("click");
    }
    else {
        $("#overlay").click(function (e) {
            HideFacebookDialog();
        });
    }
}


function removeFollowers(removeid, userid) {
    try {
        debugger;

        var confirms = confirm("Are you sure to unfollow the user");

        if (confirms) {
            debugger;
            $.ajax
        ({
            type: "POST",
            url: "../AjaxHome.aspx?op=removefollowers&removeid=" + removeid + "&userid=" + userid,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
             try {
                    if (msg.toString().contains("logout")) {
//                        alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  
                // alert(msg);
                //debugger;

            }
        });
        }
    } catch (e) {

    }

}



function RecentFollowersOnHome() {

    try {
        $.ajax
        ({
            type: "POST",
            url: "../AjaxHome.aspx?op=recentfollowers",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
             try {
                    if (msg.toString().contains("logout")) {
//                        alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  
                // alert(msg);
                //debugger;
                $("#recentfollowers").html(msg);
            }
        });
    } catch (e) {

    }
}


/*Hide Facebook pages dialog which redirect 
on facebook to add facebook accounts or fan pages*/
function HideFacebookDialog() {

    $("#overlay").hide();
    $("#fbConnect").fadeOut(300);
    $("#fbConnect").hide();
}



/*Bind the social profiles of all networks,
will display on home page*/
function BindSocialProfiles() {
    try {
        $.ajax
        ({
            type: "POST",
            url: "../AjaxHome.aspx?op=social_connectivity",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
             try {
                    if (msg.toString().contains("logout")) {
//                        alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  
                // alert(msg);
                //debugger;
                $("#manageprofiles").html(msg);
            }
        });
    } catch (e) {

    }
}


/*bind the divs which has the basic user profile in
home page according to Social Profiles*/
function BindMidSnaps(loadtype) {
    $("#hm_loader").css('display', 'block');
    $("#hm_loader").attr('src', '../Contents/img/43px_on_transparent1.gif');

    $.ajax
        ({
            type: "POST",
            url: "../AjaxHome.aspx?op=midsnaps&loadtype=" + loadtype,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
             try {
                    if (msg.toString().contains("logout")) {
//                        alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  

                debugger;
                $(document).on('scroll', onScroll);
                $("#midsnaps").append(msg);
                $("#hm_loader").attr("src", "").css('display', 'none');
            }
        });
}



/*this function will call when user want to scroll down to see all 
the user profiles div according to social profiles*/
function onScroll(event) {
    var closeToBottom = ($(window).scrollTop() + $(window).height() > $(document).height() - 100);
    if (closeToBottom) {
        $(document).off('scroll', onScroll);
        BindMidSnaps("scroll");
    }
}



var reset = function () {
    $("#toggleCSS").href = "../Contents/css/alertify.default.css";
    alertify.set({
        labels: {
            ok: "OK",
            cancel: "Cancel"
        },
        delay: 5000,
        buttonReverse: false,
      
    });
};



function SimpleMessageAlert(msg) {
    try {
        alertify.alert(msg);
    } catch (e) {

    }
}

/*This function will delete the profile from our database 
and remove profile from Network Profiles**/
function confirmDel(profileid, profile, userid) {
    try {
        debugger;
        reset();
        alertify.set({ buttonReverse: true });
        alertify.confirm("Are you Sure want to delete the account.And account data will be erased completely", function (e) {
            if (e) {
                debugger;
                try {
                    $('#' + profileid).hide();
                } catch (e) {

                }
                try {
                    $('#mid_' + profileid).hide();
                } catch (e) {

                }
                try {
                    $('#so_' + profileid).remove();
                } catch (e) {

                }
                $.ajax
        ({
            type: "POST",
            url: "../AjaxHome.aspx?op=accountdelete&profile=" + profile + "&profileid=" + profileid,
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
             try {
                    if (msg.toString().contains("logout")) {
//                        alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  
                debugger;
                alertify.success("Account Deleted Successfully");
                try {
                var s = $("#ContentPlaceHolder1_usedAccount").html();
                
                

                } catch (e) {

                }
                //                reset();
                //                $("#alertify-cover").addClass();
                //                $("#alertify").addClass();
                //                $("#alertify-logs").addClass();
            }
        });




            } else {
                debugger;
                //alertify.alert("clicked ok");
                // user clicked "cancel"
            }
        });

        // var confir = confirm("Are you Sure want to delete the account.And your data will be erased completely");



    } catch (e) {

    }
}



/*Binding All profiles for 
compose message to post on Social Networks */
function bindProfilesComposeMessage() {
    debugger;
    try {

       

       

            $.ajax
                 ({
            type: "GET",
            url: "../AjaxHome.aspx?op=MasterComposeLetsTalk",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
             try {
                    if (msg.toString().contains("logout")) {
//                        alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  
                debugger;
                $("#composeMaster").html(msg);
            }
        });
       

    } catch (e) {

    }
}


/*This array will contain the all selected profiles
to post message on Social Netowork*/
var chkidforusertest = new Array();


/*Function to Compose Message for single 
user to send the message on SocialNetwork*/
function composemessage(id, network) {
    debugger;
    try {
//        var userid = id.split('_');
//        var imageurl = document.getElementById('imgurl_' + userid[1]).innerHTML;
//        try {
//            document.getElementById('imageofuser').src = imageurl;
//        } catch (e) {

//        }

//        try {
//            document.getElementById('imageofuser_scheduler').src = imageurl;
//        } catch (e) {

//        }
//        if (network == 'fb') {
//            document.getElementById('socialIcon').src = "../Contents/img/facebook.png";
//            try {
//                document.getElementById('socialIcon_scheduler').src = "../Contents/img/facebook.png";
//            } catch (e) {

//            }
//            singleprofileid = 'fb_' + userid[1];
//            singleprofileIdforscheduler = 'fbscheduler_' + userid[1];
//        } else if (network == 'twt') {
//            document.getElementById('socialIcon').src = "../Contents/img/twitter.png";
//            try {
//                document.getElementById('socialIcon_scheduler').src = "../Contents/img/twitter.png";
//            } catch (e) {

//            }
//            singleprofileid = 'twt_' + userid[1];
//            singleprofileIdforscheduler = 'twtscheduler_' + userid[1];

//        } else if (network == 'lin') {
//            document.getElementById('socialIcon').src = "../Contents/img/link.png";
//            try {
//                document.getElementById('socialIcon_scheduler').src = "../Contents/img/link.png";
//            } catch (e) {

//            }
//            singleprofileid = 'lin_' + userid[1];
//            singleprofileIdforscheduler = 'linscheduler_' + userid[1];
//        }

//        try {
//            $("#loginBox_scheduler").hide();
//        } catch (e) {

//        }
             $('.open_profile').slideToggle();

          var sti = '';
          var name = $('#'+ id + ' > a > span').html();
          var ids = id.split('_');
          if(network == 'fb')
          {
           s =  '<span id=\'fb_'+ids[1]+'\' class="btn span_add" onclick="$(this).remove()">'+
                                '<img width="15" src="../Contents/img/facebook.png" alt="">'+
                                name +
                                '<span data-dismiss="alert" class="close pull-right">×</span></span>';
          }else if(network == 'twt')
          {
           s =  '<span id=\'twt_'+ids[1]+'\' class="btn span_add" onclick="$(this).remove()">'+
                                '<img width="15" src="../Contents/img/twitter.png" alt="">'+
                              name +
                                '<span data-dismiss="alert" class="close pull-right" >×</span></span>';
          }else if(network == 'lin')
          {

          if(ids.length > 2)
          {
           s =  '<span id=\'lin_'+ids[1]+'_' +ids[2] +'\'  class="btn span_add" onclick="$(this).remove()">'+
                                '<img width="15" src="../Contents/img/link.png" alt="">'+
                              name +
                                '<span data-dismiss="alert" class="close pull-right" >×</span></span>';
          }else
          {
            s =  '<span id=\'lin_'+ids[1]+'\'  class="btn span_add" onclick="$(this).remove()">'+
                                '<img width="15" src="../Contents/img/link.png" alt="">'+
                              name +
                                '<span data-dismiss="alert" class="close pull-right" >×</span></span>';
          }
          }

         $(".add_profile_content").append(s);
     

    } catch (e) {
        debugger;
    }
}





/*Post a single message for Multiple user of different social networks*/
function addAnotherProfileforMessage(id, network) {
    debugger;
    var divbindforscheduler = '';
    var divbind = '';
    var innerhtmlofMulti = $("#divformultiusers").html();
    var userid = id.split('_');
    //alert(userid.length);
    var username = document.getElementById('composename_' + userid[1]).innerHTML;
    var innerhtmlofmultischeduler = $("#divformultiusers_scheduler").html();



    try {
        if (innerhtmlofMulti.indexOf(username) == -1) {
            debugger;



            if (network == 'fb') {
                divbind = '<div style="height:21px;width:auto;min-width:22%" class="btn span12" id="fb_' + userid[1] + '"  onclick="delProfilesFromMultiusers(this.id)" ><img src="../Contents/img/facebook.png" alt="" width="15"/>' + username + '<span data-dismiss="alert" class="close pull-right">×</span></div>';

            } else if (network == 'twt') {
                divbind = '<div style="height:21px;width:auto;min-width:22%" class="btn span12" id="twt_' + userid[1] + '" onclick="delProfilesFromMultiusers(this.id)"><img src="../Contents/img/twitter.png" alt="" width="15"/>' + username + '<span data-dismiss="alert" class="close pull-right">×</span></div></div>';

            } else if (network == 'lin') {
                divbind = '<div style="height:21px;width:auto;min-width:22%" class="btn span12" id="lin_' + userid[1] + '" onclick="delProfilesFromMultiusers(this.id)"><img src="../Contents/img/link.png" alt="" width="15"/>' + username + '<span data-dismiss="alert" class="close pull-right">×</span></div></div>';

            }
        }
    } catch (e) {

    }



    try {
        if (innerhtmlofmultischeduler.indexOf(username) == -1) {
            if (network == 'fb') {
                divbindforscheduler = '<div style="height:31px;" class="btn span12" id="fbscheduler_' + userid[1] + '"  onclick="delProfilesFromMultiusers(this.id)" ><img src="../Contents/img/facebook.png" alt="" width="15"/>' + username + '<span data-dismiss="alert" class="close pull-right">×</span></div></div>';

            } else if (network == 'twt') {
                divbindforscheduler = '<div style="height:31px;" class="btn span12" id="twtscheduler_' + userid[1] + '" onclick="delProfilesFromMultiusers(this.id)"><img src="../Contents/img/twitter.png" alt="" width="15"/>' + username + '<span data-dismiss="alert" class="close pull-right">×</span></div></div>';

            } else if (network == 'lin') {
                divbindforscheduler = '<div style="height:31px;" class="btn span12" id="linscheduler_' + userid[1] + '" onclick="delProfilesFromMultiusers(this.id)"><img src="../Contents/img/link.png" alt="" width="15"/>' + username + '<span data-dismiss="alert" class="close pull-right">×</span></div>';
            }
        }
    } catch (e) {

    }
    try {
    //alert("divbind");
        $("#divformultiusers").append(divbind);
    } catch (e) {

    }
    try {
        $("#divformultiusers_scheduler").append(divbindforscheduler);
    } catch (e) {

    }
    try {
        $("#addBox").hide();
    } catch (e) {

    }
    try {
        $("#ab_scheduler").hide();
    } catch (e) {

    }

}


/*To clear the popup on close of Compose popup*/
function closeonCompose() {
    debugger;
    $("#adddates_scheduler").html('');
    chkidforusertest.length = 0;

    try {
        datearr.length = 0;
    } catch (e) {

    }

    debugger;
    try {
        $("#divformultiusers").html('');
    } catch (e) {

    }
    try {
        $("#divformultiusers_scheduler").html('');
    } catch (e) {

    }
    try {
        $("#textareavaluetosendmessage").val('');
    } catch (e) {

    }
    try {
        $("#textareavaluetosendmessage_scheduler").val('');
    } catch (e) {

    }
    try {
        $('#messageCount').html(140);
    } catch (e) {

    }
    try {
        $('#messageCount_scheduler').html(140 + ' Characters Remaining');
    } catch (e) {

    }
    chkidforusertest.length = 0;
    try {
        $("#sendMessageBtn").html('<img src="../Contents/img/sendbtn.png" alt="" />');
    } catch (e) {

    }
    try {
      //  $("#sendMessageBtn_scheduler").html('<img src="../Contents/img/sendbtn.png" alt="" />');
    } catch (e) {

    }
}


/*This function upload the file into folder to send message with file*/
function ajaxFileUpload() {
    //starting setting some animation when the ajax starts and completes
    //    $("#loading")
    //        .ajaxStart(function () {
    //            $(this).show();
    //        })
    //        .ajaxComplete(function () {
    //            $(this).hide();
    //        });

    /*
    prepareing ajax file upload
    url: the url of script file handling the uploaded files
    fileElementId: the file type of input element id and it will be the index of  $_FILES Array()
    dataType: it support json, xml
    secureuri:use secure protocol
    success: call back function when the ajax complete
    error: callback function when the ajax failed
           
    */
    try {
        //        $.ajaxFileUpload
        //        ({
        //            url: '../FileUpload.ashx',
        //            fileElementId: 'fileuploadImage',
        //            dataType: 'json',
        //            success: function (data, status) {
        //                if (typeof (data.error) != 'undefined') {
        //                    if (data.error != '') {
        //                        alert(data.error);
        //                    } else {
        //                        alert(data.msg);
        //                    }
        //                }
        //            },
        //            error: function (data, status, e) {
        //                debugger;
        //                alert(e);
        //            }
        //        });

        $('#fileuploadImage').fileupload({
            dataType: 'json',
            url: '../FileUpload.ashx',
            done: function (e, data) {
                data.context.text('Upload finished.');
            }
        });

    } catch (e) {
        debugger;
    }



}



/*This function  sends the composed 
message of  SocialNetwork*/
function SendMessage() {
    debugger;
//    
    try {
        
        var msg = $("#txtCompose").val();

        if(msg != '')
        {
        var chkidforusertest = new Array();


         var bindingofdata_scheduler = document.getElementById("profilesadd");
            var countdiv_scheduler = bindingofdata_scheduler.getElementsByTagName('span');

            if(countdiv_scheduler.length != 0)
            {
            for (var i = 0; i < countdiv_scheduler.length; i++) {
                chkidforusertest.push(countdiv_scheduler[i].id);
            }


         $.ajax
            ({
                type: 'POST',
                url: '../AjaxHome.aspx?op=sendmessage&message=' + msg + '&userid[]=' + chkidforusertest,
                data: '',
                processData: false,
                contentType: false,
                // contentType: "application/json; charset=utf-8",
                success: function (data) {
                 try {
                    if (msg.toString().contains("logout")) {
//                        alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  
                    debugger;
                    alertify.success("Message sent successfully");
                   $("#txtCompose").val('');
                   
                    closeonCompose();
                }
            });
            }else
            {
            alertify.alert("Please Select Profiles");
            }
        }else
        {
        alertify.alert('Please Enter Message');
        }
            }
    catch (e) {
    alert(e);
    }
}

function delProfilesFromMultiusers(id) {
    debugger;

    try {
        debugger;
        //$("#divformultiusers_scheduler").find("#" + id).remove();

        $("#" + id).remove();
    } catch (e) {
        debugger;
        alert(e);
    }
}

function CountMessages() {
    debugger;
    var gotajax = $.ajax({

        type: 'POST',
        url: '../AjaxHome.aspx?op=countmessages',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
         try {
                    if (msg.toString().contains("logout")) {
//                        alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  
            debugger;
            $("#incom_messages").html(data);
            $("#incomMessages").html(data);
        }

    });

}



function getFacebookUsersForHome()
{
try {
    $.ajax({

        type: 'POST',
        url: '../AjaxHome.aspx?op=getFbUsersForHome',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
         try {
                    if (msg.toString().contains("logout")) {
//                        alert("logout");
                        window.location = "../Default.aspx?hint=logout";
                        return false;
                    }
                }
                catch (e) {
                }  
            debugger;
          $("#fbUserMid").html(data);

        try {
           var id = $("#fbUserMid li:first").attr('id');
           var net = id.split('_');
           $("#userimg_fb").attr('src','http://graph.facebook.com/'+ net[1]+ '/picture?type=large');
           getUserDetailsforHome(net[1],net[0]);
     

           } 
           catch (e) {
            //alert(e);
            $("#recentmsgs_fb").html('');
            }
        }

    });
} 
catch (e) {
    
}

}

                    function getTwitterUsersForHome()
                    {
                    try {
                        $.ajax({

                            type: 'POST',
                            url: '../AjaxHome.aspx?op=getTwtUsersForHome',
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                             try {
                                  if (msg.toString().contains("logout")) {
                                  //                        alert("logout");
                                   window.location = "../Default.aspx?hint=logout";
                                   return false;
                                   }
                                 }
                                  catch (e) {
                                 }  
                                debugger;
                              $("#twtUserMid").html(data);

                            try {
                                var id = $("#twtUserMid li:first").attr('id');
                                var net = id.split('_');
                                //      $("#userimg_twt").attr('src','http://graph.facebook.com/'+ net[1]+ '/picture?type=large');
                                getUserDetailsforHome(net[1],net[0]);
     

                                } 
                                catch (e) {
                                ///    alert(e);
                                $("#recentmsgs_twt").html('');
                                }
                              }

                          });
                    } catch (e) {
    
                    }

                    }


                    
                    function getLinkedinUsersForHome()
                    {
                    try {
                        $.ajax({

                            type: 'POST',
                            url: '../AjaxHome.aspx?op=getLinUsersForHome',
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                             try {
                                   if (msg.toString().contains("logout")) {
                                    //                        alert("logout");
                                   window.location = "../Default.aspx?hint=logout";
                                   return false;
                                  }               
                                }
                                catch (e) {
                                    }  
                                debugger;
                              $("#linUserMid").html(data);

                            try {
                         var id = $("#linUserMid li:first").attr('id');
                         var net = id.split(',');
                      //   $("#userimg_lin").attr('src','http://graph.facebook.com/'+ net[1]+ '/picture?type=large');
                         getUserDetailsforHome(net[1],net[0]);
     

                    } catch (e) {
                       // alert(e);
                        $("#recentmsgs_lin").html('');
                    }
                            }

                        });
                    } catch (e) {
    
                    }

                    }






                    function getGplusUsersForHome()
                    {
                    try {
                        $.ajax({

                            type: 'POST',
                            url: '../AjaxHome.aspx?op=getGplusUsersForHome',
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                             try {
                                    if (msg.toString().contains("logout")) {
                                    //                        alert("logout");
                                    window.location = "../Default.aspx?hint=logout";
                                    return false;
                                   }   
                                 }
                                  catch (e) {
                                 }  
                                debugger;
                              $("#gplusUserMid").html(data);

                            try {
                                var id = $("#gplusUserMid li:first").attr('id');
                                var net = id.split('_');
                                //   $("#userimg_lin").attr('src','http://graph.facebook.com/'+ net[1]+ '/picture?type=large');
                                getUserDetailsforHome(net[1],net[0]);
     

                    } catch (e) {
                       // alert(e);
                        $("#recentmsgs_gp").html("");
                    }
                            }

                        });
                    } catch (e) {
    
                    }

                    }


            function getUserDetailsforHome(userid,network)
            {
            try {
              $.ajax({

                    type: 'POST',
                    url: '../AjaxHome.aspx?op=getUserDetails&userid='+ userid + '&network='+network,
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                     try {
                           if (msg.toString().contains("logout")) {
//                         alert("logout");
                           window.location = "../Default.aspx?hint=logout";
                           return false;
                           }
                         }
                           catch (e) {
                         }  
                        debugger;
                        if(network == 'fb')
                        {
                        if(data == '')
                        {
                        $("#recentmsgs_fb").html('No Facebook Profile is Added');
                        }else
                        {
                        $("#fbFeed").html('');
                       $("#fbFeed").html(data);
                       $("#fans_fb").html($("#fb_fans").html());
                       }
           }else if(network == 'twt')
           {
            $("#twtsection").show();
            $("#twtfeed").html('');
             $("#twtfeed").html(data);

             //call for scheduler


             $.ajax({
                    type: "POST",
                    url: "../Feeds/AjaxFeeds.aspx?op=scheduler&network=twitter&profileid=" + userid,
                    data: '',
                    contentType: "application/json; charset=utf-8",
                    dataType: "html",
                    success: function (msg) {
                    try {
                           if (msg.toString().contains("logout")) {
//                         alert("logout");
                           window.location = "../Default.aspx?hint=logout";
                           return false;
                           }
                         }
                           catch (e) {
                         }  
                       // alert('abhay');
                        //alert(msg);
                        $("#twtsection").show();
                        $("#twtschedule").html('');
                        $("#twtschedule").html(msg);
                    }
                });

             //end schedular
             //call for twt home
             $.ajax({
                            type: "POST",
                            url: "../Feeds/AjaxFeeds.aspx?op=twitternetworkdetails&profileid=" + userid,
                            data: '',
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (msg1) {
                            try {
                           if (msg.toString().contains("logout")) {
//                         alert("logout");
                           window.location = "../Default.aspx?hint=logout";
                           return false;
                           }
                         }
                           catch (e) {
                         }  
                                 $("#twthome").show();
                        $("#twthome").html('');
                                $("#twthome").html(msg1);
                            }
                        });

             //end call for home


             $("#fans_twt").html($("#twt_fans").html());


             $("#userimg_twt").attr('src',$("#twt_imgurl").html());
           }else if(network == 'lin')
           {
           $("#linsection").show();
             $("#linfeed").html(data);
             $("#fans_lin").html($("#lin_fans").html());

             //call for scheduler


             $.ajax({
                    type: "POST",
                    url: "../Feeds/AjaxFeeds.aspx?op=scheduler&network=linkedin&profileid=" + userid,
                    data: '',
                    contentType: "application/json; charset=utf-8",
                    dataType: "html",
                    success: function (msg) {
                    try {
                           if (msg.toString().contains("logout")) {
//                         alert("logout");
                           window.location = "../Default.aspx?hint=logout";
                           return false;
                           }
                         }
                           catch (e) {
                         }  
                       // alert('abhay');
                        //alert(msg);
                        $("#linsection").show();
                        $("#linscheduled").html('');
                        $("#linscheduled").html(msg);
                    }
                });

             //end schedular
             //call for twt home
             $.ajax({
                            type: "POST",
                            url: "../Feeds/AjaxFeeds.aspx?op=linkedinfeeds&profileid=" + userid,
                            data: '',
                            contentType: "application/json; charset=utf-8",
                            dataType: "html",
                            success: function (msg1) {
                            try {
                           if (msg.toString().contains("logout")) {
//                         alert("logout");
                           window.location = "../Default.aspx?hint=logout";
                           return false;
                           }
                         }
                           catch (e) {
                         }  
                                 $("#linsection").show();
                        $("#linfeed").html('');
                                $("#linfeed").html(msg1);
                            }
                        });

             //end call for home



             $("#userimg_lin").attr('src',$("#lin_imgurl").html());


           }else if(network == 'gplus')
           {
             $("#fbFeed").html(data);
             $("#fans_lin").html($("#lin_fans").html());


             $("#userimg_lin").attr('src',$("#lin_imgurl").html());
           }
                    }

                });
               
            }catch(e)
            {
            
            }

            }