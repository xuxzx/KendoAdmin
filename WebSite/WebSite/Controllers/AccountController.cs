using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Controllers.Abstract;
using WebSite.Helper;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: /Account/

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminModel model)
        {
            if(ModelState.IsValid)
            {
                var admin = UnitOfWork.Admins.Where(x => x.LoginName.ToLower() == model.LoginName.ToLower() && x.Password.ToLower() == model.Password.ToLower()).FirstOrDefault();
                if (admin != null)
                {
                    Session["UserSession"] = UserManager.CreateUserSession(admin.Id, admin.LoginName, admin.NickName);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.ErrMsg = "用户名或密码错误";
                }
            }
            else
            {
                ViewBag.ErrMsg = "请填写用户名密码";
            }            
            return View(model);
        }

        public ActionResult LogOut()
        {
            this.Session.RemoveAll();
            return View("Login");
        }
    }
}
