//tipo de mensajes ->  anim error, success, info, anim warning
function mensaje(texto,titulo,tipo) {
    new Messi(texto, {
        title: titulo,
        titleClass: tipo, modal: true, buttons: [{ id: 0, label: 'Close', val: 'X' }]
    });
}

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

function validar(obj) {
    var respuesta = 0;
    for (var i in obj) {
        if (obj[i] == null || obj[i].length < 1 || /^\s+$/.test(obj[i])) {
            if (i === 'twitter') {
                respuesta = respuesta + 0;
                $("#" + i).css('background', '#FFF');
            } else {
                respuesta = respuesta + 1;
                $("#" + i).css('background', '#E2E4FF');
            }
        } else {
			if (i === 'inputEmail')  {
				var correo= /^[A-Za-z][A-Za-z0-9_\.-]*@[A-Za-z0-9_-]+\.[A-Za-z0-9_.]+[A-za-z]$/;
				if(!correo.exec(obj[i]))    
				{
				    respuesta = respuesta +1;
					$("#"+i).css('background','#E0AAAA');
				} else {
				    respuesta = respuesta +0;
					$("#"+i).css('background','#FFF');
				}		
			} else {
			    respuesta = respuesta + 0;
			    $("#"+i).css('background','#FFF');	
			}
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

function getForm() {
    
    var inputEmail = $("#inputEmail");
    var inputName = $("#inputName");
    var inputLast = $("#inputLast");
    var inputCompany = $("#inputCompany");
    var inputPhone = $("#inputPhone");
    var inputJob = $("#inputJob");
    var inputCountry = $("#inputCountry");
    var twitter = $("#inputTwitter");
    var city = decodeURI(QueryString.city);
    var authorization = $("#authorize");
	var isChecked = $('#authorize').is(':checked');
	var Relationship = $("#inputRelationship");
	
    var formulario = new Object();
	    formulario.inputName = inputName.val();
	    formulario.inputLast = inputLast.val();
	    formulario.inputCompany = inputCompany.val();
	    formulario.inputEmail = inputEmail.val();
	    formulario.inputPhone = inputPhone.val();
	    formulario.inputJob = inputJob.val();
	    formulario.inputCountry = inputCountry.val();
	    formulario.twitter = twitter.val();
	    formulario.city = city;
		formulario.authorization = (isChecked == true ? "yes":"no");
		formulario.Relationship = Relationship.val();
		
		
    return formulario;
}



function getIdioma(city) {
    var idioma;
	idiomas = {
	  // Ingles
	  'New York': "Ingles",
	  'San Juan, Puerto Rico': "Ingles",
	  'Toronto': "Ingles",
	  'Atlanta, GA': "Ingles",
	  'Cincinnati, OH': "Ingles",
	  'Seattle, WA': "Ingles",
	  'Boston, MA': "Ingles",
	  'Montreal': "Ingles",
	  'Dallas, TX': "Ingles",
	  'St. Paul, MN': "Ingles",
	  'Irvine, CA': "Ingles",
	 
	  // Español
	  'México': "Español",
	  'Bogotá': "Español",
	  'Buenos Aires': "Español",
	 
	  // Portugues
	  'São Paulo': "Portugues",
	  
	  // Frances
	  'Montréal': "Frances"
	  
	}
    
	if (idiomas[city] === "Ingles") {
	        idioma = "Ingles";
	}
	else if (idiomas[city] == "Español") {
	    idioma = "Español";
	}
	else if (idiomas[city] == "Portugues")  {
	    idioma = "Portugues";
	}
	else if (idiomas[city] == "Frances")  {
	    idioma = "Frances";
	}
	
	return idioma;
}

function envioDeCorreo(jsonCorreo) {
    var string1 = "";
	var items = $.parseJSON(jsonCorreo);
	if (items.authorization === "yes") {
		$.post('../Controller/enviar_correo.php', {datos : jsonCorreo},
        function(data){
           if (data != 0) {
               string1 = "The information has been registered successfully, please wait..";
           }
           else {
               string1 = "The information has been registered successfully, But has been a failure to send mail";
           }
		   limpiarCampos();
		   setTimeout(function(){  
                 url = "http://www2.avaya.com/am/camp/ch/apcd2014/toronto/toronto-thanks.html";  
                 $(location).attr('href',url);  
            },500);
			return false;
        });
	}
     
} 
	

function registrarInfo(formulario) {
    var idioma = getIdioma(formulario.city);
    
    var datae = { 'inputEmail': formulario.inputEmail, 'inputName': formulario.inputName, 'inputLast': formulario.inputLast, 'inputCompany': formulario.inputCompany, 'inputPhone': formulario.inputPhone, 'inputJob': formulario.inputJob, 'inputCountry': formulario.inputCountry, 'twitter': formulario.twitter, 'city': formulario.city, 'authorization': formulario.authorization, 'idioma':idioma,'Relationship':formulario.Relationship };
    $.ajax({
        type: "POST",
        url: "register.aspx/putData",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datae),
        dataType: "json",
        success: function (resultado) {
            if (resultado.d !== -1) {
                if (idioma === "Ingles") {
                    document.location.href = "thanks.html?city="+formulario.city;
                } else if (idioma === "Español") {
                    document.location.href = "thanks-es.html?city=" + formulario.city;
                } else if (idioma === "Portugues") {
                    document.location.href = "thanks-po.html?city=" + formulario.city;
                } else if (idioma === "Frances") {
                    document.location.href = "thanks-fr.html?city=" + formulario.city;
                }
            } else {
                mensaje("Alert, please try again", "Avaya PCD", "anim error");
            }
        }
    });
    return false;
}

function validaMail(cityevent, mail) {

    var datae = { 'cityevent': cityevent, 'email': mail };
    $.ajax({
        type: "POST",
        url: "register.aspx/validateEmail",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datae),
        dataType: "json",
        success: function (resultado) {
            if (resultado.d === "ok") {
                mensaje("The email has already been registered previously for this event", "Avaya PCD", "anim error");
                $("#inputEmail").val("");
            } else {
                if (validaFormatoMail(mail)) {
                    traerDatos(mail);
                }
            }
        }
    });
    return false;
}

function validaFormatoMail(mail) {
    var rta;
    var correo = /^[A-Za-z][A-Za-z0-9_\.-]*@[A-Za-z0-9_-]+\.[A-Za-z0-9_.]+[A-za-z]$/;
    if (!correo.exec(mail)) {
        rta = false;
    } else {
        rta = true;
    }
    return rta;
}
	


$(document).ready(function () {

    var city = decodeURI(QueryString.city);
    var title = decodeURI(QueryString.title);

    if ((title === undefined) || (title === "undefined")) {
        if (city != undefined) {
            var idioma = getIdioma(city);
            if (idioma === "Ingles") {
				
				if ((city == "New York") || (city == "San Juan, Puerto Rico") || (city == "Toronto") || (city == "Atlanta, GA") || (city == "Cincinnati, OH")) {
					document.location.href = "http://www4.avaya.com/usa/events/APCD-2014/register-close.html?city="+city;
				} else {
				    document.location.href = "register.aspx?title=Avaya Partner Connection Day  -  " + city + "&city=" + city;	
				}
                
            } else if (idioma === "Español") {
				if ((city == "México") || (city == "Bogotá")) {
                    document.location.href = "http://www4.avaya.com/usa/events/APCD-2014/register-close-es.html?city="+city;
				} else {
					document.location.href = "register-es.aspx?title=Avaya Partner Connection Day  -  " + city + "&city=" + city;
				}
            } else if (idioma === "Portugues") {
				if (city == "São Paulo")  {
                    document.location.href = "http://www4.avaya.com/usa/events/APCD-2014/register-close-po.html?city="+city;
				} else {
					document.location.href = "register-po.aspx?title=Avaya Partner Connection Day  -  " + city + "&city=" + city;
				}
			} else if (idioma === "Frances") {
                document.location.href = "register-fr.aspx?title=Journée Connexion Partenaires Avaya  -  " + city + "&city=" + city;
            }

        }
    }
    else {
        $("#titulos").html(title);
    }
    


    $('#Register').click(function () {
		var formulario = getForm();
		var validado = validar(formulario);
		if (validado) {
		    registrarInfo(formulario);
		} else {
			mensaje("Please check the Mandatory fields or Email Format","Register","anim error")
		}
		return false;
    });

    $('#inputEmail').blur(function () {
        var cityevent = city;
        var mail = $("#inputEmail").val();
        validaMail(cityevent, mail);
    });

	
	$("#lblchecked").change(function () {
	  $('#lblchecked :checkbox:checked').each(function() {
          regiones = regiones + $(this).val()+ ',';
      });
	}).change();
	
	
	
	$('#Register2').click(function () {
		var formulario2 = getForm2();
		var validado = validar(formulario2);
		if (validado){
			var jsonDatos = JSON.stringify(formulario);
		    $.post('../Controller/Registro.php', {jsonDatos : jsonDatos},
		    function(data){
		        if (data != 0) {
				    envioDeCorreo(jsonDatos);
				}
		        else {
				    mensaje("Error Register Data.!","Avaya PCD","anim error")
		        }
		        return false;   
		    }); 
		} else {
			mensaje("Please check the Mandatory fields or Email Format","Avaya PCD","anim error")
		}
	});
        
});

function traerDatos(email) {
    var datae = { 'email': email };
    $.ajax({
        type: "POST",
        url: "register.aspx/getDatosOld",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datae),
        dataType: "json",
        success: function (resultado) {
            if (resultado.d !== "null") {
                var jposts = resultado.d;
                var item = $.parseJSON(jposts);

                $("#inputEmail").val(item[0].email);
                $("#inputName").val(item[0].nombre);
                $("#inputLast").val(item[0].apellido);
                $("#inputCompany").val(item[0].empresa);
                $("#inputJob").val(item[0].titleContact);
                $("#inputPhone").val(item[0].telefono);
                $("#inputCountry ").val(item[0].idCountry);
            }
        }
    });
    return false;
}