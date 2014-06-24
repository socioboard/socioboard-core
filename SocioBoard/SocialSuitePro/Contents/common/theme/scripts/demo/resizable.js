/* ==========================================================
 * ErgoAdmin v1.2
 * resizable.js
 * 
 * http://www.mosaicpro.biz
 * Copyright MosaicPro
 *
 * Built exclusively for sale @Envato Marketplaces
 * ========================================================== */ 

/* Utility functions */

// reset resizable menu to default: temporary (optional)
function resetResizableMenu(temp)
{
	$('#menu, #content').attr('style', '');
	if (temp !== true)
	{
		$.cookie('menuWidth', null);
		$.cookie('menuWidthDif', null);
	}
}

// toggle back to the last resized sidebar
function lastResizableMenuPosition()
{
	if ($.cookie('menuWidth') && $.cookie('menuWidthDif'))
	{
		$('#menu').css({ 'width': parseInt($.cookie('menuWidth')) + "px" });
		$('.menu-left #content').css({ 'marginLeft': parseInt($.cookie('menuWidth')) + "px" });
		$('.menu-right #content').css({
			'marginRight': parseInt($.cookie('menuWidth')),
			'left': "-=" + parseInt($.cookie('menuWidthDif'))
		});
	}
	if (!$('.container-fluid:first').is('.menu-hidden') && $('#menu').is('.hidden-phone'))
		$('#menu').removeClass('hidden-phone');
}

$(function()
{
	/* Resizable Sidebar */
	if (!Modernizr.touch)
	{
		$( "#menu" ).resizable({
			animate: false,
			helper: "ui-resizable-helper menu",
			handles: "e, w",
			maxWidth: 260,
			minWidth: 228,
			stop: function(event, ui)
			{
				if (typeof toggleResizableWhileTourOpen != 'undefined')
					toggleResizableWhileTourOpen(this, true);
			},
			resize: function(event, ui)
			{
				if ($(this).is(".ui-state-disabled"))
				{
					ui.size.width = ui.originalSize.width;
					ui.size.height = ui.originalSize.height;
					return false;
				}
				
				var dif = ui.size.width - ui.originalSize.width;
				$('.menu-left #content').css({ 'marginLeft': ui.size.width + "px" });
				$('.menu-right #content').css({ 
					'marginRight': ui.size.width,
					'left': "-=" + dif
				});
				
				$.cookie('menuWidth', ui.size.width);
				$.cookie('menuWidthDif', dif);
			}
		});
	}
});