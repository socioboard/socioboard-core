/* ==========================================================
 * ErgoAdmin v1.2
 * guidedtour.js
 * 
 * http://www.mosaicpro.biz
 * Copyright MosaicPro
 *
 * Built exclusively for sale @Envato Marketplaces
 * ========================================================== */ 

// handle menu resizable functionality while guided tour is open
function toggleResizableWhileTourOpen(resizable, alert)
{
	if ($('body').is('.tlypageguide-open') && alert === true)
	{
		alertWhileTourOpen();
	}
	if ($('body').is('.tlypageguide-open') && !$(resizable).is(".ui-state-disabled"))
	{
		resetResizableMenu();
		$(resizable).addClass( "ui-state-disabled" );
	}
	if (!$('body').is('.tlypageguide-open') && $(resizable).is(".ui-state-disabled"))
	{
		$(resizable).removeClass( "ui-state-disabled" );
	}
	if ($('body').is('.tlypageguide-open') && $(resizable).is(".ui-state-disabled"))
	{
		setTimeout(function(){
			toggleResizableWhileTourOpen(resizable);
		}, 1000);
	}
}

// handle menu toggle button while guided tour is open
function toggleMenuButtonWhileTourOpen(alert)
{
	if ($('body').is('.tlypageguide-open') && alert === true)
	{
		alertWhileTourOpen();
	}
	if ($('body').is('.tlypageguide-open') && !$('.navbar.main .btn-navbar').is(".ui-state-disabled"))
	{
		$('.navbar.main .btn-navbar').addClass( "ui-state-disabled" );
	}
	if (!$('body').is('.tlypageguide-open') && $('.navbar.main .btn-navbar').is(".ui-state-disabled"))
	{
		$('.navbar.main .btn-navbar').removeClass( "ui-state-disabled" );
	}
	if ($('body').is('.tlypageguide-open') && $('.navbar.main .btn-navbar').is(".ui-state-disabled"))
	{
		setTimeout(function(){
			toggleMenuButtonWhileTourOpen();
		}, 1000);
	}
	return $('.navbar.main .btn-navbar').is( ".ui-state-disabled" );
}

//handle limited functionality alerts while guided tour open
function alertWhileTourOpen()
{
	$.gritter.add({
		title: 'Ouch!',
		text: 'Part of the functionality is disabled during the guided tour. To enable all functionality, please close the tour.',
		before_open: function()
		{
            if ($('.gritter-item-wrapper').length == 1)
            	return false;
        },
        time: 5000,
        class_name: 'gritter-primary'
	});
}

$(function()
{
	if (!$('#tlyPageGuide').length)
		return false;
	
	tl.pg.init({
		custom_open_button: '#guided-tour #open-tour'
	});
	
	// nice loading animation for tour button
	$('#guided-tour').animate({ right: 0 }, 1000);
	
	// remove open tour button
	$('#guided-tour #close-tour').click(function(){
		$(this).parent().animate({ right: '-100%' }, function(){
			$(this).remove();
		});
	});
	
	// additional functionality when opening the guided tour
	$('#guided-tour #open-tour').click(function()
	{
		if ($('body').is('.tlypageguide-open'))
			return;
		
		if ($('.container-fluid:first').is('.menu-hidden'))
			toggleMenuHidden();
		
		if ($('.container-fluid:first').is('.menu-right'))
			$('#toggle-menu-position').prop('checked', false).trigger('change').parent().removeClass('checked');
	});
	
	// when fluid, toggle ribbon Guided Tour visibility
	if ($('#guided-tour').length)
	{
		$('.topnav.pull-right').hover(function(){
			if ($('.container-fluid:first').is('.fixed'))
				return false;
			$('#guided-tour').stop(true, true).fadeOut();
		}, function(){
			if ($('.container-fluid:first').is('.fixed'))
				return false;
			$('#guided-tour').stop(true, true).fadeIn();
		});
	}
});