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
                if (!ModelState.IsValid)
                {
                    ViewBag.Users = ctx.FindUserByUserName(info.UserName);
                    return View(info);
                }
                UserBLL user = ctx.FindUserByUserName(info.UserName);
                if (user == null)
                {
                    info.Message = $"The Username '{info.UserName}' does not exist in the database";
                    return View(info);
                }

                string actual = user.Hash;
                string potential = info.Password + user.Salt;
                //string potential = info.Password;
                bool validateuser = System.Web.Helpers.Crypto.VerifyHashedPassword(actual, potential);
                //bool validateuser = potential == actual;
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

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationModel newuser)
        {
            UserBLL user = new UserBLL();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    if (!ModelState.IsValid)
                    {
                        ViewBag.Users = ctx.FindUserByUserName(newuser.UserName);
                        return View(newuser);
                    }
                    if (null == ctx.FindUserByUserName(newuser.UserName))
                    {
                        if (newuser.Password != newuser.PasswordAgain)
                        {
                            newuser.Message = "Passwords do not match";
                            return View(newuser);
                        }
                        else
                        {
                            //nothing
                        }
                        user.Salt =
                        System.Web.Helpers.Crypto.GenerateSalt(16);
                        user.Hash =
                        System.Web.Helpers.Crypto.HashPassword(newuser.Password + user.Salt);
                        user.UserName = newuser.UserName;
                        user.Email = newuser.Email;
                        user.RoleID = 3;//this is the lowest priveledged user
                        ctx.CreateUser(user);
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        newuser.Message = "UserName already exits";
                        return View(newuser);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            //logic goes here to verify that username doesnt exist; and add to database as a new user
            //variable newuser contains the information supplied by the user

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "We Value Your Feedback.";

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