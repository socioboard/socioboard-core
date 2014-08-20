google.load("visualization", "1", {packages:["corechart"]});
      google.setOnLoadCallback(drawChart);
      function drawChart() {
        var data = google.visualization.arrayToDataTable([
          ['Age', 'Age'],
          ['18-20',  100],
          ['21-24',  30],
          ['25-34',  66],
          ['35-44',  38],
		  ['45-54',  70],
          ['55-64',  60],
          ['65+',  20]
		  
        ]);

         var options = {
             hAxis: { textPosition: 'none' },
			 series: [{color: '#606160', visibleInLegend: true}]
           
        };

        var chart = new google.visualization.BarChart(document.getElementById('chart_div'));
        chart.draw(data, options);
      }
	  
	  //Social Graph

      //  google.load("visualization", "1", {packages:["corechart"]});
      function getEngaegmentInfluence(item) {
          debugger;
          google.setOnLoadCallback(drawChart_a);
          function drawChart_a() {
              var data = google.visualization.arrayToDataTable(item);

              var options = {
                  series: [{ color: '#7ac143', visibleInLegend: true }, { color: '#1d67a7', visibleInLegend: true}]
              };

              var chart = new google.visualization.LineChart(document.getElementById('social_graph'));
              chart.draw(data, options);
          }
      }
	  
	  //New Follower Graph

       google.load("visualization", "1", {packages:["corechart"]});
      function getTwitterNewFollower(item) {
         // alert("New Follower");
         debugger;
      google.setOnLoadCallback(drawChart_b);
      function drawChart_b() {
          debugger;

          var data = google.visualization.arrayToDataTable(item);
          var options = {
              hAxis: { textPosition: 'none' },
              vAxis: { textPosition: 'none' },
              hAxis: { gridlines: { color: '#ffffff'} },
              series: [{ color: '#7ac143', visibleInLegend: true}]
          };

          var chart = new google.visualization.LineChart(document.getElementById('newfollower_graph'));
          chart.draw(data, options);
      }
//    var barChartData = {
//			labels : ["1","2","3","4","5","6","7"],
//			datasets : [
//				{
//					fillColor : "rgba(178,178,178,1)",
//					strokeColor : "rgba(178,178,178,0)",
//					data : [23,4,3,4,54,6,78]
//				}
//			]
//			
//		}

//var myLine = new Chart(document.getElementById("newfollower_graph").getContext("2d")).Line(barChartData);
      }
	  
	  
	  //New Followed Graph

	   google.load("visualization", "1", {packages:["corechart"]});
	  function getTwitterFollowing(item) {
	      google.setOnLoadCallback(drawChart_c);
	      function drawChart_c() {
	          debugger;
	          var data = google.visualization.arrayToDataTable(item);

	          var options = {
	              hAxis: { textPosition: 'none' },
	              vAxis: { textPosition: 'none' },
	              hAxis: { gridlines: { color: '#ffffff'} },
	              series: [{ color: '#7ac143', visibleInLegend: true}]
	          };

	          var chart = new google.visualization.LineChart(document.getElementById('newfollowed_graph'));
	          chart.draw(data, options);
	      }

	  }
	  //Direct message received Graph

	  //  google.load("visualization", "1", {packages:["corechart"]});
	  function getDirectMessageReceive(item) {
	      google.setOnLoadCallback(drawChart_d);
	      function drawChart_d() {
	          var data = google.visualization.arrayToDataTable(item);

	          var options = {
	              hAxis: { textPosition: 'none' },
	              vAxis: { textPosition: 'none' },
	              hAxis: { gridlines: { color: '#ffffff'} },
	              series: [{ color: '#535353', visibleInLegend: true}]
	          };

	          var chart = new google.visualization.LineChart(document.getElementById('msg_rec_graph'));
	          chart.draw(data, options);
	      }
	  }

      //DM Sent
	  function getDirectMessageSent(item) {
	      google.setOnLoadCallback(drawChart_d);
	      function drawChart_d() {
	          var data = google.visualization.arrayToDataTable(item);

	          var options = {
	              hAxis: { textPosition: 'none' },
	              vAxis: { textPosition: 'none' },
	              hAxis: { gridlines: { color: '#ffffff'} },
	              series: [{ color: '#535353', visibleInLegend: true}]
	          };

	          var chart = new google.visualization.LineChart(document.getElementById('dir_msg_sent_graph'));
	          chart.draw(data, options);
	      }
	  }
	  //@ Mentions Graph

	  function getTwtMention(item) {
	      google.setOnLoadCallback(drawChart_d);
	      function drawChart_d() {
	          var data = google.visualization.arrayToDataTable(item);

	          var options = {
	              hAxis: { textPosition: 'none' },
	              vAxis: { textPosition: 'none' },
	              hAxis: { gridlines: { color: '#ffffff'} },
	              series: [{ color: '#535353', visibleInLegend: true}]
	          };

	          var chart = new google.visualization.LineChart(document.getElementById('mention_graph'));
	          chart.draw(data, options);
	      }
	  }



//	  google.load("visualization", "1", {packages:["corechart"]});
//      google.setOnLoadCallback(drawChart_e);
//      function drawChart_e() {
//        var data = google.visualization.arrayToDataTable([
//          ['Month', 'ENGAGEMENT'],
//          ['May',  4],
//          ['June',  4],
//          ['July',  4],
//          ['August',  4]
//        ]);

