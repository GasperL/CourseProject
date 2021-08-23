$(document).ready(function(){
    const table = $('#person_listing').DataTable({
        "scrollCollapse": true,
        "paging": true,
        "pagingType": "numbers",
        "lengthChange": false,
        "language": {
            "lengthMenu": "Display _MENU_ records per page",
            "zeroRecords": "Ничего не найдено",
            "info": "Страница _PAGE_ из _PAGES_",
            "infoEmpty": "Не найдено ни одной записи",
            "infoFiltered": ""
        }
    });

    $('#custom-filter').keyup( function() {
        table.search( this.value ).draw();
    } );
});


function readURL(input) {
    if (input.files && input.files[0]) {
        const reader = new FileReader();
        reader.onload = function (e) {
            $('#imageResult')
                .attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

$(function () {
    $('#upload').on('change', function () {
        readURL(input);
    });
});

function dynamicValue(input, card){
    const node = $(input).on("input", function() {
        if (input.type === "range"){
            $(card).text("$" + node.val().toString())
        }
        else{
            $(card).text(node.val().toString())
        }
    });
}