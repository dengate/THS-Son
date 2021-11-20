using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using THS.Models;
using System.Web.Security;
using System.Globalization;

namespace THS.Controllers
{
    public class tablo
    {
        string a;
        string b;
        string c;
        string d;
        string e;
        public string urunism
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

        public string urunfiyat
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

        public string miktar
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
        public string ıd
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
        public string urunıd
        {
            get
            {
                return e;
            }

            set
            {
                e = value;
            }
        }

    }

    public class kategoriurun
    {
        string a;
        string b;
        string c;
        string d;
        string e;
        public string Urun_adi
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

        public string Urun_fiyati
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

        public string Urun_ID
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
        public string Urun_kalorisi
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
        public string Urun_resmi
        {
            get
            {
                return e;
            }

            set
            {
                e = value;
            }
        }

    }

    public class RestoranlarController : BaseController
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
        AhmetEntities2 db = new AhmetEntities2();

        [HttpPost]
        public JsonResult Sil(string urn)
        {

            int musid = Convert.ToInt32(Session["musterid"]);
            int siparisid = Convert.ToInt32(urn);
            if (db.siparis.Where(u => u.Siparis_ID == siparisid).Select(u => u.Miktar).First() == 1)
            {
                sipari temp = db.siparis.Find(siparisid);
                db.siparis.Remove(temp);
                db.SaveChanges();
            }
            else
            {
                sipari temp3 = db.siparis.Find(siparisid);
                temp3.Miktar--;
                db.SaveChanges();
            }
            List<tablo> table = new List<tablo>();
            var list = db.siparis.Join(db.uruns,
                                    u => u.Urun_ID,
                                    a => a.Urun_ID,
                                    (u, a) => new { a.Urun_fiyati, u.hazir, a.Urun_adi, u.Miktar, u.Siparis_ID, u.Musteri_ID, u.durum }).Where(u => u.Musteri_ID == musid && u.durum == true && u.hazir == false).OrderBy(u => u.Urun_adi)
                                    .ToList();
            foreach (var item in list)
            {
                table.Add(new tablo()
                {
                    urunism = item.Urun_adi,
                    urunfiyat = item.Urun_fiyati.ToString(),
                    miktar = item.Miktar.ToString(),
                    ıd = item.Siparis_ID.ToString(),
                });

            }
            string html = "";
            foreach (var item in table)
            {
                double toplam = (Convert.ToInt32(item.miktar)) * (Convert.ToDouble(item.urunfiyat));
                html += "<div class=\"sepeturuncontainer\"><div style=\"display:none\" id=\"id\">" + item.ıd + "</div><div class=\"sepeturunisim\"><div class=\"sepeturunyazi\" id=\"urunism\">" + item.urunism + "</div></div><div class=\"sepeturunmiktar\"><div class=\"sepeturunyazi\" id=\"urunmktr\">" + item.miktar + "</div> </div><div class=\"sepeturunfiyat\"><div class=\"sepeturunyazi\" id=\"urunfiyat\">" + toplam + "</div> </div><div class=\"sepetsilbuton\"><div class=\"sepetsilbutonyazi\" id=\"sil\"> Sil </div></div></div>";
            }
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ToplamFiyat()
        {

            int musid = Convert.ToInt32(Session["musterid"]);
            var liste2 = db.siparis.Where(u => u.Musteri_ID == musid && u.durum == true && u.hazir == false).ToList();
            double toplam = 0;
            foreach (var item in liste2)
            {
                toplam += Convert.ToDouble(item.Miktar) * (db.uruns.Where(u => u.Urun_ID == item.Urun_ID).Select(u => u.Urun_fiyati).First());
            }
            string html = "Toplam: " + toplam + " TL";
            return Json(html, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult ToplamPisme()
        {

            int musid = Convert.ToInt32(Session["musterid"]);
            var liste2 = db.siparis.Where(u => u.Musteri_ID == musid && u.durum == true && u.hazir == false).ToList();
            double toplam = 0;
            string toplamtemp = "";
            foreach (var item in liste2)
            {
                toplamtemp = db.uruns.Where(u => u.Urun_ID == item.Urun_ID).Select(u => u.Urun_pismeSuresi).First().ToString();
                toplam += Convert.ToInt32(toplamtemp);
            }
            string html = toplam + " dk";
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult YorumKontrol(string urn)
        {

            int musid = Convert.ToInt32(Session["musterid"]);
            int urunid = Convert.ToInt32(urn);

            string html = "";

            if (db.yorums.Where(u => u.urun_ID == urunid && u.musteri_ID == musid).Count() == 0)
            {
                html += "Yorumunuz İçin Teşekkürler!!";
            }
            else
            {
                html += "Aynı ürüne yalnızca 1 yorum yapılabilir!!";
            }
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult YorumGonder(string urn, string pn, string yrm)
        {

            int urunid = Convert.ToInt32(urn);
            int puan = Convert.ToInt32(pn);
            int musid = Convert.ToInt32(Session["musterid"]);
            int resid = Convert.ToInt32(Session["RestoranId"]);

            if (db.yorums.Where(u => u.urun_ID == urunid && u.musteri_ID == musid).Count() == 0)
            {
                yorum yrum = new yorum();
                yrum.puan = puan;
                yrum.urun_ID = urunid;
                yrum.yorumlar = yrm;
                yrum.musteri_ID = musid;
                yrum.restoran_ID = resid;
                db.yorums.Add(yrum);
                db.SaveChanges();
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SiparisVer()
        {

            int musid = Convert.ToInt32(Session["musterid"]);

            var liste = db.siparis.Where(u => u.Musteri_ID == musid && u.hazir == false && u.durum == true).Select(u => u.Siparis_ID).ToList();
            var count = db.siparis.Where(u => u.Musteri_ID == musid && u.hazir == false && u.durum == true).Select(u => u.Siparis_ID).Count();
            foreach (var item in liste)
            {
                sipari temp = db.siparis.Find(item);
                temp.durum = false;
                db.SaveChanges();
            }
            string html = "";
            if (count == 0)
            {
                html = "Sepetinizde ürün yok!!";
            }
            else
            {
                html = "Siparişiniz alındı!!";
            }
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult YorumlarıCek(string urn)
        {

            int urunid = Convert.ToInt32(urn);
            string html = "";
            var liste = db.yorums.Where(u => u.urun_ID == urunid).ToList();

            foreach (var item in liste)
            {
                html += "<div style=\"width:100%\">Puan:" + item.puan + "/10</div><div class=\"yorumyazidivi\">" + item.yorumlar + "</div>";
            }
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Ekle( string urn, string mktr)
        {

            int urunid = Convert.ToInt32(urn);
            int musid = Convert.ToInt32(Session["musterid"]);
            int miktar = Convert.ToInt32(mktr);
            sipari urun = new sipari();
            var count = db.siparis.Where(u => u.Urun_ID == urunid && u.Musteri_ID == musid && u.durum == true && u.hazir == false).Count();
            if (count != 0)
            {
                var temp3 = db.siparis.Where(u => u.Urun_ID == urunid && u.Musteri_ID == musid && u.hazir == false).Select(u => u.Miktar).First();
                var temp4 = db.siparis.Where(u => u.Urun_ID == urunid && u.Musteri_ID == musid && u.hazir == false).Select(u => u.Siparis_ID).First();
                urun.Siparis_ID = temp4;
                urun.Musteri_ID = musid;
                urun.Urun_ID = urunid;
                urun.durum = true;
                urun.hazir = false;
                urun.Miktar = temp3 + miktar;
                db.Entry(urun).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                urun.Urun_ID = urunid;
                urun.Musteri_ID = musid;
                urun.durum = true;
                urun.hazir = false;
                urun.Miktar = miktar;
                db.siparis.Add(urun);
                db.SaveChanges();
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SepetSayi()
        {

            int musid = Convert.ToInt32(Session["musterid"]);
            int miktar = 0;
            var liste = db.siparis.Where(u => u.Musteri_ID == musid && u.durum == true && u.hazir == false).Select(u => u.Miktar).ToList();

            foreach (var item in liste)
            {
                miktar += item.Value;
            }
            string html = "" + miktar;
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AltKategori(string ktgr)
        {

            int id = Convert.ToInt32(ktgr);

            var temp2 = db.altkategoris.Where(u => u.Kategori_ID == id).OrderBy(u => u.Altkategori_ismi).ToList();

            string html = "<option  class=\"gizli\" style=\"display:none; visibility:hidden; opacity:0;\">000</option><option selected id=\"secili\">AltKategori..</option>";

            foreach (var item in temp2)
            {
                html += "<option  class=\"gizli\" style=\"display:none; visibility:hidden; opacity:0;\">" + item.Altkategori_ID + "</option><option>" + item.Altkategori_ismi + "</option>";

            }
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UrunPuan(string urn)
        {

            int urunid = Convert.ToInt32(urn);
            string html = "";
            double toplampuan = 0;
            int count = db.yorums.Where(u => u.urun_ID == urunid).Count();
            if (count != 0)
            {
                var puanlar = db.yorums.Where(u => u.urun_ID == urunid).Select(u => u.puan).ToList();
                foreach (var item in puanlar)
                {
                    toplampuan += Convert.ToDouble(item);
                }
                toplampuan = toplampuan / (double)count;
                html = "<span>Puan: " + Math.Round(toplampuan, 1) + "/10</span>";
            }
            else
            {
                html = "<span> Puan: Puanlanmamış</span>";
            }
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Kategorize(string urn, string ktgr)
        {
            int ziyid = Convert.ToInt32(Session["ziyaret"]);
            if (Session["ziyaret"] == null)
            {
                ziyid = 1;
            }
            
            string html = "";
            if (urn == "000")
            {
                int rest = Convert.ToInt32(Session["RestoranId"]);
                if (ktgr == "000")
                {
                    var liste = db.uruns.Where(u => u.Restoran_ID == rest).OrderBy(u => u.Urun_adi).ToList();
                    if (ziyid == 1)
                    {
                        foreach (var item in liste)
                        {
                            html += "<div class=\"uruncontainer\" id=\"urun\"><div class=\"urunust\"><img src=\"../Fotograflar/" + item.Urun_resmi + "\" class=\"urunimg\" /></div><div id=\"urnid\" style=\"display:none;\">" + item.Urun_ID + "</div><div class=\"urunisim\" id=\"urunism\">" + item.Urun_adi + "</div><div class=\"urunaltbtn\"><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Fiyat:</div><div class=\"urunaltbtndiv2\">" + item.Urun_fiyati + " TL</div></div><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Kalori:</div><div class=\"urunaltbtndiv2\">" + item.Urun_kalorisi + " kcal</div></div><div id=\"detaybtn\" class=\"urunaltbtndiv1 detaybtn\"><div class=\"urunaltbtndivdetay\">Detay</div></div></div></div>";

                        }
                    }
                    else
                    {
                        foreach (var item in liste)
                        {
                            html += "<div class=\"uruncontainer\" id=\"urun\"><div class=\"urunust\"><img src=\"../Fotograflar/" + item.Urun_resmi + "\" class=\"urunimg\" /><div class=\"urunyanbtn\" id=\"sdf\"><div class=\"urunyanbtndiv1\"><div class=\"urunyanbtndiv2\"><div class=\"urunyanbtndiv3\">Sepete Ekle</div></div></div></div></div><div id=\"urnid\" style=\"display:none;\">" + item.Urun_ID + "</div><div class=\"urunisim\" id=\"urunism\">" + item.Urun_adi + "</div><div class=\"urunaltbtn\"><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Fiyat:</div><div class=\"urunaltbtndiv2\">" + item.Urun_fiyati + " TL</div></div><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Kalori:</div><div class=\"urunaltbtndiv2\">" + item.Urun_kalorisi + " kcal</div></div><div id=\"detaybtn\" class=\"urunaltbtndiv1 detaybtn\"><div class=\"urunaltbtndivdetay\">Detay</div></div></div></div>";

                        }
                    }
                }
                else
                {
                    int id = Convert.ToInt32(ktgr);
                    var liste = db.kategoris.Join(db.altkategoris,
                                              u => u.Kategori_ID,
                                              a => a.Kategori_ID,
                                              (u, a) => new { a.Altkategori_ID, u.Kategori_ID }).Join(db.uruns,
                                                                                   u => u.Altkategori_ID,
                                                                                   e => e.Altkategori_ID,
                                                                                   (u, e) => new { u.Kategori_ID, e.Urun_resmi, e.Urun_ID, e.Urun_adi, e.Urun_fiyati, e.Urun_kalorisi }).Where(u => u.Kategori_ID == id).OrderBy(u => u.Urun_adi).ToList();
                    if (ziyid == 1)
                    {
                        foreach (var item in liste)
                        {
                            html += "<div class=\"uruncontainer\" id=\"urun\"><div class=\"urunust\"><img src=\"../Fotograflar/" + item.Urun_resmi + "\" class=\"urunimg\" /></div><div id=\"urnid\" style=\"display:none;\">" + item.Urun_ID + "</div><div class=\"urunisim\" id=\"urunism\">" + item.Urun_adi + "</div><div class=\"urunaltbtn\"><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Fiyat:</div><div class=\"urunaltbtndiv2\">" + item.Urun_fiyati + " TL</div></div><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Kalori:</div><div class=\"urunaltbtndiv2\">" + item.Urun_kalorisi + " kcal</div></div><div id=\"detaybtn\" class=\"urunaltbtndiv1 detaybtn\"><div class=\"urunaltbtndivdetay\">Detay</div></div></div></div>";

                        }
                    }
                    else
                    {
                        foreach (var item in liste)
                        {
                            html += "<div class=\"uruncontainer\" id=\"urun\"><div class=\"urunust\"><img src=\"../Fotograflar/" + item.Urun_resmi + "\" class=\"urunimg\" /><div class=\"urunyanbtn\" id=\"sdf\"><div class=\"urunyanbtndiv1\"><div class=\"urunyanbtndiv2\"><div class=\"urunyanbtndiv3\">Sepete Ekle</div></div></div></div></div><div id=\"urnid\" style=\"display:none;\">" + item.Urun_ID + "</div><div class=\"urunisim\" id=\"urunism\">" + item.Urun_adi + "</div><div class=\"urunaltbtn\"><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Fiyat:</div><div class=\"urunaltbtndiv2\">" + item.Urun_fiyati + " TL</div></div><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Kalori:</div><div class=\"urunaltbtndiv2\">" + item.Urun_kalorisi + " kcal</div></div><div id=\"detaybtn\" class=\"urunaltbtndiv1 detaybtn\"><div class=\"urunaltbtndivdetay\">Detay</div></div></div></div>";

                        }
                    }
                }
                
            }
            else
            {
                int id = Convert.ToInt32(urn);
                var liste = db.uruns.Where(u => u.Altkategori_ID == id).OrderBy(u => u.Urun_adi).ToList();
                if (ziyid == 1)
                {
                    foreach (var item in liste)
                    {
                        html += "<div class=\"uruncontainer\" id=\"urun\"><div class=\"urunust\"><img src=\"../Fotograflar/" + item.Urun_resmi + "\" class=\"urunimg\" /></div><div id=\"urnid\" style=\"display:none;\">" + item.Urun_ID + "</div><div class=\"urunisim\" id=\"urunism\">" + item.Urun_adi + "</div><div class=\"urunaltbtn\"><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Fiyat:</div><div class=\"urunaltbtndiv2\">" + item.Urun_fiyati + " TL</div></div><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Kalori:</div><div class=\"urunaltbtndiv2\">" + item.Urun_kalorisi + " kcal</div></div><div id=\"detaybtn\" class=\"urunaltbtndiv1 detaybtn\"><div class=\"urunaltbtndivdetay\">Detay</div></div></div></div>";

                    }
                }
                else
                {
                    foreach (var item in liste)
                    {
                        html += "<div class=\"uruncontainer\" id=\"urun\"><div class=\"urunust\"><img src=\"../Fotograflar/" + item.Urun_resmi + "\" class=\"urunimg\" /><div class=\"urunyanbtn\" id=\"sdf\"><div class=\"urunyanbtndiv1\"><div class=\"urunyanbtndiv2\"><div class=\"urunyanbtndiv3\">Sepete Ekle</div></div></div></div></div><div id=\"urnid\" style=\"display:none;\">" + item.Urun_ID + "</div><div class=\"urunisim\" id=\"urunism\">" + item.Urun_adi + "</div><div class=\"urunaltbtn\"><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Fiyat:</div><div class=\"urunaltbtndiv2\">" + item.Urun_fiyati + " TL</div></div><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Kalori:</div><div class=\"urunaltbtndiv2\">" + item.Urun_kalorisi + " kcal</div></div><div id=\"detaybtn\" class=\"urunaltbtndiv1 detaybtn\"><div class=\"urunaltbtndivdetay\">Detay</div></div></div></div>";

                    }
                }
            }
                                 
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Kategorize2(string ktgr)
        {
            int ziyid = Convert.ToInt32(Session["ziyaret"]);
            if (Session["ziyaret"] == null)
            {
                ziyid = 1;
            }

            string html = "";
            if (ktgr == "000")
            {
                int rest = Convert.ToInt32(Session["RestoranId"]);
                var liste = db.uruns.Where(u => u.Restoran_ID == rest).OrderBy(u => u.Urun_adi).ToList();
                if (ziyid == 1)
                {
                    foreach (var item in liste)
                    {
                        html += "<div class=\"uruncontainer\" id=\"urun\"><div class=\"urunust\"><img src=\"../Fotograflar/" + item.Urun_resmi + "\" class=\"urunimg\" /></div><div id=\"urnid\" style=\"display:none;\">" + item.Urun_ID + "</div><div class=\"urunisim\" id=\"urunism\">" + item.Urun_adi + "</div><div class=\"urunaltbtn\"><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Fiyat:</div><div class=\"urunaltbtndiv2\">" + item.Urun_fiyati + " TL</div></div><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Kalori:</div><div class=\"urunaltbtndiv2\">" + item.Urun_kalorisi + " kcal</div></div><div id=\"detaybtn\" class=\"urunaltbtndiv1 detaybtn\"><div class=\"urunaltbtndivdetay\">Detay</div></div></div></div>";

                    }
                }
                else
                {
                    foreach (var item in liste)
                    {
                        html += "<div class=\"uruncontainer\" id=\"urun\"><div class=\"urunust\"><img src=\"../Fotograflar/" + item.Urun_resmi + "\" class=\"urunimg\" /><div class=\"urunyanbtn\" id=\"sdf\"><div class=\"urunyanbtndiv1\"><div class=\"urunyanbtndiv2\"><div class=\"urunyanbtndiv3\">Sepete Ekle</div></div></div></div></div><div id=\"urnid\" style=\"display:none;\">" + item.Urun_ID + "</div><div class=\"urunisim\" id=\"urunism\">" + item.Urun_adi + "</div><div class=\"urunaltbtn\"><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Fiyat:</div><div class=\"urunaltbtndiv2\">" + item.Urun_fiyati + " TL</div></div><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Kalori:</div><div class=\"urunaltbtndiv2\">" + item.Urun_kalorisi + " kcal</div></div><div id=\"detaybtn\" class=\"urunaltbtndiv1 detaybtn\"><div class=\"urunaltbtndivdetay\">Detay</div></div></div></div>";

                    }
                }
            }

            else
            {
                int id = Convert.ToInt32(ktgr);

                List<kategoriurun> tablo = new List<kategoriurun>();
                var liste = db.kategoris.Join(db.altkategoris,
                                              u => u.Kategori_ID,
                                              a => a.Kategori_ID,
                                              (u, a) => new { a.Altkategori_ID, u.Kategori_ID }).Join(db.uruns,
                                                                                   u => u.Altkategori_ID,
                                                                                   e => e.Altkategori_ID,
                                                                                   (u, e) => new { u.Kategori_ID, e.Urun_resmi, e.Urun_ID, e.Urun_adi, e.Urun_fiyati, e.Urun_kalorisi }).Where(u => u.Kategori_ID == id).OrderBy(u => u.Urun_adi).ToList();

                foreach (var item in liste)
                {
                    tablo.Add(new kategoriurun
                    {
                        Urun_adi = item.Urun_adi,
                        Urun_fiyati = item.Urun_fiyati.ToString(),
                        Urun_ID = item.Urun_ID.ToString(),
                        Urun_kalorisi = item.Urun_kalorisi.ToString(),
                        Urun_resmi = item.Urun_resmi,
                    });
                }


                if (ziyid == 1)
                {
                    foreach (var item in tablo)
                    {
                        html += "<div class=\"uruncontainer\" id=\"urun\"><div class=\"urunust\"><img src=\"../Fotograflar/" + item.Urun_resmi + "\" class=\"urunimg\" /></div><div id=\"urnid\" style=\"display:none;\">" + item.Urun_ID + "</div><div class=\"urunisim\" id=\"urunism\">" + item.Urun_adi + "</div><div class=\"urunaltbtn\"><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Fiyat:</div><div class=\"urunaltbtndiv2\">" + item.Urun_fiyati + " TL</div></div><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Kalori:</div><div class=\"urunaltbtndiv2\">" + item.Urun_kalorisi + " kcal</div></div><div id=\"detaybtn\" class=\"urunaltbtndiv1 detaybtn\"><div class=\"urunaltbtndivdetay\">Detay</div></div></div></div>";

                    }
                }
                else
                {
                    foreach (var item in tablo)
                    {
                        html += "<div class=\"uruncontainer\" id=\"urun\"><div class=\"urunust\"><img src=\"../Fotograflar/" + item.Urun_resmi + "\" class=\"urunimg\" /><div class=\"urunyanbtn\" id=\"sdf\"><div class=\"urunyanbtndiv1\"><div class=\"urunyanbtndiv2\"><div class=\"urunyanbtndiv3\">Sepete Ekle</div></div></div></div></div><div id=\"urnid\" style=\"display:none;\">" + item.Urun_ID + "</div><div class=\"urunisim\" id=\"urunism\">" + item.Urun_adi + "</div><div class=\"urunaltbtn\"><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Fiyat:</div><div class=\"urunaltbtndiv2\">" + item.Urun_fiyati + " TL</div></div><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Kalori:</div><div class=\"urunaltbtndiv2\">" + item.Urun_kalorisi + " kcal</div></div><div id=\"detaybtn\" class=\"urunaltbtndiv1 detaybtn\"><div class=\"urunaltbtndivdetay\">Detay</div></div></div></div>";

                    }
                } 
            }

            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Restoranlar2(string id)
        {

            int semtid = Convert.ToInt32(id);
            var restoran = db.restorans.Where(u => u.Semt_ID == semtid).OrderBy(u => u.Restoran_adi).Select(u => u.Restoran_adi).ToList();
            string html = "<option>İşletmeler</option>";
            foreach (var item in restoran)
            {
                html += "<option>" + item + "</option>";

            }
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Sirala(string secim)
        {
            int rest = Convert.ToInt32(Session["RestoranId"]);
            var liste = db.uruns.Where(u => u.Restoran_ID == rest).OrderBy(u => u.Urun_adi).ToList();
            if (secim == "1")
            {
                liste = db.uruns.Where(u => u.Restoran_ID == rest).OrderByDescending(u => u.Urun_fiyati).ToList();
            }
            else if (secim == "2")
            {
                liste = db.uruns.Where(u => u.Restoran_ID == rest).OrderBy(u => u.Urun_fiyati).ToList(); 
            }
            else if (secim == "3")
            {
                 liste = db.uruns.Where(u => u.Restoran_ID == rest).OrderByDescending(u => u.Urun_kalorisi).ToList();
            }
            else if (secim == "4")
            {
                liste = db.uruns.Where(u => u.Restoran_ID == rest).OrderBy(u => u.Urun_kalorisi).ToList();
            }
            else
            {
               
            }

            int ziyid = Convert.ToInt32(Session["ziyaret"]);
            if (Session["ziyaret"] == null)
            {
                ziyid = 1;
            }
            var html = "";
            
            
            if (ziyid == 1)
            {
                foreach (var item in liste)
                {
                    html += "<div class=\"uruncontainer\" id=\"urun\"><div class=\"urunust\"><img src=\"../Fotograflar/" + item.Urun_resmi + "\" class=\"urunimg\" /></div><div id=\"urnid\" style=\"display:none;\">" + item.Urun_ID + "</div><div class=\"urunisim\" id=\"urunism\">" + item.Urun_adi + "</div><div class=\"urunaltbtn\"><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Fiyat:</div><div class=\"urunaltbtndiv2\">" + item.Urun_fiyati + " TL</div></div><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Kalori:</div><div class=\"urunaltbtndiv2\">" + item.Urun_kalorisi + " kcal</div></div><div id=\"detaybtn\" class=\"urunaltbtndiv1 detaybtn\"><div class=\"urunaltbtndivdetay\">Detay</div></div></div></div>";

                }
            }
            else
            {
                foreach (var item in liste)
                {
                    html += "<div class=\"uruncontainer\" id=\"urun\"><div class=\"urunust\"><img src=\"../Fotograflar/" + item.Urun_resmi + "\" class=\"urunimg\" /><div class=\"urunyanbtn\" id=\"sdf\"><div class=\"urunyanbtndiv1\"><div class=\"urunyanbtndiv2\"><div class=\"urunyanbtndiv3\">Sepete Ekle</div></div></div></div></div><div id=\"urnid\" style=\"display:none;\">" + item.Urun_ID + "</div><div class=\"urunisim\" id=\"urunism\">" + item.Urun_adi + "</div><div class=\"urunaltbtn\"><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Fiyat:</div><div class=\"urunaltbtndiv2\">" + item.Urun_fiyati + " TL</div></div><div class=\"urunaltbtndiv1\"><div class=\"urunaltbtndiv3\">Kalori:</div><div class=\"urunaltbtndiv2\">" + item.Urun_kalorisi + " kcal</div></div><div id=\"detaybtn\" class=\"urunaltbtndiv1 detaybtn\"><div class=\"urunaltbtndivdetay\">Detay</div></div></div></div>";

                }
            }
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Detay(string urn)
        {

            int urunid = Convert.ToInt32(urn);
            var urundetay = db.uruns.Where(u => u.Urun_ID == urunid).Select(u => u.Urun_aciklamasi).First();
            string html = "<span>" + urundetay + "</span>";
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OdemeYap()
        {
            int musid = Convert.ToInt32(Session["musterid"]);
            double toplam = 0;
            string html = "";
            var hazircount = db.siparis.Where(u => u.Musteri_ID == musid && u.hazir == false).Count();
            var elemancount = db.siparis.Where(u => u.Musteri_ID == musid).Count();
            if (elemancount == 0)
            {
                html += "Siparişiniz Bulunmamakta, ödeme yapılamaz!";
            }
            else if (hazircount != 0)
            {
                html += "Hazır olmayan siparişleriniz var, ödeme yapılamaz!";
            }

            else
            {
                var liste = db.siparis.Where(u => u.Musteri_ID == musid && u.durum == false && u.hazir == true).Select(u => new { u.Miktar, u.Urun_ID }).ToList();
                foreach (var item in liste)
                {
                    toplam += (double)item.Miktar * db.uruns.Where(u => u.Urun_ID == item.Urun_ID).Select(u => u.Urun_fiyati).First();
                }
                var format = "yyyy-MM-dd HH:mm:ss";
                var stringDate = DateTime.Now.ToString(format);

                odeme odm = new odeme();
                odm.Musteri_ID = musid;
                odm.Odeme_tipi = "Kart";
                odm.Tutar = toplam;
                odm.Odeme_zamani = stringDate;
                db.odemes.Add(odm);
                db.SaveChanges();

                var liste2 = db.siparis.Where(u => u.Musteri_ID == musid && u.durum == false && u.hazir == true).Select(u => u.Siparis_ID).ToList();
                foreach (var item in liste2)
                {
                    sipari spr = db.siparis.Find(item);
                    db.siparis.Remove(spr);
                    db.SaveChanges();

                }
                var list2 = db.musteris.Where(u => u.Musteri_ID == musid).Select(u => u.Masa_ID).First();
                var list = db.musteris.Where(u => u.Masa_ID == list2 && u.cikis == false).Count();
                if (list < 2)
                {
                    masa ms = db.masas.Find(list2);
                    ms.Masa_bosmu = false;
                    db.SaveChanges();
                }

                musteri temp = db.musteris.Find(musid);
                temp.cikis = true;
                db.SaveChanges();
                Session.Clear();
                FormsAuthentication.SignOut();
                var cnt = Request.Cookies.AllKeys.Count();
                for (int i = 0; i < cnt; i++)
                {

                    if (Request.Cookies.AllKeys.Count() != 0)
                    {
                        if (Request.Cookies.AllKeys[0] != null)
                        {
                            //var c = new HttpCookie("MyCookie");
                            //c.Expires = DateTime.Now.AddDays(-1);
                            //Response.Cookies.Add(c);
                            Request.Cookies.Remove(Request.Cookies.AllKeys[0]);
                        } 
                    }
                }
                


            }



            return Json(html, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Odeme()
        {
            int zyrt = Convert.ToInt32(Session["ziyaret"]);
            if (zyrt == 1)
            {
                return RedirectToAction("isletme");
            }
            int musid = Convert.ToInt32(Session["musterid"]);
            var elemancount = db.siparis.Where(u => u.Musteri_ID == musid && u.durum == false).Count();
            List<tablo> table = new List<tablo>();
            if (elemancount != 0)
            {
                
                var list = db.siparis.Join(db.uruns,
                                        u => u.Urun_ID,
                                        a => a.Urun_ID,
                                        (u, a) => new { a.Urun_fiyati, a.Urun_adi, u.Miktar, u.Urun_ID, u.Siparis_ID, u.Musteri_ID, u.durum, u.hazir }).Where(u => u.Musteri_ID == musid && u.durum == false && u.hazir == true)
                                        .ToList();
                foreach (var item in list)
                {
                    table.Add(new tablo()
                    {
                        urunism = item.Urun_adi,
                        urunfiyat = item.Urun_fiyati.ToString(),
                        miktar = item.Miktar.ToString(),
                        ıd = item.Siparis_ID.ToString(),
                        urunıd = item.Urun_ID.ToString(),
                    });

                }
                var hazircount = db.siparis.Where(u => u.Musteri_ID == musid && u.hazir == false && u.durum == false).Count();
                if (hazircount != 0)
                {
                    ViewBag.Temp = "Hazır Olmayan Siparişleriniz Bulunmaktadır !!";
                }                
                else
                {
                    ViewBag.Temp = "1";
                }
                
            }
            else
            {
                ViewBag.Temp = "Siparişiniz Bulunmamaktadır !!";              
            }
            var liste2 = db.siparis.Where(u => u.Musteri_ID == musid && u.durum == false && u.hazir == true).ToList();
            double toplam = 0;
            foreach (var item in liste2)
            {
                toplam += Convert.ToDouble(item.Miktar) * (db.uruns.Where(u => u.Urun_ID == item.Urun_ID).Select(u => u.Urun_fiyati).First());
            }


            ViewBag.isletme = Session["isletme"].ToString();
            ViewBag.masanum = Session["id"].ToString();
            ViewBag.RestId = Session["RestoranId"];
            ViewBag.Toplam = toplam;
            ViewBag.Ip = musid;
            ViewBag.Liste = table;

            return View();
        }

        public ActionResult Sepet()
        {
            int zyrt = Convert.ToInt32(Session["ziyaret"]);
            if (zyrt == 1)
            {
                return RedirectToAction("isletme");
            }
            int musid = Convert.ToInt32(Session["musterid"]);
            List<tablo> table = new List<tablo>();
            var list = db.siparis.Join(db.uruns,
                                    u => u.Urun_ID,
                                    a => a.Urun_ID,
                                    (u, a) => new { a.Urun_fiyati, u.hazir, a.Urun_adi, u.Miktar, u.Siparis_ID, u.Musteri_ID, u.durum }).Where(u => u.Musteri_ID == musid && u.durum == true && u.hazir == false).OrderBy(u => u.Urun_adi)
                                    .ToList();
            foreach (var item in list)
            {
                table.Add(new tablo()
                {
                    urunism = item.Urun_adi,
                    urunfiyat = item.Urun_fiyati.ToString(),
                    miktar = item.Miktar.ToString(),
                    ıd = item.Siparis_ID.ToString(),
                });

            }
            var liste2 = db.siparis.Where(u => u.Musteri_ID == musid && u.durum == true && u.hazir == false).ToList();
            double toplam = 0;
            int toplam2 = 0;
            string toplamtemp = "";
            foreach (var item in liste2)
            {
                toplam += Convert.ToDouble(item.Miktar) * (db.uruns.Where(u => u.Urun_ID == item.Urun_ID).Select(u => u.Urun_fiyati).First());
                toplamtemp = db.uruns.Where(u => u.Urun_ID == item.Urun_ID).Select(u => u.Urun_pismeSuresi).First().ToString();
                toplam2 += Convert.ToInt32(toplamtemp);
            }
            ViewBag.isletme = Session["isletme"].ToString();
            ViewBag.masanum = Session["id"].ToString();
            ViewBag.Toplam = toplam;
            ViewBag.pismetoplam = toplam2;
            ViewBag.Ip = musid;
            ViewBag.Liste = table;
            return View();

        }

        public ActionResult GetId()
        {

            string urunadi = Request.QueryString["urunadi"];
            int temp2 = 0;
            string temp3 = "";
            string temp4 = "";
            string semt = "";
            string isletme = "";
            int semtid;
            if (urunadi[0] == 'é')
            {
                Session["ziyaret"] = 0;
                for (int i = 1; i < urunadi.Length; i++)
                {

                    if (temp2 == 1)
                    {
                        temp4 += urunadi[i];
                    }
                    if (urunadi[i] == '~')
                    {
                        temp2 = 1;
                    }
                    if (temp2 == 0)
                    {
                        temp3 += urunadi[i];
                    }
                }
            }
            else
            {
                Session["ziyaret"] = 1;
                for (int i = 0; i < urunadi.Length; i++)
                {

                    if (temp2 == 1)
                    {
                        temp4 += urunadi[i];
                    }
                    if (urunadi[i] == '~')
                    {
                        temp2 = 1;
                    }
                    if (temp2 == 0)
                    {
                        temp3 += urunadi[i];
                    }
                }
            }
            temp2 = 0;
            Session["id"] = temp4;
            for (int i = 0; i < temp3.Length; i++)
            {
                if (temp2 == 0)
                {
                    if (temp3[i] != '_')
                    {

                    }
                    else
                    {
                        temp2 = 1;
                    }
                }
                else if (temp2 == 1)
                {
                    if (temp3[i] != '_')
                    {
                        semt += temp3[i];
                    }
                    else
                    {
                        temp2 = 2;
                    }
                }
                else if (temp2 == 2)
                {
                    if (temp3[i] != '_')
                    {
                        isletme += temp3[i];
                    }
                    else
                    {
                        temp2 = 3;
                    }
                }
                
            }
            semtid = db.semts.Where(u => u.Semt_adi == semt).Select(u => u.Semt_ID).First();
            int restoranid = db.restorans.Where(u => u.Restoran_adi == isletme && u.Semt_ID == semtid).Select(u => u.Restoran_ID).First();
            Session["restoranidsi"] = restoranid;
            Session["isletme"] = temp3;
            Session["isletmeadi"] = isletme;
            var security = db.restorans.Where(u => u.Restoran_ID == restoranid).Select(u => u.Security).First();
            int zyrt = Convert.ToInt32(Session["ziyaret"]);
            if (zyrt == 1 || security != 1)
            {
                return RedirectToAction("isletme");
            }
            else
            {
                return RedirectToAction("Kontrol");
            }
            

        }

        public ActionResult Kontrol()
        {
            return View();      
        }

        [HttpPost]
        public ActionResult Onay(string kod)
        {
            int realkod = Convert.ToInt32(kod);
            int rstid = Convert.ToInt32(Session["restoranidsi"]);
            if (realkod == db.restorans.Where(u => u.Restoran_ID == rstid).Select(u => u.Random).First())
            {
                Session["onay"] = "1";
                return RedirectToAction("isletme");
            }
            else {
                return RedirectToAction("Kontrol");
            }

            
        }

        public ActionResult isletme()
        {
            int restid = Convert.ToInt32(Session["restoranidsi"]);
            string ziyaret = "1";
            if (Session["ziyaret"] != null)
            {
                ziyaret = Session["ziyaret"].ToString();
            }

            ViewBag.ziyaret = ziyaret;

            string x = ViewBag.ID;

            if (ziyaret == "0")
            {
                if (x == "0")
                {
                    int masaıd = Convert.ToInt32(Session["id"]);
                    musteri mus = new musteri();
                    mus.Masa_ID = db.masas.Where(u => u.Restoran_ID == restid && u.Masa_numarasi == masaıd).Select(u => u.Masa_ID).First();
                    mus.Restoran_ID = restid;
                    mus.durum = false;
                    mus.cikis = false;
                    db.musteris.Add(mus);
                    db.SaveChanges();
                    var list = db.masas.Where(u => u.Restoran_ID == restid && u.Masa_numarasi == masaıd).Select(u => u.Masa_ID).First();
                    masa ms = db.masas.Find(list);
                    if (ms.Masa_bosmu == false)
                    {
                        ms.Masa_bosmu = true;
                        db.SaveChanges();
                    }
                    int y = mus.Musteri_ID;
                    FormsAuthentication.SetAuthCookie("" + y, true);
                    Session["musterid"] = y;
                    ViewBag.sepetmktr = 0;
                    ViewBag.musteridurum = db.musteris.Where(u => u.Musteri_ID == y).Select(u => u.durum).First();
                }
                else
                {
                    int z = Convert.ToInt32(x);
                    if (restid == db.musteris.Where(u => u.Musteri_ID == z).Select(u => u.Restoran_ID).First())
                    {
                        musteri temp = db.musteris.Find(z);
                        temp.durum = true;
                        db.SaveChanges();
                        Session["musterid"] = x;
                        ViewBag.musteridurum = db.musteris.Where(u => u.Musteri_ID == z).Select(u => u.durum).First();
                        int miktar = 0;
                        var liste = db.siparis.Where(u => u.Musteri_ID == z && u.durum == true && u.hazir == false).Select(u => u.Miktar).ToList();

                        foreach (var item in liste)
                        {
                            miktar += item.Value;
                        }
                        ViewBag.sepetmktr = miktar;
                    }
                    else
                    {
                        return RedirectToAction("");
                    }
                }
            }


            var Urun = db.uruns.Where(u => u.Restoran_ID == restid).OrderBy(u => u.Urun_adi).ToList();
            var Kategori = db.kategoris.Where(u => u.Restoran_ID == restid).OrderBy(u => u.Kategori_ismi).ToList();
            var AltKategori = db.altkategoris.OrderBy(u => u.Altkategori_ismi).ToList();
            ViewData["Urun"] = Urun;
            ViewData["AltKategori"] = AltKategori;
            ViewData["Kategori"] = Kategori;
            ViewBag.Title = Session["isletmeadi"];
            Session["RestoranId"] = restid;
            return View();
        }

    }
}

