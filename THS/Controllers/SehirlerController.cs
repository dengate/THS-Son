using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THS.Models;

namespace THS.Controllers
{
    public class rest
    {
        string a;
        string b;
        public string sehirıd
        {
            get
            {
                return a;
            }

            set
            {
                a = value;
            }
        }

        public string restıd
        {
            get
            {
                return b;
            }

            set
            {
                b = value;
            }
        }

    }
    public class semt
    {
        string a;
        string b;
        public string semtid
        {
            get
            {
                return a;
            }

            set
            {
                a = value;
            }
        }

        public string semtadi
        {
            get
            {
                return b;
            }

            set
            {
                b = value;
            }
        }

    }
    public class yapılan
    {
        string a;
        string b;
        string c;
        string d;
        public string yorumm
        {
            get
            {
                return a;
            }

            set
            {
                a = value;
            }
        }

        public string urunadi
        {
            get
            {
                return b;
            }

            set
            {
                b = value;
            }
        }
        public string puan
        {
            get
            {
                return c;
            }

            set
            {
                c = value;
            }
        }
        public string restadi
        {
            get
            {
                return d;
            }

            set
            {
                d = value;
            }
        }


    }
    public class SehirlerController : Controller
    {
        AhmetEntities2 db = new AhmetEntities2();

