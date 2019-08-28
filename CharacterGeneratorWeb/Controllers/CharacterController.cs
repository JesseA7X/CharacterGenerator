using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using CharacterGeneratorWeb.Models;
using Lumberjack;

namespace CharacterGeneratorWeb.Controllers
{
    [MustBeLoggedIn]
    [MustBeInRole(Roles = "Admin")]
    public class CharacterController : Controller
    {
        

        

        public ActionResult Page(int PageNumber, int PageSize)
        {
            List<CharacterBLL> Model = new List<CharacterBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.PageNumber = PageNumber;
                    ViewBag.PageSize = PageSize;
                    ViewBag.TotalCharacterCount = ctx.ObtainCharacterCount();
                    Model = ctx.GetCharacters(PageNumber * PageSize, PageSize);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View("Index", Model);
        }

        //GET: Character
        public ActionResult Index()
        {
            //return RedirectToRoute(new { Controller = "XXX", Action = "Page", PageNumber = 0, PageSize = ApplicationConfig.DefaultPageSize });
            int PageNumber = 0;
            int PageSize = ApplicationConfig.DefaultPageSize;
            List<CharacterBLL> Model = new List<CharacterBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.PageNumber = PageNumber;
                    ViewBag.PageSize = PageSize;
                    ViewBag.TotalCharacterCount = ctx.ObtainCharacterCount();
                    Model = ctx.GetCharacters(PageNumber * PageSize, PageSize);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(Model);
        }
        // GET: Character/Details/5
        public ActionResult Details(int id)
        {
            CharacterBLL character;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    character = ctx.FindByCharacterID(id);
                    if (null == character)
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
            return View(character);
        }


        // GET: Character/Create
        [MustBeInRole(Roles = "Admin,VerifiedUser")]
        public ActionResult Create()
        {
            CharacterBLL defcharacter = new CharacterBLL();
            defcharacter.CharacterID = 0;
            return View(defcharacter);
        }


        // POST: Character/Create
        [MustBeInRole(Roles = "Admin,VerifiedUser")]
        
        [HttpPost]
        public ActionResult Create(CharacterBLL collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.CreateCharacter(collection);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }

        // GET: Character/Edit/5
        public ActionResult Edit(int id)
        {

            {
                CharacterBLL character;
                try
                {
                    using (ContextBLL ctx = new ContextBLL())
                    {
                        character = ctx.FindByCharacterID(id);
                        if (null == character)
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
                return View(character);
            }
        }

        // POST: Character/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CharacterBLL collection)
        {

            {
                try
                {
                    // TODO: Add insert logic here
                    using (ContextBLL ctx = new ContextBLL())
                    {
                        ctx.JustUpdateCharacter(collection);
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

        // GET: Character/Delete/5
        public ActionResult Delete(int id)
        {

            {

                {
                    CharacterBLL character;
                    try
                    {
                        using (ContextBLL ctx = new ContextBLL())
                        {
                            character = ctx.FindByCharacterID(id);
                            if (null == character)
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
                    return View(character);
                }
            }
        }

        // POST: Character/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, CharacterBLL collection)
        {

            {

                {
                    try
                    {
                        // TODO: Add insert logic here
                        using (ContextBLL ctx = new ContextBLL())
                        {
                            ctx.DeleteCharacter(id);
                        }
                        return RedirectToAction("MyIndex");
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
