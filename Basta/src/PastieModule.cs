using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Nancy;
using Raven.Client;

namespace Basta
{
    public class PastieModule : NancyModule
    {
        public PastieModule()
        {
            Get["/"] = parameters => 
            {
                return View["Create.liquid", new PastieModel()];
            };

            Get["/About"] = parameters =>
            {
                return View["About.liquid"];
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

                pastie.Content = null;
                pastie.Creation = null;
                pastie.Expiration = null;

                // Sending only Id of the pastie created
                return Response.AsJson<PastieModel>(pastie);
            };

            Get["/{id}"] = parameters =>
            {
                using (var session = Storage.Instance.OpenSession())
                {
                    return View["Details.liquid", session.Load<PastieModel>((string)parameters.id)];
                }
            };
        }
    }
}
