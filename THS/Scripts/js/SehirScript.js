$(function () {
    $(window).ready(function () {
        $(".aramacontainer").children(".sehirsec").children("#SemtSecim").children(".gizli").wrap('<div>');
    });
});

$(function () {
    $("#SemtSecim").click(function () {
        if ($(this).find(":selected").hasClass("gizli")) {
            $(this).find("#secili").prop('selected', true);
        }
        
        else {
            var y = $(this).find(":selected").prev().find(".gizli").html();
            if (y == "000" || $("#SemtSecim").val() == null) {

            }
            else {

                $.ajax(
                    {
                        url: "/Restoranlar/Restoranlar2/",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ id: y }),
                        success: function (gelenbilgi) {
                            $("#isletmeSecim").html(gelenbilgi);

                        },
                        error: function () {
                            alert("Hata oluştu");
                        }
                    }
                    );
            }
        }
    });

});

$(function () {
    $("#SemtSecim").change(function () {
        if ($(this).find(":selected").hasClass("gizli")) {
            $(this).find("#secili").prop('selected', true);
        }

        else {
            var y = $(this).find(":selected").prev().find(".gizli").html();
            if (y == "000" || $("#SemtSecim").val() == null) {

            }
            else {

                $.ajax(
                    {
                        url: "/Restoranlar/Restoranlar2/",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ id: y }),
                        success: function (gelenbilgi) {
                            $("#isletmeSecim").html(gelenbilgi);

                        },
                        error: function () {
                            alert("Hata oluştu");
                        }
                    }
                    );
            }
        }
    });

});

$(function () {
    $("#isletmeSecim").click(function () {        
        var temp = $("#isletmeSecim").val();
        var url = $("#url").val();
        
        if (temp == "İşletmeler" || temp == null) {

        }
        else {
            $(".loading").css("visibility", "visible");
            $(".loading").css("opacity", "1");
            var val = "";
            //for (var i = 0; i < temp.length; i++) {
            //    if (temp[i] != ' ') {
            //        val += temp[i];
            //    }
            //}
            var sehir = $("#sehirismi").html();
            var semt = $("#SemtSecim").find(":selected").html();
            window.location = url + "/Restoranlar/" + "GetId" + "?urunadi=" + sehir + "_" + semt + "_" + temp + "~2";
        }
    });

});

$(function () {
    $("#isletmeSecim").change(function () {
        var temp = $("#isletmeSecim").val();
        var url = $("#url").val();
        if (temp == "İşletmeler" || temp == null) {

        }
        else {
            $(".loading").css("visibility", "visible");
            $(".loading").css("opacity", "1");
            var val = "";
            //for (var i = 0; i < temp.length; i++) {
            //    if (temp[i] != ' ') {
            //        val += temp[i];
            //    }
            //}
            var sehir = $("#sehirismi").html();
            var semt = $("#SemtSecim").find(":selected").html();
            window.location = url + "/Restoranlar/" + "GetId" + "?urunadi=" + sehir + "_" + semt + "_" + temp + "~2";
        }
    });

});


function turkce(temp) {
    var temp2 = "";
    for (var i = 0; i < temp.length; i++) {
        if (temp[i] == 'Ç') {
            temp2 += 'C';
        }
        else if (temp[i] == 'ç') {
            temp2 += 'c';
        }
        else if (temp[i] == 'Ü') {
            temp2 += 'U';
        }
        else if (temp[i] == 'ü') {
            temp2 += 'u';
        }
        else if (temp[i] == 'Ö') {
            temp2 += 'O';
        }
        else if (temp[i] == 'ö') {
            temp2 += 'o';
        }
        else if (temp[i] == 'Ğ') {
            temp2 += 'G';
        }
        else if (temp[i] == 'ğ') {
            temp2 += 'g';
        }
        else if (temp[i] == 'Ş') {
            temp2 += 'S';
        }
        else if (temp[i] == 'ş') {
            temp2 += 's';
        }
        else if (temp[i] == 'İ') {
            temp2 += 'I';
        }
        else
            temp2 += temp[i];
    }

    return temp;
}
