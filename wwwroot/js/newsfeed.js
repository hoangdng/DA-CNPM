$(".form-element").on("change", function () {
    var form = $("#filter-form").serialize();
    $.ajax({
        type: 'POST',
        url: "/Home/FilterIndex",
        data: form,
        success: function (data) {
            $('#newsfeed').html(data);
        }
    });
});


//$("#filter-form").submit(function (event) {
//    event.preventDefault();
//    var form = $("#filter-form").serialize();
//    $.ajax({
//        type: 'POST',
//        url: "/Home/FilterIndex",
//        data: form,
//        success: function (data) {
//            $('#newsfeed').html(data);
//        }
//    });
//});

