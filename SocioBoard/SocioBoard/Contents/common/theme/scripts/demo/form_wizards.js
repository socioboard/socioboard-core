/* ==========================================================
 * ErgoAdmin v1.2
 * form_wizards.js
 * 
 * http://www.mosaicpro.biz
 * Copyright MosaicPro
 *
 * Built exclusively for sale @Envato Marketplaces
 * ========================================================== */ 

$(function()
{
	var bWizardTabClass = '';
	$('.wizard').each(function()
	{
		if ($(this).is('#rootwizard'))
			bWizardTabClass = 'bwizard-steps';
		else
			bWizardTabClass = '';

		var wiz = $(this);
		
		$(this).bootstrapWizard(
		{
			onNext: function(tab, navigation, index) 
			{
				if(index==1)
				{
					// Make sure we entered the title
					if(!wiz.find('#inputTitle').val()) {
						alert('You must enter the product title');
						wiz.find('#inputTitle').focus();
						return false;
					}
				}
			}, 
			onLast: function(tab, navigation, index) 
			{
				// Make sure we entered the title
				if(!wiz.find('#inputTitle').val()) {
					alert('You must enter the product title');
					wiz.find('#inputTitle').focus();
					return false;
				}
			}, 
			onTabClick: function(tab, navigation, index) 
			{
				// Make sure we entered the title
				if(!wiz.find('#inputTitle').val()) {
					alert('You must enter the product title');
					wiz.find('#inputTitle').focus();
					return false;
				}
			},
			onTabShow: function(tab, navigation, index) 
			{
				var $total = navigation.find('li:not(.status)').length;
				var $current = index+1;
				var $percent = ($current/$total) * 100;
				
				if (wiz.find('.bar').length)
				{
					wiz.find('.bar').css({width:$percent+'%'});
					wiz.find('.bar')
						.find('.step-current').html($current)
						.parent().find('.steps-total').html($total)
						.parent().find('.steps-percent').html(Math.round($percent) + "%");
				}
				
				// update status
				if (wiz.find('.step-current').length) wiz.find('.step-current').html($current);
				if (wiz.find('.steps-total').length) wiz.find('.steps-total').html($total);
				if (wiz.find('.steps-complete').length) wiz.find('.steps-complete').html(($current-1));
				
				// mark all previous tabs as complete
				navigation.find('li:not(.status)').removeClass('primary');
				navigation.find('li:not(.status):lt('+($current-1)+')').addClass('primary');
	
				// If it's the last tab then hide the last button and show the finish instead
				if($current >= $total) {
					wiz.find('.pagination .next').hide();
					wiz.find('.pagination .finish').show();
					wiz.find('.pagination .finish').removeClass('disabled');
				} else {
					wiz.find('.pagination .next').show();
					wiz.find('.pagination .finish').hide();
				}
			},
			tabClass: bWizardTabClass,
			nextSelector: '.next', 
			previousSelector: '.previous',
			firstSelector: '.first', 
			lastSelector: '.last'
		});

		wiz.find('.finish').click(function() 
		{
			alert('Finished!, Starting over!');
			wiz.find("a[data-toggle*='tab']:first").trigger('click');
		});
	});
});