//        var options = {
//		  hAxis: { textPosition: 'none' },
//		  vAxis: { textPosition: 'none' },
//		 hAxis: {gridlines: {color: '#ffffff'}},
//		  series: [{color: '#75bf55', visibleInLegend: true}]
//        };

//        var chart = new google.visualization.LineChart(document.getElementById('mention_graph'));
//        chart.draw(data, options);
//      }
	  
	  
	  //Message Sent Graph

      // google.load("visualization", "1", {packages:["corechart"]});
      function getSentMsg(item) {
          debugger;
          google.setOnLoadCallback(drawChart_f);
          function drawChart_f() {
              var data = google.visualization.arrayToDataTable(item);

              var options = {
                  hAxis: { textPosition: 'none' },
                  vAxis: { textPosition: 'none' },
                  hAxis: { gridlines: { color: '#ffffff'} },
                  series: [{ color: '#75bf55', visibleInLegend: true}]
              };

              var chart = new google.visualization.LineChart(document.getElementById('msg_sent_graph'));
              chart.draw(data, options);
          }
      }
	  
	  //Message Received Graph

      //  google.load("visualization", "1", {packages:["corechart"]});
      function getIncomingMsg(item) {
          debugger;
          google.setOnLoadCallback(drawChart_g);
          function drawChart_g() {
              var data = google.visualization.arrayToDataTable(item);

              var options = {
                  hAxis: { textPosition: 'none' },
                  vAxis: { textPosition: 'none' },
                  hAxis: { gridlines: { color: '#ffffff'} },
                  series: [{ color: '#535353', visibleInLegend: true}]
              };

              var chart = new google.visualization.LineChart(document.getElementById('msg_rece_graph'));
              chart.draw(data, options);
          }
      }
	  //Click Graph
	  
	  google.load("visualization", "1", {packages:["corechart"]});
      google.setOnLoadCallback(drawChart_h);
      function drawChart_h() {
        var data = google.visualization.arrayToDataTable([
          ['Month', 'ENGAGEMENT'],
          ['May',  2],
          ['June',  2],
          ['July',  2],
          ['August',  2]
        ]);

        var options = {
		  hAxis: { textPosition: 'none' },
		  vAxis: { textPosition: 'none' },
		 hAxis: {gridlines: {color: '#ffffff'}},
		  series: [{color: '#75bf55', visibleInLegend: true}]
        };

        var chart = new google.visualization.LineChart(document.getElementById('click_graph'));
        chart.draw(data, options);
      }
	  
	  //Retweet Graph

      //  google.load("visualization", "1", {packages:["corechart"]});
      function getRetweet(item) {
          google.setOnLoadCallback(drawChart_i);
          function drawChart_i() {
              var data = google.visualization.arrayToDataTable([
          ['Month', 'ENGAGEMENT'],
          ['May', 2],
          ['June', 2],
          ['July', 2],
          ['August', 2]
        ]);

              var options = {
                  hAxis: { textPosition: 'none' },
                  vAxis: { textPosition: 'none' },
                  hAxis: { gridlines: { color: '#ffffff'} },
                  series: [{ color: '#75bf55', visibleInLegend: true}]
              };

              var chart = new google.visualization.LineChart(document.getElementById('retweet_graph'));
              chart.draw(data, options);
          }

      }
	  //Retweet Graph
	  
	  google.load("visualization", "1", {packages:["corechart"]});
      google.setOnLoadCallback(drawChart_j);
      function drawChart_j() {
        var data = google.visualization.arrayToDataTable([
          ['Month', 'ENGAGEMENT'],
          ['May',  2],
          ['June',  2],
          ['July',  2],
          ['August',  2]
        ]);

        var options = {
		  hAxis: { textPosition: 'none' },
		  vAxis: { textPosition: 'none' },
		 hAxis: {gridlines: {color: '#ffffff'}},
		  series: [{color: '#75bf55', visibleInLegend: true}]
        };

        var chart = new google.visualization.LineChart(document.getElementById('dir_msg_sent_graph'));
        chart.draw(data, options);
      }
	  
	  
	  // Completed percentage 1
      //google.load("visualization", "1", {packages:["corechart"]});
      function getTwitterAgeWise(item) {
          debugger;
          var twtAgeArray = item;
	var barAgeChartData = {
			labels : ["18-20","21-24","25-34","35-44","45-54","55-64","65+"],
			datasets : [
				{
					fillColor : "rgba(178,178,178,1)",
					strokeColor : "rgba(178,178,178,0)",
					data : item
				}
			]
			
		}

var myLine = new Chart(document.getElementById("bar_5").getContext("2d")).Bars(barAgeChartData);
      }
	  
	  // Completed percentage 2
	  google.load("visualization", "1", {packages:["corechart"]});
      google.setOnLoadCallback(drawChart_percent2);
      function drawChart_percent2() {
        var data = google.visualization.arrayToDataTable([
          ['Age', 'Age'],
          ['18-20',  85]
          
		  
        ]);

         var options = {
             hAxis: { textPosition: 'none' },
			 vAxis: { textPosition: 'none' },
			 series: [{color: '#cb786f', visibleInLegend: true}]
           
        };

        var chart = new google.visualization.BarChart(document.getElementById('percent_complete-2'));
        chart.draw(data, options);
      }