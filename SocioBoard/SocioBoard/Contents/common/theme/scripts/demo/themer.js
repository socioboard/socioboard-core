/* ==========================================================
 * ErgoAdmin v1.2
 * themer.js
 * 
 * http://www.mosaicpro.biz
 * Copyright MosaicPro
 *
 * Built exclusively for sale @Envato Marketplaces
 * ========================================================== */ 

function themerUpdateColors(primary)
{
	updatePrimaryColor(primary, true, true);
}

//Converts an RGB object to a hex string
function rgb2hex(rgb) 
{
	var hex = [
		rgb.r.toString(16),
		rgb.g.toString(16),
		rgb.b.toString(16)
	];
	$.each(hex, function(nr, val) {
		if (val.length === 1) hex[nr] = '0' + val;
	});
	return '#' + hex.join('');
}

// converts a string to RGB object
function rgbString2obj(string)
{
	var parts = string.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/);
	var rgbObj = { r: Number(parts[1]), g: Number(parts[2]), b: Number(parts[3]) };
	return rgbObj;
}

function updatePrimaryColor(hex, attach, charts)
{
	themerPrimaryColor = hex;
	$('#themer-primary-cp').val(themerPrimaryColor);
	$.minicolors.refresh();
	
	if (attach === true)
		attachStylesheet();
	
	if (charts === true)
		updateCharts();
	
	if (themerPrimaryColor != themerThemes[themerSelectedTheme].primaryColor)
		themerCustom[themerSelectedTheme].primaryColor = themerPrimaryColor;
	else
		themerCustom[themerSelectedTheme].primaryColor = null;
	
	$.cookie('themerCustom', JSON.stringify(themerCustom));
	
	toggleGetCode();
}

function toggleGetCode()
{
	var tcs = themerCustom[themerSelectedTheme];
	
	if (themerSelectedTheme != 0 || (themerSelectedTheme == 0 && tcs.primaryColor != null))
	{
		if ($('#themer-getcode').is(':hidden')) $('#themer-getcode').show();
	}
	else
	{
		if ($('#themer-getcode').is(':visible')) $('#themer-getcode').hide();
	}
}

var themerAdvanced = $.cookie('themerAdvanced') != null ? $.cookie('themerAdvanced') == true : false;
function themerAdvancedToggle()
{
	var cp = [$('#themer-primary-cp'), $('#themer-header-cp'), $('#themer-menu-cp')];
	
	if ($('#themer-advanced-toggle').is(':checked'))
	{
		$('#themer').addClass('themer-advanced');
		$.each(cp, function(k,v){ v.attr('data-textfield', true).removeClass('minicolors-hidden'); });
	}
	else
	{
		$('#themer').removeClass('themer-advanced');
		$.each(cp, function(k,v){ v.attr('data-textfield', false).addClass('minicolors-hidden'); });
	}
}

