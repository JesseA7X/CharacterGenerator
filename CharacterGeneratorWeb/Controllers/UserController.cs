using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using CharacterGeneratorWeb.Models;

namespace CharacterGeneratorWeb.Controllers
{
    [MustBeLoggedIn]
    [MustBeInRole(Roles = "Admin")]
    public class UserController : Controller
    {

        // this function is to help populate the  role dropdown
        List<SelectListItem> GetRoleItems()
        {
            List<SelectListItem> ProposedReturnValue = new List<SelectListItem>();
            using (ContextBLL ctx = new ContextBLL())
            {
                List<RoleBLL> roles = ctx.GetRoles(0, 25);
                foreach (RoleBLL r in roles)
                {
                    SelectListItem x = new SelectListItem();

                    x.Value = r.RoleID.ToString();
                    x.Text = r.RoleName;
                    ProposedReturnValue.Add(x);
                }
            }
            return ProposedReturnValue;
        }

        public ActionResult Page(int PageNumber, int PageSize)
        {
            List<UserBLL> Model = new List<UserBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.TotalUsers = ctx.ObtainUserCount();
                    Model = ctx.GetUsers(PageNumber * PageSize, PageSize);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View("Index", Model);
        }
        // GET: User
        public ActionResult Index()
        {
            List<UserBLL> Model = new List<UserBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    int usercount = ctx.ObtainUserCount();
                    Model = ctx.GetUsers(0, usercount);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }

            return View(Model);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            UserBLL User;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    User = ctx.FindUserByUserID(id);
                    if (null == User)
                    {
                        return View("ItemNotFound"); 
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(User);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            UserBLL defUser = new UserBLL();
            defUser.UserID = 0;
            ViewBag.Roles = GetRoleItems();
            return View(defUser);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserBLL collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.CreateUser(collection);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            UserBLL User;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    User = ctx.FindUserByUserID(id);
                    if (null == User)
                    {
                        return View("ItemNotFound"); 
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            //ViewBag.Roles = GetRoleItems();
            return View(User);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UserBLL collection)
        {
            try
            {
                // TODO: Add update logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.JustUpdateUser(collection);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            UserBLL User;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    User = ctx.FindUserByUserID(id);
                    if (null == User)
                    {
                        return View("ItemNotFound");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(User);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, UserBLL collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.DeleteUser(id);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View();
            }
        }
    }
}
