using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;

namespace THS.Controllers
{
    public class KullanıcıGırısController : Controller
    {
        //
        // GET: /KullancıGırıs/

        public ActionResult KullanıcıGırısIndex()
        {
            var urlHost = Request.Url.GetLeftPart(UriPartial.Authority);
            Session["url"] = urlHost;         
            return View();
        }

    }
}
