//tipo de mensajes ->  default, info, primary, sucess, warning, danger
function message(texto, titulo, tipo) {
    BootstrapDialog.show({
        title: titulo,
        message: texto,
        cssClass: 'type-' + tipo
    });
    return false;
}

function datosCalendario(traductor) {

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

    var datae = { 'traductor': traductor  }
    $.ajax({
        type: "POST",
        url: "Lista_Traducciones.aspx/getEvents",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datae),
        dataType: "json",
        success: function (resultado) {
            if (resultado.d === "null" || resultado.d === "" || resultado.d === "[]") {
                message("No data found", "Alert", "danger");
                $("#calendar").css("visibility", "hidden");
                
            } else {
                $("#calendar").css("visibility", "visible");
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
        }, error: function () {
            message("Data Error", "Alert", "danger");
            $("#calendar").css("visibility", "hidden");
        }
    });

}


function traerTraductores() {
    $("#traductores").html('');
    $.ajax({
        type: "POST",
        url: "Lista_Traducciones.aspx/traerTraductores",
        contentType: "application/json; charset=utf-8",
        data: '{ }',
        dataType: "json",
        success: function (resultado) {

            if (resultado.d == "fail") {
                message("Data error, please try again", "Register", "danger");
            } else {
                var jposts = resultado.d;
                var item = $.parseJSON(jposts);
                $("#traductores").append('<option value="" selected="selected"></option>');
                $.each(item, function (i, valor) {
                    //introducimos los option del Json obtenido
                    $("#traductores").append('<option value="' + valor.id + '">' + valor.nombre + '</option>');
                });
            }
        },
        error: function () {
            message("Data error, please try again", "Register", "danger");
        }
    });
    return false;
}

function obtenerNombreUsuario() {
    var rta = 0;
    $.ajax({
        type: "POST",
        url: "solicitante.aspx/obtenerDatosUsuario",
        dataType: "text",
        contentType: "application/json; charset=utf-8",
        data: '{}',
        dataType: "json",
        success: function (result) {
            if (result.d === "fail") {
                document.location.href = "admin.aspx";
            } else {
                var datos = result.d;
                var array = datos.split(",");
                var nombre = array[0];
                var foto = array[1];

                //$(document.body).show();
                $("#userName").html('Welcome: ' + nombre + '&nbsp;&nbsp;<img src="images/' + foto + '" style="width:50px;height:50px;"/>');
            }
        }
    });
    return false;
}

$(document).ready(function () {
    obtenerNombreUsuario();

    jQuery('body')
	  .delay(500)
	  .queue(
	  	function (next) {
	  	    jQuery(this).css('padding-right', '1px');
	  	}
	  );

    traerTraductores();


    $("#Register").click(function () {
        var traductor = $("#traductores").val();
        datosCalendario (traductor);

    });




});


