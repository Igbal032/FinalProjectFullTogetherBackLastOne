$('.carousel').carousel({
    })
// $(".modal").modal('show')


$('.main-carousel .container-fluid .row .owl-carousel').owlCarousel({
    loop: false,
    autoplay: true,
    autoplayTimeout: 2000,
    autoplayHoverPause: true,
    responsiveClass: true,
    responsive: {
        0: {
            items: 1,
            nav: true
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