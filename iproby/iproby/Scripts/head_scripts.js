function show_personal_account() {
    $('.auth_box').css('display', 'none');
    $('.personal_account_box').css('display', 'block');
}
$( document ).ready(function() {
    $('.dropdown-menu li').click(function () {
        $(this).parent().parent().children('.btn').html($(this).text().trim());
     $('.' + $(this).parent().attr('load')).val($(this).text().trim());
    });
});
//$body = $("body");

//$.ajaxSetup({
//    beforeSend:function(){
//        console.log('send');
//    },
//    complete:function(){
//        // hide gif here, eg:
//        console.log('receive');
//    }
//});
$(document).ajaxSuccess(function() {
    $('.dropdown-menu li').click(function () {
        $(this).parent().parent().children('.btn').html($(this).text().trim());
        $('.' + $(this).parent().attr('load')).val($(this).text().trim());
        if ($(this).text().trim().indexOf("Выберите") == -1) {


        };
    });
    
});

