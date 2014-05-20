var oTable;
var giRedraw = false;
var dataTab;
var aData;
var iframe;
var datosfiltro;

jQuery('body')
	  .delay(500)
	  .queue(
	  	function (next) {
	  	    jQuery(this).css('padding-right', '1px');
	  	}
);

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

function filterBySolicitante(solicitante) {
    var datae = { 'user': solicitante, 'rol': 'solicitante' }
    datosfiltro = "solicitante," + solicitante;
    $.ajax({
        type: "POST",
        url: "report.aspx/getDatosReg2",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datae),
        dataType: "json",
        success: AjaxRefreshDataSucceeded,
        error: AjaxRefreshDataFailed
    });
}

function filterByResponsable(responsable) {
    var datae = { 'user': responsable, 'rol': 'responsable' }
    datosfiltro = "responsable," + responsable;
    $.ajax({
        type: "POST",
        url: "report.aspx/getDatosReg2",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datae),
        dataType: "json",
        success: AjaxRefreshDataSucceeded,
        error: AjaxRefreshDataFailed
    });
}

function cargarDatos() {
    myApp.showPleaseWait();
    datosfiltro = "all";
    $.ajax({
        type: "POST",
        url: "report.aspx/getDatosReg",
        contentType: "application/json; charset=utf-8",
        data: "{ }",
        dataType: "json",
        success: AjaxGetFieldDataSucceeded,
        error: AjaxGetFieldDataFailed
    });
}

function cargarDatos2() {
    datosfiltro = "all";
    $.ajax({
        type: "POST",
        url: "report.aspx/getDatosReg",
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
                
                "aoColumns": [{ "mDataProp": "solicit_id" }, { "mDataProp": "S_key_name" }, { "mDataProp": "solicitante" }, { "mDataProp": "original" }, { "mDataProp": "translate" }, { "mDataProp": "estado" }, { "mDataProp": "S_register_date2" }, { "mDataProp": "T_Fecha_Estimada2" }, { "mDataProp": "InCharge" }, { "mDataProp": "duration" }],
                "sPaginationType": "bootstrap",
                "aaSorting": [[8, "desc"]],
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

function validarAdmin() {
    $.ajax({
        type: "POST",
        url: "translationsByUser.aspx/validaSession",
        contentType: "application/json; charset=utf-8",
        data: '{}',
        dataType: "json",
        success: function (resultado) {
            if (resultado.d === "fail") {
                document.location.href = "admin.aspx";
            } else {
                var datos = resultado.d;
                var array = datos.split(",");
                var nombre = array[0];
                var foto = array[1];
                $("#userName").html('Welcome: ' + nombre + '&nbsp;&nbsp;<img src="images/' + foto + '" style="width:50px;height:50px;"/>');
            }
        },
        error: function (resultado) {
            document.location.href = "admin.aspx";
        }
    });
    return false;
}


$(document).ready(function () {
    jQuery('body')
	  .delay(500)
	  .queue(
	  	function (next) {
	  	    jQuery(this).css('padding-right', '1px');
	  	}
	  );

    validarAdmin();

    cargarDatos();
    
    $("#my_solicits").css({ "background-color": "#8e040a", "color": "#fff" });

    $("#fileToUpload").change(function () {
        if (this.value != "") {
            if (validarExtension(this.value)) {
                if (FileSize()) {
                    $("#doc_content").css("display", "block");
                    $("#name_document").text(this.value);
                }
            } else {
                message("Please check the filetype, system accept PDF,DOC,DOCX,TXT.", "Error", "danger");
                $("#fileToUpload").val("");
            }
        } else {
            $("#doc_content").css("display", "none");
        }
    });
    

    $("#toExcel").click(function () {
        exportarTabla("XLS");
    });
    

    $("#datatables #tbody").click(function (event) {
        $(oTable.fnSettings().aoData).each(function () {
            $(this.nTr).removeClass('row_selected');
        });
        $(event.target.parentNode).addClass('row_selected');

        var tds = $(event.target.parentNode).find("td");
        if (tds.value !== undefined) {
            var node = tds[0].childNodes[0];
            aData = node.data;
        }
    });

    $("#showAll").click(function () {
        if (oTable !== undefined) {
            cargarDatos2();
        }
        else {
            cargarDatos();
        }
    });


    var date = new Date();
    date.setDate(date.getDate()+2);


    $("#exit").click(function () {
        cerrarSession();
    });
});

function cerrarSession() {
    $.ajax({
        type: "POST",
        url: "solicitante.aspx/cerrarSession",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (resultado) {
            document.location.href = "admin.aspx";
        }
    });
    return false;
}


//Descargar el Excel del listado de solicitudes
function exportarTabla(formato) {
    if (datosfiltro !== "all") {
        var array = datosfiltro.split(",");
         var datae = { 'user': array[1], 'rol': array[0] }
    }
    else {
        var datae = { 'user': 0, 'rol': "all" }
    }
  $.ajax({
        type: "POST",
        url: "report.aspx/Convertir",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datae),
        dataType: "json",
        success: function (result) {
            var jposts = result.d;
            var mensaje;
            var titulo;

            if (result.d != "fail") {
                downloadURL(result.d,"ExcelFiles");
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
