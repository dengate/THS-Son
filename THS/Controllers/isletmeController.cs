using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THS.Models;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace THS.Controllers
{

    public class isletmeController : Controller
    {
        public string turkce(string temp)
        {
            var temp2 = "";
            for (var i = 0; i < temp.Length; i++)
            {
                if (temp[i] == 'Ç')
                {
                    temp2 += 'C';
                }
                else if (temp[i] == 'ç')
                {
                    temp2 += 'c';
                }
                else if (temp[i] == 'Ü')
                {
                    temp2 += 'U';
                }
                else if (temp[i] == 'ü')
                {
                    temp2 += 'u';
                }
                else if (temp[i] == 'Ö')
                {
                    temp2 += 'O';
                }
                else if (temp[i] == 'ö')
                {
                    temp2 += 'o';
                }
                else if (temp[i] == 'Ğ')
                {
                    temp2 += 'G';
                }
                else if (temp[i] == 'ğ')
                {
                    temp2 += 'g';
                }
                else if (temp[i] == 'Ş')
                {
                    temp2 += 'S';
                }
                else if (temp[i] == 'ş')
                {
                    temp2 += 's';
                }
                else if (temp[i] == 'İ')
                {
                    temp2 += 'I';
                }
                else
                    temp2 += temp[i];
            }

            return temp2;
        }
        public class siparisler
        {
            string a;
            string d;
            string f;

            public string mstid
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
                    return d;
                }

                set
                {
                    d = value;
                }
            }
            public string masaid
            {
                get
                {
                    return f;
                }

                set
                {
                    f = value;
                }
            }


        }

        public class tablo2
        {
            string a;
            string b;
            public string id
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

            public string isim
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

        public class odemeler
        {
            string a;
            string b;
            string d;
            string f;

            public string masaid
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

            public string mstrid
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

            public string odemezamani
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
            public string tutari
            {
                get
                {
                    return f;
                }

                set
                {
                    f = value;
                }
            }


        }
        AhmetEntities2 db = new AhmetEntities2();

        [HttpPost]
        public JsonResult SiparisYenile(string rest)
        {

            int restoranid = Convert.ToInt32(Session["restid"]);
            List<siparisler> table = new List<siparisler>();
            var list = db.siparis.Join(db.musteris,
                                    u => u.Musteri_ID,
                                    a => a.Musteri_ID,
                                    (u, a) => new { u.durum, u.hazir, u.Miktar, u.Urun_ID, u.Musteri_ID, a.Restoran_ID, a.Masa_ID, a.cikis }).Where(u => u.Restoran_ID == restoranid && u.durum == false && u.hazir == false && u.cikis == false)
                                    .ToList();
            int tempmstid = 0;
            int tempurunid = 0;
            List<int> mstlist = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                int temp = 0;
                tempmstid = Convert.ToInt32(list[i].Musteri_ID);
                for (int k = 0; k < mstlist.Count; k++)
                {
                    if (mstlist[k] == tempmstid)
                    {
                        temp = 1;
                    }
                }

                if (temp == 0)
                {
                    string tempurunadi = "";
                    int l = 1;
                    mstlist.Add(tempmstid);
                    foreach (var item in list)
                    {
                        var tempmusid = Convert.ToInt32(item.Musteri_ID);
                        if (tempmstid == tempmusid)
                        {
                            tempurunid = Convert.ToInt32(item.Urun_ID);
                            if (l == 0)
                            {
                                tempurunadi += ",";
                            }
                            tempurunadi += db.uruns.Where(u => u.Urun_ID == tempurunid).Select(u => u.Urun_adi).First();
                            tempurunadi += "(x" + item.Miktar + ")";
                        }
                        l = 0;
                    }
                    int tempmasaid = Convert.ToInt32(list[i].Masa_ID);
                    table.Add(new siparisler()
                    {
                        mstid = list[i].Musteri_ID.ToString(),
                        urunadi = tempurunadi,
                        masaid = db.masas.Where(u => u.Restoran_ID == restoranid && u.Masa_ID == tempmasaid).Select(u => u.Masa_numarasi).First().ToString(),
                    });

                }

            }
            string html = "";
            foreach (var item in table)
            {
                html += "<div id=\"altdivbtns\" class=\"altdivbtns\"><div id=\"mstid\" style=\"display:none;\">" + item.mstid + "</div><div class=\"altdivbtnsol\"><div class=\"altdivbtnsolust\">Masa</div><div class=\"altdivbtnsolalt\">" + item.masaid + "</div></div><div class=\"altdivbtnsag\"><div class=\"altdivbtnsagust\">Siparişler</div><div class=\"altdivbtnsagalt\">" + item.urunadi + "</div></div></div>";
            }

            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OdemeYenile(string rest)
        {
            int restoranid = Convert.ToInt32(Session["restid"]);
            List<odemeler> table = new List<odemeler>();
            var format = "yyyy-MM-dd";
            var stringDate = DateTime.Now.ToString(format);

            var list = db.odemes.Join(db.musteris,
                                    u => u.Musteri_ID,
                                    a => a.Musteri_ID,
                                    (u, a) => new { a.cikis, u.Musteri_ID, a.Masa_ID, u.Odeme_zamani, u.Tutar, a.Restoran_ID }).Where(u => u.cikis == true && u.Odeme_zamani.Contains(stringDate) && u.Restoran_ID == restoranid)
                                    .ToList();

            foreach (var item in list)
            {
                int tempmusid = Convert.ToInt32(item.Musteri_ID);
                int tempmasaid = Convert.ToInt32(item.Masa_ID);
                table.Add(new odemeler()
                {
                    masaid = db.masas.Where(u => u.Restoran_ID == restoranid && u.Masa_ID == tempmasaid).Select(u => u.Masa_numarasi).First().ToString(),
                    mstrid = item.Musteri_ID.ToString(),
                    odemezamani = item.Odeme_zamani,
                    tutari = item.Tutar.ToString(),
                });
            }
            string html = "";
            foreach (var item in table)
            {
                html += "<div class=\"altdivbtns\" style=\"cursor:default !important;\"><div class=\"altdivbtnsol\"><div class=\"altdivbtnsolust\">Masa</div><div class=\"altdivbtnsolalt\">" + item.masaid + "</div></div><div class=\"altdivbtnsol\"><div class=\"altdivbtnsolust\">Müşteri</div><div class=\"altdivbtnsolalt\" style=\"font-size:20px !important;\"><div style=\"width:100%;padding-top:25%\">" + item.mstrid + "</div></div></div><div class=\"altdivbtnsol\"><div class=\"altdivbtnsolust\" style=\"height:38% !important;\" >Ödeme Zamanı</div><div class=\"altdivbtnsolalt\" style=\"font-size:12px !important; height:62% !important;\"><div style=\"width:100%;padding-top:25%\">" + item.odemezamani + "</div></div></div><div class=\"altdivbtnsol\"><div class=\"altdivbtnsolust\">Tutar</div><div class=\"altdivbtnsolalt\" style=\"font-size:20px !important;\"><div style=\"width:100%;padding-top:25%\">" + item.tutari + " TL</div></div></div></div>";
            }

            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MasaYenile(string rest)
        {
            int count;
            int restoranid = Convert.ToInt32(Session["restid"]);
            var list = db.masas.Where(u => u.Restoran_ID == restoranid).ToList();
            foreach (var item in list)
            {
                if (item.Masa_bosmu == true)
                {
                    count = db.musteris.Where(u => u.Restoran_ID == restoranid && u.Masa_ID == item.Masa_ID && u.cikis == false).Count();
                    if (count == 0)
                    {
                        masa ms = db.masas.Find(item.Masa_ID);
                        ms.Masa_bosmu = false;
                        db.SaveChanges();
                        item.Masa_bosmu = false;
                    }
                }
                if (item.Masa_bosmu == false)
                {
                    count = db.musteris.Where(u => u.Restoran_ID == restoranid && u.Masa_ID == item.Masa_ID && u.cikis == false).Count();
                    if (count != 0)
                    {
                        masa ms = db.masas.Find(item.Masa_ID);
                        ms.Masa_bosmu = true;
                        db.SaveChanges();
                        item.Masa_bosmu = true;
                    }
                }
            }
            string html = "";
            foreach (var item in list)
            {
                if (item.Masa_bosmu == false)
                {
                    html += "<div class=\"altdivbtns\" style=\"cursor: default !important; background-color: #00a000; border:none !important; width: 19.5% !important; height: 18% !important;\"><div class=\"altdivbtnsol\" style=\"float: none !important; margin: auto; margin-top: 1%; width: 25% !important;\"><div class=\"altdivbtnsolust\">Masa</div><div class=\"altdivbtnsolalt\">" + item.Masa_numarasi + "</div></div></div>";
                }
                else
                {
                    html += "<div class=\"altdivbtns\" style=\"cursor: default !important; background-color: #c51616; border:none !important; width: 19.5% !important; height: 18% !important;\"><div class=\"altdivbtnsol\" style=\"float: none !important; margin: auto; margin-top: 1%; width: 25% !important;\"><div class=\"altdivbtnsolust\">Masa</div><div class=\"altdivbtnsolalt\">" + item.Masa_numarasi + "</div></div></div>";
                }
            }

            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Sil(string mst)
        {

            int mstid = Convert.ToInt32(mst);

            var liste = db.siparis.Where(u => u.Musteri_ID == mstid).Select(u => u.Siparis_ID).ToList();

            foreach (var item in liste)
            {
                sipari spr = db.siparis.Find(item);
                db.siparis.Remove(spr);
                db.SaveChanges();
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult KategoriGetir()
        {

            int restoranid = Convert.ToInt32(Session["restid"]);
            var liste = db.kategoris.Where(u => u.Restoran_ID == restoranid).ToList();
            string html = "<option  class=\"gizli\" style=\"display:none; visibility:hidden; opacity:0;\">000</option><option selected id=\"secili\">Kategori..</option>";
            foreach (var item in liste)
            {
                html += "<option  class=\"gizli\" style=\"display:none; visibility:hidden; opacity:0;\">" + item.Kategori_ID + "</option><option>" + item.Kategori_ismi + "</option>";
            }

            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AltKategoriGetir(string id)
        {

            int restoranid = Convert.ToInt32(Session["restid"]);
            int urnid = Convert.ToInt32(id);
            var altktgrid = db.uruns.Where(u => u.Urun_ID == urnid).Select(u => u.Altkategori_ID).First();
            List<tablo2> table = new List<tablo2>();
            var list = db.kategoris.Join(db.altkategoris,
            u => u.Kategori_ID,
            a => a.Kategori_ID,
            (u, a) => new { a.Altkategori_ID, a.Altkategori_ismi, u.Restoran_ID }).Where(u => u.Restoran_ID == restoranid).OrderBy(u => u.Altkategori_ismi)
            .ToList();
            foreach (var item in list)
            {
                table.Add(new tablo2()
                {
                    id = item.Altkategori_ID.ToString(),
                    isim = item.Altkategori_ismi,
                });
            }
            string html = "";
            foreach (var item in table)
            {
                if (item.id == altktgrid.ToString())
                {
                    html += "<option  class=\"gizli\" style=\"display:none; visibility:hidden; opacity:0;\">" + item.id + "</option><option selected>" + item.isim + "</option>";
                }
                else
                {
                    html += "<option  class=\"gizli\" style=\"display:none; visibility:hidden; opacity:0;\">" + item.id + "</option><option>" + item.isim + "</option>"; 
                }
            }

            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult KategoriEkle(string ad)
        {

            int restoranid = Convert.ToInt32(Session["restid"]);
            kategori ktgr = new kategori();
            ktgr.Kategori_ismi = ad;
            ktgr.Restoran_ID = restoranid;
            db.kategoris.Add(ktgr);
            db.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AltKategoriEkle(string ad, string id)
        {

            int restoranid = Convert.ToInt32(Session["restid"]);
            int kid = Convert.ToInt32(id);
            altkategori altktgr = new altkategori();
            altktgr.Altkategori_ismi = ad;
            altktgr.Kategori_ID = kid;
            db.altkategoris.Add(altktgr);
            db.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MusteriSayisi(string rest)
        {

            int restoranid = Convert.ToInt32(Session["restid"]);

            string html = "Müşteri Sayısı: " + db.musteris.Where(u => u.Restoran_ID == restoranid && u.cikis == false).Count();

            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Hazir(string mst)
        {

            int mstid = Convert.ToInt32(mst);

            var liste = db.siparis.Where(u => u.Musteri_ID == mstid).Select(u => u.Siparis_ID).ToList();

            foreach (var item in liste)
            {
                sipari temp3 = db.siparis.Find(item);
                temp3.hazir = true;
                db.SaveChanges();
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Siparisler()
        {
            if (Session["restid"] == null)
            {
                return RedirectToAction("Giris");
            }
            int restid = Convert.ToInt32(Session["restid"]);
            List<siparisler> table = new List<siparisler>();
            var list = db.siparis.Join(db.musteris,
                                    u => u.Musteri_ID,
                                    a => a.Musteri_ID,
                                    (u, a) => new { u.durum, u.hazir, u.Miktar, u.Urun_ID, u.Musteri_ID, a.Restoran_ID, a.Masa_ID, a.cikis }).Where(u => u.Restoran_ID == restid && u.durum == false && u.hazir == false && u.cikis == false)
                                    .ToList();
            int tempmstid = 0;
            int tempurunid = 0;
            List<int> mstlist = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                int temp = 0;
                tempmstid = Convert.ToInt32(list[i].Musteri_ID);
                for (int k = 0; k < mstlist.Count; k++)
                {
                    if (mstlist[k] == tempmstid)
                    {
                        temp = 1;
                    }
                }

                if (temp == 0)
                {
                    string tempurunadi = "";
                    int l = 1;
                    mstlist.Add(tempmstid);
                    foreach (var item in list)
                    {
                        var tempmusid = Convert.ToInt32(item.Musteri_ID);
                        if (tempmstid == tempmusid)
                        {
                            tempurunid = Convert.ToInt32(item.Urun_ID);
                            if (l == 0)
                            {
                                tempurunadi += ",";
                            }
                            tempurunadi += db.uruns.Where(u => u.Urun_ID == tempurunid).Select(u => u.Urun_adi).First();
                            tempurunadi += "(x" + item.Miktar + ")";
                        }
                        l = 0;
                    }
                    int tempmasaid = Convert.ToInt32(list[i].Masa_ID);
                    table.Add(new siparisler()
                    {
                        mstid = list[i].Musteri_ID.ToString(),
                        urunadi = tempurunadi,
                        masaid = db.masas.Where(u => u.Restoran_ID == restid && u.Masa_ID == tempmasaid).Select(u => u.Masa_numarasi).First().ToString(),
                    });

                }

            }

            ViewBag.Siparisler = table;
            ViewBag.MusteriSayisi = db.musteris.Where(u => u.Restoran_ID == restid && u.cikis == false).Count();//
            ViewBag.Sayfa = 0;
            ViewBag.Restoranid = restid;
            return View();
        }

        public ActionResult Odemeler()
        {
            if (Session["restid"] == null)
            {
                return RedirectToAction("Giris");
            }
            int restid = Convert.ToInt32(Session["restid"]);
            List<odemeler> table = new List<odemeler>();
            var format = "yyyy-MM-dd";
            var stringDate = DateTime.Now.ToString(format);

            var list = db.odemes.Join(db.musteris,
                                    u => u.Musteri_ID,
                                    a => a.Musteri_ID,
                                    (u, a) => new { a.cikis, u.Musteri_ID, u.Odeme_zamani, a.Masa_ID, u.Tutar, a.Restoran_ID }).Where(u => u.cikis == true && u.Odeme_zamani.Contains(stringDate) && u.Restoran_ID == restid)
                                    .ToList();

            foreach (var item in list)
            {
                int tempmusid = Convert.ToInt32(item.Musteri_ID);
                int tempmasaid = Convert.ToInt32(item.Masa_ID);
                table.Add(new odemeler()
                {
                    masaid = db.masas.Where(u => u.Restoran_ID == restid && u.Masa_ID == tempmasaid).Select(u => u.Masa_numarasi).First().ToString(),
                    mstrid = item.Musteri_ID.ToString(),
                    odemezamani = item.Odeme_zamani,
                    tutari = item.Tutar.ToString(),
                });
            }

            ViewBag.Odemeler = table;
            ViewBag.MusteriSayisi = db.musteris.Where(u => u.Restoran_ID == restid && u.cikis == false).Count();
            ViewBag.Sayfa = 1;
            ViewBag.Restoranid = restid;
            return View();
        }

        public ActionResult Masalar()
        {
            if (Session["restid"] == null)
            {
                return RedirectToAction("Giris");
            }
            int restid = Convert.ToInt32(Session["restid"]);
            int count;
            var list = db.masas.Where(u => u.Restoran_ID == restid).ToList();
            foreach (var item in list)
            {
                if (item.Masa_bosmu == true)
                {
                    count = db.musteris.Where(u => u.Masa_ID == item.Masa_ID && u.cikis == false).Count();
                    if (count == 0)
                    {
                        masa ms = db.masas.Find(item.Masa_ID);
                        ms.Masa_bosmu = false;
                        db.SaveChanges();
                        item.Masa_bosmu = false;
                    }
                }
                if (item.Masa_bosmu == false)
                {
                    count = db.musteris.Where(u => u.Masa_ID == item.Masa_ID && u.cikis == false).Count();
                    if (count != 0)
                    {
                        masa ms = db.masas.Find(item.Masa_ID);
                        ms.Masa_bosmu = true;
                        db.SaveChanges();
                        item.Masa_bosmu = true;
                    }
                }
            }


            ViewBag.Masalar = list;
            ViewBag.MusteriSayisi = db.musteris.Where(u => u.Restoran_ID == restid && u.cikis == false).Count();
            ViewBag.Sayfa = 2;
            ViewBag.Restoranid = restid;
            return View();
        }

        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Giris(string kullaniciadi, string sifre)
        {
            int kulcount = db.yoneticis.Where(u => u.Yonetici_mail == kullaniciadi).Count();
            if (kulcount == 0)
            {
                ViewBag.durum = 1;
                ViewBag.durumyazi = "Kullanıcı adı yanlış!";
                return View();
            }
            else
            {
                int sifrecount = db.yoneticis.Where(u => u.Yonetici_mail == kullaniciadi && u.Yonetici_sifre == sifre).Count();
                if (sifrecount == 0)
                {
                    ViewBag.durum = 1;
                    ViewBag.durumyazi = "Şifre yanlış!";
                    return View();
                }
                else
                {
                    Session["restid"] = db.yoneticis.Where(u => u.Yonetici_mail == kullaniciadi && u.Yonetici_sifre == sifre).Select(u => u.Restoran_ID).First();
                    return RedirectToAction("AnaSayfa");
                }
            }
        }

        public ActionResult Random()
        {
            if (Session["restid"] == null)
            {
                return RedirectToAction("Giris");
            }
            var id = Convert.ToInt32(Session["restid"]);
            var rndm = db.restorans.Where(u => u.Restoran_ID == id).Select(u => u.Random).First();
            ViewBag.rndm = rndm;
            return View();
        }

        [HttpPost]
        public JsonResult RandomGetir()
        {
            Random rastgele = new Random();
            int sayi = rastgele.Next(10000, 100000);
            var id = Convert.ToInt32(Session["restid"]);
            restoran rst = db.restorans.Find(id);
            rst.Random = sayi;
            db.SaveChanges();

            return Json(sayi, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnaSayfa()
        {
            if (Session["restid"] == null)
            {
                return RedirectToAction("Giris");
            }
            var id = Convert.ToInt32(Session["restid"]);
            string temp2 = db.restorans.Where(u => u.Restoran_ID == id).Select(u => u.Restoran_adi).First();
            var semtid = db.restorans.Where(u => u.Restoran_ID == id).Select(u => u.Semt_ID).First();
            var semtadi = db.semts.Where(u => u.Semt_ID == semtid).Select(u => u.Semt_adi).First();
            var ilceid = db.semts.Where(u => u.Semt_ID == semtid).Select(u => u.Ilce_ID).First();
            var ilid = db.ilces.Where(u => u.Ilce_ID == ilceid).Select(u => u.Sehir_ID).First();
            var il = db.sehirs.Where(u => u.Sehir_ID == ilid).Select(u => u.Sehir_adi).First();
            string restoranad = "";
            restoranad += il + "_" + semtadi + "_" + temp2;
            restoranad = turkce(restoranad);
            Session["restoranad"] = restoranad;
            ViewBag.restoran = restoranad;
            return View();
        }

        public ActionResult RestoranBilgi()
        {
            if (Session["restid"] == null)
            {
                return RedirectToAction("Giris");
            }
            return View();
        }

        public ActionResult YemekEkle()
        {
            if (Session["restid"] == null)
            {
                return RedirectToAction("Giris");
            }
            var id = Convert.ToInt32(Session["restid"]);
            List<tablo2> table = new List<tablo2>();
            var list = db.kategoris.Join(db.altkategoris,
            u => u.Kategori_ID,
            a => a.Kategori_ID,
            (u, a) => new { a.Altkategori_ID, a.Altkategori_ismi, u.Restoran_ID }).Where(u => u.Restoran_ID == id).OrderBy(u => u.Altkategori_ismi)
            .ToList();
            foreach (var item in list)
            {
                table.Add(new tablo2()
                {
                    id = item.Altkategori_ID.ToString(),
                    isim = item.Altkategori_ismi,
                });
            }
            ViewBag.alt1 = table;
            return View();
        }
        [HttpPost]
        public ActionResult RestoranBilgi(string isletmeadi, string isletmeadresi, string mailadresi, string tip, string pnumber)
        {


            restoran rest = new restoran();
            rest.Restoran_adi = isletmeadi;
            rest.Restoran_adresi = isletmeadresi;
            rest.Restoran_mail = mailadresi;
            rest.Restoran_tipi = tip;
            rest.Restoran_telefonno = pnumber;
            db.restorans.Add(rest);
            db.SaveChanges();

            return View();
        }
        public ActionResult Contact()
        {
            if (Session["restid"] == null)
            {
                return RedirectToAction("Giris");
            }

            return View();

        }
        [HttpPost]
        public ActionResult Contact(string ad, string soyad, string isadi, string mail, string pnumber, string baslik, string mesaj)
        {
            MailMessage mailim = new MailMessage();
            MailMessage dönüs = new MailMessage();
            dönüs.To.Add(mail);
            dönüs.From = new MailAddress("thsdeneme@gmail.com");
            dönüs.Subject = "Başvuru";
            dönüs.Body = "Sayın " + ad + " " + soyad + " mailiniz elimize ulaşmıştır. En kısa sürede dönüş sağlanacaktır." + "<br>" + "İyi çalışmalar dileriz" + "<br>" + "THS";
            dönüs.IsBodyHtml = true;
            mailim.To.Add("aehapdaeog@gmail.com");
            mailim.From = new MailAddress("thsdeneme@gmail.com");
            mailim.Subject = baslik;
            mailim.Body = "Sayın Yetkili, " + ad + " " + soyad + " " + "kişisinden gelen mesajın içeriği aşağıdaki gibidir. <br>" + isadi + "<br>" + pnumber + "<br>" + mail + "<br>" + mesaj;
            mailim.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("thsdeneme@gmail.com", "Ths741852.");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mailim);
                smtp.Send(dönüs);


            }
            catch (Exception ex)
            {
                TempData["Message"] = "Mesaj gönderilemedi.Hata nedeni:" + ex.Message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult YemekEkle(string ad, float fiyat, string aciklama, int kalori, int sure, string KategoriSecim, HttpPostedFileBase myfile)
        {
            var id = Convert.ToInt32(Session["restid"]);
            string temp2 = db.restorans.Where(u => u.Restoran_ID == id).Select(u => u.Restoran_adi).First();
            string restoranad = "";
            for (var i = 0; i < temp2.Length; i++)
            {
                if (temp2[i] != ' ')
                {
                    restoranad += temp2[i];
                }
            }
            var semtid = db.restorans.Where(u => u.Restoran_ID == id).Select(u => u.Semt_ID).First();
            var semtadi = db.semts.Where(u => u.Semt_ID == semtid).Select(u => u.Semt_adi).First();
            var ilceid = db.semts.Where(u => u.Semt_ID == semtid).Select(u => u.Ilce_ID).First();
            var ilid = db.ilces.Where(u => u.Ilce_ID == ilceid).Select(u => u.Sehir_ID).First();
            var il = db.sehirs.Where(u => u.Sehir_ID == ilid).Select(u => u.Sehir_adi).First();
            int temp = db.altkategoris.Where(u => u.Altkategori_ismi == KategoriSecim).Select(u => u.Altkategori_ID).First();
            if (myfile.ContentLength > 0)
            {
                string filePath = Path.Combine(Server.MapPath("~/Fotograflar"), il + "-" + semtadi + "-" + restoranad + "-" + ad + ".png");
                myfile.SaveAs(filePath);
            }

            urun urun1 = new urun();
            urun1.Urun_aciklamasi = aciklama;
            urun1.Urun_fiyati = fiyat;
            urun1.Urun_kalorisi = kalori;
            urun1.Urun_adi = ad;
            urun1.Urun_pismeSuresi = sure;
            urun1.Urun_resmi = il + "-" + semtadi + "-" + restoranad + "-" + ad + ".png";
            urun1.Restoran_ID = id;
            urun1.Altkategori_ID = temp;
            db.uruns.Add(urun1);
            db.SaveChanges();
            ViewBag.restoran = Session["restoranad"];

            return View("AnaSayfa");
        }
        public ActionResult Rezervasyon()
        {

            return View();
        }
        public ActionResult Menu()
        {
            if (Session["restid"] == null)
            {
                return RedirectToAction("Giris");
            }
            var id = Convert.ToInt32(Session["restid"]);
            var urunler = db.uruns.Where(u => u.Restoran_ID == id).ToList();
            ViewData["urunler2"] = urunler;
            return View();
        }
        [HttpPost]
        public JsonResult Guncelle(string id, string ad, string fiyat, string aciklama, string kalori, string pisme, string ktgrid)
        {
            int id2 = Convert.ToInt32(id);
            double fiyat2 = Convert.ToDouble(fiyat);
            int kalori2 = Convert.ToInt32(kalori);
            int altktgrid = Convert.ToInt32(ktgrid);
            int pisme2 = Convert.ToInt32(pisme);
            urun yemek = db.uruns.Find(id2);
            yemek.Urun_adi = ad;
            yemek.Urun_fiyati = fiyat2;
            yemek.Urun_aciklamasi = aciklama;
            yemek.Urun_kalorisi = kalori2;
            yemek.Urun_pismeSuresi = pisme2;
            yemek.Altkategori_ID = altktgrid;

            db.SaveChanges();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public ActionResult KendiMenu()
        {
            var id = Convert.ToInt32(Session["restid"]);
            if (Session["restid"] == null)
            {
                return RedirectToAction("Giris");
            }
            List<tablo2> table = new List<tablo2>();
            var urunler1 = db.sistemuruns.ToList();
            ViewData["urunler"] = urunler1;
            var list = db.kategoris.Join(db.altkategoris,
            u => u.Kategori_ID,
            a => a.Kategori_ID,
            (u, a) => new { a.Altkategori_ID, a.Altkategori_ismi, u.Restoran_ID }).Where(u => u.Restoran_ID == id).OrderBy(u => u.Altkategori_ismi)
            .ToList();
            foreach (var item in list)
            {
                table.Add(new tablo2()
                {
                    id = item.Altkategori_ID.ToString(),
                    isim = item.Altkategori_ismi,
                });
            }
            ViewBag.alt = table;
            return View();
        }

        public ActionResult QrKod()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Ekle(string katid, string id)
        {
            var id3 = Convert.ToInt32(Session["restid"]);
            int katid2 = Convert.ToInt32(katid);
            int id2 = Convert.ToInt32(id);
            urun urun1 = new urun();

            urun1.Urun_resmi = db.sistemuruns.Where(u => u.Urun_ID == id2).Select(u => u.Urun_resmi).First();
            urun1.Urun_kalorisi = db.sistemuruns.Where(u => u.Urun_ID == id2).Select(u => u.Urun_kalorisi).First();
            urun1.Urun_pismeSuresi = db.sistemuruns.Where(u => u.Urun_ID == id2).Select(u => u.Urun_pismeSuresi).First();
            urun1.Urun_aciklamasi = db.sistemuruns.Where(u => u.Urun_ID == id2).Select(u => u.Urun_aciklamasi).First();
            urun1.Urun_adi = db.sistemuruns.Where(u => u.Urun_ID == id2).Select(u => u.Urun_adi).First();
            urun1.Urun_fiyati = (double)(db.sistemuruns.Where(u => u.Urun_ID == id2).Select(u => u.Urun_fiyati).First());
            urun1.Restoran_ID = id3;
            urun1.Altkategori_ID = katid2;
            db.uruns.Add(urun1);
            db.SaveChanges();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Urunsil(string id)
        {

            int urnid = Convert.ToInt32(id);
            var liste = db.yorums.Where(u => u.urun_ID == urnid).Select(u => u.Yorum_ID).ToList();
            foreach (var item in liste)
            {
                yorum yrm = db.yorums.Find(item);
                db.yorums.Remove(yrm);
                db.SaveChanges();
            }
            urun spr = db.uruns.Find(urnid);
            db.uruns.Remove(spr);
            db.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}
