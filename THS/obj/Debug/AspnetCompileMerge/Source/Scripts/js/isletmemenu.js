$(function () {
    $("body").on("click", "#buton", function () {
        var val3 = $(this).parent("div").parent("div").children("#id").html();
        var val = $(this).parent("div").parent("div").children(".price").children("#urunadi").html();
        var val2 = $(this).parent("div").parent("div").children(".price").children("#urunfiyati").html();
        var val4 = $(this).parent("div").parent("div").children(".price").children("#urunaciklamasi").html();
        var val5 = $(this).parent("div").parent("div").children(".price").children("#urunkalorisi").html();
        var val6 = $(this).parent("div").parent("div").children(".price").children("#urunpisme").html();
        $("#popupid").html(val3);
        $("#popupad2").val(val);
        $("#popupfiyat2").val(val2);
        $("#popupaciklama2").val(val4);
        $("#popupkalori2").val(val5);
        $("#popuppisme2").val(val6);
        $.ajax(
                 {
                     url: "/isletme/AltKategoriGetir/",
                     type: "POST",
                     contentType: 'application/json; charset=utf-8',
                     data: JSON.stringify({ id: val3 }),
                     success: function (gelenbilgi) {
                         $("#AltKategoriSecim").html(gelenbilgi);
                     },
                     error: function () {
                         alert("Hata oluştu");
                     }
                 }
        );
        $(".modal1").css("visibility", "visible");
        $(".modal1").css("opacity", "1");

    });
});
$(function () {
    $("body").on("click", "#guncelle", function () {
        var x = $("#popupid").html();
        var y = $("#popupad2").val();
        var z = $("#popupfiyat2").val();
        var a = $("#popupaciklama2").val();
        var b = $("#popupkalori2").val();
        var c = $("#popuppisme2").val();
        var altktgr = $("#AltKategoriSecim").find(":selected").prev().html();
        $.ajax(
           {
               url: "/isletme/Guncelle/",
               type: "POST",
               contentType: 'application/json; charset=utf-8',
               data: JSON.stringify({ id: x, ad: y, fiyat: z, aciklama: a, kalori: b, pisme: c, ktgrid: altktgr }),
               success: function (gelenbilgi) {
                   alert("Başarıyla Güncellendi");
                   $(".modal1").css("visibility", "hidden");
                   $(".modal1").css("opacity", "0");
                   location.reload();
               },
               error: function () {
                   alert("Hata oluştu");
               }
           }
           );
    });
});

$(function () {
    $("body").on("click", "#sil", function () {
        var val3 = $(this).parent("div").children("#id").html();
        $("#detayurunid").html(val3);
        $(".modal2").css("visibility", "visible");
        $(".modal2").css("opacity", "1");
    });
});

$(function () {
    $("body").on("click", ".carpi2", function () {
        $(".modal2").css("visibility", "hidden");
        $(".modal2").css("opacity", "0");
        $(".modal3").css("visibility", "hidden");
        $(".modal3").css("opacity", "0");
        $(".modal4").css("visibility", "hidden");
        $(".modal4").css("opacity", "0");
    });
});

$(function () {
    $("body").on("click", ".carpi", function () {
        $(".modal1").css("visibility", "hidden");
        $(".modal1").css("opacity", "0");
    });
});

$(function () {
    $("body").on("click", "#yorumlar", function () {
        var val = $("#detayurunid").html();
        $.ajax(
          {
              url: "/isletme/Urunsil/",
              type: "POST",
              contentType: 'application/json; charset=utf-8',
              data: JSON.stringify({ id: val }),
              success: function (gelenbilgi) {
                  alert("Başarıyla Silindi");
                  location.reload();
              },
              error: function () {
                  alert("Hata oluştu");
              }
          }
          );
        $(".modal2").css("visibility", "hidden");
        $(".modal2").css("opacity", "0");
    });
});

$(function () {
    $("body").on("click", "#ktgr", function () {
        $(".modal3").css("visibility", "visible");
        $(".modal3").css("opacity", "1");
    });
});

$(function () {
    $("body").on("click", "#altktgr", function () {
        $.ajax(
          {
              url: "/isletme/KategoriGetir/",
              type: "POST",
              contentType: 'application/json; charset=utf-8',
              success: function (gelenbilgi) {
                  $("#KategoriSecim").html(gelenbilgi);
              },
              error: function () {
                  alert("Hata oluştu");
              }
          }
          );
        $(".modal4").children(".Detay").children("#altktgrdiv").children("#altktgrdiv2").children("#KategoriSecim").children(".gizli").wrap('<div>');
        $(".modal4").css("visibility", "visible");
        $(".modal4").css("opacity", "1");
    });
});

$(function () {
    $("body").on("click", "#ktgrekle", function () {
        var ad = $("#ktgradi").val();
        if (ad == "") {
            alert("Kategori Adı Boş Bırakılamaz!");
        }
        else {
            $.ajax(
          {
              url: "/isletme/KategoriEkle/",
              type: "POST",
              contentType: 'application/json; charset=utf-8',
              data: JSON.stringify({ ad: ad }),
              success: function (gelenbilgi) {
                  $(".modal3").css("visibility", "hidden");
                  $(".modal3").css("opacity", "0");
                  alert("Başarıyla Eklendi!");
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
    $("body").on("click", "#altktgrekle", function () {
        var ad = $("#altktgradi").val();
        var id = $("#KategoriSecim").find(":selected").prev().html();
        if (ad == "" || id == "000" || id == null) {
            alert("Lütfen Adı girdiğinizden ve bir Kategori seçtiğinizden emin olun!");
        }
        else {
            $.ajax(
                 {
                     url: "/isletme/AltKategoriEkle/",
                     type: "POST",
                     contentType: 'application/json; charset=utf-8',
                     data: JSON.stringify({ ad: ad, id: id }),
                     success: function (gelenbilgi) {
                         $(".modal4").css("visibility", "hidden");
                         $(".modal4").css("opacity", "0");
                         alert("Başarıyla Eklendi!");
                     },
                     error: function () {
                         alert("Hata oluştu");
                     }
                 }
                 );
        }
    });
});