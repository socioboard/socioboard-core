var selectprofiles = new Array();
var chktwt = new Array();

/*twitter*/
$("#selectallTwt").change(function () {
    if ($('#selectallTwt').is(':checked')) {


        var totalprofiels = document.getElementById('ContentPlaceHolder1_totalaccountscheck').innerHTML;


        try {

            for (var i = 0; i < totalprofiels; i++) {
                var vdffd = document.getElementById('twittercheck_' + i);
                if (vdffd != undefined) {
                    chktwt.push(vdffd);
                }
            }
            if (chktwt.length == 0) {
                alertify.alert("No profiles for Twitter");
                $('#selectallTwt').removeAttr('checked');
            } else {
                for (var j = 0; j < chktwt.length; j++) {

                    var cid = chktwt[j].id;
                    $("#" + cid).attr("checked", "checked");
                    selectprofiles.push(cid);
                }
            }

        } catch (e)
            { }

    } else {
            var totalprofielss = document.getElementById('ContentPlaceHolder1_totalaccountscheck').innerHTML;


        try {

            for (var i = 0; i < totalprofielss; i++) {
                var vdffdd = document.getElementById('twittercheck_' + i);
                if (vdffdd != undefined) {
                    chktwt.push(vdffdd);
                }
            }
            if (chktwt.length != 0) {
                for (var j = 0; j < chktwt.length; j++) {
                    var cid = chktwt[j].id;
                    $("#" + cid).removeAttr("checked");
                    selectprofiles.splice(cid, 1);
                }
            }

        } catch (e) {
        }

    }

});


/*facebook */
var chkfb = new Array();
$("#selectallFb").change(function () {
    debugger;



    if ($('#selectallFb').is(':checked')) {
        var totalprofiels = document.getElementById('ContentPlaceHolder1_totalaccountscheck').innerHTML;
        try {
            for (var i = 0; i < totalprofiels; i++) {
                var vdffd = document.getElementById('facebookcheck_' + i);
                if (vdffd != undefined) {
                    chkfb.push(vdffd);
                }
            }
            if (chkfb.length == 0) {
                alertify.alert("No profiles for Facebook");
                $('#selectallFb').removeAttr('checked');
            } else {
                for (var j = 0; j < chkfb.length; j++) {

                    var cid = chkfb[j].id;
                    $("#" + cid).attr("checked", "checked");
                    selectprofiles.push(cid);
                }
            }

        } catch (e)
            { }

    } else {
            var totalprofielss = document.getElementById('ContentPlaceHolder1_totalaccountscheck').innerHTML;

        try {

            for (var i = 0; i < totalprofielss; i++) {
                var vdffdd = document.getElementById('facebookcheck_' + i);
                if (vdffdd != undefined) {
                    chkfb.push(vdffdd);
                }
            }
            if (chkfb.length != 0) {
                for (var j = 0; j < chkfb.length; j++) {
                    var cid = chkfb[j].id;
                    $("#" + cid).removeAttr("checked");
                    selectprofiles.splice(cid, 1);
                }
            }

        } catch (e) {
        }

    }
});

/***//*********************************/
var chklin = new Array();
$("#selectallLd").change(function () {
    debugger;
    if ($('#selectallLd').is(':checked')) {
        var totalprofiels = document.getElementById('ContentPlaceHolder1_totalaccountscheck').innerHTML;
        try {
            for (var i = 0; i < totalprofiels; i++) {
                var vdffd = document.getElementById('linkedincheck_' + i);
                if (vdffd != undefined) {
                    chklin.push(vdffd);
                }
            }
            if (chklin.length == 0) {
               alertify.alert("No profiles for Linkedin");
                $('#selectallLd').removeAttr('checked');
            } else {
                for (var j = 0; j < chklin.length; j++) {

                    var cid = chklin[j].id;
                    $("#" + cid).attr("checked", "checked");
                    selectprofiles.push(cid);
                }
            }

        } catch (e)
            { }

    } else {
            var totalprofielss = document.getElementById('ContentPlaceHolder1_totalaccountscheck').innerHTML;
        try {

            for (var i = 0; i < totalprofielss; i++) {
                var vdffdd = document.getElementById('linkedincheck_' + i);
                if (vdffdd != undefined) {
                    chklin.push(vdffdd);
                }
            }
            if (chklin.length != 0) {
                for (var j = 0; j < chklin.length; j++) {
                    var cid = chklin[j].id;
                    $("#" + cid).removeAttr("checked");
                    selectprofiles.splice(cid, 1);
                }
            }

        } catch (e) {
        }

    }
});


/*************Instagram**********************************/
var chkins = new Array();
$("#selectallIns").change(function () {
    debugger;
    if ($('#selectallIns').is(':checked')) {
        var totalprofiels = document.getElementById('ContentPlaceHolder1_totalaccountscheck').innerHTML;
        try {

            for (var i = 0; i < totalprofiels; i++) {
                var vdffd = document.getElementById('instagramcheck_' + i);
                if (vdffd != undefined) {
                    chkins.push(vdffd);
                }
            }
            if (chkins.length == 0) {
             alertify.alert("No profiles for Instagram");
                $('#selectallIns').removeAttr('checked');
            } else {
                for (var j = 0; j < chkins.length; j++) {

                    var cid = chkins[j].id;
                    $("#" + cid).attr("checked", "checked");
                    selectprofiles.push(cid);
                }
            }

        } catch (e)
            { }

    } else {
            var totalprofielss = document.getElementById('ContentPlaceHolder1_totalaccountscheck').innerHTML;


        try {

            for (var i = 0; i < totalprofielss; i++) {
                var vdffdd = document.getElementById('instagramcheck_' + i);
                if (vdffdd != undefined) {
                    chkins.push(vdffdd);
                }
            }
            if (chkins.length != 0) {
                for (var j = 0; j < chkins.length; j++) {
                    var cid = chkins[j].id;
                    $("#" + cid).removeAttr("checked");
                    selectprofiles.splice(cid, 1);
                }
            }

        } catch (e) {
        }

    }
});



/*************Tumblr**********************************/
var chktumb = new Array();
$("#selectallTumblr").change(function () {
    debugger;
    if ($('#selectallTumblr').is(':checked')) {
        var totalprofiels = document.getElementById('ContentPlaceHolder1_totalaccountscheck').innerHTML;
        try {

            for (var i = 0; i < totalprofiels; i++) {
                var vdffd = document.getElementById('tumblrcheck_' + i);
                if (vdffd != undefined) {
                    chktumb.push(vdffd);
                }
            }
            if (chktumb.length == 0) {
                alertify.alert("No profiles for Tumblr");
                $('#selectallIns').removeAttr('checked');
            } else {
                for (var j = 0; j < chktumb.length; j++) {

                    var cid = chktumb[j].id;
                    $("#" + cid).attr("checked", "checked");
                    selectprofiles.push(cid);
                }
            }

        } catch (e)
            { }

    } else {
        var totalprofielss = document.getElementById('ContentPlaceHolder1_totalaccountscheck').innerHTML;


        try {

            for (var i = 0; i < totalprofielss; i++) {
                var vdffdd = document.getElementById('tumblrcheck_' + i);
                if (vdffdd != undefined) {
                    chktumb.push(vdffdd);
                }
            }
            if (chkins.length != 0) {
                for (var j = 0; j < chktumb.length; j++) {
                    var cid = chktumb[j].id;
                    $("#" + cid).removeAttr("checked");
                    selectprofiles.splice(cid, 1);
                }
            }

        } catch (e) {
        }

    }
});
