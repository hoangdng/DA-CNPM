// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Write your JavaScript code.
$('#myModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget); // Button that triggered the modal
    var title = button.data('title');// Extract info from data-* attributes
    var description = button.data('description');
    var image = button.data('image');
    var user = button.data('user');
    var date = button.data('date');
    // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
    // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
    var modal = $(this);
    modal.find('.modal-title').text(title);
    modal.find('.modal-body #description').text(description);
    modal.find('.modal-footer #user').text(user);
    modal.find('.modal-footer #date').text(date);
    modal.find('.modal-body img').attr("src", image);
});

$('#create-form').submit(function () {
    toastr.success("Đăng bài thành công");
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-center",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "swing",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
});

