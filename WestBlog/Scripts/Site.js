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
        var ischeck = $(this).is(':checked');
        if (!ischecked){
            $('#reasonRemoved').show();
        }
    })

});