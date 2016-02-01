/// <reference path="D:\!PROJECTS\FromGitHub\iproby\iproby\iproby\Views/Announ/AddClients.cshtml" />
/// <reference path="D:\!PROJECTS\FromGitHub\iproby\iproby\iproby\Views/Announ/AddClients.cshtml" />
$('.registration').click(function () {
    load_registration();
})
$('.login_btn').click(function () {
    load_authorization();
})


$(function () {
    $('.find_clients_btn').click(function () {
        $.ajax({
            url: '/Announ/AddClients',
            type: 'GET',
            success: function (result) {
                $('#dialogAddClients').html(result);

                $('#add_clients').modal({
                    backdrop: 'static',
                    keyboard: true
                }, 'show');
                $('form').validator();
                $('form').submit(function (e) {
                    if (e.isDefaultPrevented()) {
                        //alert('32');
                    } else {
                        //tinymce.get('p').getContent();
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
                                $('.return_result_clients').html(result);
                                //setTimeout(function () {
                                //    locatio.reload();
                                //}, 3000)
                            }
                        });
                    }
                    return false;
                });
            }
        });

       
     });

    $('.find_workers_btn').click(function () {
        $.ajax({
            url: '/Announ/AddWorkers',
            type: 'GET',
            success: function (result) {
                $('#dialogAddClients').html(result);
             
                $('#add_workers').modal({
                    backdrop: 'static',
                    keyboard: true
                }, 'show');
               
                $('#add_workers form').validator();
                $('#add_workers form').submit(function (e) {
                    if (e.isDefaultPrevented()) {
                    // data: 'parent_type=' + $("input[name='parent_type']").val() + '&type=' + $("input[name='type']").val() + '&name=' + $("input[name='name']").val() + '&about=' + $("input[name='about']").val() + '&description=' + $("input[name='description']").val() + '&subjects=' + $("input[name='subjects']").val() + '&price=' + $("input[name='price']").val(),
                    } else {
                        
                        $.ajax({
                            url: '/Announ/AddWorkers',
                            type: 'POST',
                            data: $('#add_workers form').serialize(),
                            beforeSend: function () {
                                //    $('.return_wait').html('<div style="height:150px;width:100%;text-align:center;"> <br><br><h4 class="modal-title"> <span class="glyphicon glyphicon-time">  </span>  Пожалуйста, подождите... </h4><div class="progress" style="width:50%;margin:0 auto;"><div class="progress-bar progress-bar-info progress-bar-striped active" style="width:100%"></div><br/><br/></div></div>')
                                $('#add_workers .loading-wait-btn').button('loading');
                            },
                            success: function (result) {
                                $('#add_workers .return_result').html(result);
                                //setTimeout(function () {
                                //    location.reload();
                                //}, 3000)
                            }
                        });
                    }
                    return false;
                });
            }
         });
    });
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
        }, 'show').css({
            width: '660px',
           margin: '0 auto'
        });;
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
                        $('#myContent').load("/Content/dialogs/RegistrationStep2.html", function () {
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
                        //setTimeout(function () {
                        //    location.reload();
                        //}, 5000)
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
    if (getParameterByName('emailstatus').indexOf('confirmed') > -1) {
        $('#myContent').load("/Content/dialogs/ConfirmSuccess.html", function () {
            $('#myDialog').modal({
                backdrop: 'static',
                keyboard: true
            }, 'show');
        });
    } ;
    if (getParameterByName('emailstatus').indexOf('notconfirmed') > -1) {
        $('#myContent').load("/Content/dialogs/ConfirmFail.html", function () {
            $('#myDialog').modal({
                backdrop: 'static',
                keyboard: true
            }, 'show');
        });
    }
});
$(function () {
    $('.send_rss_mail').click(function () {
        var pattern = /^([a-z0-9_\.-])+@[a-z0-9-]+\.([a-z]{2,4}\.)?[a-z]{2,4}$/;
        if (pattern.test($('.send_rss_mail_input').val())) {
            $.ajax({
                url: "/Information/SendMailRss",
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
        if ($('.is_index_flag').val() == 1) {
            $.ajax({
                url: "/Announ/SearchResultAll",
                type: "post",
                data: "search_text=" + $('.search_text_input').val(),
                success: function (result) {
                    $('.search_result_block').html(result);
                    $('.search_text_info').text($('.search_text_input').val());
                    $('.write_message').click(function () {
                        load_write_message($(this).attr('data-customer_id'));
                    });
                }
            });
        } else {
            $.ajax({
                url: "/Announ/SearchResultInTypes",
                type: "post",
                data: "search_text=" + $('.search_text_input').val() + "&type_id=" + $('.announ_type_id').val(),
                success: function (result) {
                    $('.search_result_block').html(result);
                    $('.search_text_info').text($('.search_text_input').val());
                    $('.write_message').click(function () {
                        load_write_message($(this).attr('data-customer_id'));
                    });
                }
            });
        }
         
    });
    $('.message_author').click(function () {
        $(this).toggleClass("bg-success");
       
    });
    $('.write_message').click(function () {
        load_write_message($(this).attr('data-customer_id'));
    });

    $('.like_btn').click(function () {
        $(this).addClass('like_clicked');
        $.ajax({
            url: "/Announ/AddLike",
            type: "post",
            data: "announ_id=" + $(this).attr('data-announ_id'),
            success: function (result) {
                $('.like_clicked').html('' + result);
                $('.like_clicked').removeClass('like_clicked');
            }
        });
    });
    
    $('.dislike_btn').click(function () {
        $(this).addClass('like_clicked');
        $.ajax({
            url: "/Announ/AddDislike",
            type: "post",
            data: "announ_id=" + $(this).attr('data-announ_id'),
            success: function (result) {
                $('.like_clicked').html('' + result);
                $('.like_clicked').removeClass('like_clicked');
            }
        });
    });

    $('.spros').click(function () {
        $('.spros').css('background-image', 'url(/Content/img/announ_target_ico.png)');
        $('.spros').css('color', 'white');
        $('.predlojenie').css('color', '#115a56');
        $('.predlojenie').css('background-image', 'none');
        
    });

    var target = location.href;
   
    if (target.indexOf("Rabota") > 0) {
        $('.spros').css('background-image', 'url(/Content/img/announ_target_ico.png)');
        $('.spros').css('color', 'white');
        $('.predlojenie').css('color', '#115a56');
        $('.predlojenie').css('background-image', 'none');
        var predlojenie = target.substring(0, target.indexOf("Rabota") - 1) + '/Service';
        var spros = target + "/Rabota";
        spros = spros.replace('//', '/');
        $('.spros').attr('href', target);
        $('.predlojenie').attr('href', predlojenie);
    } else {
        $('.predlojenie').css('background-image', 'url(/Content/img/announ_target_ico.png)');
        $('.predlojenie').css('color', 'white');
        $('.spros').css('color', '#115a56');
        $('.spros').css('background-image', 'none');
        var spros = target.substring(0, target.indexOf("Service") - 1) + "/Rabota";
        
        $('.spros').attr('href', spros);
        $('.predlojenie').attr('href', target);

    }

    $('.predlojenie').click(function () {
        $('.predlojenie').css('background-image', 'url(/Content/img/announ_target_ico.png)');
        $('.predlojenie').css('color', 'white');
        $('.spros').css('color', '#115a56');
        $('.spros').css('background-image', 'none');
        
    });
    $('.close').click(function () {
        
    });
});

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});


$(document).ready(function () {
    $('.find_btn_types').click(function () {
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
            $('.return_result_search').html(result);
            //setTimeout(function () {
            //    location.reload();
            //}, 5000)
        }
    });
    });
});
