using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CharacterGeneratorWeb.Models;
using BusinessLogicLayer;

namespace CharacterGeneratorWeb.Controllers
{
    [MustBeLoggedIn]public class MyCharacterController : Controller
    {
        
        public ActionResult MyIndex(int PageNumber, int PageSize)
        {
            List<CharacterBLL> Model = new List<CharacterBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    UserBLL user = ctx.FindUserByUserName(User.Identity.Name);
                    if (null == user)
                    {
                        return View("Error");
                    }
                    else
                    {
                        //There is nothing to be put into this clause
                    }
                    ViewBag.PageNumber = PageNumber;
                    ViewBag.PageSize = PageSize;
                    ViewBag.TotalCharacterCount = ctx.ObtainCharactersRelatedToUserIDCount(user.UserID);
                    Model = ctx.GetCharactersRelatedToUserID(user.UserID, PageNumber * PageSize, PageSize);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View("Index", Model);
        }

        public ActionResult MyPage(int PageNumber, int PageSize)
        {
            List<CharacterBLL> Model = new List<CharacterBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    UserBLL user = ctx.FindUserByUserName(User.Identity.Name);
                    ViewBag.PageNumber = PageNumber;
                    ViewBag.PageSize = PageSize;
                    ViewBag.TotalCharacterCount = ctx.ObtainCharactersRelatedToUserIDCount(user.UserID);
                    Model = ctx.GetCharactersRelatedToUserID(user.UserID, PageNumber * PageSize, PageSize);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View("Index", Model);
        }

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

        [MustBeInRole(Roles = "Admin,VerifiedUser")]
        public ActionResult Create()
        {
            CharacterBLL defcharacter = new CharacterBLL();
            defcharacter.CharacterID = 0;
            return View(defcharacter);
        }

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
                return RedirectToAction("MyIndex");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }

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
                    return RedirectToAction("MyIndex");
                }
                catch (Exception ex)
                {
                    ViewBag.Exception = ex;
                    return View("Error");
                }
            }
        }

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