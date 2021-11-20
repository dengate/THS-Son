using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THS.Models;

namespace THS.Controllers
{
    public class YoneticiController : Controller
    {
        public class tablo
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

            public string ad
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
        AhmetEntities2 db = new AhmetEntities2();
      
        public ActionResult Giris()
        {
            
            return View();
        }

        public ActionResult Ana()
        {
            if (Session["giriskontrol"] == null)
            {
                return RedirectToAction("Giris");
            }
            return View();
        }

        public ActionResult RestoranEkle()
        {
            if (Session["giriskontrol"] == null)
            {
                return RedirectToAction("Giris");
            }
            var liste = db.sehirs.OrderBy(u => u.Sehir_adi).ToList();
            ViewData["sehirler"] = liste;
            return View();
        }

        [HttpPost]
        public JsonResult Giris(string ad, string sifre)
        {
            if (ad == "a" && sifre == "a")
            {
                Session["giriskontrol"] = "1";
                return Json("Ana", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Giris", JsonRequestBehavior.AllowGet);
            } 
        }

        [HttpPost]
        public JsonResult Restoranlar2(string id)
        {

            int sehirid = Convert.ToInt32(id);
            List<tablo> table = new List<tablo>();
            var semt = db.ilces.Join(db.semts,
                                    u => u.Ilce_ID,
                                    a => a.Ilce_ID,
                                    (u, a) => new { u.Sehir_ID, a.Semt_ID, a.Semt_adi }).Where(u=> u.Sehir_ID == sehirid).OrderBy(u => u.Semt_adi).ToList();

            foreach (var item in semt)
            {
                table.Add(new tablo()
                {
                    id = item.Semt_ID.ToString(),
                    ad = item.Semt_adi,
                });

            }
            string html = "<option>Semt</option>";
            foreach (var item in table)
            {
                html += "<option class=\"gizli2\" style=\"display:none; visibility:hidden; opacity:0;\">" + item.id + "</option>";
                html += " <option>" + item.ad + "</option>";

            }
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Ekle(string isletmead, string mail, string adres, string tip, string telefon, string sayi, string sifre,string sec, string yoneticiad, string yoneticisoyad,string semtid)
        {
            int masasayisi = Convert.ToInt32(sayi);
            int scrty = Convert.ToInt32(sec);
            restoran rstrn = new restoran();
            rstrn.Restoran_tipi = tip;
            rstrn.Restoran_telefonno = telefon;
            rstrn.Restoran_mail = mail;
            rstrn.Restoran_adresi = adres;
            rstrn.Restoran_adi = isletmead;
            rstrn.Restoran_resim = "Bizimrestoran.jpg";
            rstrn.Random = 11111;
            rstrn.Security = scrty;
            rstrn.Semt_ID = Convert.ToInt32(semtid);
            db.restorans.Add(rstrn);
            db.SaveChanges();
            yonetici yntc = new yonetici();
            yntc.Yonetici_adi = yoneticiad;
            yntc.Yonetici_soyadi = yoneticisoyad;
            yntc.Yonetici_sifre = sifre;
            yntc.Restoran_ID = rstrn.Restoran_ID;
            yntc.Yonetici_kullaniciadi = yoneticiad + yoneticisoyad;
            yntc.Yonetici_telefonno = telefon;
            yntc.Yonetici_mail = mail;
            db.yoneticis.Add(yntc);
            db.SaveChanges();
            for (int i = 0; i < masasayisi; i++)
            {
                var id = db.masas.Max(u => u.Masa_ID);
                masa ms = new masa();
                ms.Masa_bosmu = false;
                ms.Masa_kisiSayisi = 4;
                ms.Masa_numarasi = i + 1;
                ms.Restoran_ID = rstrn.Restoran_ID;
                ms.Masa_ID = id + 1;
                db.masas.Add(ms);
                db.SaveChanges();
            }
            
            return Json("", JsonRequestBehavior.AllowGet);
        }

    }
}
