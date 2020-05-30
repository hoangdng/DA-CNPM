$("#cb-danang").on("change", function () {
    $("#newsfeed").load(location.href + " #newsfeed>*");
})