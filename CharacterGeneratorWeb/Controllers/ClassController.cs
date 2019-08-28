using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using CharacterGeneratorWeb.Models;

namespace CharacterGeneratorWeb.Controllers
{
    [MustBeLoggedIn]public class ClassController : Controller
    {

        public ActionResult Page(int PageNumber, int PageSize)
        {
            List<ClassBLL> Model = new List<ClassBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.TotalUsers = ctx.ObtainUserCount();
                    Model = ctx.GetClasses(PageNumber * PageSize, PageSize);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View("Index", Model);
        }
        // GET: Class
        public ActionResult Index()
        {
            List<ClassBLL> Model = new List<ClassBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Model = ctx.GetClasses(0, 20);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Model);
        }

        // GET: Class/Details/5
        public ActionResult Details(int id)
        {
            ClassBLL @class;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    @class = ctx.FindClassByClassID(id);
                    if (null == @class)
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
            return View(@class);
        }

        // GET: Class/Create
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Create()
        {
            ClassBLL defClass = new ClassBLL();
            defClass.ClassID = 0;
            return View(defClass);
        }

        // POST: Class/Create
        [HttpPost]
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Create(ClassBLL collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.CreateClass(collection);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }

        // GET: Class/Edit/5
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Edit(int id)
        {

            {
                ClassBLL @class;
                try
                {
                    using (ContextBLL ctx = new ContextBLL())
                    {
                        @class = ctx.FindClassByClassID(id);
                        if (null == @class)
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
                return View(@class);
            }
        }

        // POST: Class/Edit/5
        [HttpPost]
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Edit(int id, ClassBLL collection)
        {

            {
                try
                {
                    // TODO: Add insert logic here
                    using (ContextBLL ctx = new ContextBLL())
                    {
                        ctx.JustUpdateClass(collection);
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

        // GET: Class/Delete/5
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Delete(int id)
        {

            {
                ClassBLL @class;
                try
                {
                    using (ContextBLL ctx = new ContextBLL())
                    {
                        @class = ctx.FindClassByClassID(id);
                        if (null == @class)
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
                return View(@class);
            }
        }

        // POST: Class/Delete/5
        [HttpPost]
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Delete(int id, ClassBLL collection)
        {

            {
                try
                {
                    // TODO: Add insert logic here
                    using (ContextBLL ctx = new ContextBLL())
                    {
                        ctx.DeleteClass(id);
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
}
