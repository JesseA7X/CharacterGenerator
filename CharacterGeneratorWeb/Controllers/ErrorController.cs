using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CharacterGeneratorWeb.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult LogError()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Lumberjack.Logger.Log(ex);
                ViewBag.Exception = ex;
                return View("Error");
            }
        }


    }
}