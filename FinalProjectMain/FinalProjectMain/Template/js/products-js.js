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

$(document).ready(function(e){

   $(".sorting-filter .product-grid-list .grid a i").on("click",function (e) {
       
   if($(this).hasClass("fa-th")){
       $(this).css({"color":"red"})
       $(".fa-align-justify").css({"color":"black"})
   }
   else{
       $(".fa-align-justify").css({"color":"red"})
       $(".fa-th").css({"color":"black"})
   }

   })


   $(".pagination-part .pagination .page-item a").on("click",function (e){

    if($(".page-link").hasClass("bg-danger")){

        $(".page-link").removeClass("bg-danger")

        $(this).addClass("bg-danger")
    }

   })

})