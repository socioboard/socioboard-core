function drawVisualization() {
        // Some raw data (not necessarily accurate)
        var data = google.visualization.arrayToDataTable([
          ['Month', 'Bolivia',  'Average'],
          ['May 22',  -25,        80],
          ['',  -35,       120],
          ['May 24',  -17,        110],
          ['',  -39,        130],
          ['May 26',  -16,       80],
		  ['',  -25,        80],
          ['May 28',  -25,       120],
          ['',  -27,        110],
          ['May 30',  -39,        130],
          ['',  -36,       80],
		  ['Jun 1',  -15,        80],
          ['',  -15,       120],
          ['Jun 3',  -17,        110],
          ['',  -29,        130],
          ['Jun 5',  -16,       80],
		  ['',  -5,        80],
          ['Jun 7',  -15,       120],
          ['',  -27,        110],
          ['Jun 9', -39,        130],
          ['',  -16,       80]
        ]);

        var options = {
          //title : 'Monthly Coffee Production by Country',
          //vAxis: {title: "Cups"},
          hAxis: {title: "Month"},
          seriesType: "bars",
		  
          series: {1: {type: "line", color: '#3b5998', visibleInLegend: true}}
        };

        var chart = new google.visualization.ComboChart(document.getElementById('fb_combo_chart'));
        chart.draw(data, options);
      }
      google.setOnLoadCallback(drawVisualization);
	  
	  
	  //Impression Chart
      // google.load("visualization", "1", {packages:["corechart"]});
      function getImpression(item) {
          debugger;
          google.setOnLoadCallback(drawChart_impr);
          function drawChart_impr() {
              var data = google.visualization.arrayToDataTable(item);

              var options = {
                  series: [{ color: '#3b5998', visibleInLegend: true}]
              };

              var chart = new google.visualization.ColumnChart(document.getElementById('impress_chart'));
              chart.draw(data, options);
          }

      }
	  //Impression Breakdown Chart 1
	  google.load("visualization", "1", {packages:["corechart"]});
      google.setOnLoadCallback(drawChart_pie1);
      function drawChart_pie1() {
        var data = google.visualization.arrayToDataTable([
          ['Task', 'Hours per Day'],
          ['Work',     11],
          ['Eat',      2],
          ['Commute',  2],
          ['Watch TV', 2],
          ['Sleep',    7]
        ]);

        var options = {
          //title: 'My Daily Activities'
        };

        var chart = new google.visualization.PieChart(document.getElementById('impress_pie1'));
        chart.draw(data, options);
      }
	  
	  
	  
	  //Impression Breakdown Chart 2
	  google.load("visualization", "1", {packages:["corechart"]});
      google.setOnLoadCallback(drawChart_pie2);
      function drawChart_pie2() {
        var data = google.visualization.arrayToDataTable([
          ['Task', 'Hours per Day'],
          ['Work',     20],
          ['Eat',      2],
          ['Commute',  2],
          ['Watch TV', 2],
          ['Sleep',    3]
        ]);

        var options = {
          //title: 'My Daily Activities'
        };

        var chart = new google.visualization.PieChart(document.getElementById('impress_pie2'));
        chart.draw(data, options);
      }
	  
	  
	  //Impression daily Chart 
	  google.load("visualization", "1", {packages:["corechart"]});
      google.setOnLoadCallback(drawChart_impr_day);
      function drawChart_impr_day() {
        var data = google.visualization.arrayToDataTable([
          ['Day', 'Impress'],
          ['Sun',  1000],
          ['Mon',  1170],
          ['Tue',  660],
		  ['Wed',  1000],
          ['Thu',  1170],
          ['Fri',  660],
          ['Sat',  1030]
        ]);

        var options = {
           hAxis: { textPosition: 'none' },
		   series: [{color: '#7c7c7a', visibleInLegend: true}]
        };

        var chart = new google.visualization.BarChart(document.getElementById('impress_daily'));
        chart.draw(data, options);
      }
	  
	  
	  //Impression Location Chart 
	  google.load("visualization", "1", {packages:["corechart"]});
      google.setOnLoadCallback(drawChart_locat);
      function drawChart_locat() {
        var data = google.visualization.arrayToDataTable([
          ['Location', 'Impress'],
          ['Delhi',  1000],
          ['Banglore',  1170],
          ['Mumbai',  660],
		  ['Delhi,India',  1000],
          ['Pune',  1030]
        ]);

        var options = {
           hAxis: { textPosition: 'none' },
		   series: [{color: '#7c7c7a', visibleInLegend: true}]
        };

        var chart = new google.visualization.BarChart(document.getElementById('location_graph'));
        chart.draw(data, options);
      }
	  
	  
	  //stories combo chart
	  function drawVisualization_a() {
        // Some raw data (not necessarily accurate)
        var data = google.visualization.arrayToDataTable([
          ['Month', 'Bolivia',  'Average'],
          ['May 22',  25,        80],
          ['',  35,       120],
          ['May 24',  17,        110],
          ['',  39,        130],
          ['May 26',  16,       80],
		  ['',  25,        80],
          ['May 28',  25,       120],
          ['',  27,        110],
          ['May 30',  39,        130],
          ['',  36,       80],
		  ['Jun 1',  15,        80],
          ['',  15,       120],
          ['Jun 3',  17,        110],
          ['',  29,        130],
          ['Jun 5',  16,       80],
		  ['',  5,        80],
          ['Jun 7',  15,       120],
          ['',  27,        110],
          ['Jun 9', 39,        130],
          ['',  16,       80]
        ]);

        var options = {
          //title : 'Monthly Coffee Production by Country',
          //vAxis: {title: "Cups"},
          hAxis: {title: "Month"},
          seriesType: "bars",
		  
          series: {1: {type: "line", color: '#b0b0b0', visibleInLegend: true}}
        };

        var chart = new google.visualization.ComboChart(document.getElementById('stories_combo_chart'));
        chart.draw(data, options);
      }
      google.setOnLoadCallback(drawVisualization_a);



      //Share pie Chart
      function getLocationCount(item) {
          document.getElementById('share_pie').innerHtml = "";
          google.load("visualization", "1", { packages: ["corechart"] });
          debugger;
        
          function drawChart_pie3() {
              var data = google.visualization.arrayToDataTable(item);
              debugger;
              var options = {
              //title: 'My Daily Activities'
          };
         
          var chart = new google.visualization.PieChart(document.getElementById('share_pie'));
          chart.draw(data, options);
      }
      google.setOnLoadCallback(drawChart_pie3);
  }
	  //Share daily Chart 
	  google.load("visualization", "1", {packages:["corechart"]});
      google.setOnLoadCallback(drawChart_share_day);
      function drawChart_share_day() {
        var data = google.visualization.arrayToDataTable([
          ['Day', 'Impress'],
          ['Sun',  1000],
          ['Mon',  1170],
          ['Tue',  660],
		  ['Wed',  1000],
          ['Thu',  1170],
          ['Fri',  660],
          ['Sat',  1030]
        ]);

        var options = {
           hAxis: { textPosition: 'none' },
		   series: [{color: '#7c7c7a', visibleInLegend: true}]
        };

        var chart = new google.visualization.BarChart(document.getElementById('share_daily'));
        chart.draw(data, options);
      }
	  
	  //Impression Location Chart 
	  google.load("visualization", "1", {packages:["corechart"]});
      google.setOnLoadCallback(drawChart_locat_share);
      function drawChart_locat_share() {
        var data = google.visualization.arrayToDataTable([
          ['Location', 'Impress'],
          ['Delhi',  1000],
          ['Banglore',  1170],
          ['Mumbai',  660],
		  ['Delhi,India',  1000],
          ['Pune',  1030]
        ]);

        var options = {
           hAxis: { textPosition: 'none' },
		   series: [{color: '#7c7c7a', visibleInLegend: true}]
        };

        var chart = new google.visualization.BarChart(document.getElementById('location_graph2'));
        chart.draw(data, options);
    }

    function getAgeDiff(arrAge)
    {
        debugger;
        var bar = new RGraph.Bar('cvs', arrAge)
                .Set('grouping', 'stacked')
            //.Set('labels', ['John','James','Fred','Luke','Luis'])
            //.Set('labels.above', true)
            //.Set('labels.above.decimals', 2)
                .Set('linewidth', 0)
            //.Set('strokestyle', 'white')
                .Set('colors', ['Gradient(#4572A7:#66f)', 'Gradient(#AA4643:white)', 'Gradient(#89A54E:white)'])
                .Set('shadow', true)
                .Set('shadow.offsetx', 1)
                .Set('shadow.offsety', 2)
                .Set('shadow.blue', 5)
            //.Set('hmargin', 5)
                .Set('gutter.left', 0)
                .Set('background.grid.vlines', false)
                .Set('background.grid.border', false)
				.Set('background.grid.hlines', false)
                .Set('axis.color', '#ccc')
                .Set('noyaxis', true)
            //.Set('key', ['Monday','Tuesday','Wednesday'])
                .Set('key.position', 'gutter');
            bar.Set('key.position.x', bar.canvas.width - 50)
                .Set('key.position.y', 10)
            //.Set('key.colors', ['blue','#c00','#0c0'])
            /*.ondraw = function (obj)
            {
            for (var i=0; i<obj.coords.length; ++i) {
            obj.context.fillStyle = 'white';
            RGraph.Text(obj.context, 'Verdana', 8, obj.coords[i][0] + (obj.coords[i][2] / 2), obj.coords[i][1] + (obj.coords[i][3] / 2),obj.data_arr[i].toString(),'center', 'center', null,null,null,true);
            }
            }*/

            bar.Draw();

        }

        function getStories(item) {
            debugger;

            //var fbStoryArray=<%=strstoriesArray %>";
	        var barChartData = {
			labels : ["1","2","3","4","5","6","7"],
			datasets : [
				{
					fillColor : "rgba(178,178,178,1)",
					strokeColor : "rgba(178,178,178,0)",
					data: item
				}
			]
			
		}

var myLine = new Chart(document.getElementById("stories_combo_chart").getContext("2d")).Bars(barChartData);
debugger;
        }
      