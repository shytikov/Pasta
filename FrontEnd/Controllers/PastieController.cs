using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class PastieController : Controller
    {
        //
        // GET: /Pastie/

        public ActionResult Create()
        {
            return View();
        }

    }
}
