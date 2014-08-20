/* ==========================================================
 * ErgoAdmin v1.2
 * maps_vector.js
 * 
 * http://www.mosaicpro.biz
 * Copyright MosaicPro
 *
 * Built exclusively for sale @Envato Marketplaces
 * ========================================================== */ 

$(function()
{
	
	$('#maps_vector_tabs a[data-toggle="tab"]').on('shown', function (e)
	{
		if ($(this).attr('data-init'))
			return;
		
		$(this).attr('data-init', 1);
		switch ($(this).attr('href'))
		{
			case '#tab1':
				initWorldMapGDP();
				break;
				
			case '#tab2':
				initWorldMapMarkers();
				break;
				
			case '#tab3':
				initUSAUnemployment();
				break;
				
			case '#tab4':
				initRegionSelection();
				break;
				
			case '#tab5':
				initFranceElections();
				break;
				
			case '#tab6':
				initRandomColors();
				break;
				
			case '#tab7':
				initMallMap();
				break;
				
			case '#tab8':
				initProjectionMap();
				break;
		}
	});
	
	// load this map by default
	initWorldMapGDP();
	
	// GDP by country
	function initWorldMapGDP()
	{
		$('#world-map-gdp').vectorMap({
			map: 'world_mill_en',
			series: {
				regions: [{
					values: gdpData,
					scale: ['#C8EEFF', '#0071A4'],
					normalizeFunction: 'polynomial'
				}]
			},
			onLabelShow: function(e, el, code){
				el.html(el.html()+' (GDP - '+gdpData[code]+')');
			}
		});
	}
	
	// World map markers
	function initWorldMapMarkers()
	{
		$('#world-map-markers').vectorMap({
			map: 'world_mill_en',
			scaleColors: ['#C8EEFF', '#0071A4'],
			normalizeFunction: 'polynomial',
			hoverOpacity: 0.7,
			hoverColor: false,
			markerStyle: {
				initial: {
					fill: primaryColor,
					stroke: '#383f47'
				}
			},
			backgroundColor: '#383f47',
			markers: [
			          {latLng: [41.90, 12.45], name: 'Vatican City'},
			          {latLng: [43.73, 7.41], name: 'Monaco'},
			          {latLng: [-0.52, 166.93], name: 'Nauru'},
			          {latLng: [-8.51, 179.21], name: 'Tuvalu'},
			          {latLng: [43.93, 12.46], name: 'San Marino'},
			          {latLng: [47.14, 9.52], name: 'Liechtenstein'},
			          {latLng: [7.11, 171.06], name: 'Marshall Islands'},
			          {latLng: [17.3, -62.73], name: 'Saint Kitts and Nevis'},
			          {latLng: [3.2, 73.22], name: 'Maldives'},
			          {latLng: [35.88, 14.5], name: 'Malta'},
			          {latLng: [12.05, -61.75], name: 'Grenada'},
			          {latLng: [13.16, -61.23], name: 'Saint Vincent and the Grenadines'},
			          {latLng: [13.16, -59.55], name: 'Barbados'},
			          {latLng: [17.11, -61.85], name: 'Antigua and Barbuda'},
			          {latLng: [-4.61, 55.45], name: 'Seychelles'},
			          {latLng: [7.35, 134.46], name: 'Palau'},
			          {latLng: [42.5, 1.51], name: 'Andorra'},
			          {latLng: [14.01, -60.98], name: 'Saint Lucia'},
			          {latLng: [6.91, 158.18], name: 'Federated States of Micronesia'},
			          {latLng: [1.3, 103.8], name: 'Singapore'},
			          {latLng: [1.46, 173.03], name: 'Kiribati'},
			          {latLng: [-21.13, -175.2], name: 'Tonga'},
			          {latLng: [15.3, -61.38], name: 'Dominica'},
			          {latLng: [-20.2, 57.5], name: 'Mauritius'},
			          {latLng: [26.02, 50.55], name: 'Bahrain'},
			          {latLng: [0.33, 6.73], name: 'São Tomé and Príncipe'}
			          ]
		});
	}
	
	// USA unemployment
	function initUSAUnemployment()
	{
		$.getJSON( basePath + 'theme/scripts/plugins/maps/jvectormap/data/us-unemployment.json', function(data){
			var val = 2009;
			statesValues = jvm.values.apply({}, jvm.values(data.states)),
			metroPopValues = Array.prototype.concat.apply([], jvm.values(data.metro.population)),
			metroUnemplValues = Array.prototype.concat.apply([], jvm.values(data.metro.unemployment));

			$('#usa-unemployment').vectorMap({
				map: 'us_aea_en',
				markers: data.metro.coords,
				series: {
					markers: [{
						attribute: 'fill',
						scale: ['#FEE5D9', '#A50F15'],
						values: data.metro.unemployment[val],
						min: jvm.min(metroUnemplValues),
						max: jvm.max(metroUnemplValues)
					},{
						attribute: 'r',
						scale: [5, 20],
						values: data.metro.population[val],
						min: jvm.min(metroPopValues),
						max: jvm.max(metroPopValues)
					}],
					regions: [{
						scale: ['#DEEBF7', '#08519C'],
						attribute: 'fill',
						values: data.states[val],
						min: jvm.min(statesValues),
						max: jvm.max(statesValues)
					}]
				},
				onMarkerLabelShow: function(event, label, index){
					label.html(
							'<b>'+data.metro.names[index]+'</b><br/>'+
							'<b>Population: </b>'+data.metro.population[val][index]+'</br>'+
							'<b>Unemployment rate: </b>'+data.metro.unemployment[val][index]+'%'
					);
				},
				onRegionLabelShow: function(event, label, code){
					label.html(
							'<b>'+label.html()+'</b></br>'+
							'<b>Unemployment rate: </b>'+data.states[val][code]+'%'
					);
				}
			});

			var mapObject = $('#usa-unemployment').vectorMap('get', 'mapObject');

			$("#usa-unemployment-slider").slider({
				value: val,
				min: 2005,
				max: 2009,
				step: 1,
				create: JQSliderCreate,
				slide: function( event, ui ) {
					$('#usa-unemployment-slider-year strong').html(ui.value);
					val = ui.value;
					mapObject.series.regions[0].setValues(data.states[ui.value]);
					mapObject.series.markers[0].setValues(data.metro.unemployment[ui.value]);
					mapObject.series.markers[1].setValues(data.metro.population[ui.value]);
				}
			});
		});
	}
	
	// regions selection
	function initRegionSelection()
	{
		map = new jvm.WorldMap({
			container: $('#regions-selection'),
			map: 'de_merc_en',
			regionsSelectable: true,
			markersSelectable: true,
			markers: [
			          {latLng: [52.50, 13.39], name: 'Berlin'},
			          {latLng: [53.56, 10.00], name: 'Hamburg'},
			          {latLng: [48.13, 11.56], name: 'Munich'},
			          {latLng: [50.95, 6.96], name: 'Cologne'},
			          {latLng: [50.11, 8.68], name: 'Frankfurt am Main'},
			          {latLng: [48.77, 9.17], name: 'Stuttgart'},
			          {latLng: [51.23, 6.78], name: 'Dusseldorf'},
			          {latLng: [51.51, 7.46], name: 'Dortmund'},
			          {latLng: [51.45, 7.01], name: 'Essen'},
			          {latLng: [53.07, 8.80], name: 'Bremen'}
			],
			markerStyle: {
				initial: {
					fill: '#4DAC26'
				},
			    selected: {
			    	fill: '#CA0020'
			    }
			},
			regionStyle: {
				initial: {
					fill: '#B8E186'
				},
			    selected: {
			    	fill: '#F4A582'
			    }
			},
			series: {
				markers: [{
					attribute: 'r',
			        scale: [5, 15],
			        values: [
			                 887.70,
			        		 755.16,
			        		 310.69,
			        		 405.17,
			        		 248.31,
			        		 207.35,
			        		 217.22,
			        		 280.71,
			        		 210.32,
			        		 325.42
			        ]
				}]
			},
			onRegionSelected: function(){
				if (window.localStorage) {
					window.localStorage.setItem(
							'jvectormap-selected-regions',
			        		JSON.stringify(map.getSelectedRegions())
					);
				}
			},
			onMarkerSelected: function(){
				if (window.localStorage) {
					window.localStorage.setItem(
							'jvectormap-selected-markers',
			        		JSON.stringify(map.getSelectedMarkers())
					);
			        }
			}
		});
		map.setSelectedRegions( JSON.parse( window.localStorage.getItem('jvectormap-selected-regions') || '[]' ) );
		map.setSelectedMarkers( JSON.parse( window.localStorage.getItem('jvectormap-selected-markers') || '[]' ) );
	}
	
	// France elections
	function initFranceElections()
	{
		$.getJSON('theme/scripts/plugins/maps/jvectormap/data/france-elections.json', function(data){
			new jvm.WorldMap({
				map: 'fr_merc_en',
				container: $('#france-2007'),
				series: {
					regions: [{
						scale: {
							'1': '#4169E1',
							'2': '#FF69B4'
						},
						attribute: 'fill',
						values: data['year2007'].results
					}]
				}
			});

			new jvm.WorldMap({
				map: 'fr_merc_en',
				container: $('#france-2012'),
				series: {
					regions: [{
						scale: {
							'1': '#FF69B4',
							'2': '#4169E1'
						},
						attribute: 'fill',
						values: data['year2012'].results
					}]
				}
			});
		});
	}
	
	// random colors
	var palette = ['#66C2A5', '#FC8D62', '#8DA0CB', '#E78AC3', '#A6D854'],
		generateColors = function(){
	        var colors = {},
	            key;
	
	        for (key in colorsMap.regions) {
	          colors[key] = palette[Math.floor(Math.random()*palette.length)];
	        }
	        return colors;
	      },
	      colorsMap;
	      
	function initRandomColors()
	{
		colorsMap = new jvm.WorldMap({
			map: 'es_merc_en',
			container: $('#random-colors-map'),
			series: {
				regions: [{
					attribute: 'fill'
				}]
			}
		});
		colorsMap.series.regions[0].setValues(generateColors());
		$('#update-colors-button').click(function(e){
			e.preventDefault();
			colorsMap.series.regions[0].setValues(generateColors());
		});
	}
	
	// mall map
	function initMallMap()
	{
		$('#mall-map').vectorMap({
			map: 'mall',
			backgroundColor: 'transparent',
			markers: [{
				coords: [60, 110],
				name: 'Escalator 1',
				style: {fill: 'yellow'}
			},{
				coords: [260, 95],
				name: 'Escalator 2',
				style: {fill: 'yellow'}
			},{
				coords: [434, 95],
				name: 'Escalator 3',
				style: {fill: 'yellow'}
			},{
				coords: [634, 110],
				name: 'Escalator 4',
				style: {fill: 'yellow'}
			}],
			series: {
				regions: [{
					values: {
						F102: 'SPORTS & OUTDOOR',
						F103: 'HOME DECOR',
						F105: 'FASHION',
						F106: 'OTHER',
						F108: 'BEAUTY & SPA',
						F109: 'FASHION',
						F110: 'BEAUTY & SPA',
						F111: 'URBAN FAVORITES',
						F114: 'SERVICES',
						F166: 'DINING',
						F167: 'FASHION',
						F169: 'DINING',
						F170: 'ENTERTAINMENT',
						F172: 'DINING',
						F174: 'DINING',
						F115: 'KIDS STUFF',
						F117: 'LIFESTYLE',
						F118: 'URBAN FAVORITES',
						F119: 'FASHION',
						F120: 'FASHION',
						F122: 'KIDS STUFF',
						F124: 'KIDS STUFF',
						F125: 'KIDS STUFF',
						F126: 'KIDS STUFF',
						F128: 'KIDS STUFF',
						F129: 'LIFESTYLE',
						F130: 'HOME DECOR',
						F132: 'DINING',
						F133: 'SPORTS & OUTDOOR',
						F134: 'KIDS STUFF',
						F135: 'LIFESTYLE',
						F136: 'LIFESTYLE',
						F139: 'KIDS STUFF',
						F153: 'DINING',
						F155: 'FASHION',
						F156: 'URBAN FAVORITES',
						F157: 'URBAN FAVORITES',
						F158: 'LINGERIE & UNDERWEAR',
						F159: 'FASHION',
						F160: 'FASHION',
						F162: 'FASHION',
						F164: 'FASHION',
						F165: 'FASHION',
						FR01: 'REST ROOMS',
						FR02: 'REST ROOMS',
						FR03: 'REST ROOMS',
						FR04: 'REST ROOMS',
						FFC: 'DINING'
					},
					scale: {
						"FASHION": "#2761ad",
						"LINGERIE & UNDERWEAR": "#d58aa3",
						"BEAUTY & SPA": "#ee549f",
						"URBAN FAVORITES": "#15bbba",
						"SPORTS & OUTDOOR": "#8864ab",
						"KIDS STUFF": "#ef4e36",
						"ENTERTAINMENT": "#e47325",
						"HOME DECOR": "#a2614f",
						"LIFESTYLE": "#8a8934",
						"DINING": "#73bb43",
						"REST ROOMS": "#6c260f",
						"SERVICES": "#504d7c",
						"OTHER": "#c7b789"
					}
				}]
			},
			onRegionLabelShow: function(e, el, code){
				if (el.html() === '') {
					e.preventDefault();
				}
			}
		});
	}
	
	// reverse projection map
	var mapProjection,
	    markerIndex = 0,
	    markersCoords = {};
	
	function initProjectionMap()
	{
		mapProjection = new jvm.WorldMap({
			map: 'us_lcc_en',
			markerStyle: {
				initial: {
					fill: 'red'
				}
			},
			container: $('#projection-map'),
			onMarkerLabelShow: function(e, label, code){
				mapProjection.label.text(markersCoords[code].lat.toFixed(2)+', '+markersCoords[code].lng.toFixed(2));
			},
			onMarkerClick: function(e, code){
				mapProjection.removeMarkers([code]);
				mapProjection.label.hide();
			}
		});
		
		mapProjection.container.click(function(e){
			var latLng = mapProjection.pointToLatLng(e.offsetX, e.offsetY),
			targetCls = $(e.target).attr('class');

			if (latLng && (!targetCls || (targetCls && $(e.target).attr('class').indexOf('jvectormap-marker') === -1))) {
				markersCoords[markerIndex] = latLng;
				mapProjection.addMarker(markerIndex, {latLng: [latLng.lat, latLng.lng]});
				markerIndex += 1;
			}
		});
	}
	
});