function generateCSS(basePath)
{
	if(!basePath)
		basePath = "";
		
	var css =
		"@primaryColor: " + themerPrimaryColor + ";\n" +
		"#gradient {\n" +
		".vertical(@startColor: #555, @endColor: #333) {\n" +
		"   background-color: mix(@startColor, @endColor, 60%);\n" +
		"   background-image: -moz-linear-gradient(top, @startColor, @endColor); // FF 3.6+\n" +
		"   background-image: -webkit-gradient(linear, 0 0, 0 100%, from(@startColor), to(@endColor)); // Safari 4+, Chrome 2+\n" +
		"   background-image: -webkit-linear-gradient(top, @startColor, @endColor); // Safari 5.1+, Chrome 10+\n" +
		"   background-image: -o-linear-gradient(top, @startColor, @endColor); // Opera 11.10\n" +
		"   background-image: linear-gradient(to bottom, @startColor, @endColor); // Standard, IE10\n" +
		"   background-repeat: repeat-x;\n" +
		"   filter: e(%(\"progid:DXImageTransform.Microsoft.gradient(startColorstr='%d', endColorstr='%d', GradientType=0)\",argb(@startColor),argb(@endColor))); // IE9 and down\n" +
		"}\n" +
		"}\n\n" +
		
		primaryBgColorTargets.join(", \n") + "\n" + 
		"{\n" +
		"	background-color: @primaryColor;\n"+
		"}\n\n" +
		
		primaryGradientTargets.join(", \n") + "\n" + 
		"{\n" +
		"	#gradient > .vertical(lighten(@primaryColor, 15%), @primaryColor);\n"+
		"}\n\n" +
		
		primaryTextColorTargets.join(", \n") + "\n" + 
		"{\n" +
		"	color: @primaryColor;\n"+
		"}\n\n" +
		
		primaryBorderColorTargets.join(", \n") + "\n" + 
		"{\n" +
		"	border-color: @primaryColor;\n"+
		"}\n\n";
		
	css += 
		".table-primary tbody td\n" +
		"{\n" +
		"	background-color: lighten(@primaryColor, 50%);\n" +
		"}\n\n" +
		".table-primary tbody tr.selected td, .table-primary tbody tr.selectable:hover td\n" +
		"{\n" +
		"	background-color: lighten(@primaryColor, 40%);\n" +
		"}\n\n" +
		".table-primary.table-bordered tbody td, .table-primary, .pagination ul > .disabled > a, .pagination ul > .disabled > span\n" +
		"{\n" +
		"	border-color: lighten(@primaryColor, 50%);\n" +
		"}\n\n" +
		
		// top nav special
		".navbar.main .topnav > li.open.dd-1:not(.dd-2) .dropdown-menu > li:not(.profile):hover > a\n" +
		"{\n" +
		"	box-shadow: 0 5px 5px -5px darken(@primaryColor, 20%) inset, 0 -5px 5px -5px darken(@primaryColor, 20%) inset;\n" +
		"}\n\n" +
		".navbar.main .topnav > li.open.dd-2 .dropdown-menu > li:not(.profile):hover > a\n" +
		"{\n" +
		"	background: fade(@primaryColor, 7%);\n" +
		"}\n\n" +
		
		// sidebar special
		"#menu .slim-scroll > ul.menu-0 > li.active > a, #menu > ul.menu-0 > li.active > a\n" +
		"{\n" +
		"	box-shadow: 0 1px 0 0 lighten(@primaryColor, 10%) inset;\n" +
		"	-moz-box-shadow: 0 1px 0 0 lighten(@primaryColor, 10%) inset;\n" +
		"	-webkit-box-shadow: 0 1px 0 0 lighten(@primaryColor, 10%) inset;\n" +
		"	text-shadow: 0 1px 0 darken(@primaryColor, 20%);\n" +
		"}\n\n" +
		
		// footer special
		"#footer .copy a\n" +
		"{\n" +
		"	color: lighten(@primaryColor, 30%);\n" +
		"}\n\n" +
		
		// primary button special
		".btn-primary\n" +
		"{\n" +
		"	box-shadow: 0 1px 0 0 rgba(255, 255, 255, 0.3) inset, 0 0 0 1px darken(@primaryColor, 20%);\n" +
		"	-moz-box-shadow: 0 1px 0 0 rgba(255, 255, 255, 0.3) inset, 0 0 0 1px darken(@primaryColor, 20%);\n" +
		"	-webkit-box-shadow: 0 1px 0 0 rgba(255, 255, 255, 0.3) inset, 0 0 0 1px darken(@primaryColor, 20%);\n" +
		"}\n\n" +
		
		// border left color
		".bwizard-steps li.active:after, .sliderContainer .ui-rangeSlider-rightArrow .ui-rangeSlider-arrow-inner,\n" +
		".widget-chat .media .media-object.pull-right:after\n" +
		"{\n" +
		"	border-left-color: @primaryColor;\n" +
		"}\n\n" +
		
		// border right color
		".sliderContainer .ui-rangeSlider-leftArrow .ui-rangeSlider-arrow-inner,\n" +
		".widget-chat .media .media-object.pull-left:after\n" +
		"{\n" +
		"	border-right-color: @primaryColor;\n" +
		"}\n\n" +
		
		// border top color
		"#tlyPageGuideWrapper #tlyPageGuide li.tlypageguide-active.tlypageguide_right:after, #tlyPageGuideWrapper #tlyPageGuide li.tlypageguide-active.tlypageguide_left:after, #tlyPageGuideWrapper #tlyPageGuide li.tlypageguide-active.tlypageguide_top:after\n" +
		"{\n" +
		"	border-top-color: @primaryColor;\n" +
		"}\n\n" +
		
		// border bottom color
		"#tlyPageGuideWrapper #tlyPageGuide li.tlypageguide-active.tlypageguide_bottom:after\n" +
		"{\n" +
		"	border-bottom-color: @primaryColor;\n" +
		"}\n\n" +
		
		// active primary button
		".btn-primary:active, .btn-primary.active\n" +
		"{\n" +
		"	background-color: darken(@primaryColor, 20%);\n" +
		"}\n\n" +
		
		// hover/focus primary button
		".btn-primary:hover, .btn-primary:focus\n" +
		"{\n" +
		"	background-color: darken(@primaryColor, 5%);\n" +
		"}\n\n" +
		
		// guide tour
		".tlypageguide_shadow:after\n" +
		"{\n" +
		"	background-color: fade(@primaryColor, 20%);\n" +
		"}\n\n" +
		
		".widget .widget-body.list.list-2 ul li\n" +
		"{\n" +
		"	&.active { border-color: lighten(@primaryColor, 20%); }\n" +
		"	a { color: lighten(@primaryColor, 20%); i:before { background: lighten(@primaryColor, 50%); color: lighten(@primaryColor, 10%); border-color: lighten(@primaryColor, 20%); } }\n" +
		"}";
		
	return css;
}

