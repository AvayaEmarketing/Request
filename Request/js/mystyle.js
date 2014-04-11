$(document).ready(function(){
     
//------- ASSETS--- ---
        
    $('.avnet').mouseover(function(){
         $(".texto-avnet").delay(300) .fadeIn(200);
         $(".bg-avnet").fadeOut(200);
    }).mouseleave(function(){
        $(".texto-avnet").fadeOut(200);
        $(".bg-avnet").delay(400) .fadeIn(200); 
        });

    $('#spain-tooltip').popover('hide');
    
    $('#singapore-tooltip').popover('hide');
        
});


