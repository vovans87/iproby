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

$(document).ajaxSuccess(function() {
    $('.dropdown-menu li').click(function () {
        $(this).parent().parent().children('.btn').html($(this).text().trim());
        $('.' + $(this).parent().attr('load')).val($(this).text().trim());
        if ($(this).text().trim().indexOf("Выберите") == -1) {
            
            
            //tinymce.init({
            //    selector: "#description",
            //    height: 200,
            //    menubar: false,
            //    statusbar: false
            //});
        };
    });
    
});

