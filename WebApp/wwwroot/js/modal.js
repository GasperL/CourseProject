(function ($) {
    const initialize = () => {
        $(".popup").on('click', function (e) {
            const url = $(this).data('url');
            $.get(url).done((data) => {
                const modalDiv = $('#modal-add');
                modalDiv.find(".modal-dialog").html(data);
                modalDiv.modal("show");
            });
        });
    }
    $(() => initialize());
}(jQuery));