function attachStylesheet(basePath, reset)
{
	/*if(!$("#themer-stylesheet").length) $('body').append('<div id="themer-stylesheet"></div>');
	$("#themer-stylesheet").html($('<style type="text/less">' + generateCSS(basePath) + '</style>'));*/
	
	if (themerSelectedTheme == 0)
	{
		$('#themer-stylesheet').empty();
		less.refreshStyles();
		if (reset === true) return false;
	}
	
	if(!$("#themer-stylesheet").length) 
		$('head').append('<style id="themer-stylesheet"></style>');
	
	var code = generateCSS(basePath);
	latestCode.less = code;
	
	$('#themer-stylesheet').attr('type', 'text/x-less').text(code);
	less.refreshStyles();
}

function updateCharts()
{
	if (typeof primaryColor != 'undefined')
		primaryColor = themerPrimaryColor;
		
	if (typeof genSparklines != 'undefined') 
		genSparklines();
	
	if (typeof charts == 'undefined')
		return false;
	
	// apply styling
	charts.utility.chartColors.shift();
	charts.utility.chartColors.unshift(themerPrimaryColor);
	
	if (typeof charts.website_traffic_graph != 'undefined' && charts.website_traffic_graph.plot != null)
		charts.website_traffic_graph.init();
	
	if (typeof charts.website_traffic_overview != 'undefined' && charts.website_traffic_overview.plot != null)
		charts.website_traffic_overview.init();
	
	if (typeof charts.traffic_sources_pie != 'undefined' && charts.traffic_sources_pie.plot != null)
		charts.traffic_sources_pie.init();
	
	if (typeof charts.chart_simple != 'undefined' && charts.chart_simple.plot != null)
		charts.chart_simple.init();
	
	if (typeof charts.chart_lines_fill_nopoints != 'undefined' && charts.chart_lines_fill_nopoints.plot != null)
		charts.chart_lines_fill_nopoints.init();
	
	if (typeof charts.chart_ordered_bars != 'undefined' && charts.chart_ordered_bars.plot != null)
		charts.chart_ordered_bars.init();
	
	if (typeof charts.chart_donut != 'undefined' && charts.chart_donut.plot != null)
		charts.chart_donut.init();
	
	if (typeof charts.chart_stacked_bars != 'undefined' && charts.chart_stacked_bars.plot != null)
		charts.chart_stacked_bars.init();
	
	if (typeof charts.chart_pie != 'undefined' && charts.chart_pie.plot != null)
		charts.chart_pie.init();
	
	if (typeof charts.chart_horizontal_bars != 'undefined' && charts.chart_horizontal_bars.plot != null)
		charts.chart_horizontal_bars.init();
	
	if (typeof charts.chart_live != 'undefined' && charts.chart_live.plot != null)
		charts.chart_live.init();
}

