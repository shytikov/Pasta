using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontEnd.Models;
using Raven.Client;
using FrontEnd.Warehouse;
using System.Text;

namespace FrontEnd.Controllers
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

            this.DocumentSession.Store(pastie);
            return RedirectToAction("Details", "Pastie", pastie.Id);
        }

        //
        // GET: /Pastie/Details/5

        public ActionResult Details(string id)
        {
            return View();
        }
    }
}
