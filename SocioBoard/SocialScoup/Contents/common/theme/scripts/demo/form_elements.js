/* ==========================================================
 * ErgoAdmin v1.2
 * form_elements.js
 * 
 * http://www.mosaicpro.biz
 * Copyright MosaicPro
 *
 * Built exclusively for sale @Envato Marketplaces
 * ========================================================== */ 

$(function()
{
	// button state demo
	$('#btn-loading')
	    .click(function () {
	        var btn = $(this)
	        btn.button('loading')
	        setTimeout(function () {
	            btn.button('reset')
	        }, 3000)
	    });
	
/* Select2 - Advanced Select Controls */
	
	// Basic
	$('#select2_1').select2();
	
	// Multiple
	$('#select2_2').select2();
	
	// Placeholders
	$("#select2_3").select2({
		placeholder: "Select a State",
		allowClear: true
	});
	$("#select2_4").select2({
	    placeholder: "Select a State",
	    allowClear: true
	});
	
	// tagging support
	$("#select2_5").select2({tags:["red", "green", "blue"]});
	
	// enable/disable mode
	$("#select2_6_1").select2();
	$("#select2_6_2").select2();
	$("#select2_6_enable").click(function() { $("#select2_6_1,#select2_6_2").select2("enable"); });
	$("#select2_6_disable").click(function() { $("#select2_6_1,#select2_6_2").select2("disable"); });
	
	// templating
	function format(state) {
	    if (!state.id) return state.text; // optgroup
	    return "<img class='flag' src='http://ivaynberg.github.com/select2/images/flags/" + state.id.toLowerCase() + ".png'/>" + state.text;
	}
	$("#select2_7").select2({
	    formatResult: format,
	    formatSelection: format,
	    escapeMarkup: function(m) { return m; }
	});
	
	/* DateTimePicker */
	
	// default
	$("#datetimepicker1").datetimepicker({
		format: 'yyyy-mm-dd hh:ii',
		startDate: "2013-02-14 10:00"
	});
	
	// component
	$('#datetimepicker2').datetimepicker({
		format: "dd MM yyyy - hh:ii",
		startDate: "2013-02-14 10:00"
	});
	
	// positioning
	$('#datetimepicker3').datetimepicker({
		format: "dd MM yyyy - hh:ii",
        autoclose: true,
        todayBtn: true,
        startDate: "2013-02-14 10:00",
        pickerPosition: "bottom-left"
	});
	
	// advanced
	$('#datetimepicker4').datetimepicker({
		format: "dd MM yyyy - hh:ii",
        autoclose: true,
        todayBtn: true,
        startDate: "2013-02-14 10:00",
        minuteStep: 10
	});
	
	// meridian
	$('#datetimepicker5').datetimepicker({
		format: "dd MM yyyy - HH:ii P",
	    showMeridian: true,
	    autoclose: true,
	    startDate: "2013-02-14 10:00",
	    todayBtn: true
	});
	
});