function updateTheme(themeSelect)
{
	if ($('#themer-theme').val() != themeSelect) $('#themer-theme').val(themeSelect);
	
	themerSelectedTheme = themeSelect; // index
	$.cookie('themerSelectedTheme', themerSelectedTheme);
	
	var uPrimaryColor = themerCustom[themeSelect].primaryColor != null ? themerCustom[themeSelect].primaryColor : themerThemes[themeSelect].primaryColor;
	
	updatePrimaryColor(uPrimaryColor, false, true);
	
	if (themeSelect == 0 && themerCustom[themeSelect].primaryColor == null)
		attachStylesheet('', true); // reset
	else
		attachStylesheet();
}

function themerGetCode(less)
{
	var tlc;
	if (less === true)
		tlc = latestCode.less;
	else
		tlc = latestCode.css();
		
	//bootbox.alert($('<textarea class="input-block-level" rows="10"></textarea>').val(tlc));
	bootbox.alert($('<pre class="prettyprint lang-html" id="themer-pretty"></pre>').html(tlc));
}

var primaryGradientTargets = 
[
 	"#menu .slim-scroll > ul.menu-0 > li.active > a",
 	"#menu .slim-scroll > ul.menu-0 > li.active:not(.hasSubmenu) > a:hover",
 	"#menu > ul.menu-0 > li.active:not(.hasSubmenu) > a:hover",
 	".widget-stats.primary",
 	".btn-primary",
 	".tabsbar:not(.tabsbar-2) ul li.active a"
];

