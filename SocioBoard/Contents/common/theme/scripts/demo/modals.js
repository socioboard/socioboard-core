/* ==========================================================
 * ErgoAdmin v1.2
 * modals.js
 * 
 * http://www.mosaicpro.biz
 * Copyright MosaicPro
 *
 * Built exclusively for sale @Envato Marketplaces
 * ========================================================== */ 

$(function()
{
	$('#modals-bootbox-alert').click(function()
	{
		bootbox.alert("Hello World!", function(result) 
		{
			$.gritter.add({
				title: 'Callback!',
				text: "I'm just a BootBox Alert callback!"
			});
		});
	});
	$('#modals-bootbox-confirm').click(function()
	{
		bootbox.confirm("Are you sure?", function(result) 
		{
			$.gritter.add({
				title: 'Callback!',
				text: "BootBox Confirm Callback with result: "+ result
			});
		});
	});
	$('#modals-bootbox-prompt').click(function()
	{
		bootbox.prompt("What is your name?", function(result) 
		{                
			if (result === null) {                                             
				$.gritter.add({
					title: 'Callback!',
					text: "BootBox Prompt Dismissed!"
				});                            
			} else {
				$.gritter.add({
					title: 'Hi ' + result,
					text: "BootBox Prompt Callback with result: "+ result
				});                          
			}
		});
	});
	$('#modals-bootbox-custom').click(function()
	{
		bootbox.dialog("I am a custom dialog", [{
		    "label" : "Success!",
		    "class" : "btn-success",
		    "callback": function() {
		    	$.gritter.add({
					title: 'Callback!',
					text: "Great success"
				});
		    }
		}, {
		    "label" : "Danger!",
		    "class" : "btn-danger",
		    "callback": function() {
		    	$.gritter.add({
					title: 'Callback!',
					text: "Uh oh, look out!"
				});
		    }
		}, {
		    "label" : "Click ME!",
		    "class" : "btn-primary",
		    "callback": function() {
		    	$.gritter.add({
					title: 'Callback!',
					text: "Primary button!"
				});
		    }
		}, {
		    "label" : "Just a button..."
		}]);
	});
});