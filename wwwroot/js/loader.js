$(window).on('load', function (event) {
	$('body').removeClass('preloading');
	$('.loader').delay(100).fadeOut('fast');
	$(this.window).ready();
});