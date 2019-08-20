using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Security.Principal;
using System.Threading;

namespace CharacterGeneratorWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ApplicationConfig.RegisterApplicationVariables();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            string UserName = Session["AUTHUserName"] as string;
            string SessRoles = Session["AUTHRoles"] as string;
            if(string.IsNullOrEmpty(UserName))
            {
                return;
            }
            GenericIdentity id = new GenericIdentity(UserName, "Jesses RaptorAuth");
            if(SessRoles == null) { SessRoles = ""; }
            string[] roles = SessRoles.Split(',');
            GenericPrincipal p = new GenericPrincipal(id, roles);
            HttpContext.Current.User = p;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            if (ex is ThreadAbortException)
                return; //redirects may cause this exception
            Lumberjack.Logger.Log(ex);
        }

    }
}
