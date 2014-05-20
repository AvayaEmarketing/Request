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

function cargarDatos(mes, anio) {
    var datae = { 'mes': mes, 'anio': anio }
    $.ajax({
        type: "POST",
        url: "translationsByUser.aspx/getDatosGraph",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datae),
        dataType: "json",
        success: AjaxGetFieldDataSucceeded,
        error: AjaxGetFieldDataFailed
    });
}


function AjaxGetFieldDataFailed(result) {
    alert("ERROR");
}

function AjaxGetFieldDataSucceeded(result) {
    var dataTab = "";
    try {
        dataTab = $.parseJSON(result.d);
    }
    catch (e) {
        dataTab = "";
    }
    if (dataTab !== "") {
        $("#cvs").css("display", "block");
        var bar = new RGraph.Bar('cvs', dataTab.valores)
            .Set('gutter.top', 15)
            .Set('gutter.left', 35)
            .Set('gutter.right', 15)
            .Set('background.grid.vlines', false)
            .Set('background.grid.border', false)
            .Set('colors', ['Gradient(white:#f11:#f11)', 'Gradient(white:#1cc:#1cc)', 'Gradient(white:#00f:#00f)'])
            .Set('tooltips.event', 'mousemove')
            .Set('tooltips', dataTab.tooltips)
            .Set('labels', dataTab.nombres)
            .Set('highlight.stroke', 'rgba(0,0,0,0.1)')
            .Set('strokestyle', 'rgba(0,0,0,0)')
            .Set('shadow', true)
            .Set('shadow.offsetx', -2)
            .Set('shadow.offsety', -2)
            .Set('shadow.blur', 5)
            .Set('ymax', 20)
            .Set('noyaxis', true)
            .Set('ylabels', true)
            .Set('title.yaxis', 'No. Requests')
            .Set('labels.above', true)
            .Set('linewidth', 2)
            .Set('hmargin', 15)
            .Draw();
    } else {
        $("#cvs").css("display","none");
        message("No data found","Alert","danger");
    }
}

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

    validarAdmin();

    $("#filtrar").click(function () {
        var anio = $("#anio").val();
        var mes = $("#mes").val();
        cargarDatos(mes,anio);
    });
    
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