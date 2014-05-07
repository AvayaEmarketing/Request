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

function validarJson(divs_id) {
    if ((divs_id.substring(divs_id.length - 1, divs_id.length)) === ']')
        divs_id = divs_id.substring(0, divs_id.length - 1);

    if ((divs_id.substring(0, 1)) === '[')
        divs_id = divs_id.substring(1, divs_id.length);

    return divs_id;

}

function getInfo() {

    $.ajax({
        type: "POST",
        url: "rev_details.aspx/getInfo",
        contentType: "application/json; charset=utf-8",
        data: '{}',
        dataType: "json",
        success: function (resultado) {
            if (resultado.d === "fail") {
                document.location.href = "admin.aspx";
            } else {
                var jposts = resultado.d;
                jposts = validarJson(jposts);
                var item = $.parseJSON(jposts);

                $("#user_id").val(item.usuario);
                $("#role").val(item.rol);
                $("#Firstname").val(item.nombre);
                $("#Lastname").val(item.apellido);
                $("#Email").val(item.email_empresa);
                $("#Address").val(item.direccion);
                $("#Phone").val(item.celular);

            }

        },
        error: function (error) {
            message("Data error, please try again", "Attention", "danger");
        }

    });
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
    getInfo();

    $("#exit").click(function () {
        cerrarSession();
    });

});

function cerrarSession() {
    $.ajax({
        type: "POST",
        url: "rev_details.aspx/cerrarSession",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (resultado) {
            document.location.href = "admin.aspx";
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