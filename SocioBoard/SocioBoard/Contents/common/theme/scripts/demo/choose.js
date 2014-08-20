/* ==========================================================
 * ErgoAdmin v1.2
 * choose.js
 * 
 * http://www.mosaicpro.biz
 * Copyright MosaicPro
 *
 * Built exclusively for sale @Envato Marketplaces
 * ========================================================== */ 

$(function()
{
	$('#choose-preview')
		.find('.options select')
		.on('change', function()
		{
			var box = $(this).parents('.box:first');
			var select_layout = box.find('select[data-type="layout"]');
			var select_menu = box.find('select[data-type="menu"]');
			
			box.find('.actions a').removeClass('btn-active');
			$('#' + select_layout.val() + '-' + select_menu.val()).addClass('btn-active');
		});
});