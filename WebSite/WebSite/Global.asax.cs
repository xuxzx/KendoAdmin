using Mindscape.LightSpeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebSite.Model;

namespace WebSite
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        internal static readonly LightSpeedContext<LightSpeedModelUnitOfWork> LightSpeedDataContext = new LightSpeedContext<LightSpeedModelUnitOfWork>("default");

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void OnEndRequest(object sender, EventArgs e)
        {
            var scope = new PerRequestUnitOfWorkScope<LightSpeedModelUnitOfWork>(LightSpeedDataContext);
            if (scope.HasCurrent)
            {
                scope.Current.Dispose();
            }
        }
    }
}