using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontEnd.Models;

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

        //
        // POST: /Pastie/

        [HttpPost]
        public ActionResult Create(Pastie pastie)
        {
            return View();
        }

        //
        // GET: /Pastie/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
