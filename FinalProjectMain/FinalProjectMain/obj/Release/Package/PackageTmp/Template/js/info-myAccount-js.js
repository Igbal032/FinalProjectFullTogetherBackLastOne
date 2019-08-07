$('.owl-carousel').owlCarousel({
    loop:true,
    margin:20,
    responsiveClass:true,
    responsive:{
        0:{
            items:1,
            nav:true
        },
        543:{
            items:2,
            nav:false
        },
        534:{
            items:2,
            nav:false
        },
        525:{
            items:2,
            nav:false
        },
        600:{
            items:3,
            nav:false
        },
        1000:{
            items:5,
            nav:true,
        }
    }
})

$.fn.datepicker.defaults.format = "dd/mm/yyyy";
$('.datepicker').datepicker({
    
});

$(document).ready(function (params) {
    
    $(".btnShowPassword").on("click", function(e){
        if($("#inputPassword").prop('type')=='text'){
            $("#inputPassword").prop({type:"password"})
        }
        else{
            $("#inputPassword").prop({type:"text"})
        }
    })

     $(".btnShowNewPassword").on("click", function(e){
        if($("#inputNewPassword").prop('type')=='text'){
            $("#inputNewPassword").prop({type:"password"})
        }
        else{
            $("#inputNewPassword").prop({type:"text"})
        }
    })

})
