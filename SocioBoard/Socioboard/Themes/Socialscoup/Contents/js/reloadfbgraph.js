function reloadfbGraph(fbData) {

    debugger;

    var FanCnt = fbData[0].split(",");
    var UnfanCnt = fbData[1].split(",");
    var TimePeriod = fbData[2].split(",");

    debugger;




    ////////Like & Unlike/////////////////////

    debugger;
    var itemstwt = new Array(FanCnt.length);
    var eng = 0, inf = 0;
    itemstwt[0] = new Array(3);
    itemstwt[0][0] = "Days";
    itemstwt[0][1] = "Like";
    itemstwt[0][2] = "Unlike";

    for (var i = 1; i < FanCnt.length; i++) {
        itemstwt[i] = new Array(3);
        if (FanCnt[i] != "") {
            // itemstwt[i][0] = i;
            itemstwt[i][0] = TimePeriod[i];
            itemstwt[i][1] = Number(FanCnt[i]);
            itemstwt[i][2] = Number(UnfanCnt[i]);
            eng = eng + Number(FanCnt[i]);
            inf = inf + Number(UnfanCnt[i]);
            // retwt = Number(retwt) + Number(reTwt[i])
        }
    }
    var fancnt = Math.round((eng / FanCnt.length) * 100);
    var unfancnt = Math.round((inf / UnfanCnt.length) * 100);
    $("#spanEng").html("LIKE: " + fancnt + " %");
    $("#spanInf").html("UNLIKE: " + unfancnt + " %");

    var data = google.visualization.arrayToDataTable(itemstwt);

    var options = {
        series: [{ color: '#7ac143', visibleInLegend: true }, { color: '#1d67a7', visibleInLegend: true}]
    };

    var chart = new google.visualization.LineChart(document.getElementById('social_graph1'));
    chart.draw(data, options);
}








function reloadimpresGraph(fbData) {

    debugger;
    var impresn = fbData[1].split(",");
    var date = fbData[0].split(",");

    debugger;
    debugger;
    var itemstwt = new Array(impresn.length);
    var eng = 0;
    itemstwt[0] = new Array(2);
    itemstwt[0][0] = "Days";
    itemstwt[0][1] = "Page Impression";
    //itemstwt[0][2] = "Unlike";

    for (var i = 1; i < impresn.length; i++) {
        itemstwt[i] = new Array(2);
        if (impresn[i] != "") {
            // itemstwt[i][0] = i;
            itemstwt[i][0] = date[i];
            itemstwt[i][1] = Number(impresn[i]);
            //itemstwt[i][2] = Number(UnfanCnt[i]);
            eng = eng + Number(impresn[i]);
            //inf = inf + Number(UnfanCnt[i]);
            // retwt = Number(retwt) + Number(reTwt[i])
        }
    }
    var fancnt = Math.round((eng / impresn.length) * 100);
    //var unfancnt = Math.round((inf / UnfanCnt.length) * 100);
    $("#spanEng").html("Page Impression: " + fancnt + " %");
    //$("#spanInf").html("UNLIKE: " + unfancnt + " %");

    var data = google.visualization.arrayToDataTable(itemstwt);

    var options = {
        series: [{ color: '#7ac143', visibleInLegend: true }, { color: '#1d67a7', visibleInLegend: true}]
    };

    var chart = new google.visualization.LineChart(document.getElementById('impres_graph'));
    chart.draw(data, options);
}



function reloadStoriesGraph(fbData) {

    debugger;
    var impresn = fbData[1].split(",");
    var date = fbData[0].split(",");

    debugger;
    debugger;
    var itemstwt = new Array(impresn.length);
    var eng = 0;
    itemstwt[0] = new Array(2);
    itemstwt[0][0] = "Days";
    itemstwt[0][1] = "Stories";
    //itemstwt[0][2] = "Unlike";

    for (var i = 1; i < impresn.length; i++) {
        itemstwt[i] = new Array(2);
        if (impresn[i] != "") {
            // itemstwt[i][0] = i;
            itemstwt[i][0] = date[i];
            itemstwt[i][1] = Number(impresn[i]);
            //itemstwt[i][2] = Number(UnfanCnt[i]);
            eng = eng + Number(impresn[i]);
            //inf = inf + Number(UnfanCnt[i]);
            // retwt = Number(retwt) + Number(reTwt[i])
        }
    }
    var fancnt = Math.round((eng / impresn.length) * 100);
    //var unfancnt = Math.round((inf / UnfanCnt.length) * 100);
    $("#spanEng").html("Stories Counted: " + fancnt + " %");
    //$("#spanInf").html("UNLIKE: " + unfancnt + " %");

    var data = google.visualization.arrayToDataTable(itemstwt);

    var options = {
        series: [{ color: '#7ac143', visibleInLegend: true }, { color: '#1d67a7', visibleInLegend: true}]
    };

    var chart = new google.visualization.LineChart(document.getElementById('Stories_Graph'));
    chart.draw(data, options);
}