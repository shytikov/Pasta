using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Nancy;
using Raven.Client;

namespace Basta
{
    public class Module : NancyModule
    {
        public Module()
        {
            Get["/"] = parameters => 
            {
                return View["Create.liquid"];
            };

            Get["/About"] = parameters =>
            {
                return View["About.liquid"];
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

                pastie.Content = null;
                pastie.Creation = null;
                pastie.Expiration = null;

                // Sending only Id of the pastie created
                return Response.AsJson<Pastie>(pastie);
            };

            Get["/{id}"] = parameters =>
            {
                using (var session = Storage.Instance.OpenSession())
                {
                    return View["Details.liquid", session.Load<Pastie>((string)parameters.id)];
                }
            };
        }
    }
}
