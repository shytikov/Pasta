using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Basta.Models;
using Raven.Client;
using Basta.Warehouse;
using System.Text;
using Nancy;

namespace Basta.Controllers
{
    public class PastieController : Controller, IDataAccess
    {
        public IDocumentSession DocumentSession { get; set; }

        //
        // GET: /

        public ActionResult Create()
        {
            var pastie = new Pastie();
            return View(pastie);
        }

        //
        // POST: /

        [HttpPost]
        [DataAccess]
        public ActionResult Create(Pastie pastie)
        {
            while (this.DocumentSession.Load<Pastie>(pastie.Id) != null) 
            {
                pastie.RefreshId();
            }

            pastie.Creation = DateTime.UtcNow;
            pastie.Expiration = DateTime.UtcNow.AddMonths(1);

            this.DocumentSession.Store(pastie);
            return RedirectToAction("Details", "Pastie", new { Id = pastie.Id }); 
        }

        //
        // GET: /5de9y

        [DataAccess]
        public ActionResult Details(string id)
        {
            var pastie = this.DocumentSession.Load<Pastie>(id);

            if (pastie == null || pastie.IsExpired() == true)
            {
                if (pastie != null)
                {
                    this.DocumentSession.Delete(pastie);
                }

                return RedirectToAction("Create");
            }
            else
            {
                return View(pastie);
            }
        }

        //
        // GET: /About

        public ActionResult About()
        {
            return View();
        }
    }
}
