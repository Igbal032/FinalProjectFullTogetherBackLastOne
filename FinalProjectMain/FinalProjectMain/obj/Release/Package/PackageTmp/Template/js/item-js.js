$('.item-pictures .owl-carousel').owlCarousel({
    loop: true,
    margin: 20,
    responsiveClass: true,
    responsive: {
        0: {
            items: 2,
            nav: true
        },
        543: {
            items: 2,
            nav: false
        },
        534: {
            items: 2,
            nav: false
        },
        525: {
            items: 2,
            nav: false
        },
        600: {
            items: 2,
            nav: false
        },
        1000: {
            items: 3,
            nav: true,
        }
    }
})

$('.carousel-related-product .container .tab-content .owl-carousel').owlCarousel({
    loop: true,
    margin: 20,
    responsiveClass: true,
    responsive: {
        0: {
            items: 1,
            nav: true
        },
        320: {
            items: 2,
            nav: false
        },
        543: {
            items: 2,
            nav: false
        },
        534: {
            items: 2,
            nav: false
        },
        525: {
            items: 2,
            nav: false
        },
        600: {
            items: 2,
            nav: false
        },
        1000: {
            items: 5,
            nav: true,
        }
    }
})

$('.col-about-allCategories .owl-carousel').owlCarousel({
    loop: true,
    margin: 20,
    responsiveClass: true,
    responsive: {
        0: {
            items: 1,
            nav: true
        },
        320: {
            items: 1,
            nav: false
        },
        543: {
            items: 1,
            nav: false
        },
        534: {
            items: 1,
            nav: false
        },
        525: {
            items: 1,
            nav: false
        },
        600: {
            items: 1,
            nav: false
        },
        1000: {
            items: 1,
            nav: true,
        }
    }
})

$('.carouselForCompany .owl-carousel').owlCarousel({
    loop: true,
    margin: 20,
    responsiveClass: true,
    responsive: {
        0: {
            items: 1,
            nav: true
        },
        320: {
            items: 1,
            nav: false
        },
        543: {
            items: 3,
            nav: false
        },
        534: {
            items: 4,
            nav: false
        },
        525: {
            items: 4,
            nav: false
        },
        600: {
            items: 5,
            nav: false
        },
        1000: {
            items: 5,
            nav: true,
        }
    }
})
$(document).ready(function (params) {

    $("#zoom_02").elevateZoom();

    $(".sm-img").on("click", function (e) {
        console.log("sdsad")
        $(".mainPicture").attr('src', $(this).attr("src"))
        $(".mainPicture").attr('data-zoom-image', $(this).attr("src"))
    })

    $(".tab-single-page .nav .nav-item a").on("click", function (e) {
        if ($(this).hasClass("description")) {
            $(this).css({ "color": "red" })
            $(".product-detail").css({ "color": "black" })
            $(".review").css({ "color": "black" })
        }
        else if ($(this).hasClass("product-detail")) {
            $(this).css({ "color": "red" })
            $(".description").css({ "color": "black" })
            $(".review").css({ "color": "black" })

        }
        else if ($(this).hasClass("review")) {
            $(this).css({ "color": "red" })
            $(".product-detail").css({ "color": "black" })
            $(".description").css({ "color": "black" })

        }
    })
})