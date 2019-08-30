using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using CharacterGeneratorWeb.Models;

namespace CharacterGeneratorWeb
{
    [MustBeInRole(Roles = "Admin,VerifiedUser")]
    public class GenerateController : Controller 
    {
        // GET: Generate
        public ActionResult Index()
        {
            using (ContextBLL ctx = new ContextBLL())
            {
                
                //CharacterBLL defcharacter = new CharacterBLL();
                CharacterGeneratorModel defcharacter = new CharacterGeneratorModel();
            //defcharacter.CharacterID = 0;
            
            defcharacter.StrengthScore = ctx.Roll();
                defcharacter.DexterityScore = ctx.Roll();
                defcharacter.ConstitutionScore = ctx.Roll();
                defcharacter.IntelligenceScore = ctx.Roll();
                defcharacter.WisdomScore = ctx.Roll();
                defcharacter.CharismaScore = ctx.Roll();
                defcharacter.CharacterName = " ";
                var races = ctx.GetRaces(0,100);
                var classes = ctx.GetClasses(0, 100);
                SelectListItem blank = new SelectListItem();
                blank.Value = "0";
                blank.Text = " ";
                List<SelectListItem> RaceItems = new List<SelectListItem>();
                RaceItems.Add(blank);
                foreach (RaceBLL race  in races)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = race.RaceID.ToString();
                    item.Text = race.RaceName;
                    RaceItems.Add(item);
                }
                List<SelectListItem> ClassItems = new List<SelectListItem>();
                
                ClassItems.Add(blank);
                foreach (ClassBLL @class in classes)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = @class.ClassID.ToString();
                    item.Text = @class.ClassName;
                    ClassItems.Add(item);
                }
                defcharacter.ClassNames = ClassItems;
                defcharacter.RaceNames = RaceItems;
                return View("gen2", defcharacter);
            }
            
        }

        [HttpPost] 
        public ActionResult Index(CharacterGeneratorModel defcharacter)
        {
            using (ContextBLL ctx = new ContextBLL())
            {
                CharacterBLL temp = defcharacter.BaseCharacter;
                var RaceModifiers=
                ctx.GetRaceModifiersRelatedToRaces(defcharacter.RaceID, 0, 100);
                var ClassModifiers = ctx.GetClassModifiersRelatedToClasses(defcharacter.ClassID, 0, 100);
                //this is my meaningful calculation
                ctx.CharacterModification(temp, RaceModifiers);
                ctx.CharacterModification(temp, ClassModifiers);
                defcharacter.ModifiedCharacter = temp;
                defcharacter.CharacterName = " ";

                var races = ctx.GetRaces(0, 100);
                var classes = ctx.GetClasses(0, 100);
                SelectListItem blank = new SelectListItem();
                blank.Value = "0";
                blank.Text = " ";
                List<SelectListItem> RaceItems = new List<SelectListItem>();
                RaceItems.Add(blank);
                foreach (RaceBLL race in races)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = race.RaceID.ToString();
                    item.Text = race.RaceName;
                    RaceItems.Add(item);
                }
                List<SelectListItem> ClassItems = new List<SelectListItem>();

                ClassItems.Add(blank);
                foreach (ClassBLL @class in classes)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = @class.ClassID.ToString();
                    item.Text = @class.ClassName;
                    ClassItems.Add(item);
                }
                defcharacter.ClassNames = ClassItems;
                defcharacter.RaceNames = RaceItems;
                //defcharacter.ModifiedCharacter.CharacterName = defcharacter.CharacterName;
                //defcharacter.ModifiedCharacter.RaceID = defcharacter.RaceID;
                //defcharacter.ModifiedCharacter.ClassID = defcharacter.ClassID;
                return View("Preview", defcharacter);
            }
        }

        [HttpPost]
        public ActionResult Create(CharacterGeneratorModel charmodel)
        {
            try
            {
                CharacterBLL defcharacter;
                defcharacter = charmodel.ModifiedCharacter;
                
                using (ContextBLL ctx = new ContextBLL())
                {
                    UserBLL name = ctx.FindUserByUserName(User.Identity.Name);
                    if (null == name)
                    {
                        ViewBag.Exeption = new Exception($"ugh username isnt valid: {User.Identity.Name}");
                        return View("Error");
                    }
                    defcharacter.UserID = name.UserID;
                    ctx.CreateCharacter(defcharacter);
                }
                return RedirectToAction("MyIndex", "MyCharacter");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }
    }
}