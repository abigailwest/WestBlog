$(document).ready(function () {

    $('.form-control').change(blackText);
    function blackText() {
        $(this).css('color', 'black');
    }

});