(function ($) {

    var d = new Date();
    var dia = d.getDate();
    var mes = d.getMonth() + 1;
    meses = mes + "";
    if (meses.length == 1) {
        meses = "0" + meses;
    }
    var dias = dia + "";
    if (dias.length == 1) {
        dias = "0" + dias;
    }
    var ano = d.getFullYear();
    var fecha = ano + "-" + meses + "-" + dias;


    $.ajax({
        type: "POST",
        url: "traductor.aspx/getEvents",
        contentType: "application/json; charset=utf-8",
        data: "{ }",
        dataType: "json",
        success: function (resultado) {
            if (resultado.d !== "null" || resultado.d !== "" || resultado.d !== "[]") {
                var jposts = resultado.d;
                items = $.parseJSON(jposts);
                var options = {
                    events_source: items,
                    view: 'week',
                    tmpl_path: 'tmpls/',
                    tmpl_cache: false,
                    day: fecha,
                    onAfterEventsLoad: function (events) {
                        if (!events) {
                            return;
                        }
                        var list = $('#eventlist');
                        list.html('');

                        $.each(events, function (key, val) {
                            $(document.createElement('li'))
                                .html('<a href="' + val.url + '">' + val.title + '</a>')
                                .appendTo(list);
                        });
                    },
                    onAfterViewLoad: function (view) {
                        $('.page-header h3').text(this.getTitle());
                        $('.btn-group button').removeClass('active');
                        $('button[data-calendar-view="' + view + '"]').addClass('active');
                    },
                    classes: {
                        months: {
                            general: 'label'
                        }
                    }
                };

                var calendar = $('#calendar').calendar(options);

                $('.btn-group button[data-calendar-nav]').each(function () {
                    var $this = $(this);
                    $this.click(function () {
                        calendar.navigate($this.data('calendar-nav'));
                    });
                });

                $('.btn-group button[data-calendar-view]').each(function () {
                    var $this = $(this);
                    $this.click(function () {
                        calendar.view($this.data('calendar-view'));
                    });
                });

                $('#first_day').change(function () {
                    var value = $(this).val();
                    value = value.length ? parseInt(value) : null;
                    calendar.setOptions({ first_day: value });
                    calendar.view();
                });

                $('#language').change(function () {
                    calendar.setLanguage($(this).val());
                    calendar.view();
                });

                $('#events-in-modal').change(function () {
                    var val = $(this).is(':checked') ? $(this).val() : null;
                    calendar.setOptions({ modal: val });
                });
                $('#events-modal .modal-header, #events-modal .modal-footer').click(function (e) {
                    //e.preventDefault();
                    //e.stopPropagation();
                });
            }
        }
    });

}(jQuery));



   

