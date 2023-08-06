
$(document).ready(function () {
    $('.datepicker').datepicker({
        format: 'yyyy-mm-dd',
        autoclose: true
    });

    $('.cleardatepicker').click(function () {
        $(this).prev('input:text').val('');
    });
});