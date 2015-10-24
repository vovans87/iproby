$('.registration').click(function () {
    load_registration();
})
$('.enter').click(function () {
    load_authorization();
})


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

});


function load_registration() {
    $('#myContent').load("/Content/dialogs/Registration.html", function () {
        $('#myDialog').modal({
            backdrop: 'static',
            keyboard: true
        }, 'show');
        $('form').validator();
        $('form').submit(function (e) {
            if (e.isDefaultPrevented()) {
                //alert('32');
            } else {

                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    beforeSend: function () {
                        $('.return_wait').html('<div style="height:150px;width:100%;text-align:center;"> <br><br><h4 class="modal-title"> <span class="glyphicon glyphicon-time">  </span>  Пожалуйста, подождите... </h4><div class="progress" style="width:50%;margin:0 auto;"><div class="progress-bar progress-bar-info progress-bar-striped active" style="width:100%"></div><br/><br/></div></div>')

                    },
                    success: function (result) {
                        //$('.modal-footer').addClass('hide');
                        $('.return_result').html(result);

                    }
                });
            }
            return false;
        });
        add_mask();
    });
};

function add_mask() {

    $.mask.definitions['*'] = '[A-Z,a-z,0-9,_,$,~,#]';
    $.mask.definitions['~'] = '[А-Я,а-я,0-9,\,,\.," "]';
    $('#inputLogin').mask('**?******************', { placeholder: "" });
    $('#InputPassword').mask('**?******************', { placeholder: "" });
    $('#InputFio').mask('~~~?~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~', { placeholder: "" });
    $('#address').mask('~~~?~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~', { placeholder: "" });
    $(".phone").mask('+7 (999) 999-99-99');
};

function load_authorization() {
    $('#myContent').load("/Content/dialogs/Login.html", function () {
        $('#myDialog').modal({
            backdrop: 'static',
            keyboard: true
        }, 'show');
        $('form').validator();
        $('form').submit(function (e) {
            if (e.isDefaultPrevented()) {
                //alert('32');
            } else {
            
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    beforeSend: function () {
                        $('.return_wait').html('<div style="height:150px;width:100%;text-align:center;"> <br><br><h4 class="modal-title"> <span class="glyphicon glyphicon-time">  </span>  Пожалуйста, подождите... </h4><div class="progress" style="width:50%;margin:0 auto;"><div class="progress-bar progress-bar-info progress-bar-striped active" style="width:100%"></div><br/><br/></div></div>')
                        
                    },
                    success: function (result) {
                        //$('.modal-footer').addClass('hide');
                        $('.return_result').html(result);
                    
                    }
                });
            }
            return false;
        });
        $.mask.definitions['*'] = '[A-Z,a-z,0-9,_,$,~,#]';
        $('#InputLoginAuth').mask('**?******************', { placeholder: "" });
        $('#InputPasswordAuth').mask('**?******************', { placeholder: "" });
    });
};

