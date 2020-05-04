

/* scrollToTop */
jQuery(document).ready(function () {

    "use strict";


    //Check to see if the window is top if not then display button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 500) {
            $('.scrollToTop').fadeIn();
        } else {
            $('.scrollToTop').fadeOut();
        }
    });

    //Click event to scroll to top
    $('.scrollToTop').click(function () {
        $('html, body').animate({ scrollTop: 0 }, 400);
        return false;
    });

});


jQuery(document).ready(function () {

    "use strict";


    $(".popup-client > span").on("click", function () {
        $(".account-popup-sec").addClass("active");
        $("html").addClass("no-scroll");
    });

    $(".close-popup").on("click", function () {
        $(".account-popup-sec").removeClass("active");
        $("html").removeClass("no-scroll");
    });

    $('.menu-toggle').on("click", function () {
        $(".menu nav").slideToggle();
    });

    //// Get Header Height //
    //var stick = $(".simple-header.for-sticky").height();
    //$(".simple-header.for-sticky").parent().css({
    //    "padding-top": stick
    //});


    $("header").on("click", function (e) {
        e.stopPropagation();
    });
    $(".menu-item-has-children > a").on("click", function () {
        $(this).parent().siblings().children("ul").slideUp();
        $(this).parent().siblings().removeClass("active");
        $(this).parent().children("ul").slideToggle();
        $(this).parent().toggleClass("active");
        return false;
    });
    //$('#vehicul-geo').slider();

    /*** FIXED Menu APPEARS ON SCROLL DOWN ***/
    //$(window).scroll(function () {
    //    var scroll = $(window).scrollTop();
    //    if (scroll >= 50) {
    //        $(".for-sticky").addClass("sticky");
    //    }
    //    else {
    //        $(".for-sticky").removeClass("sticky");
    //        $("for-sticky").addClass("");
    //    }
    //});


    ///*=================== Parallax ===================*/
    //$('.parallax').scrolly({bgParallax: true});

    //// site preloader -- also uncomment the div in the header and the css style for #preloader
    //$(window).load(function () {
    //    $('#preloader').fadeOut('slow', function () {
    //        $(this).remove();
    //    });
    //});

});
$(document).ready(function () {
    "use strict";
    jQuery('.tp-banner').show().revolution({
        dottedOverlay: "none",
        delay: 16000,
        startwidth: 1170,
        startheight: 700,
        hideThumbs: 200,
        thumbWidth: 100,
        thumbHeight: 50,
        thumbAmount: 5,
        navigationType: "bullet",
        navigationArrows: "solo",
        navigationStyle: "preview1",
        touchenabled: "on",
        onHoverStop: "on",
        swipe_velocity: 0.7,
        swipe_min_touches: 1,
        swipe_max_touches: 1,
        drag_block_vertical: false,
        parallax: "mouse",
        parallaxBgFreeze: "on",
        parallaxLevels: [7, 4, 3, 2, 5, 4, 3, 2, 1, 0],
        keyboardNavigation: "off",
        navigationHAlign: "center",
        navigationVAlign: "bottom",
        navigationHOffset: 0,
        navigationVOffset: 20,
        soloArrowLeftHalign: "left",
        soloArrowLeftValign: "center",
        soloArrowLeftHOffset: 20,
        soloArrowLeftVOffset: 0,
        soloArrowRightHalign: "right",
        soloArrowRightValign: "center",
        soloArrowRightHOffset: 20,
        soloArrowRightVOffset: 0,
        shadow: 0,
        fullWidth: "on",
        fullScreen: "off",
        spinner: "spinner4",
        stopLoop: "off",
        stopAfterLoops: -1,
        stopAtSlide: -1,
        shuffle: "off",
        autoHeight: "off",
        forceFullWidth: "off",
        hideThumbsOnMobile: "off",
        hideNavDelayOnMobile: 1500,
        hideBulletsOnMobile: "off",
        hideArrowsOnMobile: "off",
        hideThumbsUnderResolution: 0,
        hideSliderAtLimit: 0,
        hideCaptionAtLimit: 0,
        hideAllCaptionAtLilmit: 0,
        startWithSlide: 0,
        videoJsPath: "rs-plugin/videojs/",
        fullScreenOffsetContainer: ""
    });
});

$('#sort').change(function () {
    $('#order-form').submit();
});

$('#category').change(function () {
    $('#order-form').submit();
});
$('#vehicleModel').change(function () {
    $('#order-form').submit();
});
$('#make').change(function () {
    $('#order-form').submit();
});
$(document).ready(function () {
    if (window.File && window.FileList && window.FileReader) {
        $("#files").on("change", function (e) {
            var files = e.target.files,
                filesLength = files.length;
            for (var i = 0; i < filesLength; i++) {
                var f = files[i]
                var fileReader = new FileReader();
                fileReader.onload = (function (e) {
                    var file = e.target;
                    $("<span class=\"pip\">" +
                        "<img class=\"imageThumb\" src=\"" + e.target.result + "\" title=\"" + file.name + "\"/>" +
                        "<br/><span class=\"remove\">Remove image</span>" +
                        "</span>").insertAfter("#files");
                    $(".remove").click(function () {
                        $(this).parent(".pip").remove();
                    });

                });
                fileReader.readAsDataURL(f);
            }
        });
    } else {
        alert("Your browser doesn't support to File API")
    }
});
window.onload = () => {
    $('#manufacturedOn').datetimepicker({
        format: 'd.m.Y H:i',
        lang: 'en'
    });
};