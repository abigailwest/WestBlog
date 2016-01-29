$(document).ready(function () {

    $('.form-control').change(darkText);
    function darkText() {
        $(this).css('color', '#222');
    }

    $("button[name*='provider']").click(function () {
        return string.charAt(0).toUpperCase() + string.slice(1);
    })

    $('#create').click(verifySubmit);
    $('#delete').click(verifySubmit);
    function verifySubmit() {
        $('#verify').show();
    }

});