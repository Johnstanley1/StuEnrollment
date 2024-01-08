$(document).ready(function () {
    $('input[type = "datetime"]').datepicker({
        dateFormat: 'mm/dd/yy',
        changeMonth: true,
        changeYear: true,
        yearRange: '-60: +60'
    })
})