var primaryBgColorTargets = 
[
	".btn-primary",
	"#flotTip",
	".btn-group.open .btn-primary.dropdown-toggle, .btn-primary.disabled, .btn-primary[disabled], .btn-primary:hover",
	".label-primary",
	".table-primary thead th",
	".pagination ul > .active > a, .pagination ul > .active > span",
	".gallery ul li .thumb",
	".widget-activity ul.filters li.glyphicons.active i",
	".ui-slider-wrap .slider-primary .ui-slider-range",
	".accordion-heading .accordion-toggle",
	".ui-widget-header",
	".ui-state-active, .ui-widget-content .ui-state-active, .ui-widget-header .ui-state-active",
	".fc-event-skin",
	"#external-events li",
	".notyfy_wrapper.notyfy_primary",
	".progress.progress-primary .bar",
	".alert.alert-primary",
	".pagination ul > li > a:hover, .pagination ul > li.primary > a",
	".gritter-item-wrapper.gritter-primary .gritter-item",
	"#content-notification .notyfy_wrapper.notyfy_primary",
	".ribbon-wrapper .ribbon.primary",
	".label.label-primary",
	".widget-stats.primary, .widget-stats.primary:hover",
	".tabsbar:not(.tabsbar-2) ul li.active a",
	".widget.widget-wizard-pills .widget-head ul li.primary a",
	".bwizard-steps li.active",
	".sliderContainer .ui-rangeSlider-bar",
	"#tlyPageGuideWrapper #tlyPageGuide li.tlypageguide-active",
	"#tlyPageGuideWrapper #tlyPageGuideMessages .tlypageguide_close",
	"#tlyPageGuideWrapper #tlyPageGuideMessages span",
	".tabsbar.tabsbar-2.active-fill ul li.active a",
	".shop-client-products.list ul li a .glyphicons i",
	".navbar.main .topnav > li.open.dd-1 .dropdown-menu > li:not(.profile):hover > a",
	".social-large a.active, .social-large a:hover",
	"#landing_1 .banner-1 .carousel-indicators li.active"
];
var primaryTextColorTargets = 
[
 	"a, p a",
	".widget .widget-body.list ul li .count",
	".widget-stats .txt strong",
	".glyphicons.single i:before",
	".glyphicons.single",
	".table-primary tbody td.important",
	".widget.widget-3 .widget-body.large.cancellations span span:first-child",
	".widget .widget-footer a:hover, .widget .widget-footer a:hover i:before",
	".widget.widget-3 .widget-footer a:hover, .widget.widget-3 .widget-footer a:hover i:before",
	"blockquote small",
	"#menu .profile span a",
	".tabsbar.tabsbar-2 ul li.active a",
	".tabsbar.tabsbar-2 ul li.active a i:before",
	".glyphicons.primary i:before, .glyphicons.standard:not(.disabled):hover i:before",
	".menubar.links.primary ul li a",
	".text-primary",
	"#docs_icons .glyphicons i:before",
	".widget.widget-tabs-double-2 .widget-head ul li.active a i:before, .widget.widget-tabs-double-2 .widget-head ul li.active a",
	".shop-client-products.product-details .form-horizontal .price",
	"#menu .appbrand",
	".widget-activity ul.list li:hover .activity-icon i:before, .widget-activity ul.list li.highlight .activity-icon i:before",
	"#menu ul.menu-1 > li.hasSubmenu.active ul li .glyphicons:hover i:before",
	".navbar.main .topnav > li.open.dd-2 .dropdown-menu > li:not(.profile):hover > a i:before",
	"#landing_1 .banner .banner-wrapper.banner-1 p a",
	"#landing_1 .banner .banner-wrapper.banner-1 h3",
	"#landing_2 .banner .banner-wrapper.banner-1 p a, #landing_2 .banner .banner-wrapper.banner-1 .buy a",
	"#landing_2 .banner .banner-wrapper.banner-1 h3",
	"#landing_1 .banner-1 .carousel-caption a",
	"div.glyphicons.glyphicon-primary i:before"
];
var primaryBorderColorTargets = 
[
	".btn-primary",
	".ui-slider-wrap .slider-primary .ui-slider-handle",
	"#flotTip",
	".widget.widget-2.primary .widget-head",
	".widget .widget-body.list.list-2 ul li.active a i:before",
	".table-primary thead th",
	".pagination ul > .active > a, .pagination ul > .active > span",
	".widget.widget-4 .widget-head .heading",
	".ui-widget-header",
	".fc-event-skin",
	".alert.alert-primary",
	".pagination ul > li > a:hover, .pagination ul > li.primary > a",
	".widget-stats.primary",
	"#menu .slim-scroll > ul.menu-0 > li.active > a",
	".widget-chat .media .media-body",
	".widget-chat .media .media-body.right",
	".navbar.main .topnav > li.open.dd-1 .dropdown-menu > li:not(.profile):hover > a",
	".navbar.main .topnav > li.open.dd-1:not(.dd-2) .dropdown-menu > li:not(.profile):hover",
	"#menu .slim-scroll > ul.menu-0 > li.active > a, #menu > ul.menu-0 > li.active > a"
];

/*
 * Persistent Selected Theme
 */
var themerSelectedTheme = $.cookie('themerSelectedTheme') != null ? $.cookie('themerSelectedTheme') : 0;

