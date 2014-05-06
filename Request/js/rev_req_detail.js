var solicit_id;
var solicitante_id;
var responsable;
var estado;
var S_document_type;
var S_document_type2;
var S_document_name;
var S_original_language;
var S_translate_language;
var S_solicit_priority;
var S_priority_comment;
var S_observations;
var S_register_date;
var S_desired_date;
var S_Key_name;
var T_send_feedback;
var iframe;
var T_send_feedback;
var T_Fecha_Estimada;
var T_Observaciones;
var T_requiere_revision;
var T_send_review;
var T_document_translate;
var TR_format_translate;
var RT_send_review;

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

var QueryString = function () {
    // This function is anonymous, is executed immediately and 
    // the return value is assigned to QueryString!
    var query_string = {};
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        // If first entry with this name
        if (typeof query_string[pair[0]] === "undefined") {
            query_string[pair[0]] = pair[1];
            // If second entry with this name
        } else if (typeof query_string[pair[0]] === "string") {
            var arr = [query_string[pair[0]], pair[1]];
            query_string[pair[0]] = arr;
            // If third or later entry with this name
        } else {
            query_string[pair[0]].push(pair[1]);
        }
    }
    return query_string;
}();

function registrarInfo(formulario) {
    myApp.showPleaseWait();
    var datae = { 'solicit_id': solicit_id, 'solicitante_id': solicitante_id, 'responsable': responsable, 'estado': estado, 'S_document_type': S_document_type2, 'S_document_name': S_document_name, 'S_original_language': S_original_language, 'S_translate_language': S_translate_language, 'S_solicit_priority': S_solicit_priority, 'S_priority_comment': S_priority_comment, 'S_observations': S_observations, 'S_register_date': S_register_date, 'S_desired_date': S_desired_date, 'S_Key_name': S_Key_name, 'estimated_date': formulario.estimated_date, 'observations_feedback': formulario.observations_feedback, 'estado_feed': formulario.estado_feed, 'revision': formulario.revision };
    $.ajax({
        type: "POST",
        url: "rev_req_detail.aspx/putData",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datae),
        dataType: "json",
        success: function (resultado) {
            if (resultado.d !== "ok") {
                myApp.hidePleaseWait();
                message("Alert, please try again", "Register", "danger");
            } else {
                myApp.hidePleaseWait();
                T_send_feedback = "Y";
                var id = QueryString.id;
                getRequest(id);
                limpiarCampos(formulario);
                message("Information sent successfully", "Register", "danger");
                $("#responder").css({ "display": "none" });
                $("#detalles").css({ "display": "block", "margin-right": "auto", "margin-left": "auto", "*zoom": "1", "position": "relative" });
                

            }
        }
    });
    return false;
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

