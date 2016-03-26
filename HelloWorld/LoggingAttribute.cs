using System.Diagnostics;
using System.Net;
using System.Web.Mvc;

namespace HelloWorld
{
    public class LoggingAttribute : ActionFilterAttribute
    {
        private Stopwatch stopwatch;

        // Called by the ASP.NET MVC framework before the action method executes.
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentRequest = filterContext.HttpContext.Request;

            stopwatch = System.Diagnostics.Stopwatch.StartNew();

            base.OnActionExecuting(filterContext);
        }

        // Called by the ASP.NET MVC framework after the action method executes.
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            stopwatch.Stop();
            var milliseconds = stopwatch.ElapsedMilliseconds;
            System.IO.File.AppendAllText(System.Web.HttpContext.Current.Server.MapPath("~/Logger.txt"),
                string.Format("{0} : Elapsed={1}", System.DateTime.Now, stopwatch.Elapsed));

            base.OnActionExecuted(filterContext);
        }

        // Called by the ASP.NET MVC framework before the action result executes.
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        // Called by the ASP.NET MVC framework after the action result executes.
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
    }
}