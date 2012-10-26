using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using System.Web.Mvc;
using Basta.Controllers;

namespace Basta.Warehouse
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DataAccess : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction)
            {
                return;
            }
            var controller = (PastieController)filterContext.Controller;
            controller.DocumentSession = Storage.Instance.OpenSession();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
            {
                return;
            }
            var controller = (IDataAccess)filterContext.Controller;

            if (controller.DocumentSession != null && filterContext.Exception == null)
            {
                controller.DocumentSession.SaveChanges();
            }

            controller.DocumentSession.Dispose();
        }
    }
}