$(document).ready(function () {
    jQuery('body')
	  .delay(500)
	  .queue(
	  	function (next) {
	  	    jQuery(this).css('padding-right', '1px');
	  	}
	  );

    obtenerNombreUsuario();

    $("#my_solicits").css({ "background-color": "transparent", "color": "#a1aaaf" });
    var id = QueryString.id;
    if (id == undefined) {
        document.location.href = "solicitante.aspx";
    } else {
        getRequest(id);
    }

    $("#fileToUpload").change(function () {
        if (this.value != "") {
            if (FileSize()) {
                $("#doc_content").css("display", "block");
                $("#name_document").text(this.value);
            }
        } else {
            $("#doc_content").css("display", "none");
        }
    });

    $("#Register_r").click(function () {

        var formulario = getFormReview(id);
        var validado = validar(formulario);
        if (validado) {
            registrarInfoReview(formulario);
        } else {
            message("Please check the Mandatory fields", "Register", "danger");
        }
        return false;
    });

    $("#Select1").change(function () {
        if (this.value == "1") {
            $("#copy_r").css("display", "block");
            $("#file_r").css("display", "none");

        } else if (this.value == "2") {
            $("#copy_r").css("display", "none");
            $("#file_r").css("display", "block");
        }
    });

      
    $("#Download").click(function () {
        downloadURL(S_document_name, "Files");
    });

    $("#BtnDownload").click(function () {
        downloadURL(T_document_translate, "Translations");
    });

    $("#exit").click(function () {
        cerrarSession();
    });

    $("#my_solicits").click(function () {
        $("#my_solicits").css({ "background-color": "#8e040a", "color": "#fff" });
        $("#r_details").css({ "background-color": "transparent", "color": "#a1aaaf" });
        $("#actions").css({ "background-color": "transparent", "color": "#a1aaaf" });
        $("#profile").css({ "background-color": "transparent", "color": "#a1aaaf" });
    });

    $("#actions").click(function () {
        $("#my_solicits").css({ "background-color": "transparent", "color": "#a1aaaf" });
        $("#r_details").css({ "background-color": "transparent", "color": "#a1aaaf" });
        $("#actions").css({ "background-color": "#8e040a", "color": "#fff" });
        $("#profile").css({ "background-color": "transparent", "color": "#a1aaaf" });
    });

    $("#profile").click(function () {
        $("#my_solicits").css({ "background-color": "transparent", "color": "#a1aaaf" });
        $("#r_details").css({ "background-color": "transparent", "color": "#a1aaaf" });
        $("#actions").css({ "background-color": "transparent", "color": "#a1aaaf" });
        $("#profile").css({ "background-color": "#8e040a", "color": "#fff" });
    });

    $("#r_details").click(function () {
        $("#my_solicits").css({ "background-color": "transparent", "color": "#a1aaaf" });
        $("#r_details").css({ "background-color": "#8e040a", "color": "#fff" });
        $("#actions").css({ "background-color": "transparent", "color": "#a1aaaf" });
        $("#profile").css({ "background-color": "transparent", "color": "#a1aaaf" });
    });


});

function validarJson(divs_id) {
    if ((divs_id.substring(divs_id.length - 1, divs_id.length)) === ']')
        divs_id = divs_id.substring(0, divs_id.length - 1);

    if ((divs_id.substring(0, 1)) === '[')
        divs_id = divs_id.substring(1, divs_id.length);

    return divs_id;

}

function getTypeDocument(id) {
    var type = parseInt(id);
    var nombre = "";
    switch (type) {
        case 1:
            nombre = "Copy";
            break;
        case 2:
            nombre = "Video";
            break;
        case 3:
            nombre = "Document";
            break;
        case 4:
            nombre = "URL";
            break;
        case 5:
            nombre = "Presentation";
            break;
        case 6:
            nombre = "Image";
            break;
        default:

    }
    return nombre;
}

function getRequestData(id) {
    var datae = { 'id': id };
    $.ajax({
        type: "POST",
        url: "rev_req_detail.aspx/getDatosRequest",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datae),
        dataType: "json",
        success: function (resultado) {
            if (resultado.d != "[]") {
                var item = $.parseJSON(resultado.d);
                $("#tbody").html('');
                $("#tbody1").html('');
                $("#tbody2").html('');
                $.each(item, function (i, valor) {
                    if (i % 2 == 0) {
                        $("#tbody").append('<tr class="gradeA odd"><td>' + valor.S_Key_name + '</td><td>' + valor.nombre + '</td><td>' + valor.S_original_language + '</td><td>' + valor.S_translate_language + '</td><td>' + valor.S_register_date + '</td><td>' + valor.S_desired_date + '</td><td>' + valor.S_solicit_priority + '</td></tr>');
                        $("#tbody1").append('<tr class="gradeA odd"><td>' + valor.S_Key_name + '</td><td>' + valor.nombre + '</td><td>' + valor.S_original_language + '</td><td>' + valor.S_translate_language + '</td><td>' + valor.S_register_date + '</td><td>' + valor.S_desired_date + '</td><td>' + valor.S_solicit_priority + '</td></tr>');
                        $("#tbody2").append('<tr class="gradeA odd"><td>' + valor.S_Key_name + '</td><td>' + valor.nombre + '</td><td>' + valor.S_original_language + '</td><td>' + valor.S_translate_language + '</td><td>' + valor.S_register_date + '</td><td>' + valor.S_desired_date + '</td><td>' + valor.S_solicit_priority + '</td></tr>');
                    }
                    else {
                        $("#tbody").append('<tr class="gradeA even"><td>' + valor.S_Key_name + '</td><td>' + valor.nombre + '</td><td>' + valor.S_original_language + '</td><td>' + valor.S_translate_language + '</td><td>' + valor.S_register_date + '</td><td>' + valor.S_desired_date + '</td><td>' + valor.S_solicit_priority + '</td></tr>');
                        $("#tbody1").append('<tr class="gradeA even"><td>' + valor.S_Key_name + '</td><td>' + valor.nombre + '</td><td>' + valor.S_original_language + '</td><td>' + valor.S_translate_language + '</td><td>' + valor.S_register_date + '</td><td>' + valor.S_desired_date + '</td><td>' + valor.S_solicit_priority + '</td></tr>');
                        $("#tbody2").append('<tr class="gradeA even"><td>' + valor.S_Key_name + '</td><td>' + valor.nombre + '</td><td>' + valor.S_original_language + '</td><td>' + valor.S_translate_language + '</td><td>' + valor.S_register_date + '</td><td>' + valor.S_desired_date + '</td><td>' + valor.S_solicit_priority + '</td></tr>');
                    }
                });
            }
        }
    });
    return false;
}

