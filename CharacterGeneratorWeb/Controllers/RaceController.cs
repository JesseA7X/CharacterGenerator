using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using CharacterGeneratorWeb.Models;

namespace CharacterGeneratorWeb.Controllers
{
    [MustBeLoggedIn]public class RaceController : Controller
    {
        public ActionResult Page(int PageNumber, int PageSize)
        {
            List<RaceBLL> Model = new List<RaceBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.TotalUsers = ctx.ObtainUserCount();
                    Model = ctx.GetRaces(PageNumber * PageSize, PageSize);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View("Index", Model);
        }
        // GET: Race
        public ActionResult Index()
        {
            List<RaceBLL> Model = new List<RaceBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Model = ctx.GetRaces(0, 20);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Model);
        }

        // GET: Race/Details/5
        public ActionResult Details(int id)
        {

            {
                RaceBLL Race;
                try
                {
                    using (ContextBLL ctx = new ContextBLL())
                    {
                        Race = ctx.FindRaceByRaceID(id);
                        if (null == Race)
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
                return View(Race);
            }
        }

        // GET: Race/Create
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Create()
        {
            RaceBLL defRace = new RaceBLL();
            defRace.RaceID = 0;
            return View(defRace);
        }

        // POST: Race/Create
        [HttpPost]
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Create(RaceBLL collection)
        {

            {
                try
                {
                    // TODO: Add insert logic here
                    using (ContextBLL ctx = new ContextBLL())
                    {
                        ctx.CreateRace(collection);
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

        // GET: Race/Edit/5
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Edit(int id)
        {

            {

                {
                    RaceBLL Race;
                    try
                    {
                        using (ContextBLL ctx = new ContextBLL())
                        {
                            Race = ctx.FindRaceByRaceID(id);
                            if (null == Race)
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
                    return View(Race);
                }
            }
        }

        // POST: Race/Edit/5
        [HttpPost]
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Edit(int id, RaceBLL collection)
        {

            {

                {
                    try
                    {
                        // TODO: Add insert logic here
                        using (ContextBLL ctx = new ContextBLL())
                        {
                            ctx.JustUpdateRace(collection);
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

        // GET: Race/Delete/5
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Delete(int id)
        {

            {

                {
                    RaceBLL Race;
                    try
                    {
                        using (ContextBLL ctx = new ContextBLL())
                        {
                            Race = ctx.FindRaceByRaceID(id);
                            if (null == Race)
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
                    return View(Race);
                }
            }
        }

        // POST: Race/Delete/5
        [HttpPost]
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Delete(int id, RaceBLL collection)
        {

            {

                {
                    try
                    {
                        // TODO: Add insert logic here
                        using (ContextBLL ctx = new ContextBLL())
                        {
                            ctx.DeleteRace(id);
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
}
