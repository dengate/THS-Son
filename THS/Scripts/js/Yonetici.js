$(function () {
    $(window).ready(function () {
        $(".sehirsec").children("#SemtSecim").children(".gizli").wrap('<div>');
    });
});

$(function () {
    $("body").on("click", "#giris", function () {
        var ad = $("#ad").val();
        var sifre = $("#sifre").val();
        $.ajax(
            
                      {
                          url: "/Yonetici/Giris/",
                          type: "POST",
                          contentType: 'application/json; charset=utf-8',
                          data: JSON.stringify({ ad: ad, sifre: sifre }),
                          success: function (gelenbilgi) {
                              window.location = "http://localhost:65433/Yonetici/" + gelenbilgi;
                          },
                          error: function () {
                              alert("Hata oluştu");
                          }
                      }
                      );

    });
});

$(function () {
    $("body").on("click", "#ekle", function () {
        var isletmead = $("#isletmeadi").val();
        var mail = $("#mail").val();
        var adres = $("#adres").val();
        var tip = $("#tip").val();
        var telefon = $("#telefon").val();
        var sayi = $("#sayi").val();
        var sifre = $("#sifre").val();
        var sec = $("#scrty").val();
        var yoneticiad = $("#yoneticiad").val();
        var yoneticisoyad = $("#yoneticisoyad").val();
        var semtid = $("#isletmeSecim").find(":selected").prev().find(".gizli2").html();
        if (isletmead == null || mail == null || adres == null || tip == null || telefon == null || sayi == null || sifre == null || yoneticiad == null || yoneticisoyad == null || semtid == null) {
            alert("Boş Bırakılamaz!");
        }
        else {
            $.ajax(

                              {
                                  url: "/Yonetici/Ekle/",
                                  type: "POST",
                                  contentType: 'application/json; charset=utf-8',
                                  data: JSON.stringify({ isletmead: isletmead, mail: mail, adres: adres, tip: tip, telefon: telefon, sayi: sayi, sifre: sifre, sec: sec, yoneticiad: yoneticiad, yoneticisoyad: yoneticisoyad, semtid: semtid }),
                                  success: function (gelenbilgi) {
                                      alert("Eklendi!");
                                  },
                                  error: function () {
                                      alert("Hata oluştu");
                                  }
                              }
                              );
        }

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
                        url: "/Yonetici/Restoranlar2/",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ id: y }),
                        success: function (gelenbilgi) {
                            $("#isletmeSecim").html(gelenbilgi);
                            $(".isletmesec").children("#isletmeSecim").children(".gizli2").wrap('<div>');
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
                        url: "/Yonetici/Restoranlar2/",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ id: y }),
                        success: function (gelenbilgi) {
                            $("#isletmeSecim").html(gelenbilgi);
                            $(".isletmesec").children("#isletmeSecim").children(".gizli2").wrap('<div>');
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