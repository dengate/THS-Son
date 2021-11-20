//document.oncontextmenu = function(){return false}

//if(document.layers) {

//    window.captureEvents(Event.MOUSEDOWN);

//    window.onmousedown = function(e){

//        if(e.target==document)return false;

//    }

//}

//else {

//    document.onmousedown = function(){return false}

//}

//$(window).on('keydown', function (event) {
//    if (event.ctrlKey && event.shiftKey && event.keyCode == 73) {
//        return false;
//    }
//});

$(function () {
    $("body").on("click", ".sehirbtn", function () {
        $(".loading").css("visibility", "visible");
        $(".loading").css("opacity", "1");       
    });

});

$(function () {
    $("body").on("click", "#girisbuton", function () {
        $(".loading").css("visibility", "visible");
        $(".loading").css("opacity", "1");
    });

});

$(function () {
    $("body").on("click", "#bilgi", function () {
        $(".modal1").css("visibility", "visible");
        $(".modal1").css("opacity", "1");
    });
});


$(function () {
    $("body").on("click", ".close", function () {
        $(".modal1").css("visibility", "hidden");
        $(".modal1").css("opacity", "0");
    });
});
