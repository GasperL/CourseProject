(function (modals, $) {
    modals.openModal = function (input) {
        const url = $(input).data('url');
        $.get(url).done((data) => {
            const modalDiv = $('#modal-add');
            modalDiv.find(".modal-dialog").html(data);
            modalDiv.modal("show");
        });
    };
}(window.modals = window.modals ||  {}, jQuery));