function registrarInfoReview(formulario) {
    myApp.showPleaseWait();
    var datae = { 'solicit_id': solicit_id, 'solicitante_id': solicitante_id, 'responsable': responsable, 'estado': estado, 'S_document_type': S_document_type2, 'S_document_name': S_document_name, 'S_original_language': S_original_language, 'S_translate_language': S_translate_language, 'S_solicit_priority': S_solicit_priority, 'S_priority_comment': S_priority_comment, 'S_observations': S_observations, 'S_register_date': S_register_date, 'S_desired_date': S_desired_date, 'S_Key_name': S_Key_name, 'estimated_date': T_Fecha_Estimada, 'observations_feedback': T_Observaciones, 'estado_rev': formulario.estado_rev, 'revision': T_requiere_revision, 'type_send_translation': TR_format_translate, 'type_send': formulario.type_send, 'translate': formulario.translate, 'observations_r': formulario.observations_r };
    $.ajax({
        type: "POST",
        url: "rev_req_detail.aspx/putDataReview",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datae),
        dataType: "json",
        success: function (resultado) {
            if (resultado.d !== "ok") {
                myApp.hidePleaseWait();
                message("Alert, please try again", "Register", "danger");
            } else {
                if (formulario.type_send == "2") {
                    ajaxFileUpload('fileToUpload', solicit_id, formulario);
                } else {
                    myApp.hidePleaseWait();
                    limpiarCampos(formulario);
                    RT_send_review = 'YES';
                    estado = 11;
                    $("#menu_actions").html("<li><a href=\"#\">No Actions yet..</a></li>");
                    message("Information sent successfully", "Register", "danger");
                    $("#responder").css({ "display": "none" });
                    $("#review").css({ "display": "none" });
                    $("#translate").css({ "display": "none" });
                    $("#send_correction").css({ "display": "none" });
                    $("#detalles").css({ "display": "block", "margin-right": "auto", "margin-left": "auto", "*zoom": "1", "position": "relative" });
                }
            }
        }
    });
    return false;
}


function s_Review() {
    var id = QueryString.id;
    getRequestData(id);

    $("#detalles").css({ "display": "none" });
    $("#review").css({ "display": "none" });
    $("#responder").css({ "display": "none" });
    $("#translate").css({ "display": "none" });
    $("#send_correction").css({ "display": "block", "margin-right": "auto", "margin-left": "auto", "*zoom": "1", "position": "relative" });

};




function cerrarSession() {
    $.ajax({
        type: "POST",
        url: "rev_req_detail.aspx/cerrarSession",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (resultado) {
            document.location.href = "admin.aspx";
        }
    });
    return false;
}



