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
            "infoFiltered": "(filtered from _MAX_ total records)"
        }
    });

    $('#custom-filter').keyup( function() {
        table.search( this.value ).draw();
    } );
});