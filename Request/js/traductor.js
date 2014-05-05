var oTable;
var giRedraw = false;
var dataTab;
var aData;
var iframe;
var Json_data;
var year = new Date().getFullYear();
var month = new Date().getMonth();
var day = new Date().getDate();


//tipo de mensajes ->  default, info, primary, sucess, warning, danger
function message(texto, titulo, tipo) {
    BootstrapDialog.show({
        //type: 'BootstrapDialog.TYPE_' + tipo,
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



function getDatosCalendarioDetail() {
    $.ajax({
        type: "POST",
        url: "traductor.aspx/getDatosCalendario",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (resultado) {
            jposts = resultado.d;
            Json_data = $.parseJSON(jposts);
            var eventData1 = {
                options: {
                    timeslotsPerHour: 2,
                    
                },
                events: [
                    { "id": 1, "start": "2014-02-17T13:15:00.000", "end": "2014-02-22T14:15:00.000", "title": "APCD" },
	                { "id": 2, "start": "2014-02-18T09:15:00.000", "end": "2014-03-01T10:15:00.000", "title": "Evolutions" }
                ]
            };
            cargarCalendario(eventData1);
            
        }
    });
    return false;
}

function cargarDatos() {
    myApp.showPleaseWait();
    
    $.ajax({
        type: "POST",
        url: "traductor.aspx/getDatosReg",
        contentType: "application/json; charset=utf-8",
        data: "{ }",
        dataType: "json",
        success: AjaxGetFieldDataSucceeded,
        error: AjaxGetFieldDataFailed
    });
}

function cargarDatos2() {

    $.ajax({
        type: "POST",
        url: "traductor.aspx/getDatosReg",
        contentType: "application/json; charset=utf-8",
        data: "{ }",
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
                "aoColumns": [{ "mDataProp": "S_key_name" }, { "mDataProp": "nombre" }, { "mDataProp": "S_original_language" }, { "mDataProp": "S_translate_language" }, { "mDataProp": "S_register_date" }, { "mDataProp": "S_desired_date" }, { "mDataProp": "T_Fecha_Estimada" }, { "mDataProp": "S_solicit_priority" }, { "mDataProp": "Edit" }],
                "sPaginationType": "bootstrap",
                "aaSorting": [[0, "asc"]],
                "bJQueryUI": true
            });
        };
        myApp.hidePleaseWait();
    } else {
        myApp.hidePleaseWait();
        message("No Requests found", "Information", "danger");
        
    }
    
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

function AjaxGetFieldDataFailed(result) {
    alert(result.status + ' ' + result.statusText);
}

$.extend($.fn.dataTableExt.oStdClasses, {
    "sWrapper": "dataTables_wrapper form-inline"
});

function validar(obj) {
    var respuesta = 0;
    for (var i in obj) {
        if (obj[i] == null || obj[i].length < 1 || /^\s+$/.test(obj[i])) {
            respuesta = respuesta + 1;
            $("#" + i).css('background', '#E2E4FF');
        } else {
            respuesta = respuesta + 0;
            $("#" + i).css('background', '#FFF');
        }
    }
    if (respuesta === 0) {
        return true;
    } else {
        return false;
    }
}

    
$(document).ready(function () {

    jQuery('body')
	  .delay(500)
	  .queue(
	  	function (next) {
	  	    jQuery(this).css('padding-right', '1px');
	  	}
	  );
            
    cargarDatos();
    obtenerNombreUsuario();

    $("#toExcel").click(function () {
        exportarTabla("XLS");
    });

    $("#my_solicits").css({ "background-color": "#8e040a", "color": "#fff" });

    $('#Register').click(function () {
        var formulario = getForm();
        var validado = validar(formulario);
        if (validado) {
            registrarInfo(formulario);
        } else {
            message("Please check the Mandatory fields", "Register", "danger");
        }
        return false;
    });

    $("#document_type").change(function () {
        if (this.value == "1") {
            $("#copy").css("display", "block");
            $("#url").css("display", "none");
            $("#file").css("display", "none");

        } else if ((this.value == "4") || (this.value == "2")) {
            $("#copy").css("display", "none");
            $("#url").css("display", "block");
            $("#file").css("display", "none");
        } else if ((this.value == "3") || (this.value == "5") || (this.value == "6")) {
            $("#copy").css("display", "none");
            $("#url").css("display", "none");
            $("#file").css("display", "block");
        }
    });

    $("#prioridad").change(function () {
        if (this.value == "High") {
            $("#priority").css("display", "block");
        } else {
            $("#priority").css("display", "none");
        }
    });


    $("#my_solicits").click(function () {
        $("#nueva_solicitud").css({ "display": "none" });
        $("#dt_my_solicits").css({ "display": "block", "margin-right": "auto", "margin-left": "auto", "*zoom": "1", "position": "relative" });
        $("#my_solicits").css({ "background-color": "#8e040a", "color": "#fff" });
        $("#my_profile").css({ "background-color": "transparent", "color": "#a1aaaf" });
    });

    $("#my_profile").click(function () {
        $("#my_profile").css({ "background-color": "#8e040a", "color": "#fff" });
        $("#my_solicits").css({ "background-color": "transparent", "color": "#a1aaaf" });
    });

    $('#datetimepicker3').datetimepicker({
        //pickTime: false
    });
    $("#exit").click(function () {
        cerrarSession();
    });
});

    function cerrarSession() {
        $.ajax({
            type: "POST",
            url: "traductor.aspx/cerrarSession",
            contentType: "application/json; charset=utf-8",
            data: "{}",
            dataType: "json",
            success: function (resultado) {
                document.location.href = "admin.aspx";
            }
        });
        return false;
    }

    function registrarInfo(formulario) {

        var datae = { 'translation_name': formulario.translation_name, 'document_type': formulario.document_type, 'original_language': formulario.original_language, 'translate_language': formulario.translate_language, 'desired_date': formulario.desired_date, 'prioridad': formulario.prioridad, 'priority_comment': formulario.priority_comment, 'observations': formulario.observations, 'document_name': formulario.document_name };
        $.ajax({
            type: "POST",
            url: "traductor.aspx/putData",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datae),
            dataType: "json",
            success: function (resultado) {
                if (resultado.d !== -1) {
                    var divs_id = resultado.d;
                    if ((divs_id.substring(divs_id.length - 1, divs_id.length)) === ',') {
                        divs_id = divs_id.substring(0, divs_id.length - 1);
                    }
                    if ((formulario.document_type == "3") || (formulario.document_type == "5") || (formulario.document_type == "6")) {
                        ajaxFileUpload(divs_id);
                    } else {
                        setTimeout(function () {
                            message("The request is successfully created", "Solicit", "danger");
                            document.location.href = "traductor.aspx";
                        }, 2000);
                    }

                } else {
                    message("Alert, please try again", "Register", "danger");
                }
            }
        });
        return false;
    }


    function getForm() {
        var translation_name = $("#translation_name");
        var document_type = $("#document_type");
        if (document_type.val() == "1") {
            var document_name = $("#copy_field").val();
        } else if ((document_type.val() == "4") || (document_type.val() == "2")) {
            var document_name = $("#url_field").val();
        } else if ((document_type.val() == "3") || (document_type.val() == "5") || (document_type.val() == "6")) {
            var document_name = $("#fileToUpload").val();
        }
        var original_language = $("#original_language");

        var translate_language = "";
        $('#translate_language :checkbox:checked').each(function () {
            translate_language = translate_language + $(this).val() + ',';
        });
        if ((translate_language.substring(translate_language.length - 1, translate_language.length)) === ',') {
            translate_language = translate_language.substring(0, translate_language.length - 1);
        }

        var desired_date = $("#desired_date");
        var prioridad = $("#prioridad");
        var priority_comment = $("#priority_comment").val();
        if (priority_comment == "") {
            priority_comment = "EMPTY";
        }

        var observations = $("#observations");

        var formulario = new Object();
        formulario.translation_name = translation_name.val();
        formulario.document_type = document_type.val();
        formulario.document_name = document_name;
        formulario.original_language = original_language.val();
        formulario.translate_language = translate_language;
        formulario.desired_date = desired_date.val();
        formulario.prioridad = prioridad.val();
        formulario.priority_comment = priority_comment;
        formulario.observations = observations.val();


        return formulario;
    }

    //function ajaxFileUpload(id) {

    //    $.prettyLoader();
    //    $.ajaxFileUpload({
    //        url: 'AjaxFileUploader.ashx?div_id='+id,
    //        secureuri: false,
    //        fileElementId: 'fileToUpload',
    //        dataType: 'text',
    //        data: { id: id },
    //        success: function (result, status) {

    //            document.location.href = "traductor.aspx";
    //        },
    //        error: function (result, status) {
    //           alert("error");
    //        }
    //    });
    //    return false;
    //}


    function ajaxFileUpload(id) {
        $("#loading")
    .ajaxStart(function () {
        $(this).show();
    })
    .ajaxComplete(function () {
        $(this).hide();
    });

        $.ajaxFileUpload
        (
            {

                url: 'AjaxFileUploader.ashx?div_id=' + id,
                secureuri: false,
                fileElementId: 'fileToUpload',
                dataType: 'json',
                data: { name: 'logan', id: id },
                success: function (data, status) {
                    if (typeof (data.error) != 'undefined') {
                        if (data.error != '') {
                            alert(data.error);
                        } else {
                            document.location.href = "traductor.aspx";
                        }
                    }
                },
                error: function (data, status, e) {
                    alert("Please Select File");
                }
            }
        )

        return false;

    }

    function detailsTrad(id) {
        document.location.href = "trad_req_detail.aspx?id=" + id;
    }

    //Descargar el Excel del listado de traducciones
    function exportarTabla(formato) {
        $.ajax({
            type: "POST",
            url: "traductor.aspx/Convertir",
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: "json",
            success: function (result) {
                var jposts = result.d;
                var mensaje;
                var titulo;

                if (result.d != "fail") {
                    downloadURL(result.d, "ExcelFiles");
                }
                else if (result.d == "fail") {
                    mensaje = "fail, please try again";
                    titulo = "anim error";
                    new Messi(mensaje,
                    { title: 'User Proccess', titleClass: titulo, modal: true, buttons: [{ id: 0, label: 'Close', val: 'X' }] });
                }
            },
            error: AjaxtoPDFFailed
        });

    }

    function downloadURL(url, carpeta) {

        iframe = document.getElementById("hiddenDownloader");
        if (iframe === null) {
            iframe = document.createElement('iframe');
            iframe.id = "hiddenDownloader";
            iframe.style.visibility = 'hidden';
            document.body.appendChild(iframe);
        }
        iframe.src = "DescargarArchivo.ashx?file=" + url + "&carpeta=" + carpeta;
    }

    function AjaxtoPDFFailed(result) {
        alert(result.status + ' ' + result.statusText);
    }


//Funcion que permite obtener el nombre de usuario del traductor
    function obtenerNombreUsuario() {
        var rta = 0;
        $.ajax({
            type: "POST",
            url: "traductor.aspx/obtenerNombreUsuario",
            dataType: "text",
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: "json",
            success: function (result) {
                if (result.d === "fail") {
                    document.location.href = "admin.aspx";
                } else {
                    //$(document.body).show();
                    $("#userName").html("<h3>Welcome: " + result.d + "</h3>");
                }
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