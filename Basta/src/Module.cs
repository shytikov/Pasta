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
                    Content = this.Request.Form.Content,
                    Creation = DateTime.UtcNow,
                    // TODO: expiration should be more flexible
                    Expiration = DateTime.UtcNow.AddMonths(1)
                };

                Storage.Data.Add(pastie.Id, Storage.Worker.Serialize(pastie));
                Storage.Data.Flush();

                // Sending only Id of the pastie created
                return Response.AsJson(new { Id = pastie.Id });
            };

            Get["/{id}"] = parameters =>
            {
                try
                {
                    return View["Details.liquid", Storage.Worker.Deserialize<Pastie>(Storage.Data[parameters.id])];
                }
                catch (KeyNotFoundException ex)
                {
                    // TODO: add exception handling
                    return ex.ToString();
                }
            };
        }
    }
}
