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
    public class PastieController : NancyModule
    {
        public PastieController()
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
}
