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
