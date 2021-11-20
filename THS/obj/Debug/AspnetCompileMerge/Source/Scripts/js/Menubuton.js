$(function () {
    window.onclick = function (event) {

        var x = $(event.target).attr("class");
        if (screen.width < 439) {
            if (x == "butonn") {

                if ($(".menubutonlar").attr("class") == "menubutonlar test") {
                    $(".menubutonlar").css("height", "0");
                    $(".menubutonlar").css("visibility", "hidden");
                    $(".menubutonlar").css("opacity", "0");                    
                    $('.menubutonlar').removeClass('test');
                }
                else {
                    $('.menubutonlar').addClass('test');
                    $(".menubutonlar").css("height", "201px");
                    $(".menubutonlar").css("visibility", "visible");
                    $(".menubutonlar").css("opacity", "1");
                }

            }

            else {
                $(".menubutonlar").css("height", "0");
                $(".menubutonlar").css("visibility", "hidden");
                $(".menubutonlar").css("opacity", "0");
                $('.menubutonlar').removeClass('test');
            }
        }


    }
});


$(function () {
    window.onresize = function () {
        if (screen.width >= 439) {
            $(".menubutonlar").css("height", "67px");
            $(".menubutonlar").css("visibility", "visible");
            $(".menubutonlar").css("opacity", "1");
            $('.menubutonlar').addClass('test');
        }
        else {
            $(".menubutonlar").css("height", "0");
            $(".menubutonlar").css("visibility", "hidden");
            $(".menubutonlar").css("opacity", "0");
            $('.menubutonlar').removeClass('test');
        }
    }
});

$(function () {
    $("body").on("click", ".btn", function () {
        $(".loading").css("visibility", "visible");
        $(".loading").css("opacity", "1");
    });
});