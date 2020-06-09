if ($(window).width() > 992)
{
    $(window).scroll(function ()
    {
        if ($(this).scrollTop() > 100)
        {
            $('#headerFixed').addClass("sticky-top");
        } else
        {
            $('#headerFixed').removeClass("sticky-top");
        }
    });
} 