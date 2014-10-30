using Mindscape.LightSpeed;
using Mindscape.LightSpeed.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Helper;
using WebSite.Model;

namespace WebSite.Controllers.Abstract
{
    public class BaseController : LightSpeedControllerBase<LightSpeedModelUnitOfWork>
    {
        protected override LightSpeedContext<LightSpeedModelUnitOfWork> LightSpeedContext
        {
            get { return MvcApplication.LightSpeedDataContext; }
        }

        #region session manage

        private UserSession usersession = null;

        public bool HasSession
        {
            get { return this.Session["UserSession"] != null; }
        }

        public UserSession MySession
        {
            get
            {
                if (this.usersession == null)
                {
                    if (this.Session["UserSession"] != null)
                    {
                        this.usersession = Session["UserSession"] as UserSession;
                    }
                }
                return this.usersession;
            }
        }
        #endregion

    }
}
