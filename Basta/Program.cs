using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace Basta
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = parameters => "Hello World";
        }
    }
}