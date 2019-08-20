using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using CharacterGeneratorWeb.Models;

namespace CharacterGeneratorWeb.Controllers
{
    [MustBeLoggedIn]public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            List<RoleBLL> Model = new List<RoleBLL>();
            try
            {
                using(ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.PageNumber = 0;
                    ViewBag.PageSize = ApplicationConfig.DefaultPageSize;
                    ViewBag.TotalRoleCount = ctx.ObtainRoleCount();
                    Model = ctx.GetRoles(0, 20);
                }
            }
            catch(Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Model); // model is a list of roles, name of view is same as the method name
        }

        // this method is used to implement paging
        public ActionResult Page(int? PageNumber, int? PageSize)
        {
            int PageN = (PageNumber.HasValue) ? PageNumber.Value : 0;
            int PageS = (PageSize.HasValue) ? PageSize.Value : ApplicationConfig.DefaultPageSize;
            ViewBag.PageNumber = PageNumber;
            ViewBag.PageSize = PageSize;
            List<RoleBLL> Model = new List<RoleBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.TotalRoleCount = ctx.ObtainRoleCount();
                    Model = ctx.GetRoles(PageN * PageS, PageS);
                }
                return View("Index", Model);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }

        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            RoleBLL Role;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Role = ctx.FindRoleByRoleID(id);
                    if (null == Role)
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
            return View(Role);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            RoleBLL defRole = new RoleBLL();
            defRole.RoleID = 0;
            return View(defRole);
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(RoleBLL collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.CreateRole(collection);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View();
            }
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int id)
        {
            RoleBLL Role;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Role = ctx.FindRoleByRoleID(id);
                    if (null == Role)
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
            return View(Role);
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, RoleBLL collection)
        {
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.CreateRole(collection);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View();
            }
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            RoleBLL Role;
            try
            {
                using(ContextBLL ctx = new ContextBLL())
                {
                    Role = ctx.FindRoleByRoleID(id);
                    if (null == Role)
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
            return View(Role);
        }

        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, RoleBLL collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.DeleteRole(id);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }
    }
}
