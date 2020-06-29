$(window).on('load', function (event) {
	$('body').removeClass('preloading');
	$('.loader').delay(200).fadeOut('fast');
	$(this.window).ready();
});