function getRequest(id) {
    var datae = { 'id': id };
    $.ajax({
        type: "POST",
        url: "rev_req_detail.aspx/getRequest",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datae),
        dataType: "json",
        success: function (resultado) {
            if (resultado.d === "fail") {
                document.location.href = "admin.aspx";
            } else {
                var jposts = resultado.d;
                jposts = validarJson(jposts);
                var item = $.parseJSON(jposts);

                solicit_id = item.solicit_id;
                solicitante_id = item.solicitante_id;
                responsable = item.responsable;
                estado = item.estado;
                if (estado == 6) {
                    $("#menu_actions").html("<li><a href=\"#\" id=\"s_Review\" onClick=\"s_Review();\">Send Review</a></li>");
                } 
                S_document_type = getTypeDocument(item.S_document_type);
                S_document_type2 = item.S_document_type;
                S_document_name = item.S_document_name;
                S_original_language = item.S_original_language;
                S_translate_language = item.S_translate_language;
                S_solicit_priority = item.S_solicit_priority;
                S_priority_comment = item.S_priority_comment;
                S_observations = item.S_observations;
                S_register_date = item.S_register_date;
                S_desired_date = item.S_desired_date;
                S_Key_name = item.S_Key_name;
                T_send_feedback = item.T_send_feedback;
                T_Fecha_Estimada = item.T_Fecha_Estimada;
                T_Observaciones = item.T_Observaciones;
                T_requiere_revision = item.T_requiere_revision;
                T_send_review = item.TR_send_review;
                T_document_translate = item.T_document_translate;
                TR_format_translate = item.TR_format_translate;
                RT_send_review = item.RT_send_review;
                

                if (TR_format_translate == 1) {
                    $("#text_t").css("display", "block");
                    $("#doc_t").css("display", "none");
                    $("#texto_t").val(T_document_translate);
                } else {
                    $("#text_t").css("display", "none");
                    $("#doc_t").css("display", "block");
                }

                $("#translation_name").val(item.S_Key_name);
                $("#document_type").val(S_document_type);

                if (item.S_document_type == "1") {
                    $("#copy").css("display", "block");
                    $("#copy_field").val(S_document_name);
                    $("#url").css("display", "none");
                    $("#file").css("display", "none");
                    $("#s_copy").css("display", "block");
                    $("#s_url").css("display", "none");
                    $("#s_file").css("display", "none");

                } else if ((item.S_document_type == "4") || (item.S_document_type == "2")) {
                    $("#copy").css("display", "none");
                    $("#url").css("display", "block");
                    $("#url_field").val(S_document_name);
                    $("#file").css("display", "none");

                    $("#s_copy").css("display", "none");
                    $("#s_url").css("display", "block");
                    $("#s_file").css("display", "none");
                } else if ((item.S_document_type == "3") || (item.S_document_type == "5") || (item.S_document_type == "6")) {
                    $("#copy").css("display", "none");
                    $("#url").css("display", "none");
                    $("#file").css("display", "block");
                    documento = item.S_document_name;

                    $("#s_copy").css("display", "none");
                    $("#s_url").css("display", "none");
                    $("#s_file").css("display", "block");
                }

                $("#original_language").val(item.S_original_language);
                $("#translate_language").val(item.S_translate_language);
                
                $("#register_date").val(item.S_register_date);
                $("#prioridad").val(item.S_solicit_priority);
                if (item.S_solicit_priority == "High") {
                    $("#priority").css("display", "block");
                } else {
                    $("#priority").css("display", "none");
                }

                $("#delivery_date").val(item.T_Fecha_Estimada);
                $("#desired_date").val(item.S_desired_date);
                if (Date.parse(item.T_Fecha_Estimada) > Date.parse(item.S_desired_date)) {
                    $("#delivery_date").css({"font-weight": "bolder","color":"#cc0000"});  
                }
                $("#revision_date").val(item.S_register_date);
                $("#TR_observations").val(item.TR_observations);
                $("#priority_comment").val(item.S_priority_comment);
                $("#observations").val(item.S_observations);
                $("#udnombre").val(item.udnombre);
                $("#observations_feedback").val(item.T_Observaciones);
                $("#estado_feed").val(item.nombre);
                $("#estimated_date").val(item.S_register_date);

                switch (estado) {

                    case 1://Requerida
                        $("#estado").addClass("alert alert-warning");
                        $("#estado").html("<strong>Requested!</strong>");
                        break;
                    case 2://Aceptada
                        $("#estado").addClass("alert alert-info");
                        $("#estado").html("<strong>Accepted!</strong>");
                        break;
                    case 3://En espera
                        $("#estado").addClass("alert alert-info");
                        $("#estado").html("<strong>Hold up</strong>");
                        break;
                    case 4://Denegada
                        $("#estado").addClass("alert alert-danger");
                        $("#estado").html("<strong>Denied!</strong>");
                        $("#reasonsRejection").css("display", "block");
                        $("#reasonsText").html("" + item.T_Observaciones);
                        $("#reasonsText").addClass("alert alert-danger");
                        break;
                    case 5://En traduccion
                        $("#estado").addClass("alert alert-info");
                        $("#estado").html("<strong>In translation</strong>");
                        break;
                    case 6://En revisión
                        $("#estado").addClass("alert alert-info");
                        $("#estado").html("<strong>In review</strong>");
                        break;
                    case 7://Entregada
                        $("#estado").addClass("alert alert-success");
                        $("#estado").html("<strong>Delivered!</strong>");
                        break;
                    case 8://Declinada/Cancelada por sistema
                    case 9:
                        $("#estado").addClass("alert alert-danger");
                        $("#estado").html("<strong>Declined / Cancelled by system</strong>");
                        break;
                    case 10://Finalizada
                        $("#estado").addClass("alert alert-success");
                        $("#estado").html("<strong>Finished!</strong>");
                        break;
                    case 11://Revisada
                        $("#estado").addClass("alert alert-success");
                        $("#estado").html("<strong>Reviewed!</strong>");
                        break;
                    case 12://Corregida
                        $("#estado").addClass("alert alert-info");
                        $("#estado").html("<strong>Corrected!</strong>");
                        break;
                    case 13://Cancelada por el solicitante
                        $("#estado").addClass("alert alert-danger");
                        $("#estado").html("<strong> Cancelled by requestor</strong>");
                        break;
                }
            }
        }
    });

    return false;
}

