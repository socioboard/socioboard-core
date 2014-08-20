/* ==========================================================
 * ErgoAdmin v1.2
 * sliders.js
 * 
 * http://www.mosaicpro.biz
 * Copyright MosaicPro
 *
 * Built exclusively for sale @Envato Marketplaces
 * ========================================================== */ 

$(function()
{
	/* jQRangeSliders */
	
	// regular Range Slider
	$("#rangeSlider").rangeSlider();
	
	// edit Range Slider
	$("#rangeSliderEdit").editRangeSlider();
	
	// date Range Slider
	$("#rangeSliderDate").dateRangeSlider();
	
	// Range Slider without Arrows
    $("#rangeSliderWArrows").rangeSlider({ arrows: false });
    
    // Range Slider Formatter
    $("#rangeSliderFormatter").rangeSlider({
    	formatter:function(val){
    		var value = Math.round(val * 5) / 5,
    		decimal = value - Math.round(val);
    		return "$" + (decimal == 0 ? value.toString() + ".0" : value.toString());
    	}
    });
    
    // Range Slider Ruler
    $("#rangeSliderRuler").rangeSlider({
    	scales: [
	         // Primary scale
	         {
	        	 first: function(val){ return val; },
	        	 next: function(val){ return val + 10; },
	        	 stop: function(val){ return false; },
	        	 label: function(val){ return val; },
	        	 format: function(tickContainer, tickStart, tickEnd){ 
	        		 tickContainer.addClass("myCustomClass");
	        	 }
	         },
	         // Secondary scale
	         {
	        	 first: function(val){ return val; },
	        	 next: function(val){
	        		 if (val % 10 === 9){
	        			 return val + 2;
	        		 }
	        		 return val + 1;
	        	 },
	        	 stop: function(val){ return false; },
	        	 label: function(){ return null; }
	         }]
    });
    
    // Date Range Slider Ruler
    var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec"];
    $("#rangeSliderRulerDate").dateRangeSlider(
    {
    	bounds: {min: new Date(2012, 0, 1), max: new Date(2012, 11, 31, 12, 59, 59)},
    	defaultValues: {min: new Date(2012, 1, 10), max: new Date(2012, 4, 22)},
    	scales: [{
    		first: function(value){ return value; },
    		end: function(value) {return value; },
    		next: function(value){
    			var next = new Date(value);
    			return new Date(next.setMonth(value.getMonth() + 1));
    		},
    		label: function(value){
    			return months[value.getMonth()];
    		},
    		format: function(tickContainer, tickStart, tickEnd){
    			tickContainer.addClass("myCustomClass");
    		}
    	}]
    });
    
    // Range Slider Step
    $("#rangeSliderStep").rangeSlider({step: 10});
    
    // Range Slider Wheel Zoom
    $("#rangeSliderWheelZoom").rangeSlider({wheelMode: "zoom"});
    
    // Range Slider Wheel Scroll
    $("#rangeSliderWheelScroll").rangeSlider({wheelMode: "scroll", wheelSpeed: 30});
    
    /*
	 * JQueryUI Slider: Default slider
	 */
	if ($('.slider-single').size() > 0)
	{
		$( ".slider-single" ).slider({
			create: JQSliderCreate,
			value: 10,
	        animate: true,
	        start: function() { if (typeof mainYScroller != 'undefined') mainYScroller.disable(); },
	        stop: function() { if (typeof mainYScroller != 'undefined') mainYScroller.enable(); }
	    });
	}
	
	/*
	 * JQueryUI Slider: Multiple Vertical Sliders
	 */
	$( ".sliders-vertical > span" ).each(function() 
	{
        var value = parseInt( $( this ).text(), 10 );
        $( this ).empty().slider({
        	create: JQSliderCreate,
            value: value,
            range: "min",
            animate: true,
            orientation: "vertical",
            start: function() { if (typeof mainYScroller != 'undefined') mainYScroller.disable(); },
	        stop: function() { if (typeof mainYScroller != 'undefined') mainYScroller.enable(); }
        });
    });
	
	/*
	 * JQueryUI Slider: Range Slider
	 */
	if ($('.range-slider').size() > 0)
    {
		$( ".range-slider .slider" ).slider({
			create: JQSliderCreate,
	        range: true,
	        min: 0,
	        max: 500,
	        values: [ 75, 300 ],
	        slide: function( event, ui ) {
	            $( ".range-slider .amount" ).val( "$" + ui.values[ 0 ] + " - $" + ui.values[ 1 ] );
	        },
	        start: function() { if (typeof mainYScroller != 'undefined') mainYScroller.disable(); },
	        stop: function() { if (typeof mainYScroller != 'undefined') mainYScroller.enable(); }
	    });
    	$( ".range-slider .amount" ).val( "$" + $( ".range-slider .slider" ).slider( "values", 0 ) +
    			" - $" + $( ".range-slider .slider" ).slider( "values", 1 ) );
    }
	
	/*
	 * JQueryUI Slider: Snap to Increments
	 */
	if ($('.increments-slider').size() > 0)
    {
		$( ".increments-slider .slider" ).slider({
			create: JQSliderCreate,
			value:100,
	        min: 0,
	        max: 500,
	        step: 50,
	        slide: function( event, ui ) {
	            $( ".increments-slider .amount" ).val( "$" + ui.value );
	        },
	        start: function() { if (typeof mainYScroller != 'undefined') mainYScroller.disable(); },
	        stop: function() { if (typeof mainYScroller != 'undefined') mainYScroller.enable(); }
	    });
		$( ".increments-slider .amount" ).val( "$" + $( ".increments-slider .slider" ).slider( "value" ) );
    }
	
	/*
	 * JQueryUI Slider: Vertical Range Slider
	 */
	if ($('.vertical-range-slider').size() > 0)
    {
		$( ".vertical-range-slider .slider" ).slider({
			create: JQSliderCreate,
			orientation: "vertical",
	        range: true,
	        min: 0,
	        max: 500,
	        values: [ 100, 400 ],
	        slide: function( event, ui ) {
	            $( ".vertical-range-slider .amount" ).val( "$" + ui.values[ 0 ] + " - $" + ui.values[ 1 ] );
	        },
	        start: function() { if (typeof mainYScroller != 'undefined') mainYScroller.disable(); },
	        stop: function() { if (typeof mainYScroller != 'undefined') mainYScroller.enable(); }
	    });
    	$( ".vertical-range-slider .amount" ).val( "$" + $( ".vertical-range-slider .slider" ).slider( "values", 0 ) +
    			" - $" + $( ".vertical-range-slider .slider" ).slider( "values", 1 ) );
    }
	
	/*
	 * JQueryUI Slider: Range fixed minimum
	 */
	if ($('.slider-range-min').size() > 0)
	{
		$( ".slider-range-min .slider" ).slider({
			create: JQSliderCreate,
            range: "min",
            value: 150,
            min: 1,
            max: 700,
            slide: function( event, ui ) {
                $( ".slider-range-min .amount" ).val( "$" + ui.value );
            },
            start: function() { if (typeof mainYScroller != 'undefined') mainYScroller.disable(); },
	        stop: function() { if (typeof mainYScroller != 'undefined') mainYScroller.enable(); }
        });
        $( ".slider-range-min .amount" ).val( "$" + $( ".slider-range-min .slider" ).slider( "value" ) );
	}
	
	/*
	 * JQueryUI Slider: Range fixed maximum
	 */
	if ($('.slider-range-max').size() > 0)
	{
		$( ".slider-range-max .slider" ).slider({
			create: JQSliderCreate,
            range: "max",
            min: 1,
            max: 700,
            value: 150,
            slide: function( event, ui ) {
                $( ".slider-range-max .amount" ).val( "$" + ui.value );
            },
            start: function() { if (typeof mainYScroller != 'undefined') mainYScroller.disable(); },
	        stop: function() { if (typeof mainYScroller != 'undefined') mainYScroller.enable(); }
        });
        $( ".slider-range-max .amount" ).val( "$" + $( ".slider-range-max .slider" ).slider( "value" ) );
	}
});