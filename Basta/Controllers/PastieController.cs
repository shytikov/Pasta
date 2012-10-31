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
    public class NancyController : NancyModule
    {
        public NancyController()
        {
            Get["/"] = parameters => 
            {
                return View["Pastie/Create.cshtml", new Pastie()];
            };

            Get["/About"] = parameters =>
            {
                return View["Pastie/About.cshtml"];
            };

            Post["/Create"] = parameters =>
            {
                var pastie = new Pastie
                {
                    Content = this.Request.Form.Content,
                    Creation = DateTime.UtcNow,
                    Expiration = DateTime.UtcNow.AddMonths(1)
                };

                using (var session = Storage.Instance.OpenSession())
                {
                    do
                    {
                        pastie.RefreshId();
                    } while (session.Load<Pastie>(pastie.Id) != null);

                    session.Store(pastie);
                    session.SaveChanges();
                }

                return Response.AsRedirect("/" + pastie.Id);
            };

            Get["/{id}"] = parameters =>
            {
                using (var session = Storage.Instance.OpenSession())
                {
                    return View["Pastie/Details.cshtml", session.Load<Pastie>((string)parameters.id)];
                }
            };
        }
    }

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
