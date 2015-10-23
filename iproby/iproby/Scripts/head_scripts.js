function show_personal_account() {
    $('.auth_box').css('display', 'none');
    $('.personal_account_box').css('display', 'block');
}
$( document ).ready(function() {
$('.dropdown-menu li').click(function () {
    $(this).parent().prev().html($(this).text().trim());
    $('#' + $(this).parent().attr('load')).val($(this).text().trim());
    
});
});

$(document).ajaxSuccess(function() {
    $('.dropdown-menu li').click(function () {
        $(this).parent().prev().html($(this).text().trim());
        $('#' + $(this).parent().attr('load')).val($(this).text().trim());
    });
});