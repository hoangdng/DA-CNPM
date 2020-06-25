$(window).on('load', function (event) {
	$('body').removeClass('preloading');
	$('.loader').delay(1000).fadeOut('fast');
	$(this.window).ready();
});