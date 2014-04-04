var oTable;
var giRedraw = false;
var dataTab;
var aData;
var iframe;

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

function cargarDatos() {

    $.prettyLoader();
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
                "aoColumns": [{ "mDataProp": "S_key_name" }, { "mDataProp": "nombre" }, { "mDataProp": "S_original_language" }, { "mDataProp": "S_translate_language" }, { "mDataProp": "S_register_date" }, { "mDataProp": "S_desired_date" }, { "mDataProp": "S_solicit_priority" }, { "mDataProp": "Edit" }],
                "sPaginationType": "bootstrap",
                "aaSorting": [[0, "asc"]],
                "bJQueryUI": true
            });
        };
    } else {
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



    cargarDatos();

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


    //$("#datatables #tbody").click(function (event) {
    //    $(oTable.fnSettings().aoData).each(function () {
    //        $(this.nTr).removeClass('row_selected');
    //    });
    //    $(event.target.parentNode).addClass('row_selected');

    //    var tds = $(event.target.parentNode).find("td");
    //    var node = tds[0].childNodes[0];
    //    aData = node.data;
    //});

    $("#new_solicit").click(function () {
        $("#dt_my_solicits").css({ "display": "none" });
        $("#nueva_solicitud").css({ "display": "block", "margin-right": "auto", "margin-left": "auto", "*zoom": "1", "position": "relative" });
    });


    $("#my_solicits").click(function () {
        $("#nueva_solicitud").css({ "display": "none" });
        $("#dt_my_solicits").css({ "display": "block", "margin-right": "auto", "margin-left": "auto", "*zoom": "1", "position": "relative" });
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