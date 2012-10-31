using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Basta.Models;
using Basta.Warehouse;
using Nancy;
using Raven.Client;

namespace Basta.Controllers
{
    public class PastieController : NancyModule
    {
        public PastieController()
        {
            Get["/"] = parameters => 
            {
                return View["Pastie/Create.cshtml", new PastieModel()];
            };

            Get["/About"] = parameters =>
            {
                return View["Pastie/About.cshtml"];
            };

            Post["/Create"] = parameters =>
            {
                var pastie = new PastieModel
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
                    } while (session.Load<PastieModel>(pastie.Id) != null);

                    session.Store(pastie);
                    session.SaveChanges();
                }

                return Response.AsRedirect("/" + pastie.Id);
            };

            Get["/{id}"] = parameters =>
            {
                using (var session = Storage.Instance.OpenSession())
                {
                    return View["Pastie/Details.cshtml", session.Load<PastieModel>((string)parameters.id)];
                }
            };
        }
    }
}