        public ActionResult GetId()
        {

            string gelenbilgi = Request.QueryString["plaka"];
            string sehiradi = "";
            string tempplaka = "";
            var temp2 = 0;
            for (int i = 0; i < gelenbilgi.Length; i++)
            {

                if (temp2 == 1)
                {
                    sehiradi += gelenbilgi[i];
                }
                if (gelenbilgi[i] == 'é')
                {
                    temp2 = 1;
                }
                if (temp2 == 0)
                {
                    tempplaka += gelenbilgi[i];
                }
            }

            int plaka = Convert.ToInt32(tempplaka);

            Random rastgele = new Random();
            List<rest> table = new List<rest>();
            List<yapılan> table2 = new List<yapılan>();
            var restoranlar = db.restorans.Join(db.semts,
                                          u => u.Semt_ID,
                                          a => a.Semt_ID,
                                          (u, a) => new { a.Ilce_ID, u.Restoran_ID }).Join(db.ilces,
                                                                         u => u.Ilce_ID,
                                                                         e => e.Ilce_ID,
                                                                         (u, e) => new { e.Sehir_ID, u.Restoran_ID }).Where(u => u.Sehir_ID == plaka)
                                                                         .ToList();
            int count = restoranlar.Count();
            if (count != 0)
            {
                int restoran = rastgele.Next(count);
                foreach (var item in restoranlar)
                {
                    table.Add(new rest()
                    {
                        sehirıd = item.Sehir_ID.ToString(),
                        restıd = item.Restoran_ID.ToString(),
                    });

                }
                int temprestid = Convert.ToInt32(table[restoran].restıd);
                int yorumcount = db.yorums.Where(u => u.restoran_ID == temprestid).Count();
                int yorumlarcount = db.yorums.Count();
                int tempcount = 0;
                int tempcontrol = 0;
                while (yorumcount == 0)
                {
                    if (restoran == count - 1)
                    {
                        restoran = 0;
                    }
                    else
                    {
                        restoran++;
                    }
                    temprestid = Convert.ToInt32(table[restoran].restıd);
                    yorumcount = db.yorums.Where(u => u.restoran_ID == temprestid).Count();
                    if (tempcount == yorumlarcount && yorumcount == 0)
                    {
                        yorumcount = 1;
                        tempcontrol = 1;
                    }
                    tempcount++;
                }
                if (tempcontrol == 1)
                {
                    table2.Add(new yapılan()
                    {
                        urunadi = "Yok",
                        puan = "Yok",
                        restadi = "Yok",
                        yorumm = "Yok",
                    });
                }
                else
                {
                    if (yorumlarcount <= 3)
                    {
                        var liste = db.yorums.Where(u => u.restoran_ID == temprestid).ToList();

                        for (int i = 0; i < yorumlarcount; i++)
                        {
                            var urunid = liste[i].urun_ID;
                            var restid = liste[i].restoran_ID;
                            table2.Add(new yapılan()
                            {
                                urunadi = db.uruns.Where(u => u.Urun_ID == urunid).Select(u => u.Urun_adi).First(),
                                puan = liste[i].puan.ToString(),
                                restadi = db.restorans.Where(u => u.Restoran_ID == restid).Select(u => u.Restoran_adi).First(),
                                yorumm = liste[i].yorumlar,
                            });
                        }
                    }
                    else
                    {
                        var liste = db.yorums.Where(u => u.restoran_ID == temprestid).ToList();

                        List<int> rstgyrm = new List<int>();
                        for (int i = 0; i < 3; i++)
                        {
                            int temprastgele = rastgele.Next(yorumcount);
                            if (rstgyrm.Count() == 0)
                            {
                                rstgyrm.Add(temprastgele);
                            }
                            else if (rstgyrm.Count() == 1)
                            {
                                    while (rstgyrm[0] == temprastgele)
                                    {
                                        temprastgele = rastgele.Next(yorumcount);
                                    }
                                    rstgyrm.Add(temprastgele);
                            }
                            else if (rstgyrm.Count() == 2)
                            {
                                while (rstgyrm[0] == temprastgele || rstgyrm[1] == temprastgele)
                                {
                                    temprastgele = rastgele.Next(yorumcount);
                                }
                                rstgyrm.Add(temprastgele);
                            }  
                        }

                        for (int i = 0; i < 3; i++)
                        {
                            var urunid = liste[rstgyrm[i]].urun_ID;
                            var restid = liste[rstgyrm[i]].restoran_ID;
                            table2.Add(new yapılan()
                            {
                                urunadi = db.uruns.Where(u => u.Urun_ID == urunid).Select(u => u.Urun_adi).First(),
                                puan = liste[rstgyrm[i]].puan.ToString(),
                                restadi = db.restorans.Where(u => u.Restoran_ID == restid).Select(u => u.Restoran_adi).First(),
                                yorumm = liste[rstgyrm[i]].yorumlar,
                            });
                        }

                    }
                }
            }
            else
            {
                table2.Add(new yapılan()
                {
                    urunadi = "Yok",
                    puan = "Yok",
                    restadi = "Yok",
                    yorumm = "Yok",
                });
            }
            Session["Yorumsayisi"] = table2.Count();
            Session["UrunYorumu"] = table2;
            List<semt> s = new List<semt>();
            var d = db.ilces.Join(db.semts,
                                  u => u.Ilce_ID,
                                  a => a.Ilce_ID,
                                  (u, a) => new { a.Semt_adi, a.Semt_ID, u.Sehir_ID }).Where(u => u.Sehir_ID == plaka).OrderBy(u => u.Semt_adi).ToList();

            foreach (var item in d)
            {

                s.Add(new semt()
                {
                    semtadi = item.Semt_adi,
                    semtid = item.Semt_ID.ToString(),
                });
            }
            Session["ilceler"] = s;
            Session["sehiradi"] = sehiradi;

            return RedirectToAction(sehiradi);

        }
        public ActionResult Adana()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Adiyaman()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Agri()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Amasya()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Ankara()
        {

            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Antalya()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Aydin()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Balikesir()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Bilecik()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Bingol()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Bolu()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Burdur()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Bursa()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Canakkale()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Cankiri()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Corum()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Denizli()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Diyarbakir()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Edirne()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Elazig()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Erzincan()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Erzurum()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Eskisehir()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Gaziantep()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Giresun()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Hatay()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult İsparta()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Mersin()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult İstanbul()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult İzmir()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Kastamonu()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult KahramanMaras()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Kayseri()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Kirklareli()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Kirsehir()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Kocaeli()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Konya()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Kutahya()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Malatya()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Manisa()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Mardin()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Mugla()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Nevsehir()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Nigde()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Ordu()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Rize()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Sakarya()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Samsun()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Siirt()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Sinop()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Sivas()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Tekirdag()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Tokat()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Trabzon()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Sanliurfa()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Usak()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Van()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Yozgat()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Zonguldak()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Aksaray()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Karaman()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Kirikkale()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Batman()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Bartin()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult İgdir()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Yalova()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Karabuk()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Osmaniye()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }
        public ActionResult Duzce()
        {
            ViewBag.Yorumsayisi = Session["Yorumsayisi"];
            ViewBag.UrunYorumu = Session["UrunYorumu"];
            ViewBag.d = Session["ilceler"];
            return View();
        }

    }
}

