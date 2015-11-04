$('.registration').click(function () {
    load_registration();
})
$('.login_btn').click(function () {
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

$(function () {
    $('.find_clients_btn').click(function () {
        $('#add_clients').modal({
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
                        //    $('.return_wait').html('<div style="height:150px;width:100%;text-align:center;"> <br><br><h4 class="modal-title"> <span class="glyphicon glyphicon-time">  </span>  Пожалуйста, подождите... </h4><div class="progress" style="width:50%;margin:0 auto;"><div class="progress-bar progress-bar-info progress-bar-striped active" style="width:100%"></div><br/><br/></div></div>')
                        $('.loading-wait-btn').button('loading');
                    },
                    success: function (result) {
                        //$('.modal-footer').addClass('hide');
                        $('.return_result').html(result);
                        setTimeout(function () {
                            locatio.reload();
                        }, 3000)
                    }
                });
            }
            return false;
        });
     });

    $('.find_workers_btn').click(function () {
         $('#add_workers').modal({
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
                        //    $('.return_wait').html('<div style="height:150px;width:100%;text-align:center;"> <br><br><h4 class="modal-title"> <span class="glyphicon glyphicon-time">  </span>  Пожалуйста, подождите... </h4><div class="progress" style="width:50%;margin:0 auto;"><div class="progress-bar progress-bar-info progress-bar-striped active" style="width:100%"></div><br/><br/></div></div>')
                        $('.loading-wait-btn').button('loading');
                    },
                    success: function (result) {
                        //$('.modal-footer').addClass('hide');
                        $('.return_result').html(result);
                        setTimeout(function () {
                            locatio.reload();
                        }, 3000)
                    }
                });
            }
            return false;
        });

    })


});
function load_add_clients() {
    $('#myContent').load("/Content/dialogs/AddClients.html", function () {
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
                        $('.loading-wait-btn').button('loading');
                    },
                    success: function (result) {
                        $('.loading-wait-btn').button('loading');
                        $('.return_result').html(result);
                    }
                });
            }
            return false;
        });
       
    });
};

function load_add_workers() {
    $('#myContent').load("/Content/dialogs/AddWorkers.html", function () {
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
                        $('.loading-wait-btn').button('loading');
                    },
                    success: function (result) {
                        $('.loading-wait-btn').button('loading');
                        $('.return_result').html(result);
                    }
                });
            }
            return false;
        });

    });
};

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
                    //    $('.return_wait').html('<div style="height:150px;width:100%;text-align:center;"> <br><br><h4 class="modal-title"> <span class="glyphicon glyphicon-time">  </span>  Пожалуйста, подождите... </h4><div class="progress" style="width:50%;margin:0 auto;"><div class="progress-bar progress-bar-info progress-bar-striped active" style="width:100%"></div><br/><br/></div></div>')
                        $('.loading-wait-btn').button('loading');
                    },
                    success: function (result) {
                        //$('.modal-footer').addClass('hide');
                        $('.loading-wait-btn').button('loading');
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
                    //    $('.return_wait').html('<div style="height:150px;width:100%;text-align:center;"> <br><br><h4 class="modal-title"> <span class="glyphicon glyphicon-time">  </span>  Пожалуйста, подождите... </h4><div class="progress" style="width:50%;margin:0 auto;"><div class="progress-bar progress-bar-info progress-bar-striped active" style="width:100%"></div><br/><br/></div></div>')
                        $('.loading-wait-btn').button('loading');
                    },
                    success: function (result) {
                        //$('.modal-footer').addClass('hide');
                        $('.loading-wait-btn').button('reset');
                        $('.return_result').html(result);
                        setTimeout(function () {
                            location.reload();
                        }, 5000)
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


function load_write_message(customer_id) {
    $('#myContent').load("/Content/dialogs/WriteMessage.html", function () {
        $('#myDialog').modal({
            backdrop: 'static',
            keyboard: true
        }, 'show');
        
        $('.to_customer_input').val(customer_id);
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
                        $('.loading-wait-btn').button('loading');
                    },
                    success: function (result) {
                        $('.loading-wait-btn').button('reset');
                        $('.return_result').html(result);
                        setTimeout(function () {
                            location.reload();
                        }, 5000)
                    }
                });
            }
            return false;
        });
        
    });
};

function getQueryVariable(variable) {
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] == variable) {
            return pair[1];
        } else {
            return 'no_param';
        }
    }
}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

$(function () {
    if (getParameterByName('target').indexOf('clients') > -1) {
        $('.spros').css('background-image', 'url(/Content/img/announ_target_ico.png)');
        $('.spros').css('color', 'white');
        $('.predlojenie').css('color', '#115a56');
        $('.predlojenie').css('background-image', 'none');
    }
});
$(function () {
    $('.send_rss_mail').click(function () {
        var pattern = /^([a-z0-9_\.-])+@[a-z0-9-]+\.([a-z]{2,4}\.)?[a-z]{2,4}$/;
        if (pattern.test($('.send_rss_mail_input').val())) { 
            $.ajax({
                url: "/Information/SendMail",
                type: "post",
                data: "email=" + $('.send_rss_mail_input').val(),
                success: function (result) {
                    alert("Ваш email успешно добавлен в почтовую рассылку. Спасибо!");
                }
            });
        } else{
            alert("Введенный email не соответствует критериям подлинности.Проверьте email.");
        }
    });
});

$(function () {
    $('.find_btn').click(function () {
        if (getQueryVariable('type_id').indexOf('no_param') == 0) {
            $.ajax({
                url: "/Announ/SearchResultAll",
                type: "post",
                data: "search_text=" + $('.search_text_input').val(),
                success: function (result) {
                    $('.search_result_block').html(result);
                }
            });
        } else {
            $.ajax({
                url: "/Announ/SearchResultInTypes",
                type: "post",
                data: "search_text=" + $('.search_text_input').val() + "&type_id=" + getQueryVariable('type_id'),
                success: function (result) {
                    $('.search_result_block').html(result);
                }
            });
        }
    });
    $('.message_author').click(function () {
        $(this).toggleClass("bg-success");
       
    });
    $('.write_message').click(function () {
        load_write_message($(this).attr('customer_id'));
    });

    $('.like_btn').click(function () {
        $.ajax({
            url: "/Announ/AddLike",
            type: "post",
            data: "announ_id=" + $(this).attr('announ_id'),
            success: function (result) {
               location.reload();
            }
        });
    });
    
    $('.dislike_btn').click(function () {
        $.ajax({
            url: "/Announ/AddDislike",
            type: "post",
            data: "announ_id=" + $(this).attr('announ_id'),
            success: function (result) {
                location.reload();
            }
        });
    });

    $('.spros').click(function () {
        $('.spros').css('background-image', 'url(/Content/img/announ_target_ico.png)');
        $('.spros').css('color', 'white');
        $('.predlojenie').css('color', '#115a56');
        $('.predlojenie').css('background-image', 'none');
        if (getParameterByName('type_id').length > 0) {
            location.href = '/Catalog?type_id=' + getParameterByName('type_id') + '&target=clients';
        } else {
            location.href = '?target=clients';
        }
    });

    $('.predlojenie').click(function () {
        $('.predlojenie').css('background-image', 'url(/Content/img/announ_target_ico.png)');
        $('.predlojenie').css('color', 'white');
        $('.spros').css('color', '#115a56');
        $('.spros').css('background-image', 'none');
        if (getParameterByName('type_id').length > 0) {
            location.href = '/Catalog?type_id=' + getParameterByName('type_id') + '&target=workers';
        } else {
            location.href = '?target=workers';
        }
    });
});
