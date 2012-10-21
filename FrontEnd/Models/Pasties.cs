using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models
{
    public class Pasties : Controller
    {
        //
        // GET: /Pastie/

        public ActionResult Index()
        {
            return View();
        }

    }
}
