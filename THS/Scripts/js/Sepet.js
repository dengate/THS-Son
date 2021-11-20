$(function () {
    window.onresize = function () {
        if (screen.width <= 608) {
            $('#urunler').addClass('sepeturunlercontainer2');
            $('#urunler').removeClass('sepeturunlercontainer');
        }
        else {

        }
    }
});
$(function () {
    $("body").on("click", "#sil", function () {
        var x = $(this).parent("div").parent("div").children("#id").html();        
        $.ajax(
            {
                url: "/Restoranlar/Sil/",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ urn: x }),
                success: function (gelenbilgi) {
                    $("#urunler").html(gelenbilgi);
                    $.ajax(
                        {
                            url: "/Restoranlar/ToplamFiyat/",
                            type: "POST",
                            contentType: 'application/json; charset=utf-8',
                            success: function (gelenbilgi) {
                                $(".sepeturunlertoplamfiyatyazi").html(gelenbilgi);
                                $.ajax(
                                       {
                                           url: "/Restoranlar/ToplamPisme/",
                                           type: "POST",
                                           contentType: 'application/json; charset=utf-8',
                                           success: function (gelenbilgi) {
                                               $("#pisme").html(gelenbilgi);

                                           },
                                           error: function () {
                                               alert("Hata oluştu");
                                           }
                                       }
                                       );
                            },
                            error: function () {
                                alert("Hata oluştu");
                            }
                        }
                        );
                },
                error: function () {
                    alert("Hata oluştu");
                }
            }
            );
    });

});


$(function () {
    $("body").on("click", "#yorumgonder", function () {
        var x = $("#yorumid").html();
        var y = $("#puansecim").val();
        var z = $("#comments").val();
        $.ajax(
               {
                   url: "/Restoranlar/YorumKontrol/",
                   type: "POST",
                   contentType: 'application/json; charset=utf-8',
                   data: JSON.stringify({ urn: x }),
                   success: function (gelenbilgi) {
                       $("#detayazi").html(gelenbilgi);
                       $.ajax(
                               {
                                   url: "/Restoranlar/YorumGonder/",
                                   type: "POST",
                                   contentType: 'application/json; charset=utf-8',
                                   data: JSON.stringify({ urn: x, pn: y, yrm: z }),
                                   success: function (gelenbilgi) {                                       
                                       $(".modal4").css("visibility", "visible");
                                       $(".modal4").css("opacity", "1");
                                       $("#comments").val("");
                                       $("#puansecim").val("0");
                                   },
                                   error: function () {
                                       alert("Hata oluştu");
                                   }
                               }
                               );                       
                   },
                   error: function () {
                       alert("Hata oluştu");
                   }
               }
               );

       
    });

});

$(function () {
    $("body").on("click", "#odemebtn", function () {
        $(".modal1").css("visibility", "visible");
        $(".modal1").css("opacity", "1");
    });

});

$(function () {
    $("body").on("click", "#yorumyap", function () {
        var val = $(this).parent("div").parent("div").children("#urunid").html();
        $("#yorumid").html(val);
        $(".modal3").css("visibility", "visible");
        $(".modal3").css("opacity", "1");
    });

});

$(function () {
    $("body").on("click", "#mymodal4close", function () {
        $(".modal4").css("visibility", "hidden");
        $(".modal4").css("opacity", "0");
        $(".modal3").css("visibility", "hidden");
        $(".modal3").css("opacity", "0");
    });

});

$(function () {
    $("body").on("click", "#mymodal2close", function () {
        $(".modal2").css("visibility", "hidden");
        $(".modal2").css("opacity", "0");
    });

});

$(function () {
    $("body").on("click", "#mymodal3close", function () {
        $("#comments").val("");
        $("#puansecim").val("0");
        $(".modal3").css("visibility", "hidden");
        $(".modal3").css("opacity", "0");
    });

});

$(function () {
    $("body").on("click", ".odemebutonbox", function () {
        $(".modal1").css("visibility", "hidden");
        $(".modal1").css("opacity", "0");
    });

});
$(function () {
    $("body").on("click", ".close", function () {
        $(".modal1").css("visibility", "hidden");
        $(".modal1").css("opacity", "0");
    });
});

$(function () {
    $("body").on("click", ".close2", function () {
        $(".modal2").css("visibility", "hidden");
        $(".modal2").css("opacity", "0");
    });
});

$(function () {
    $("body").on("click", "#siparisbtn", function () {
        $.ajax(
            {
                url: "/Restoranlar/SiparisVer/",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                success: function (gelenbilgi) {
                    $("#urunler").html("");
                    $("#detayazi").html(gelenbilgi);
                    $.ajax(
                         {
                             url: "/Restoranlar/ToplamFiyat/",
                             type: "POST",
                             contentType: 'application/json; charset=utf-8',
                             success: function (gelenbilgi) {
                                 $(".sepeturunlertoplamfiyatyazi").html(gelenbilgi);
                                 $.ajax(
                                        {
                                            url: "/Restoranlar/ToplamPisme/",
                                            type: "POST",
                                            contentType: 'application/json; charset=utf-8',
                                            success: function (gelenbilgi) {
                                                $("#pisme").html(gelenbilgi);                                               
                                                $(".modal2").css("visibility", "visible");
                                                $(".modal2").css("opacity", "1");
                                            },
                                            error: function () {
                                                alert("Hata oluştu");
                                            }
                                        }
                                        );
                             },
                             error: function () {
                                 alert("Hata oluştu");
                             }
                         }
                         );
                },
                error: function () {
                    alert("Hata oluştu");
                }
            });
    });

});

$(function () {
    $("body").on("click", ".btn", function () {
        $(".loading").css("visibility", "visible");
        $(".loading").css("opacity", "1");
    });
});

$(function () {
    $("body").on("click", "#odemebuton", function () {
        var url = $("#url").val();
        $.ajax(
               {
                   url: "/Restoranlar/OdemeYap/",
                   type: "POST",
                   contentType: 'application/json; charset=utf-8',
                   success: function (gelenbilgi) {
                       if (gelenbilgi == "") {
                           $("#urunler").html("");
                           $(".sepeturunlertoplamfiyatyazi").html("Toplam: 0TL");
                           $(".modal2").css("visibility", "visible");
                           $(".modal2").css("opacity", "1");
                           setInterval("", 2000);
                           window.location = url + "/KullanıcıGırıs/KullanıcıGırısIndex";
                       }
                       else {
                           alert(gelenbilgi);
                       }
                       
                   },
                   error: function () {
                       alert("Hata oluştu");
                   }
               }
               );


    });

});