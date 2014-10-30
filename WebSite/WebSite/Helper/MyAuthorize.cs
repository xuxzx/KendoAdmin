using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Helper
{
    public class MyAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //需要登录
            if (filterContext.HttpContext.Session["UserSession"] == null)
            {
                base.OnAuthorization(filterContext);
                string backurl = filterContext.HttpContext.Request.Url.AbsolutePath;
                string passPort = "/Account/Login";
                //filterContext.HttpContext.Response.Redirect(passPort, true);

                filterContext.Result = new RedirectResult(passPort);
            }
        }
    }
}