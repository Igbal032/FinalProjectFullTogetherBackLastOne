

//Dropdown-menu

$('body').on('mouseenter mouseleave','.dropdown-hover',function(e){
    let dropdown=$(e.target).closest('.dropdown-hover');
     dropdown.addClass('show');
     
   setTimeout(function(){
         dropdown[dropdown.is(':hover')?'addClass':'removeClass']('show');
     },300);
 });



 // Open Sidebar

 $(".navbar-main .openSideBar").on("click", function (params) {

    if($(".navbar-main .container .sideBarForMobile").css('left') === '-40px'){

        $(".navbar-main .container .sideBarForMobile").css({'left':'-500%','transition':'1s'})
    }
    else
     $(".navbar-main .container .sideBarForMobile").css({'left':'-40px','transition':'1s'})

 })


// // //  For Language // //  //  

$(".language-dropdown .dropdown-menu a.az-lang").click(function(e){
    $(".img-main-flag").attr('src','./img/Flag/az-flag.jpg')
 })
 
 $(".language-dropdown .dropdown-menu a.en-lang").click(function(e){
     $(".img-main-flag").attr('src','./img/Flag/en-flag.jpg')
  })

  // // // For NavBar // // // 

$(window).scroll(function () {
    var scrollValForNav = $(window).scrollTop();
    if (scrollValForNav >= 238) {
        $(".navbar-main .navbar").addClass("fixed-top");
        $('.shopping-icon-fornav2').attr("style", "display: block !important");
        $('#searchForNav2').attr("style", "display: block !important");

      
    }
    else {
        $(".navbar-main .navbar").removeClass("fixed-top");
        $('.shopping-icon-fornav2').attr("style", "display: none !important");
        $('#searchForNav2').attr("style", "display: none !important");

    }

    if($(window).width() < 992)
    {
        $('.shopping-icon-fornav2').attr("style", "display: none !important");
        $('#searchForNav2').attr("style", "display: none !important");
    }
    else
    {
        $('.shopping-icon-fornav2').attr("style", "display: block !important");
        $('#searchForNav2').attr("style", "display: block !important");
    }

    var scrollValForBackTop = $(window).scrollTop();
    if (scrollValForBackTop >= 50) {
       $(".footer-bottom-part .fa-chevron-up").css({'display':'block'});  

    }
       else {
        $(".footer-bottom-part .fa-chevron-up").css({'display':'none'});      
    }
})

// // //  For Currency // //  //  

$(".currency-dropdown ul a").click(function(e) {
    console.log($(this).text())
    $(".currency-dropdown .mainCurrencyText").text($(this).text())
})

$(document).ready(function(params) {

    Waves.attach(".rippler");
    Waves.init();

    //$(".search-shop .prepend-input-for-search .dropdown ul a").on("click",function(e){

    //    $("#selectCategoryInSearch").text($(this).text())
    //})

    $(".shop.bg").tooltip({ title: "Karta Əlavə Et", placement: "bottom" })
    $(".likeButton.bg").tooltip({ title: "Bəyən", placement: "bottom" })
    $(".star.bg").tooltip({ title: "Ulduz", placement: "bottom" })
    $(".wishlist.bg").tooltip({ title: "İstək Siyahısına Əlavə Et", placement: "bottom" })

})

