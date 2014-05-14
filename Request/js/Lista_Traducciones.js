var oTable;
var giRedraw = false;
var dataTab;
var aData;

//tipo de mensajes ->  default, info, primary, sucess, warning, danger
function message(texto, titulo, tipo) {
    BootstrapDialog.show({
        title: titulo,
        message: texto,
        cssClass: 'type-' + tipo
    });
    return false;
}

var myApp;
myApp = myApp || (function () {
    var pleaseWaitDiv = $('<div class="modal hide" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false"><div class="modal-header"><h1>Processing...</h1></div><div class="modal-body"><div class="progress progress-striped active"><div class="bar" style="width: 100%;"></div></div></div></div>');

    return {
        showPleaseWait: function () {
            pleaseWaitDiv.modal();
        },
        hidePleaseWait: function () {
            pleaseWaitDiv.modal('hide');
        },

    };
})();

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
                //message("No data found", "Alert", "danger");
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
            //message("Data Error", "Alert", "danger");
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

//

function cargarDatos(traductor) {
    myApp.showPleaseWait();
    var datae = { 'traductor': traductor }
    
    $.ajax({
        type: "POST",
        url: "Lista_Traducciones.aspx/getDatosReg",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datae),
        dataType: "json",
        success: AjaxGetFieldDataSucceeded,
        error: AjaxGetFieldDataFailed
    });
}

function cargarDatos2(traductor) {
    var datae = { 'traductor': traductor }
    $.ajax({
        type: "POST",
        url: "Lista_Traducciones.aspx/getDatosReg",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datae),
        dataType: "json",
        success: AjaxRefreshDataSucceeded,
        error: AjaxRefreshDataFailed
    });
}

function AjaxRefreshDataSucceeded(result) {
    if (result.d != "[]") {
        var jposts = result.d;
        var mensaje;
        var titulo;
        dataTab = $.parseJSON(jposts);
        oTable.fnClearTable();
        oTable.fnAddData(dataTab);
    } else {
        oTable.fnClearTable();
        message("No data found", "Alert", "danger");
    }
}

function AjaxRefreshDataFailed(result) {
    alert(result.status + ' ' + result.statusText);
}

function AjaxGetFieldDataFailed(result) {
    alert(result.status + ' ' + result.statusText);
}


$(function () {
    $('.datatable').each(function () {
        var datatable = $(this);
        // SEARCH - Add the placeholder for Search and Turn this into in-line formcontrol
        var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
        search_input.attr('placeholder', 'Search')
        search_input.addClass('form-control input-small')
        search_input.css('width', '250px')

        // SEARCH CLEAR - Use an Icon
        var clear_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] a');
        clear_input.html('<i class="icon-remove-circle icon-large"></i>')
        clear_input.css('margin-left', '5px')

        // LENGTH - Inline-Form control
        var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
        length_sel.addClass('form-control input-small')
        length_sel.css('width', '75px')

        // LENGTH - Info adjust location
        var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_info]');
        length_sel.css('margin-top', '18px')
    });
});


function AjaxGetFieldDataSucceeded(result) {
    if (result.d == "fail") {
        document.location.href = "admin.aspx";
    } else if (result.d != "[]") {
        var jposts = result.d;
        var mensaje;
        var titulo;
        try {
            dataTab = $.parseJSON(jposts);
        } catch (exception) {
            dataTab = "error";
        }
        if (dataTab != "error") {
            $("#datatables").css("visibility", "visible");
            oTable = $('#datatables').dataTable({
                "bProcessing": true,
                "aaData": dataTab,
                "aoColumns": [{ "mDataProp": "S_key_name" }, { "mDataProp": "nombre" }, { "mDataProp": "S_original_language" }, { "mDataProp": "S_translate_language" }, { "mDataProp": "S_register_date" }, { "mDataProp": "S_desired_date" }, { "mDataProp": "T_Fecha_Estimada" }, { "mDataProp": "S_solicit_priority" }],
                "sPaginationType": "bootstrap",
                "aaSorting": [[6, "desc"]],
                "bJQueryUI": true
            });
        };
        myApp.hidePleaseWait();
    } else {
        myApp.hidePleaseWait();
        message("No Requests found", "Information", "danger");

    }

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

    //traerTraductores();


    $("#Register").click(function () {
        var traductor = $("#traductores").val();
        if (oTable !== undefined) {
            cargarDatos2(traductor);
        } else {
            cargarDatos(traductor);
        }
        datosCalendario (traductor);
    });




});


