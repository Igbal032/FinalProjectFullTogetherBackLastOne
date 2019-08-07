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

$(document).ready(function (params) {
    var productCount=0;
    $(".increaseCountt").on("click",function (e) {
        if(productCount<3){
            productCount++;
            $(".inputForCount").val(productCount);
        }
        else{
            alert("You cannot more than 3")
        }
  
    })

    $(".decreaseCountt").on("click",function (e) {
        if(productCount>0){
            productCount--;
            $(".inputForCount").val(productCount);
        }
        else{
            alert("Count is 0")
        }
      
    })
})