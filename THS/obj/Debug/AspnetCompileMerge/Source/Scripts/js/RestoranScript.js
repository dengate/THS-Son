$(function () {
    $(window).ready(function () {
        $(".aramacontainer").children(".sehirsec").children("#KategoriSecim").children(".gizli").wrap('<div>');
    });
});

$(function () {
    $("#AltkategoriSecim").click(function () {

        if ($(this).find(":selected").hasClass("gizli")) {
            $(this).find("#secili").prop('selected', true);
        }
        else {
            var x = $(this).find(":selected").prev().find(".gizli").html();
            var y = $("#KategoriSecim").find(":selected").prev().find(".gizli").html();
            if ($("#AltkategoriSecim").val() == null) {

            }
            else {
                $.ajax(
                    {
                        url: "/Restoranlar/Kategorize/",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ urn: x, ktgr: y }),
                        success: function (gelenbilgi) {

                            $("#urunler").html(gelenbilgi);
                            $("#viewport").attr("content", "width=device-width, initial-scale=1");
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
    $("#SiralamaSecim").change(function () {
        var secim = $("#SiralamaSecim").val();

            if (secim == 0) {

            }
            else {
                $.ajax(
                    {
                        url: "/Restoranlar/Sirala/",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ secim: secim }),
                        success: function (gelenbilgi) {
                            $("#urunler").html(gelenbilgi);
                            $("#viewport").attr("content", "width=device-width, initial-scale=1");
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
    $("#AltkategoriSecim").change(function () {
        if ($(this).find(":selected").hasClass("gizli")) {
            $(this).find("#secili").prop('selected', true);
        }
        else {
            var x = $(this).find(":selected").prev().find(".gizli").html();
            var y =$("#KategoriSecim").find(":selected").prev().find(".gizli").html();
            if ($("#AltkategoriSecim").val() == null) {

            }
            else {
                $.ajax(
                    {
                        url: "/Restoranlar/Kategorize/",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ urn: x ,ktgr: y}),
                        success: function (gelenbilgi) {
                            $("#urunler").html(gelenbilgi);
                            $("#viewport").attr("content", "width=device-width, initial-scale=1");
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
    $("body").on("click", "#detaybtn", function () {
        var y = $(this).parent("div").parent("div").children("#urnid").html();
        $("#detayurunid").html(y);
        $.ajax(
            {
                url: "/Restoranlar/Detay/",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ urn: y }),
                success: function (gelenbilgi) {
                    $("#detayaciklama").html(gelenbilgi);
                    $(".modal2").css("visibility", "visible");
                    $(".modal2").css("opacity", "1");
                    $.ajax(
                            {
                                url: "/Restoranlar/UrunPuan/",
                                type: "POST",
                                contentType: 'application/json; charset=utf-8',
                                data: JSON.stringify({ urn: y }),
                                success: function (gelenbilgi) {
                                    $("#detaypuan").html(gelenbilgi);
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
    $("#KategoriSecim").click(function () {
        if ($(this).find(":selected").hasClass("gizli")) {
            $(this).find("#secili").prop('selected', true);
        }
        else {
            var y = $(this).find(":selected").prev().find(".gizli").html();
            if ($("#KategoriSecim").val() == null) {

            }
            else {
                $.ajax(
                    {
                        url: "/Restoranlar/AltKategori/",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ ktgr: y }),
                        success: function (gelenbilgi) {
                            $("#AltkategoriSecim").html(gelenbilgi);
                            $(".aramacontainer").children(".isletmesec").children("#AltkategoriSecim").children(".gizli").wrap('<div>');
                            $.ajax(
                                {
                                    url: "/Restoranlar/Kategorize2/",
                                    type: "POST",
                                    contentType: 'application/json; charset=utf-8',
                                    data: JSON.stringify({ ktgr: y }),
                                    success: function (gelenbilgi) {
                                        $("#urunler").html(gelenbilgi);
                                        $("#viewport").attr("content", "width=device-width, initial-scale=1");
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
            }
        }
    });

});

$(function () {
    $("#KategoriSecim").change(function () {
        if ($(this).find(":selected").hasClass("gizli")) {
            $(this).find("#secili").prop('selected', true);
        }
        else {
            var y = $(this).find(":selected").prev().find(".gizli").html();
            if ($("#KategoriSecim").val() == null) {

            }
            else {
                $.ajax(
                    {
                        url: "/Restoranlar/AltKategori/",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ ktgr: y }),
                        success: function (gelenbilgi) {
                            $("#AltkategoriSecim").html(gelenbilgi);
                            $(".aramacontainer").children(".isletmesec").children("#AltkategoriSecim").children(".gizli").wrap('<div>');
                            $.ajax(
                             {
                                 url: "/Restoranlar/Kategorize2/",
                                 type: "POST",
                                 contentType: 'application/json; charset=utf-8',
                                 data: JSON.stringify({ ktgr: y }),
                                 success: function (gelenbilgi) {
                                     $("#urunler").html(gelenbilgi);
                                     $("#viewport").attr("content", "width=device-width, initial-scale=1");
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
            }
        }
    });

});

$(function () {
    $("#ekle").click(function () {

        var y = $("#secilenid").html();
        var z = $("#mktr").html();
        $.ajax(
            {
                url: "/Restoranlar/Ekle/",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ urn: y, mktr: z }),
                success: function (gelenbilgi) {
                    $(".modal3").css("visibility", "visible");
                    $(".modal3").css("opacity", "1");
                    $(".modal1").css("visibility", "hidden");
                    $(".modal1").css("opacity", "0");
                    $("#mktr").html(1);
                    $.ajax(
            {
                url: "/Restoranlar/SepetSayi/",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                success: function (gelenbilgi) {
                    $(".sepetsayi").html(gelenbilgi);
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
    $("body").on("click", "#sdf", function () {
        var val = $(this).parent("div").parent("div").children("#urunism").html();
        var val2 = $(this).parent("div").children(".urunimg").attr("src");
        var val3 = $(this).parent("div").parent("div").children("#urnid").html();
        $(".modal1").css("visibility", "visible");
        $(".modal1").css("opacity", "1");
        $("#msggg").html("<span id=\"secilenid\" style=\"display:none\">" + val3 + "</span><span id=\"secilen\">" + val + "</span>");
        $("#image").attr("src", val2);
    });
});

$(function () {
    $("body").on("click", "#minus", function () {
        var miktar = parseInt($("#mktr").html());
        if (miktar == 1) {
            alert(" Miktar 1 den az olamaz!!");
        }
        else {
            miktar = miktar - 1;
        }
        $("#mktr").html(miktar);

    });
});

$(function () {
    $("body").on("click", "#plus", function () {
        var miktar = parseInt($("#mktr").html());
        miktar = miktar + 1;
        $("#mktr").html(miktar);

    });
});

$(function () {
    $("body").on("click", ".close", function () {
        $(".modal1").css("visibility", "hidden");
        $(".modal1").css("opacity", "0");
        $("#mktr").html(1);
    });
});

$(function () {
    $("body").on("click", ".close2", function () {
        $(".modal2").css("visibility", "hidden");
        $(".modal2").css("opacity", "0");
    });
});

$(function () {
    $("body").on("click", "#yorumlar", function () {
        var y = $("#detayurunid").html();
        $.ajax(
           {
               url: "/Restoranlar/YorumlarıCek/",
               type: "POST",
               contentType: 'application/json; charset=utf-8',
               data: JSON.stringify({ urn: y }),
               success: function (gelenbilgi) {
                   $("#yorumlardivi").html(gelenbilgi);

               },
               error: function () {
                   alert("Hata oluştu");
               }
           }
           );
        $(".modal4").css("visibility", "visible");
        $(".modal4").css("opacity", "1");
    });
});

$(function () {
    $("body").on("click", "#yorumclose", function () {
        $(".modal4").css("visibility", "hidden");
        $(".modal4").css("opacity", "0");
    });
});

$(function () {
    $("body").on("click", ".close3", function () {
        $(".modal3").css("visibility", "hidden");
        $(".modal3").css("opacity", "0");
    });
});

$(function () {
    $("body").on("click", "#iptal", function () {
        $(".modal1").css("visibility", "hidden");
        $(".modal1").css("opacity", "0");
        $("#mktr").html(1);
    });
});

$(function () {
    $("body").on("click", ".sepet", function () {
        $(".loading").css("visibility", "visible");
        $(".loading").css("opacity", "1");
    });
});

$(function () {
    $("body").on("click", ".rezervasyon", function () {
        $(".loading").css("visibility", "visible");
        $(".loading").css("opacity", "1");
    });
});

$(function () {
    $("body").on("click", ".secenekmenubuton", function () {
        $(".secenekbackground").css("visibility", "hidden");
        $(".secenekbackground").css("opacity", "0");
    });
});
$(function () {
    $("body").on("click", ".secenekodemebuton", function () {
        var url = $("#url").val();
        $(".loading").css("visibility", "visible");
        $(".loading").css("opacity", "1");
        window.location = url + "/Restoranlar/Odeme";
    });
});

$(function () {
    $(".yukari").on('click', function (event) {

        if (this.hash !== "") {
            event.preventDefault();
            var hash = this.hash;
            $('html, body').animate({
                scrollTop: $(hash).offset().top
            }, 800, function () {
                window.location.hash = hash;
            });
        }
    });
});

$(function () {
    $(window).scroll(function (event) {
        if ($(window).scrollTop() > 1800) {
            $(".yukari").css("visibility", "visible");
            $(".yukari").css("opacity", "1");
        }
        else {
            $(".yukari").css("visibility", "hidden");
            $(".yukari").css("opacity", "0");
        }
    });
});