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
                    //$('.modal-footer').addClass('hide');
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

$(function () {
   
    $.mask.definitions['*'] = '[A-Z,a-z,0-9,_,$,~,#]';
    $.mask.definitions['~'] = '[А-Я,а-я,0-9,\,,\.," "]';
    $('#inputLogin').mask('**?******************', { placeholder: "" });
    $('#InputPassword').mask('**?******************', { placeholder: "" });
    $('#InputFio').mask('~~~?~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~', { placeholder: "" });
    $('#address').mask('~~~?~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~', { placeholder: "" });
    
    $(".phone").mask('+7 (999) 999-99-99');
});

$(function () {
    $('.parent_type li').click(function () {
        if ($(this).text().trim().indexOf('Образование') > -1) {
            $(".education").css("display", "block");
        } else {
            $(".education").css("display", "none");
        };
       
    
        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).attr('name') + "=" + $(this).text().trim(),
          
            success: function (result) {
                //$('.modal-footer').addClass('hide');
                obj = JSON.parse(result);
                $(".child_type").prev().html('Выберите подраздел: <span class="caret" style="float:right;margin-top:10px;"></span>');
                $(".child_type li").remove();
                obj.forEach(function (entry) {
                    var text = '<li style="width: 368px;"><a href="#">' + entry + "</a></li>";
                    $(".child_type").append(text);
                });
               
               
            }
        });
       
    });

})




