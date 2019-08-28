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
    [MustBeInRole(Roles ="Admin")]
    public class MultiController : Controller
    {

        List<SelectListItem> GetRoleItems(ContextBLL ctx)
        {
            List<SelectListItem> proposedReturnValue = new List<SelectListItem>();

            List<RoleBLL> roles = ctx.GetRoles(0, 25);
            foreach (RoleBLL r in roles)
            {
                SelectListItem x = new SelectListItem();

                x.Value = r.RoleID.ToString();
                x.Text = r.RoleName;
                proposedReturnValue.Add(x);
            }
            return proposedReturnValue;
        }

        List<SelectListItem> GetClassItems(ContextBLL ctx)
        {
            List<SelectListItem> proposedReturnValue = new List<SelectListItem>();

            List<ClassBLL> classes = ctx.GetClasses(0, 25);
            foreach (ClassBLL c in classes)
            {
                SelectListItem x = new SelectListItem();

                x.Value = c.ClassID.ToString();
                x.Text = c.ClassName;
                proposedReturnValue.Add(x);
            }
            return proposedReturnValue;
        }

        List<SelectListItem> GetRaceItems(ContextBLL ctx)
        {
            List<SelectListItem> proposedReturnValue = new List<SelectListItem>();

            List<RaceBLL> races = ctx.GetRaces(0, 25);
            foreach (RaceBLL r in races)
            {
                SelectListItem x = new SelectListItem();

                x.Value = r.RaceID.ToString();
                x.Text = r.RaceName;
                proposedReturnValue.Add(x);
            }
            return proposedReturnValue;
        }

        public ActionResult Create(int id)
        {
            using (ContextBLL ctx = new ContextBLL())
            {

                ViewBag.Roles = GetRoleItems(ctx);
                ViewBag.Classes = GetClassItems(ctx);
                ViewBag.Races = GetRaceItems(ctx);
                RoleBLL role = ctx.FindRoleByRoleID(id);
                Multi Model = new Multi();
                if (role != null)
                {
                    Model.RoleID = role.RoleID;
                    Model.NewRoleName = "";
                }
                return View(Model);

                
            }
        }

        [HttpPost]
        public ActionResult Create(Multi collection)
        {
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    if (!ModelState.IsValid)
                    {
                        ViewBag.Roles = GetRoleItems(ctx);
                         ViewBag.Races = GetRaceItems(ctx);
                        ViewBag.Classes = GetClassItems(ctx);
                        return View(collection);
                    }

                   



                    int UserID = ctx.CreateUser(collection.UserName, collection.Email, collection.RoleID, collection.Password, collection.PasswordAgain,collection.NewRoleName);
                    
                    collection.UserID = ctx.CreateCharacter( UserID,collection.CharacterName,collection.ClassID,collection.RaceID,ctx.Roll(),ctx.Roll(),ctx.Roll(),ctx.Roll(),ctx.Roll(),ctx.Roll());
                }
                return RedirectToAction("Index", "User");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            
        }
    }
}