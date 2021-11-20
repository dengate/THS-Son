$(document).ready(function () {
    $(window).scroll(function () {
        if ($(this).scrollTop() > 0) {
            $('.menu').addClass('sabitleme');
        } else {
            $('.menu').removeClass('sabitleme');
        }
    });
});


