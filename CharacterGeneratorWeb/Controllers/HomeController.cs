using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using CharacterGeneratorWeb.Models;
using Lumberjack;
using System.Web.Security;

namespace CharacterGeneratorWeb.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            // displays empty login screen with predefined returnURL
            LoginModel m = new LoginModel();
            m.Message = TempData["Message"]?.ToString() ?? "";
            m.ReturnURL = TempData["ReturnURL"]?.ToString() ?? @"~/Home";
            m.UserName = "";
            m.Password = "";

            return View(m);
        }

        [HttpPost]
        public ActionResult Login(LoginModel info)
        {
            using (ContextBLL ctx = new ContextBLL())
            {
                UserBLL user = ctx.FindUserByUserName(info.UserName);
                if (user == null)
                {
                    info.Message = $"The Username '{info.UserName}' does not exist in the database";
                    return View(info);
                }
                string actual = user.Hash;
                //string potential = user.Salt + info.Password;
                string potential = info.Password;
                //bool validateduser = System.Web.Helpers.Crypto.VerifyHashedPassword(actual,potential);
                bool validateuser = potential == actual;
                if (validateuser)
                {
                    Session["AUTHUserName"] = user.UserName;
                    Session["AUTHRoles"] = user.RoleName;
                    return Redirect(info.ReturnURL);
                }
                info.Message = "The password was incorrect";
                return View(info);
            }
            
        }

        public ActionResult Logout()
        {
            if (Session["AUTHUserName"] != null) Session.Remove("AUTHUserName");
            if (Session["AUTHRoles"] != null) Session.Remove("AUTHRoles");
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Roles()
        {
            using (BusinessLogicLayer.ContextBLL ctx = new BusinessLogicLayer.ContextBLL())
            {
                List<BusinessLogicLayer.RoleBLL> model = ctx.GetRoles(0, 100);
                return View(model);
            }
        }
    }
}