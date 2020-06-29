$("#form-comment").on('submit', function (event) {
    event.preventDefault();
    var form = $("#form-comment").serialize();
    $.ajax({
        type: 'POST',
        url: "/Posts/AddComment",
        data: form,
        success: function (data) {
            $('#comments-box').html(data);
            $("#comments-box").load(location.href + " #all-comments");
            $('#textarea-comment').val('');
        }
    });
});

$("#form-report").on('submit', function (event) {
    event.preventDefault();
    var form = $("#form-report").serialize();
    $.ajax({
        type: 'POST',
        url: "/Reports/Create",
        data: form,
        success: function (data) {
            $('#myModal').modal('toggle');
            toastr.success("Gởi report thành công");
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-top-center",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "100",
                "hideDuration": "100",
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

//$(".report-button").on('click', function (event) {
//    toastr.success("Gởi report thành công");
//    toastr.options = {
//        "closeButton": false,
//        "debug": false,
//        "newestOnTop": false,
//        "progressBar": false,
//        "positionClass": "toast-top-center",
//        "preventDuplicates": false,
//        "onclick": null,
//        "showDuration": "100",
//        "hideDuration": "100",
//        "timeOut": "200",
//        "extendedTimeOut": "200",
//        "showEasing": "swing",
//        "hideEasing": "swing",
//        "showMethod": "fadeIn",
//        "hideMethod": "fadeOut"
//    }
//});