//Send for Review
function getFormReview(key) {
    var id = key;
    var type_send = $("#Select1");
    var copy_field_r = $("#copy_field_r");
    var fileToUpload = $("#fileToUpload");
    var observations_r = $("#observations_r");

    var formulario2 = new Object();
    formulario2.id = id;
    formulario2.type_send = type_send.val();
    if (type_send.val() === "1") {
        formulario2.translate = copy_field_r.val();
    } else {
        formulario2.translate = fileToUpload.val();
    }
    formulario2.observations_r = observations_r.val();
    formulario2.estado_rev = 11;

    return formulario2;
}

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

function limpiarCampos(formulario) {
    var nombre;
    for (var i in formulario) {
        nombre = i;
        $("#" + nombre).val('');
    }
}

function ajaxFileUpload(filename, id, formulario) {
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
            url: 'AjaxFileUploader4.ashx?solicit_id=' + id,
            secureuri: false,
            fileElementId: filename,
            dataType: 'json',
            data: { name: 'logan', id: id },
            success: function (data, status) {
                if (typeof (data.error) != 'undefined') {
                    if (data.error != '') {
                        myApp.hidePleaseWait();
                        alert(data.error);
                    } else {
                        myApp.hidePleaseWait();
                        limpiarCampos(formulario);
                        RT_send_correction = 'YES';
                        $("#menu_actions").html("<li><a href=\"#\">No Actions yet..</a></li>");
                        message("Information sent successfully", "Register", "danger");
                        $("#responder").css({ "display": "none" });
                        $("#review").css({ "display": "none" });
                        $("#send_correction").css({ "display": "none" });
                        $("#detalles").css({ "display": "block", "margin-right": "auto", "margin-left": "auto", "*zoom": "1", "position": "relative" });
                        //myApp.hidePleaseWait();
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

function FileSize() {
    var input, file;
    var resultado;
    if (!window.FileReader) {
        console.error("The file API isn't supported on this browser yet.");
        return;
    }

    input = document.getElementById('fileToUpload');
    if (!input.files) {
        console.error("This browser doesn´t seem to support the `files` property of file inputs.");
    }
    else {
        file = input.files[0];
        var sizeInMB = file.size / 1024 / 1024;
        if (sizeInMB > 10) {
            message("<strong>" + file.name + "</strong> : Exceeds the allowable limit", "File Size", "danger");
            return false;
        }
        else return true;
    }
}