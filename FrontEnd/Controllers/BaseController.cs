using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;

namespace FrontEnd.Controllers
{
    public class BaseController : Controller
    {
        public IDocumentSession DocumentSession { get; set; }
    }
}
