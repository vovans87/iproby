$(function () {
    $('form').validator();
    //$("form").keyup(function () {
    //    $('form').validator('validate')
    //});
    $('form').submit(function (e) {
        if (e.isDefaultPrevented()) {
            //alert('32');
        } else {
            
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    $('.modal-footer').addClass('hide');
                    $('#result').html(result);
                    
                }
            });
        }
        

        return false;
    });
});

$('.modal').modal();
$('.registration').click(function () {
    location.href = "/Authorization/Registration";
})
$('.enter').click(function () {
    location.href = "/Authorization/Login";
})

//$(function () {
//    $('[data-toggle="tooltip"]').tooltip()
//})

//$(function () {
//    $("form #InputLoginRegist").keyup(function () {
//        alert($(this).serialize());
//        $.ajax({
//            url: "/status/checklogin",
//            type: "post",
//            data: $(this).serialize(),
//            success: function (result) {
//                //$('#result').html(result);
                
//                if (result == "False") {
//                    $("form #InputLoginRegist").parent().addClass("has-error");
//                    $("form .form-control-feedback").addClass("glyphicon glyphicon-remove");
//                    $(".login_check").removeClass("hide");
//                } else {
//                    $("form .form-control-feedback").removeClass("glyphicon glyphicon-remove");
//                    $("form #InputLoginRegist").parent().removeClass("has-error");
//                    $("form #InputLoginRegist").parent().addClass("has-success");
//                    $("form .form-control-feedback").addClass("glyphicon glyphicon-ok");
//                    $(".login_check").addClass("hide");
//                }
               
//            }
//        });
//    });
//});

$(function () {
   
    $.mask.definitions['*'] = '[A-Z,a-z,0-9,_,$,~,#]';
    $.mask.definitions['~'] = '[А-Я,а-я,0-9,\,,\.," "]';
    $('#inputLogin').mask('**?******************', { placeholder: "" });
    $('#InputPassword').mask('**?******************', { placeholder: "" });
    $('#InputFio').mask('~~~?~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~', { placeholder: "" });
    $('#address').mask('~~~?~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~', { placeholder: "" });
    
    $(".phone").mask('+7 (999) 999-99-99');
});


//$(function () {
//    $("#test").validate({
//    rules: {
//        login: { required: true },
//        password: { required: true },
//        email: { email: true, required: true }
//    },
//    messages: {
//        email: "Just check the box<h5 class='text-danger'>You aren't going to read the EULA</h5>"
//    },
//    tooltip_options: {
//        password: { trigger: 'focus' },
//        login: { placement: 'right', html: true }
//    },
//});
//});