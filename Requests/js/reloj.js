//Acá va la fecha del evento
var anioFinal = 2014 //Año
var mesFinal = 3 //Mes
var diaFinal = 24 //Día
 
mesFinal -= 1
function faltan()
{
	fechaFinal = new Date(anioFinal,mesFinal,diaFinal)
	fechaActual = new Date()
	diferencia = fechaFinal - fechaActual
	diferenciaSegundos = diferencia /1000
	diferenciaMinutos = diferenciaSegundos/60
	diferenciaHoras = diferenciaMinutos/60
	diferenciaDias = diferenciaHoras/24
	diferenciaHoras2 = parseInt(diferenciaHoras) - (parseInt(diferenciaDias) *24)
	diferenciaMinutos2 = parseInt(diferenciaMinutos) - (parseInt(diferenciaHoras) * 60)
	diferenciaSegundos2 = parseInt(diferenciaSegundos) - (parseInt(diferenciaMinutos) * 60)
	diferenciaDias = parseInt(diferenciaDias)
	if (diferenciaDias < 10 && diferenciaDias > -1){diferenciaDias = "0" + diferenciaDias}
	if(diferenciaHoras2 < 10 && diferenciaHoras2 > -1){diferenciaHoras2 = "0" + diferenciaHoras2}
	if(diferenciaMinutos2 < 10 && diferenciaMinutos2 > -1){diferenciaMinutos2 = "0" + diferenciaMinutos2}
	if(diferenciaSegundos2 < 10 && diferenciaSegundos2 > -1){diferenciaSegundos2 = "0" + diferenciaSegundos2}
	if(diferenciaDias <= 0 && diferenciaHoras2<= 0 && diferenciaMinutos2 <= 0 && diferenciaSegundos2 <= 0)
		{
		diferenciaDias = 0
		diferenciaHoras2 = 0
		diferenciaMinutos2 = 0
		diferenciaSegundos2 = 0
		
//Esto es lo que muestra si alcanza el día o si lo supera		
		document.getElementById('dias').innerHTML =  '' + diferenciaDias 
	document.getElementById('tiempo').innerHTML =  '&nbsp;&nbsp;' + diferenciaHoras2 + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + diferenciaMinutos2 + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + diferenciaSegundos2
		}
	else{

//Acá es que muestra el tiempo exacto que hace falta		
		document.getElementById('dias').innerHTML =  '' + diferenciaDias 
		document.getElementById('tiempo').innerHTML =  '&nbsp;&nbsp;' + diferenciaHoras2 + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + diferenciaMinutos2 + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + diferenciaSegundos2
		setTimeout('faltan()',1000)
		}
}