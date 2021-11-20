using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace THS.Controllers
{
    public class AnaSayfaController : Controller
    {
        //
        // GET: /AnaSayfa/

        public ActionResult hakkimizda()
        {
            return View();
        }
        public ActionResult iletisim()
        {
            return View();
        }

    }
}
