using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THS.Models;
using System.Web.Security;

namespace THS.Controllers
{
    public class BaseController : Controller
    {
        AhmetEntities2 db = new AhmetEntities2();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var temp = filterContext.HttpContext.User.Identity.Name;
                ViewBag.ID = temp;
            }
            else
                ViewBag.ID = "0";

            base.OnActionExecuting(filterContext);
        }

    }
}
