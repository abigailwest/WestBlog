$(document).ready(function () {

    $('.form-control').change(darkText);
    function darkText() {
        $(this).css('color', '#222');
    }

    $('#edit').click(verifySubmit);
    $('#create').click(verifySubmit);
    $('#delete').click(verifySubmit);
    function verifySubmit() {
        $('#verify').show();
    }

    $("#publish-check").change(function(){
        if (!$(this).is(':checked')) {
            $('#reasonRemoved').show();
        }
    })

    $("#media-check").change(function(){
        if ($(this).is(":checked")) {
            $("#media-change").show();
            $("#media-same").remove();
        }
    })


});