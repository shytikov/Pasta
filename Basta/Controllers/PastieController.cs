using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Basta.Models;
using Raven.Client;
using Basta.Warehouse;
using System.Text;

namespace Basta.Controllers
{
    public class PastieController : Controller
    {
        #region RavenDB's specifics

        public IDocumentSession DocumentSession { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction)
            {
                return;
            }

            this.DocumentSession = Storage.Instance.OpenSession();
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
            {
                return;
            }

            if (this.DocumentSession != null && filterContext.Exception == null)
            {
                this.DocumentSession.SaveChanges();
            }

            this.DocumentSession.Dispose();
            base.OnActionExecuted(filterContext);
        }
        #endregion

        //
        // GET: /Pastie/Create

        public ActionResult Create()
        {
            var pastie = new Pastie();
            return View(pastie);
        }

        //
        // POST: /Pastie/Create

        [HttpPost]
        public ActionResult Create(Pastie pastie)
        {
            while (this.DocumentSession.Load<Pastie>(pastie.Id) != null) 
            {
                pastie.RefreshId();
            }

            pastie.Creation = DateTime.UtcNow;
            pastie.Expiration = DateTime.UtcNow.AddDays(1);

            this.DocumentSession.Store(pastie);
            return RedirectToAction("Details", "Pastie", new { Id = pastie.Id }); 
        }

        //
        // GET: /Pastie/Details/5

        public ActionResult Details(string id)
        {
            var pastie = this.DocumentSession.Load<Pastie>(id);

            if (pastie.Expiration <= DateTime.UtcNow)
            {
                this.DocumentSession.Delete<Pastie>(pastie);
                return RedirectToAction("Create");
            }
            else
            {
                return View(pastie);
            }
        }
    }
}
