$(document).ready(function () {

    $('.form-control').change(darkText);
    function darkText() {
        $(this).css('color', '#222');
    }

    $('#create').click(verifySubmit);
    $('#delete').click(verifySubmit);
    function verifySubmit() {
        $('#verify').show();
    }

});