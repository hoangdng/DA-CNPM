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

$(".form-element").on("input", function () {
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

//Chua xong.....................
$(".save-button").on('click', function (event) {
    event.preventDefault();
    var form = $(".form-savepost").serialize();
    $.ajax({
        type: 'POST',
        url: "/Users/SavePost",
        data: form,
        success: function (data) {
            //$("#newsfeed").html(data);
            //$("#main-content").load(location.href + " #newsfeed");
            toastr.success("Lưu bài thành công");
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-top-center",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "100",
                "hideDuration": "1000",
                "timeOut": "200",
                "extendedTimeOut": "200",
                "showEasing": "swing",
                "hideEasing": "swing",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
        }
    });
});


