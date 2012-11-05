using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Nancy;

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
                    Id = Storage.GenerateId(),
                    Content = this.Request.Form.Content,
                    Creation = DateTime.UtcNow,
                    // TODO: expiration should be more flexible
                    Expiration = DateTime.UtcNow.AddMonths(1)
                };

                Storage.Instance.Add(pastie.Id, pastie);
                Storage.Instance.Flush();

                pastie.Content = null;
                pastie.Creation = null;
                pastie.Expiration = null;

                // Sending only Id of the pastie created
                return Response.AsJson<Pastie>(pastie);
            };

            Get["/{id}"] = parameters =>
            {
                return View["Details.liquid", Storage.Instance[parameters.id]];
            };
        }
    }
}
