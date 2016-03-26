using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HelloWorld
{
    public class AuthorizeIPAddress : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentRequest = filterContext.HttpContext.Request;

            if (currentRequest.UserHostAddress == "::1")
            {
                //filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                throw new Exception();
            }

            base.OnActionExecuting(filterContext);
        }
    }
}