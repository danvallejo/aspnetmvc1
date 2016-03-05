using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HelloWorld
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error()
        {
            try
            {
                var exception = Server.GetLastError();
                Server.ClearError();

                if (exception.Message.Contains("does not exist"))
                {
                    // display the non existant page error with a link to home page
                }

                try
                {
                    // email
                }
                catch (Exception ex)
                {
                    // do nothing
                }

                var routeData = new RouteData();
                routeData.Values.Add("controller", "Error");
                routeData.Values.Add("action", "Error");

                IController errorController = new Controllers.ErrorController();
                errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            }
            catch (Exception ex)
            {
                // Do something here
            }
            finally
            {
                Server.ClearError();
            }
        }
    }
}
