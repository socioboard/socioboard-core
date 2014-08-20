function reloadGraph(twtData) {
    twtArr = twtData[0].split(",");
    twtFollowArr = twtData[1].split(",");
    twtInMsgArr = twtData[2].split(",");
    twtDMRecArr = twtData[3].split(",");
    twtDMArrSent = twtData[4].split(",");
    twtSentMsgArr = twtData[5].split(",");
    reTwt = twtData[6].split(",");
    ageDiff = twtData[7].split(",");
    twtmention = twtData[10].split(",");

    var engTwt = twtData[8].split(",");
    var infTwt = twtData[9].split(",");
    var TwtDate = twtData[11].split(",");

    debugger;
    ///////////Followers/////////////////////
    var items = new Array(twtArr.length);
    items[0] = new Array(2);
    items[0][0] = "Age";
    items[0][1] = "Visits";
    for (var i = 0; i < twtArr.length; i++) {
        items[i + 1] = new Array(2);
        if (twtArr[i] != "") {
            items[i + 1][0] = i;
            items[i + 1][1] = Number(twtArr[i]);
        }

    }
    var data = google.visualization.arrayToDataTable(items);
    var options = {
        hAxis: { textPosition: 'none' },
        vAxis: { textPosition: 'none' },
        hAxis: { gridlines: { color: '#ffffff'} },
        series: [{ color: '#7ac143', visibleInLegend: true}]
    };

    var chart = new google.visualization.LineChart(document.getElementById('newfollower_graph'));
    chart.draw(data, options);

    ////////////////////Following///////////////////////////////////////

    var itemstwtFollow = new Array(twtFollowArr.length);
    itemstwtFollow[0] = new Array(2);
    itemstwtFollow[0][0] = "Days";
    itemstwtFollow[0][1] = "Following";
    for (var i = 1; i <= twtFollowArr.length; i++) {
        itemstwtFollow[i] = new Array(2);
        if (twtFollowArr[i] != "") {
            itemstwtFollow[i][0] = i;
            itemstwtFollow[i][1] = Number(twtFollowArr[i]);
        }
    }
    var data = google.visualization.arrayToDataTable(itemstwtFollow);

    var options = {
        hAxis: { textPosition: 'none' },
        vAxis: { textPosition: 'none' },
        hAxis: { gridlines: { color: '#ffffff'} },
        series: [{ color: '#7ac143', visibleInLegend: true}]
    };

    var chart = new google.visualization.LineChart(document.getElementById('newfollowed_graph'));
    chart.draw(data, options);

    ////////////////////Mention///////////////////////////////////////

    var itemstwtMention = new Array(twtmention.length);
    itemstwtMention[0] = new Array(2);
    itemstwtMention[0][0] = "Days";
    itemstwtMention[0][1] = "Following";
    for (var i = 1; i <= twtmention.length; i++) {
        itemstwtMention[i] = new Array(2);
        if (twtmention[i] != "") {
            itemstwtMention[i][0] = i;
            itemstwtMention[i][1] = Number(twtmention[i]);
        }
    }
    var data = google.visualization.arrayToDataTable(itemstwtMention);

    var options = {
        hAxis: { textPosition: 'none' },
        vAxis: { textPosition: 'none' },
        hAxis: { gridlines: { color: '#ffffff'} },
        series: [{ color: '#7ac143', visibleInLegend: true}]
    };

    var chart = new google.visualization.LineChart(document.getElementById('mention_graph'));
    chart.draw(data, options);



    ////////////////////Incoming Msgs///////////////////

    try {
        var itemstwtInMsg = new Array(twtInMsgArr.length);
        itemstwtInMsg[0] = new Array(2);
        itemstwtInMsg[0][0] = "Days";
        itemstwtInMsg[0][1] = "Following";
        for (var i = 1; i <= twtInMsgArr.length; i++) {
            itemstwtInMsg[i] = new Array(2);
            if (twtInMsgArr[i] != "") {
                itemstwtInMsg[i][0] = i;
                itemstwtInMsg[i][1] = Number(twtInMsgArr[i]);               
            }
        }
        var data = google.visualization.arrayToDataTable(itemstwtInMsg);
        var options = {
            hAxis: { textPosition: 'none' },
            vAxis: { textPosition: 'none' },
            hAxis: { gridlines: { color: '#ffffff'} },
            series: [{ color: '#535353', visibleInLegend: true}]
        };

        var chart = new google.visualization.LineChart(document.getElementById('msg_rece_graph'));
        chart.draw(data, options);
    }
    catch (e) {
        console.log(e);
                }
    /////////////Sent Msg////////////////////////

    debugger;
    var itemstwtSentMsg = new Array(twtSentMsgArr.length);
    itemstwtSentMsg[0] = new Array(2);
    itemstwtSentMsg[0][0] = "Days";
    itemstwtSentMsg[0][1] = "Following";
    for (var i = 1; i <= twtSentMsgArr.length; i++) {
        itemstwtSentMsg[i] = new Array(2);
        if (twtSentMsgArr[i] != "") {
            itemstwtSentMsg[i][0] = i;
            itemstwtSentMsg[i][1] = Number(twtSentMsgArr[i]);           
        }
    }
    var data = google.visualization.arrayToDataTable(itemstwtSentMsg);
    var options = {
        hAxis: { textPosition: 'none' },
        vAxis: { textPosition: 'none' },
        hAxis: { gridlines: { color: '#ffffff'} },
        series: [{ color: '#75bf55', visibleInLegend: true}]
    };
    var chart = new google.visualization.LineChart(document.getElementById('msg_sent_graph'));
    chart.draw(data, options);

    /////////////////DM Recieve//////////

       var itemstwtDMRec = new Array(twtDMRecArr.length);
                var dmr = 0;
                itemstwtDMRec[0] = new Array(2);
                itemstwtDMRec[0][0] = "Days";
                itemstwtDMRec[0][1] = "Following";
                for (var i = 1; i <= twtDMRecArr.length; i++) {
                    itemstwtDMRec[i] = new Array(2);
                    if (twtDMRecArr[i] != "") {
                        itemstwtDMRec[i][0] = i;
                        itemstwtDMRec[i][1] = Number(twtDMRecArr[i]);
                        dmr = Number(dmr) + Number(twtDMRecArr[i]);                       
                    }

                }
                var data = google.visualization.arrayToDataTable(itemstwtDMRec);

	          var options = {
	              hAxis: { textPosition: 'none' },
	              vAxis: { textPosition: 'none' },
	              hAxis: { gridlines: { color: '#ffffff'} },
	              series: [{ color: '#535353', visibleInLegend: true}]
	          };

	          var chart = new google.visualization.LineChart(document.getElementById('msg_rec_graph'));
	          chart.draw(data, options);

	          //////////// DM Sent////////////////
	          var itemstwtDMSent = new Array(twtDMArrSent.length);
	          var dmSent = 0;
	          itemstwtDMSent[0] = new Array(2);
	          itemstwtDMSent[0][0] = "Days";
	          itemstwtDMSent[0][1] = "Following";
	          for (var i = 1; i <= twtDMArrSent.length; i++) {
	              itemstwtDMSent[i] = new Array(2);
	              if (twtDMArrSent[i - 1] != "") {
	                  itemstwtDMSent[i][0] = i;
	                  itemstwtDMSent[i][1] = Number(twtDMArrSent[i]);
	                  dmSent = Number(dmSent) + Number(twtDMArrSent[i]);
	              }
	          }
	          var data = google.visualization.arrayToDataTable(itemstwtDMSent);

	          var options = {
	              hAxis: { textPosition: 'none' },
	              vAxis: { textPosition: 'none' },
	              hAxis: { gridlines: { color: '#ffffff'} },
	              series: [{ color: '#535353', visibleInLegend: true}]
	          };

	          var chart = new google.visualization.LineChart(document.getElementById('dir_msg_sent_graph'));
	          chart.draw(data, options);

	          /////////////////ReTweet///////////////////
	          var itemsretwt = new Array(reTwt.length);
	          var retwt = 0;
	          itemsretwt[0] = new Array(2);
	          itemsretwt[0][0] = "Days";
	          itemsretwt[0][1] = "Following";
	          for (var i = 1; i <= reTwt.length; i++) {
	              itemsretwt[i] = new Array(2);
	              if (reTwt[i - 1] != "") {
	                  itemsretwt[i][0] = i;
	                  itemsretwt[i][1] = Number(reTwt[i]);
	                  retwt = Number(retwt) + Number(reTwt[i]);
	                 
	              }
	          }
	          var data = google.visualization.arrayToDataTable(itemsretwt);

	          var options = {
	              hAxis: { textPosition: 'none' },
	              vAxis: { textPosition: 'none' },
	              hAxis: { gridlines: { color: '#ffffff'} },
	              series: [{ color: '#75bf55', visibleInLegend: true}]
	          };

	          var chart = new google.visualization.LineChart(document.getElementById('retweet_graph'));
	          chart.draw(data, options);

	          //////////////Age Diff/////////////////////////
               var itemsage = new Array(ageDiff.length);

               for (var i = 0; i < ageDiff.length; i++) {
                   if (ageDiff[i] != "") {
                       itemsage[i] = Number(ageDiff[i]);
                   }
               }
	          var barAgeChartData = {
	              labels: ["18-20", "21-24", "25-34", "35-44", "45-54", "55-64", "65+"],
	              datasets: [
				{
				    fillColor: "rgba(178,178,178,1)",
				    strokeColor: "rgba(178,178,178,0)",
				    data: itemsage
				}
			]

	          }

var myLine = new Chart(document.getElementById("bar_5").getContext("2d")).Bars(barAgeChartData);

////////Engagement & Influence/////////////////////

debugger;     
var itemstwt = new Array(engTwt.length);
var eng = 0, inf = 0;
itemstwt[0] = new Array(3);
itemstwt[0][0] = "Days";
itemstwt[0][1] = "Engagement";
itemstwt[0][2] = "Influence";

for (var i = 1; i < engTwt.length; i++) {
    itemstwt[i] = new Array(3);
    if (engTwt[i] != "") {
        // itemstwt[i][0] = i;
        itemstwt[i][0] = TwtDate[i];
        itemstwt[i][1] = Number(engTwt[i]);
        itemstwt[i][2] = Number(infTwt[i]);
        eng = eng + Number(engTwt[i]);
        inf = inf + Number(infTwt[i]);
        // retwt = Number(retwt) + Number(reTwt[i])
    }
}
var engPer = Math.round((eng / engTwt.length) * 100);
var infPer = Math.round((inf / infTwt.length) * 100);
$("#spanEng").html("ENGAGEMENT: " + engPer + " %");
$("#spanInf").html("INFLUENCE: " + infPer + " %");

var data = google.visualization.arrayToDataTable(itemstwt);

var options = {
    series: [{ color: '#7ac143', visibleInLegend: true }, { color: '#1d67a7', visibleInLegend: true}]
};

var chart = new google.visualization.LineChart(document.getElementById('social_graph'));
chart.draw(data, options);
}