/*
 * Holds the latest CSS/LESS
 */
var latestCode = {
	css: function(){ return $('#themer-stylesheet').text(); },
	less: null
};

var themerThemes = [
	{
		name: "Default",
		primaryColor: primaryColor,
		visible: true
	},
	{
		name: "Brown",
		primaryColor: "#ba5d32",
		visible: true
	},
	{
		name: "Purple-Gray",
		primaryColor: "#86618f",
		visible: true
	},
	{
		name: "Purple-Wine",
		primaryColor: "#b94b6f",
		visible: true
	},
	{
		name: "Blue-Gray",
		primaryColor: "#496cad",
		visible: true
	},
	{
		name: "Green Army",
		primaryColor: "#6f8745",
		visible: true
	},
	{
		name: "Black & White",
		primaryColor: "#575757",
		visible: true
	},
	{
		name: "Army",
		primaryColor: "#7a7a3a",
		visible: false
	},
	{
		name: "Evil Army",
		primaryColor: "#567a3a",
		visible: false
	},
	{
		name: "Forest",
		primaryColor: "#947131",
		visible: false
	},
	{
		name: "Cold Blue",
		primaryColor: "#676d8a",
		visible: false
	},
	{
		name: "Warm Blue",
		primaryColor: "#cc5470",
		visible: false
	},
	{
		name: "Experiment #2",
		primaryColor: "#438080",
		visible: false
	}
];

/*
 * Persistent Custom Theme Colors
 */
var themerCustomDefault = [];
$.each(themerThemes, function(k,v) { themerCustomDefault[k] = { primaryColor: null }; });
var themerCustom = $.cookie('themerCustom') != null ? $.parseJSON($.cookie('themerCustom')) : themerCustomDefault;

if (themerThemes.length != themerCustom.length)
{
	$.each(themerThemes, function(k,v){ if (typeof themerCustom[k] == 'undefined') themerCustom[k] = v; });
	$.cookie('themerCustom', JSON.stringify(themerCustom));
}

$(function()
{
	if ($('#themer').length)
	{
		var themerOpened = $.cookie('themerOpened') ? $.cookie('themerOpened') : 0;
		
		$('#themer')
			.on('shown', function(){ $.cookie('themerOpened', 1); })
			.on('hidden', function(){ $.cookie('themerOpened', 0); });
		
		$('#themer .close2').on('click', function(){
			$('#themer').collapse('hide');
		});
		
		if (themerOpened == 1)
			$('#themer').collapse('show');
		
		$("#themer-primary-cp")
			.attr('data-default', themerPrimaryColor)
			.on('change', function(){
				var input = $(this),
				hex = input.val();
				if (hex) updatePrimaryColor(hex, true, true);
			});
		
		var themeSelect = $('#themer-theme');
		$.each(themerThemes, function( i, p ) {
			if (p.visible === true)
			{
				var option = $("<option></option>").text(p.name).val(i);
				themeSelect.append(option);
			}
		});
		themeSelect.on('change', function(e) 
		{
			e.preventDefault();
			updateTheme(themeSelect.val());
		});
		
		$('#themer-getcode-less').click(function(e){
			e.preventDefault();
			themerGetCode(true);
		});
		
		$('#themer-getcode-css').click(function(e){
			e.preventDefault();
			themerGetCode();
		});
		
		$('#themer-custom-reset').click(function()
		{
			themerCustom[themerSelectedTheme].primaryColor = null;
			
			$.cookie('themerCustom', JSON.stringify(themerCustom));
			updateTheme(themerSelectedTheme);
		});
		
		$('#themer-advanced-toggle').on('change', function()
		{
			$.cookie('themerAdvanced', $(this).is(':checked') ? "1" : "0");
			themerAdvancedToggle();
		});
		
		if (themerAdvanced)
			$('#themer-advanced-toggle').prop('checked', true).trigger('change');
		
		updateTheme(themerSelectedTheme);
	}
});