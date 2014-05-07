//tipo de mensajes ->  default, info,primary,sucess,warning,danger
function mensaje(texto, titulo, tipo) {
    BootstrapDialog.show({
        title:titulo,
        message:texto,
        cssClass: 'type-' + tipo

    })
}

function getInternetExplorerVersion() {
    var ua = window.navigator.userAgent;
    var msie = ua.indexOf('MSIE ');
    var trident = ua.indexOf('Trident/');

    if (msie > 0) {
        // Si es IE 10 o más viejo => retorna el número de versión
        return parseInt(ua.substring(msie + 5, ua.indexOf('.', msie)), 10);
    }

    if (trident > 0) {
        // Si es IE 11 o más nuevo => retorna el número de versión
        var rv = ua.indexOf('rv:');
        return parseInt(ua.substring(rv + 3, ua.indexOf('.', rv)), 10);
    }

    // otro browser
    return false;
}


$(document).keypress(function (e) {
    if (e.which == 13) {
        $.each(BootstrapDialog.dialogs, function (id, dialog) {
            dialog.close();
        });
        $("#login").click();
    }
});


$(document).ready(function () {
    version = getInternetExplorerVersion();
    if (version < 10 && version!==false) {
        $("#BrowserOut").modal('show');
    }
    if (version > 8) {
        $("body").css("background-attachment", "local");
        $("body").css("background-attachment", "local");
        $("body").css("background-attachment", "local");
    } else {
        $("body").css("background-attachment", "fixed");
        $("body").css("background-attachment", "fixed");
        $("body").css("background-attachment", "fixed");
    }
    
    $("#login").click(function () {
        $.prettyLoader();
        var pass = $("#UserPass").val();
        pass = $.sha256(pass);
        var usreg = $("#usuario").val();
        var app = "TRAD";
        var datae = { 'name': usreg, 'pass': pass, 'app': app };
        $.ajax({
            type: "POST",
            url: "admin.aspx/validarIngresoAdmin",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datae),
            dataType: "json",
            success: function (resultado) {
                if (resultado.d === "solicitante") {
                    document.location.href = "solicitante.aspx";
                } else if (resultado.d == "traductor") {
                    document.location.href = "traductor.aspx";
                }  else if (resultado.d == "revisor"){
                    document.location.href = "revisor.aspx";
                }  else if (resultado.d == "administrador") {
                    document.location.href = "administrador.aspx";
                } else {
                    mensaje("Alert, please check your credentials", "Request Application", "danger");
                }
            }
        });
        return false;
    });
});