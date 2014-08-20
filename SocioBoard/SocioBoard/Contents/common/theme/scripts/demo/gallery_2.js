/* ==========================================================
 * ErgoAdmin v1.2
 * gallery_2.js
 * 
 * http://www.mosaicpro.biz
 * Copyright MosaicPro
 *
 * Built exclusively for sale @Envato Marketplaces
 * ========================================================== */ 

function masonryGallery()
{
	var $container = $('.gallery-masonry ul');
	$container.each(function()
	{
		var c = $(this);
		
		if (c.is('.masonry'))
			c.masonry('reload');

		c.imagesLoaded( function()
		{
			c.masonry({
				gutterWidth: 9,
		    	itemSelector : 'li',
		    	columnWidth: c.find('li:first').width(),
		    	isAnimated: true,
		    	animationOptions: {
		    		duration: 250,
		    	    easing: 'linear',
		    	    queue: true
		    	}
		  	});
		});
	});
}

$(function()
{
	masonryGallery();

	$(window).resize(function(e){
		masonryGallery();
	});
});